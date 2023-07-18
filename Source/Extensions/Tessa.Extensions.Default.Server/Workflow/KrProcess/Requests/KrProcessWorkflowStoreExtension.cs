using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    public sealed class KrProcessWorkflowStoreExtension : KrWorkflowStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;
        private readonly KrSettingsLazy settingsLazy;
        private readonly IKrProcessRunner asyncRunner;
        private readonly IObjectModelMapper objectModelMapper;
        private readonly IKrScope krScope;
        private readonly IKrTokenProvider tokenProvider;
        private readonly IKrProcessContainer processContainer;
        private readonly IKrProcessCache processCache;
        private readonly IKrEventManager eventManager;

        #endregion

        #region Constructors

        public KrProcessWorkflowStoreExtension(
            IKrTokenProvider krTokenProvider,
            [Dependency(CardRepositoryNames.Default)] ICardRepository cardRepositoryToCreateNextRequest,
            ICardRepository cardRepositoryToStoreNextRequest,
            ICardRepository cardRepositoryToCreateTasks,
            ICardTaskHistoryManager taskHistoryManager,
            ICardGetStrategy cardGetStrategy,
            IWorkflowQueueProcessor workflowQueueProcessor,
            IKrTypesCache typesCache,
            KrSettingsLazy settingsLazy,
            IKrProcessRunnerProvider runnerProvider,
            IObjectModelMapper objectModelMapper,
            IKrScope krScope,
            IKrTokenProvider tokenProvider,
            IKrProcessContainer processContainer,
            IKrProcessCache processCache,
            IKrEventManager eventManager)
            : base(
                  krTokenProvider,
                  cardRepositoryToCreateNextRequest,
                  cardRepositoryToStoreNextRequest,
                  cardRepositoryToCreateTasks,
                  taskHistoryManager,
                  cardGetStrategy,
                  workflowQueueProcessor)
        {
            Check.ArgumentNotNull(runnerProvider, nameof(runnerProvider));

            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.settingsLazy = settingsLazy ?? throw new NullReferenceException(nameof(settingsLazy));
            this.asyncRunner = runnerProvider.GetRunner(KrProcessRunnerNames.Async);
            this.objectModelMapper = objectModelMapper ?? throw new ArgumentNullException(nameof(objectModelMapper));
            this.krScope = krScope ?? throw new ArgumentNullException(nameof(krScope));
            this.tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            this.processContainer = processContainer ?? throw new ArgumentNullException(nameof(processContainer));
            this.processCache = processCache ?? throw new ArgumentNullException(nameof(processCache));
            this.eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task AfterRequest(
            ICardStoreExtensionContext context)
        {
            if (context.Info.GetAsyncProcessCompletedSimultaniosly())
            {
                context.Response.Info.SetAsyncProcessCompletedSimultaniosly();
            }

            var pi = context.Info.GetProcessInfoAtEnd();
            if (pi != null)
            {
                context.Response.Info.SetProcessInfoAtEnd(pi);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override async ValueTask<bool> CardIsAllowedAsync(
            Card card,
            ICardStoreExtensionContext context) => true;

        /// <inheritdoc/>
        protected override async ValueTask<bool> TaskIsAllowedAsync(CardTask task, ICardStoreExtensionContext context) =>
            await this.processContainer.IsTaskTypeRegisteredAsync(task.TypeID, context.CancellationToken);

        /// <inheritdoc/>
        protected override async ValueTask<bool> CanHandleQueueItemAsync(WorkflowQueueItem queueItem, ICardStoreExtensionContext context) =>
            KrConstants.KrProcessName == queueItem.Signal.ProcessTypeName
            || KrConstants.KrSecondaryProcessName == queueItem.Signal.ProcessTypeName
            || KrConstants.KrNestedProcessName == queueItem.Signal.ProcessTypeName;

        /// <inheritdoc/>
        protected override async ValueTask<bool> CanStartProcessAsync(
            Guid? processID,
            string processName,
            ICardStoreExtensionContext context)
        {
            if (KrConstants.KrProcessName != processName
                && KrConstants.KrSecondaryProcessName != processName
                && KrConstants.KrNestedProcessName != processName)
            {
                return false;
            }

            var card = context.Request.Card;
            var allowed = await KrProcessHelper
                .CardSupportsRoutesAsync(card, context.DbScope, this.typesCache, context.CancellationToken);
            if (!allowed)
            {
                context.ValidationResult.AddError(this, "$KrProcess_Disabled");
            }
            return allowed;
        }

        /// <inheritdoc/>
        protected override async Task StartProcessAsync(
            Guid? processID,
            string processName,
            IWorkflowWorker workflowWorker,
            CancellationToken cancellationToken = default)
        {
            var manager = (KrProcessWorkflowManager) workflowWorker.Manager;
            var request = manager.WorkflowContext.Request;

            if (processName == KrConstants.KrSecondaryProcessName)
            {
                // Для вторичного процесса нужно создать сателлит.
                if (request.TryGetStartingProcessName() != processName)
                {
                    return;
                }

                processID ??= Guid.NewGuid();
                var card = await this.krScope.CreateSecondaryKrSatelliteAsync(
                    manager.WorkflowContext.CardID,
                    processID.Value,
                    cancellationToken);

                if (card is  null)
                {
                    return;
                }

                manager.SpecifySatelliteID(card.ID);
            }

            await workflowWorker.StartProcessAsync(
                processName,
                parameters: request.TryGetStartingKrProcessParameters(),
                newProcessID: processID,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        protected override async ValueTask<IWorkflowContext> CreateContextAsync(
            ICardStoreExtensionContext context,
            CardStoreRequest nextRequest)
        {
            await this.MoveTokenToNextRequestAsync(
                context.Request.Card,
                nextRequest,
                context.DbScope,
                context.CancellationToken);
            var card = context.Request.Card;
            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                context.DbScope,
                context.CancellationToken);
            var components = await KrComponentsHelper.GetKrComponentsAsync(
                card.TypeID,
                docTypeID,
                this.typesCache,
                context.CancellationToken);
            return new KrProcessWorkflowContext(
                card.ID,
                docTypeID,
                components,
                context,
                nextRequest,
                this.CardRepositoryToCreateTasks,
                this.TaskHistoryManager,
                this.CardGetStrategy,
                this.objectModelMapper,
                this.asyncRunner,
                this.krScope,
                this.settingsLazy,
                this.processCache,
                this.eventManager);
        }

        /// <inheritdoc/>
        protected override async ValueTask<IWorkflowManager> CreateManagerAsync(
            IWorkflowContext workflowContext,
            CancellationToken cancellationToken = default) =>
            new KrProcessWorkflowManager((KrProcessWorkflowContext) workflowContext, this.WorkflowQueueProcessor);

        /// <inheritdoc/>
        protected override async ValueTask<IWorkflowWorker> CreateWorkerAsync(
            IWorkflowManager workflowManager,
            CancellationToken cancellationToken = default) =>
            new KrProcessWorkflowWorker((KrProcessWorkflowManager) workflowManager);

        /// <inheritdoc/>
        protected override ValueTask<bool> UnknownTaskIsAllowedAsync(CardTask task, ICardStoreExtensionContext context)
            => new ValueTask<bool>(true);

        #endregion

        #region Private Methods

        private async ValueTask MoveTokenToNextRequestAsync(
            Card card,
            CardStoreRequest nextRequest,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            // если карточка использует права доступа, рассчитываемые через KrToken,
            // то мы можем вычислить права на сохранение по Workflow по правам на предыдущее сохранение
            var krToken = KrToken.TryGet(card.Info);
            if (krToken is not null)
            {
                var nextCard = nextRequest.Card;

                if (!krToken.TryGetDocTypeID(out var docTypeID))
                {
                    docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                        nextCard,
                        dbScope,
                        cancellationToken);
                }

                var nextKrToken = this.tokenProvider.CreateToken(
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
        }

        #endregion
    }
}
