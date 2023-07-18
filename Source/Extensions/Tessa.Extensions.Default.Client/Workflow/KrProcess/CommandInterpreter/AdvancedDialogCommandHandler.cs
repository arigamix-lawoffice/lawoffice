#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Extensions.Platform.Client.UI;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Базовый класс обработчика клиентской команды отображения диалога.
    /// </summary>
    public abstract class AdvancedDialogCommandHandler : ClientCommandHandlerBase
    {
        #region Fields

        private readonly Func<IAdvancedCardDialogManager> createAdvancedCardDialogManagerFunc;

        private readonly ISession session;

        private readonly ICardMetadata cardMetadata;
        
        #endregion

        #region Constructor

        protected AdvancedDialogCommandHandler(
            Func<IAdvancedCardDialogManager> createAdvancedCardDialogManagerFunc,
            ISession session,
            ICardMetadata cardMetadata)
        {
            this.createAdvancedCardDialogManagerFunc = NotNullOrThrow(createAdvancedCardDialogManagerFunc);
            this.session = NotNullOrThrow(session);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task Handle(
            IClientCommandHandlerContext context)
        {
            var coSettings = this.PrepareDialogCommand(context);

            if (coSettings is null)
            {
                return Task.CompletedTask;
            }

            var editor = UIContext.Current.CardEditor;
            if (editor is not null)
            {
                editor.Info[CardTaskDialogUIExtension.DialogNonTaskCompletionOptionSettingsKey] = coSettings;
                editor.Info[CardTaskDialogUIExtension.OnDialogButtonPressedKey] =
                    (Func<ICardEditorModel, CardTaskCompletionOptionSettings, string?, bool, ValueTask>) (
                        async (
                            dialogCardEditor,
                            cos,
                            buttonName,
                            completeTask) => await this.CompleteDialogAsync(
                                dialogCardEditor,
                                editor,
                                cos,
                                buttonName,
                                completeTask,
                                context));
            }
            else
            {
                _ = this.ShowGlobalDialogAsync(
                    coSettings,
                    context);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Protected Abstract Methods

        /// <summary>
        /// Метод для подготовки диалога для выполнения.
        /// </summary>
        /// <param name="context">Контекст обработки клиентской команды.</param>
        /// <returns>Информация для формирования диалога, или значение <see langword="null"/>, если невозможно сформировать диалог.</returns>
        protected abstract CardTaskCompletionOptionSettings? PrepareDialogCommand(IClientCommandHandlerContext context);

        /// <summary>
        /// Метод выполнения диалога.
        /// </summary>
        /// <param name="actionResult">Результат выполнения диалога.</param>
        /// <param name="context">Контекст команды диалога.</param>
        /// <param name="cardEditor">Редактор карточки диалога.</param>
        /// <param name="parentCardEditor">Редактор карточки, для которой открывается диалог, если диалог открывается в рамках карточки.</param>
        /// <returns>Значение <see langword="true"/>, если необходимо закрыть диалог, иначе - <see langword="false"/>.</returns>
        protected abstract ValueTask<bool> CompleteDialogCoreAsync(
            CardTaskDialogActionResult actionResult,
            IClientCommandHandlerContext context,
            ICardEditorModel cardEditor,
            ICardEditorModel? parentCardEditor = null);

        #endregion

        #region Private Methods

        /// <summary>
        /// Отображает карточку в окне диалога.
        /// </summary>
        /// <param name="coSettings">Параметры диалога.</param>
        /// <param name="context">Контекст обработки клиентской команды.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task ShowGlobalDialogAsync(
            CardTaskCompletionOptionSettings coSettings,
            IClientCommandHandlerContext context)
        {
            var info = coSettings.Info;
            if (coSettings.PreparedNewCard is not null
                && coSettings.PreparedNewCardSignature is not null)
            {
                info[CardHelper.NewCardBilletKey] = coSettings.PreparedNewCard;
                info[CardHelper.NewCardBilletSignatureKey] = coSettings.PreparedNewCardSignature;
            }

            info[CardTaskDialogHelper.StoreMode] = Int32Boxes.Box((int) coSettings.StoreMode);

            await this.CreateNewCardAsync(
                this.createAdvancedCardDialogManagerFunc(),
                coSettings,
                info,
                async ctx =>
                {
                    ctx.Editor.Context.SetDialogClosingAction((uiCtx, eventArgs) =>
                    {
                        if (uiCtx.CardEditor.OperationInProgress)
                        {
                            eventArgs.Cancel = true;
                        }
                        else if (!uiCtx.CardEditor.IsClosed
                                && (!coSettings.IsCloseWithoutConfirmation
                                || uiCtx.CardEditor.CardModel.Card.HasChanges()))
                        {
                            var dialogResult = Keyboard.Modifiers.HasNot(ModifierKeys.Alt)
                                && TessaDialog.Confirm("$KrProcess_Dialog_ConfirmClose");

                            if (!dialogResult)
                            {
                                eventArgs.Cancel = true;
                            }
                        }

                        return TaskBoxes.False;
                    });
                },
                ctx =>
                {
                    // Подготовка диалога к использованию.
                    this.PrepareDialog(ctx.Editor, coSettings, context);

                    return ValueTask.CompletedTask;
                },
                CancellationToken.None).ConfigureAwait(false);
        }

        private void PrepareDialog(
           ICardEditorModel editor,
           CardTaskCompletionOptionSettings coSettings,
           IClientCommandHandlerContext context)
        {
            editor.StatusBarIsVisible = false;

            var toolbarActions = editor.Toolbar.Actions;
            var bottomActions = editor.BottomToolbar.Actions;
            var bottomButtons = editor.BottomDialogButtons;

            toolbarActions.Clear();
            bottomActions.Clear();
            bottomButtons.Clear();

            foreach (var actionInfo in coSettings.Buttons)
            {
                var name = actionInfo.Name;
                var icon = string.IsNullOrEmpty(actionInfo.Icon)
                    ? null
                    : editor.Toolbar.CreateIcon(actionInfo.Icon);

                bool CanExecuteButtonAction(object? _)
                {
                    return !editor.BlockingOperationInProgress;
                }

                switch (actionInfo.CardButtonType)
                {
                    case CardButtonType.ToolbarButton:
                        var topActionAsync = this.GetButtonActionAsync(editor, coSettings, context, actionInfo);
                        toolbarActions.Add(new CardToolbarAction(
                            name,
                            actionInfo.Caption,
                            icon,
                            // ReSharper disable once AccessToModifiedClosure
                            new DelegateCommand(async _ => await topActionAsync(null), CanExecuteButtonAction),
                            order: actionInfo.Order
                        ));
                        break;
                    case CardButtonType.BottomToolbarButton:
                        var bottomActionAsync = this.GetButtonActionAsync(editor, coSettings, context, actionInfo);
                        bottomActions.Add(new CardToolbarAction(
                            name,
                            actionInfo.Caption,
                            icon,
                            // ReSharper disable once AccessToModifiedClosure
                            new DelegateCommand(async _ => await bottomActionAsync(null), CanExecuteButtonAction),
                            order: actionInfo.Order
                        ));
                        break;
                    case CardButtonType.BottomDialogButton:
                        bottomButtons.Add(new UIButton(
                            actionInfo.Caption,
                            this.GetButtonActionAsync(editor, coSettings, context, actionInfo),
                            () => CanExecuteButtonAction(null)
                        ));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"{nameof(actionInfo)}.{nameof(actionInfo.CardButtonType)}",
                            actionInfo.CardButtonType,
                            null);
                }
            }
        }

        private Func<UIButton?, ValueTask> GetButtonActionAsync(
            ICardEditorModel editor,
            CardTaskCompletionOptionSettings coSettings,
            IClientCommandHandlerContext context,
            CardTaskDialogButtonInfo buttonInfo)
        {
            return buttonInfo.Cancel
                ? async _ => await editor.CloseAsync()
                : async _ => await this.CompleteDialogAsync(
                    editor,
                    null,
                    coSettings,
                    buttonInfo.Name,
                    buttonInfo.CompleteDialog,
                    context);
        }

        private async Task CompleteDialogAsync(
            ICardEditorModel dialogCardEditor,
            ICardEditorModel? parentCardEditor,
            CardTaskCompletionOptionSettings coSettings,
            string? buttonName,
            bool completeDialog,
            IClientCommandHandlerContext context)
        {
            var closeDialog = false;
            using (dialogCardEditor.SetOperationInProgress(true))
            {
                var dialogCard = dialogCardEditor.CardModel.Card.Clone();
                var files = dialogCardEditor.CardModel.FileContainer.Files;

                var validationResult = await this.PrepareFilesForStoreAsync(
                    dialogCard,
                    files);

                await TessaDialog.ShowNotEmptyAsync(validationResult);
                if (validationResult.HasErrors)
                {
                    return;
                }

                var actionResult = new CardTaskDialogActionResult
                {
                    MainCardID = Guid.Empty,
                    PressedButtonName = buttonName,
                    StoreMode = coSettings.StoreMode,
                    KeepFiles = coSettings.KeepFiles,
                    CompleteDialog = completeDialog,
                };
                actionResult.SetDialogCard(dialogCard);

                closeDialog = await this.CompleteDialogCoreAsync(
                    actionResult,
                    context,
                    dialogCardEditor,
                    parentCardEditor);
            }
            if (closeDialog)
            {
                await dialogCardEditor.CloseAsync();
            }
        }

        /// <summary>
        /// Асинхронно задаёт контент указанных файлов в соответствующие <see cref="CardFile.Info"/> карточки файлов.
        /// </summary>
        /// <param name="dialogCard">Карточка диалога.</param>
        /// <param name="files">Коллекция файлов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат выполнения операции, агрегированный для всех файлов.</returns>
        private async Task<ValidationResult> PrepareFilesForStoreAsync(
            Card dialogCard,
            ICollection<IFile> files,
            CancellationToken cancellationToken = default)
        {
            var validationResults = new ValidationResultBuilder
            {
                await files.EnsureAllContentModifiedAsync(cancellationToken)
            };

            if (validationResults.IsSuccessful())
            {
                await CardTaskDialogHelper.SetFileContentToInfoAsync(
                    dialogCard,
                    files,
                    this.session,
                    validationResults,
                    cancellationToken);
            }

            return validationResults.Build();
        }

        /// <summary>
        /// Асинхронно создаёт и открывает карточку в диалоге. Карточка создаётся в режиме по умолчанию или по шаблону.
        /// </summary>
        /// <param name="advancedCardDialogManager"><inheritdoc cref="IAdvancedCardDialogManager" path="/summary"/></param>
        /// <param name="completionOptionSettings"><inheritdoc cref="CardTaskCompletionOptionSettings" path="/summary"/></param>
        /// <param name="info"><inheritdoc cref="CreateCardOptions.Info" path="/summary"/></param>
        /// <param name="cardEditorModifierActionAsync"><inheritdoc cref="CreateCardOptions.CardEditorModifierActionAsync" path="/summary"/></param>
        /// <param name="cardModifierActionAsync"><inheritdoc cref="CreateCardOptions.CardModifierActionAsync" path="/summary"/></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task CreateNewCardAsync(
            IAdvancedCardDialogManager advancedCardDialogManager,
            CardTaskCompletionOptionSettings completionOptionSettings,
            Dictionary<string, object?>? info = null,
            CardEditorCreationActionAsync? cardEditorModifierActionAsync = null,
            CardEditorCreationActionAsync? cardModifierActionAsync = null,
            CancellationToken cancellationToken = default)
        {
            var dialogDisplayValue = completionOptionSettings.DisplayValue;
            if (completionOptionSettings.CardNewMethod == CardTaskDialogNewMethod.Default &&
                string.IsNullOrEmpty(dialogDisplayValue) &&
                (await this.cardMetadata.GetCardTypesAsync(cancellationToken)).TryGetValue(completionOptionSettings.DialogTypeID, out var dialogType))
            {
                dialogDisplayValue = dialogType.Caption;
            }
            
            var createCardOptions = new CreateCardOptions
            {
                Info = info,
                DisplayValue = dialogDisplayValue,
                UIContext = UIContext.Current,
                CardEditorModifierActionAsync = cardEditorModifierActionAsync,
                CardModifierActionAsync = async ctx =>
                {
                    ctx.Editor.DialogName = completionOptionSettings.DialogName;

                    if (cardModifierActionAsync is not null)
                    {
                        await cardModifierActionAsync(ctx);
                    }
                },
                SaveCreationRequest = false,
            };

            switch (completionOptionSettings.CardNewMethod)
            {
                case CardTaskDialogNewMethod.Default:
                    await advancedCardDialogManager.CreateCardAsync(
                        completionOptionSettings.DialogTypeID,
                        options: createCardOptions,
                        cancellationToken: cancellationToken).ConfigureAwait(false);
                    break;
                case CardTaskDialogNewMethod.Template:
                    await advancedCardDialogManager.CreateFromTemplateAsync(
                        completionOptionSettings.DialogTypeID,
                        createCardOptions,
                        cancellationToken).ConfigureAwait(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(CardTaskCompletionOptionSettings.CardNewMethod),
                        completionOptionSettings.CardNewMethod,
                        default);
            }
        }

        #endregion

    }
}
