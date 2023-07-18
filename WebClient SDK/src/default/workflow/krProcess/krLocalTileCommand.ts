import { IKrTileCommand } from './interfaces';
import { IUIContext, showConfirm } from 'tessa/ui';
import {
  KrProcessInstance,
  KrTileInfo,
  launchProcessWithCardEditor
} from 'tessa/workflow/krProcess';
import { Guid } from 'tessa/platform';
import { ICardModel, CardSavingRequest, CardSavingMode } from 'tessa/ui/cards';
import { ITile } from 'tessa/ui/tiles';

export class KrLocalTileCommand implements IKrTileCommand {
  //#region ctor

  private constructor() {}

  //#endregion

  //#region instance

  private static _instance: KrLocalTileCommand;

  public static get instance(): KrLocalTileCommand {
    if (!KrLocalTileCommand._instance) {
      KrLocalTileCommand._instance = new KrLocalTileCommand();
    }
    return KrLocalTileCommand._instance;
  }

  //#endregion

  public async onClickAction(context: IUIContext, _tile: ITile, tileInfo: KrTileInfo) {
    const cardEditor = context.cardEditor;
    let model: ICardModel;
    if (tileInfo.id === Guid.empty || !cardEditor || !(model = cardEditor.cardModel!)) {
      return;
    }

    if (tileInfo.askConfirmation) {
      const result = await showConfirm(tileInfo.confirmationMessage);
      if (!result) {
        return;
      }
    }

    await cardEditor.setOperationInProgress(async () => {
      if (await model.hasChanges()) {
        if (
          !(await cardEditor.saveCard(
            context,
            undefined,
            new CardSavingRequest(CardSavingMode.RefreshOnSuccess)
          ))
        ) {
          return;
        }
      }
      const process = KrProcessInstance.createWithParams({
        processId: tileInfo.id,
        processInfo: {},
        cardId: context.cardEditor!.cardModel!.card.id
      });

      await launchProcessWithCardEditor(process, cardEditor, true);
    });
  }
}
