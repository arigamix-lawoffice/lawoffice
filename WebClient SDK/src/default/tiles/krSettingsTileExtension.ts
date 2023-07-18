import {
  TileExtension,
  ITileLocalExtensionContext,
  Tile,
  TileGroups,
  TileEvaluationEventArgs
} from 'tessa/ui/tiles';
import { userSession } from 'common/utility';
import {
  UIContext,
  UIButton,
  showError,
  showMessage,
  showConfirm,
  showLoadingOverlay,
  showNotEmpty
} from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
import { showFormDialog } from 'tessa/ui/uiHost';
import { DotNetType, createTypedField } from 'tessa/platform';
import { CardSection } from 'tessa/cards';
import { localize } from 'tessa/localization/localize';
import { CardRequest, CardService, CardResponse } from 'tessa/cards/service';

export class KrSettingsTileExtension extends TileExtension {
  public initializingLocal(context: ITileLocalExtensionContext): void {
    const panel = context.workspace.leftPanel;
    panel.tiles.push(
      new Tile({
        name: 'GenerateTestCards',
        caption: '$KrTiles_GenerateTestCards',
        icon: 'ta icon-thin-001',
        contextSource: panel.contextSource,
        command: KrSettingsTileExtension.generateAction,
        group: TileGroups.Cards,
        order: 10,
        evaluating: KrSettingsTileExtension.enableOnSettingsCardAndAdministrator
      })
    );
  }

  private static enableOnSettingsCardAndAdministrator(e: TileEvaluationEventArgs) {
    const editor = e.current.context.cardEditor;

    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor &&
        !!editor.cardModel &&
        editor.cardModel.cardType.id === '35a03878-57b6-4263-ae36-92eb59032132' && // KrSettingsTypeID
        userSession.isAdmin
    );
  }

  private static async generateAction() {
    const editor = UIContext.current.cardEditor;
    let model: ICardModel;
    if (editor && (model = editor.cardModel!)) {
      const section = model.card.sections.get('KrCardGeneratorVirtual')!;
      const form = model.cardType.forms.find(x => x.name === 'Generator')!;

      await showFormDialog(
        form,
        model,
        () => {
          section.fields.set('UserCount', 0, DotNetType.Int32);
          section.fields.set('PartnerCount', 0, DotNetType.Int32);
        },
        [
          new UIButton('$KrTiles_CreateCardsButton', btn =>
            KrSettingsTileExtension.generateButtonAction(section, () => btn.close())
          ),
          new UIButton('$UI_Common_Cancel', btn => btn.close())
        ]
      );

      section.clearChanges();
    }
  }

  private static async generateButtonAction(section: CardSection, closeAction: () => void) {
    const fields = section.fields;
    const userCount = fields.get('UserCount');
    if (userCount < 0) {
      await showError('$KrTiles_WarnUserCountNegative');
      return false;
    }

    const partnerCount = fields.get('PartnerCount');
    if (partnerCount < 0) {
      await showError('$KrTiles_WarnPartnerCountNegative');
      return false;
    }

    if (userCount === 0 && partnerCount === 0) {
      await showMessage('$KrTiles_WarnCardsCountNotDefined');
      return false;
    }

    const text =
      userCount === 0
        ? `${localize('$KrTiles_CreatePartnersConfirmation')} ${partnerCount}. ${localize(
            '$UI_Common_ContinueConfirmation'
          )}`
        : partnerCount === 0
        ? `${localize('$KrTiles_CreateUsersConfirmation')} ${userCount}. ${localize(
            '$UI_Common_ContinueConfirmation'
          )}`
        : `${localize('$KrTiles_CreatePartnersConfirmation')} ${partnerCount}.\n` +
          `${localize('$KrTiles_CreateUsersConfirmation')} ${userCount}.\n` +
          `${localize('$UI_Common_ContinueConfirmation')}`;

    if (!(await showConfirm(text))) {
      return false;
    }

    if (
      userCount > 1000 &&
      !(await showConfirm(
        `${localize('$KrTiles_WarnTooMuchUsers')} ${userCount}. ${localize(
          '$UI_Common_ContinueConfirmation'
        )}`,
        '$UI_Common_Attention'
      ))
    ) {
      return false;
    }

    if (
      partnerCount > 1000 &&
      !(await showConfirm(
        `${localize('$KrTiles_WarnTooMuchPartners')} ${partnerCount}. ${localize(
          '$UI_Common_ContinueConfirmation'
        )}`,
        '$UI_Common_Attention'
      ))
    ) {
      return false;
    }

    closeAction();

    let response!: CardResponse;
    await showLoadingOverlay(async () => {
      const request = new CardRequest();
      request.requestType = '207e75b5-abb8-403a-a12a-897019afccf6'; // TestData
      request.info = {
        UserCount: createTypedField(userCount, DotNetType.Int32),
        PartnerCount: createTypedField(partnerCount, DotNetType.Int32)
      };
      response = await CardService.instance.request(request);
    });

    const result = response.validationResult.build();
    await showNotEmpty(result);

    return true;
  }
}
