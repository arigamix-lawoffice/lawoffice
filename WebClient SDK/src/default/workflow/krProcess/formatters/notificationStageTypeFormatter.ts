import { plainColumnName, sectionName } from 'tessa/workflow';
import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor,
  notificationDescriptor
} from 'tessa/workflow/krProcess';
import {
  CardRowState
} from 'tessa/cards';

export class NotificationStageTypeFormatter extends KrStageTypeFormatter {

  private _excludeDeputies = plainColumnName('KrNotificationSettingVirtual', 'ExcludeDeputies');
  private _excludeSubscribers = plainColumnName('KrNotificationSettingVirtual', 'ExcludeSubscribers');
  private _optionalRecipients = sectionName('KrNotificationOptionalRecipientsVirtual');

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [notificationDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);

    const excludeDeputies = context.stageRow.get(this._excludeDeputies);
    const excludeSubscribers = context.stageRow.get(this._excludeSubscribers);
    const optionalRecipients = context.card.sections.tryGet(this._optionalRecipients)!
      .rows
      .filter(x => x.state !== CardRowState.Deleted && x.get("StageRowID") == context.stageRow.rowId)
      .map(x => x.get("RoleName"));

    context.displayTimeLimit = '';
    context.displaySettings = this.getDisplaySettings(excludeDeputies, excludeSubscribers, optionalRecipients);
  }

  private getDisplaySettings(
    excludeDeputies: boolean,
    excludeSubscribers: boolean,
    optionalRecipients: Array<string>
  ): string {
    let settings = '';
    if (excludeDeputies) {
      settings = '{$UI_KrNotification_ExcludeDeputies}';
    }
    if (excludeSubscribers) {
      if (excludeDeputies) {
        settings += '\n';
      }
      settings += '{$UI_KrNotification_ExcludeSubscribers}';
    }
    if (optionalRecipients.length > 0) {
      if (settings.length > 0) {
        settings += '\n';
      }
      settings += '{$CardTypes_Controls_OptionalRecipients}: ';
      optionalRecipients.forEach((x, i) => {
        if (i > 0) {
          settings += ', ';
        }
        settings += x;
      });
    }

    return settings;
  }

}
