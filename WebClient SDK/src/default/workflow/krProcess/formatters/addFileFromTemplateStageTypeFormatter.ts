import { plainColumnName } from 'tessa/workflow';
import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor,
  addFromTemplateDescriptor } from 'tessa/workflow/krProcess';

export class AddFileFromTemplateStageTypeFormatter extends KrStageTypeFormatter {

  private _name = plainColumnName('KrAddFromTemplateSettingsVirtual', 'Name');
  private _fileTemplateName = plainColumnName('KrAddFromTemplateSettingsVirtual', 'FileTemplateName');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [addFromTemplateDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);
    context.displayParticipants = '';
    context.displayTimeLimit = '';
    context.displaySettings = `${context.stageRow.tryGet(this._fileTemplateName) || ''}\n${context.stageRow.tryGet(this._name) || ''}`;
  }

}