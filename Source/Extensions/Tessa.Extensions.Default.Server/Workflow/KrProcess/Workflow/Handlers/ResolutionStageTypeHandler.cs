#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="Shared.Workflow.KrProcess.StageTypeDescriptors.ResolutionDescriptor"/>.
    /// </summary>
    public class ResolutionStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResolutionStageTypeHandler"/>.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardMetadata">Метаинформация, необходимую для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="cardGetStrategy">Стратегия загрузки карточки.</param>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="tokenProvider">Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="session">Сессия пользователя.</param>
        public ResolutionStageTypeHandler(
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            ICardGetStrategy cardGetStrategy,
            IKrScope krScope,
            IKrTokenProvider tokenProvider,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            ISession session)
        {
            this.CardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.CardMetadata = cardMetadata ?? throw new ArgumentNullException(nameof(cardMetadata));
            this.CardGetStrategy = cardGetStrategy ?? throw new ArgumentNullException(nameof(cardGetStrategy));
            this.KrScope = krScope ?? throw new ArgumentNullException(nameof(krScope));
            this.TokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            this.RoleGetStrategy = roleGetStrategy ?? throw new ArgumentNullException(nameof(roleGetStrategy));
            this.ContextRoleManager = contextRoleManager ?? throw new ArgumentNullException(nameof(contextRoleManager));
            this.Session = session;
        }

        #endregion

        #region Protected Properties and Constants

        /// <summary>
        /// Имя ключа, по которому в <see cref="KrObjectModel.Stage.InfoStorage"/> содержится идентификатор первого задания бизнес процесса <see cref="WfHelper.ResolutionProcessName"/>. Тип значения: <see cref="Guid"/>.
        /// </summary>
        protected const string TaskRowID = nameof(TaskRowID);

        /// <summary>
        /// Возвращает или задаёт репозиторий для управления карточками.
        /// </summary>
        protected ICardRepository CardRepository { get; set; }

        /// <summary>
        /// Возвращает или задаёт метаинформацию, необходимую для использования типов карточек совместно с пакетом карточек.
        /// </summary>
        protected ICardMetadata CardMetadata { get; set; }

        /// <summary>
        /// Возвращает или задаёт стратегию загрузки карточки.
        /// </summary>
        protected ICardGetStrategy CardGetStrategy { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект, обеспечивающий создание и валидацию токена безопасности для типового решения.
        /// </summary>
        protected IKrTokenProvider TokenProvider { get; set; }

        /// <summary>
        /// Стратегия для получения информации о ролях.
        /// </summary>
        protected IRoleGetStrategy RoleGetStrategy { get; set; }

        /// <summary>
        /// Возвращает или задаёт репозиторий для управления ролевой моделью.
        /// </summary>
        protected IContextRoleManager ContextRoleManager { get; set; }

        /// <summary>
        /// Возвращает или задаёт сессию пользователя.
        /// </summary>
        protected ISession Session { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает отправителя для типовой задачи.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="senderID">Идентификатор роли отправителя типовой задачи, указанный в настройках этапа.</param>
        /// <returns>Отправитель типовой задачи.</returns>
        private static async Task<Author?> GetStageSenderAsync(
            IStageTypeHandlerContext context,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            Guid senderID)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(roleGetStrategy, nameof(roleGetStrategy));
            Check.ArgumentNotNull(contextRoleManager, nameof(contextRoleManager));

            var role = await roleGetStrategy.GetRoleParamsAsync(senderID, context.CancellationToken);
            if (role.Type is null)
            {
                context.ValidationResult.AddError("$KrProcess_ErrorMessage_AuthorRoleIsntFound");
                return null;
            }

            switch (role.Type)
            {
                case RoleType.Personal:
                    return new Author(senderID, role.Name);

                case RoleType.Context:
                    var mainCardID = context.MainCardID;
                    if (!mainCardID.HasValue)
                    {
                        context.ValidationResult.AddError("$KrProcess_ErrorMessage_ContextRoleRequiresCard");
                        return null;
                    }

                    var contextRole = await contextRoleManager.GetContextRoleAsync(senderID, context.CancellationToken);

                    var users = await contextRoleManager.GetCardContextUsersAsync(contextRole, mainCardID.Value, cancellationToken: context.CancellationToken);
                    if (users.Count > 0)
                    {
                        return new Author(users[0].UserID, users[0].UserName);
                    }
                    context.ValidationResult.AddError("$KrProcess_ErrorMessage_ContextRoleIsEmpty");
                    return null;

                default:
                    context.ValidationResult.AddError("$KrProcess_ErrorMessage_OnlyPersonalAndContextRoles");
                    return null;
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var performers = context.Stage.Performers;
            if (performers.Count == 0)
            {
                return StageHandlerResult.CompleteResult;
            }

            if (!(context.CardExtensionContext is ICardStoreExtensionContext storeContext))
            {
                return StageHandlerResult.EmptyResult;
            }

            var initiator = context.WorkflowProcess.Author;
            var overridenAuthor = context.Stage.Author;

            var settings = context.Stage.SettingsStorage;
            var senderRoleID = settings.TryGet<Guid?>(KrResolutionSettingsVirtual.SenderID);
            Author? sender;
            Author? author;
            if (senderRoleID is not null)
            {
                sender = await GetStageSenderAsync(
                    context, 
                    this.RoleGetStrategy, 
                    this.ContextRoleManager,
                    senderRoleID.Value);
                if (sender is null)
                {
                    return StageHandlerResult.EmptyResult;
                }
            }
            else
            {
                sender = 
                    initiator ?? new Author(this.Session.User.ID, this.Session.User.Name);
            }

            if (overridenAuthor is null)
            {
                author = sender;
            }
            else
            {
                author = await HandlerHelper.GetStageAuthorAsync(context, this.RoleGetStrategy, this.ContextRoleManager, this.Session);
                if (author is null)
                {
                    return StageHandlerResult.EmptyResult;
                }
            }

            var authorID = author.AuthorID;
            var authorName = author.AuthorName;
            var senderID = sender.AuthorID;
            var senderName = sender.AuthorName;
            var task = new CardTask { TypeID = DefaultTaskTypes.WfResolutionProjectTypeID };
            var resolutionFields = task.Card.Sections.GetOrAddEntry(WfHelper.ResolutionSection).Fields;
            resolutionFields[WfHelper.ResolutionKindIDField] = settings.TryGet<object>(KrResolutionSettingsVirtual.KindID);
            resolutionFields[WfHelper.ResolutionKindCaptionField] = settings.TryGet<object>(KrResolutionSettingsVirtual.KindCaption);
            resolutionFields[WfHelper.ResolutionAuthorIDField] = authorID;
            resolutionFields[WfHelper.ResolutionAuthorNameField] = authorName;
            resolutionFields[WfHelper.ResolutionSenderIDField] = senderID;
            resolutionFields[WfHelper.ResolutionSenderNameField] = senderName;
            resolutionFields[WfHelper.ResolutionControllerIDField] = settings.TryGet<object>(KrResolutionSettingsVirtual.ControllerID);
            resolutionFields[WfHelper.ResolutionControllerNameField] = settings.TryGet<object>(KrResolutionSettingsVirtual.ControllerName);
            resolutionFields[WfHelper.ResolutionCommentField] = settings.TryGet<object>(KrResolutionSettingsVirtual.Comment);
            resolutionFields[WfHelper.ResolutionPlannedField] = settings.TryGet<object>(KrResolutionSettingsVirtual.Planned);
            resolutionFields[WfHelper.ResolutionDurationInDaysField] = settings.TryGet<object>(KrResolutionSettingsVirtual.DurationInDays);
            resolutionFields[WfHelper.ResolutionWithControlField] = settings.TryGet<object>(KrResolutionSettingsVirtual.WithControl);
            resolutionFields[WfHelper.ResolutionMassCreationField] = settings.TryGet<object>(KrResolutionSettingsVirtual.MassCreation);
            resolutionFields[WfHelper.ResolutionMajorPerformerField] = settings.TryGet<object>(KrResolutionSettingsVirtual.MajorPerformer);

            var performerRows = task.Card.Sections.GetOrAddTable(WfHelper.ResolutionPerformersSection).Rows;
            for (var i = 0; i < performers.Count; i++)
            {
                var performerRow = performerRows.Add();
                performerRow.RowID = Guid.NewGuid();
                performerRow.State = CardRowState.Inserted;

                var performer = performers[i];
                performerRow[WfHelper.ResolutionPerformerRoleIDField] = performer.PerformerID;
                performerRow[WfHelper.ResolutionPerformerRoleNameField] = performer.PerformerName;
                performerRow[WfHelper.ResolutionPerformerOrderField] = Int32Boxes.Box(i);
            }

            var card = await this.KrScope.GetMainCardAsync(storeContext.Request.Card.ID,
                context.ValidationResult);

            if (card is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            card = card.Clone();
            
            // Дайджест нужно получить до очистки секций и остальных полей карточки
            var digest = await this.CardRepository.GetDigestAsync(card, CardDigestEventNames.ActionHistoryStoreRouteCreateCard, context.CancellationToken);

            var token = this.TokenProvider.CreateFullToken(card);

            card.Sections.Clear();
            card.Files.Clear();
            card.Tasks.Clear();
            card.TaskHistory.Clear();
            card.TaskHistoryGroups.Clear();
            card.Info.Clear();
            card.Permissions.Clear();
            token.Set(card.Info);

            var historyGroupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);
            var rowID = Guid.NewGuid();
            var storeRequest = new CardStoreRequest { Card = card }
                .SetStartingProcessTaskGroupRowID(historyGroupID)
                .SetStartingProcessNextTask(task)
                .SetStartingProcessName(WfHelper.ResolutionProcessName)
                .SetStartingProcessTaskRowID(rowID);
            if (digest is not null)
            {
                storeRequest.SetDigest(digest); 
            }
            var response = await this.CardRepository.StoreAsync(storeRequest,
                context.CancellationToken);

            context.ValidationResult.Add(response.ValidationResult);
            if (!response.ValidationResult.IsSuccessful())
            {
                return StageHandlerResult.EmptyResult;
            }

            var process = context.ProcessInfo;
            var scope = context.CardExtensionContext.DbScope!;
            var db = scope.Db;

            var settingsJson =
                await db
                    .SetCommand(
                        scope.BuilderFactory
                            .Select()
                                .C("th", "Settings")
                            .From("TaskHistory", "th").NoLock()
                            .Where()
                                .C("th", "RowID").Equals().P("RowID")
                            .Build(),
                        db.Parameter("RowID", rowID))
                    .LogCommand()
                    .ExecuteStringAsync(context.CancellationToken);

            var historyItemSettings = 
                (string.IsNullOrEmpty(settingsJson)
                    ? null
                    : StorageHelper.DeserializeFromTypedJson(settingsJson))
                ?? new Dictionary<string, object?>(StringComparer.Ordinal); 

            historyItemSettings[TaskHistorySettingsKeys.ProcessID] = process.ProcessID;
            historyItemSettings[TaskHistorySettingsKeys.ProcessKind] = process.ProcessTypeName;

            await db
                .SetCommand(
                    scope.BuilderFactory
                        .Update("TaskHistory")
                            .C("Settings").Assign().P("Settings")
                        .Where()
                            .C("RowID").Equals().P("RowID")
                        .Build(),
                    db.Parameter("RowID", rowID, DataType.Guid),
                    db.Parameter("Settings", StorageHelper.SerializeToTypedJson(historyItemSettings), DataType.BinaryJson))
                .LogCommand()
                .ExecuteNonQueryAsync(context.CancellationToken);

            context.Stage.InfoStorage[TaskRowID] = rowID;
            return StageHandlerResult.InProgressResult;
        }

        /// <inheritdoc/>
        public override async Task<bool> HandleStageInterruptAsync(IStageTypeHandlerContext context)
        {
            var storeContext = (ICardStoreExtensionContext) context.CardExtensionContext;
            var scope = storeContext.DbScope!;
            await using (scope.Create())
            {
                var db = scope.Db;
                var tasksToRevoke = await db
                    .SetCommand(
                        scope.BuilderFactory
                            .With("ChildTaskHistory", e => e
                                    .Select().C("t", "RowID")
                                    .From("TaskHistory", "t").NoLock()
                                    .Where()
                                        .C("t", "ParentRowID").Equals().P("RootRowID")
                                    .UnionAll()
                                    .Select().C("t", "RowID")
                                    .From("TaskHistory", "t").NoLock()
                                    .InnerJoin("ChildTaskHistory", "p")
                                        .On().C("p", "RowID").Equals().C("t", "ParentRowID"),
                                columnNames: new[] { "RowID" },
                                recursive: true)
                            .Select().C("t", "RowID")
                            .From("Tasks", "t").NoLock()
                            .InnerJoin("ChildTaskHistory", "h")
                                .On().C("h", "RowID").Equals().C("t", "RowID")
                            .Build(),
                        db.Parameter("RootRowID", context.Stage.InfoStorage.Get<Guid>(TaskRowID), DataType.Guid))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(context.CancellationToken);

                if (tasksToRevoke.Count == 0)
                {
                    return true;
                }

                var tasksToLoad = tasksToRevoke;

                var card = await this.KrScope.GetMainCardAsync(
                    storeContext.Request.Card.ID,
                    cancellationToken: context.CancellationToken);

                if (card is null)
                {
                    return false;
                }

                var cardTasks = card.TryGetTasks();
                if (cardTasks is not null)
                {
                    tasksToLoad = new List<Guid>(tasksToLoad);

                    foreach (var cardTask in cardTasks)
                    {
                        tasksToLoad.Remove(cardTask.RowID);
                    }
                }

                if (tasksToLoad.Count > 0)
                {
                    var taskContexts = await this.CardGetStrategy.TryLoadTaskInstancesAsync(
                        card.ID, card, db, this.CardMetadata, context.ValidationResult, storeContext.Session,
                        getTaskMode: CardGetTaskMode.All, loadCalendarInfo: false, taskRowIDList: tasksToLoad);
                    foreach (var taskContext in taskContexts ?? Enumerable.Empty<CardGetContext>())
                    {
                        await this.CardGetStrategy.LoadSectionsAsync(taskContext, context.CancellationToken);
                    }
                }

                cardTasks = card.TryGetTasks();
                if (cardTasks is null)
                {
                    return true;
                }

                foreach (var taskToRevoke in cardTasks)
                {
                    if (tasksToRevoke.Contains(taskToRevoke.RowID))
                    {
                        taskToRevoke.Action = CardTaskAction.Complete;
                        taskToRevoke.State = CardRowState.Deleted;
                        taskToRevoke.Flags = taskToRevoke.Flags & ~CardTaskFlags.Locked | CardTaskFlags.UnlockedForPerformer | CardTaskFlags.HistoryItemCreated;
                        taskToRevoke.Result = "$ApprovalHistory_TaskCancelled";
                        taskToRevoke.OptionID = DefaultCompletionOptions.Cancel;

                        var resolutionFields = taskToRevoke.Card.Sections.GetOrAddEntry(WfHelper.ResolutionSection).Fields;
                        resolutionFields[WfHelper.ResolutionRevokeChildrenField] = BooleanBoxes.False;
                    }
                }

                // Всегда true, поскольку задачи находятся в процессе WfHelper.ResolutionProcessName.
                return true;
            }
        }

        /// <inheritdoc/>
        public override Task<StageHandlerResult> HandleSignalAsync(IStageTypeHandlerContext context)
        {
            var signal = context.SignalInfo;
            if (signal?.Signal?.Name == KrPerformSignal)
            {
                return Task.FromResult(StageHandlerResult.CompleteResult);
            }

            return Task.FromResult(StageHandlerResult.EmptyResult);
        }

        #endregion
    }
}
