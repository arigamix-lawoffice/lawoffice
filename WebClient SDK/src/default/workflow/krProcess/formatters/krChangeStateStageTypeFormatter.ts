import { plainColumnName } from 'tessa/workflow';
import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor,
  changesStateDescriptor } from 'tessa/workflow/krProcess';

export class KrChangeStateStageTypeFormatter extends KrStageTypeFormatter {

  private _settingsStateName = plainColumnName('KrChangeStateSettingsVirtual', 'StateName');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [changesStateDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    const state = context.stageRow.tryGet(this._settingsStateName);
    context.displaySettings = !state
      ? ''
      : state[0] === '$'
        ? `{$UI_KrChangeState_State}: {${state}}`
        : `{$UI_KrChangeState_State}: ${state}`;
  }

}