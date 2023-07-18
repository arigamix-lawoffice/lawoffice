import {
  CardUIExtension,
  IBlockViewModel,
  ICardUIExtensionContext,
  IControlViewModel
} from 'tessa/ui/cards';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import { deepClone, hasFlag, hasNotFlag, unseal, Visibility } from 'tessa/platform';
import { CardType, CardTypeFlags } from 'tessa/cards/types';
import {
  createNavigateBackAction,
  getCaptionWithRightArrow,
  saveCardWithTaskModifier,
  TaskAction,
  TaskActionType,
  TaskActionViewModel,
  TaskFormContentViewModelEventArgs,
  TaskGroupingType,
  TaskNavigator,
  TaskViewModel,
  TaskViewModelEventArgs,
  TaskWorkspaceState
} from 'tessa/ui/cards/tasks';
import {
  CardRowState,
  CardSectionType,
  CardSingletonCache,
  CardTableType,
  CardTaskAction,
  CardTaskFlags,
  CardTaskState
} from 'tessa/cards';
import { CardMetadata, CardMetadataSection } from 'tessa/cards/metadata';
import { clone } from 'tessa/platform/storage';
import { LabelViewModel } from 'tessa/ui/cards/controls';

/**
 * Скрывает результаты запроса комментария из задания согласования если комментарий не запрашивался.
 * Скрывает поле "Комментарий" для варианта завершения "Согласовать", если установлена соответствующая настройка.
 */
export class KrUIExtension extends CardUIExtension {
  private _commentIsHiddenForApproval: boolean | null = null;

  private static postponeMetadataInitializing(e: TaskViewModelEventArgs) {
    const targetMetadata = unseal<CardMetadata>(e.task.postponeMetadata);
    const targetType = targetMetadata.cardTypes[0];
    const targetForm = targetType.forms[0];
    const targetBlocks = targetForm.blocks;

    // удаляем блок с информацией по заданию, т.к. он будет скопирован ниже из основной формы
    if (targetBlocks.length > 0) {
      targetBlocks.splice(0, 1);
    }

    // копируем все блоки из основной формы задания в начало формы откладывания
    const sourceType = unseal<CardType>(deepClone(e.task.taskModel.cardType));
    const sourceForm = sourceType.forms[0];
    targetBlocks.splice(0, 0, ...sourceForm.blocks);

    // копируем настройки формы из основной формы задания в форму откладывания
    targetForm.formSettings = clone(sourceForm.formSettings);

    // копируем метаинформацию по виртуальной таблице для формы откладывания
    const krCommentsInfoVirtual =
      e.task.taskModel.cardMetadata.getSectionByName('KrCommentsInfoVirtual');
    if (krCommentsInfoVirtual) {
      targetMetadata.sections.push(unseal<CardMetadataSection>(deepClone(krCommentsInfoVirtual)));

      const sourceItem = sourceType.schemeItems.find(x => x.sectionId === krCommentsInfoVirtual.id);
      if (sourceItem) {
        targetType.schemeItems.push(sourceItem);
      }
    }

    const krAdditionalApprovalInfoVirtual = e.task.taskModel.cardMetadata.getSectionByName(
      'KrAdditionalApprovalInfoVirtual'
    );
    if (krAdditionalApprovalInfoVirtual) {
      targetMetadata.sections.push(
        unseal<CardMetadataSection>(deepClone(krAdditionalApprovalInfoVirtual))
      );

      const sourceItem = sourceType.schemeItems.find(
        x => x.sectionId === krAdditionalApprovalInfoVirtual.id
      );
      if (sourceItem) {
        targetType.schemeItems.push(sourceItem);
      }
    }

    const metadataSection = e.task.taskModel.cardMetadata.getSectionByName(
      'KrAdditionalApprovalsRequestedInfoVirtual'
    );
    if (metadataSection) {
      targetMetadata.sections.push(unseal(<CardMetadataSection>deepClone(metadataSection)));
      const sourceItem = sourceType.schemeItems.find(x => x.sectionId === metadataSection.id);
      if (sourceItem) {
        targetType.schemeItems.push(sourceItem);
      }
    }
  }

  private static postponeContentInitializing(e: TaskFormContentViewModelEventArgs) {
    let sourceSection = e.task.taskModel.card.sections.tryGet('KrCommentsInfoVirtual');
    if (!sourceSection) {
      return;
    }

    let targetSection = e.card.sections.tryGet('KrCommentsInfoVirtual');
    if (!targetSection) {
      return;
    }

    targetSection.setFrom(sourceSection);

    sourceSection = e.task.taskModel.card.sections.tryGet('KrAdditionalApprovalInfoVirtual');
    if (!sourceSection) {
      return;
    }

    targetSection = e.card.sections.tryGet('KrAdditionalApprovalInfoVirtual');
    if (!targetSection) {
      return;
    }

    targetSection.setFrom(sourceSection);

    sourceSection = e.task.taskModel.card.sections.tryGet('SequenceProvider');
    if (!sourceSection) {
      return;
    }
    targetSection = e.card.sections.tryGet('KrAdditionalApprovalsRequestedInfoVirtual');
    if (!targetSection) {
      return;
    }
    targetSection.setFrom(sourceSection);
  }

  public initialized(context: ICardUIExtensionContext): void {
    const model = context.model;
    if (
      !(model.mainForm instanceof DefaultFormTabWithTasksViewModel) ||
      hasNotFlag(model.cardType.flags, CardTypeFlags.AllowTasks)
    ) {
      return;
    }

    const formWithTasks = model.mainForm as DefaultFormTabWithTasksViewModel;

    for (const taskViewModel of formWithTasks.tasks) {
      this.modifyTaskAndAttachHandlers(taskViewModel);
      this.modifyUniversalTask(taskViewModel);
      this.modifySigningTask(taskViewModel);
    }
  }

  private commentIsHiddenForApproval = (): boolean => {
    if (this._commentIsHiddenForApproval != null) {
      return this._commentIsHiddenForApproval;
    }

    const krSettings = CardSingletonCache.instance.cards.get('KrSettings');
    if (!krSettings) {
      this._commentIsHiddenForApproval = false;
      return false;
    }

    const value = krSettings.sections.get('KrSettings')!.fields.get('HideCommentForApprove');
    this._commentIsHiddenForApproval = value;
    return value;
  };

  private modifyTaskAndAttachHandlers(taskViewModel: TaskViewModel) {
    const taskModel = taskViewModel.taskModel;
    if (
      (taskModel.cardType.id !== 'e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c' && // KrApproveTypeID
        taskModel.cardType.id !== 'b3d8eae3-c6bf-4b59-bcc7-461d526c326c' && // KrAdditionalApprovalTypeID
        taskModel.cardType.id !== '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6') || // KrSigningTypeID
      taskModel.cardTask!.isLockedEffective
    ) {
      return;
    }

    let commentBlock: IBlockViewModel;
    // скрываем блок с комментариями в текущем представлении
    if (
      taskModel.card.sections.get('KrCommentsInfoVirtual')!.rows.length === 0 &&
      !!(commentBlock = taskModel.blocks.get('CommentsBlockShort')!)
    ) {
      // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
      commentBlock.blockVisibility = Visibility.Collapsed;
    }

    if (
      taskModel.cardType.id === 'e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c' || // KrApproveTypeID
      taskModel.cardType.id === 'b3d8eae3-c6bf-4b59-bcc7-461d526c326c' || // KrAdditionalApprovalTypeID
      taskModel.cardType.id === '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6' // KrSigningTypeID
    ) {
      let additionalApprovalBlock: IBlockViewModel;
      // скрываем блок с заданиями доп согласования в текущем представлении
      if (
        taskModel.card.sections.get('KrAdditionalApprovalInfoVirtual')!.rows.length === 0 &&
        !!(additionalApprovalBlock = taskModel.blocks.get('AdditionalApprovalBlockShort')!)
      ) {
        // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
        additionalApprovalBlock.blockVisibility = Visibility.Collapsed;
      }

      // Скрываем блок с запрошенными заданиями доп согласования в текущем задании
      let additionalApprovalsRequestedInfoTable: IControlViewModel;
      if (
        taskModel.card.sections.tryGet('KrAdditionalApprovalsRequestedInfoVirtual') &&
        taskModel.card.sections.tryGet('KrAdditionalApprovalsRequestedInfoVirtual')!.rows.length ===
          0 &&
        !!(additionalApprovalsRequestedInfoTable = taskModel.controls.get(
          'AdditionalApprovalsRequestedInfoTable'
        )!)
      ) {
        // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
        additionalApprovalsRequestedInfoTable.controlVisibility = Visibility.Collapsed;
      }
    }

    // в начальной форме задания гарантированно нет поля "Комментарий",
    // которое может понадобиться скрыть для варианта "Согласовать"

    // скрываем блок с комментариями в других представлениях
    taskViewModel.workspaceChanged.add(e => {
      // получить блок по taskModel.Blocks.TryGet нельзя, т.к. для формы откладывания заданий будет свой экземпляр блока,
      // при этом TryGet вернёт блок для предыдущей формы карточки

      const form = e.task.taskWorkspace.form;
      if (!form) {
        return;
      }

      const blocks = form.blocks;
      let innerCommentBlock: IBlockViewModel;
      if (
        taskModel.card.sections.get('KrCommentsInfoVirtual')!.rows.length === 0 &&
        !!(innerCommentBlock = blocks.find(x => x.name === 'CommentsBlockShort')!)
      ) {
        // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
        innerCommentBlock.blockVisibility = Visibility.Collapsed;
      }

      if (
        taskModel.cardType.id === 'e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c' || // KrApproveTypeID
        taskModel.cardType.id === 'b3d8eae3-c6bf-4b59-bcc7-461d526c326c' || // KrAdditionalApprovalTypeID
        taskModel.cardType.id === '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6' // KrSigningTypeID
      ) {
        let innerAdditionalApprovalBlock: IBlockViewModel;
        if (
          taskModel.card.sections.get('KrAdditionalApprovalInfoVirtual')!.rows.length === 0 &&
          !!(innerAdditionalApprovalBlock = blocks.find(
            x => x.name === 'AdditionalApprovalBlockShort'
          )!)
        ) {
          // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
          innerAdditionalApprovalBlock.blockVisibility = Visibility.Collapsed;
        }

        // Скрываем блок с запрошенными заданиями доп согласования в текущем задании.
        let additionalApprovalsRequestedInfoTable: IControlViewModel;
        if (
          taskModel.card.sections.tryGet('KrAdditionalApprovalsRequestedInfoVirtual') &&
          taskModel.card.sections.tryGet('KrAdditionalApprovalsRequestedInfoVirtual')!.rows
            .length === 0 &&
          !!(additionalApprovalsRequestedInfoTable = taskModel.controls.get(
            'AdditionalApprovalsRequestedInfoTable'
          )!)
        ) {
          // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
          additionalApprovalsRequestedInfoTable.controlVisibility = Visibility.Collapsed;
        }

        // скрываем поле "Комментарий" для варианта завершения "Согласовать"
        let approvalCommentBlock: IBlockViewModel;
        if (
          form.name === 'Approve' &&
          this.commentIsHiddenForApproval() &&
          !!(approvalCommentBlock = blocks.find(x => x.name === 'CommentBlock')!)
        ) {
          approvalCommentBlock.blockVisibility = Visibility.Collapsed;
        }
      }
    });

    // подписываемся на построение метаинформации и виртуальной карточки для формы откладывания задания
    taskViewModel.postponeMetadataInitializing.add(KrUIExtension.postponeMetadataInitializing);
    taskViewModel.postponeContentInitializing.add(KrUIExtension.postponeContentInitializing);
  }

  private modifySigningTask(taskViewModel: TaskViewModel) {
    const taskModel = taskViewModel.taskModel;
    if (
      taskModel.cardType.id === '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6' // KrSigningTypeID
    ) {
      taskViewModel.modifyWorkspace(e => {
        if (
          e.task.taskModel.card.sections.tryGet('KrSigningTaskOptions') &&
          !e.task.taskModel.card.sections
            .tryGet('KrSigningTaskOptions')!
            .fields.tryGet('AllowAdditionalApproval')
        ) {
          const actionIndex = e.task.taskWorkspace.additionalActions.findIndex(
            x => x.completionOption!.id === 'c726d8ba-73b9-4867-87fe-387d4c61a75a'
          ); // AdditionalApproval
          if (actionIndex > -1) {
            e.task.taskWorkspace.additionalActions.splice(actionIndex, 1);
          }
        }
      });
    }
  }

  private modifyUniversalTask(taskViewModel: TaskViewModel) {
    const taskModel = taskViewModel.taskModel;
    if (
      taskModel.cardType.id === '9c6d9824-41d7-41e6-99f1-e19ea9e576c5' && // KrUniversalTaskTypeID
      taskModel.cardTask &&
      taskModel.cardTask.isCanPerform &&
      (taskModel.cardTask.storedState === CardTaskState.InProgress ||
        hasFlag(taskModel.cardTask!.flags, CardTaskFlags.AutoStart) ||
        hasFlag(taskModel.cardType.flags, CardTypeFlags.AutoStartTasks)) &&
      (taskModel.cardTask.storedState !== CardTaskState.InProgress ||
        hasFlag(taskModel.cardTask.flags, CardTaskFlags.CurrentPerformer))
    ) {
      taskViewModel.modifyWorkspace(e => this.modifyUniversalTaskAction(e.task));
    }
  }

  private modifyUniversalTaskAction = (task: TaskViewModel) => {
    if (task.taskWorkspace.form) {
      return;
    }

    const actionsInitialCount = task.taskWorkspace.actions.length;
    const additionalActionsInitialCount = task.taskWorkspace.additionalActions.length;
    const taskModel = task.taskModel;
    const section = taskModel.card.sections.getOrAdd('KrUniversalTaskOptions');
    section.type = CardSectionType.Table;
    section.tableType = CardTableType.Collection;
    const rowsStorage = section.rows;
    const rows = rowsStorage.map(x => x);
    rows.sort((a, b) => a.get('Order') - b.get('Order'));
    for (const row of rows) {
      const optionId = row.get('OptionID');
      const caption = row.get('Caption');
      const showComment = row.get('ShowComment');
      const message = row.get('Message');
      const additional = row.get('Additional');

      if (additional) {
        const index = task.taskWorkspace.additionalActions.length - additionalActionsInitialCount;

        task.taskWorkspace.additionalActions.splice(
          index,
          0,
          this.generateTaskAction(
            task.taskNavigator,
            optionId,
            caption,
            message,
            showComment ? 'WithComment' : ''
          )
        );
      } else {
        task.taskWorkspace.actions.splice(
          task.taskWorkspace.actions.length - actionsInitialCount,
          0,
          this.generateTaskAction(
            task.taskNavigator,
            optionId,
            caption,
            message,
            showComment ? 'WithComment' : ''
          )
        );
      }
    }
  };

  private generateTaskAction(
    navigator: TaskNavigator,
    optionId: guid,
    caption: string,
    message: string,
    showComment: string
  ): TaskAction {
    return !message && !showComment
      ? new TaskActionViewModel(
          caption,
          () =>
            saveCardWithTaskModifier(navigator.taskModel, task => {
              task.action = CardTaskAction.Complete;
              task.state = CardRowState.Deleted;
              task.optionId = optionId;
            }),
          TaskActionType.Complete,
          TaskGroupingType.Default,
          null,
          navigator.taskModel
        )
      : new TaskActionViewModel(
          getCaptionWithRightArrow(caption),
          () => {
            navigator.navigateToForm(TaskWorkspaceState.OptionForm, ExtendedTaskForm, [
              this.generateTaskAction(navigator, optionId, caption, '', ''),
              createNavigateBackAction(navigator)
            ]);

            const newTaskModel = navigator.taskModel;
            let blockViewModel: IBlockViewModel | undefined;
            if ((blockViewModel = newTaskModel.blocks.get(ExtendedTaskForm))) {
              let controlViewModel: IControlViewModel | undefined;
              if (
                (controlViewModel = blockViewModel.controls.find(p => p.name === 'MessageLabel'))
              ) {
                const label = controlViewModel as LabelViewModel;
                if (message) {
                  label.controlVisibility = Visibility.Visible;
                  label.text = message;
                } else {
                  label.controlVisibility = Visibility.Collapsed;
                }
              }

              if ((controlViewModel = blockViewModel.controls.find(p => p.name === 'Comment'))) {
                controlViewModel.controlVisibility = showComment
                  ? Visibility.Visible
                  : Visibility.Collapsed;
              }
            }
          },
          TaskActionType.NavigateToForm,
          TaskGroupingType.Default,
          null,
          navigator.taskModel
        );
  }
}

const ExtendedTaskForm = 'Extended';
