using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using NotificationHelper = Tessa.Extensions.Default.Shared.Notices.NotificationHelper;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.EditDescriptor"/>.
    /// </summary>
    public class EditStageTypeHandler : StageTypeHandlerBase
    {
        #region Constants

        /// <summary>
        /// Имя ключа, по которому в <see cref="Stage.InfoStorage"/> содержится идентификатор этапа на который необходимо выполнить переход после завершения этапа "Доработка". Используется при возврате на этап согласование или подписание после доработки автором. Тип значения: <see cref="Guid"/>.
        /// </summary>
        public const string ReturnToStage = nameof(ReturnToStage);

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeregistrationStageTypeHandler"/>.
        /// </summary>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="calendarService">Интерфейс API бизнес календаря.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="tasksRevoker">Объект выполняющий отзыв заданий этапа.</param>
        /// <param name="notificationManager">Объект для отправки уведомлений, построенных по карточке уведомления.</param>
        /// <param name="cardCache">Потокобезопасный кэш с карточками и дополнительными настройками.</param>
        public EditStageTypeHandler(
            IKrScope krScope,
            IBusinessCalendarService calendarService,
            ISession session,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            IStageTasksRevoker tasksRevoker,
            [Dependency(NotificationManagerNames.DeferredWithoutTransaction)] INotificationManager notificationManager,
            ICardCache cardCache)
        {
            this.KrScope = krScope ?? throw new ArgumentNullException(nameof(krScope));
            this.CalendarService = calendarService;
            this.Session = session;
            this.RoleGetStrategy = roleGetStrategy;
            this.ContextRoleManager = contextRoleManager;
            this.TasksRevoker = tasksRevoker;
            this.NotificationManager = notificationManager;
            this.CardCache = cardCache;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Возвращает объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; }

        /// <summary>
        /// Возвращает объект предоставляющий методы для взаимодействия с бизнес календарём.
        /// </summary>
        protected IBusinessCalendarService CalendarService { get; }

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

        /// <summary>
        /// Возвращает объект выполняющий отзыв заданий этапа.
        /// </summary>
        protected IStageTasksRevoker TasksRevoker { get; }

        /// <summary>
        /// Возвращает объект для отправки уведомлений, построенных по карточке уведомления.
        /// </summary>
        protected INotificationManager NotificationManager { get; }

        /// <summary>
        /// Возвращает потокобезопасный кэш с карточками и дополнительными настройками.
        /// </summary>
        protected ICardCache CardCache { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Начинает новый цикл согласования.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="result">Результат выполнения или значение <see langword="null"/>, если необходимо завершить этап, если не выполняется обработка доработки автором, то в этом случае игнорируется. Если не задан, то этап завершается с результатом выполнения <see cref="StageHandlerResult.CompleteResult"/>.</param>
        /// <returns>Результат выполнения этапа.</returns>
        protected virtual StageHandlerResult StartApproval(IStageTypeHandlerContext context, StageHandlerResult? result = default)
        {
            var returnToStage = context.Stage.InfoStorage.TryGet<Guid?>(ReturnToStage);
            if (returnToStage.HasValue)
            {
                context.Stage.InfoStorage.Remove(ReturnToStage);
                return StageHandlerResult.Transition(returnToStage.Value, keepStageStates: true);
            }

            var fields = context.ContextualSatellite.Sections[KrApprovalCommonInfo.Name].Fields;
            fields[KrApprovalCommonInfo.ApprovedBy] = string.Empty;
            fields[KrApprovalCommonInfo.DisapprovedBy] = string.Empty;

            return result ?? StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Увеличивает номер цикла согласования, если это разрешено настроками этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        protected virtual void TryIncrementCycle(IStageTypeHandlerContext context)
        {
            if (context.Stage.SettingsStorage.TryGet<bool?>(KrEditSettingsVirtual.IncrementCycle) == true)
            {
                var info = context.WorkflowProcess.InfoStorage;
                info[Keys.Cycle] = Int32Boxes.Box(info.TryGet<int>(Keys.Cycle) + 1);
            }
        }

        /// <summary>
        /// Возвращает значение, показывающее, выполнялся ли предыдущий этап из другой группы этапов или карточки.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Значение <see langword="true"/>, если отсутствует информация о ходе выполнения процесса или, если есть этап выполнявшийся в другой группе или карточке или такой не найден, иначе - <see langword="false"/>, если найден не скрытый предыдущий этап выполнявшийся в группе и текущей карточке, что и текущий этап.</returns>
        protected virtual bool TransitFromDifferentGroup(IStageTypeHandlerContext context)
        {
            var trace = this.KrScope.GetKrProcessRunnerTrace();

            if (trace is null
                || trace.Count == 0)
            {
                return true;
            }

            var cardID = context.MainCardID;
            var currentStageGroupID = context.Stage.StageGroupID;

            for (var i = trace.Count - 1; 0 <= i; i--)
            {
                var traceItem = trace[i];

                if (traceItem.ProcessID != context.ProcessInfo.ProcessID)
                {
                    continue;
                }

                var prevStage = traceItem.Stage;

                if (prevStage.StageGroupID != currentStageGroupID
                    || traceItem.CardID != cardID)
                {
                    return true;
                }
                if (!prevStage.Hidden)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task BeforeInitializationAsync(IStageTypeHandlerContext context)
        {
            await base.BeforeInitializationAsync(context);

            HandlerHelper.ClearCompletedTasks(context.Stage);
        }

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var stage = context.Stage;
            var settings = stage.SettingsStorage;
            var returnToStage = stage.InfoStorage.ContainsKey(ReturnToStage);
            if (!returnToStage)
            {
                this.TryIncrementCycle(context);
            }

            if (settings.TryGet<bool?>(KrEditSettingsVirtual.DoNotSkipStage) != true
                && this.TransitFromDifferentGroup(context))
            {
                SetVisibility(false);
                return this.StartApproval(context, StageHandlerResult.SkipResult);
            }

            SetVisibility(true);

            var author = await HandlerHelper.GetStageAuthorAsync(context, this.RoleGetStrategy, this.ContextRoleManager, this.Session);
            if (author is null)
            {
                return StageHandlerResult.EmptyResult;
            }
            var authorID = author.AuthorID;

            var digest = settings.TryGet<string>(KrEditSettingsVirtual.Comment);
            var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);
            var incrementCycle = settings.TryGet<bool?>(KrEditSettingsVirtual.IncrementCycle) == true;

            var taskInfo = await context.WorkflowAPI.SendTaskAsync(
                returnToStage || !incrementCycle
                    ? DefaultTaskTypes.KrEditInterjectTypeID
                    : DefaultTaskTypes.KrEditTypeID,
                string.Empty,
                settings.Get<Guid>(KrSinglePerformerVirtual.PerformerID),
                settings.Get<string>(KrSinglePerformerVirtual.PerformerName),
                modifyTaskAction: (p, ct) =>
                {
                    p.AddAuthor(authorID);
                    p.Planned = stage.Planned;
                    p.PlannedWorkingDays = context.Stage.Planned.HasValue ? null : context.Stage.TimeLimitOrDefault;
                    p.GroupRowID = groupID;
                    if (!string.IsNullOrWhiteSpace(digest))
                    {
                        p.Digest = digest;
                    }
                    var (kindID, kindCaption) = HandlerHelper.GetTaskKind(context);
                    HandlerHelper.SetTaskKind(p, kindID, kindCaption, context);

                    return new ValueTask();
                },
                cancellationToken: context.CancellationToken);

            if (taskInfo is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            var sentTask = taskInfo.Task;
            sentTask.Flags |= CardTaskFlags.CreateHistoryItem;
            context.ContextualSatellite.AddToHistory(sentTask.RowID, context.WorkflowProcess.InfoStorage.TryGet(Keys.Cycle, 1));
            await context.WorkflowAPI.AddActiveTaskAsync(sentTask.RowID, context.CancellationToken);

            if (context.CardExtensionContext is ICardStoreExtensionContext storeContext)
            {
                await CardComponentHelper.FillTaskAssignedRolesAsync(sentTask, storeContext.DbScope, cancellationToken: context.CancellationToken);

                context.ValidationResult.Add(
                    await this.NotificationManager.SendAsync(
                        DefaultNotifications.TaskNotification,
                        sentTask.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID).Select(x => x.RoleID).ToArray(),
                        new NotificationSendContext()
                        {
                            MainCardID = context.MainCardID ?? Guid.Empty,
                            Info = NotificationHelper.GetInfoWithTask(sentTask),
                            ModifyEmailActionAsync = async (email, ct) =>
                            {
                                NotificationHelper.ModifyEmailForMobileApprovers(
                                    email,
                                    sentTask,
                                    await NotificationHelper.GetMobileApprovalEmailAsync(this.CardCache, ct));

                                NotificationHelper.ModifyTaskCaption(
                                    email,
                                    sentTask);
                            },
                            GetCardFuncAsync = (validationResult, ct) =>
                                context.MainCardAccessStrategy.GetCardAsync(
                                    validationResult: validationResult,
                                    cancellationToken: ct),
                        },
                        context.CancellationToken));
            }

            if ((settings.TryGet<bool?>(KrEditSettingsVirtual.ChangeState) ?? default)
                && !returnToStage
                && !this.KrScope.Info.TryGet<bool>(Keys.IgnoreChangeState))
            {
                context.WorkflowProcess.State = KrState.Editing;
            }

            return StageHandlerResult.InProgressResult;

            void SetVisibility(
                bool visible)
            {
                if (settings.TryGet<bool?>(KrEditSettingsVirtual.ManageStageVisibility) == true)
                {
                    context.Stage.Hidden = !visible;
                    context.Stage.AddAutomaticallyChangedValue(nameof(Stage.Hidden));
                }
            }
        }

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleTaskCompletionAsync(IStageTypeHandlerContext context)
        {
            var task = context.TaskInfo.Task;
            var taskType = task.TypeID;
            if (taskType != DefaultTaskTypes.KrEditTypeID
                 && taskType != DefaultTaskTypes.KrEditInterjectTypeID)
            {
                return StageHandlerResult.EmptyResult;
            }

            if (task.Card.Sections.TryGetValue(KrTaskCommentVirtual.Name, out var commSec)
                && commSec.Fields.TryGetValue(KrTaskCommentVirtual.Comment, out var commentObj)
                && commentObj is string comment)
            {
                context.WorkflowProcess.AuthorComment = comment;

                if (!string.IsNullOrEmpty(comment))
                {
                    await HandlerHelper.SetTaskResultAsync(context, task, comment);
                }
            }

            await context.WorkflowAPI.RemoveActiveTaskAsync(context.TaskInfo.Task.RowID, context.CancellationToken);

            HandlerHelper.AppendToCompletedTasksWithPreparing(context.Stage, task);

            return this.StartApproval(context);
        }

        /// <inheritdoc/>
        public override async Task<bool> HandleStageInterruptAsync(
            IStageTypeHandlerContext context)
        {
            context.Stage.InfoStorage.Remove(ReturnToStage);
            return await this.TasksRevoker.RevokeAllStageTasksAsync(new StageTaskRevokerContext(context, context.CancellationToken));
        }

        #endregion
    }
}
