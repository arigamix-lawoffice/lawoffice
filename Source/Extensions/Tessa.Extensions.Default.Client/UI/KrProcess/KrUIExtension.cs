using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Forms;
using Tessa.UI.Cards.Tasks;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    /// <summary>
    /// Скрывает результаты запроса комментария из задания согласования если комментарий не запрашивался.
    /// Скрывает поле "Комментарий" для варианта завершения "Согласовать", если установлена соответствующая настройка.
    /// </summary>
    public sealed class KrUIExtension : CardUIExtension
    {
        #region Constructors

        public KrUIExtension(ICardCache cardCache)
        {
            this.cardCache = cardCache ?? throw new ArgumentNullException(nameof(cardCache));
        }

        #endregion

        #region Fields

        private readonly ICardCache cardCache;

        private bool? commentIsHiddenForApproval;

        #endregion

        #region Private Methods

        private async ValueTask<bool> CommentIsHiddenForApprovalAsync(CancellationToken cancellationToken = default)
        {
            if (this.commentIsHiddenForApproval.HasValue)
            {
                return this.commentIsHiddenForApproval.Value;
            }

            var cardValue = await this.cardCache.Cards.GetAsync(DefaultCardTypes.KrSettingsTypeName, cancellationToken);

            var value = cardValue.IsSuccess
                && cardValue.GetValue().Entries.Get<bool>(KrConstants.KrSettings.Name, KrConstants.KrSettings.HideCommentForApprove);

            this.commentIsHiddenForApproval = value;
            return value;
        }

        private static Task ModifyUniversalTaskAsync(TaskViewModel taskViewModel)
        {
            var taskModel = taskViewModel.TaskModel;
            if (taskModel.CardType.ID != DefaultTaskTypes.KrUniversalTaskTypeID
                || !taskModel.CardTask.IsCanPerform
                || taskModel.CardTask.StoredState != CardTaskState.InProgress
                && taskModel.CardTask.Flags.HasNot(CardTaskFlags.AutoStart)
                && taskModel.CardType.Flags.HasNot(CardTypeFlags.AutoStartTasks)
                || taskModel.CardTask.StoredState == CardTaskState.InProgress
                && taskModel.CardTask.Flags.HasNot(CardTaskFlags.CurrentPerformer))
            {
                return Task.CompletedTask;
            }

            return taskViewModel.ModifyWorkspaceAsync(static (taskViewModel, _) =>
            {
                if (taskViewModel.Workspace.Form?.Name is not null)
                {
                    return ValueTask.CompletedTask;
                }

                var actionsInitialCount = taskViewModel.Workspace.Actions.Count;
                var additionalActionsInitialCount = taskViewModel.Workspace.AdditionalActions.Count;

                foreach (var row in
                    taskViewModel.TaskModel
                        .Card
                        .Sections
                        .GetOrAddTable(KrConstants.KrUniversalTaskOptions.Name)
                        .Rows
                        .OrderBy(x => x.TryGet<int?>(KrConstants.KrUniversalTaskOptions.Order)))
                {
                    var optionID = row.Get<Guid>(KrConstants.KrUniversalTaskOptions.OptionID);
                    var caption = row.Get<string>(KrConstants.KrUniversalTaskOptions.Caption);
                    var showComment = row.Get<bool>(KrConstants.KrUniversalTaskOptions.ShowComment);
                    var message = row.Get<string>(KrConstants.KrUniversalTaskOptions.Message);
                    var additional = row.Get<bool>(KrConstants.KrUniversalTaskOptions.Additional);

                    if (additional)
                    {
                        var index = taskViewModel.Workspace.AdditionalActions.Count - additionalActionsInitialCount;

                        if (index == 0)
                        {
                            taskViewModel.Workspace.AdditionalActions.Insert(0, new TaskSeparatorActionViewModel());
                            additionalActionsInitialCount++;
                        }

                        taskViewModel.Workspace.AdditionalActions.Insert(
                            index,
                            GenerateTaskAction(
                                taskViewModel.Navigator,
                                optionID,
                                caption,
                                message,
                                showComment));
                    }
                    else
                    {
                        taskViewModel.Workspace.Actions.Insert(
                            taskViewModel.Workspace.Actions.Count - actionsInitialCount,
                            GenerateTaskAction(
                                taskViewModel.Navigator,
                                optionID,
                                caption,
                                message,
                                showComment));
                    }
                }

                return ValueTask.CompletedTask;
            });
        }

        private static ITaskAction GenerateTaskAction(
            TaskNavigator navigator,
            Guid optionID,
            string caption,
            string message,
            bool showComment)
        {
            var taskModel = navigator.TaskModel;
            var isSetMessage = !string.IsNullOrEmpty(message);
            if (!showComment && !isSetMessage)
            {
                return new TaskActionViewModel(
                    caption,
                    async () => await TaskNavigationHelper.SaveCardAsync(
                        taskModel,
                        (task, ct) =>
                        {
                            task.Action = CardTaskAction.Complete;
                            task.State = CardRowState.Deleted;
                            task.OptionID = optionID;
                            return ValueTask.CompletedTask;
                        }),
                    TaskActionType.Complete,
                    TaskGroupingType.Default,
                    model: taskModel);
            }

            return new TaskActionViewModel(
                TaskNavigationHelper.GetCaptionWithRightArrow(caption),
                async () =>
                {
                    await navigator.NavigateToFormAsync(
                        TaskWorkspaceState.OptionForm,
                        KrConstants.Ui.ExtendedTaskForm,
                        new ObservableCollection<ITaskAction>
                        {
                            GenerateTaskAction(navigator, optionID, caption, null, false),
                            TaskNavigationHelper.CreateNavigateBackAction(navigator),
                        }).ConfigureAwait(false);

                    var newTaskModel = navigator.TaskModel;
                    if (newTaskModel.Blocks.TryGet(KrConstants.Ui.ExtendedTaskForm, out var blockViewModel))
                    {
                        await DispatcherHelper.InvokeInUIAsync(() =>
                        {
                            IControlViewModel controlViewModel;
                            if ((controlViewModel = blockViewModel.Controls.FirstOrDefault(p => p.Name == KrConstants.Ui.MessageLabel)) is not null
                                && controlViewModel is LabelViewModel label)
                            {
                                if (isSetMessage)
                                {
                                    label.ControlVisibility = Visibility.Visible;
                                    label.Text = message;
                                }
                                else
                                {
                                    label.ControlVisibility = Visibility.Collapsed;
                                }
                            }

                            if ((controlViewModel = blockViewModel.Controls.FirstOrDefault(p => p.Name == KrConstants.Ui.Comment)) is not null)
                            {
                                controlViewModel.ControlVisibility = showComment
                                    ? Visibility.Visible
                                    : Visibility.Collapsed;
                            }
                        });
                    }
                },
                TaskActionType.NavigateToForm,
                TaskGroupingType.Default,
                model: taskModel);
        }

        private void ModifyTaskAndAttachHandlers(TaskViewModel taskViewModel)
        {
            var taskModel = taskViewModel.TaskModel;
            if ((taskModel.CardType.ID != DefaultTaskTypes.KrApproveTypeID
                && taskModel.CardType.ID != DefaultTaskTypes.KrAdditionalApprovalTypeID
                && taskModel.CardType.ID != DefaultTaskTypes.KrSigningTypeID)
                || taskModel.CardTask.IsLockedEffective)
            {
                return;
            }

            // скрываем блок с комментариями в текущем представлении
            if (taskModel.Card.Sections[KrConstants.KrCommentsInfoVirtual.Name].Rows.Count == 0
                && taskModel.Blocks.TryGet(KrConstants.Ui.CommentsBlockShort, out var commentBlock))
            {
                //Если секция есть, но ее поля незаполнены - значит запроса комментария не было
                commentBlock.BlockVisibility = Visibility.Collapsed;
                taskModel.MainFormWithBlocks.RearrangeSelf();
            }

            if (taskModel.CardType.ID == DefaultTaskTypes.KrApproveTypeID
                || taskModel.CardType.ID == DefaultTaskTypes.KrSigningTypeID
                || taskModel.CardType.ID == DefaultTaskTypes.KrAdditionalApprovalTypeID)
            {
                // скрываем блок с заданиями доп согласования в текущем представлении
                if (taskModel.Card.Sections[KrConstants.KrAdditionalApprovalInfo.Virtual].Rows.Count == 0
                    && taskModel.Blocks.TryGet(KrConstants.Ui.AdditionalApprovalBlockShort, out var additionalApprovalBlock))
                {
                    //Если секция есть, но ее поля незаполнены - значит запроса комментария не было
                    additionalApprovalBlock.BlockVisibility = Visibility.Collapsed;
                    taskModel.MainFormWithBlocks.RearrangeSelf();
                }

                // Скрываем блок с запрошенными заданиями доп согласования в текущем задании.
                if (taskModel.Card.Sections.TryGetValue(KrConstants.KrAdditionalApprovalsRequestedInfo.Virtual, out var additionalApprovalsRequestedInfoSection)
                    && additionalApprovalsRequestedInfoSection.Rows.Count == 0
                    && taskModel.Controls.TryGet(KrConstants.Ui.AdditionalApprovalsRequestedInfoTable, out var additionalApprovalsRequestedInfoTable))
                {
                    // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
                    additionalApprovalsRequestedInfoTable.ControlVisibility = Visibility.Collapsed;
                    taskModel.MainFormWithBlocks.RearrangeSelf();
                }
            }

            // в начальной форме задания гарантированно нет поля "Комментарий",
            // которое может понадобиться скрыть для варианта "Согласовать"

            // скрываем блок с комментариями в других представлениях
            taskViewModel.WorkspaceChanged += async (sender, e) =>
            {
                // получить блок по taskModel.Blocks.TryGet нельзя, т.к. для формы откладывания заданий будет свой экземпляр блока,
                // при этом TryGet вернёт блок для предыдущей формы карточки

                var senderT = (TaskViewModel) sender;
                IFormWithBlocksViewModel form = senderT?.Workspace.Form;
                if (form == null)
                {
                    return;
                }

                var taskCardModel = senderT.TaskModel;
                var blocks = form.Blocks;

                var rearrangeForm = false;

                IBlockViewModel innerCommentBlock;
                if (taskCardModel.Card.Sections[KrConstants.KrCommentsInfoVirtual.Name].Rows.Count == 0
                    && (innerCommentBlock = blocks.FirstOrDefault(x => x.Name == KrConstants.Ui.CommentsBlockShort)) is not null)
                {
                    //Если секция есть, но ее поля незаполнены - значит запроса комментария не было
                    innerCommentBlock.BlockVisibility = Visibility.Collapsed;
                    rearrangeForm = true;
                }

                if (taskCardModel.CardType.ID == DefaultTaskTypes.KrApproveTypeID
                    || taskCardModel.CardType.ID == DefaultTaskTypes.KrSigningTypeID
                    || taskCardModel.CardType.ID == DefaultTaskTypes.KrAdditionalApprovalTypeID)
                {
                    IBlockViewModel innerAdditionalApprovalBlock;
                    if (taskCardModel.Card.Sections[KrConstants.KrAdditionalApprovalInfo.Virtual].Rows.Count == 0
                        && (innerAdditionalApprovalBlock = blocks.FirstOrDefault(x => x.Name == KrConstants.Ui.AdditionalApprovalBlockShort)) is not null)
                    {
                        //Если секция есть, но ее поля незаполнены - значит запроса комментария не было
                        innerAdditionalApprovalBlock.BlockVisibility = Visibility.Collapsed;
                        rearrangeForm = true;
                    }

                    // Скрываем блок с запрошенными заданиями доп согласования в текущем задании.
                    if (taskCardModel.Card.Sections.TryGetValue(KrConstants.KrAdditionalApprovalsRequestedInfo.Virtual, out var additionalApprovalsRequestedInfoSection)
                        && additionalApprovalsRequestedInfoSection.Rows.Count == 0
                        && taskCardModel.Controls.TryGet(KrConstants.Ui.AdditionalApprovalsRequestedInfoTable, out var additionalApprovalsRequestedInfoTable))
                    {
                        // Если секция есть, но ее поля незаполнены - значит запроса комментария не было
                        additionalApprovalsRequestedInfoTable.ControlVisibility = Visibility.Collapsed;
                        rearrangeForm = true;
                    }

                    var deferral = e.Defer();
                    try
                    {
                        // скрываем поле "Комментарий" для варианта завершения "Согласовать"
                        IBlockViewModel approvalCommentBlock;
                        if (form.Name == "Approve"
                            && await this.CommentIsHiddenForApprovalAsync()
                            && (approvalCommentBlock = blocks.FirstOrDefault(x => x.Name == KrConstants.Ui.CommentBlock)) is not null)
                        {
                            approvalCommentBlock.BlockVisibility = Visibility.Collapsed;
                            rearrangeForm = true;
                        }

                        if (rearrangeForm)
                        {
                            form.RearrangeSelf();
                        }
                    }
                    catch (Exception ex)
                    {
                        deferral.SetException(ex);
                    }
                    finally
                    {
                        deferral.Dispose();
                    }
                }
            };

            // подписываемся на построение метаинформации и виртуальной карточки для формы откладывания задания
            taskViewModel.PostponeMetadataInitializing += PostponeMetadataInitializingAsync;
            taskViewModel.PostponeContentInitializing += PostponeContentInitializing;
        }

        private static async void PostponeMetadataInitializingAsync(object sender, TaskViewModelEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                var targetMetadata = e.Task.PostponeMetadata;
                var targetType = (await targetMetadata.GetCardTypesAsync())[0];
                CardTypeForm targetForm = targetType.Forms[0];
                var targetBlocks = targetForm.Blocks;

                // удаляем блок с информацией по заданию, т.к. он будет скопирован ниже из основной формы
                if (targetBlocks.Count > 0)
                {
                    targetBlocks.RemoveAt(0);
                }

                // копируем все блоки из основной формы задания в начало формы откладывания, потом сортируем по Order
                CardType sourceType = await e.Task.TaskModel.CardType.DeepCloneAsync();
                CardTypeNamedForm sourceForm = sourceType.Forms[0];
                await sourceForm.Blocks.CopyToTheBeginningOfAsync(targetBlocks);

                // копируем настройки формы из основной формы задания в форму откладывания
                targetForm.FormSettings.Clear();
                StorageHelper.Merge(sourceForm.FormSettings, targetForm.FormSettings);

                // копируем метаинформацию по виртуальной таблице для формы откладывания
                var taskSections = await e.Task.TaskModel.CardMetadata.GetSectionsAsync();
                CardMetadataSectionCollection targetSections = null;
                if (taskSections.TryGetValue(KrConstants.KrCommentsInfoVirtual.Name, out var metadataSection))
                {
                    (targetSections = await targetMetadata.GetSectionsAsync()).Add(await metadataSection.DeepCloneAsync());

                    var sourceItem = sourceType.SchemeItems
                        .FirstOrDefault(x => x.SectionID == metadataSection.ID);
                    if (sourceItem is not null)
                    {
                        targetType.SchemeItems.Add(sourceItem);
                    }
                }

                if (taskSections.TryGetValue(KrConstants.KrAdditionalApprovalInfo.Virtual, out metadataSection))
                {
                    (targetSections ?? await targetMetadata.GetSectionsAsync()).Add(await metadataSection.DeepCloneAsync());

                    var sourceItem = sourceType.SchemeItems
                        .FirstOrDefault(x => x.SectionID == metadataSection.ID);
                    if (sourceItem is not null)
                    {
                        targetType.SchemeItems.Add(sourceItem);
                    }
                }

                if (taskSections.TryGetValue(KrConstants.KrAdditionalApprovalsRequestedInfo.Virtual, out metadataSection))
                {
                    (targetSections ?? await targetMetadata.GetSectionsAsync()).Add(await metadataSection.DeepCloneAsync());

                    var sourceItem = sourceType.SchemeItems
                        .FirstOrDefault(x => x.SectionID == metadataSection.ID);
                    if (sourceItem is not null)
                    {
                        targetType.SchemeItems.Add(sourceItem);
                    }
                }
            }
            catch (Exception ex)
            {
                deferral.SetException(ex);
            }
            finally
            {
                deferral.Dispose();
            }
        }

        private static void PostponeContentInitializing(object sender, TaskFormContentViewModelEventArgs e)
        {
            if (e.Task.TaskModel.Card.Sections.TryGetValue(KrConstants.KrCommentsInfoVirtual.Name, out var sourceSection)
                && e.Card.Sections.TryGetValue(KrConstants.KrCommentsInfoVirtual.Name, out var targetSection))
            {
                targetSection.Set(sourceSection);
            }

            if (e.Task.TaskModel.Card.Sections.TryGetValue(KrConstants.KrAdditionalApprovalInfo.Virtual, out sourceSection)
                && e.Card.Sections.TryGetValue(KrConstants.KrAdditionalApprovalInfo.Virtual, out targetSection))
            {
                targetSection.Set(sourceSection);
            }

            if (e.Task.TaskModel.Card.Sections.TryGetValue(KrConstants.KrAdditionalApprovalsRequestedInfo.Virtual, out sourceSection)
                && e.Card.Sections.TryGetValue(KrConstants.KrAdditionalApprovalsRequestedInfo.Virtual, out targetSection))
            {
                targetSection.Set(sourceSection);
            }
        }

        /// <summary>
        /// Модифицирует задание <see cref="DefaultTaskTypes.KrSigningTypeID"/>.
        /// </summary>
        /// <param name="taskViewModel">Модель представления для вывода области с заданием.</param>
        /// <returns>Асинхронная задача.</returns>
        private static Task ModifySigningTaskAsync(TaskViewModel taskViewModel)
        {
            var taskModel = taskViewModel.TaskModel;

            if (taskModel.CardType.ID != DefaultTaskTypes.KrSigningTypeID)
            {
                return Task.CompletedTask;
            }

            return taskViewModel.ModifyWorkspaceAsync(static (taskViewModel, isFirstChange) =>
            {
                if (taskViewModel.TaskModel.Card.Sections.TryGetValue(KrConstants.KrSigningTaskOptions.Name, out var additionalApprovalInfoSection)
                    && !additionalApprovalInfoSection.Fields.TryGet<bool>(KrConstants.KrSigningTaskOptions.AllowAdditionalApproval)
                    && taskViewModel.Workspace.AdditionalActions.TryFirst(i => i.CompletionOption?.ID == DefaultCompletionOptions.AdditionalApproval, out var additionalApprovalAction))
                {
                    taskViewModel.Workspace.AdditionalActions.Remove(additionalApprovalAction);
                }

                return ValueTask.CompletedTask;
            });
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var model = context.Model;
            if (model.MainForm is not DefaultFormTabWithTasksViewModel formWithTasks
                || model.CardType.Flags.HasNot(CardTypeFlags.AllowTasks))
            {
                return;
            }

            foreach (var taskViewModel in formWithTasks.Tasks.OfType<TaskViewModel>())
            {
                this.ModifyTaskAndAttachHandlers(taskViewModel);
                await ModifyUniversalTaskAsync(taskViewModel);
                await ModifySigningTaskAsync(taskViewModel);
            }

            formWithTasks.HiddenTaskCreated += async (s, e) =>
            {
                this.ModifyTaskAndAttachHandlers(e.Task);

                var deferral = e.Defer();
                try
                {
                    await ModifyUniversalTaskAsync(e.Task);
                    await ModifySigningTaskAsync(e.Task);
                }
                catch (Exception ex)
                {
                    deferral.SetException(ex);
                }
                finally
                {
                    deferral.Dispose();
                }
            };
        }

        #endregion
    }
}
