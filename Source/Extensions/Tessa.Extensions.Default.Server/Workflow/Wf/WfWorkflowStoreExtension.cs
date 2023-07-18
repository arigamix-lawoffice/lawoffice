using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Extensions.Templates;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    /// <summary>
    /// Расширение, выполняющее взаимодействие с бизнес-процессом Workflow при сохранении карточки.
    /// </summary>
    public class WfWorkflowStoreExtension :
        WorkflowStoreExtension
    {
        #region Constructors

        public WfWorkflowStoreExtension(
            KrSettingsLazy settingsLazy,
            IKrTypesCache krTypesCache,
            IKrTokenProvider krTokenProvider,
            ICardCache cardCache,
            IRoleGetStrategy roleGetStrategy,
            ICardContextRoleCache contextRoleCache,
            IKrPermissionsManager permissionsManager,
            [Dependency(CardRepositoryNames.Default)] ICardRepository cardRepositoryToCreateNextRequest,
            ICardRepository cardRepositoryToStoreNextRequest,
            ICardRepository cardRepositoryToCreateTasks,
            ICardRepository cardRepositoryToDeleteSatellite,
            ICardRepository cardRepositoryToCreateAndStoreSatellite,
            ICardTaskHistoryManager taskHistoryManager,
            ICardGetStrategy cardGetStrategy,
            [Dependency(NotificationManagerNames.DeferredWithoutTransaction)] INotificationManager notificationManager,
            IWorkflowQueueProcessor workflowQueueProcessor)
            : base(
                cardRepositoryToCreateNextRequest,
                cardRepositoryToStoreNextRequest,
                cardRepositoryToCreateTasks,
                taskHistoryManager,
                cardGetStrategy,
                workflowQueueProcessor)
        {
            this.settingsLazy = settingsLazy;
            this.krTypesCache = krTypesCache;
            this.krTokenProvider = krTokenProvider;
            this.cardCache = cardCache;
            this.roleGetStrategy = roleGetStrategy;
            this.contextRoleCache = contextRoleCache;
            this.permissionsManager = permissionsManager;
            this.cardRepositoryToCreateAndStoreSatellite = cardRepositoryToCreateAndStoreSatellite;
            this.cardRepositoryToDeleteSatellite = cardRepositoryToDeleteSatellite;
            this.notificationManager = notificationManager;
        }

        #endregion

        #region Fields

        private List<Guid> historyItemsToDeleteRowIDList;   // = null

        private readonly KrSettingsLazy settingsLazy;

        private readonly IKrTypesCache krTypesCache;

        private readonly IKrTokenProvider krTokenProvider;

        private readonly ICardCache cardCache;

        private readonly IRoleGetStrategy roleGetStrategy;

        private readonly ICardContextRoleCache contextRoleCache;

        private readonly INotificationManager notificationManager;

        private readonly IKrPermissionsManager permissionsManager;

        private readonly ICardRepository cardRepositoryToCreateAndStoreSatellite;

        private readonly ICardRepository cardRepositoryToDeleteSatellite;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Проверяет, что в заданном контексте <see cref="IWorkflowContext"/> задан идентификатор
        /// карточки-сателлита Workflow. Если он там отсутствует, то создаётся и сохраняется сателлит,
        /// а затем его идентификатор устанавливается в контексте. Возвращает признак того,
        /// что метод выполнен успешно, и в контексте действительно присутствует карточка-сателлит.
        /// </summary>
        /// <param name="context">
        /// Контекст, в котором должен быть установлен идентификатор карточки-сателлита Workflow.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если идентификатор уже установлен в контексте,
        /// или он был установлен после создания и сохранения карточки-сателлита;
        /// <c>false</c>, если идентификатор не был установлен, и в процессе создания или сохранения
        /// карточки-сателлита возникли ошибки, которые были сохранены в контексте.
        /// </returns>
        protected virtual async Task<bool> EnsureSatelliteExistsAsync(IWorkflowContext context, CancellationToken cancellationToken = default)
        {
            Guid? satelliteID = WfHelper.TryGetSatelliteID(context);
            if (satelliteID.HasValue)
            {
                // сателлит уже был создан
                return true;
            }

            // сателлита нет, поэтому его надо создать
            // мы находимся в транзакции и блокировке на запись для основной карточки

            var newRequest = new CardNewRequest { CardTypeID = DefaultCardTypes.WfSatelliteTypeID };
            CardNewResponse newResponse = await this.cardRepositoryToCreateAndStoreSatellite.NewAsync(newRequest, cancellationToken);

            ValidationResult newResponseResult = newResponse.ValidationResult.Build();
            context.ValidationResult.Add(newResponseResult);
            if (!newResponseResult.IsSuccessful)
            {
                return false;
            }

            Card satellite = newResponse.Card;
            Guid newSatelliteID = Guid.NewGuid();
            satellite.ID = newSatelliteID;

            Guid mainCardID = context.NextRequest.Card.ID;
            WfHelper.SetSatelliteMainCardID(satellite, mainCardID);
            CardSatelliteHelper.SetupUniversalSatellite(satellite, mainCardID);

            satellite.RemoveAllButChanged(CardStoreMode.Insert);
            var storeRequest = new CardStoreRequest { Card = satellite };
            CardStoreResponse storeResponse = await this.cardRepositoryToCreateAndStoreSatellite.StoreAsync(storeRequest, cancellationToken);

            ValidationResult storeResponseResult = storeResponse.ValidationResult.Build();
            context.ValidationResult.Add(storeResponseResult);
            if (!storeResponseResult.IsSuccessful)
            {
                return false;
            }

            // только когда карточка-сателлит успешно создана и сохранена,
            // её идентификатор устанавливается в контексте
            WfHelper.SetSatelliteID(context, newSatelliteID);
            return true;
        }


        /// <summary>
        /// Завершает задание резолюции Workflow и возвращает результат задания
        /// или <c>null</c>, если результат задания не удалось получить.
        /// </summary>
        /// <param name="taskOptionID">Идентификатор варианта завершения резолюции.</param>
        /// <param name="taskTypeID">Идентификатор типа задания резолюции.</param>
        /// <param name="task">Задание резолюции.</param>
        /// <returns>
        /// Результат задания или <c>null</c>, если результат не удалось получить.
        /// </returns>
        protected virtual string CompleteResolutionAndGetResult(
            Guid taskOptionID,
            Guid taskTypeID,
            CardTask task)
        {
            // в используемых строках локализации хранятся локализуемые строки формата вида {$String}
            // поэтому мы их как бы "локализуем" сразу, а на самом деле локализация производится при отображении

            string result;
            if (taskOptionID == DefaultCompletionOptions.Cancel)
            {
                if (taskTypeID == DefaultTaskTypes.WfResolutionProjectTypeID)
                {
                    // задание будет удалено вместе с записью в историю действий,
                    // т.к. проект резолюции мог быть создан случайно

                    this.DeferTaskHistoryRemovalUntilTransaction(task.RowID);
                }

                result = LocalizationManager.GetString("WfResolution_Result_Cancelled");
            }
            else if (taskOptionID == DefaultCompletionOptions.Revoke)
            {
                result = LocalizationManager.GetString("WfResolution_Result_Revoked");
            }
            else if (taskOptionID == DefaultCompletionOptions.SendToPerformer)
            {
                CardRow[] performersRows = WfHelper.TryGetPerformers(task);
                if (performersRows is null)
                {
                    return null;
                }

                string performers = string.Join(
                    "; ",
                    performersRows
                        .Select(x =>
                            LocalizationManager.Localize(
                                x.Get<string>(WfHelper.ResolutionPerformerRoleNameField))));

                Dictionary<string, object> resolutionFields = task.Card.Sections[WfHelper.ResolutionSection].RawFields;
                string comment = resolutionFields.Get<string>(WfHelper.ResolutionCommentField).NormalizeComment();
                bool massCreation = performersRows.Length > 1 && resolutionFields.Get<bool>(WfHelper.ResolutionMassCreationField);
                if (massCreation)
                {
                    bool withControl = resolutionFields.Get<bool>(WfHelper.ResolutionWithControlField);
                    if (withControl)
                    {
                        result = string.IsNullOrEmpty(comment)
                            ? string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithMassCreationAndWithControlAndWithoutComment"),
                                performers)
                            : string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithMassCreationAndWithControlAndWithComment"),
                                performers,
                                comment);
                    }
                    else
                    {
                        result = string.IsNullOrEmpty(comment)
                            ? string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithMassCreationAndWithoutComment"),
                                performers)
                            : string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithMassCreationAndWithComment"),
                                performers,
                                comment);
                    }
                }
                else
                {
                    bool withControl = resolutionFields.Get<bool>(WfHelper.ResolutionWithControlField);
                    if (withControl)
                    {
                        result = string.IsNullOrEmpty(comment)
                            ? string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithControlAndWithoutComment"),
                                performers)
                            : string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithControlAndWithComment"),
                                performers,
                                comment);
                    }
                    else
                    {
                        result = string.IsNullOrEmpty(comment)
                            ? string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithoutControlAndWithoutComment"),
                                performers)
                            : string.Format(
                                LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_SentToPerformerWithoutControlAndWithComment"),
                                performers,
                                comment);
                    }
                }
            }
            else if (taskOptionID == DefaultCompletionOptions.Complete)
            {
                Dictionary<string, object> resolutionFields = task.Card.Sections[WfHelper.ResolutionSection].RawFields;
                string comment = resolutionFields.Get<string>(WfHelper.ResolutionCommentField).NormalizeComment();

                result = string.IsNullOrEmpty(comment)
                    ? LocalizationManager.GetString("WfResolution_Result_CompletedWithoutComment")
                    : string.Format(LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_Completed"), comment);
            }
            else
            {
                return null;
            }

            if (taskTypeID == DefaultTaskTypes.WfResolutionChildTypeID)
            {
                Dictionary<string, object> resolutionFields = task.Card.Sections[WfHelper.ResolutionSection].RawFields;
                string parentComment = resolutionFields.Get<string>(WfHelper.ResolutionParentCommentField).NormalizeComment();
                if (!string.IsNullOrEmpty(parentComment))
                {
                    result = string.Format(LocalizationManager.LocalizeAndEscapeFormat("$WfResolution_Result_ParentComment"), parentComment)
                          + Environment.NewLine
                          + result;
                }
            }

            return result;
        }


        /// <summary>
        /// Добавляет заданный идентификатор в список идентификаторов заданий,
        /// записи по истории которых будут удалены в транзакции на сохранение карточки,
        /// если у таких записей отсутствуют дочерние записи.
        /// </summary>
        /// <param name="taskRowID">
        /// Идентификатор задания, записи по истории которого будут удалены.
        /// </param>
        protected void DeferTaskHistoryRemovalUntilTransaction(Guid taskRowID)
        {
            if (this.historyItemsToDeleteRowIDList is null)
            {
                this.historyItemsToDeleteRowIDList = new List<Guid>();
            }

            this.historyItemsToDeleteRowIDList.Add(taskRowID);
        }


        /// <summary>
        /// Возвращает признак того, что бизнес-процесс резолюций может быть запущен.
        /// </summary>
        /// <param name="context">
        /// Контекст сохранения карточки, в процессе которого может быть запущен бизнес-процесс.
        /// </param>
        /// <returns>Признак того, что бизнес-процесс резолюций может быть запущен.</returns>
        protected virtual async ValueTask<bool> CanStartResolutionProcessAsync(ICardStoreExtensionContext context)
        {
            Card card = context.Request.Card;
            Guid mainCardID = card.ID;
            IDbScope dbScope = context.DbScope;

            await using (dbScope.Create())
            {
                (bool isSuccessful, string errorMessage) = await KrComponentsHelper.CheckKrComponentsAsync(
                    card,
                    null, // DocTypeID будет получен в CheckKrComponentsAsync.
                    dbScope,
                    this.krTypesCache,
                    KrComponents.Resolutions,
                    context.CancellationToken);

                if (!isSuccessful)
                {
                    context.ValidationResult.AddError(this, errorMessage);
                    return false;
                }

                KrToken krToken = KrToken.TryGet(card.Info);

                var permContext = await permissionsManager.TryCreateContextAsync(
                    new KrPermissionsCreateContextParams
                    {
                        Card = card,
                        IsStore = true,
                        ValidationResult = context.ValidationResult,
                        AdditionalInfo = context.Info,
                        PrevToken = krToken,
                        ExtensionContext = context,
                        ServerToken = context.Info.TryGetServerToken(),
                    },
                    cancellationToken: context.CancellationToken);

                if (permContext is null)
                {
                    return false;
                }

                return
                    await permissionsManager.CheckRequiredPermissionsAsync(
                        permContext,
                        KrPermissionFlagDescriptors.CreateResolutions);
            }
        }

        #endregion

        #region CardStoreExtension Base Overrides

        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            // здесь выполняется Workflow
            await base.BeforeCommitTransaction(context);

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            if (this.historyItemsToDeleteRowIDList is not null)
            {
                // если попали сюда, то есть хотя бы один элемент в списке
                var db = context.DbScope.Db;
                var builderFactory = context.DbScope.BuilderFactory;

                foreach (Guid rowID in this.historyItemsToDeleteRowIDList)
                {
                    // удаляем запись в истории только в том случае, если отсутствовали дочерние записи
                    // они могли присутствовать, например, при отправке дочерних резолюций для проекта резолюции

                    int deletedCount = await db
                        .SetCommand(
                            builderFactory
                                .DeleteFrom("TaskHistory")
                                .Where()
                                    .C("TaskHistory", "RowID").Equals().P("RowID")
                                    .And().NotExists(b => b
                                        .Select().V(null)
                                        .From("TaskHistory", "pt").NoLock()
                                        .Where().C("pt", "ParentRowID").Equals().C("TaskHistory", "RowID"))
                                .Build(),
                            db.Parameter("RowID", rowID))
                        .LogCommand()
                        .ExecuteNonQueryAsync(context.CancellationToken);

                    if (deletedCount > 0)
                    {
                        // удаляем запись из истории в карточке WfSatellite

                        await db
                            .SetCommand(
                                builderFactory
                                    .DeleteFrom("WfSatelliteTaskHistory")
                                    .Where().C("RowID").Equals().P("RowID")
                                    .Build(),
                                db.Parameter("RowID", rowID))
                            .LogCommand()
                            .ExecuteNonQueryAsync(context.CancellationToken);

                        // удаляем сателлит задания WfTaskCard, если он есть (в случае отмены постановки задачи)

                        Guid? taskCardID = await CardSatelliteHelper.TryGetUniversalSatelliteIDAsync(
                            context.DbScope,
                            context.Request.Card.ID, 
                            rowID, 
                            DefaultCardTypes.WfTaskCardTypeID,
                            context.CancellationToken);
                            
                        if (taskCardID.HasValue)
                        {
                            var deleteRequest = new CardDeleteRequest
                            {
                                CardID = taskCardID.Value,
                                CardTypeID = DefaultCardTypes.WfTaskCardTypeID,
                                DeletionMode = CardDeletionMode.WithoutBackup
                            };

                            string digest = context.Request.TryGetDigest();
                            if (!string.IsNullOrEmpty(digest))
                            {
                                deleteRequest.SetDigest(digest);
                            }

                            CardDeleteResponse deleteResponse = await this.cardRepositoryToDeleteSatellite.DeleteAsync(deleteRequest, context.CancellationToken);
                            context.ValidationResult.Add(deleteResponse.ValidationResult);
                        }
                    }
                }
            }
        }

        #endregion

        #region WorkflowStoreExtension Base Overrides

        protected override async ValueTask<bool> CardIsAllowedAsync(Card card, ICardStoreExtensionContext context) =>
            card.StoreMode == CardStoreMode.Update
            && await WfHelper.TypeSupportsWorkflowAsync(this.krTypesCache, context.CardType, context.CancellationToken);

        protected override async ValueTask<bool> TaskIsAllowedAsync(CardTask task, ICardStoreExtensionContext context) =>
            WfHelper.TaskTypeIsResolution(task.TypeID);

        protected override async ValueTask ModifyCompletedTasksBeforeRequestAsync(
            List<CardTask> completedTasks,
            ICardStoreExtensionContext context)
        {
            await base.ModifyCompletedTasksBeforeRequestAsync(completedTasks, context);

            foreach (CardTask task in completedTasks)
            {
                Guid? taskOptionID = task.OptionID;
                if (taskOptionID.HasValue)
                {
                    Guid taskTypeID = task.TypeID;
                    if (WfHelper.TaskTypeIsResolution(taskTypeID))
                    {
                        string result = this.CompleteResolutionAndGetResult(taskOptionID.Value, taskTypeID, task);
                        if (result is not null)
                        {
                            task.Result = result;
                        }
                    }
                }
            }
        }

        /// <doc path='info[@type="WorkflowStoreExtension" and @item="CanHandleQueueItem"]'/>
        protected override async ValueTask<bool> CanHandleQueueItemAsync(
            WorkflowQueueItem queueItem,
            ICardStoreExtensionContext context) =>
            WfHelper.ResolutionSubProcessName == queueItem.Signal.ProcessTypeName;

        protected override ValueTask<bool> CanStartProcessAsync(
            Guid? processID,
            string processName,
            ICardStoreExtensionContext context)
        {
            switch (processName)
            {
                case WfHelper.ResolutionProcessName:
                    return this.CanStartResolutionProcessAsync(context);

                default:
                    return new ValueTask<bool>(false);
            }
        }

        protected override async Task StartProcessAsync(
            Guid? processID,
            string processName,
            IWorkflowWorker workflowWorker,
            CancellationToken cancellationToken = default)
        {
            if (!await this.EnsureSatelliteExistsAsync(workflowWorker.Manager, cancellationToken))
            {
                return;
            }

            switch (processName)
            {
                case WfHelper.ResolutionProcessName:
                    await workflowWorker.StartProcessAsync(
                        WfHelper.ResolutionSubProcessName,
                        newProcessID: processID,
                        cancellationToken: cancellationToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(processName), processName, null);
            }
        }

        protected override async ValueTask<IWorkflowContext> CreateContextAsync(
            ICardStoreExtensionContext context,
            CardStoreRequest nextRequest)
        {
            IWorkflowContext workflowContext = await base.CreateContextAsync(context, nextRequest);

            Guid mainCardID = workflowContext.NextRequest.Card.ID;
            Guid? satelliteID = await WfHelper.TryGetSatelliteIDAsync(
                context.DbScope,
                mainCardID,
                context.CancellationToken);
            WfHelper.SetSatelliteID(workflowContext, satelliteID);

            // если карточка использует права доступа, рассчитываемые через KrToken,
            // то мы можем вычислить права на сохранение по Workflow по правам на предыдущее сохранение
            KrToken krToken = KrToken.TryGet(context.Request.Card.Info);
            if (krToken is not null)
            {
                Card nextCard = nextRequest.Card;

                if (!krToken.TryGetDocTypeID(out var docTypeID))
                {
                    docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                        nextCard,
                        context.DbScope,
                        context.CancellationToken);
                }

                KrToken nextKrToken = this.krTokenProvider.CreateToken(
                    nextCard,
                    krToken.PermissionsVersion,
                    krToken.Permissions,
                    krToken.ExtendedCardSettings,
                    t => t.SetDocTypeID(docTypeID));

                nextKrToken.Set(nextCard.Info);
            }

            // на крайний случай запрещаем кидать предупреждения при нерассчитанных правах
            // на сохранение по Workflow; тогда права рассчитываются автоматически
            nextRequest.SetIgnorePermissionsWarning();

            return workflowContext;
        }

        protected override async ValueTask<IWorkflowManager> CreateManagerAsync(
            IWorkflowContext workflowContext,
            CancellationToken cancellationToken = default) =>
            new WfWorkflowManager(
                workflowContext,
                this.WorkflowQueueProcessor,
                this.settingsLazy,
                this.roleGetStrategy,
                this.krTypesCache,
                this.cardCache,
                this.notificationManager);

        protected override async ValueTask<IWorkflowWorker> CreateWorkerAsync(
            IWorkflowManager workflowManager,
            CancellationToken cancellationToken = default) =>
            new WfWorkflowWorker(
                (WfWorkflowManager)workflowManager,
                this.CardRepositoryToCreateTasks);

        #endregion
    }
}
