using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using NLog;
using Tessa.Cards;
using Tessa.Extensions.Default.Client.Forums;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Notifications;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;
using Unity;

namespace Tessa.Extensions.Default.Client.Notifications
{
    /// <summary>
    /// Объект, управляющий уведомлениями по новым заданиям.
    /// </summary>
    public class KrNotificationManager :
        IKrNotificationManager,
        IDisposable
    {
        #region Constructors

        public KrNotificationManager(
            KrSettingsLazy settingsLazy,
            INotificationUIManager notificationUIManager,
            INotificationUIFactory notificationUIFactory,
            IUserSettings userSettings,
            IViewService viewService,
            ISession session,
            ICardMetadata generalMetadata,
            ISessionFailedChecker sessionFailedChecker,
            [OptionalDependency] IUnityDisposableContainer disposableContainer = null)
        {
            this.settingsLazy = NotNullOrThrow(settingsLazy);
            this.notificationUIManager = NotNullOrThrow(notificationUIManager);
            this.notificationUIFactory = NotNullOrThrow(notificationUIFactory);
            this.userSettings = NotNullOrThrow(userSettings);
            this.viewService = NotNullOrThrow(viewService);
            this.session = NotNullOrThrow(session);
            this.generalMetadata = NotNullOrThrow(generalMetadata);
            this.sessionFailedChecker = NotNullOrThrow(sessionFailedChecker);

            disposableContainer?.Register(this);
        }

        #endregion

        #region RowInfo Private Class

        private sealed class RowInfo
        {
            #region Constructors

            public RowInfo(
                object rowValue,
                Dictionary<string, int> indicesByName)
            {
                this.row = (IList<object>) rowValue;
                this.indicesByName = indicesByName;
            }

            #endregion

            #region Fields

            private readonly IList<object> row;

            private readonly Dictionary<string, int> indicesByName;

            #endregion

            #region Methods

            public T Get<T>(string column)
            {
                int index = this.indicesByName[column];
                return (T) this.row[index];
            }


            public DateTime GetDateTime(string column)
            {
                int index = this.indicesByName[column];
                DateTime value = (DateTime) this.row[index];

                return value.Kind != DateTimeKind.Unspecified
                    ? value.ToUniversalTime()
                    : DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }

            #endregion
        }

        #endregion

        #region TaskInfo Private Class

        private sealed class TaskInfo
        {
            #region Constructors

            public TaskInfo(
                ITaskInfoModel model,
                Guid cardID,
                string cardDigest,
                string cardTypeCaption)
            {
                this.Model = model;
                this.CardID = cardID;
                this.CardDigest = cardDigest;
                this.CardTypeCaption = cardTypeCaption;
            }

            #endregion

            #region Properties

            public ITaskInfoModel Model { get; }

            public Guid CardID { get; }

            public string CardDigest { get; }

            public string CardTypeCaption { get; }

            #endregion
        }

        #endregion

        #region Fields

        /// <summary>
        /// Представление "Мои задания".
        /// </summary>
        private ITessaView tasksView;

        /// <summary>
        /// Представление "Функциональные роли задания с замещениями".
        /// </summary>
        private ITessaView cardTaskSessionRolesView;

        /// <summary>
        /// Представление "Функциональные роли".
        /// </summary>
        private ITessaView cardTaskAssignedRolesView;

        /// <summary>
        /// Дата и время предыдущего запроса на получение новых заданий.
        /// Задания загружаются начиная с этой даты и позже.
        /// </summary>
        private DateTime prevDateTime;

        private volatile bool canFetch;

        private volatile bool isFetchingNow;

        private readonly KrSettingsLazy settingsLazy;

        private readonly INotificationUIManager notificationUIManager;

        private readonly INotificationUIFactory notificationUIFactory;

        private readonly IUserSettings userSettings;

        private readonly IViewService viewService;

        private readonly ISession session;

        private readonly ICardMetadata generalMetadata;

        private readonly ISessionFailedChecker sessionFailedChecker;

        private readonly AsyncLock asyncLock = new AsyncLock();

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Минимальный интервал между проверками, запускаемыми по таймеру или вручную.
        /// Чтобы пользователи не могли много раз подряд нажимать на хоткей и загружать сервер.
        /// </summary>
        private static readonly TimeSpan minCheckInterval = TimeSpan.FromSeconds(1.0);

        #endregion

        #region Private Methods

        private ICommand CreateOpenViewCommand(KrSettings settings) =>
            this.notificationUIFactory.ProcessLinkCommand(
                string.Format(
                    "Action=ItemActivate&WID={0:D}&IID={1:D}",
                    settings.NotificationWorkplaceToOpenMyTasksID,
                    settings.NotificationNodeToOpenMyTasksID));

        #endregion

        #region Protected Methods

        protected virtual async Task CheckTasksCoreAsync(bool manualCheck, CancellationToken cancellationToken = default)
        {
            KrSettings settings = await this.settingsLazy.GetValueAsync(cancellationToken).ConfigureAwait(false);

            IViewMetadata tasksViewMetadata = await this.tasksView.GetMetadataAsync(cancellationToken).ConfigureAwait(false);
            ITessaViewRequest tasksViewRequest = new TessaViewRequest(tasksViewMetadata) { CalculateRowCounting = false };

            IViewColumnMetadata creationDateColumn = tasksViewMetadata.Columns.FindByName(settings.NotificationSortingColumnAlias);
            if (creationDateColumn is not null && !string.IsNullOrEmpty(creationDateColumn.SortBy))
            {
                var sortingColumns = tasksViewRequest.SortingColumns;
                if (sortingColumns is null)
                {
                    sortingColumns = new List<ISortingColumn>();
                    tasksViewRequest.SortingColumns = sortingColumns;
                }

                sortingColumns.Add(
                    new SortingColumn
                    {
                        Alias = settings.NotificationSortingColumnAlias,
                        SortDirection = settings.NotificationSortingColumnDirection,
                    });
            }

            var parameters = new List<RequestParameter>();

            var viewSpecialParameters = new ViewSpecialParameters(
                new ViewCurrentUserParameters(this.session),
                new ViewPagingParameters(),
                new ViewCardParameters());

            // ReSharper disable once AssignNullToNotNullAttribute
            viewSpecialParameters.ProvideCurrentUserIdParameter(parameters);

            int pageLimit = -1;

            if (tasksViewMetadata.Paging != Paging.No)
            {
                const int currentPage = 1;
                pageLimit = settings.NotificationPageLimit;

                viewSpecialParameters.ProvidePageLimitParameter(
                    parameters,
                    Paging.Always,
                    pageLimit,
                    false);

                viewSpecialParameters.ProvidePageOffsetParameter(
                    parameters,
                    Paging.Always,
                    currentPage,
                    pageLimit,
                    false);
            }

            // состояние заданий = "Новое"
            IViewParameterMetadata statusParam = tasksViewMetadata.Parameters.FindByName("Status");
            if (statusParam is not null)
            {
                parameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(statusParam)
                        .AddCriteria(
                            new EqualsCriteriaOperator(),
                            "$Cards_TaskStates_New",
                            Int32Boxes.Zero)
                        .AsRequestParameter());
            }

            // дата создания задания будет "позже", чем предыдущая запрошенная дата
            IViewParameterMetadata creationDateParam = tasksViewMetadata.Parameters.FindByName("CreationDate");
            if (creationDateParam is not null)
            {
                parameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(creationDateParam)
                        .AddCriteria(
                            new GreatThanCriteriaOperator(),
                            FormattingHelper.FormatDateTime(this.prevDateTime),
                            this.prevDateTime)
                        .AsRequestParameter());
            }

            // Функциональная роль автора
            IViewParameterMetadata functionRoleAuthorParam = tasksViewMetadata.Parameters.FindByName("FunctionRoleAuthorParam");
            if (functionRoleAuthorParam is not null)
            {
                parameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(functionRoleAuthorParam)
                        .AddCriteria(
                            new EqualsCriteriaOperator(),
                            "$Enum_FunctionRoles_Author",
                            CardFunctionRoles.AuthorID)
                        .AsRequestParameter());
            }

            // Функциональная роль исполнителя
            IViewParameterMetadata functionRolePerformerParam = tasksViewMetadata.Parameters.FindByName("FunctionRolePerformerParam");
            if (functionRolePerformerParam is not null)
            {
                parameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(functionRolePerformerParam)
                        .AddCriteria(
                            new EqualsCriteriaOperator(),
                            "$Enum_FunctionRoles_Performer",
                            CardFunctionRoles.PerformerID)
                        .AsRequestParameter());
            }

            // при следующей проверке заданий мы проверим дату относительно текущей
            // или относительно даты последнего задания
            this.prevDateTime = DateTime.UtcNow;

            tasksViewRequest.Values = parameters;

            // получаем данные представления
            ITessaViewResult viewResult;
            await using (SessionRequestTypeContext.Create(SessionRequestType.Background))
            {
                try
                {
                    viewResult = await this.tasksView.GetDataAsync(tasksViewRequest, cancellationToken).ConfigureAwait(false);
                }
                catch (ValidationException ex) when (ex.Result.Items.Any(i => i.ObjectType == nameof(SessionException)))
                {
                    await this.sessionFailedChecker
                        .NotifyOnSessionFailedAsync(cancellationToken)
                        .ConfigureAwait(false);
                    return;
                }
            }

            IList<object> viewResultRows = viewResult?.Rows;

            if (viewResult is null || viewResultRows is null || viewResultRows.Count == 0)
            {
                if (manualCheck)
                {
                    await this.notificationUIManager.ShowTextAsync(
                        "$KrNotifications_NoNewTasksAreAvailable",
                        "$KrNotifications_ClickTaskTextToolTip",
                        clickCommand: this.CreateOpenViewCommand(settings)).ConfigureAwait(false);
                }

                return;
            }

            // есть хотя бы одно задание
            string[] columns = (viewResult.Columns ?? Array.Empty<string>()).Cast<string>().ToArray();

            var indicesByName = new Dictionary<string, int>(columns.Length);
            for (int i = 0; i < columns.Length; i++)
            {
                indicesByName[columns[i]] = i;
            }

            int totalKnownTasksCount = viewResultRows.Count;
            var tasks = new List<TaskInfo>(totalKnownTasksCount);

            foreach (object rowValue in viewResultRows)
            {
                var row = new RowInfo(rowValue, indicesByName);

                DateTime created = row.GetDateTime("Created");
                if (this.prevDateTime < created)
                {
                    // чтобы повторно не затягивать задание, мы сдвигаем дату;
                    // чаще всего представление возвращаем время как Local, а мы сравниваем даты как UTC
                    this.prevDateTime = created.ToUniversalTime();
                }

                if (tasks.Count >= settings.NotificationMaxTasksToDisplay)
                {
                    // не добавляем уведомление по новому заданию, если заданий уже достаточное количество
                    continue;
                }

                var task = new TaskInfoModel((await this.generalMetadata.GetEnumerationsAsync(cancellationToken)).FunctionRoles)
                {
                    Flags = CardTaskFlags.CanPerform,
                    Digest = row.Get<string>("TaskInfo")?.Limit(350),
                    Planned = row.GetDateTime("PlannedDate"),
                    PlannedQuants = (int?) row.Get<long?>("QuantsToFinish"),
                    TypeCaption = row.Get<string>("TypeCaption"),
                    Created = created,
                    CreatedByID = row.Get<Guid>("CreatedByID"),
                    CreatedByName = row.Get<string>("CreatedByName"),
                    TimeZoneUtcOffsetMinutes = row.Get<int>("TimeZoneUtcOffsetMinutes"),
                    FormattedPlanned = row.Get<string>("TimeToCompletion")
                };

                Guid cardID = row.Get<Guid>("CardID");
                string cardDigest = row.Get<string>("CardName");
                string cardTypeCaption = row.Get<string>("CardTypeName");

                var taskRowID = row.Get<Guid>("TaskRowID");

                var taskAssignedRoles = await this.GetTaskAssignedRolesAsync(taskRowID, cancellationToken);
                if (taskAssignedRoles is null || taskAssignedRoles.Count == 0)
                {
                    return;
                }
                task.TaskAssignedRoles = taskAssignedRoles;

                var sessionRolesList = await this.GetSessionRolesListAsync(task.TaskAssignedRoles, taskRowID, cancellationToken);
                if (sessionRolesList is null || sessionRolesList.Count == 0)
                {
                    return;
                }
                task.TaskSessionRoles = sessionRolesList;

                tasks.Add(new TaskInfo(task, cardID, cardDigest, cardTypeCaption));
            }

            // генерируем уведомления по всем заданиям, а также в самом низу уведомление "у вас столько-то заданий",
            // по которому можно провалиться в представление

            var notifications = new List<INotificationViewModel>(tasks.Count + 1);
            TimeSpan duration = settings.NotificationDuration;

            notifications.Add(
                await this.notificationUIFactory.CreateTextAsync(
                    totalKnownTasksCount < pageLimit || pageLimit < 0
                        ? string.Format(await LocalizationManager.GetStringAsync("KrNotifications_AvailableTasksMessage", cancellationToken), totalKnownTasksCount)
                        : string.Format(await LocalizationManager.GetStringAsync("KrNotifications_AvailableTasksMessage_TooMany", cancellationToken), pageLimit),
                    "$KrNotifications_ClickTaskTextToolTip",
                    duration,
                    this.CreateOpenViewCommand(settings),
                    autoClose: true, cancellationToken: cancellationToken).ConfigureAwait(false));

            foreach (TaskInfo task in tasks)
            {
                notifications.Add(
                    await this.notificationUIFactory.CreateTaskAsync(
                        task.Model,
                        "$KrNotifications_ClickTaskToolTip",
                        task.CardDigest,
                        task.CardTypeCaption,
                        duration,
                        this.notificationUIFactory.OpenCardCommand(task.CardID), cancellationToken).ConfigureAwait(false));
            }

            notifications.Add(
                await this.notificationUIFactory.CreateHideAllAsync(duration, cancellationToken).ConfigureAwait(false));

            // здесь собственно отправка уведомлений
            await this.notificationUIManager.ShowAsync(notifications.ToArray()).ConfigureAwait(false);
        }

        private async Task<List<CardTaskSessionRole>> GetSessionRolesListAsync(
            IReadOnlyCollection<CardTaskAssignedRole> taskAssignedRoles,
            Guid taskRowID,
            CancellationToken cancellationToken)
        {
            IViewMetadata sessionRolesViewMetadata = await this.cardTaskSessionRolesView.GetMetadataAsync(cancellationToken).ConfigureAwait(false);
            ITessaViewRequest functionRolesViewRequest = new TessaViewRequest(sessionRolesViewMetadata) { CalculateRowCounting = false };

            var functionRolesRequestParameters = new List<RequestParameter>();

            IViewParameterMetadata taskRowIDParam = sessionRolesViewMetadata.Parameters.FindByName("TaskRowID");
            if (taskRowIDParam is not null)
            {
                functionRolesRequestParameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(taskRowIDParam)
                        .AddCriteria(
                            new EqualsCriteriaOperator(),
                            taskRowID.ToString(),
                            taskRowID)
                        .AsRequestParameter());
            }

            functionRolesViewRequest.Values = functionRolesRequestParameters;

            // получаем данные представления
            ITessaViewResult functionRolesViewResult;
            await using (SessionRequestTypeContext.Create(SessionRequestType.Background))
            {
                functionRolesViewResult = await this.cardTaskSessionRolesView
                    .GetDataAsync(functionRolesViewRequest, cancellationToken).ConfigureAwait(false);
            }

            IList<object> functionRolesViewResultRows = functionRolesViewResult?.Rows;

            if (functionRolesViewResult is null || functionRolesViewResultRows is null ||
                functionRolesViewResultRows.Count == 0)
            {
                return null;
            }

            // есть хотя бы одно задание
            string[] functionRolesColumns = (functionRolesViewResult.Columns ?? Array.Empty<string>()).Cast<string>().ToArray();

            var functionRolesIndicesByName = new Dictionary<string, int>(functionRolesColumns.Length);
            for (int i = 0; i < functionRolesColumns.Length; i++)
            {
                functionRolesIndicesByName[functionRolesColumns[i]] = i;
            }

            int totalKnownFunctionRolesCount = functionRolesViewResultRows.Count;

            var sessionFunctionRolesList = new List<CardTaskSessionRole>(totalKnownFunctionRolesCount);

            foreach (object rowValue in functionRolesViewResultRows)
            {
                var row = new RowInfo(rowValue, functionRolesIndicesByName);

                var taskAssignedRole = taskAssignedRoles.FirstOrDefault(p => p.RowID == row.Get<Guid>("TaskRoleRowID"));
                if (taskAssignedRole is null)
                {
                    continue;
                }

                if (taskAssignedRole.ParentRowID is not null)
                {
                    taskAssignedRole = GetTaskAssignedRolesParent(taskAssignedRoles, taskAssignedRole);
                }

                sessionFunctionRolesList.Add(
                    new CardTaskSessionRole(
                        new Dictionary<string, object>())
                    {
                        FunctionRoleID = taskAssignedRole.TaskRoleID,
                        TaskRoleRowID = taskAssignedRole.RowID,
                        IsDeputy = row.Get<bool>("IsDeputy")
                    });
            }

            return sessionFunctionRolesList;
        }

        private static CardTaskAssignedRole GetTaskAssignedRolesParent(IReadOnlyCollection<CardTaskAssignedRole> taskAssignedRoles, CardTaskAssignedRole taskAssignedRole)
        {
            var currentParent = taskAssignedRoles.FirstOrDefault(p => p.RowID == taskAssignedRole.ParentRowID);
            if (currentParent is null)
            {
                throw new InvalidOperationException(
                    $"Can't find parent for TaskAssignedRole with RowID = {taskAssignedRole.RowID}.");
            }

            return
                !currentParent.ParentRowID.HasValue
                    ? currentParent
                    : GetTaskAssignedRolesParent(taskAssignedRoles, currentParent);
        }

        private async Task<List<CardTaskAssignedRole>> GetTaskAssignedRolesAsync(Guid taskRowID, CancellationToken cancellationToken)
        {
            IViewMetadata taskAssignedRolesViewMetadata = await this.cardTaskAssignedRolesView.GetMetadataAsync(cancellationToken).ConfigureAwait(false);
            ITessaViewRequest taskAssignedRolesViewRequest = new TessaViewRequest(taskAssignedRolesViewMetadata) { CalculateRowCounting = false };

            var taskAssignedRolesRequestParameters = new List<RequestParameter>();

            IViewParameterMetadata taskRowIDParam = taskAssignedRolesViewMetadata.Parameters.FindByName("TaskRowID");
            if (taskRowIDParam is not null)
            {
                taskAssignedRolesRequestParameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(taskRowIDParam)
                        .AddCriteria(
                            new EqualsCriteriaOperator(),
                            taskRowID.ToString(),
                            taskRowID)
                        .AsRequestParameter());
            }

            taskAssignedRolesViewRequest.Values = taskAssignedRolesRequestParameters;

            ITessaViewResult taskAssignedRolesViewResult;
            await using (SessionRequestTypeContext.Create(SessionRequestType.Background))
            {
                taskAssignedRolesViewResult = await this.cardTaskAssignedRolesView
                    .GetDataAsync(taskAssignedRolesViewRequest, cancellationToken).ConfigureAwait(false);
            }

            IList<object> taskAssignedRolesViewResultRows = taskAssignedRolesViewResult?.Rows;

            if (taskAssignedRolesViewResult is null || taskAssignedRolesViewResultRows is null ||
                taskAssignedRolesViewResultRows.Count == 0)
            {
                return null;
            }

            // есть хотя бы одно задание
            string[] taskAssignedRolesColumns = (taskAssignedRolesViewResult.Columns ?? Array.Empty<string>()).Cast<string>().ToArray();

            var taskAssigmedRolesIndicesByName = new Dictionary<string, int>(taskAssignedRolesColumns.Length);
            for (int i = 0; i < taskAssignedRolesColumns.Length; i++)
            {
                taskAssigmedRolesIndicesByName[taskAssignedRolesColumns[i]] = i;
            }

            int totalKnownTaskAssignedRolesCount = taskAssignedRolesViewResultRows.Count;

            var taskAssignedRolesList = new List<CardTaskAssignedRole>(totalKnownTaskAssignedRolesCount);

            foreach (object rowValue in taskAssignedRolesViewResultRows)
            {
                var row = new RowInfo(rowValue, taskAssigmedRolesIndicesByName);

                taskAssignedRolesList.Add(
                    new CardTaskAssignedRole(
                        new Dictionary<string, object>())
                    {
                        RowID = row.Get<Guid>("AssignedRoleRowID"),
                        TaskRoleID = row.Get<Guid>("AssignedRoleTaskRoleID"),
                        RoleID = row.Get<Guid>("AssignedRoleRoleID"),
                        RoleName = row.Get<string>("RoleName"),
                        RoleTypeID = row.Get<Guid>("RoleTypeID"),
                        Position = row.Get<string>("Position"),
                        ParentRowID = row.Get<Guid?>("ParentRowID"),
                        Master = row.Get<bool>("Master"),
                        ShowInTaskDetails = row.Get<bool>("ShowInTaskDetails"),
                    });

            }

            return taskAssignedRolesList;
        }

        #endregion

        #region IKrNotificationManager Members

        /// <summary>
        /// Подготавливаем инфраструктуру для периодического затягивания информации по новым заданиям.
        /// При этом сам запрос <see cref="CheckTasksAsync"/> выполнять не требуется.
        /// </summary>
        public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            KrSettings settings = await this.settingsLazy.GetValueAsync(cancellationToken).ConfigureAwait(false);

            if ((this.tasksView = await this.viewService.GetByNameAsync(settings.NotificationViewAlias, cancellationToken).ConfigureAwait(false)) is null ||
                (this.cardTaskSessionRolesView = await this.viewService.GetByNameAsync("CardTaskSessionRoles", cancellationToken).ConfigureAwait(false)) is null ||
                (this.cardTaskAssignedRolesView = await this.viewService.GetByNameAsync("TaskAssignedRoles", cancellationToken).ConfigureAwait(false)) is null)
            {
                // у пользователя нет доступа к одному из необходимых для работы представлений, или нет самого представления
                return;
            }

            // при первом запуске загружаем задания за последний час
            this.prevDateTime = DateTime.UtcNow
                .AddTicks(-settings.NotificationIntervalToGetTasksAfterInitialization.Ticks);

            this.canFetch = true;
        }


        /// <summary>
        /// Освобождает инфраструктуру для периодического затягивания информации по новым заданиям.
        /// </summary>
        public ValueTask ShutdownAsync(CancellationToken cancellationToken = default)
        {
            this.canFetch = false;
            return new ValueTask();
        }


        /// <summary>
        /// Возвращает признак того, что уведомления по заданиям включены.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если уведомления по заданиям включены;
        /// <c>false</c> в противном случае.
        /// </returns>
        public ValueTask<bool> CanCheckTasksAsync(CancellationToken cancellationToken = default) =>
            new ValueTask<bool>(
                this.canFetch
                && !this.isFetchingNow
                && !this.notificationUIManager.IsMuted()
                && !this.userSettings.TryGet<bool>("KrUserSettingsVirtual", "DisableTaskPopupNotifications"));


        /// <summary>
        /// Проверяет новые задания и отображает уведомления, если они есть.
        /// Метод вызывается в потоке UI, но фактическое отображение должно быть асинхронное.
        /// </summary>
        /// <param name="manualCheck">
        /// Признак того, что проверка выполняется вручную. При этом на экране отображаются дополнительные сообщения.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task CheckTasksAsync(bool manualCheck = false, CancellationToken cancellationToken = default)
        {
            if (!await this.CanCheckTasksAsync(cancellationToken).ConfigureAwait(false)
                || DateTime.UtcNow - this.prevDateTime < minCheckInterval
                || await this.sessionFailedChecker.IsCurrentSessionFailedAsync(cancellationToken).ConfigureAwait(false))
            {
                return;
            }

            if (!this.canFetch || this.isFetchingNow)
            {
                return;
            }

            using (await this.asyncLock.EnterAsync(cancellationToken).ConfigureAwait(false))
            {
                if (!this.canFetch
                    || this.isFetchingNow
                    || DateTime.UtcNow - this.prevDateTime < minCheckInterval)
                {
                    return;
                }

                this.isFetchingNow = true;

                try
                {
                    await this.CheckTasksCoreAsync(manualCheck, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);

                    // выводим уведомление об ошибке, причём уведомление быстроисчезающее

                    try
                    {
                        await this.notificationUIManager.ShowAsync(
                            await this.notificationUIFactory.CreateTextAsync(
                                "$KrNotifications_ErrorOccuredWhenLoadingTasks",
                                clickCommand: new DelegateCommand(p => TessaDialog.ShowException(ex)))
                                .ConfigureAwait(false))
                            .ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex2)
                    {
                        // ошибку при выводе уведомления логируем
                        logger.LogException(ex2);
                    }
                }
                finally
                {
                    this.isFetchingNow = false;
                }
            }
        }

        #endregion

        #region IDisposable Members

        /// <doc path='info[@type="IDisposable" and @item="Dispose"]'/>
        public void Dispose() => this.Dispose(true);

        /// <doc path='info[@type="IDisposable" and @item="IsDisposed"]'/>
        protected bool IsDisposed { get; private set; }

        /// <doc path='info[@type="IDisposable" and @item="Dispose:disposing"]'/>
        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.asyncLock.Dispose();
            this.IsDisposed = true;
        }

        #endregion
    }
}
