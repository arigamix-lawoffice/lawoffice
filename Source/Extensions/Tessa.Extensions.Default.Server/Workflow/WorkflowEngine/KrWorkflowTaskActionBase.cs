using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Actions.Descriptors;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Signals;
using Tessa.Workflow.Storage;

using Unity;

using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using static Tessa.Workflow.Actions.WorkflowDialogAction;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Базовый класс для обработчиков действий маршрутов.
    /// </summary>
    public abstract class KrWorkflowTaskActionBase : KrWorkflowActionBase
    {
        #region Nested Types

        private static class TaskActions
        {
            public const int InProgress = 0;
            public const int ReturnToRole = 1;
            public const int Postpone = 2;
            public const int ReturnFromPostpone = 3;
            public const int Complete = 4;
        }

        private sealed class TasksContext : IDisposable
        {
            private readonly IWorkflowEngineContext context;
            private List<CardTask> processTaskList;

            public TasksContext(
                IWorkflowEngineContext context)
            {
                this.context = context;

                context.Info[nameof(TasksContext)] = this;
            }

            public void AddTask(CardTask task)
            {
                if (this.processTaskList is null)
                {
                    this.processTaskList = new List<CardTask>();
                }
                // Если нет заданий в контексте, то первое обрабатываемое задание добавляем в контекст выполнения для текущего действия
                // Для последующих действий и узлов уже используется список из processTaskList
                if (this.context.Tasks.Count == 0)
                {
                    this.context.Tasks.Add(task);
                }
                this.processTaskList.Add(task);
            }

            public void Dispose()
            {
                if (this.processTaskList is not null)
                {
                    this.context.Tasks.Clear();
                    this.context.Tasks.AddRange(this.processTaskList);
                }
                this.context.Info.Remove(nameof(TasksContext));
            }
        }

        #endregion

        #region Fields

        public const string EventsSectionName = "WeTaskActionEvents";
        public const string NotificationRolesSectionName = "WeTaskActionNotificationRoles";
        public const string DialogsSectionName = "WeTaskActionDialogs";

        public static readonly Tuple<string, string>[] TaskParams = new Tuple<string, string>[]
        {
            new Tuple<string, string>(nameof(CardTask), "task"),
            new Tuple<string, string>("dynamic", "taskCard"),
            new Tuple<string, string>("dynamic", "taskCardTables"),
        };

        public static readonly Tuple<string, string>[] TaskCompleteParams = new Tuple<string, string>[]
        {
            new Tuple<string, string>(nameof(CardTask), "task"),
            new Tuple<string, string>("dynamic", "taskCard"),
            new Tuple<string, string>("dynamic", "taskCardTables"),
            new Tuple<string, string>(nameof(WorkflowTaskNotificationInfo), "notificationInfo"),
        };
        protected readonly INotificationManager notificationManager;
        protected readonly IWorkflowEngineCardsScope cardsScope;
        private readonly ICardRepository cardRepositoryDef;
        protected readonly ICardServerPermissionsProvider serverPermissionsProvider;
        protected readonly ISignatureProvider signatureProvider;
        protected readonly Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory;
        protected readonly ICardFileManager cardFileManager;

        #endregion

        #region Constructors

        protected KrWorkflowTaskActionBase(
            WorkflowActionDescriptor descriptor,
            INotificationManager notificationManager,
            IWorkflowEngineCardsScope cardsScope,
            ICardRepository cardRepository,
            ICardRepository cardRepositoryDef,
            ICardServerPermissionsProvider serverPermissionsProvider,
            ISignatureProvider signatureProvider,
            Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory,
            ICardFileManager cardFileManager,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IKrDocumentStateManager krDocumentStateManager)
                : base(
                      actionDescriptor: descriptor,
                      cardRepository: cardRepository,
                      requestExtender: requestExtender,
                      calendarService: calendarService,
                      krDocumentStateManager: krDocumentStateManager)
        {
            this.notificationManager = notificationManager;
            this.cardsScope = cardsScope;
            this.cardRepositoryDef = cardRepositoryDef;
            this.serverPermissionsProvider = serverPermissionsProvider;
            this.signatureProvider = signatureProvider;
            this.ctcBuilderFactory = ctcBuilderFactory;
            this.cardFileManager = cardFileManager;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override void PrepareForExecute(WorkflowActionStateStorage actionState, IWorkflowEngineContext context)
        {
            actionState.Hash.Remove(EventsSectionName);
            actionState.Hash.Remove(DialogsSectionName);
            actionState.Hash.Remove(ButtonsSectionName);
            actionState.Hash.Remove(ButtonLinksSectionName);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Выполняет компиляцию методов - обработчиков событий.
        /// </summary>
        /// <param name="builder">Билдер добавления методов в действия WorkflowEngine.</param>
        /// <param name="action">Действие, компиляция скриптов которого должна быть выполнена.</param>
        protected void CompileEvents(
            IWorkflowCompilationSyntaxTreeBuilder builder,
            WorkflowActionStorage action)
        {
            var eventsRows = action.Hash.TryGet<IList>(EventsSectionName);

            if (eventsRows is not null
                && eventsRows.Count > 0)
            {
                List<string> inWork = new List<string>(),
                             retToRole = new List<string>(),
                             postpone = new List<string>(),
                             retToPostpone = new List<string>(),
                             complete = new List<string>();
                foreach (Dictionary<string, object> row in eventsRows)
                {
                    if (!string.IsNullOrWhiteSpace(WorkflowEngineHelper.Get<string>(row, WorkflowActionMethods.TaskEventMethod.StorePath)))
                    {
                        switch (WorkflowEngineHelper.Get<int>(row, "Event", "ID"))
                        {
                            case TaskActions.InProgress:
                                inWork.Add(WorkflowActionMethods.TaskEventMethod.GetMethodName(row));
                                break;
                            case TaskActions.ReturnToRole:
                                retToRole.Add(WorkflowActionMethods.TaskEventMethod.GetMethodName(row));
                                break;
                            case TaskActions.Postpone:
                                postpone.Add(WorkflowActionMethods.TaskEventMethod.GetMethodName(row));
                                break;
                            case TaskActions.ReturnFromPostpone:
                                retToPostpone.Add(WorkflowActionMethods.TaskEventMethod.GetMethodName(row));
                                break;
                            case TaskActions.Complete:
                                complete.Add(WorkflowActionMethods.TaskEventMethod.GetMethodName(row));
                                break;
                        }
                    }
                }

                this.CreateGlobalEventMethod(builder, inWork, nameof(TaskActions.InProgress));
                this.CreateGlobalEventMethod(builder, retToRole, nameof(TaskActions.ReturnToRole));
                this.CreateGlobalEventMethod(builder, postpone, nameof(TaskActions.Postpone));
                this.CreateGlobalEventMethod(builder, retToPostpone, nameof(TaskActions.ReturnFromPostpone));
                this.CreateGlobalEventMethod(builder, complete, nameof(TaskActions.Complete));
            }
        }

        /// <summary>
        /// Метод для создания подписки на обрабатываемые события.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        protected void SubscribeOnEvents(IWorkflowEngineContext context)
        {
            var eventsRows = context.ActionTemplate.Hash.TryGet<IList>(EventsSectionName);

            if (eventsRows is not null)
            {
                bool inWork = false, retToRole = false, postpone = false, retToPostpone = false;
                foreach (Dictionary<string, object> row in eventsRows)
                {
                    switch (WorkflowEngineHelper.Get<int>(row, "Event", "ID"))
                    {
                        case TaskActions.InProgress:
                            inWork = true;
                            break;
                        case TaskActions.ReturnToRole:
                            retToRole = true;
                            break;
                        case TaskActions.Postpone:
                            postpone = true;
                            break;
                        case TaskActions.ReturnFromPostpone:
                            retToPostpone = true;
                            break;
                    }
                }

                if (inWork)
                {
                    this.CreateSubscription(context, WorkflowSignalTypes.ProgressTask);
                }
                if (retToRole)
                {
                    this.CreateSubscription(context, WorkflowSignalTypes.ReinstateTask);
                }
                if (postpone)
                {
                    this.CreateSubscription(context, WorkflowSignalTypes.PostponeTask);
                }
                if (retToPostpone)
                {
                    this.CreateSubscription(context, WorkflowSignalTypes.ReturnFromPostponeTask);
                }
            }
        }

        /// <summary>
        /// Метод для обработки события.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="taskID">Идентификатор обрабатываемого задания.</param>
        /// <param name="force">Значение <see langword="true"/>, если не надо выполнять проверку возможности обработки задания с идентификатором <paramref name="taskID"/> в соответствии с параметрами сигнала типа <see cref="WorkflowEngineTaskSignal"/>, иначе - <see langword="false"/>.</param>
        /// <seealso cref="WorkflowEngineTaskSignal"/>
        /// <seealso cref="WorkflowEngineTaskSignal.TaskExists(Guid)"/>
        protected async Task PerformEvent(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            Guid taskID,
            bool force = default)
        {
            if (force || context.Signal.As<WorkflowEngineTaskSignal>().TaskExists(taskID))
            {
                var task = await context.GetTaskAsync(taskID, context.CancellationToken);

                if (task is not null)
                {
                    this.AddTaskToNextContextTasks(context, task);
                    if (scriptObject is not null)
                    {
                        var methodName = context.Signal.Type switch
                        {
                            WorkflowSignalTypes.ProgressTask => nameof(TaskActions.InProgress),
                            WorkflowSignalTypes.ReinstateTask => nameof(TaskActions.ReturnToRole),
                            WorkflowSignalTypes.PostponeTask => nameof(TaskActions.Postpone),
                            WorkflowSignalTypes.ReturnFromPostponeTask => nameof(TaskActions.ReturnFromPostpone),
                            _ => null,
                        };

                        if (methodName is not null)
                        {
                            await scriptObject.ExecuteActionAsync(
                                methodName,
                                WorkflowActionMethods.TaskEventMethod,
                                task,
                                task.Card.DynamicEntries,
                                task.Card.DynamicTables);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для создания подписок действия.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="signalType">Тип сигнала для которого выполняется создание подписки.</param>
        protected void CreateSubscription(IWorkflowEngineContext context, string signalType)
        {
            var preconditions = context.ActionTemplate.PreConditions;
            if (preconditions is null
                || preconditions.Count == 0
                || preconditions.Contains(signalType))
            {
                context.CommandSubscriptions.Add(new WorkflowCommandSubscriptionStorage(context.NodeInstance.ID, context.ProcessInstance.ID, signalType));
            }
        }

        /// <summary>
        /// Метод для удаления задания с его историей.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="currentTaskID">Идентификатор обрабатываемого задания.</param>
        /// <param name="force">Значение <see langword="true"/>, если не надо выполнять проверку возможности обработки задания с идентификатором <paramref name="taskID"/> в соответствии с параметрами сигнала типа <see cref="WorkflowEngineTaskSignal"/>, иначе - <see langword="false"/>.</param>
        /// <returns>Значение <see langword="true"/>, если задание с указанным идентификатором удалено, иначе - <see langword="false"/>.</returns>
        /// <seealso cref="WorkflowEngineTaskSignal"/>
        /// <seealso cref="WorkflowEngineTaskSignal.TaskExists(Guid)"/>
        /// <seealso cref="DeleteTaskCoreAsync(IWorkflowEngineContext, Guid)"/>
        protected async Task<bool> DeleteTaskAsync(IWorkflowEngineContext context, Guid currentTaskID, bool force = false)
        {
            var taskSignal = context.Signal.As<WorkflowEngineTaskSignal>();
            if (force
                || taskSignal.TaskExists(currentTaskID))
            {
                var mainCard = await context.GetMainCardAsync(context.CancellationToken);

                if (mainCard is not null)
                {
                    var task = mainCard.Tasks.FirstOrDefault(x => x.RowID == currentTaskID);
                    if (task is not null)
                    {
                        task.State = CardRowState.Deleted;
                        this.AddTaskToNextContextTasks(context, task);

                        await this.DeleteTaskCoreAsync(context, currentTaskID);

                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Метод для обновления активного задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="currentTaskID">Идентификатор обрабатываемого задания.</param>
        /// <remarks>Выполняет обработку только тех заданий, которые удовлетворяют проверку <see cref="WorkflowEngineTaskSignal.TaskExists(Guid)"/>, где в качестве параметра передаётся <paramref name="currentTaskID"/>.</remarks>
        protected async Task UpdateTaskAsync(IWorkflowEngineContext context, Guid currentTaskID)
        {
            var taskSignal = context.Signal.As<WorkflowEngineTaskSignal>();
            if (taskSignal.TaskExists(currentTaskID))
            {
                var mainCard = await context.GetMainCardAsync(context.CancellationToken);
                if (mainCard is null)
                {
                    return;
                }

                var task = mainCard.Tasks.FirstOrDefault(x => x.RowID == currentTaskID);

                if (task is not null)
                {
                    this.AddTaskToNextContextTasks(context, task);
                    task.State = CardRowState.Modified;
                    if (taskSignal.Date.HasValue)
                    {
                        task.Planned = taskSignal.Date;
                        task.PlannedQuants = null;
                        task.Flags |= CardTaskFlags.UpdatePlanned;
                    }
                    if (!string.IsNullOrEmpty(taskSignal.Digest))
                    {
                        var placeholderManager = context.Container.Resolve<IPlaceholderManager>();
                        task.Digest = await placeholderManager.ReplaceTextAsync(
                            taskSignal.Digest,
                            context.Session,
                            context.Container,
                            context.DbScope,
                            null,
                            mainCard,
                            task: context.Task,
                            info: context.CreatePlaceholderInfo(),
                            withScripts: true,
                            cancellationToken: context.CancellationToken);
                        task.Flags |= CardTaskFlags.UpdateDigest;
                    }
                    if (taskSignal.RoleID.HasValue)
                    {
                        await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope, cancellationToken: context.CancellationToken);
                        
                        task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID).ForEach(x => x.State = CardTaskAssignedRoleState.Deleted);
                        task.AddRole(
                            taskSignal.RoleID.Value,
                            taskSignal.RoleName,
                            CardFunctionRoles.PerformerID,
                            master: true);
                        task.Flags |= CardTaskFlags.UpdateTaskAssignedRoles;
                        if (task.StoredState != CardTaskState.Created)
                        {
                            task.Action = CardTaskAction.Reinstate;
                            task.Info[WorkflowEngineHelper.TaskCompletedByWorkflowEngineKey] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для обработки завершения задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="currentTaskID">Идентификатор обрабатываемого задания.</param>
        /// <param name="force">Значение <see langword="true"/>, если не надо выполнять проверку возможности обработки задания с идентификатором <paramref name="taskID"/> в соответствии с параметрами сигнала типа <see cref="WorkflowEngineTaskSignal"/>, иначе - <see langword="false"/>.</param>
        /// <param name="overrideOptionID"></param>
        /// <seealso cref="WorkflowEngineTaskSignal"/>
        /// <seealso cref="WorkflowEngineTaskSignal.TaskExists(Guid)"/>
        protected async Task CompleteTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            Guid currentTaskID,
            bool force = false,
            Guid? overrideOptionID = null)
        {
            var taskSignal = context.Signal.As<WorkflowEngineTaskSignal>();
            if (force
                || taskSignal.TaskExists(currentTaskID))
            {
                // Если переданный сигнал - сигнал задания и это действие отправило задание
                var storeTask = context.StoreCard?.Tasks.FirstOrDefault(x => x.RowID == currentTaskID);
                if (storeTask is not null && storeTask.Action == CardTaskAction.Complete)
                {
                    this.AddTaskToNextContextTasks(context, storeTask);

                    // Формируем результат
                    storeTask.Result = await this.GetResultAsync(context, storeTask);

                    if (scriptObject is not null)
                    {
                        await scriptObject.ExecuteActionAsync(
                            nameof(TaskActions.Complete),
                            WorkflowActionMethods.TaskEventMethod,
                            storeTask,
                            storeTask.Card.DynamicEntries,
                            storeTask.Card.DynamicTables);
                    }

                    // Берем вариант завершения и по нему рассчитываем переходы, которые нужно выполнить
                    var optionID = storeTask.OptionID ?? Guid.Empty;

                    await this.CompleteTaskCoreAsync(context, storeTask, optionID, scriptObject);
                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return;
                    }

                    await this.ProcessDialogAsync(context, storeTask, optionID, scriptObject);
                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return;
                    }

                    // Записываем результат в TaskHistory
                    await using (context.DbScope.Create())
                    {
                        var db = context.DbScope.Db;

                        // запрос на обновление результата в истории заданий
                        await db.SetCommand(
                            context.DbScope.BuilderFactory
                                .Update("TaskHistory")
                                .C("Result").Equals().P("Result")
                                .Where().C("RowID").Equals().P("RowID")
                                .Build(),
                            db.Parameter("RowID", currentTaskID),
                            db.Parameter("Result", storeTask.Result))
                            .LogCommand()
                            .ExecuteNonQueryAsync(context.CancellationToken);
                    }
                }
                else
                {
                    var mainCard = await context.GetMainCardAsync(context.CancellationToken);
                    if (mainCard is null)
                    {
                        return;
                    }

                    var task = mainCard.Tasks.FirstOrDefault(x => x.RowID == currentTaskID);
                    if (task is not null)
                    {
                        this.AddTaskToNextContextTasks(context, task);
                        var result = await this.GetResultAsync(context, task);

                        var optionID = overrideOptionID ?? taskSignal.OptionID ?? Guid.Empty;
                        var optionInfo = (await context.CardMetadata.GetCardTypesAsync(context.CancellationToken))[task.TypeID]
                            .CompletionOptions.FirstOrDefault(x => x.TypeID == optionID);

                        task.Action = CardTaskAction.Complete;
                        task.State = optionInfo is not null && optionInfo.Flags.Has(CardTypeCompletionOptionFlags.DoNotDeleteTask) ? CardRowState.Modified : CardRowState.Deleted;
                        task.OptionID = optionID;
                        task.Result = result;
                        task.Info[WorkflowEngineHelper.TaskCompletedByWorkflowEngineKey] = true;

                        if (scriptObject is not null)
                        {
                            await scriptObject.ExecuteActionAsync(
                                nameof(TaskActions.Complete),
                                WorkflowActionMethods.TaskEventMethod,
                                task,
                                task.Card.DynamicEntries,
                                task.Card.DynamicTables);
                        }

                        await this.CompleteTaskCoreAsync(context, task, optionID, scriptObject);
                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }

                        await this.ProcessDialogAsync(context, task, optionID, scriptObject);
                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для создания контекста работы с заданиями.
        /// Контекст нужен для сохранения измененных действием заданий и записью их в <see cref="IWorkflowEngineContext.Tasks"/>.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <returns>Возвращает контекст работы с заданиями.</returns>
        protected IDisposable CreateTasksContext(IWorkflowEngineContext context) => new TasksContext(context);

        /// <summary>
        /// Метод для установки задания в список обрабатываемых заданий <see cref="IWorkflowEngineContext.Tasks"/> для последующих узлов и действий.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <param name="task">Задание, которое нужно указать как обрабатываемое.</param>
        protected void AddTaskToNextContextTasks(IWorkflowEngineContext context, CardTask task)
        {
            var tasksContext = context.Info.TryGet<TasksContext>(nameof(TasksContext));
            if (tasksContext is not null)
            {
                tasksContext.AddTask(task);
            }
        }

        /// <summary>
        /// Метод для получения текста с учетом плейсхолдеров.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <param name="text">Текст для обработки.</param>
        /// <param name="task">Задание для замены.</param>
        /// <returns>Строка текста с заменёнными плейсхолдерами.</returns>
        protected async Task<string> GetWithPlaceholdersAsync(
            IWorkflowEngineContext context,
            string text,
            CardTask task)
        {
            var mainCard = context.IsMainCardLoaded
                ? await context.GetMainCardAsync(context.CancellationToken)
                : null;

            var placeholderManager = context.Container.Resolve<IPlaceholderManager>();
            return await placeholderManager.ReplaceTextAsync(
                text,
                context.Session,
                context.Container,
                context.DbScope,
                null,
                mainCard,
                cardID: context.ProcessInstance.CardID,
                task: task,
                info: context.CreatePlaceholderInfo(task),
                withScripts: true,
                cancellationToken: context.CancellationToken);
        }

        /// <summary>
        /// Метод для отправки уведомления о запуске задания. Параметры считываются из секции с именем <paramref name="sectionName"/>.
        /// </summary>
        /// <param name="context">Контекст обработки процесса</param>
        /// <param name="scriptObject">Объект со скриптами объекта</param>
        /// <param name="task">Запускаемое задание</param>
        /// <param name="sectionName">Имя секции, откуда берутся настройки для уведомления.</param>
        /// <param name="methodDescriptor">Дескриптор метода. Если значение равно значению по умолчанию для типа, то используется имя метода <see cref="WorkflowActionMethods.TaskStartNotificationMethod"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>
        /// Настройки уведомления, расположенные в секции <paramref name="sectionName"/>, должны быть расположены в следующих полях:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Имя поля</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description>NotificationID</description>
        ///         <description>Идентификатор типа уведомления.</description>
        ///     </item>
        ///     <item>
        ///         <description>ExcludeDeputies</description>
        ///         <description>Не отправлять заместителям.</description>
        ///     </item>
        ///     <item>
        ///         <description>ExcludeSubscribers</description>
        ///         <description>Не отправлять подписчикам.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        protected async Task SendStartTaskNotificationAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            string sectionName,
            WorkflowActionMethodDescriptor methodDescriptor = default)
        {
            var notificationID = await context.GetAsync<Guid?>(sectionName, "Notification", "ID");
            var excludeDeputies = await context.GetAsync<bool?>(sectionName, "ExcludeDeputies") ?? false;
            var excludeSubscribers = await context.GetAsync<bool?>(sectionName, "ExcludeSubscribers") ?? false;

            await this.SendStartTaskNotificationAsync(
                context,
                scriptObject,
                task,
                notificationID,
                excludeDeputies,
                excludeSubscribers,
                methodDescriptor ?? WorkflowActionMethods.TaskStartNotificationMethod);
        }

        /// <summary>
        /// Метод для отправки уведомления о запуске задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса</param>
        /// <param name="scriptObject">Объект со скриптами объекта</param>
        /// <param name="task">Запускаемое задание</param>
        /// <param name="notificationID">Идентификатор типа уведомления.</param>
        /// <param name="excludeDeputies">Не отправлять заместителям.</param>
        /// <param name="excludeSubscribers">Не отправлять подписчикам.</param>
        /// <param name="methodDescriptor">Дескриптор метода.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task SendStartTaskNotificationAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            Guid? notificationID,
            bool excludeDeputies,
            bool excludeSubscribers,
            WorkflowActionMethodDescriptor methodDescriptor)
        {
            if (notificationID.HasValue)
            {
                var cardID = context.ProcessInstance.CardID;
                
                await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope, cancellationToken: context.CancellationToken);

                context.ValidationResult.Add(
                    await this.notificationManager.SendAsync(
                        notificationID.Value,
                        task.TaskAssignedRoles
                            .Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID)
                            .Select(x => x.RoleID)
                            .ToArray(),
                        new NotificationSendContext()
                        {
                            MainCardID = cardID,
                            ExcludeDeputies = excludeDeputies,
                            Info = context.CreatePlaceholderInfo(task, clone: true),
                            ModifyEmailActionAsync = (email, ct) =>
                            {
                                return scriptObject?.ExecuteActionAsync(
                                    methodDescriptor.MethodName,
                                    methodDescriptor,
                                    email,
                                    task) ?? Task.CompletedTask;
                            },
                            DisableSubscribers = excludeSubscribers,
                            GetCardFuncAsync = (validationResult, ct) =>
                                this.cardsScope.GetCardAsync(
                                    cardID,
                                    validationResult,
                                    ct),
                        },
                        context.CancellationToken));
            }
        }

        /// <summary>
        /// Асинхронно отправляет уведомление о завершении задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="info">Информация о уведомлении получаемая из строки содержащей параметры обработки варианта завершения.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task SendCompleteTaskNotificationAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            WorkflowTaskNotificationInfo info)
        {
            await this.SendCompleteTaskNotificationAsync(
                context,
                scriptObject,
                task,
                info,
                WorkflowActionMethods.TaskCompleteNotificationMethod.GetMethodName(info.Row),
                WorkflowActionMethods.TaskCompleteNotificationMethod);
        }

        /// <summary>
        /// Асинхронно отправляет уведомление о завершении задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="info">Информация по уведомлению получаемая из строки содержащей параметры обработки варианта завершения.</param>
        /// <param name="methodName">Имя метода с помощью которого выполняется модификация шаблона сообщения.</param>
        /// <param name="methodDescriptor">Дескриптор метода.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task SendCompleteTaskNotificationAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            WorkflowTaskNotificationInfoBase info,
            string methodName,
            WorkflowActionMethodDescriptor methodDescriptor)
        {
            if (info.NotificationID.HasValue
                && !info.Cancel)
            {
                var roles = await info.GetRolesAsync();
                if (info.SendToPerformer)
                {
                    if (task.UserID.HasValue)
                    {
                        roles.Add(task.UserID.Value);
                    }
                    else
                    {
                        await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope, cancellationToken: context.CancellationToken);
                        roles.AddRange(task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID).Select(x => x.RoleID));
                    }
                }

                if (info.SendToAuthor)
                {
                    await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope, cancellationToken: context.CancellationToken);
                    roles.AddRange(task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.AuthorID).Select(x => x.RoleID));
                }

                var cardID = context.ProcessInstance.CardID;

                context.ValidationResult.Add(
                    await this.notificationManager.SendAsync(
                        info.NotificationID.Value,
                        roles,
                        new NotificationSendContext()
                        {
                            MainCardID = cardID,
                            ExcludeDeputies = info.ExcludeDeputies,
                            Info = context.CreatePlaceholderInfo(task, clone: true),
                            ModifyEmailActionAsync = (email, ct) =>
                            {
                                return scriptObject?.ExecuteActionAsync(methodName, methodDescriptor, email, task) ?? Task.CompletedTask;
                            },
                            DisableSubscribers = info.ExcludeSubscribers,
                            GetCardFuncAsync = (validationResult, ct) =>
                                this.cardsScope.GetCardAsync(
                                    cardID,
                                    validationResult,
                                    ct),
                        },
                        context.CancellationToken));
            }
        }

        /// <summary>
        /// Асинхронно отправляет уведомление о завершении действия.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="info">Информация по отправляемому уведомлению о завершении действия.</param>
        /// <param name="methodName">Имя метода с помощью которого выполняется модификация шаблона сообщения.</param>
        /// <param name="methodDescriptor">Дескриптор метода.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task SendCompleteActionNotificationAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            WorkflowNotificationInfoBase info,
            string methodName,
            WorkflowActionMethodDescriptor methodDescriptor)
        {
            if (info.NotificationID.HasValue
                && !info.Cancel)
            {
                var roles = await info.GetRolesAsync();

                var cardID = context.ProcessInstance.CardID;

                context.ValidationResult.Add(
                    await this.notificationManager.SendAsync(
                        info.NotificationID.Value,
                        roles,
                        new NotificationSendContext()
                        {
                            MainCardID = cardID,
                            ExcludeDeputies = info.ExcludeDeputies,
                            Info = context.CreatePlaceholderInfoWithoutTask(clone: true),
                            ModifyEmailActionAsync = (email, ct) =>
                            {
                                return scriptObject?.ExecuteActionAsync(methodName, methodDescriptor, email) ?? Task.CompletedTask;
                            },
                            DisableSubscribers = info.ExcludeSubscribers,
                            GetCardFuncAsync = (validationResult, ct) =>
                                this.cardsScope.GetCardAsync(
                                    cardID,
                                    validationResult,
                                    ct),
                        },
                        context.CancellationToken));
            }
        }

        protected async Task CreateDialogsAsync(
            IWorkflowEngineContext context,
            CardTask task,
            IWorkflowEngineCompiled scriptObject)
        {
            var dialogRows = GetDialogsRows(context);
            var buttonsSettings = GetButtonsRows(context);

            // Обрабатываем построчно, с учётом Order. Если вариант завершения уже был ранее, то не обрабатываем второй диалог.
            List<Guid> processedOptions = null;
            foreach (var dialogRow in dialogRows)
            {
                var completionOptionSettings = await this.CreateCompletionOptionSettingsAsync(context, dialogRow, buttonsSettings);
                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                if (completionOptionSettings is null)
                {
                    continue;
                }

                processedOptions ??= new List<Guid>();
                if (processedOptions.Contains(completionOptionSettings.CompletionOptionID))
                {
                    continue;
                }
                else
                {
                    processedOptions.Add(completionOptionSettings.CompletionOptionID);
                }

                if (scriptObject is not null)
                {
                    using var dialogContext = new WorkflowDialogContext(
                        context,
                        (ct) => GetOrCreateDialogCardAsync(
                            this.cardsScope,
                            this.serverPermissionsProvider,
                            this.cardRepositoryDef,
                            context,
                            completionOptionSettings,
                            ct),
                        completionOptionSettings,
                        this.cardsScope,
                        this.cardFileManager);

                    await scriptObject.ExecuteActionAsync(
                        WorkflowActionMethods.TaskDialogInitScript.GetMethodName(dialogRow),
                        WorkflowActionMethods.TaskDialogInitScript,
                        dialogContext);
                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return;
                    }

                    await PrepareSettingsWithDialogContextAsync(
                        this.cardsScope,
                        this.CardRepository,
                        this.signatureProvider,
                        context,
                        completionOptionSettings,
                        dialogContext,
                        task.RowID);

                    CardTaskDialogHelper.SetCompletionOptionSettings(task, completionOptionSettings);
                }
            }
        }

        protected async Task ProcessDialogAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid optionID,
            IWorkflowEngineCompiled scriptObject)
        {
            var dialogID = task.RowID;
            var actionInfo = CardTaskDialogHelper.GetCardTaskDialogActionResult(task);
            var coInfo = CardTaskDialogHelper.GetCompletionOptionSettings(task, optionID);

            if (coInfo is null
                || actionInfo is null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(coInfo.DialogAlias)
                && coInfo.StoreMode == CardTaskDialogStoreMode.Card
                && coInfo.PersistentDialogCardID != Guid.Empty)
            {
                AddAliasedDialog(context, coInfo.DialogAlias, coInfo.PersistentDialogCardID);
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            using var dialogContext = scriptObject is not null
                ? new WorkflowDialogContext(
                    context,
                    (ct) => CardTaskDialogHelper.GetDialogCardAsync(
                        task,
                        optionID,
                        (cardID, ct) => this.cardsScope.GetCardAsync(cardID, context.ValidationResult, ct),
                        cancellationToken: ct),
                    coInfo,
                    this.cardsScope,
                    this.cardFileManager,
                    actionResult: actionInfo)
                : null;
            var dialogRowID = coInfo.Info.TryGet<Guid>("DialogRowID");
            var dialogRows = GetDialogsRows(context);
            Dictionary<string, object> dialogRow = null;

            if (dialogRowID == default
                || (dialogRow = dialogRows.FirstOrDefault(x => x.Get<Guid>("RowID") == dialogRowID)) is null)
            {
                // Строка обработки завершения диалога уже удалена, игнорируем обработку
                return;
            }

            if (scriptObject is not null)
            {
                await scriptObject.ExecuteActionAsync(
                    WorkflowActionMethods.TaskDialogSavingScript.GetMethodName(dialogRow),
                    WorkflowActionMethods.TaskDialogSavingScript,
                    dialogContext);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }
            }

            var pressedButtonName = actionInfo.PressedButtonName;
            if (!string.IsNullOrEmpty(pressedButtonName))
            {
                var allButtons = GetButtonsRows(context);
                var pressedButton = allButtons.FirstOrDefault(
                    x => x.TryGet<string>("Name") == pressedButtonName
                        && WorkflowEngineHelper.Get<Guid>(x, "TaskDialog", "RowID") == dialogRowID);
                if (pressedButton is not null)
                {
                    if (scriptObject is not null)
                    {
                        await scriptObject.ExecuteActionAsync(
                            WorkflowActionMethods.TaskDialogButtonScript.GetMethodName(pressedButton),
                            WorkflowActionMethods.TaskDialogButtonScript,
                            dialogContext);

                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }
                    }

                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return;
                    }

                    var buttonRowID = pressedButton.TryGet<Guid>("RowID");
                    var links = context.ActionTemplate.Hash.TryGet<IList>(ButtonLinksSectionName);
                    if (links is not null)
                    {
                        foreach (var linkID in links
                            .Cast<Dictionary<string, object>>()
                            .Where(x => WorkflowEngineHelper.Get<Guid>(x, "Button", "RowID") == buttonRowID)
                            .Select(x => WorkflowEngineHelper.Get<Guid>(x, "Link", "ID")))
                        {
                            context.Links[linkID] = WorkflowEngineSignal.CreateDefaultSignal(StorageHelper.Clone(context.Signal.Hash));
                        }
                    }
                }
            }

            if (actionInfo.CompleteDialog)
            {
                if (scriptObject is not null)
                {
                    await scriptObject.ExecuteActionAsync(
                        WorkflowActionMethods.TaskDialogActionScript.GetMethodName(dialogRow),
                        WorkflowActionMethods.TaskDialogActionScript,
                        dialogContext);

                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return;
                    }
                }
            }
            else
            {
                // Если диалог не завершается по нажатию кнопки, то оставляем диалог открытым.
                // Если тип хранения, settings, то сохраняем измененную карточку в задании
                context.ResponseInfo.SetKeepTaskDialog(!string.IsNullOrEmpty(pressedButtonName));
                if (coInfo.StoreMode == CardTaskDialogStoreMode.Settings
                    && dialogContext is not null
                    && dialogContext.CardLoaded)
                {
                    var dialogCard = await dialogContext.GetCardObjectAsync();
                    if (dialogCard is null)
                    {
                        return;
                    }

                    dialogCard.RemoveChanges();
                    coInfo.DialogCard = dialogCard;

                    var mainCard = await context.GetMainCardAsync(context.CancellationToken);
                    if (mainCard is null)
                    {
                        return;
                    }

                    var dialogTask = mainCard.Tasks.FirstOrDefault(x => x.RowID == dialogID);
                    if (dialogTask is not null)
                    {
                        CardTaskDialogHelper.SetCompletionOptionSettings(dialogTask, coInfo);
                        dialogTask.State = CardRowState.Modified;
                        dialogTask.Flags = dialogTask.Flags.SetFlag(CardTaskFlags.UpdateSettings | CardTaskFlags.HistoryItemCreated, true);
                    }
                }
            }
        }

        /// <summary>
        /// Завершает дочерние задания указанных типов.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="parentTaskRowID">Идентификатор родительского задания.</param>
        /// <param name="taskTypeIDs">Коллекция типов обрабатываемых заданий.</param>
        /// <param name="modifyActionAsync">Действие выполняемое перед завершением задания.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task CompleteSubtasksAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            Guid parentTaskRowID,
            ICollection<Guid> taskTypeIDs,
            Func<CardTask, Task> modifyActionAsync)
        {
            var mainCard = await context.GetMainCardAsync(context.CancellationToken);

            if (mainCard is null)
            {
                return;
            }

            foreach (var task in mainCard.Tasks.Where(i => i.ParentRowID == parentTaskRowID && taskTypeIDs.Contains(i.TypeID)))
            {
                task.Action = CardTaskAction.Complete;

                await modifyActionAsync(task);
                await this.CompleteTaskAsync(context, scriptObject, task.RowID, true, task.OptionID);
            }
        }

        /// <summary>
        /// Возвращает коллекцию содержащую список SQL исполнителей.
        /// </summary>
        /// <param name="sqlPerformerScript">SQL скрипт возвращающий список SQL исполнителей представленный в виде двух столбцов: идентификатор роли, имя роли.</param>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <returns>Коллекция содержащая список SQL исполнителей.</returns>
        protected async Task<IReadOnlyCollection<RoleEntryStorage>> GetSqlPerformers(
            string sqlPerformerScript,
            IWorkflowEngineContext context)
        {
            if (string.IsNullOrWhiteSpace(sqlPerformerScript))
            {
                return EmptyHolder<RoleEntryStorage>.Collection;
            }

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                db.SetCommand(sqlPerformerScript,
                    db.Parameter("CardID", context.ProcessInstance.CardID),
                    db.Parameter("UserID", context.Session.User.ID))
                    .LogCommand();

                await using (var reader = await db.ExecuteReaderAsync(context.CancellationToken))
                {
                    if (!await reader.ReadAsync(context.CancellationToken))
                    {
                        return EmptyHolder<RoleEntryStorage>.Collection;
                    }

                    if (reader.FieldCount != 2
                        || reader[0] is not Guid firstRoleID
                        || reader[1] is not string firstRoleName)
                    {
                        ValidationSequence
                            .Begin(context.ValidationResult)
                            .SetObjectName(this)
                            .ErrorDetails("$KrActions_IncorrectCalculatedPerformersQuery_IncorrectSqlResultSet", sqlPerformerScript)
                            .End();
                        return EmptyHolder<RoleEntryStorage>.Collection;
                    }

                    var result = new List<RoleEntryStorage>
                    {
                        new RoleEntryStorage(default, firstRoleID, firstRoleName)
                    };

                    while (await reader.ReadAsync(context.CancellationToken))
                    {
                        result.Add(new RoleEntryStorage(default, reader.GetGuid(0), reader.GetNullableString(1)));
                    }

                    // Проверка есть ли ещё запросы, в т.ч. содержащие ошибки.
                    if (await reader.NextResultAsync(context.CancellationToken))
                    {
                        ValidationSequence
                            .Begin(context.ValidationResult)
                            .SetObjectName(this)
                            .ErrorDetails("$KrActions_IncorrectCalculatedPerformersQuery_SeveralQueries", sqlPerformerScript)
                            .End();
                        return EmptyHolder<RoleEntryStorage>.Collection;
                    }

                    return result;
                }
            }
        }

        /// <summary>
        /// Создаёт дайджест задания на основе дайджеста указанного в настройках действия, комментария инициатора процесса согласования и дополнительного комментария.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="baseDigest">Дайджест указанный в настройках действия.</param>
        /// <param name="additionalComment">Дополнительный комментарий.</param>
        /// <returns>Дайджест.</returns>
        /// <remarks>Для разделения частей дайджеста используется строка локализации "KrActions_TaskDigestPartsSeparator".</remarks>
        protected async Task<string> CreateDigestAsync(
            IWorkflowEngineContext context,
            string baseDigest,
            string additionalComment = default)
        {
            const int capacityDefault = 64;

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return default;
            }

            var authorComment = sCard.Sections[KrApprovalCommonInfo.Name].Fields.TryGet<string>(KrApprovalCommonInfo.AuthorComment);

            var isNullOrEmptyAuthorComment = string.IsNullOrEmpty(authorComment);
            var isNullOrEmptyAdditionalComment = string.IsNullOrEmpty(additionalComment);

            baseDigest = await this.GetWithPlaceholdersAsync(
                context,
                baseDigest,
                context.Task);

            if (isNullOrEmptyAuthorComment
                && isNullOrEmptyAdditionalComment)
            {
                return baseDigest;
            }

            var sb = StringBuilderHelper.Acquire(capacityDefault);
            sb.Append(baseDigest);

            if (!isNullOrEmptyAuthorComment)
            {
                AppendDigestPartsSeparator(sb)
                .Append(authorComment);
            }

            if (!isNullOrEmptyAdditionalComment)
            {
                AppendDigestPartsSeparator(sb)
                .Append(additionalComment);
            }

            return sb.ToStringAndRelease();

            static StringBuilder AppendDigestPartsSeparator(StringBuilder sb)
            {
                if (sb.Length > 0)
                {
                    sb
                        .AppendLine()
                        .AppendLine("{$KrActions_TaskDigestPartsSeparator}");
                }

                return sb;
            }
        }

        /// <summary>
        /// Делегирует задание другому пользователю.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="task">Делегируемое задание.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task DelegateTaskAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask task,
            string digest,
            Guid? taskKindID = default,
            string taskKindCaption = default)
        {
            const int capacityDefault = 128;

            await context.TryRemoveActiveTaskAsync(task.RowID);

            var fields = task.Card.Sections[KrTask.Name].Fields;
            var result = StringBuilderHelper.Acquire(capacityDefault)
                .Append("{$ApprovalHistory_TaskIsDelegated} \"")
                .Append(fields.Get<string>(KrTask.DelegateName))
                .Append('"');
            var comment = fields.TryGet<string>(KrTask.Comment);

            if (string.IsNullOrWhiteSpace(comment))
            {
                comment = default;
            }
            else
            {
                result
                    .Append(". {$ApprovalHistory_Comment}: ")
                    .Append(comment);
            }

            task.Result = result.ToStringAndRelease();

            digest = await this.CreateDigestAsync(context, digest, comment);

            var delegatedTask = await context.SendTaskAsync(
                task.TypeID,
                digest,
                task.Planned,
                null,
                null,
                fields.Get<Guid>(KrTask.DelegateID),
                fields.Get<string>(KrTask.DelegateName),
                parentRowID: task.RowID,
                cancellationToken: context.CancellationToken);

            if (delegatedTask is null
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }
            
            await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope, cancellationToken: context.CancellationToken);

            foreach (var author in task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.AuthorID))
            {
                var performer = delegatedTask.AddRole(
                    author.RoleID,
                    author.RoleName,
                    CardFunctionRoles.AuthorID,
                    showInTaskDetails: true);
                performer.Position = author.Position;
            }

            delegatedTask.Info[CardHelper.TaskKindIDKey] = taskKindID;
            delegatedTask.Info[CardHelper.TaskKindCaptionKey] = taskKindCaption;

            if (task.Card.Sections.TryGetValue(KrAdditionalApprovalInfo.Name, out var oldSection))
            {
                var additionalApprovalInfoSection = new CardSection(KrAdditionalApprovalInfo.Name, oldSection.GetStorage())
                {
                    Type = CardSectionType.Table
                };

                foreach (var row in additionalApprovalInfoSection.Rows)
                {
                    row.Fields[KrAdditionalApprovalInfo.ID] = delegatedTask.RowID;
                    row.State = CardRowState.Inserted;
                }

                delegatedTask.Card.Sections[KrAdditionalApprovalInfo.Name].Set(additionalApprovalInfoSection);
            }

            this.AddTaskToNextContextTasks(context, delegatedTask);
            await context.AddActiveTaskAsync(delegatedTask.RowID);

            await this.DelegateTaskCoreAsync(
                context,
                scriptObject,
                task,
                delegatedTask);
        }

        /// <summary>
        /// Обрабатывает завершение заданий типа <see cref="DefaultTaskTypes.KrRequestCommentTypeID"/>.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="taskOptionID">Идентификатор варианта завершения с которым завершается задание. Может отличаться от <see cref="CardTask.OptionID"/> из <paramref name="task"/>, если он был изменён в скрипте, обрабатывающим завершение задания.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task RequestCommentTaskCompleteAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid taskOptionID)
        {
            if (task.TypeID == DefaultTaskTypes.KrRequestCommentTypeID)
            {
                if (taskOptionID == DefaultCompletionOptions.AddComment)
                {
                    task.Result = task.Card.Sections[KrRequestComment.Name].Fields.TryGet<string>(KrRequestComment.Comment);
                    await context.TryRemoveActiveTaskAsync(task.RowID);
                }
                else if (taskOptionID == DefaultCompletionOptions.Cancel)
                {
                    await context.TryRemoveActiveTaskAsync(task.RowID);
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Асинхронно создаёт задание запроса комментария (<see cref="DefaultTaskTypes.KrRequestCommentTypeID"/>).
        /// При создании задания используются данные родительского задания, в том числе значения из секции <see cref="KrCommentators.Name"/>.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="parentTask">Родительское задание.</param>
        /// <returns>Созданное задание или значение <see langword="null"/>, если произошла ошибка.</returns>
        public async Task<CardTask> SendRequestCommentTaskAsync(
            IWorkflowEngineContext context,
            CardTask parentTask)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(parentTask, nameof(parentTask));

            var roleUsers = GetRoles(
                parentTask.Card.Sections[KrCommentators.Name].Rows);

            if (roleUsers.Count == 0)
            {
                context.ValidationResult.AddError(this, "$KrMessages_NeedToSpecifyRespondent");
                return null;
            }

            var comment = parentTask.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID
                ? parentTask.Card.Sections
                    .TryGet(KrAdditionalApprovalTaskInfo.Name)
                    ?.TryGetRawFields()
                    ?.TryGet<string>(KrAdditionalApprovalTaskInfo.Comment)
                : parentTask.Card.Sections
                    .TryGet(KrTask.Name)
                    ?.TryGetRawFields()
                    ?.TryGet<string>(KrTask.Comment);

            var task = await this.SendRequestCommentTaskAsync(
                context,
                parentTask.RowID,
                null,
                null,
                parentTask.Planned,
                parentTask.PlannedQuants,
                parentTask.UserID ?? context.Session.User.ID,
                parentTask.UserName ?? context.Session.User.Name,
                comment);

            if (task is null)
            {
                return null;
            }

            for (var i = 0; i < roleUsers.Count; i++)
            {
                var roleUser = roleUsers[i];
                task.AddPerformer(
                    roleUser.ID,
                    roleUser.Name,
                    i == 0);
            }

            return task;

            static IList<RoleUser> GetRoles(
                IEnumerable<CardRow> commentatorRows)
            {
                var commentators = commentatorRows
                    .Select(static c =>
                        c.TryGetValue(KrCommentators.CommentatorID, out var id)
                        && c.TryGetValue(KrCommentators.CommentatorName, out var name)
                        ? new RoleUser((Guid) id, (string) name)
                        : (RoleUser?) null)
                    .Where(static c => c.HasValue)
                    .Select(static c => c.Value)
                    .Distinct(RoleUserIDComparer<RoleUser>.Instance)
                    .ToArray();

                return commentators;
            }
        }

        /// <summary>
        /// Асинхронно создаёт задание запроса комментария (<see cref="DefaultTaskTypes.KrRequestCommentTypeID"/>).
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="parentTaskRowID">Идентификатор родительского задания.</param>
        /// <param name="roleID">Идентификатор роли на которую должно быть отправлено задание или значение <see langword="null"/>, если он будет задан позже.</param>
        /// <param name="roleName">Имя роли на которую должно быть отправлено задание.</param>
        /// <param name="planned">Срок завершения. Имеет приоритет перед <paramref name="plannedQuants"/> при определении даты завершения задания.</param>
        /// <param name="plannedQuants">
        /// Количество квантов календаря от времени на момент загрузки задания до времени его запланированного завершения <see cref="CardTask.Planned"/>
        /// или <see langword="null"/>, если должно использоваться значение <paramref name="planned"/>.
        /// </param>
        /// <param name="authorID">Идентификатор роли автора.</param>
        /// <param name="authorName">Имя роли автора.</param>
        /// <param name="digest">Дайджест задания.</param>
        /// <returns>Созданное задание или значение <see langword="null"/>, если произошла ошибка.</returns>
        public async Task<CardTask> SendRequestCommentTaskAsync(
            IWorkflowEngineContext context,
            Guid parentTaskRowID,
            Guid? roleID,
            string roleName,
            DateTime? planned,
            int? plannedQuants,
            Guid authorID,
            string authorName,
            string digest = default)
        {
            var taskHistoryItemRowID = Guid.NewGuid();
            await this.AddTaskHistoryAsync(
                context,
                DefaultTaskTypes.KrInfoRequestCommentTypeID,
                DefaultTaskTypes.KrInfoRequestCommentTypeName,
                "$CardTypes_TypesNames_KrInfoRequestComment",
                DefaultCompletionOptions.RequestComments,
                digest,
                modifyAction: (item) =>
                {
                    item.RowID = taskHistoryItemRowID;
                    item.ParentRowID = parentTaskRowID;
                });

            var answerTask = await context.SendTaskAsync(
                DefaultTaskTypes.KrRequestCommentTypeID,
                digest,
                planned,
                plannedQuants,
                null,
                roleID,
                roleName,
                parentRowID: parentTaskRowID,
                cancellationToken: context.CancellationToken);

            if (answerTask is null
                || !context.ValidationResult.IsSuccessful())
            {
                return null;
            }

            answerTask.HistoryItemParentRowID = taskHistoryItemRowID;

            var sections = answerTask.Card.Sections;
            sections[TaskCommonInfo.Name].Fields[TaskCommonInfo.Info] = answerTask.Digest;

            var requestCommentFields = sections[KrRequestComment.Name].Fields;
            requestCommentFields[KrRequestComment.AuthorRoleID] = authorID;
            requestCommentFields[KrRequestComment.AuthorRoleName] = authorName;
            answerTask.AddRole(
                authorID,
                authorName,
                CardFunctionRoles.AuthorID,
                showInTaskDetails: true);

            this.AddTaskToNextContextTasks(context, answerTask);
            await context.AddActiveTaskAsync(answerTask.RowID);

            return answerTask;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Создает внешний (вызываемый) метод для события
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="innerMethods"></param>
        /// <param name="methodName"></param>
        private void CreateGlobalEventMethod(
            IWorkflowCompilationSyntaxTreeBuilder builder,
            List<string> innerMethods,
            string methodName)
        {
            if (innerMethods.Count > 0)
            {
                builder.AddMethod(
                    "void",
                    methodName,
                    TaskParams,
                    "await " + string.Join("(task, taskCard, taskCardTables); await ", innerMethods) + "(task, taskCard, taskCardTables);");
            }
        }

        private static IEnumerable<Dictionary<string, object>> GetDialogsRows(IWorkflowEngineContext context)
        {
            return
                context.ActionTemplate.Hash.TryGet<IList>(DialogsSectionName)?.Cast<Dictionary<string, object>>()
                ?? Array.Empty<Dictionary<string, object>>();
        }

        private async ValueTask<CardTaskCompletionOptionSettings> CreateCompletionOptionSettingsAsync(
            IWorkflowEngineContext context,
            Dictionary<string, object> dialogRow,
            IEnumerable<Dictionary<string, object>> buttonsSettings)
        {
            var ctcBuilder = this.ctcBuilderFactory.Invoke();
            var dialogRowID = dialogRow.Get<Guid>("RowID");

            if (buttonsSettings is not null)
            {
                foreach (var buttonStorage in buttonsSettings.Where(x => WorkflowEngineHelper.Get<Guid>(x, "TaskDialog", "RowID") == dialogRowID))
                {
                    var button = new CardTaskDialogButtonInfo
                    {
                        Name = buttonStorage.TryGet<string>("Name"),
                        CardButtonType = (CardButtonType?) WorkflowEngineHelper.Get<int?>(buttonStorage, "Type", "ID") ?? CardButtonType.ToolbarButton,
                        Caption = buttonStorage.TryGet<string>("Caption"),
                        Icon = buttonStorage.TryGet<string>("Icon"),
                        Cancel = buttonStorage.TryGet<bool?>("Cancel") ?? false,
                        CompleteDialog = !(buttonStorage.TryGet<bool?>("NotEnd") ?? false),
                        Order = buttonStorage.TryGet<int>("Order"),
                    };
                    ctcBuilder.AddButton(button);
                }
            }

            var storeModeInt = WorkflowEngineHelper.Get<int?>(dialogRow, "CardStoreMode", "ID");
            if (storeModeInt.HasValue)
            {
                ctcBuilder.SetStoreMode((CardTaskDialogStoreMode) storeModeInt);
            }

            var completionOptionID = WorkflowEngineHelper.Get<Guid?>(dialogRow, "CompletionOption", "ID");
            if (completionOptionID.HasValue)
            {
                ctcBuilder.SetCompletionOption(completionOptionID.Value);
            }
            else
            {
                // Если не задан вариант завершения, то игнорируем такую настройку диалога
                return null;
            }

            var templateID = WorkflowEngineHelper.Get<Guid?>(dialogRow, "Template", "ID");
            if (templateID.HasValue)
            {
                ctcBuilder
                    .SetDialogType(templateID.Value)
                    .SetCardNewMethod(CardTaskDialogNewMethod.Template);
            }
            else
            {
                var dialogTypeID = WorkflowEngineHelper.Get<Guid?>(dialogRow, "DialogType", "ID");
                if (dialogTypeID.HasValue)
                {
                    ctcBuilder
                        .SetDialogType(dialogTypeID.Value)
                        .SetCardNewMethod(CardTaskDialogNewMethod.Default);
                }
                else
                {
                    context.ValidationResult.AddError(
                        this,
                        "$WorkflowEngine_Actions_Dialog_DialogTypeIsRequired",
                        context.NodeTemplate.GetObjectName(true),
                        context.ActionTemplate.GetObjectName(true),
                        WorkflowEngineHelper.Get<string>(dialogRow, "CompletionOption", "Caption"));
                    return null;
                }
            }

            ctcBuilder
                .SetOpenMode(CardTaskDialogOpenMode.Never)
                .SetDialogCaption(WorkflowEngineHelper.Get<string>(dialogRow, "DisplayValue"))
                .SetDialogName(WorkflowEngineHelper.Get<string>(dialogRow, "DialogName"))
                .SetDialogAlias(WorkflowEngineHelper.Get<string>(dialogRow, "DialogAlias"))
                .SetKeepFiles(await context.GetAsync<bool?>(MainSection, "KeepFiles") ?? false);

            var coSettings = await ctcBuilder.BuildAsync(context.ValidationResult, context.CancellationToken);
            if (coSettings.StoreMode == CardTaskDialogStoreMode.Card
                && !string.IsNullOrEmpty(coSettings.DialogAlias))
            {
                coSettings.PersistentDialogCardID = GetAliasedDialog(context, coSettings.DialogAlias);
            }
            // Записываем идентификатор строки диалога, по которой он был сформирован
            coSettings.Info["DialogRowID"] = dialogRowID;

            return coSettings;
        }

        #endregion

        #region Abstract Methods And Properties

        protected abstract Task<string> GetResultAsync(
            IWorkflowEngineContext context,
            CardTask task);

        protected abstract Task CompleteTaskCoreAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid optionID,
            IWorkflowEngineCompiled scriptObject);

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Удаляет указанное задание.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="taskRowID">Идентификатор удаляемого задания.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>В реализации по умолчанию удаляет информацию о задании из истории заданий.</remarks>
        protected virtual async Task DeleteTaskCoreAsync(
            IWorkflowEngineContext context,
            Guid taskRowID)
        {
            // Удаляем запись из TaskHistory, если она уже есть
            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;

                // запрос на удаление записи из истории заданий
                await db.SetCommand(
                    context.DbScope.BuilderFactory
                        .DeleteFrom("TaskHistory")
                        .Where().C("RowID").Equals().P("RowID")
                            .And().NotExists(b => b
                                .Select().Top(1).V(1).From("TaskHistory").NoLock()
                                .Where().C("ParentRowID").Equals().P("RowID")
                                .Limit(1))
                        .Build(),
                    db.Parameter("RowID", taskRowID))
                    .LogCommand()
                    .ExecuteNonQueryAsync(context.CancellationToken);
            }
        }

        /// <summary>
        /// Делегирует задание другому пользователю.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <param name="originalTask">Делегируемое задание.</param>
        /// <param name="delegatedTask">Делегированное задание.</param>
        /// <returns>Асинхронная задача.</returns>
        protected virtual Task DelegateTaskCoreAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject,
            CardTask originalTask,
            CardTask delegatedTask) => Task.CompletedTask;

        #endregion
    }
}
