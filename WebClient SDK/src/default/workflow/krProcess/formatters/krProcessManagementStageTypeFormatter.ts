import { plainColumnName } from 'tessa/workflow';
import {
  KrStageTypeFormatter,
  IKrStageTypeFormatterContext,
  StageTypeHandlerDescriptor,
  processManagementDescriptor
} from 'tessa/workflow/krProcess';

export class KrProcessManagementStageTypeFormatter extends KrStageTypeFormatter {
  private _managePrimaryProcess = plainColumnName(
    'KrProcessManagementStageSettingsVirtual',
    'ManagePrimaryProcess'
  );
  private _modeId = plainColumnName('KrProcessManagementStageSettingsVirtual', 'ModeID');
  private _modeName = plainColumnName('KrProcessManagementStageSettingsVirtual', 'ModeName');
  private _stageName = plainColumnName('KrProcessManagementStageSettingsVirtual', 'StageName');
  private _groupName = plainColumnName('KrProcessManagementStageSettingsVirtual', 'StageGroupName');
  private _groupRowName = plainColumnName(
    'KrProcessManagementStageSettingsVirtual',
    'StageRowGroupName'
  );
  private _signal = plainColumnName('KrProcessManagementStageSettingsVirtual', 'Signal');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [processManagementDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext): void {
    const managePrimaryProcess = context.stageRow.tryGet(this._managePrimaryProcess) || false;
    const modeId = context.stageRow.tryGet(this._modeId);
    const modeName = context.stageRow.tryGet(this._modeName);
    let stageName = context.stageRow.tryGet(this._stageName);
    let groupName = context.stageRow.tryGet(this._groupName);
    let groupRowName = context.stageRow.tryGet(this._groupRowName);
    const signal = context.stageRow.tryGet(this._signal);

    let builder = '';

    if (modeName) {
      builder += `{${modeName}}\n`;
    }

    if (modeId === 0 && stageName) {
      if (stageName[0] === '$') {
        stageName = `{${stageName}}`;
      }

      if (groupRowName) {
        groupRowName = groupRowName[0] === '$' ? ` ({${groupRowName}})` : ` (${groupRowName})`;
      }

      builder += `${stageName + groupRowName}\n`;
    } else if (modeId === 1 && groupName) {
      if (groupName[0] === '$') {
        groupName = `{${groupName}}`;
      }

      builder += `${groupName}\n`;
    } else if (modeId === 5 && signal) {
      builder += `${signal}\n`;
    }

    if (managePrimaryProcess) {
      builder += '{$CardTypes_Controls_ManagePrimaryProcess}\n';
    }

    context.displaySettings = builder;
  }
}
