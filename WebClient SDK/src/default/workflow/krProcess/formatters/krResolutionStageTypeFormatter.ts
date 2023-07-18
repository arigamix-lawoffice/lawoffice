import {
  KrStageTypeFormatter,
  IKrStageTypeFormatterContext,
  StageTypeHandlerDescriptor,
  resolutionDescriptor
} from 'tessa/workflow/krProcess';
import { plainColumnName } from 'tessa/workflow';
import { tryGetFromInfo } from 'tessa/ui';

export class KrResolutionStageTypeFormatter extends KrStageTypeFormatter {
  private _kindCaption = plainColumnName('KrResolutionSettingsVirtual', 'KindCaption');
  private _authorName = plainColumnName('KrResolutionSettingsVirtual', 'AuthorName');
  private _controllerName = plainColumnName('KrResolutionSettingsVirtual', 'ControllerName');
  private _planned = plainColumnName('KrResolutionSettingsVirtual', 'Planned');
  private _durationInDays = plainColumnName('KrResolutionSettingsVirtual', 'DurationInDays');
  private _withControl = plainColumnName('KrResolutionSettingsVirtual', 'WithControl');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [resolutionDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);

    const settings = context.stageRow;
    const settingsStorage = settings.getStorage();
    const buidler = { text: '' };

    this.appendString(
      buidler,
      settingsStorage,
      this._kindCaption,
      '{$CardTypes_Controls_Kind}',
      true
    );
    this.appendString(buidler, settingsStorage, this._authorName, '{$CardTypes_Controls_From}');

    if (tryGetFromInfo(settingsStorage, this._withControl, false)) {
      this.appendString(
        buidler,
        settingsStorage,
        this._controllerName,
        '{$CardTypes_Controls_Controller}',
        false,
        true
      );
    }

    context.displaySettings = buidler.text;

    const planned = settings.tryGet(this._planned);
    const timeLimit = settings.tryGet(this._durationInDays);
    this.defaultDateFormatting(planned, timeLimit, context);
  }
}
