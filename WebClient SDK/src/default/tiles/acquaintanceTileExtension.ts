import { UIContext, showNotEmpty, createCardModel, UIButton } from 'tessa/ui';
import {
  TileExtension,
  ITileGlobalExtensionContext,
  Tile,
  TileGroups,
  TileEvaluationEventArgs,
  ITileLocalExtensionContext,
  disableWithCollapsing,
  openMarkedCard
} from 'tessa/ui/tiles';
import { showView, showFormDialog } from 'tessa/ui/uiHost';
import { ICardModel, ICardEditorModel } from 'tessa/ui/cards';
import { CardStoreMode, CardRowState, CardRow } from 'tessa/cards';
import { CardRequest, CardService, CardNewRequest } from 'tessa/cards/service';
import { CardTypeFlags } from 'tessa/cards/types';
import { RequestParameterBuilder } from 'tessa/views';
import { ViewParameterMetadata, equalsCriteriaOperator } from 'tessa/views/metadata';
import { hasFlag, TypedField, DotNetType, createTypedField, Guid } from 'tessa/platform';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';
import {
  KrTypesCache,
  KrComponents,
  KrToken,
  getKrComponentsByCard,
  CreateResolutions
} from 'tessa/workflow';
import { LocalizationManager } from 'tessa/localization';
import { MetadataStorage } from 'tessa';
import { SchemeType } from 'tessa/scheme';

export class AcquaintanceTileExtension extends TileExtension {
  //#region TileExtension

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const panel = context.workspace.leftPanel;
    const contextSource = panel.contextSource;

    const tile = new Tile({
      name: 'AcquaintanceGroup',
      caption: '$KrTiles_AcquaintanceGroup',
      icon: 'ta icon-thin-083',
      contextSource,
      group: TileGroups.Cards,
      order: 28,
      evaluating: AcquaintanceTileExtension.enableOnCardUpdateAndNotTaskCard,
      tiles: [
        new Tile({
          name: 'Acquaintance',
          caption: '$KrTiles_Acquaintance',
          icon: 'ta icon-thin-083',
          contextSource,
          command: AcquaintanceTileExtension.showAcquaintanceWindow,
          group: TileGroups.Cards,
          order: 1,
          evaluating: AcquaintanceTileExtension.enableOnCardUpdateAndNotTaskCard
        }),

        new Tile({
          name: 'AcquaintanceHistory',
          caption: '$KrTiles_AcquaintanceHistory',
          icon: 'ta icon-thin-084',
          contextSource,
          command: AcquaintanceTileExtension.openAcquaintanceHistoryView,
          group: TileGroups.Cards,
          order: 2,
          evaluating: AcquaintanceTileExtension.enableOnCardUpdateAndNotTaskCard
        })
      ]
    });

    tile.info['.actionsGrouping'] = true;
    panel.tiles.push(tile);
  }

  public initializingLocal(context: ITileLocalExtensionContext) {
    const panel = context.workspace.leftPanel;
    const editor = panel.context.cardEditor;
    const acquaintanceGroup = panel.tryGetTile('AcquaintanceGroup');
    if (
      !!editor &&
      !!editor.cardModel &&
      !!acquaintanceGroup &&
      (!AcquaintanceTileExtension.typeSupportsWorkflow(editor.cardModel) ||
        !AcquaintanceTileExtension.canUseResolutions(editor.cardModel))
    ) {
      disableWithCollapsing(acquaintanceGroup);
    }
  }

  //#endregion

  //#region evaluatings

  private static enableOnCardUpdateAndNotTaskCard(e: TileEvaluationEventArgs) {
    const editor = e.currentTile.context.cardEditor;
    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor &&
        !!editor.cardModel &&
        editor.cardModel.card.storeMode === CardStoreMode.Update &&
        editor.cardModel.cardType.id !== 'de75a343-8164-472d-a20e-4937819760ac'
    ); // WfTaskCard
  }

  //#endregion

  //#region actions

  private static async showAcquaintanceWindow() {
    const context = UIContext.current;
    const cardEditor = context.cardEditor;

    if (!cardEditor || !cardEditor.cardModel) {
      return;
    }

    const mainCardModel = cardEditor.cardModel;
    const cardIsNew = mainCardModel.card.storeMode === CardStoreMode.Insert;

    if (
      !cardIsNew &&
      ((await mainCardModel.hasChanges()) ||
        AcquaintanceTileExtension.notEnoughPermissions(mainCardModel))
    ) {
      await openMarkedCard(
        'kr_calculate_resolution_permissions',
        null, // Не требуем подтверждения действия, если не было изменений
        async () => true, // Автоматом подтверждаем сохранение
        async () => {
          if (AcquaintanceTileExtension.notEnoughPermissions(cardEditor.cardModel!)) {
            await showNotEmpty(
              ValidationResult.fromText(
                `${LocalizationManager.instance.localize('$KrMessages_NoPermissionsTo')}\
               ${LocalizationManager.instance.localize('$KrPermissions_CreateResolutions')}`,
                ValidationResultType.Error
              )
            );
            return false;
          }

          AcquaintanceTileExtension.openRolesDialog(cardEditor);
          return true;
        }
      );
    } else {
      AcquaintanceTileExtension.openRolesDialog(cardEditor);
    }
  }

  private static openAcquaintanceHistoryView() {
    const context = UIContext.current;
    const cardEditor = context.cardEditor;

    if (cardEditor && cardEditor.cardModel) {
      const cardId = cardEditor.cardModel.card.id;

      const parameterMetadata = new ViewParameterMetadata();
      parameterMetadata.alias = 'CardIDParam';
      parameterMetadata.caption = '$Views_Acquaintance_CardID';
      parameterMetadata.hidden = true;
      parameterMetadata.schemeType = SchemeType.Guid;
      parameterMetadata.multiple = false;

      const parameters = [
        new RequestParameterBuilder()
          .withMetadata(parameterMetadata)
          .addCriteria(equalsCriteriaOperator(), cardId, cardId)
          .asRequestParameter()
      ];

      showView({
        viewAlias: 'AcquaintanceHistory',
        displayValue: '$Views_AcquaintanceHistory',
        parameters
      });
    }
  }

  private static canUseResolutions(model: ICardModel): boolean {
    const usedComponents = getKrComponentsByCard(model.card, KrTypesCache.instance);
    return hasFlag(usedComponents, KrComponents.Resolutions);
  }

  private static typeSupportsWorkflow(model: ICardModel): boolean {
    return (
      hasFlag(model.cardType.flags, CardTypeFlags.AllowTasks) &&
      KrTypesCache.instance.cardTypes.some(x => x.id === model.cardType.id)
    );
  }

  private static notEnoughPermissions(model: ICardModel): boolean {
    const krToken = KrToken.tryGet(model.card.info);
    return !!krToken && !krToken.hasPermission(CreateResolutions);
  }

  private static async openRolesDialog(editor: ICardEditorModel) {
    const model = editor.cardModel;
    if (!model) {
      return;
    }

    // Запрашиваем информируемых по умолчанию
    const defaultRolesRequest = new CardRequest();
    defaultRolesRequest.requestType = '7a2cb692-7a55-4519-b193-0ce2de435523'; // GetDefaultAcquaintanceRoles
    defaultRolesRequest.cardId = model.card.id;

    const defaultRolesResponse = await CardService.instance.request(defaultRolesRequest);
    const defaultRolesResult = defaultRolesResponse.validationResult.build();
    await showNotEmpty(defaultRolesResult);
    if (!defaultRolesResult.isSuccessful) {
      return;
    }

    // Построение диалога
    const dialogsType = MetadataStorage.instance.cardMetadata.getCardTypeByName('Dialogs');
    if (!dialogsType) {
      return;
    }

    const dialogForm = dialogsType.forms.find(x => x.name === 'Acquaintance');
    if (!dialogForm) {
      return;
    }

    const request = new CardNewRequest();
    request.cardTypeId = dialogsType.id;
    const response = await CardService.instance.new(request);
    response.card.id = Guid.newGuid();
    await showNotEmpty(response.validationResult.build());
    if (!response.validationResult.isSuccessful) {
      return;
    }

    const windowCardModel = createCardModel(response.card, response.sectionRows);

    // Получаем список ролей для ознакомления по умолчанию
    const defaultRoles: { id: guid; name: string }[] = [];
    const defaultRolesResponseInfo = defaultRolesResponse.info;
    const defaultRolesDictionary = defaultRolesResponseInfo['.DefaultRoles'];
    if (defaultRolesDictionary) {
      const idsList = defaultRolesDictionary['IDList'] as TypedField<DotNetType.Guid>[];
      const namesList = defaultRolesDictionary['NameList'] as TypedField<DotNetType.String>[];
      if (idsList && namesList && idsList.length === namesList.length) {
        for (let i = 0; i < idsList.length; i++) {
          defaultRoles.push({
            id: idsList[i].$value,
            name: namesList[i].$value
          });
        }
      }
    }

    const rolesRows = windowCardModel.card.sections.get('DialogRoles')!.rows;
    for (const role of defaultRoles) {
      const roleRow = new CardRow();
      roleRow.rowId = Guid.newGuid();
      roleRow.set('RoleID', role.id, DotNetType.Guid);
      roleRow.set('RoleName', role.name, DotNetType.String);
      roleRow.state = CardRowState.None;
      rolesRows.push(roleRow);
    }

    await showFormDialog(dialogForm, windowCardModel, null, [
      new UIButton('$UI_Common_OK', async btn => {
        if (rolesRows.length === 0) {
          await showNotEmpty(
            ValidationResult.fromText(
              '$KrMessages_Acquaintance_RolesRequired',
              ValidationResultType.Warning
            )
          );
          return;
        }

        // Получаем комментарий
        const comment = windowCardModel.card.sections.get('Dialogs')!.fields.get('Comment') || '';

        const card = model.card;
        const cardId = card.id;

        // Запрос на отправку данных для массового ознакомления
        const sendAcquaintanceRequest = new CardRequest();
        sendAcquaintanceRequest.requestType = '87e36c4a-0cb5-4226-8580-c339b4bbf2b7'; // Acquaintance
        sendAcquaintanceRequest.cardId = cardId;

        sendAcquaintanceRequest.info['.Comment'] = createTypedField(comment, DotNetType.String);
        sendAcquaintanceRequest.info['.Roles'] = rolesRows.map(x => x.getField('RoleID')!);
        sendAcquaintanceRequest.info['.ExcludeDeputies'] = createTypedField(
          false,
          DotNetType.Boolean
        );
        sendAcquaintanceRequest.info['.AddSuccessMessage'] = createTypedField(
          true,
          DotNetType.Boolean
        );
        // sendAcquaintanceRequest.info['.PlaceholderAliases']
        // sendAcquaintanceRequest.info['.AdditionalInfo'];

        const sendAcquaintanceResponse = await CardService.instance.request(
          sendAcquaintanceRequest
        );
        await showNotEmpty(sendAcquaintanceResponse.validationResult.build());
        if (!sendAcquaintanceResponse.validationResult.isSuccessful) {
          return;
        }

        btn.close();
      }),
      new UIButton('$UI_Common_Cancel', btn => {
        btn.close();
      })
    ]);
  }

  //#endregion
}
