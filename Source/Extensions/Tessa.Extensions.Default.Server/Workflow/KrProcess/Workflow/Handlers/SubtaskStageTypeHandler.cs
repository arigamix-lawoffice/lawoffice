#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using NotificationHelper = Tessa.Extensions.Default.Shared.Notices.NotificationHelper;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Представляет абстрактный обработчик этапа поддерживающий дочерние задания.
    /// </summary>
    public abstract class SubtaskStageTypeHandler : StageTypeHandlerBase
    {
        #region Fields

        private static readonly Guid[] subTaskTypeIDs = { DefaultTaskTypes.KrRequestCommentTypeID };

        private readonly ICardCache cardCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SubtaskStageTypeHandler"/>.
        /// </summary>
        /// <param name="roleGetStrategy"><inheritdoc cref="IRoleGetStrategy" path="/summary"/></param>
        /// <param name="contextRoleManager"><inheritdoc cref="IContextRoleManager" path="/summary"/></param>
        /// <param name="cardMetadata"><inheritdoc cref="ICardMetadata" path="/summary"/></param>
        /// <param name="cardGetStrategy"><inheritdoc cref="ICardGetStrategy" path="/summary"/></param>
        /// <param name="krScope"><inheritdoc cref="IKrScope" path="/summary"/></param>
        /// <param name="calendarService"><inheritdoc cref="IBusinessCalendarService" path="/summary"/></param>
        /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
        /// <param name="tasksRevoker"><inheritdoc cref="IStageTasksRevoker" path="/summary"/></param>
        /// <param name="notificationManager"><inheritdoc cref="INotificationManager" path="/summary"/></param>
        protected SubtaskStageTypeHandler(
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            IBusinessCalendarService calendarService,
            ICardMetadata cardMetadata,
            ICardGetStrategy cardGetStrategy,
            IKrScope krScope,
            IStageTasksRevoker tasksRevoker,
            INotificationManager notificationManager,
            ICardCache cardCache,
            ISession session)
        {
            this.RoleGetStrategy = NotNullOrThrow(roleGetStrategy);
            this.ContextRoleManager = NotNullOrThrow(contextRoleManager);
            this.CalendarService = NotNullOrThrow(calendarService);
            this.CardMetadata = NotNullOrThrow(cardMetadata);
            this.CardGetStrategy = NotNullOrThrow(cardGetStrategy);
            this.KrScope = NotNullOrThrow(krScope);
            this.TasksRevoker = NotNullOrThrow(tasksRevoker);
            this.NotificationManager = NotNullOrThrow(notificationManager);
            this.cardCache = NotNullOrThrow(cardCache);
            this.Session = NotNullOrThrow(session);
        }

        #endregion

        #region Protected Properties and Constants

        protected const string SubtaskCount = nameof(SubtaskCount);
        protected const string ResultAction = nameof(ResultAction);
        protected const string ResultTransitTo = nameof(ResultTransitTo);
        protected const string ResultKeepStates = nameof(ResultKeepStates);
        protected const string Interjected = nameof(Interjected);

        protected IRoleGetStrategy RoleGetStrategy { get; set; }
        protected IContextRoleManager ContextRoleManager { get; set; }
        protected IBusinessCalendarService CalendarService { get; set; }
        protected ICardMetadata CardMetadata { get; set; }
        protected ICardGetStrategy CardGetStrategy { get; set; }
        protected IKrScope KrScope { get; set; }

        protected IStageTasksRevoker TasksRevoker { get; }
        protected INotificationManager NotificationManager { get; }
        protected ISession Session { get; set; }

        /// <summary>
        /// Возвращает название поля содержащего комментарий к заданию.
        /// </summary>
        protected abstract string CommentNameField { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Возвращает массив идентификаторов типов дочерних заданий которые должны быть завершены.
        /// </summary>
        protected virtual Guid[] GetSubTaskTypesToRevoke() => subTaskTypeIDs;

        /// <summary>
        /// Возвращает стандартный дайджест задания созданный по информации из контекста обработчика этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="additionalComment">Дополнительный комментарий.</param>
        /// <returns>Дайджест задания.</returns>
        protected virtual string GetTaskDigest(IStageTypeHandlerContext context, string? additionalComment = null)
        {
            var builder = new StringBuilder()
                .Append("{$KrMessages_Stage}: ")
                .Append(LocalizationManager.EscapeIfLocalizationString(context.Stage.Name));

            var stageComment = context.Stage.SettingsStorage.TryGet<string>(this.CommentNameField);
            if (!string.IsNullOrEmpty(stageComment))
            {
                builder.Append(". ").Append(stageComment);
            }

            var authorComment = context.WorkflowProcess.AuthorComment;
            if (!string.IsNullOrEmpty(authorComment))
            {
                builder
                    .AppendLine()
                    .AppendLine("---------------")
                    .Append(authorComment);
            }

            if (!string.IsNullOrEmpty(additionalComment))
            {
                builder
                    .AppendLine()
                    .AppendLine("---------------")
                    .Append(additionalComment);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Обрабатывает делегирование задания.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="delegatedTask">Делегируемое задание.</param>
        /// <returns>Асинхронная задача.</returns>
        protected virtual Task HandleTaskDelegateAsync(IStageTypeHandlerContext context, CardTask delegatedTask) =>
            Task.CompletedTask;

        /// <summary>
        /// Обрабатывает отмену этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="taskTypeIDs">Массив идентификаторов типов завершаемых заданий.</param>
        /// <param name="revoke">Действие, выполняемое при завершении задания.</param>
        /// <returns>Значение <see langword="true"/>, если не было завершено заданий, иначе - <see langword="false"/>.</returns>
        protected virtual async Task<bool> HandleStageInterruptAsync(
            IStageTypeHandlerContext context,
            Guid[] taskTypeIDs,
            Action<CardTask> revoke)
        {
            context.Stage.InfoStorage.Remove(SubtaskCount);
            return await this.RevokeTasksAsync(context, taskTypeIDs, revoke, removeFromActive: true) == 0;
        }

        /// <summary>
        /// Отправляет задание указанного типа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="typeID">Идентификатор типа задания.</param>
        /// <param name="digest">Дайджест задания.</param>
        /// <param name="performer">Исполнитель задания или значение <see langword="null"/>, если он будет задан в <paramref name="modifyTask"/>.</param>
        /// <param name="modifyTask">Метод модифицирующий задание.</param>
        /// <param name="createHistory">Значение <see langword="true"/>, если в историю процесса должна быть добавлена информация о задании, иначе - <see langword="false"/>.</param>
        /// <returns>Созданное задание или значение по умолчанию для типа, если при создании задания произошла ошибка.</returns>
        protected Task<CardTask?> SendTaskAsync(
            IStageTypeHandlerContext context,
            Guid typeID,
            string digest,
            Performer? performer,
            Func<CardTask, CancellationToken, ValueTask>? modifyTask = null,
            bool createHistory = true) =>
            this.SendTaskAsync(
                context,
                typeID,
                digest,
                performer?.PerformerID,
                performer?.PerformerName,
                modifyTask,
                createHistory);

        /// <summary>
        /// Отправляет задание указанного типа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="typeID">Идентификатор типа задания.</param>
        /// <param name="digest">Дайджест задания.</param>
        /// <param name="performerID">Идентификатор роли, на которую отправляется задание или значение <see langword="null"/>, если он будет задан в <paramref name="modifyTask"/>.</param>
        /// <param name="performerName">Имя роли, на которую отправляется задание.</param>
        /// <param name="modifyTask">Метод модифицирующий задание.</param>
        /// <param name="createHistory">Значение <see langword="true"/>, если в историю процесса должна быть добавлена информация о задании, иначе - <see langword="false"/>.</param>
        /// <returns>Созданное задание или значение по умолчанию для типа, если при создании задания произошла ошибка.</returns>
        protected virtual async Task<CardTask?> SendTaskAsync(
            IStageTypeHandlerContext context,
            Guid typeID,
            string? digest,
            Guid? performerID,
            string? performerName,
            Func<CardTask, CancellationToken, ValueTask>? modifyTask = null,
            bool createHistory = true)
        {
            var taskInfo = await context.WorkflowAPI.SendTaskAsync(
                typeID,
                digest,
                performerID,
                performerName,
                modifyTaskAction: modifyTask,
                cancellationToken: context.CancellationToken);

            if (taskInfo is null)
            {
                return null;
            }

            var task = taskInfo.Task;

            if (createHistory)
            {
                var advisory = context.Stage.SettingsStorage.TryGet<bool?>(KrApprovalSettingsVirtual.Advisory) ?? false;
                task.Flags |= CardTaskFlags.CreateHistoryItem;
                context.ContextualSatellite.AddToHistory(
                    task.RowID,
                    context.WorkflowProcess.InfoStorage.TryGet(Keys.Cycle, 1),
                    advisory);
            }

            await context.WorkflowAPI.AddActiveTaskAsync(task.RowID, context.CancellationToken);

            if (context.CardExtensionContext is ICardStoreExtensionContext storeContext)
            {
                await CardComponentHelper.FillTaskAssignedRolesAsync(task, storeContext.DbScope!, cancellationToken: context.CancellationToken);
            }

            context.ValidationResult.Add(
                await this.NotificationManager.SendAsync(
                    DefaultNotifications.TaskNotification,
                    task.TaskAssignedRoles
                        .Where(static x => x.TaskRoleID == CardFunctionRoles.PerformerID)
                        .Select(static x => x.RoleID)
                        .ToArray(),
                    new NotificationSendContext()
                    {
                        MainCardID = context.MainCardID ?? Guid.Empty,
                        Info = NotificationHelper.GetInfoWithTask(task),
                        ModifyEmailActionAsync = async (email, ct) =>
                        {
                            NotificationHelper.ModifyEmailForMobileApprovers(
                                email,
                                task,
                                await NotificationHelper.GetMobileApprovalEmailAsync(this.cardCache, ct));

                            NotificationHelper.ModifyTaskCaption(
                                email,
                                task);
                        },
                        GetCardFuncAsync = (validationResult, ct) =>
                            context.MainCardAccessStrategy.GetCardAsync(
                                validationResult: validationResult,
                                cancellationToken: ct),
                    },
                    context.CancellationToken));

            return task;
        }

        /// <summary>
        /// Отправляет дочернее к указанному задание.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="parentTask">Родительское задание.</param>
        /// <param name="typeID">Идентификатор типа создаваемого задания.</param>
        /// <param name="digest">Дайджест задания.</param>
        /// <param name="performerID">Идентификатор роли, на которую отправляется задание или значение <see langword="null"/>, если он будет задан в <paramref name="modifyTask"/>.</param>
        /// <param name="performerName">Имя роли, на которую отправляется задание.</param>
        /// <param name="modifyTask">Метод модифицирующий задание.</param>
        /// <param name="createHistory">Значение <see langword="true"/>, если в историю процесса должна быть добавлена информация о задании, иначе - <see langword="false"/>.</param>
        /// <returns>Созданное задание или значение по умолчанию для типа, если при создании задания произошла ошибка.</returns>
        protected virtual async Task<CardTask?> SendSubTaskAsync(
            IStageTypeHandlerContext context,
            CardTask parentTask,
            Guid typeID,
            string? digest,
            Guid? performerID,
            string? performerName,
            Func<CardTask, CancellationToken, ValueTask>? modifyTask = null,
            bool createHistory = true)
        {
            var task = await this.SendTaskAsync(
                context,
                typeID,
                digest,
                performerID,
                performerName,
                modifyTask,
                createHistory);

            if (task is null)
            {
                return null;
            }

            task.ParentRowID = parentTask.RowID;

            var info = context.Stage.InfoStorage;
            var count = info.TryGet<int>(SubtaskCount) + 1;
            info[SubtaskCount] = Int32Boxes.Box(count);

            return task;
        }

        /// <summary>
        /// Завершает задания указанных типов.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа в котором выполняется завершение заданий.</param>
        /// <param name="taskTypeIDs">Массив идентификаторов типов завершаемых заданий.</param>
        /// <param name="revoke">Действие, выполняемое при завершении задания.</param>
        /// <param name="removeFromActive">Значение <see langword="true"/>, если необходимо удалить задание из списка активных, иначе - <see langword="false"/>.</param>
        /// <returns>Число завершённых заданий.</returns>
        protected virtual async Task<int> RevokeTasksAsync(
            IStageTypeHandlerContext context,
            Guid[] taskTypeIDs,
            Action<CardTask> revoke,
            bool removeFromActive = false)
        {
            var storeContext = (ICardStoreExtensionContext) context.CardExtensionContext;
            var scope = storeContext.DbScope!;
            await using (scope.Create())
            {
                var db = scope.Db;
                var query = scope.BuilderFactory
                    .Select().C("t", "RowID")
                    .From("Tasks", "t").NoLock()
                    .InnerJoin("WorkflowTasks", "wt").NoLock()
                        .On().C("wt", "RowID").Equals().C("t", "RowID")
                    .Where().C("t", "TypeID").Q(" IN (");

                var index = 0;
                var parameters = new DataParameter[taskTypeIDs.Length + 1]; // Число отзываемых типов заданий + ProcessID.
                while (index < taskTypeIDs.Length)
                {
                    var parameterName = $"TypeID{index}";
                    var parameter = db.Parameter(parameterName, taskTypeIDs[index]);
                    query.Parameter(parameterName);
                    parameters[index++] = parameter;
                }

                parameters[index] = db.Parameter("ProcessID", context.ProcessInfo.ProcessID);

                var tasksToRevoke = await db
                    .SetCommand(
                        query
                            .Q(")")
                            .And().C("wt", "ProcessRowID").Equals().P("ProcessID")
                            .Build(),
                        parameters)
                    .LogCommand()
                    .ExecuteListAsync<Guid>(context.CancellationToken);
                return await this.RevokeTasksCoreAsync(context, tasksToRevoke, revoke, removeFromActive);
            }
        }

        /// <summary>
        /// Завершает дочерние задания указанных типов.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа в котором выполняется завершение заданий.</param>
        /// <param name="parentTask">Родительское задание.</param>
        /// <param name="taskTypeIDs">Массив типов завершаемых заданий.</param>
        /// <param name="revoke">Действие, выполняемое при завершении задания.</param>
        /// <param name="removeFromActive">Значение <see langword="true"/>, если необходимо удалить задание из списка активных, иначе - <see langword="false"/>.</param>
        /// <returns>Число завершённых заданий.</returns>
        protected virtual async Task<int> RevokeSubTasksAsync(
            IStageTypeHandlerContext context,
            CardTask parentTask,
            Guid[] taskTypeIDs,
            Action<CardTask> revoke,
            bool removeFromActive = false)
        {
            var storeContext = (ICardStoreExtensionContext) context.CardExtensionContext;
            var scope = storeContext.DbScope!;
            await using (scope.Create())
            {
                var db = scope.Db;
                var query = scope.BuilderFactory
                    .Select().C("RowID")
                    .From("Tasks").NoLock()
                    .Where().C("TypeID").Q(" IN (");

                var index = 0;
                var parameters = new DataParameter[taskTypeIDs.Length + 1];
                while (index < taskTypeIDs.Length)
                {
                    var parameterName = $"TypeID{index}";
                    var parameter = db.Parameter(parameterName, taskTypeIDs[index]);
                    query.Parameter(parameterName);
                    parameters[index++] = parameter;
                }

                parameters[index] = db.Parameter("ParentApprovalID", parentTask.RowID);

                var tasksToRevoke = await db
                    .SetCommand(
                        query.Q(")")
                            .And().C("ParentID").Equals().P("ParentApprovalID")
                            .Build(),
                        parameters)
                    .LogCommand()
                    .ExecuteListAsync<Guid>(context.CancellationToken);
                return await this.RevokeTasksCoreAsync(context, tasksToRevoke, revoke, removeFromActive);
            }
        }

        /// <summary>
        /// Завершает дочерние задания типов возвращаемых <see cref="GetSubTaskTypesToRevoke"/>.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа в котором выполняется завершение заданий.</param>
        /// <param name="parentTask">Родительское задание.</param>
        /// <returns>Число завершённых заданий.</returns>
        protected virtual Task<int> RevokeSubTasksAsync(
            IStageTypeHandlerContext context,
            CardTask parentTask)
        {
            return this.RevokeSubTasksAsync(
                context,
                parentTask,
                this.GetSubTaskTypesToRevoke(),
                t =>
                {
                    t.OptionID = DefaultCompletionOptions.Cancel;
                    t.Result = "$ApprovalHistory_ParentTaskIsCompleted";
                });
        }

        /// <summary>
        /// Завершает задания имеющие идентификаторы из указанного списка.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа в котором выполняется завершение заданий.</param>
        /// <param name="tasksToRevoke">Список идентификаторов завершаемых заданий.</param>
        /// <param name="revoke">Действие, выполняемое при завершении задания.</param>
        /// <param name="removeFromActive">Значение <see langword="true"/>, если необходимо удалить задание из списка активных, иначе - <see langword="false"/>.</param>
        /// <returns>Число завершённых заданий.</returns>
        protected virtual async Task<int> RevokeTasksCoreAsync(
            IStageTypeHandlerContext context,
            List<Guid> tasksToRevoke,
            Action<CardTask> revoke,
            bool removeFromActive) =>
            await this.TasksRevoker.RevokeTasksAsync(new StageTaskRevokerContext(context, context.CancellationToken)
            {
                CardID = context.MainCardID ?? Guid.Empty,
                TaskIDs = tasksToRevoke,
                RemoveFromActive = removeFromActive,
                TaskModificationAction = task =>
                {
                    const string revokedByParent = CardHelper.SystemKeyPrefix + "revokedByParent";
                    task.Info[revokedByParent] = BooleanBoxes.True;
                    revoke(task);
                }
            });

        /// <summary>
        /// Обрабатывает завершение дочерних заданий.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат обработки.</returns>
        protected virtual StageHandlerResult SubTaskCompleted(IStageTypeHandlerContext context)
        {
            var info = context.Stage.InfoStorage;
            var count = info.Get<int>(SubtaskCount) - 1;
            info[SubtaskCount] = Int32Boxes.Box(count);

            if (count == 0 && info.TryGetValue(ResultAction, out var actionUntyped))
            {
                var action = (StageHandlerAction) (int) actionUntyped;
                var transitTo = info.TryGet<Guid?>(ResultTransitTo);
                var keepStates = info.TryGet<bool?>(ResultKeepStates);

                info.Remove(ResultAction);
                info.Remove(ResultTransitTo);
                info.Remove(ResultKeepStates);

                return new StageHandlerResult(action, transitTo, keepStates);
            }

            return StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Обрабатывает завершение этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="result">Результат с которым завершается этап.</param>
        /// <returns>Результат обработки.</returns>
        protected virtual StageHandlerResult StageCompleted(IStageTypeHandlerContext context, StageHandlerResult result)
        {
            var info = context.Stage.InfoStorage;
            if (info.TryGet<int>(SubtaskCount) == 0)
            {
                return result;
            }

            info.Add(ResultAction, (int) result.Action);

            var transitTo = result.TransitionID;
            if (transitTo.HasValue)
            {
                info.Add(ResultTransitTo, transitTo.Value);
            }

            var keepStates = result.KeepStageStates;
            if (keepStates.HasValue)
            {
                info.Add(ResultKeepStates, keepStates.Value);
            }

            return StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Возвращает номер текущего цикла согласования из <see cref="WorkflowProcess.InfoStorage"/>.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Номер цикла согласования.</returns>
        protected static int GetCycle(IStageTypeHandlerContext context) =>
            context.WorkflowProcess.InfoStorage.TryGet<int>(Keys.Cycle);

        /// <summary>
        /// Возвращает значение, показывающее,
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns></returns>
        protected static bool IsInterjected(
            IStageTypeHandlerContext context)
        {
            if (!context.Stage.InfoStorage.TryGetValue(Interjected, out var interjected))
            {
                return false;
            }

            switch (interjected)
            {
                // Легаси, когда хранился только флажок
                case bool interjectedBool:
                    return interjectedBool;
                // Хранится цикл интерджекта, чтобы не было пропуска этапа при вернулось->доработка->отзыв->запуск процесса
                case int interjectedInt:
                    var cycle = GetCycle(context);
                    return interjectedInt == cycle;
                case null:
                    return false;
                default:
                    throw new InvalidOperationException($"Invalid value of interjected key in approval stage {interjected}");
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task BeforeInitializationAsync(IStageTypeHandlerContext context)
        {
            await base.BeforeInitializationAsync(context);

            HandlerHelper.ClearCompletedTasks(context.Stage);
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleTaskCompletionAsync(IStageTypeHandlerContext context)
        {
            var task = context.TaskInfo.Task;
            var optionID = task.OptionID;
            if (optionID == DefaultCompletionOptions.RequestComments)
            {
                HandlerHelper.AppendToCompletedTasksWithPreparing(context.Stage, task);
                var roleUsers = GetRoles(
                    task.Card.Sections[KrCommentators.Name].Rows);

                if (roleUsers.Count == 0)
                {
                    context.ValidationResult.AddError(this, "$KrMessages_NeedToSpecifyRespondent");
                    return StageHandlerResult.EmptyResult;
                }

                var comment = task.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID
                    ? task.Card.Sections
                        .TryGet(KrAdditionalApprovalTaskInfo.Name)
                        ?.TryGetRawFields()
                        ?.TryGet<string>(KrAdditionalApprovalTaskInfo.Comment)
                    : task.Card.Sections
                        .TryGet(KrTask.Name)
                        ?.TryGetRawFields()
                        ?.TryGet<string>(KrTask.Comment);

                var storeContext = (ICardStoreExtensionContext) context.CardExtensionContext;
                var utcNow = DateTime.UtcNow;
                var user = storeContext.Session.User;
                var option = (await this.CardMetadata.GetEnumerationsAsync(context.CancellationToken)).CompletionOptions[DefaultCompletionOptions.RequestComments];
                var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);

                // Временная зона текущего сотрудника и календарь, для записи в историю заданий
                var userZoneInfo = await this.CalendarService.GetRoleTimeZoneInfoAsync(user.ID, context.CancellationToken);
                var userCalendarInfo = await this.CalendarService.GetRoleCalendarInfoAsync(user.ID, context.CancellationToken);
                if (userCalendarInfo is null)
                {
                    context.ValidationResult.AddError(this, await LocalizationManager.FormatAsync("$KrMessages_NoRoleCalendar", user.ID, context.CancellationToken));
                    return StageHandlerResult.EmptyResult;
                }
                var settings = new Dictionary<string, object?>(StringComparer.Ordinal)
                {
                    [TaskHistorySettingsKeys.PerformerID] = user.ID,
                    [TaskHistorySettingsKeys.PerformerName] = user.Name
                };

                var questionItem = new CardTaskHistoryItem
                {
                    State = CardTaskHistoryState.Inserted,
                    ParentRowID = task.RowID,
                    RowID = Guid.NewGuid(),
                    TypeID = DefaultTaskTypes.KrInfoRequestCommentTypeID,
                    TypeName = DefaultTaskTypes.KrInfoRequestCommentTypeName,
                    TypeCaption = "$CardTypes_TypesNames_KrInfoRequestComment",
                    Created = utcNow,
                    Planned = utcNow,
                    InProgress = utcNow,
                    Completed = utcNow,
                    CompletedByID = user.ID,
                    CompletedByName = user.Name,
                    CompletedByRole = user.Name,
                    UserID = user.ID,
                    UserName = user.Name,
                    AuthorID = user.ID,
                    AuthorName = user.Name,
                    OptionID = option.ID,
                    OptionName = option.Name,
                    OptionCaption = option.Caption,
                    Result = comment,
                    GroupRowID = groupID,
                    TimeZoneID = userZoneInfo.TimeZoneID,
                    TimeZoneUtcOffsetMinutes = (int?) userZoneInfo.TimeZoneUtcOffset.TotalMinutes,
                    CalendarID = userCalendarInfo.CalendarID,
                    Settings = settings,
                    AssignedOnRole = user.Name
                };

                var cycle = context.WorkflowProcess.InfoStorage.TryGet(Keys.Cycle, 1);

                var mainCard = await this.KrScope.GetMainCardAsync(
                    storeContext.Request.Card.ID,
                    cancellationToken: context.CancellationToken);

                if (mainCard is null)
                {
                    return StageHandlerResult.EmptyResult;
                }

                mainCard.TaskHistory.Add(questionItem);

                context.ContextualSatellite.AddToHistory(questionItem.RowID, cycle);

                var answerTask = await this.SendSubTaskAsync(
                    context,
                    task,
                    DefaultTaskTypes.KrRequestCommentTypeID,
                    comment,
                    null,
                    null,
                    modifyTask: (t, _) =>
                    {
                        for (var i = 0; i < roleUsers.Count; i++)
                        {
                            var user = roleUsers[i];
                            t.AddPerformer(user.ID, user.Name, i == 0);
                        }

                        t.AddAuthor(user.ID, user.Name);
                        t.ParentRowID = task.RowID;
                        t.HistoryItemParentRowID = questionItem.RowID;
                        t.GroupRowID = groupID;

                        return ValueTask.CompletedTask;
                    },
                    createHistory: true);

                if (answerTask is not null)
                {
                    var card = answerTask.Card;
                    card.Sections[TaskCommonInfo.Name].Fields[TaskCommonInfo.Info] = answerTask.Digest;
                    card.Sections[KrRequestComment.Name].Fields[KrRequestComment.AuthorRoleID] = task.UserID ?? user.ID;
                    card.Sections[KrRequestComment.Name].Fields[KrRequestComment.AuthorRoleName] = task.UserName ?? user.Name;
                }

                return StageHandlerResult.InProgressResult;
            }

            if (task.TypeID == DefaultTaskTypes.KrRequestCommentTypeID)
            {
                if (optionID == DefaultCompletionOptions.AddComment ||
                    optionID == DefaultCompletionOptions.Cancel)
                {
                    if (optionID == DefaultCompletionOptions.AddComment)
                    {
                        var comment = task.Card.Sections[KrRequestComment.Name].Fields.TryGet<string>(KrRequestComment.Comment);
                        await HandlerHelper.SetTaskResultAsync(context, task, comment);

                        if (context.CardExtensionContext is ICardStoreExtensionContext storeContext)
                        {
                            await CardComponentHelper.FillTaskAssignedRolesAsync(task, storeContext.DbScope!, cancellationToken: context.CancellationToken);
                        }
                        
                        context.ValidationResult.Add(
                            await this.NotificationManager
                                .SendAsync(
                                    DefaultNotifications.CommentNotification,
                                    task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.AuthorID).Select(x => x.RoleID).ToArray(),
                                    new NotificationSendContext()
                                    {
                                        MainCardID = context.MainCardID ?? Guid.Empty,
                                        GetCardFuncAsync = (validationResult, ct) =>
                                            context.MainCardAccessStrategy.GetCardAsync(
                                                validationResult: validationResult,
                                                cancellationToken: ct),
                                        Info = NotificationHelper.GetInfoWithTask(task),
                                    },
                                    context.CancellationToken));
                    }

                    // Таск завершается без дальнейшей обработки.
                    // Обновление родительского таска в расширении KrUpdateParentTaskExtension.
                    await context.WorkflowAPI.RemoveActiveTaskAsync(task.RowID, context.CancellationToken);
                    return this.SubTaskCompleted(context);
                }
            }

            if (optionID == DefaultCompletionOptions.Delegate)
            {
                HandlerHelper.AppendToCompletedTasksWithPreparing(context.Stage, task);
                var fields = task.Card.Sections[KrTask.Name].Fields;

                // Новый StringBuilder по той причине, что длина строк
                // может быть достаточно большой и StringBuilder не будет
                // возвращаться обратно в StringBuilderHelper.
                var result = new StringBuilder()
                    .Append("{$ApprovalHistory_TaskIsDelegated} \"")
                    .Append(fields.Get<string>(KrTask.DelegateName))
                    .Append('"');

                string digest;
                var comment = fields.TryGet<string>(KrTask.Comment);

                if (string.IsNullOrWhiteSpace(comment))
                {
                    digest = this.GetTaskDigest(context);
                }
                else
                {
                    digest = this.GetTaskDigest(context, comment);

                    result
                        .Append(". {$ApprovalHistory_Comment}: ")
                        .Append(comment);
                }

                await context.WorkflowAPI.TryRemoveActiveTaskAsync(task.RowID, context.CancellationToken);
                await HandlerHelper.SetTaskResultAsync(context, task, result.ToString());
                await this.RevokeSubTasksAsync(context, task);
                var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);
                var (kindID, kindCaption) = HandlerHelper.GetTaskKind(context);

                var author = await HandlerHelper.GetStageAuthorAsync(context, this.RoleGetStrategy, this.ContextRoleManager, this.Session);
                var authorID = author.AuthorID;
                await this.SendTaskAsync(
                    context,
                    task.TypeID,
                    digest,
                    fields.Get<Guid>(KrTask.DelegateID),
                    fields.Get<string>(KrTask.DelegateName),
                    async (t, ct) =>
                    {
                        t.AddAuthor(authorID);
                        t.Planned = task.Planned;
                        t.ParentRowID = task.RowID;
                        t.HistoryItemParentRowID = task.RowID;
                        t.GroupRowID = groupID;
                        HandlerHelper.SetTaskKind(t, kindID, kindCaption, context);

                        await this.HandleTaskDelegateAsync(context, t);
                    });

                return StageHandlerResult.InProgressResult;
            }

            return StageHandlerResult.EmptyResult;
        }

        /// <inheritdoc/>
        public override Task<bool> HandleStageInterruptAsync(IStageTypeHandlerContext context) => TaskBoxes.True;

        #endregion

        #region Private Methods

        private static IList<RoleUser> GetRoles(
            IEnumerable<CardRow> commentators)
        {
            var users = commentators
                .Select(static c =>
                    c.TryGetValue(KrCommentators.CommentatorID, out var id)
                    && c.TryGetValue(KrCommentators.CommentatorName, out var name)
                    ? new RoleUser((Guid) id!, (string) name!)
                    : (RoleUser?) null)
                .Where(static c => c.HasValue)
                .Select(static c => c!.Value)
                .Distinct(RoleUserIDComparer<RoleUser>.Instance)
                .ToArray();

            return users;
        }

        #endregion
    }
}
