import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { hasNotFlag, Visibility } from 'tessa/platform';
import { CardTypeFlags } from 'tessa/cards/types';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import { CardTaskFlags } from 'tessa/cards';

export class KrCommentRequestUIExtension extends CardUIExtension {

  public initialized(context: ICardUIExtensionContext) {
    const cardType = context.model.cardType;
    if (hasNotFlag(cardType.flags, CardTypeFlags.AllowTasks)) {
      return;
    }

    const model = context.model;
    if (!(model.mainForm instanceof DefaultFormTabWithTasksViewModel)) {
      return;
    }

    for (let task of model.mainForm.tasks) {
      const taskModel = task.taskModel;
      if (taskModel.cardType.id === 'f0360d95-4f88-4809-b926-57b34a2f69f5' // KrRequestCommentTypeID
        && hasNotFlag(taskModel.cardTask!.flags, CardTaskFlags.CanPerform)
        && !taskModel.cardTask!.isLockedEffective
      ) {
        const commentControl = taskModel.controls.get('Comment');
        if (commentControl) {
          commentControl.controlVisibility = Visibility.Collapsed;
        }
      }
    }
  }

}