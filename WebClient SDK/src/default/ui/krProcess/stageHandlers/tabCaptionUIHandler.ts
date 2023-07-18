import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler } from 'tessa/workflow/krProcess';

import { MetadataStorage } from 'tessa';
import { TabContentIndicator } from '../../tabContentIndicator';
import { TabControlViewModel } from 'tessa/ui/cards/controls';

/**
 * UI обработчик этапов, настраивающий заголовки стандартных вкладок на форме с настройками этапа: Условие, Инициализация, Постобработка.
 */
export class TabCaptionUIHandler extends KrStageTypeUIHandler {
  //#region fields

  private _dispose: Function | null;

  //#endregion

  //#region base overrides

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    const control = context.rowModel.controls.get('CSharpSourceTable');
    if (control && control instanceof TabControlViewModel) {
      const sectionMeta = MetadataStorage.instance.cardMetadata.getSectionByName('KrStagesVirtual');

      if (!sectionMeta) {
        return;
      }

      const fieldIds: [string, string][] = sectionMeta.columns.map(x => [x.id || '', x.name || '']);

      const indicator = new TabContentIndicator(control, context.row.getStorage(), fieldIds, true);
      indicator.update();
      this._dispose = context.row.fieldChanged.addWithDispose(indicator.fieldChangedAction);
    }
  }

  public async finalize(): Promise<void> {
    if (this._dispose) {
      this._dispose();
      this._dispose = null;
    }
  }

  //#endregion
}
