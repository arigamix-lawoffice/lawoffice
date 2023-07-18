import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import {
  CardButtonType,
  CardTaskCompletionOptionSettings,
  CardTaskDialogActionResult,
  systemKeyPrefix,
  Card,
  CardTaskDialogNewMethod,
  CardTaskDialogButtonInfo
} from 'tessa/cards';
import {
  IUIContext,
  showConfirm,
  showNotEmpty,
  UIButton,
  UIContext,
  createCardEditorModel,
  tryGetFromInfo
} from 'tessa/ui';
import {
  AdvancedCardDialogManager,
  CardEditorCreationContext,
  CardToolbarAction,
  ICardEditorModel
} from 'tessa/ui/cards';
import { createTypedField, DotNetType, Guid } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
import { IFile, setFileContentToInfo } from 'tessa/files';
import { CardRequestExtensionContext } from 'tessa/cards/extensions';
import { getTessaIcon } from 'common/utility';
import { ValidationResult, ValidationResultBuilder } from 'tessa/platform/validation';
import { ArgumentOutOfRangeError } from 'tessa/platform/errors';
import { CreateCardArg } from 'tessa/ui/uiHost/common';
import { MetadataStorage } from 'tessa/metadataStorage';

/**
 * Базовый класс обработчика клиентской команды отображения диалога.
 */
export abstract class AdvancedDialogCommandHandler extends ClientCommandHandlerBase {
  //#region methods

  async handle(context: IClientCommandHandlerContext): Promise<void> {
    const coSettings = this.prepareDialogCommand(context);
    if (!coSettings) {
      return;
    }

    // в ТК при вызове диалога через тайл или при нажатии на кнопку в тулбаре UIContext не сохраняется.
    // в ЛК UIContext, который создается в showGlobalDialogAsync, сохраняется и доступен в расширениях,
    // поэтому поведение в этой части расширения может отличаться.
    // Для того чтобы обойти эту проблему в WeAdvancedDialogCommandHandler и KrAdvancedDialogCommandHandler прокидывается флажок
    // Когда этот флажок есть в CardRequestExtensionContext (и только для этого типа реквестов), то мы пропускаем проверку UIContext.
    let skipEditor = false;
    if (context.outerContext && context.outerContext instanceof CardRequestExtensionContext) {
      skipEditor = tryGetFromInfo(
        context.outerContext.request.info,
        systemKeyPrefix + 'WebAdvancedDialogCommandSkipUIContextFlag',
        false
      );
    }

    const editor = UIContext.current.cardEditor;
    if (editor && !skipEditor) {
      editor.info[systemKeyPrefix + 'CardEditorCompletionOptionSettings'] = coSettings;
      editor.info[systemKeyPrefix + 'CardEditorCompletionOptionSettingsOnButtonPressed'] = async (
        dialogCardEditor: ICardEditorModel,
        cos: CardTaskCompletionOptionSettings,
        buttonName: string | null,
        completeTask: boolean
      ) =>
        await this.completeDialog(dialogCardEditor, editor, cos, buttonName, completeTask, context);
    } else {
      setTimeout(() => this.showGlobalDialog(coSettings, context));
    }
  }

  //#endregion

  //#region protected methods

  /**
   * Метод для подготовки диалога для выполнения.
   *
   * @param {IClientCommandHandlerContext} context Контекст обработки клиентской команды.
   * @returns {CardTaskCompletionOptionSettings | null} Информация для формирования диалога, или значение null, если невозможно сформировать диалог.
   */
  protected abstract prepareDialogCommand(
    context: IClientCommandHandlerContext
  ): CardTaskCompletionOptionSettings | null;

  /**
   * Метод выполнения диалога.
   *
   * @param {CardTaskDialogActionResult} actionResult Результат выполнения диалога.
   * @param {IClientCommandHandlerContext} context Контекст команды диалога.
   * @param {ICardEditorModel} cardEditor Редактор карточки диалога.
   * @param {(ICardEditorModel | null)} parentCardEditor Редактор карточки, для которой открывается диалог, если диалог открывается в рамках карточки.
   * @returns {Promise<boolean>} Значение true, если необходимо закрыть диалог, иначе false.
   */
  protected abstract completeDialogCore(
    actionResult: CardTaskDialogActionResult,
    context: IClientCommandHandlerContext,
    cardEditor: ICardEditorModel,
    parentCardEditor: ICardEditorModel | null
  ): Promise<boolean>;

  //#endregion

  //#region private methods

  /**
   * Отображает карточку в окне диалога.
   *
   * @param {CardTaskCompletionOptionSettings} coSettings Параметры диалога.
   * @param {IClientCommandHandlerContext} context Контекст обработки клиентской команды.
   */
  private async showGlobalDialog(
    coSettings: CardTaskCompletionOptionSettings,
    context: IClientCommandHandlerContext
  ): Promise<void> {
    const info = coSettings.info;
    if (coSettings.preparedNewCard && coSettings.preparedNewCardSignature) {
      info[systemKeyPrefix + 'NewBilletCard'] = createTypedField(
        coSettings.preparedNewCard,
        DotNetType.Binary
      );
      info[systemKeyPrefix + 'NewBilletCardSignature'] = createTypedField(
        coSettings.preparedNewCardSignature,
        DotNetType.Binary
      );
    }
    info[systemKeyPrefix + 'StoreMode'] = createTypedField(coSettings.storeMode, DotNetType.Int32);

    const contextInstance = UIContext.create(
      new UIContext({
        cardEditor: createCardEditorModel(),
        actionOverridings: AdvancedCardDialogManager.instance.createUIContextActionOverridings()
      })
    );
    try {
      await AdvancedDialogCommandHandler.createNewCard(
        coSettings,
        info,
        ctx => {
          ctx.editor.context.info[systemKeyPrefix + 'DialogClosingAction'] = async (
            uiCtx: IUIContext,
            args: { cancel: boolean }
          ) => {
            if (uiCtx.cardEditor!.operationInProgress) {
              args.cancel = true;
            } else if (
              !uiCtx.cardEditor!.isClosed &&
              (!coSettings.isCloseWithoutConfirmation ||
                uiCtx.cardEditor!.cardModel!.card.hasChanges())
            ) {
              // TODO: Keyboard.Modifiers.HasNot(ModifierKeys.Alt)
              const dialogResult = await showConfirm('$KrProcess_Dialog_ConfirmClose');
              if (!dialogResult) {
                args.cancel = true;
              }
            }
            return false;
          };
        },
        ctx => {
          // Подготовка диалога к использованию.
          this.prepareDialog(ctx.editor, coSettings, context);
        }
      );
    } finally {
      contextInstance.dispose();
    }
  }

  private prepareDialog(
    editor: ICardEditorModel,
    coSettings: CardTaskCompletionOptionSettings,
    context: IClientCommandHandlerContext
  ) {
    editor.statusBarIsVisible = false;

    editor.toolbar.clearItems();
    editor.bottomToolbar.clearItems();
    editor.bottomDialogButtons.length = 0;

    for (const actionInfo of coSettings.buttons) {
      const name = actionInfo.name;
      const icon = !!actionInfo.icon ? getTessaIcon(actionInfo.icon) : '';

      switch (actionInfo.cardButtonType) {
        case CardButtonType.BottomToolbarButton:
          editor.bottomToolbar.addItem(
            new CardToolbarAction({
              name,
              caption: actionInfo.caption,
              icon: icon,
              command: this.getButtonAction(editor, coSettings, context, actionInfo),
              order: actionInfo.order
            })
          );
          break;
        case CardButtonType.ToolbarButton:
          editor.toolbar.addItem(
            new CardToolbarAction({
              name,
              caption: actionInfo.caption,
              icon: icon,
              command: this.getButtonAction(editor, coSettings, context, actionInfo),
              order: actionInfo.order
            })
          );
          break;
        case CardButtonType.BottomDialogButton:
          editor.bottomDialogButtons.push(
            new UIButton(
              actionInfo.caption,
              this.getButtonAction(editor, coSettings, context, actionInfo)
            )
          );
          break;
        default:
          throw new ArgumentOutOfRangeError('actionInfo.cardButtonType', actionInfo.cardButtonType);
      }
    }
  }

  private getButtonAction(
    editor: ICardEditorModel,
    coSettings: CardTaskCompletionOptionSettings,
    context: IClientCommandHandlerContext,
    buttonInfo: CardTaskDialogButtonInfo
  ): () => Promise<void> {
    return buttonInfo.cancel
      ? async () => {
          await editor.close();
        }
      : async () =>
          await this.completeDialog(
            editor,
            null,
            coSettings,
            buttonInfo.name,
            buttonInfo.completeDialog,
            context
          );
  }

  private async completeDialog(
    dialogCardEditor: ICardEditorModel,
    parentCardEditor: ICardEditorModel | null,
    coSettings: CardTaskCompletionOptionSettings,
    buttonName: string | null,
    completeDialog: boolean,
    context: IClientCommandHandlerContext
  ) {
    let closeDialog = false;

    await dialogCardEditor.setOperationInProgress(async () => {
      const dialogCard = dialogCardEditor.cardModel!.card.clone();

      const validationResult = await AdvancedDialogCommandHandler.prepareFilesForStore(
        dialogCard,
        dialogCardEditor.cardModel!.fileContainer.files
      );

      await showNotEmpty(validationResult);
      if (validationResult.hasErrors) {
        return;
      }

      const actionResult = new CardTaskDialogActionResult();
      actionResult.mainCardId = Guid.empty;
      actionResult.pressedButtonName = buttonName;
      actionResult.storeMode = coSettings.storeMode;
      actionResult.keepFiles = coSettings.keepFiles;
      actionResult.completeDialog = completeDialog;
      actionResult.setDialogCard(dialogCard);

      closeDialog = await this.completeDialogCore(
        actionResult,
        context,
        dialogCardEditor,
        parentCardEditor
      );
    });

    if (closeDialog) {
      await dialogCardEditor.close();
    }
  }

  /**
   * Задаёт контент указанных файлов в соответствующие CardFile.Info карточки файлов.
   *
   * @param {Card} dialogCard Карточка диалога.
   * @param {Readonly<IFile[]>} files Коллекция файлов.
   * @returns {Promise<ValidationResult>} Результат выполнения операции, агрегированный для всех файлов.
   */
  private static async prepareFilesForStore(
    dialogCard: Card,
    files: Readonly<IFile[]>
  ): Promise<ValidationResult> {
    const validationResult = new ValidationResultBuilder();
    for (const file of files) {
      validationResult.add(await file.ensureContentModified());
    }

    if (validationResult.isSuccessful) {
      await setFileContentToInfo(dialogCard, files, validationResult);
    }
    return validationResult.build();
  }

  /**
   * Создаёт и открывает карточку в диалоге. Карточка создаётся в режиме по умолчанию или по шаблону.
   *
   * @param {CardTaskCompletionOptionSettings} dialogSettings {@link CardTaskCompletionOptionSettings}
   * @param {IStorage} info {@link IStorage}
   * @param {(context: CardEditorCreationContext) => void} cardEditorModifierAction
   * @param {(context: CardEditorCreationContext) => void} cardModifierAction
   */
  private static async createNewCard(
    dialogSettings: CardTaskCompletionOptionSettings,
    info: IStorage,
    cardEditorModifierAction: (context: CardEditorCreationContext) => void,
    cardModifierAction: (context: CardEditorCreationContext) => void
  ): Promise<void> {
    const cardNewMethod = dialogSettings.cardNewMethod;

    let dialogDisplayValue = dialogSettings.displayValue;
    if (
      cardNewMethod === CardTaskDialogNewMethod.Default &&
      (!dialogDisplayValue || dialogDisplayValue === '')
    ) {
      const dialogType = MetadataStorage.instance.cardMetadata.getCardTypeById(
        dialogSettings.dialogTypeId
      );
      if (dialogType) {
        dialogDisplayValue = dialogType.caption ?? undefined;
      }
    }

    const args: CreateCardArg = {
      cardTypeId: dialogSettings.dialogTypeId,
      info: info,
      displayValue: dialogDisplayValue,
      context: UIContext.current,
      cardEditorModifierAction: cardEditorModifierAction,
      cardModifierAction: (ctx: CardEditorCreationContext) => {
        ctx.editor.dialogName = dialogSettings.dialogName;
        cardModifierAction(ctx);
      },
      dialogOptions: {
        dialogName: dialogSettings.dialogName
      },
      saveCreationRequest: false
    };

    switch (cardNewMethod) {
      case CardTaskDialogNewMethod.Default:
        await AdvancedCardDialogManager.instance.createCard(args);
        break;

      case CardTaskDialogNewMethod.Template:
        await AdvancedCardDialogManager.instance.createFromTemplate(
          dialogSettings.dialogTypeId,
          args
        );
        break;

      default:
        throw new ArgumentOutOfRangeError(
          'CardTaskCompletionOptionSettings.cardNewMethod',
          cardNewMethod
        );
    }
  }

  //#endregion
}
