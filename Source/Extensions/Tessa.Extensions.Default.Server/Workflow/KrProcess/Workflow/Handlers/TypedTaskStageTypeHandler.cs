using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Properties.Resharper;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Unity;
using NotificationHelper = Tessa.Extensions.Default.Shared.Notices.NotificationHelper;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.TypedTaskDescriptor"/>.
    /// </summary>
    public class TypedTaskStageTypeHandler : StageTypeHandlerBase
    {
        #region Nested Types

        /// <summary>
        /// Тип параметра метода сценария "После завершения задания".
        /// </summary>
        public class ScriptContext
        {
            #region Fields

            private readonly Func<Guid, Performer, DateTime?, double?, string, CancellationToken, Task<CardTask>> sendTaskAction;

            private readonly IStageTypeHandlerContext context;

            private readonly IStageTasksRevoker tasksRevoker;

            #endregion

            #region Constructors

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="ScriptContext"/>.
            /// </summary>
            /// <param name="task">Завершённое задание.</param>
            /// <param name="context">Контекст обработчика этапа.</param>
            /// <param name="tasksRevoker">Объект выполняющий отзыв заданий этапа.</param>
            /// <param name="sendTaskAction">Метод выполняющий отправку заданий.</param>
            public ScriptContext(
                CardTask task,
                IStageTypeHandlerContext context,
                IStageTasksRevoker tasksRevoker,
                Func<Guid, Performer, DateTime?, double?, string, CancellationToken, Task<CardTask>> sendTaskAction)
            {
                this.Task = task;
                this.sendTaskAction = sendTaskAction;
                this.context = context;
                this.tasksRevoker = tasksRevoker;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Возвращает завершаемое задание.
            /// </summary>
            [UsedImplicitly]
            public CardTask Task { get; }

            /// <summary>
            /// Возвращает или задаёт значение, показывающее, что этап должен быть немедленно завершён, активные задания, при этом, будут отозваны.
            /// </summary>
            [UsedImplicitly]
            public bool CompleteStage { get; set; } = false;

            #endregion

            #region Public Methods

            /// <summary>
            /// Отправляет новое задание.
            /// </summary>
            /// <param name="performer">Исполнитель.</param>
            /// <param name="taskType">Идентификатор типа задания. Если не задан, то используется значение параметра этапа "Тип задания".</param>
            /// <param name="planned">Дата запланированного завершения задания.</param>
            /// <param name="timeLimit">Срок (рабочие дни).</param>
            /// <param name="digest">Дайджест, если задано значение по умолчанию для типа, что используется начение параметра этапа "Дайджест".</param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Созданное задание или значение по умолчанию для типа, если при создании задания произошла ошибка.</returns>
            [UsedImplicitly]
            public async Task<CardTask> SendTaskAsync(
                Performer performer,
                Guid? taskType = null,
                DateTime? planned = null,
                double? timeLimit = null,
                string digest = null,
                CancellationToken cancellationToken = default)
            {
                var actualTaskType = taskType
                    ?? this.context.Stage.SettingsStorage.TryGet<Guid>(KrConstants.KrTypedTaskSettingsVirtual.TaskTypeID);
                var actualDigest = digest
                    ?? this.context.Stage.SettingsStorage.TryGet<string>(KrConstants.KrTypedTaskSettingsVirtual.TaskDigest);

                return await this.sendTaskAction(actualTaskType, performer, planned, timeLimit, actualDigest, cancellationToken);
            }

            /// <summary>
            /// Отзывает задание с указанным идентификатором.
            /// </summary>
            /// <param name="taskID">Идентификатор отзываемого задания.</param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Число отозванных задач.</returns>
            [UsedImplicitly]
            public async Task<int> RevokeTaskAsync(
                Guid taskID,
                CancellationToken cancellationToken = default)
            {
                var cardID = this.context.MainCardID;
                if (cardID is null)
                {
                    throw new InvalidOperationException("MainCardID is null.");
                }

                return await this.tasksRevoker.RevokeTaskAsync(new StageTaskRevokerContext(this.context, cancellationToken)
                {
                    CardID = cardID.Value,
                    TaskID = taskID,
                })
                    ? 1
                    : 0;
            }

            /// <summary>
            /// Отзывает все задания с указанными идентификаторами.
            /// </summary>
            /// <param name="taskIDs">Перечисление идентификатором отзываемых задач.</param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Число отозванных задач.</returns>
            [UsedImplicitly]
            public async Task<int> RevokeTaskAsync(
                IEnumerable<Guid> taskIDs,
                CancellationToken cancellationToken = default)
            {
                var cardID = this.context.MainCardID;
                if (cardID is null)
                {
                    throw new InvalidOperationException("MainCardID is null.");
                }

                return await this.tasksRevoker.RevokeTasksAsync(new StageTaskRevokerContext(this.context, cancellationToken)
                {
                    CardID = cardID.Value,
                    TaskIDs = taskIDs.ToList(),
                });
            }

            #endregion
        }

        #endregion

        #region Constants And Static Fields

        /// <summary>
        /// Дескриптор метода "После завершения задания".
        /// </summary>
        public static readonly KrExtraSourceDescriptor AfterTaskMethodDescriptor = new KrExtraSourceDescriptor("AfterTask")
        {
            DisplayName = "$KrStages_TypedTask_AfterTask",
            ParameterName = "TypedTaskContext",
            ParameterType = $"global::{typeof(TypedTaskStageTypeHandler).FullName}.{nameof(ScriptContext)}",
            ScriptField = KrConstants.KrTypedTaskSettingsVirtual.AfterTaskCompletion
        };

        /// <summary>
        /// Ключ по которому в <see cref="Stage.InfoStorage"/> содержится общее число заданий. Тип значения: <see cref="int"/>.
        /// </summary>
        protected const string ActiveTasksCount = nameof(ActiveTasksCount);

        /// <summary>
        /// Ключ по которому в <see cref="Stage.InfoStorage"/> содержится число заданий которое ещё надо отозвать. Тип значения: <see cref="int"/>.
        /// </summary>
        protected const string CompleteStageCountdown = nameof(CompleteStageCountdown);

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; }

        /// <summary>
        /// Возвращает сессию пользователя.
        /// </summary>
        protected ISession Session { get; }

        /// <summary>
        /// Стратегия для получения информации о ролях.
        /// </summary>
        protected IRoleGetStrategy RoleGetStrategy { get; }

        /// <summary>
        /// Возвращает обработчик контекстных ролей.
        /// </summary>
        protected IContextRoleManager ContextRoleManager { get; }

        /// <inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/>
        protected IKrStageTemplateCompilationCache CompilationCache { get; }

        /// <summary>
        /// Возвращает unity-контейнер.
        /// </summary>
        protected IUnityContainer UnityContainer { get; }

        /// <summary>
        /// Возвращает объект выполняющий отзыв заданий этапа.
        /// </summary>
        protected IStageTasksRevoker TasksRevoker { get; }

        /// <summary>
        /// Возвращает объект для взаимодействия с базой данных.
        /// </summary>
        protected IDbScope DbScope { get; }

        /// <summary>
        /// Возвращает объект для отправки уведомлений, построенных по карточке уведомления.
        /// </summary>
        protected INotificationManager NotificationManager { get; }

        /// <summary>
        /// Возвращает потокобезопасный кэш с карточками и дополнительными настройками.
        /// </summary>
        protected ICardCache CardCache { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TypedTaskStageTypeHandler"/>.
        /// </summary>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="compilationCache"><inheritdoc cref="CompilationCache" path="/summary"/></param>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="tasksRevoker">Объект выполняющий отзыв заданий этапа.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="notificationManager">Объект для отправки уведомлений, построенных по карточке уведомления.</param>
        /// <param name="cardCache">Потокобезопасный кэш с карточками и дополнительными настройками.</param>
        public TypedTaskStageTypeHandler(
            IKrScope krScope,
            ISession session,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            IKrStageTemplateCompilationCache compilationCache,
            IUnityContainer unityContainer,
            IStageTasksRevoker tasksRevoker,
            IDbScope dbScope,
            [Dependency(NotificationManagerNames.DeferredWithoutTransaction)] INotificationManager notificationManager,
            ICardCache cardCache)
        {
            this.KrScope = NotNullOrThrow(krScope);
            this.Session = NotNullOrThrow(session);
            this.RoleGetStrategy = NotNullOrThrow(roleGetStrategy);
            this.ContextRoleManager = NotNullOrThrow(contextRoleManager);
            this.CompilationCache = NotNullOrThrow(compilationCache);
            this.UnityContainer = NotNullOrThrow(unityContainer);
            this.TasksRevoker = NotNullOrThrow(tasksRevoker);
            this.DbScope = NotNullOrThrow(dbScope);
            this.NotificationManager = NotNullOrThrow(notificationManager);
            this.CardCache = NotNullOrThrow(cardCache);
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        public override async Task BeforeInitializationAsync(IStageTypeHandlerContext context)
        {
            await base.BeforeInitializationAsync(context);

            HandlerHelper.ClearCompletedTasks(context.Stage);
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleStageStartAsync(
            IStageTypeHandlerContext context)
        {
            var stage = context.Stage;
            var performers = stage.Performers;
            if (performers.Count == 0)
            {
                return StageHandlerResult.CompleteResult;
            }

            var settingsStorage = context.Stage.SettingsStorage;
            var taskType = settingsStorage.TryGet<Guid?>(KrConstants.KrTypedTaskSettingsVirtual.TaskTypeID);

            if (!taskType.HasValue)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(stage),
                        "$KrStages_TypedTask_TaskType"));
                return StageHandlerResult.EmptyResult;
            }

            var krSettings = await this.CardCache.Cards.GetAsync(DefaultCardTypes.KrSettingsTypeName);
            ListStorage<CardRow> rows;
            if (!krSettings.GetValue().Sections.TryGetValue(KrConstants.KrSettingsRouteExtraTaskTypes.Name, out var krSettingsRouteExtraTaskTypes)
                || (rows = krSettingsRouteExtraTaskTypes.TryGetRows()) is null
                || rows.All(i => i.Fields.Get<Guid>(KrConstants.KrSettingsRouteExtraTaskTypes.TaskTypeID) != taskType.Value))
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(stage),
                        LocalizationManager.Format(
                            "$KrStages_TypedTask_TaskTypeUndefined",
                            settingsStorage.TryGet<string>(KrConstants.KrTypedTaskSettingsVirtual.TaskTypeCaption),
                            settingsStorage.TryGet<string>(KrConstants.KrTypedTaskSettingsVirtual.TaskTypeName))));
                return StageHandlerResult.EmptyResult;
            }

            var digest = settingsStorage.TryGet<string>(KrConstants.KrTypedTaskSettingsVirtual.TaskDigest);

            foreach (var performer in performers)
            {
                var task = await this.SendTaskAsync(context, taskType.Value, performer, null, null, digest);

                if (task is null)
                {
                    return StageHandlerResult.EmptyResult;
                }
            }

            stage.InfoStorage.Remove(CompleteStageCountdown);
            stage.InfoStorage[ActiveTasksCount] = Int32Boxes.Box(performers.Count);

            return StageHandlerResult.InProgressResult;
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleTaskCompletionAsync(
            IStageTypeHandlerContext context)
        {
            var task = context.TaskInfo.Task;
            var stage = context.Stage;

            var completeStageCountdown = stage.InfoStorage.TryGet<int>(CompleteStageCountdown);
            if (completeStageCountdown >= 1)
            {
                stage.InfoStorage[CompleteStageCountdown] = Int32Boxes.Box(--completeStageCountdown);
                return completeStageCountdown == 0
                    ? StageHandlerResult.CompleteResult
                    : StageHandlerResult.InProgressResult;
            }

            if (task.State == CardRowState.Deleted)
            {
                stage.InfoStorage[ActiveTasksCount] = Int32Boxes.Box(stage.InfoStorage.TryGet<int>(ActiveTasksCount) - 1);

                HandlerHelper.AppendToCompletedTasksWithPreparing(stage, task);
            }

            var ctx = new ScriptContext(
                task,
                context,
                this.TasksRevoker,
                async (ttid, prf, plnd, tmlmt, dg, ct) =>
                {
                    stage.InfoStorage[ActiveTasksCount] = Int32Boxes.Box(stage.InfoStorage.TryGet<int>(ActiveTasksCount) + 1);
                    return await this.SendTaskAsync(context, ttid, prf, plnd, tmlmt, dg);
                });

            if (context.Stage.TemplateID.HasValue)
            {
                var compilationObject = await this.CompilationCache.GetAsync(
                    context.Stage.TemplateID.Value,
                    cancellationToken: context.CancellationToken);

                var inst = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrRuntimeClass,
                        SourceIdentifiers.StageAlias,
                        context.Stage.ID),
                    context.ValidationResult,
                    true);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return StageHandlerResult.EmptyResult;
                }

                if (inst is not null)
                {
                    await HandlerHelper.InitScriptContextAsync(this.UnityContainer, inst, context);
                    await inst.InvokeExtraAsync(AfterTaskMethodDescriptor.MethodName, ctx);
                }
            }

            if (ctx.CompleteStage)
            {
                return await this.CompleteStageAsync(context);
            }

            return stage.InfoStorage.TryGet<int>(ActiveTasksCount) == 0
                ? StageHandlerResult.CompleteResult
                : StageHandlerResult.InProgressResult;
        }

        /// <inheritdoc/>
        public override async Task<bool> HandleStageInterruptAsync(IStageTypeHandlerContext context) =>
            await this.TasksRevoker.RevokeAllStageTasksAsync(new StageTaskRevokerContext(context, context.CancellationToken));

        #endregion

        #region Protected Methods

        /// <summary>
        /// Отправляет новое задание.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="taskType">Идентификатор типа задания.</param>
        /// <param name="performer">Исполнитель.</param>
        /// <param name="planned">Дата выполнения.</param>
        /// <param name="timeLimit">Срок (рабочие дни).</param>
        /// <param name="digest">Дайджест.</param>
        /// <returns>Созданное задание или значение по умолчанию для типа, если при отправке задания произошла ошибка.</returns>
        protected async Task<CardTask> SendTaskAsync(
            IStageTypeHandlerContext context,
            Guid taskType,
            Performer performer,
            DateTime? planned,
            double? timeLimit,
            string digest)
        {
            var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);
            var author = await HandlerHelper.GetStageAuthorAsync(context, this.RoleGetStrategy, this.ContextRoleManager, this.Session);
            if (author == null)
            {
                return null;
            }
            var (kindID, kindCaption) = HandlerHelper.GetTaskKind(context);

            var authorID = author.AuthorID;
            var taskInfo = await context.WorkflowAPI.SendTaskAsync(
                taskType,
                digest,
                performer.PerformerID,
                performer.PerformerName,
                modifyTaskAction: (t, ct) =>
                {
                    t.AddAuthor(authorID);
                    t.GroupRowID = groupID;
                    t.Planned = planned ?? context.Stage.Planned;
                    t.PlannedWorkingDays = t.Planned.HasValue ? null : (timeLimit ?? context.Stage.TimeLimitOrDefault);
                    HandlerHelper.SetTaskKind(t, kindID, kindCaption, context);

                    return new ValueTask();
                },
                cancellationToken: context.CancellationToken);

            if (taskInfo is null)
            {
                return default;
            }

            var task = taskInfo.Task;

            task.Flags |= CardTaskFlags.CreateHistoryItem;
            context.ContextualSatellite.AddToHistory(task.RowID,
                context.WorkflowProcess.InfoStorage.TryGet(KrConstants.Keys.Cycle, 1));

            if (context.CardExtensionContext is ICardStoreExtensionContext storeContext)
            {
                await CardComponentHelper.FillTaskAssignedRolesAsync(task, storeContext.DbScope, cancellationToken: context.CancellationToken);
            }

            await context.WorkflowAPI.AddActiveTaskAsync(task.RowID, context.CancellationToken);
            context.ValidationResult.Add(
                await this.NotificationManager.SendAsync(
                    DefaultNotifications.TaskNotification,
                    task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID).Select(x => x.RoleID).ToArray(),
                    new NotificationSendContext()
                    {
                        MainCardID = context.MainCardID ?? Guid.Empty,
                        Info = NotificationHelper.GetInfoWithTask(task),
                        ModifyEmailActionAsync = async (email, ct) =>
                        {
                            NotificationHelper.ModifyEmailForMobileApprovers(
                                email,
                                task,
                                await NotificationHelper.GetMobileApprovalEmailAsync(this.CardCache, ct));

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
        /// Обрабатывает завершение этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат обработки этапа.</returns>
        protected async Task<StageHandlerResult> CompleteStageAsync(IStageTypeHandlerContext context)
        {
            var stage = context.Stage;
            await using (this.DbScope.Create())
            {
                var db = this.DbScope.Db;
                // Получение списка заданий из таблицы WorkflowTasks
                var currentTasks = await db.SetCommand(
                        this.DbScope.BuilderFactory
                            .Select()
                            .C("RowID")
                            .From("WorkflowTasks").NoLock()
                            .Where().C("ProcessRowID").Equals().P("pid")
                            .Build(),
                        db.Parameter("pid", context.ProcessInfo.ProcessID))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(context.CancellationToken);
                if (currentTasks.Count == 0)
                {
                    return StageHandlerResult.CompleteResult;
                }
                stage.InfoStorage[CompleteStageCountdown] = Int32Boxes.Box(currentTasks.Count);
                await this.TasksRevoker.RevokeAllStageTasksAsync(new StageTaskRevokerContext(context, context.CancellationToken));
                return StageHandlerResult.InProgressResult;
            }
        }

        #endregion
    }
}
