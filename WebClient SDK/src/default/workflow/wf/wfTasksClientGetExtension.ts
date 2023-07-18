import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
import { Card, CardTask } from 'tessa/cards';
import { hasNotFlag } from 'tessa/platform';
import { CardTypeFlags } from 'tessa/cards/types';
import { taskTypeIsResolution } from 'tessa/workflow';

/**
 * Загрузка карточки с управлением секциями заданий на клиенте.
 */
export class WfTasksClientGetExtension extends CardGetExtension {

  public afterRequest(context: ICardGetExtensionContext) {
    let card: Card;
    if (!context.requestIsSuccessful
      || !context.cardType
      || hasNotFlag(context.cardType.flags, CardTypeFlags.AllowTasks)
      || !context.response
      || !(card = context.response.tryGetCard()!)
    ) {
      return;
    }

    const tasks = card.tryGetTasks();
    if (tasks && tasks.length > 0) {
      for (let task of tasks) {
        if (taskTypeIsResolution(task.typeId)
          && !task.isLocked
        ) {
          WfTasksClientGetExtension.setResolutionFieldChanged(task);
        }
      }
    }
  }

  private static setResolutionFieldChanged(task: CardTask) {
    const taskCard = task.tryGetCard();
    if (!taskCard) {
      return;
    }

    const taskSections = taskCard.tryGetSections();
    if (!taskSections) {
      return;
    }

    const resolutionSection = taskSections.tryGet('WfResolutions');
    if (!resolutionSection) {
      return;
    }

    let fieldsChangingInClosure = false;
    resolutionSection.fields.fieldChanged.add(e => {
      if (fieldsChangingInClosure) {
        return;
      }

      switch (e.fieldName) {
        case 'Planned':
          fieldsChangingInClosure = true;
          e.storage.set('DurationInDays', null);
          fieldsChangingInClosure = false;
          break;
        case 'DurationInDays':
          fieldsChangingInClosure = true;
          e.storage.set('Planned', null);
          fieldsChangingInClosure = false;
          break;
        case 'ShowAdditional':
          if (!e.fieldValue) {
            e.storage.set('KindID', null);
            e.storage.set('KindCaption', null);
            e.storage.set('AuthorID', null);
            e.storage.set('AuthorName', null);
          }
          break;
        case 'WithControl':
          if (!e.fieldValue) {
            e.storage.set('ControllerID', null);
            e.storage.set('ControllerName', null);
          }
          break;
      }
    });
  }

}