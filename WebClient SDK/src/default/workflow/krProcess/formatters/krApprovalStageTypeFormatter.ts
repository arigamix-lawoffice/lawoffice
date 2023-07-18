import { plainColumnName } from 'tessa/workflow';
import {
  KrStageTypeFormatter,
  IKrStageTypeFormatterContext,
  StageTypeHandlerDescriptor,
  approvalDescriptor
} from 'tessa/workflow/krProcess';

export class KrApprovalStageTypeFormatter extends KrStageTypeFormatter {
  private _isAdvisory = plainColumnName('KrApprovalSettingsVirtual', 'Advisory');
  private _isParallel = plainColumnName('KrApprovalSettingsVirtual', 'IsParallel');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [approvalDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext): void {
    super.format(context);

    let sb = '';
    if (context.stageRow.tryGet(this._isAdvisory)) {
      sb += '{$UI_KrApproval_Advisory}\n\r';
    }

    sb += context.stageRow.tryGet(this._isParallel)
      ? '{$UI_KrApproval_Parallel}'
      : '{$UI_KrApproval_Sequential}';
    context.displaySettings = sb;
  }
}
