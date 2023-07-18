using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Tessa.Scheme;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Bindings;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Signals;
using Tessa.Workflow.Storage;
using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Обработчик действия <see cref="KrDescriptors.KrSigningDescriptor"/>.
    /// </summary>
    public sealed class KrSigningAction : KrWorkflowMultiTaskActionBase
    {
        #region Constants And Static Fields

        /// <summary>
        /// Имя ключа, по которому в <see cref="CardInfoStorageObject.Info"/> хранится признак показывающий,
        /// что не требуется дополнительно обрабатывать значение <see cref="CardTask.Result"/>
        /// в <see cref="GetResultAsync(IWorkflowEngineContext, CardTask)"/>. Тип значения: <see cref="bool"/>.
        /// </summary>
        private const string IsProcessedTaskResultKey = CardHelper.SystemKeyPrefix + "IsProcessedTaskResult";

        private const string MainSectionName = KrSigningActionVirtual.SectionName;

        /// <summary>
        /// Идентификатор типа задания подписания.
        /// </summary>
        private static readonly Guid taskTypeID = DefaultTaskTypes.KrSigningTypeID;

        /// <summary>
        /// Значение по умолчанию для параметра "Длительность, рабочие дни". Данное значение используется только,
        /// если в схеме не указано значение по умолчанию для поля <see cref="KrSigningActionVirtual.Period"/>.
        /// </summary>
        private const double PeriodInDaysDefaultValue = 1.0;

        /// <summary>
        /// Значение по умолчанию для параметра "Длительность, рабочие дни". Данное значение используется только,
        /// если в схеме не указано значение по умолчанию для поля <see cref="KrWeEditInterjectOptionsVirtual.Period"/>.
        /// </summary>
        private const double RevisionPeriodInDaysDefaultValue = 1.0;

        /// <summary>
        /// Массив типов обрабатываемых сигналов.
        /// </summary>
        private static readonly string[] signalTypes =
        {
            WorkflowSignalTypes.CompleteTask,
            WorkflowSignalTypes.DeleteTask,
            WorkflowSignalTypes.UpdateTask,
        };

        /// <summary>
        /// Состояние документа при обработке действия.
        /// </summary>
        private static readonly KrState activeState = KrState.Signing;

        /// <summary>
        /// Состояние документа при положительном завершении обработки действия.
        /// </summary>
        private static readonly KrState positiveState = KrState.Signed;

        /// <summary>
        /// Состояние документа при отрицательном завершении обработки действия.
        /// </summary>
        private static readonly KrState negativeState = KrState.Declined;

        /// <summary>
        /// Идентификатор варианта завершения действия при положительном завершении обработки.
        /// </summary>
        private static readonly Guid positiveActionCompletionOptionID = ActionCompletionOptions.Signed;

        /// <summary>
        /// Идентификатор варианта завершения действия при отрицательном завершении обработки.
        /// </summary>
        private static readonly Guid negativeActionCompletionOptionID = ActionCompletionOptions.Declined;

        #endregion

        #region Fields

        private readonly IRoleGetStrategy roleGetStrategy;
        private readonly IContextRoleManager contextRoleManager;
        private readonly IWorkflowBindingParser bindingParser;
        private readonly IWorkflowBindingExecutor bindingExecutor;
        private readonly ICardCache cardCache;
        private readonly Func<IPlaceholderManager> getPlaceholderManagerFunc;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSigningAction"/>.
        /// </summary>
        public KrSigningAction(
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            [Dependency(NotificationManagerNames.DeferredWithoutTransaction)]
            INotificationManager notificationManager,
            IWorkflowEngineCardsScope cardsScope,
            ICardRepository cardRepository,
            [Dependency(CardRepositoryNames.Default)]
            ICardRepository cardRepositoryDef,
            ICardServerPermissionsProvider serverPermissionsProvider,
            ISignatureProvider signatureProvider,
            Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory,
            ICardFileManager cardFileManager,
            IWorkflowBindingParser bindingParser,
            IWorkflowBindingExecutor bindingExecutor,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            ICardCache cardCache,
            Func<IPlaceholderManager> getPlaceholderManagerFunc,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  descriptor: KrDescriptors.KrSigningDescriptor,
                  notificationManager: notificationManager,
                  cardsScope: cardsScope,
                  cardRepository: cardRepository,
                  cardRepositoryDef: cardRepositoryDef,
                  serverPermissionsProvider: serverPermissionsProvider,
                  signatureProvider: signatureProvider,
                  ctcBuilderFactory: ctcBuilderFactory,
                  cardFileManager: cardFileManager,
                  requestExtender: requestExtender,
                  calendarService: calendarService,
                  krDocumentStateManager: krDocumentStateManager)
        {
            this.roleGetStrategy = roleGetStrategy;
            this.contextRoleManager = contextRoleManager;
            this.bindingParser = bindingParser;
            this.bindingExecutor = bindingExecutor;
            this.cardCache = cardCache;
            this.getPlaceholderManagerFunc = getPlaceholderManagerFunc;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override bool Compile(
            IWorkflowCompilationSyntaxTreeBuilder builder,
            WorkflowActionStorage action)
        {
            if (base.Compile(builder, action))
            {
                this.CompileEvents(builder, action);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override void PrepareForExecute(WorkflowActionStateStorage actionState, IWorkflowEngineContext context)
        {
            base.PrepareForExecute(actionState, context);
            actionState.Hash.Remove(KrSigningActionOptionsVirtual.SectionName);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Схема выполнения действия:<para/>
        /// 1. Получаем список идентификаторов обрабатываемых действием заданий;<para/>
        /// 2. Если задания есть, то очищаем список переходов;<para/>
        /// 3. Если тип сигнала - default, то:<para/>
        /// 3.1. Очищаем список переходов.<para/>
        /// 3.1. Если заданий нет, то:<para/>
        /// 3.1.1 Есть исполнители, то создаем задание и все необходимые подписки;<para/>
        /// 3.1.2. Нет исполнителей, то завершаем действие с вариантом завершения <see cref="positiveActionCompletionOptionID"/>;
        /// 3.2. Если задания есть, игнорируем создание задания;<para/>
        /// 4. Если тип сигнала из списка обрабатываемых типов сигналов, то:<para/>
        /// 4.1. Если заданий нет, то игнорируем;<para/>
        /// 4.2. Если задания есть, то для каждого задания обрабатываем сигнал.<para/>
        /// </remarks>
        protected override async Task ExecuteAsync(IWorkflowEngineContext context, IWorkflowEngineCompiled scriptObject)
        {
            await base.ExecuteAsync(context, scriptObject);

            var taskIDList = GetProcessingTaskIDList(context);
            var hasTaskIDList = taskIDList?.Count > 0;
            var signalType = context.Signal.Type;

            if (hasTaskIDList)
            {
                context.Links.Clear();
            }

            using (this.CreateTasksContext(context))
            {
                if (signalType == WorkflowSignalTypes.Default)
                {
                    context.Links.Clear();

                    if (!hasTaskIDList)
                    {
                        var sqlPerformerScript = await context.GetAsync<string>(
                            MainSectionName,
                            KrSigningActionVirtual.SqlPerformersScript);

                        var sqlPerformers = await this.GetSqlPerformers(sqlPerformerScript, context);
                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }

                        var performersRows = await context.GetAllRowsAsync(KrWeRolesVirtual.SectionName) ?? Enumerable.Empty<Dictionary<string, object>>();
                        var performers = WorkflowHelper.CombinePerformers(
                            performersRows.Select(i => new RoleEntryStorage(
                                WorkflowEngineHelper.Get<Guid>(i, Names.Table_RowID),
                                WorkflowEngineHelper.Get<Guid>(i, WorkflowEngineHelper.WildCardHashMark, Names.Table_ID),
                                WorkflowEngineHelper.Get<string>(i, WorkflowEngineHelper.WildCardHashMark, Table_Field_Name))),
                            sqlPerformers,
                            SqlApproverRoleID);

                        WorkflowEngineHelper.Set(
                            context.ActionInstance.Hash,
                            performers.Select(i => i.GetStorage()).ToArray(),
                            NamesKeys.RoleList);

                        context.ActionInstance.Hash[NamesKeys.IsNegativeActionResult] = BooleanBoxes.False;

                        if (!performers.Any())
                        {
                            context.ValidationResult.AddError(this, WorkflowHelper.GetValidatePerformerNotSpecifiedMessage(context.ActionTemplate, context.NodeTemplate));
                            return;
                        }

                        if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.ChangeStateOnStart) == true)
                        {
                            await this.SetStateIDAsync(
                                context,
                                activeState,
                                context.CancellationToken);
                        }

                        WorkflowHelper.SetCurrentPerformerIndex(context.ActionInstance.Hash, 0);

                        if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.IsParallel) == true)
                        {
                            await this.SendSigningTaskAsync(context, scriptObject, performers);
                        }
                        else
                        {
                            await this.SendSigningTaskAsync(context, scriptObject, performers.Take(1));
                        }

                        foreach (var newSignalType in signalTypes)
                        {
                            this.CreateSubscription(context, newSignalType);
                        }

                        this.SubscribeOnEvents(context);
                    }
                }
                else if (hasTaskIDList)
                {
                    switch (signalType)
                    {
                        case WorkflowSignalTypes.CompleteTask:
                            foreach (var taskID in taskIDList.Cast<Guid>().ToArray())
                            {
                                await this.CompleteTaskAsync(context, scriptObject, taskID);
                            }

                            break;

                        case WorkflowSignalTypes.DeleteTask:
                            foreach (var taskID in taskIDList.Cast<Guid>().ToArray())
                            {
                                if (await this.DeleteTaskAsync(context, taskID))
                                {
                                    taskIDList.Remove(taskID);

                                    await context.TryRemoveActiveTaskAsync(taskID);

                                    // Если действие работает в режиме параллельной отправки заданий, то не требуется выполнять других действий. Для корректной обработки удаления достаточно увеличить порядковый номер текущего исполнителя.
                                    // Удаление задания в режиме последовательной отправки заданий прерывает выполнение действия.

                                    if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.IsParallel) == true)
                                    {
                                        WorkflowHelper.CurrentPerformerIndexIncrement(context.ActionInstance.Hash);
                                    }
                                }
                            }

                            break;

                        case WorkflowSignalTypes.UpdateTask:
                            foreach (var taskID in taskIDList.Cast<Guid>())
                            {
                                await this.UpdateTaskAsync(context, taskID);
                            }

                            break;

                        default:
                            foreach (var taskID in taskIDList.Cast<Guid>())
                            {
                                await this.PerformEvent(context, scriptObject, taskID);
                            }

                            break;
                    }
                }
            }
        }

        #endregion

        #region WorkflowTaskActionBase Overrides

        /// <inheritdoc/>
        protected override async Task<string> GetResultAsync(
            IWorkflowEngineContext context,
            CardTask task)
        {
            if (task.Info.TryGet<bool>(IsProcessedTaskResultKey))
            {
                return task.Result;
            }

            if (task.TypeID == taskTypeID)
            {
                return
                    await this.GetWithPlaceholdersAsync(
                        context,
                        await context.GetAsync<string>(MainSectionName, KrSigningActionVirtual.Result),
                        task);
            }

            return task.Result;
        }

        /// <inheritdoc/>
        protected override async Task CompleteTaskCoreAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid taskOptionID,
            IWorkflowEngineCompiled scriptObject)
        {
            var oldTaskResult = task.Result;
            Guid? actionOptionID;
            using (this.CreateTasksContext(context))
            {
                actionOptionID = await this.CompleteTaskPredefinedActionProcessingAsync(context, scriptObject, task, taskOptionID);
            }

            await this.CompleteTaskScriptAsync(
                context,
                task,
                taskOptionID,
                scriptObject,
                (!string.IsNullOrEmpty(oldTaskResult) || string.IsNullOrEmpty(task.Result))
                && string.Equals(oldTaskResult, task.Result, StringComparison.Ordinal));

            if (actionOptionID.HasValue
                && !context.Cancel)
            {
                await this.CompleteActionAsync(context, scriptObject, actionOptionID.Value);
            }

            context.Cancel = false;

            if (task.State == CardRowState.Deleted)
            {
                GetProcessingTaskIDList(context)?.Remove(task.RowID);
            }
        }

        /// <inheritdoc/>
        protected override async Task DelegateTaskCoreAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask originalTask,
            CardTask delegatedTask)
        {
            var originalTaskRowID = originalTask.RowID;
            var delegatedTaskRowID = delegatedTask.RowID;

            await this.CompleteSubtasksAsync(
                context,
                scriptObject,
                originalTaskRowID,
                new[]
                {
                    DefaultTaskTypes.KrRequestCommentTypeID
                },
                async (task) =>
                {
                    task.OptionID = DefaultCompletionOptions.Cancel;
                    task.Result = "$ApprovalHistory_ParentTaskIsCompleted";
                    task.Info[IsProcessedTaskResultKey] = BooleanBoxes.True;

                    await context.TryRemoveActiveTaskAsync(task.RowID);
                });

            if (originalTask.Card.Sections.TryGetValue(KrAdditionalApprovalInfo.Name, out var oldSection))
            {
                var newSection = new CardSection(KrAdditionalApprovalInfo.Name, oldSection.GetStorage())
                {
                    Type = CardSectionType.Table
                };

                foreach (var row in newSection.Rows)
                {
                    row.Fields[KrAdditionalApprovalInfo.ID] = delegatedTaskRowID;
                    row.State = CardRowState.Inserted;
                }

                delegatedTask.Card.Sections[KrAdditionalApprovalInfo.Name].Set(newSection);
            }

            if (delegatedTask.TypeID == taskTypeID)
            {
                delegatedTask.Card.Sections[KrSigningTaskOptions.Name].Fields[KrSigningTaskOptions.AllowAdditionalApproval] =
                    BooleanBoxes.Box(await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.AllowAdditionalApproval) ?? false);
            }

            if (originalTask.Settings.TryGet<bool>(NamesKeys.CanEditCard))
            {
                delegatedTask.Settings[NamesKeys.CanEditCard] = BooleanBoxes.True;
            }

            if (originalTask.Settings.TryGet<bool>(NamesKeys.CanEditAnyFiles))
            {
                delegatedTask.Settings[NamesKeys.CanEditAnyFiles] = BooleanBoxes.True;
            }

            originalTask.Info.Add(KrReassignAdditionalApprovalStoreExtension.ReassignTo, delegatedTaskRowID);

            AddNewProcessingTaskID(context, delegatedTaskRowID);
            GetProcessingTaskIDList(context).Remove(originalTaskRowID);
            await context.AddToHistoryAsync(
                delegatedTaskRowID,
                WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));

            if (scriptObject is not null)
            {
                await scriptObject.ExecuteActionAsync(
                    KrWorkflowActionMethods.KrSigningInitMethod.MethodName,
                    KrWorkflowActionMethods.KrSigningInitMethod,
                    delegatedTask,
                    delegatedTask.Card.DynamicEntries,
                    delegatedTask.Card.DynamicTables);
            }

            await this.SendStartTaskNotificationAsync(
                context,
                scriptObject,
                delegatedTask,
                MainSectionName,
                methodDescriptor: KrWorkflowActionMethods.KrSigningStartNotificationMethod);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Обрабатывает завершение заданий в соответствии с предопределённой логикой обработки.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="taskOptionID">Идентификатор варианта завершения с которым завершается задание. Может отличаться от <see cref="CardTask.OptionID"/> из <paramref name="task"/>, если он был изменён в скрипте, обрабатывающим завершение задания.</param>
        /// <returns>Идентификатор варианта завершения действия, для которого должны быть обработаны связи выполняющиеся после завершения обработки этого действия или значение по умолчанию для типа, если действие не завершается.</returns>
        private async Task<Guid?> CompleteTaskPredefinedActionProcessingAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            Guid taskOptionID)
        {
            Guid? actionOptionID = default;
            if (task.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID)
            {
                if (taskOptionID == DefaultCompletionOptions.Approve)
                {
                    await this.CompleteAdditionalApprovalTaskAsync(context, scriptObject, task, false);
                }
                else if (taskOptionID == DefaultCompletionOptions.Disapprove)
                {
                    await this.CompleteAdditionalApprovalTaskAsync(context, scriptObject, task, true);
                }
                else if (taskOptionID == DefaultCompletionOptions.Revoke)
                {
                    await this.CompleteSubtasksAsync(context, scriptObject, task.RowID);
                }
                else if (taskOptionID == DefaultCompletionOptions.AdditionalApproval)
                {
                    await this.StartAdditionalApprovalTaskAsync(context, scriptObject, task);
                }

                context.Cancel = true;
            }
            else if (task.TypeID == taskTypeID)
            {
                if (taskOptionID == DefaultCompletionOptions.Sign)
                {
                    actionOptionID = await this.CompleteSigningTaskAsync(context, scriptObject, task, false);
                }
                else if (taskOptionID == DefaultCompletionOptions.Decline)
                {
                    actionOptionID = await this.CompleteSigningTaskAsync(context, scriptObject, task, true);
                }
                else if (taskOptionID == DefaultCompletionOptions.AdditionalApproval
                         && (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.AllowAdditionalApproval) ?? default))
                {
                    await this.StartAdditionalApprovalTaskAsync(context, scriptObject, task);
                }
                else if (taskOptionID == DefaultCompletionOptions.Delegate)
                {
                    await this.DelegateTaskAsync(
                        context,
                        scriptObject,
                        task,
                        await context.GetAsync<string>(MainSectionName, KrApprovalActionVirtual.Digest),
                        await context.GetAsync<Guid?>(MainSectionName, KrApprovalActionVirtual.Kind, Names.Table_ID),
                        await context.GetAsync<string>(MainSectionName, KrApprovalActionVirtual.Kind, Table_Field_Caption));
                }
            }
            else if (task.TypeID == DefaultTaskTypes.KrEditInterjectTypeID)
            {
                if (taskOptionID == DefaultCompletionOptions.Continue)
                {
                    actionOptionID = positiveActionCompletionOptionID;
                }
            }

            if (taskOptionID == DefaultCompletionOptions.RequestComments)
            {
                var answerTask = await this.SendRequestCommentTaskAsync(
                    context,
                    task);

                if (answerTask is null)
                {
                    return default;
                }

                await context.AddToHistoryAsync(
                    answerTask.RowID,
                    WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));

                AddNewProcessingTaskID(context, answerTask.RowID);

                if (scriptObject is not null)
                {
                    await scriptObject.ExecuteActionAsync(
                        KrWorkflowActionMethods.RequestCommentTaskInitMethod.MethodName,
                        KrWorkflowActionMethods.RequestCommentTaskInitMethod,
                        answerTask,
                        answerTask.Card.DynamicEntries,
                        answerTask.Card.DynamicTables);
                }

                await this.SendStartTaskNotificationAsync(
                    context,
                    scriptObject,
                    answerTask,
                    KrWeRequestCommentOptionsVirtual.SectionName,
                    methodDescriptor: KrWorkflowActionMethods.RequestCommentTaskStartNotificationMethod);
            }

            await this.RequestCommentTaskCompleteAsync(
                context,
                task,
                taskOptionID);

            return actionOptionID;
        }

        /// <summary>
        /// Асинхронно отправляет задание на указанную коллекцию ролей.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="roles">Коллекция ролей на которые должны быть отправлены задания.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SendSigningTaskAsync(IWorkflowEngineContext context, IWorkflowEngineCompiled scriptObject, IEnumerable<RoleEntryStorage> roles)
        {
            var periodInDays = (double?) (await context.GetAsync<object>(MainSectionName, KrSigningActionVirtual.Period)
                ?? (await context.CardMetadata.GetSectionsAsync(context.CancellationToken))[MainSectionName]
                    .Columns[KrSigningActionVirtual.Period]
                    .DefaultValue)
                ?? PeriodInDaysDefaultValue;
            var planned = await context.GetAsync<DateTime?>(MainSectionName, KrSigningActionVirtual.Planned);

            var parentTaskRowID = context.Signal.As<WorkflowEngineTaskSignal>().ParentTaskRowID;
            var authorID = await context.GetAsync<Guid?>(MainSectionName, KrSigningActionVirtual.Author, Names.Table_ID);

            authorID = await context.GetAuthorIDAsync(this.roleGetStrategy, this.contextRoleManager, authorID);
            if (!authorID.HasValue)
            {
                return;
            }

            var kindID = await context.GetAsync<Guid?>(MainSectionName, KrSigningActionVirtual.Kind, Names.Table_ID);
            var kindCaption = await context.GetAsync<string>(MainSectionName, KrSigningActionVirtual.Kind, Table_Field_Caption);

            var digest = await context.GetAsync<string>(MainSectionName, KrSigningActionVirtual.Digest);
            digest = await this.CreateDigestAsync(context, digest);

            foreach (var performer in roles)
            {
                var cardTask =
                    await context.SendTaskAsync(
                        taskTypeID,
                        digest,
                        planned,
                        null,
                        periodInDays,
                        performer.ID,
                        performer.Name,
                        parentRowID: parentTaskRowID,
                        cancellationToken: context.CancellationToken);

                if (cardTask is null
                    || !context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                context.ValidationResult.Add(WorkflowHelper.SetTaskKind(cardTask, kindID, kindCaption, this));

                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.CanEditCard) == true)
                {
                    cardTask.Settings[NamesKeys.CanEditCard] = BooleanBoxes.True;
                }

                if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.CanEditAnyFiles) == true)
                {
                    cardTask.Settings[NamesKeys.CanEditAnyFiles] = BooleanBoxes.True;
                }

                cardTask.Card.Sections[KrSigningTaskOptions.Name].Fields[KrSigningTaskOptions.AllowAdditionalApproval] =
                    BooleanBoxes.Box(await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.AllowAdditionalApproval) ?? default);

                context.ActionInstance.Hash[NamesKeys.EditInterjectAuthorID] = authorID;
                if (authorID.HasValue)
                {
                    cardTask.AddRole(
                        authorID.Value,
                        null,
                        CardFunctionRoles.AuthorID,
                        showInTaskDetails: true);
                }

                this.AddTaskToNextContextTasks(context, cardTask);
                AddNewProcessingTaskID(context, cardTask.RowID);
                await context.AddActiveTaskAsync(cardTask.RowID);
                await context.AddToHistoryAsync(cardTask.RowID, WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));

                if (scriptObject is not null)
                {
                    await scriptObject.ExecuteActionAsync(
                        KrWorkflowActionMethods.KrSigningInitMethod.MethodName,
                        KrWorkflowActionMethods.KrSigningInitMethod,
                        cardTask,
                        cardTask.Card.DynamicEntries,
                        cardTask.Card.DynamicTables);
                }

                await this.SendStartTaskNotificationAsync(
                    context,
                    scriptObject,
                    cardTask,
                    MainSectionName,
                    methodDescriptor: KrWorkflowActionMethods.KrSigningStartNotificationMethod);
            }
        }

        /// <summary>
        /// Обрабатывает создание заданий дополнительного согласования.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Задание содержащее параметры создания задания дополнительного согласования.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task StartAdditionalApprovalTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task)
        {
            var user = context.Session.User;

            var krAdditionalApprovalSectionFields = task.Card.Sections.TryGet(KrAdditionalApproval.Name)?.TryGetRawFields();
            var firstIsResponsible = krAdditionalApprovalSectionFields?.TryGet<bool>(KrAdditionalApproval.FirstIsResponsible) ?? false;
            var comment = krAdditionalApprovalSectionFields?.TryGet<string>(KrAdditionalApproval.Comment);
            var plannedDays = krAdditionalApprovalSectionFields?.TryGet<double?>(KrAdditionalApproval.TimeLimitation);

            var isAdvisoryParentTask = await context.GetAsync<bool?>(MainSectionName, KrApprovalActionVirtual.IsAdvisory) ?? false;

            foreach (var role in task.Card.Sections[KrAdditionalApprovalUsers.Name].Rows.OrderBy(static u => u.Get<int>(KrAdditionalApprovalUsers.Order)))
            {
                var roleID = role.Get<Guid>(KrAdditionalApprovalUsers.RoleID);
                var roleName = role.Get<string>(KrAdditionalApprovalUsers.RoleName);

                await this.SendAdditionalApprovalTaskAsync(
                    context,
                    scriptObject,
                    task,
                    roleID,
                    roleName,
                    firstIsResponsible,
                    user.ID,
                    comment,
                    isAdvisoryParentTask,
                    null,
                    plannedDays);

                if (firstIsResponsible)
                {
                    firstIsResponsible = false;
                }
            }
        }

        /// <summary>
        /// Отправляет задание дополнительного согласования.
        /// </summary>
        /// <param name="context">Контекст обработчики процесса.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="parentTask">Родительское задание согласования.</param>
        /// <param name="isAdvisoryParentTask">Значение <see langword="true"/>, если <paramref name="parentTask"/> является рекомендательным, иначе - <see langword="false"/>.</param>
        /// <param name="roleID">Идентификатор роли на которую отправляется задание.</param>
        /// <param name="roleName">Имя роли на которую отправляется задание.</param>
        /// <param name="isResponsible">Значение <see langword="true"/>, если <paramref name="roleID"/> является ответственным исполнителем, иначе - <see langword="false"/>.</param>
        /// <param name="authorID">Идентификатор роли автора задания.</param>
        /// <param name="comment">Комментария к заданию.</param>
        /// <param name="planned">Дата запланированного завершения задания. Имеет приоритет при определении даты завершения задания над <paramref name="plannedDays"/>.</param>
        /// <param name="plannedDays">Длительность (рабочие дни).</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SendAdditionalApprovalTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask parentTask,
            Guid roleID,
            string roleName,
            bool isResponsible,
            Guid? authorID,
            string comment,
            bool isAdvisoryParentTask,
            DateTime? planned,
            double? plannedDays)
        {
            var taskHistoryItemRowID = Guid.NewGuid();
            await this.AddTaskHistoryAsync(
                context,
                DefaultTaskTypes.KrInfoAdditionalApprovalTypeID,
                DefaultTaskTypes.KrInfoAdditionalApprovalTypeName,
                "$CardTypes_TypesNames_KrAdditionalApproval",
                DefaultCompletionOptions.AdditionalApproval,
                comment ?? (isResponsible
                    ? "{$KrMessages_ResponsibleAdditionalApprovalComment}"
                    : "{$KrMessages_DefaultAdditionalApprovalComment}"),
                modifyAction: (item) =>
                {
                    item.RowID = taskHistoryItemRowID;
                    item.ParentRowID = parentTask.RowID;
                });

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return;
            }

            var authorComment = sCard.Sections[KrApprovalCommonInfo.Name].Fields.TryGet<string>(KrApprovalCommonInfo.AuthorComment);

            var digest = string.IsNullOrWhiteSpace(authorComment)
                ? comment
                : comment + Environment.NewLine + authorComment;

            if (isResponsible)
            {
                digest = string.IsNullOrWhiteSpace(digest)
                    ? "{$KrMessages_ResponsibleAdditionalDigest}"
                    : "{$KrMessages_ResponsibleAdditionalDigest}." + Environment.NewLine + digest;
            }

            var task =
                await context.SendTaskAsync(
                    DefaultTaskTypes.KrAdditionalApprovalTypeID,
                    digest,
                    planned,
                    null,
                    plannedDays,
                    roleID,
                    roleName: roleName,
                    parentRowID: parentTask.RowID,
                    cancellationToken: context.CancellationToken);

            if (task is null)
            {
                return;
            }

            if (authorID.HasValue)
            {
                task.AddAuthor(authorID.Value);
            }

            task.HistoryItemParentRowID = taskHistoryItemRowID;

            var taskSections = task.Card.Sections;
            taskSections[TaskCommonInfo.Name].Fields[TaskCommonInfo.Info] = comment;

            await CardComponentHelper.FillTaskAssignedRolesAsync(parentTask, context.DbScope, cancellationToken: context.CancellationToken);
            var performer = parentTask.TaskAssignedRoles.FirstOrDefault(x => x.TaskRoleID == CardFunctionRoles.PerformerID && x.ParentRowID is null);

            // Не найден исполнитель, которому нужно вернуть задание.
            if (performer is null)
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(CardValidationKeys.UnknownRoleInTask, Guid.Empty, context.ProcessInstance.CardID)
                    .End();
                return;
            }

            var taskTaskInfoInfoFields = taskSections[KrAdditionalApprovalTaskInfo.Name].Fields;
            taskTaskInfoInfoFields[KrAdditionalApprovalTaskInfo.AuthorRoleID] = performer.RoleID;
            taskTaskInfoInfoFields[KrAdditionalApprovalTaskInfo.AuthorRoleName] = performer.RoleName;
            taskTaskInfoInfoFields[KrAdditionalApprovalTaskInfo.IsResponsible] = BooleanBoxes.Box(isResponsible);

            this.AddTaskToNextContextTasks(context, task);
            AddNewProcessingTaskID(context, task.RowID);
            await context.AddActiveTaskAsync(task.RowID);
            await context.AddToHistoryAsync(
                task.RowID,
                WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash),
                isAdvisoryParentTask);

            if (scriptObject is not null)
            {
                await scriptObject.ExecuteActionAsync(
                    KrWorkflowActionMethods.AdditionalApprovalTaskInitMethod.MethodName,
                    KrWorkflowActionMethods.AdditionalApprovalTaskInitMethod,
                    task,
                    task.Card.DynamicEntries,
                    task.Card.DynamicTables);
            }

            await this.SendStartTaskNotificationAsync(
                context,
                scriptObject,
                task,
                KrWeAdditionalApprovalOptionsVirtual.SectionName,
                methodDescriptor: KrWorkflowActionMethods.AdditionalApprovalTaskStartNotificationMethod);
        }

        /// <summary>
        /// Обрабатывает завершение задания согласования.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="isNegativeResult">Значение <see langword="true"/>, если задание было завершено отказом, иначе - <see langword="false"/>.</param>
        /// <returns>Идентификатор варианта завершения действия, для которого должны быть обработаны связи выполняющиеся после завершения обработки этого действия или значение по умолчанию для типа, если действие не завершается.</returns>
        private async Task<Guid?> CompleteSigningTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            bool isNegativeResult)
        {
            await context.TryRemoveActiveTaskAsync(task.RowID);
            await this.CompleteSubtasksAsync(context, scriptObject, task.RowID);

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return null;
            }

            await WorkflowHelper.AppendApprovalInfoUserCompleteTaskAsync(
                sCard.Sections,
                context.DbScope,
                context.Session.User,
                task,
                isNegativeResult,
                context.CancellationToken);

            if (isNegativeResult)
            {
                context.ActionInstance.Hash[NamesKeys.IsNegativeActionResult] = BooleanBoxes.True;
            }

            if (isNegativeResult
                || !(await this.cardCache.Cards.GetAsync(KrSettings.Name, context.CancellationToken))
                    .GetValue()
                    .Sections[KrSettings.Name].Fields.Get<bool>(KrSettings.HideCommentForApprove))
            {
                var comment = task.Card.Sections[KrTask.Name].Fields.TryGet<string>(KrTask.Comment);

                if (!string.IsNullOrWhiteSpace(comment))
                {
                    if (this.bindingParser.IsBinding(comment))
                    {
                        comment = await this.bindingExecutor.GetAsync<string>(context, comment);
                    }

                    task.Result = await this.GetWithPlaceholdersAsync(
                        context,
                        comment,
                        task);
                }
            }

            var currentPerformerIndex = WorkflowHelper.CurrentPerformerIndexIncrement(context.ActionInstance.Hash);

            // Завершено последнее задание?

            // может быть List<object> или List<Dictionary<string, object>>, поэтому используем ковариантный интерфейс
            if (currentPerformerIndex >= context.ActionInstance.Hash.Get<IReadOnlyCollection<object>>(NamesKeys.RoleList).Count)
            {
                if (context.ActionInstance.Hash.Get<bool>(NamesKeys.IsNegativeActionResult))
                {
                    await this.NegativeResultProcessingAsync(context);
                    return negativeActionCompletionOptionID;
                }

                await this.PositiveResultProcessingAsync(context, scriptObject);
                return positiveActionCompletionOptionID;
            }
            else
            {
                if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.IsParallel) != true)
                {
                    if (isNegativeResult)
                    {
                        await this.NegativeResultProcessingAsync(context);

                        if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.ExpectAllSigners) != true)
                        {
                            return negativeActionCompletionOptionID;
                        }
                    }

                    await this.SendSigningTaskAsync(
                        context,
                        scriptObject,
                        context.ActionInstance.Hash
                            // может быть List<object> или List<Dictionary<string, object>>, поэтому используем ковариантный интерфейс
                            .Get<IReadOnlyCollection<object>>(NamesKeys.RoleList)
                            .Select(i => new RoleEntryStorage((Dictionary<string, object>) i))
                            .Skip(currentPerformerIndex)
                            .Take(1));

                    context.Cancel = true;
                }
            }

            return null;
        }

        /// <summary>
        /// Обрабатывает завершение задания дополнительного согласования.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="disapproved">Значение <see langword="true"/>, если задание было завершено отказом, иначе - <see langword="false"/>.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task CompleteAdditionalApprovalTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            bool disapproved)
        {
            await context.TryRemoveActiveTaskAsync(task.RowID);
            await this.CompleteSubtasksAsync(context, scriptObject, task.RowID);

            var comment = task.Card.Sections[KrAdditionalApprovalTaskInfo.Name].Fields
                .TryGet<string>(KrAdditionalApprovalTaskInfo.Comment);

            if (!string.IsNullOrEmpty(comment))
            {
                if (this.bindingParser.IsBinding(comment))
                {
                    comment = await this.bindingExecutor.GetAsync<string>(context, comment);
                }

                task.Result = await this.GetWithPlaceholdersAsync(
                    context,
                    comment,
                    task);
            }

            var parentTaskRowID = task.ParentRowID;
            if (!parentTaskRowID.HasValue)
            {
                return;
            }

            Guid? approverID;
            Guid? responsibleID;
            int notCompleted;

            await using (context.DbScope.Create())
            {
                approverID = await context.DbScope.Db
                    .SetCommand(
                        context.DbScope.BuilderFactory
                            .Select().C("UserID")
                            .From(Names.Tasks).NoLock()
                            .Where().C(Names.Tasks_RowID).Equals().P("RowID")
                            .Build(),
                        context.DbScope.Db.Parameter("RowID", parentTaskRowID.Value))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(context.CancellationToken);

                var isResponsible = task.Card.Sections[KrAdditionalApprovalTaskInfo.Name].Fields
                    .TryGet<bool>(KrAdditionalApprovalTaskInfo.IsResponsible);

                responsibleID = isResponsible
                    ? await context.DbScope.Db
                        .SetCommand(
                            context.DbScope.BuilderFactory
                                .Select().C("t", "UserID")
                                .From(Names.Tasks, "t").NoLock()
                                .InnerJoin(KrAdditionalApprovalInfo.Name, "i").NoLock()
                                .On().C("i", KrAdditionalApprovalInfo.RowID).Equals().C("t", Names.Tasks_RowID)
                                .Where().C("i", KrAdditionalApprovalInfo.ID).Equals().P("RowID")
                                .And().C("i", KrAdditionalApprovalInfo.IsResponsible).Equals().V(true)
                                .Build(),
                            context.DbScope.Db.Parameter("RowID", parentTaskRowID.Value))
                        .LogCommand()
                        .ExecuteAsync<Guid?>(context.CancellationToken)
                    : default;

                notCompleted = await context.DbScope.Db
                    .SetCommand(
                        context.DbScope.BuilderFactory
                            .Select().Count().Substract(1)
                            .From(KrAdditionalApprovalInfo.Name).NoLock()
                            .Where().C(KrAdditionalApprovalInfo.ID).Equals().P("RowID")
                            .And().C(KrAdditionalApprovalInfo.Completed).IsNull()
                            .Build(),
                        context.DbScope.Db.Parameter("RowID", parentTaskRowID.Value))
                    .LogCommand()
                    .ExecuteAsync<int>(context.CancellationToken);
            }

            var roleList = new List<Guid>();
            if (approverID.HasValue)
            {
                roleList.Add(approverID.Value);
            }

            if (responsibleID.HasValue)
            {
                roleList.Add(responsibleID.Value);
            }

            if (roleList.Count > 0)
            {
                var isCompleted = notCompleted == 0;

                context.ValidationResult.Add(
                    await this.notificationManager
                        .SendAsync(
                            isCompleted
                                ? DefaultNotifications.AdditionalApprovalNotificationCompleted
                                : DefaultNotifications.AdditionalApprovalNotification,
                            roleList,
                            new NotificationSendContext()
                            {
                                MainCardID = context.ProcessInstance.CardID,
                                Info = Shared.Notices.NotificationHelper.GetInfoWithTask(task),
                                GetCardFuncAsync = (validationResult, ct) =>
                                    this.cardsScope.GetCardAsync(
                                        context.ProcessInstance.CardID,
                                        validationResult,
                                        ct),
                                ModifyEmailActionAsync = async (email, ct) =>
                                {
                                    if (!isCompleted)
                                    {
                                        email.PlaceholderAliases.SetReplacement(
                                            "subjectLabel",
                                            disapproved
                                                ? "$DisapprovedAdditionalApprovalNotificationTemplate_SubjectLabel"
                                                : "$ApprovedAdditionalApprovalNotificationTemplate_SubjectLabel");
                                        email.PlaceholderAliases.SetReplacement(
                                            "taskCount",
                                            $"text:{notCompleted}");
                                    }

                                    email.PlaceholderAliases.SetReplacement(
                                        "resultLabel",
                                        disapproved
                                            ? "$DisapprovedNotificationTemplate_BodyLabel"
                                            : "$ApprovedNotificationTemplate_BodyLabel");
                                }
                            },
                            context.CancellationToken));
            }
        }

        /// <summary>
        /// Обрабатывает положительный вариант завершения основного задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task PositiveResultProcessingAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject)
        {
            if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.ReturnWhenApproved) == true)
            {
                await this.SendEditInterjectTaskAsync(context, scriptObject);

                context.Cancel = true;
            }
        }

        /// <summary>
        /// Асинхронно отправляет задание доработки автором.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SendEditInterjectTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject)
        {
            var cardTask = await context.SendEditInterjectTaskAsync(
                this.getPlaceholderManagerFunc(),
                this.roleGetStrategy,
                this.contextRoleManager,
                context.ActionInstance.Hash.Get<Guid>(NamesKeys.EditInterjectAuthorID),
                RevisionPeriodInDaysDefaultValue);

            if (cardTask is null)
            {
                return;
            }

            this.AddTaskToNextContextTasks(context, cardTask);
            AddNewProcessingTaskID(context, cardTask.RowID);
            await context.AddActiveTaskAsync(cardTask.RowID);
            await context.AddToHistoryAsync(cardTask.RowID, WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));

            if (scriptObject is not null)
            {
                await scriptObject.ExecuteActionAsync(
                    KrWorkflowActionMethods.EditInterjectTaskInitMethod.MethodName,
                    KrWorkflowActionMethods.EditInterjectTaskInitMethod,
                    cardTask,
                    cardTask.Card.DynamicEntries,
                    cardTask.Card.DynamicTables);
            }

            await this.SendStartTaskNotificationAsync(
                context,
                scriptObject,
                cardTask,
                KrWeEditInterjectOptionsVirtual.SectionName,
                methodDescriptor: KrWorkflowActionMethods.EditInterjectTaskStartNotificationMethod);
        }

        /// <summary>
        /// Обрабатывает отрицательный вариант завершения основного задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task NegativeResultProcessingAsync(
            IWorkflowEngineContext context)
        {
            var taskHistoryItemRowID = Guid.NewGuid();
            await this.AddTaskHistoryByTaskAsync(
                context,
                DefaultTaskTypes.KrRebuildTypeID,
                DefaultCompletionOptions.RebuildDocument,
                default,
                modifyAction: item => item.RowID = taskHistoryItemRowID);

            await context.AddToHistoryAsync(taskHistoryItemRowID, WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));
        }

        /// <summary>
        /// Асинхронно отзывает дочерние задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="taskRowID">Идентификатор родительского задания.</param>
        /// <returns>Асинхронная задача.</returns>
        private Task CompleteSubtasksAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            Guid taskRowID)
        {
            return this.CompleteSubtasksAsync(
                context,
                scriptObject,
                taskRowID,
                new[]
                {
                    DefaultTaskTypes.KrAdditionalApprovalTypeID,
                    DefaultTaskTypes.KrRequestCommentTypeID
                },
                async (task) =>
                {
                    task.OptionID = task.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID
                        ? DefaultCompletionOptions.Revoke
                        : DefaultCompletionOptions.Cancel;
                    task.Result = "$ApprovalHistory_ParentTaskIsCompleted";
                    task.Info[IsProcessedTaskResultKey] = BooleanBoxes.True;

                    await context.TryRemoveActiveTaskAsync(task.RowID);
                });
        }

        /// <summary>
        /// Асинхронно обрабатывает скрипт выполняющийся при завершении задания и отправляет уведомления.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="optionID">Идентификатор варианта завершения с которым завершается задание. Может отличаться от <see cref="CardTask.OptionID"/> из <paramref name="task"/>, если он был изменён в скрипте, обрабатывающим завершение задания.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="isSetResult">Значение <see langword="true"/>, если результат завершения задания определяется настройками обработки варианта завершения задания, иначе - <see langword="false"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task CompleteTaskScriptAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid optionID,
            IWorkflowEngineCompiled scriptObject,
            bool isSetResult)
        {
            var optionRows = await context.GetAllRowsAsync(context.ActionTemplate.Hash, KrSigningActionOptionsVirtual.SectionName);

            if (optionRows?.Any() == true)
            {
                var taskTypeID = task.TypeID;
                var processRows = optionRows
                    .Where(x =>
                        WorkflowEngineHelper.Get<Guid?>(
                            x,
                            ActionSeveralTaskTypesOptionsBase.TaskType,
                            Names.Table_ID) == taskTypeID
                        && WorkflowEngineHelper.Get<Guid?>(
                            x,
                            ActionOptionsBase.Option,
                            Names.Table_ID) == optionID);

                foreach (var processRow in processRows)
                {
                    if (isSetResult)
                    {
                        var completionResult = processRow.TryGet<string>(KrSigningActionOptionsVirtual.Result);
                        if (!string.IsNullOrWhiteSpace(completionResult))
                        {
                            if (this.bindingParser.IsBinding(completionResult))
                            {
                                completionResult = await this.bindingExecutor.GetAsync<string>(context, completionResult);
                            }

                            task.Result = await this.GetWithPlaceholdersAsync(
                                context,
                                completionResult,
                                task);
                        }
                    }

                    var info = new WorkflowTaskNotificationInfo(
                        context,
                        processRow,
                        ActionNotificationRolesBase.Option,
                        KrSigningActionNotificationRolesVirtual.SectionName);

                    if (scriptObject is not null)
                    {
                        await scriptObject.ExecuteActionAsync(
                            KrWorkflowActionMethods.KrSigningOptionMethod.GetMethodName(processRow),
                            KrWorkflowActionMethods.KrSigningOptionMethod,
                            task,
                            task.Card.DynamicEntries, task.Card.DynamicTables,
                            info);
                    }

                    await this.SendCompleteTaskNotificationAsync(
                        context,
                        scriptObject,
                        task,
                        info);
                }
            }
        }

        /// <summary>
        /// Асинхронно обрабатывает завершение действия.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="actionOptionID">Идентификатор варианта завершения действия.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task CompleteActionAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            Guid actionOptionID)
        {
            if (await context.GetAsync<bool?>(MainSectionName, KrSigningActionVirtual.ChangeStateOnEnd) == true)
            {
                await this.SetStateIDAsync(
                    context,
                    actionOptionID == positiveActionCompletionOptionID ? positiveState : negativeState,
                    context.CancellationToken);
            }

            var optionActionRows = await context.GetAllRowsAsync(context.ActionTemplate.Hash, KrSigningActionOptionsActionVirtual.SectionName);

            if (optionActionRows?.Any() == true)
            {
                var processRows = optionActionRows
                    .Where(x => WorkflowEngineHelper.Get<Guid?>(
                        x, ActionOptionsActionBase.ActionOption, Names.Table_ID) == actionOptionID);

                var linksForPerforming = new HashSet<Guid>();

                foreach (var processRow in processRows)
                {
                    var info = new WorkflowTaskNotificationInfo(
                        context,
                        processRow,
                        KrSigningActionNotificationActionRolesVirtual.Option,
                        KrSigningActionNotificationActionRolesVirtual.SectionName);

                    if (scriptObject is not null)
                    {
                        await scriptObject.ExecuteActionAsync(
                            KrWorkflowActionMethods.KrSigningActionOptionActionMethod.GetMethodName(info.Row),
                            KrWorkflowActionMethods.KrSigningActionOptionActionMethod,
                            info);
                    }

                    await this.SendCompleteActionNotificationAsync(
                        context,
                        scriptObject,
                        info,
                        KrWorkflowActionMethods.KrSigningCompleteActionNotificationMethod.GetMethodName(info.Row),
                        KrWorkflowActionMethods.KrSigningCompleteActionNotificationMethod);

                    var linkRows = context.ActionTemplate.Hash.TryGet<IList>(KrSigningActionOptionLinksVirtual.SectionName);
                    var rowID = WorkflowEngineHelper.Get<Guid>(processRow, Names.Table_RowID);
                    var linkIDs =
                        linkRows?
                            .Cast<Dictionary<string, object>>()
                            .Where(x => WorkflowEngineHelper.Get<Guid?>(
                                x, ActionOptionActionLinksBase.ActionOption, Names.Table_RowID) == rowID)
                            .Select(x => WorkflowEngineHelper.Get<Guid>(
                                x, ActionOptionActionLinksBase.Link, Names.Table_ID))
                        ?? EmptyHolder<Guid>.Collection;

                    foreach (var linkID in linkIDs)
                    {
                        linksForPerforming.Add(linkID);
                    }
                }

                foreach (var linkID in linksForPerforming)
                {
                    context.Links[linkID] = WorkflowEngineSignal.CreateDefaultSignal(StorageHelper.Clone(context.Signal.Hash));
                }
            }
        }

        #endregion
    }
}
