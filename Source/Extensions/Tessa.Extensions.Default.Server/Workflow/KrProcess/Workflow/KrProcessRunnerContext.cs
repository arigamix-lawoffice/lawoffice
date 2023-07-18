using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Контекст <see cref="IKrProcessRunner"/>.
    /// </summary>
    public sealed class KrProcessRunnerContext : IKrProcessRunnerContext
    {
        #region Fields

        private readonly bool resurrection;

        private readonly Lazy<KrProcessRunnerInitiationCause> initiationCauseLazy;

        private readonly Func<IKrProcessRunnerContext, ValueTask> updateCardFuncAsync;

        #endregion

        #region Constructors

        public KrProcessRunnerContext(
            IWorkflowAPIBridge workflowAPI,
            IKrTaskHistoryResolver taskHistoryResolver,
            IMainCardAccessStrategy mainCardAccessStrategy,
            Guid? cardID,
            CardType cardType,
            Guid? docTypeID,
            KrComponents? krComponents,
            Card contextualSatellite,
            Card processHolderSatellite,
            WorkflowProcess workflowProcess,
            ProcessHolder processHolder,
            IWorkflowProcessInfo processInfo,
            IValidationResultBuilder validationResult,
            ICardExtensionContext cardContext,
            Func<IPreparingGroupRecalcStrategy> defaultPreparingGroupStrategyFunc,
            string parentProcessTypeName,
            Guid? parentProcessID,
            bool isProcessHolderCreated,
            Func<IKrProcessRunnerContext, ValueTask> updateCardFuncAsync,
            bool notMessageHasNoActiveStages,
            IKrSecondaryProcess secondaryProcess = null,
            bool ignoreGroupScripts = false,
            bool resurrection = false,
            CancellationToken cancellationToken = default)
        {
            this.WorkflowAPI = workflowAPI;
            this.TaskHistoryResolver = taskHistoryResolver;
            this.MainCardAccessStrategy = mainCardAccessStrategy;
            this.CardID = cardID;
            this.CardType = cardType;
            this.DocTypeID = docTypeID;
            this.KrComponents = krComponents;
            this.ContextualSatellite = contextualSatellite;
            this.ProcessHolderSatellite = processHolderSatellite;
            this.WorkflowProcess = workflowProcess;
            this.ProcessHolder = processHolder;
            this.ProcessInfo = processInfo;
            this.ValidationResult = validationResult;
            this.CardContext = cardContext;
            this.DefaultPreparingGroupStrategyFunc = defaultPreparingGroupStrategyFunc;
            this.ParentProcessTypeName = parentProcessTypeName;
            this.ParentProcessID = parentProcessID;
            this.IsProcessHolderCreated = isProcessHolderCreated;
            this.updateCardFuncAsync = updateCardFuncAsync;
            this.NotMessageHasNoActiveStages = notMessageHasNoActiveStages;
            this.SecondaryProcess = secondaryProcess;
            this.IgnoreGroupScripts = ignoreGroupScripts;
            this.resurrection = resurrection;
            this.initiationCauseLazy = new Lazy<KrProcessRunnerInitiationCause>(this.DetermineInitiationCause);
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IKrProcessRunnerContext Members

        /// <inheritdoc />
        public IWorkflowAPIBridge WorkflowAPI { get; }

        /// <inheritdoc />
        public IKrTaskHistoryResolver TaskHistoryResolver { get; }

        /// <inheritdoc />
        public IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <inheritdoc />
        public Guid? CardID { get; }

        /// <inheritdoc />
        public CardType CardType { get; }

        /// <inheritdoc />
        public Guid? DocTypeID { get; }

        /// <inheritdoc />
        public KrComponents? KrComponents { get; }

        /// <inheritdoc />
        public ProcessHolder ProcessHolder { get; }

        /// <inheritdoc />
        public Card ContextualSatellite { get; }

        /// <inheritdoc />
        public Card ProcessHolderSatellite { get; }

        /// <inheritdoc />
        public WorkflowProcess WorkflowProcess { get; }

        /// <inheritdoc />
        public KrProcessRunnerInitiationCause InitiationCause => this.initiationCauseLazy.Value;

        /// <inheritdoc />
        public IWorkflowProcessInfo ProcessInfo { get; }

        /// <inheritdoc />
        public IWorkflowTaskInfo TaskInfo => this.ProcessInfo as IWorkflowTaskInfo;

        /// <inheritdoc />
        public IWorkflowSignalInfo SignalInfo => this.ProcessInfo as IWorkflowSignalInfo;

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public ICardExtensionContext CardContext { get; }

        /// <inheritdoc />
        public IKrSecondaryProcess SecondaryProcess { get; }

        /// <inheritdoc />
        public string ParentProcessTypeName { get; }

        /// <inheritdoc />
        public Guid? ParentProcessID { get; }

        /// <inheritdoc />
        public bool IgnoreGroupScripts { get; }

        /// <inheritdoc />
        public Dictionary<Guid, IKrExecutionUnit> ExecutionUnitCache { get; } =
            new Dictionary<Guid, IKrExecutionUnit>();

        /// <inheritdoc />
        public List<Guid> SkippedStagesByCondition { get; } = new List<Guid>(16);

        /// <inheritdoc />
        public List<Guid> SkippedGroupsByCondition { get; } = new List<Guid>(8);

        /// <inheritdoc />
        public Func<IPreparingGroupRecalcStrategy> DefaultPreparingGroupStrategyFunc { get; }

        /// <inheritdoc />
        public IPreparingGroupRecalcStrategy PreparingGroupStrategy { get; set; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        /// <inheritdoc />
        public bool IsProcessHolderCreated { get; }

        /// <inheritdoc/>
        public bool NotMessageHasNoActiveStages { get; }

        /// <inheritdoc />
        public async ValueTask UpdateCardAsync() => await this.updateCardFuncAsync(this);

        #endregion

        #region Private Methods

        private KrProcessRunnerInitiationCause DetermineInitiationCause()
        {
            if (this.SignalInfo is not null)
            {
                return KrProcessRunnerInitiationCause.Signal;
            }

            // Избежание нескольких кастов.
            var taskInfo = this.TaskInfo;
            if (taskInfo is not null)
            {
                return taskInfo.Task.Action == CardTaskAction.Reinstate
                    ? KrProcessRunnerInitiationCause.ReinstateTask
                    : KrProcessRunnerInitiationCause.CompleteTask;
            }

            if (this.ProcessInfo is not null)
            {
                return KrProcessRunnerInitiationCause.StartProcess;
            }

            return this.resurrection
                ? KrProcessRunnerInitiationCause.Resurrection
                : KrProcessRunnerInitiationCause.InMemoryLaunching;
        }

        #endregion
    }
}