import { IKrTileCommand } from './interfaces';
import { Guid } from 'tessa/platform';
import { IUIContext, LoadingOverlay, showConfirm, showNotEmpty } from 'tessa/ui';
import { KrProcessInstance, KrTileInfo, launchProcess } from 'tessa/workflow/krProcess';
import { ITile } from 'tessa/ui/tiles';

export class KrGlobalTileCommand implements IKrTileCommand {
  //#region ctor

  private constructor() {}

  //#endregion

  //#region instance

  private static _instance: KrGlobalTileCommand;

  public static get instance(): KrGlobalTileCommand {
    if (!KrGlobalTileCommand._instance) {
      KrGlobalTileCommand._instance = new KrGlobalTileCommand();
    }
    return KrGlobalTileCommand._instance;
  }

  //#endregion

  public async onClickAction(_context: IUIContext, _tile: ITile, tileInfo: KrTileInfo) {
    if (tileInfo.id === Guid.empty) {
      return;
    }

    if (tileInfo.askConfirmation) {
      const result = await showConfirm(tileInfo.confirmationMessage);
      if (!result) {
        return;
      }
    }

    await LoadingOverlay.instance.show(async () => {
      const process = KrProcessInstance.createWithParams({ processId: tileInfo.id });
      const result = await launchProcess(process, { raiseErrorWhenExecutionIsForbidden: true });
      if (result) {
        await showNotEmpty(result.validationResult.build());
      }
    });
  }
}
