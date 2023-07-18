using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Раннер используемый для выполнения асинхронных процессов маршрутов документов.
    /// </summary>
    public sealed class KrAsyncProcessRunner :
        KrProcessRunnerBase
    {
        #region Constructors

        public KrAsyncProcessRunner(
            IKrProcessContainer processContainer,
            IKrStageGroupCompilationCache stageGroupCompilationCache,
            IKrStageTemplateCompilationCache stageTemplateCompilationCache,
            [Dependency(KrExecutorNames.CacheExecutor)] Func<IKrExecutor> executorFunc,
            IKrScope scope,
            IDbScope dbScope,
            IKrProcessCache processCache,
            IUnityContainer unityContainer,
            ISession session,
            IKrProcessRunnerProvider runnerProvider,
            IKrTypesCache typesCache,
            ICardMetadata cardMetadata,
            IKrProcessStateMachine stateMachine,
            IKrStageInterrupter interrupter,
            IKrSqlExecutor sqlExecutor,
            ICardCache cardCache,
            IKrStageSerializer stageSerializer,
            IObjectModelMapper objectModelMapper,
            IKrTokenProvider tokenProvider,
            ICardRepository cardRepository,
            [Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardRepository cardRepositoryEwt,
            ICardStreamServerRepository cardStreamServerRepository,
            [Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardStreamServerRepository cardStreamServerRepositoryEwt,
            ICardTransactionStrategy cardTransactionStrategy)
            : base(
                  processContainer,
                  stageGroupCompilationCache,
                  stageTemplateCompilationCache,
                  executorFunc,
                  scope,
                  dbScope,
                  processCache,
                  unityContainer,
                  session,
                  runnerProvider,
                  typesCache,
                  cardMetadata,
                  stateMachine,
                  interrupter,
                  sqlExecutor,
                  cardCache,
                  stageSerializer,
                  objectModelMapper,
                  tokenProvider,
                  cardRepository,
                  cardRepositoryEwt,
                  cardStreamServerRepository,
                  cardStreamServerRepositoryEwt,
                  cardTransactionStrategy)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override KrProcessRunnerMode RunnerMode { get; } = KrProcessRunnerMode.Async;

        /// <inheritdoc />
        protected override async Task<bool> PrepareAsync(IKrProcessRunnerContext context)
        {
            if (this.Scope.HasLaunchedRunner(context.ProcessInfo.ProcessID))
            {
                context.ValidationResult.AddError(this, "$KrProcess_ErrorMessage_NestedProcessRunner");
                return false;
            }

            this.Scope.AddLaunchedRunner(context.ProcessInfo.ProcessID);

            if (context.InitiationCause != KrProcessRunnerInitiationCause.StartProcess)
            {
                return true;
            }

            if (context.WorkflowProcess.CurrentApprovalStageRowID.HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_ProcessAlreadyStarted");
                return false;
            }

            await this.SetAuthorAsync(context);

            await this.InitialRecalcAsync(context);

            if (!context.ValidationResult.IsSuccessful())
            {
                return false;
            }

            if (context.WorkflowProcess.Stages.Count == 0)
            {
                if (!context.NotMessageHasNoActiveStages)
                {
                    context.ValidationResult.AddError(this, KrErrorHelper.FormatEmptyRoute(context.SecondaryProcess));
                }

                return false;
            }

            foreach (var stage in context.WorkflowProcess.Stages)
            {
                stage.State = KrStageState.Inactive;
            }

            return true;
        }

        /// <inheritdoc />
        protected override ValueTask FinalizeAsync(
            IKrProcessRunnerContext context,
            Exception exc = null)
        {
            if (!context.NotMessageHasNoActiveStages
                && context.WorkflowProcess.Stages.Count > 0
                && context.InitiationCause == KrProcessRunnerInitiationCause.StartProcess
                && context.WorkflowProcess.Stages.All(p => p.State == KrStageState.Skipped || p.Hidden && context.SecondaryProcess is null))
            {
                context.ValidationResult.AddError(this, KrErrorHelper.FormatEmptyRoute(context.SecondaryProcess));
            }

            this.Scope.RemoveLaunchedRunner(context.ProcessInfo.ProcessID);

            return new ValueTask();
        }

        /// <inheritdoc/>
        protected override async Task<NextAction> ProcessStageHandlerResultAsync(
            Stage stage,
            StageHandlerResult result,
            IKrProcessRunnerContext context)
        {
            // InProgress и None не делают ничего.
            if (result.Action is not StageHandlerAction.InProgress
                and not StageHandlerAction.None)
            {
                return await base.ProcessStageHandlerResultAsync(stage, result, context);
            }

            return new NextAction();
        }

        #endregion

        #region Private Methods

        private async Task InitialRecalcAsync(
            IKrProcessRunnerContext context)
        {
            if (!context.CardID.HasValue)
            {
                return;
            }

            var executionUnits = context.SecondaryProcess is not null
                ? (await this.ProcessCache.GetStageGroupsForSecondaryProcessAsync(context.SecondaryProcess.ID, context.CancellationToken)).Select(p => p.ID)
                : null;
            await using var cardLoadingStrategy = new KrScopeMainCardAccessStrategy(context.CardID.Value, this.Scope);
            var ctx = new KrExecutionContext(
                cardContext: context.CardContext,
                mainCardAccessStrategy: cardLoadingStrategy,
                cardID: context.CardID,
                cardType: context.CardType,
                docTypeID: context.DocTypeID,
                krComponents: context.KrComponents,
                workflowProcess: context.WorkflowProcess,
                executionUnits: executionUnits, // или null, тогда выполнится все что возможно
                secondaryProcess: context.SecondaryProcess, // или null
                cancellationToken: context.CancellationToken
            );

            var executor = this.ExecutorFunc();
            var result = await executor.ExecuteAsync(ctx);
            context.ValidationResult.Add(result.Result);
        }

        #endregion
    }
}
