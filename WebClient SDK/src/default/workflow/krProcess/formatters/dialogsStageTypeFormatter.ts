import { plainColumnName } from 'tessa/workflow';
import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor,
  dialogDescriptor} from 'tessa/workflow/krProcess';

export class DialogsStageTypeFormatter extends KrStageTypeFormatter {

  private _kindCaption = plainColumnName('KrTaskKindSettingsVirtual', 'KindCaption');
  private _taskDigest = plainColumnName('KrDialogStageTypeSettingsVirtual', 'TaskDigest');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [dialogDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);

    const settings = context.stageRow.getStorage();
    const buidler = { text: '' };

    this.appendString(buidler, settings, this._kindCaption, '', true);
    this.appendString(buidler, settings, this._taskDigest, '', true, false, 30);

    context.displaySettings = buidler.text;
  }

}