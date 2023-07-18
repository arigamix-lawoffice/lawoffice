using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <inheritdoc cref="IKrEventExtensionContext"/>
    public sealed class KrEventExtensionContext : IKrEventExtensionContext
    {
        #region Constructors

        public KrEventExtensionContext(
            string eventType,
            IDictionary<string, object> info,
            Guid? mainCardID,
            CardType mainCardType,
            Guid? mainCardDocTypeID,
            IMainCardAccessStrategy mainCardAccessStrategy,
            IKrSecondaryProcess secondaryProcess,
            Card contextualSatellite,
            Card processHolderSatellite,
            ICardExtensionContext cardExtensionContext,
            IValidationResultBuilder validationResult,
            Stage stage,
            WorkflowProcess workflowProcess,
            ProcessHolder processHolder,
            IWorkflowProcessInfo processInfo,
            KrProcessRunnerMode? runnerMode,
            KrProcessRunnerInitiationCause? initiationCause,
            CancellationToken cancellationToken = default)
        {
            this.EventType = eventType;
            this.Info = info ?? new Dictionary<string, object>(StringComparer.Ordinal);
            this.MainCardID = mainCardID;
            this.MainCardType = mainCardType;
            this.MainCardDocTypeID = mainCardDocTypeID;
            this.MainCardAccessStrategy = mainCardAccessStrategy;
            this.SecondaryProcess = secondaryProcess;
            this.ContextualSatellite = contextualSatellite;
            this.ProcessHolderSatellite = processHolderSatellite;
            this.CardExtensionContext = cardExtensionContext;
            this.ValidationResult = validationResult;
            this.Stage = stage;
            this.WorkflowProcess = workflowProcess;
            this.ProcessHolder = processHolder;
            this.ProcessInfo = processInfo;
            this.TaskInfo = processInfo as IWorkflowTaskInfo;
            this.SignalInfo = processInfo as IWorkflowSignalInfo;
            this.RunnerMode = runnerMode;
            this.InitiationCause = initiationCause;
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IExtensionContext Members

        /// <doc path='info[@type="IExtensionContext" and @item="CancellationToken"]'/>
        public CancellationToken CancellationToken { get; set; }

        #endregion

        #region IKrEventExtensionContext Members

        /// <inheritdoc />
        public string EventType { get; }

        /// <inheritdoc />
        public IDictionary<string, object> Info { get; }

        /// <inheritdoc />
        public Guid? MainCardID { get; }

        /// <inheritdoc />
        public CardType MainCardType { get; }

        /// <inheritdoc />
        public Guid? MainCardDocTypeID { get; }

        /// <inheritdoc />
        public IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <inheritdoc />
        public IKrSecondaryProcess SecondaryProcess { get; }

        /// <inheritdoc />
        public Card ContextualSatellite { get; }

        /// <inheritdoc />
        public Card ProcessHolderSatellite { get; }

        /// <inheritdoc />
        public ICardExtensionContext CardExtensionContext { get; }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public Stage Stage { get; }

        /// <inheritdoc />
        public WorkflowProcess WorkflowProcess { get; }

        /// <inheritdoc />
        public ProcessHolder ProcessHolder { get; }

        /// <inheritdoc />
        public IWorkflowProcessInfo ProcessInfo { get; }

        /// <inheritdoc />
        public IWorkflowTaskInfo TaskInfo { get; }

        /// <inheritdoc />
        public IWorkflowSignalInfo SignalInfo { get; }

        /// <inheritdoc />
        public KrProcessRunnerMode? RunnerMode { get; }

        /// <inheritdoc />
        public KrProcessRunnerInitiationCause? InitiationCause { get; }

        #endregion
    }
}