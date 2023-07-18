#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Numbers;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using NotificationHelper = Tessa.Extensions.Default.Shared.Notices.NotificationHelper;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.RegistrationDescriptor"/>.
    /// </summary>
    public class RegistrationStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RegistrationStageTypeHandler"/>.
        /// </summary>
        /// <param name="numberDirectorContainer">Объект, выполняющий регистрацию и предоставляющий доступ к подсистеме номеров для типов карточек, включая объекты <see cref="INumberDirector"/>, <see cref="INumberComposer"/> и <see cref="INumberQueueProcessor"/>.</param>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="cardMetadata">Метаинформацию, необходимую для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="eventManager">Объект предоставляющий методы для отправки событий маршрутов документов.</param>
        /// <param name="calendarService">Объект предоставляющий методы для работы с бизнес календарём.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="tasksRevoker">Объект выполняющий отзыв заданий этапа.</param>
        /// <param name="notificationManager">Объект для отправки уведомлений, построенных по карточке уведомления.</param>
        /// <param name="cardCache">Потокобезопасный кэш с карточками и дополнительными настройками.</param>
        public RegistrationStageTypeHandler(
            INumberDirectorContainer numberDirectorContainer,
            IKrScope krScope,
            ISession session,
            ICardMetadata cardMetadata,
            IKrStageSerializer serializer,
            IKrEventManager eventManager,
            IBusinessCalendarService calendarService,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            IStageTasksRevoker tasksRevoker,
            [Dependency(NotificationManagerNames.DeferredWithoutTransaction)] INotificationManager notificationManager,
            ICardCache cardCache)
        {
            this.NumberDirectorContainer = numberDirectorContainer;
            this.KrScope = krScope;
            this.Session = session;
            this.CardMetadata = cardMetadata;
            this.Serializer = serializer;
            this.EventManager = eventManager;
            this.CalendarService = calendarService;
            this.RoleGetStrategy = roleGetStrategy;
            this.ContextRoleManager = contextRoleManager;
            this.TasksRevoker = tasksRevoker;
            this.NotificationManager = notificationManager;
            this.CardCache = cardCache;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Возвращает или задаёт объект, выполняющий регистрацию и предоставляющий доступ к подсистеме номеров для типов карточек, включая объекты <see cref="INumberDirector"/>, <see cref="INumberComposer"/> и <see cref="INumberQueueProcessor"/>.
        /// </summary>
        protected INumberDirectorContainer NumberDirectorContainer { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; set; }

        /// <summary>
        /// Возвращает или задаёт сессию пользователя.
        /// </summary>
        protected ISession Session { get; set; }

        /// <summary>
        /// Возвращает или задаёт метаинформацию, необходимую для использования типов карточек совместно с пакетом карточек.
        /// </summary>
        protected ICardMetadata CardMetadata { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для сериализации параметров этапов.
        /// </summary>
        protected IKrStageSerializer Serializer { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для отправки событий маршрутов документов.
        /// </summary>
        protected IKrEventManager EventManager { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с бизнес календарём.
        /// </summary>
        protected IBusinessCalendarService CalendarService { get; set; }

        /// <summary>
        /// Стратегия для получения информации о ролях.
        /// </summary>
        protected IRoleGetStrategy RoleGetStrategy { get; set; }

        /// <summary>
        /// Возвращает или задаёт обработчик контекстных ролей.
        /// </summary>
        protected IContextRoleManager ContextRoleManager { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект выполняющий отзыв заданий этапа.
        /// </summary>
        protected IStageTasksRevoker TasksRevoker { get; set; }

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
        /// Выполняет регистрацию документа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="taskInfo">Регистрация производится после задания.</param>
        /// <returns>Результат обработки этапа.</returns>
        protected virtual async Task<StageHandlerResult> SyncRegistrationAsync(
            IStageTypeHandlerContext context,
            IWorkflowTaskInfo? taskInfo = null)
        {
            // Непосредственная регистрация карточки.
            if (context.MainCardID.HasValue
                && context.MainCardType is not null)
            {
                var mainCard = await this.KrScope.GetMainCardAsync(
                    context.MainCardID.Value,
                    cancellationToken: context.CancellationToken);

                if (mainCard is null)
                {
                    return StageHandlerResult.EmptyResult;
                }

                // выделение номера при регистрации
                var numberProvider = this.NumberDirectorContainer.GetProvider(context.MainCardType.ID);
                var numberDirector = numberProvider.GetDirector();
                var numberComposer = numberProvider.GetComposer();
                var numberContext = await numberDirector.CreateContextAsync(
                    numberComposer,
                    mainCard,
                    context.MainCardType,
                    context.CardExtensionContext is ICardStoreExtensionContext storeContext
                        ? storeContext.Request.Info
                        : null,
                    context.CardExtensionContext,
                    transactionMode: NumberTransactionMode.SeparateTransaction,
                    context.CancellationToken);

                await numberDirector.NotifyOnRegisteringCardAsync(numberContext, context.CancellationToken);
                context.ValidationResult.Add(numberContext.ValidationResult);

                await this.EventManager.RaiseAsync(DefaultEventTypes.RegistrationEvent, context, cancellationToken: context.CancellationToken);

                var cycle = this.GetCycle(context);
                if (taskInfo is not null)
                {
                    context.ContextualSatellite.AddToHistory(taskInfo.Task.RowID, cycle);
                }
                else
                {
                    var fakeHistoryRecord = await this.CreateRegistrationTaskHistoryItemAsync(context);

                    if (fakeHistoryRecord is null)
                    {
                        return StageHandlerResult.EmptyResult;
                    }

                    mainCard.TaskHistory.Add(fakeHistoryRecord);
                    context.ContextualSatellite.AddToHistory(fakeHistoryRecord.RowID, cycle);
                }
            }
            context.WorkflowProcess.State = KrState.Registered;
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет регистрацию документа с предварительной отправкой задания регистрации.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат обработки этапа.</returns>
        protected virtual async Task<StageHandlerResult> AsyncRegistrationAsync(
            IStageTypeHandlerContext context)
        {
            var api = context.WorkflowAPI;

            // Получаем исполнителя, указанного в настройках этапа.
            var performer = context.Stage.Performer;
            if (performer is null)
            {
                context.ValidationResult.AddError(this, "$KrStages_Registration_PerformerNotSpecified");
                return StageHandlerResult.EmptyResult;
            }
            var performerID = performer.PerformerID;
            var performerName = performer.PerformerName;

            // Установка в карточке состояния "На регистрации"
            context.WorkflowProcess.State = KrState.Registration;

            var digest = context.Stage.SettingsStorage.TryGet<string>(KrRegistrationStageSettingsVirtual.Comment)
                ?? context.Stage.Name;

            var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);

            var author = await HandlerHelper.GetStageAuthorAsync(context, this.RoleGetStrategy, this.ContextRoleManager, this.Session);
            if (author is null)
            {
                return StageHandlerResult.EmptyResult;
            }
            var authorID = author.AuthorID;
            var (kindID, kindCaption) = HandlerHelper.GetTaskKind(context);
            // Отправка задания регистрации
            var taskInfo = await api.SendTaskAsync(
                DefaultTaskTypes.KrRegistrationTypeID,
                digest,
                performerID,
                performerName,
                modifyTaskAction: (t, ct) =>
                {
                    t.AddAuthor(authorID);
                    t.Planned = context.Stage.Planned;
                    t.PlannedWorkingDays = context.Stage.Planned.HasValue ? null : context.Stage.TimeLimitOrDefault;
                    t.GroupRowID = groupID;
                    t.Flags |= CardTaskFlags.CreateHistoryItem;
                    HandlerHelper.SetTaskKind(t, kindID, kindCaption, context);

                    return new ValueTask();
                },
                cancellationToken: context.CancellationToken);

            if (taskInfo is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            // Добавление задания в список активных заданий,
            // которые будут отображаться в таблице над заданиями.
            await api.AddActiveTaskAsync(taskInfo.Task.RowID, context.CancellationToken);

            if (context.CardExtensionContext is ICardStoreExtensionContext storeContext)
            {
                await CardComponentHelper.FillTaskAssignedRolesAsync(taskInfo.Task, storeContext.DbScope!, cancellationToken: context.CancellationToken);
            }

            context.ValidationResult.Add(
                await this.NotificationManager.SendAsync(
                    DefaultNotifications.TaskNotification,
                    taskInfo.Task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID).Select(x => x.RoleID).ToArray(),
                    new NotificationSendContext()
                    {
                        MainCardID = context.MainCardID ?? Guid.Empty,
                        Info = NotificationHelper.GetInfoWithTask(taskInfo.Task),
                        ModifyEmailActionAsync = async (email, ct) =>
                        {
                            NotificationHelper.ModifyEmailForMobileApprovers(
                                email,
                                taskInfo.Task,
                                await NotificationHelper.GetMobileApprovalEmailAsync(this.CardCache, ct));

                            NotificationHelper.ModifyTaskCaption(
                                email,
                                taskInfo.Task);
                        },
                        GetCardFuncAsync = (validationResult, ct) =>
                            context.MainCardAccessStrategy.GetCardAsync(
                                validationResult: validationResult,
                                cancellationToken: ct),
                    },
                    context.CancellationToken));

            // Результат говорит подсистеме маршрутов о том, что этап находится в процессе выполнения
            return StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Создаёт запись в истории действий о выполнении регистрации.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Запись истории заданий или значение <see langword="null"/>, если произошла ошибка.</returns>
        protected virtual async Task<CardTaskHistoryItem?> CreateRegistrationTaskHistoryItemAsync(IStageTypeHandlerContext context)
        {
            if (!(await this.CardMetadata.GetCardTypesAsync(context.CancellationToken))
                .TryGetValue(DefaultTaskTypes.KrRegistrationTypeID, out var taskType))
            {
                return null;
            }

            const string result = "$ApprovalHistory_DocumentRegistered";
            var optionID = DefaultCompletionOptions.RegisterDocument;
            var userID = this.Session.User.ID;
            var userName = this.Session.User.Name;
            var utcNow = DateTime.UtcNow;

            // чтобы регистрация была позже, чем отзыв согласований и другие строчки в NextRequest-е
            var offsetNow = utcNow.AddMilliseconds(500.0);
            var option = (await this.CardMetadata.GetEnumerationsAsync(context.CancellationToken)).CompletionOptions[optionID];
            var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);
            // Временная зона текущего сотрудника и календарь, для записи в историю заданий
            var userZoneInfo = await this.CalendarService.GetRoleTimeZoneInfoAsync(userID, context.CancellationToken);
            var userCalendarInfo = await this.CalendarService.GetRoleCalendarInfoAsync(userID, context.CancellationToken);
            if (userCalendarInfo is null)
            {
                context.ValidationResult.AddError(this, await LocalizationManager.FormatAsync("$KrMessages_NoRoleCalendar", userID, context.CancellationToken));
                return null;
            }
            var settings = new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                [TaskHistorySettingsKeys.PerformerID] = userID,
                [TaskHistorySettingsKeys.PerformerName] = userName
            };

            var item = new CardTaskHistoryItem
            {
                State = CardTaskHistoryState.Inserted,
                RowID = Guid.NewGuid(),
                TypeID = DefaultTaskTypes.KrRegistrationTypeID,
                TypeName = taskType.Name,
                TypeCaption = taskType.Caption,
                Created = offsetNow,
                Planned = offsetNow,
                InProgress = offsetNow,
                Completed = offsetNow,
                UserID = userID,
                UserName = userName,
                AuthorID = userID,
                AuthorName = userName,
                Result = result,
                OptionID = optionID,
                OptionCaption = option.Caption,
                OptionName = option.Name,
                ParentRowID = null,
                CompletedByID = userID,
                CompletedByName = userName,
                CompletedByRole = userName,
                GroupRowID = groupID,
                TimeZoneID = userZoneInfo.TimeZoneID,
                TimeZoneUtcOffsetMinutes = (int?) userZoneInfo.TimeZoneUtcOffset.TotalMinutes,
                CalendarID = userCalendarInfo.CalendarID,
                Settings = settings,
                AssignedOnRole = userName
            };

            return item;
        }

        /// <summary>
        /// Возвращает номер текущего цикла согласования.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Номер текущего цикла согласования.</returns>
        protected virtual int GetCycle(IStageTypeHandlerContext context)
        {
            if (context.RunnerMode == KrProcessRunnerMode.Async
                && context.ProcessInfo.ProcessTypeName == KrProcessName)
            {
                // Для основного процесса цикл лежит в его инфо.
                return context.WorkflowProcess.InfoStorage.TryGet<int?>(Keys.Cycle) ?? 1;
            }

            return ProcessInfoCacheHelper.Get(this.Serializer, context.ContextualSatellite)?.TryGet<int?>(Keys.Cycle)
                ?? 0;
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
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            // Запоминаем состояние до начала регистрации.
            var info = ProcessInfoCacheHelper.Get(this.Serializer, context.ContextualSatellite);
            info[Keys.StateBeforeRegistration] = Int32Boxes.Box((int) context.WorkflowProcess.State);

            // При запуске этапа определяем, в каком режиме сейчас идет выполнение
            switch (context.RunnerMode)
            {
                case KrProcessRunnerMode.Sync:
                    // Выполнение в синхронном режиме, отправка заданий запрещена
                    // Выполняем регистрацию
                    return await this.SyncRegistrationAsync(context);
                case KrProcessRunnerMode.Async:
                    var withoutTask =
                        context.Stage.SettingsStorage.TryGet<bool?>(KrRegistrationStageSettingsVirtual.WithoutTask);
                    return await (withoutTask == true
                        // Выполняем регистрацию
                        ? this.SyncRegistrationAsync(context)
                        // Выполнение в асинхронном режиме, отправляем задание регистрации
                        : this.AsyncRegistrationAsync(context));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleTaskCompletionAsync(IStageTypeHandlerContext context)
        {
            // Завершение задания регистрации
            // Вся информация о задании доступна в контексте
            var taskInfo = context.TaskInfo;
            var task = taskInfo.Task;
            var taskType = task.TypeID;
            var optionID = task.OptionID ?? Guid.Empty;

            if (taskType == DefaultTaskTypes.KrRegistrationTypeID
                && optionID == DefaultCompletionOptions.RegisterDocument)
            {
                // Записываем в список активных заданий
                HandlerHelper.AppendToCompletedTasksWithPreparing(context.Stage, task);

                // Вариант завершения "Зарегистрировать"
                // Удаляем задание из списка активных
                await context.WorkflowAPI.TryRemoveActiveTaskAsync(taskInfo.Task.RowID, context.CancellationToken);
                // Проводим регистрацию документа
                await this.SyncRegistrationAsync(context, taskInfo);
                // Сообщаем подсистеме маршрутов о том, что работа этапа завершена.
                return StageHandlerResult.CompleteResult;
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public override Task<bool> HandleStageInterruptAsync(IStageTypeHandlerContext context) =>
            this.TasksRevoker.RevokeAllStageTasksAsync(new StageTaskRevokerContext(context, context.CancellationToken));

        #endregion
    }
}
