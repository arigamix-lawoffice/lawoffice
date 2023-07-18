import { plainColumnName } from 'tessa/workflow';
import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor,
  createCardDescriptor } from 'tessa/workflow/krProcess';
import { LocalizationManager } from 'tessa/localization';

export class KrCreateCardStageTypeFormatter extends KrStageTypeFormatter {

  private _templateCaption = plainColumnName('KrCreateCardStageSettingsVirtual', 'TemplateCaption');
  private _modeName = plainColumnName('KrCreateCardStageSettingsVirtual', 'ModeName');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [createCardDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    const templateName = `${context.stageRow.tryGet(this._templateCaption) || ''}\n`;
    const modeName = `${LocalizationManager.instance.localize(context.stageRow.tryGet(this._modeName) || '')}`;

    context.displaySettings = templateName + modeName;
  }

}