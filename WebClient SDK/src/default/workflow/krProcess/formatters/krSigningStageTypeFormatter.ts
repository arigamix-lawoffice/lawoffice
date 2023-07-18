import { plainColumnName } from 'tessa/workflow';
import {
  KrStageTypeFormatter,
  IKrStageTypeFormatterContext,
  StageTypeHandlerDescriptor,
  signingDescriptor
} from 'tessa/workflow/krProcess';

export class KrSigningStageTypeFormatter extends KrStageTypeFormatter {
  private _isParallel = plainColumnName('KrSigningStageSettingsVirtual', 'IsParallel');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [signingDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);
    context.displaySettings = context.stageRow.tryGet(this._isParallel)
      ? '$UI_KrApproval_Parallel'
      : '$UI_KrApproval_Sequential';
  }
}
