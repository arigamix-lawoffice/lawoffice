import {
  KrStageTypeFormatter,
  IKrStageTypeFormatterContext,
  StageTypeHandlerDescriptor,
  typedTaskDescriptor
} from 'tessa/workflow/krProcess';
import { plainColumnName } from 'tessa/workflow';

export class TypedTaskStageTypeFormatter extends KrStageTypeFormatter {
  private _taskTypeCaption = plainColumnName('KrTypedTaskSettingsVirtual', 'TaskTypeCaption');
  private _taskDigest = plainColumnName('KrTypedTaskSettingsVirtual', 'TaskDigest');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [typedTaskDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);

    const settings = context.stageRow.getStorage();
    const buidler = { text: '' };

    this.appendString(buidler, settings, this._taskTypeCaption, '', true);
    this.appendString(buidler, settings, this._taskDigest, '', true, false, 30);

    context.displaySettings = buidler.text;
  }
}
