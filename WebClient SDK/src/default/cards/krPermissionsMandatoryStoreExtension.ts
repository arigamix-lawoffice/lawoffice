import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
import { UIContext, tryGetFromInfo } from 'tessa/ui';
import { systemKeyPrefix, CardTaskAction } from 'tessa/cards';
import { getTypedFieldValue, TypedField, DotNetType } from 'tessa/platform';
import { ICardModel } from 'tessa/ui/cards';
import { LocalizationManager } from 'tessa/localization';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import { TaskViewModel } from 'tessa/ui/cards/tasks';

interface KrPermissionMandatoryRuleStorage {
  sectionId: guid;
  columnIds: guid[];
}

export class KrPermissionsMandatoryStoreExtension extends CardStoreExtension {

  public afterRequest(context: ICardStoreExtensionContext) {
    // tslint:disable-next-line:no-any
    let rules: any[] | null = null;
    if (!context.requestIsSuccessful
      && UIContext.current.cardEditor
      && context.response
      && (rules = tryGetFromInfo(context.response.info, systemKeyPrefix + 'FailedMandatoryRules'))
    ) {
      const cardModel = UIContext.current.cardEditor.cardModel!;
      const failRules: KrPermissionMandatoryRuleStorage[] = rules.map(x => {
        return {
          sectionId: getTypedFieldValue(x['SectionID']),
          columnIds: (x['ColumnIDs'] as Array<TypedField<DotNetType.Guid, guid>>).map(y => getTypedFieldValue(y))
        };
      });

      this.validateFromRules(cardModel, failRules);

      const requestTasks = context.request.card.tasks;
      const taskItems = cardModel.mainForm instanceof DefaultFormTabWithTasksViewModel ? cardModel.mainForm.tasks : null;
      if (requestTasks.length > 0
        && !!taskItems
        && taskItems.length > 0
      ) {
        for (let task of requestTasks) {
          let taskItem: TaskViewModel | null = null;
          if (task.action === CardTaskAction.Complete
            && (taskItem = taskItems.find(x => x instanceof TaskViewModel && x.taskModel.cardTask!.rowId === task.rowId)!)
          ) {
            this.validateFromRules(taskItem.taskModel, failRules);
          }
        }
      }
    }
  }

  private validateFromRules(cardModel: ICardModel, failRules: KrPermissionMandatoryRuleStorage[]) {
    for (let controlViewModel of cardModel.controlsBag) {
      const sourceInfo = controlViewModel.cardTypeControl.getSourceInfo();
      if (sourceInfo
        && failRules.some(x => x.sectionId === sourceInfo.sectionId
            && (x.columnIds.length === 0 || sourceInfo.columnIds.some(y => x.columnIds.some(z => z === y))))
      ) {
        controlViewModel.validationFunc = (c) => {
          if (c.hasEmptyValue) {
            return LocalizationManager.instance.format('$KrPermissions_MandatoryControlTemplate', c.caption);
          }

          return null;
        };
        controlViewModel.notifyUpdateValidation();
      }
    }
  }

}