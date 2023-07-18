import { plainColumnName } from 'tessa/workflow';
import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor,
  editDescriptor } from 'tessa/workflow/krProcess';

export class KrEditStageTypeFormatter extends KrStageTypeFormatter {

  private _changeState = plainColumnName('KrEditSettingsVirtual', 'ChangeState');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [editDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);
    context.displaySettings = context.stageRow.tryGet(this._changeState)
      ? '$UI_KrEdit_ChangeState'
      : '';
  }

}