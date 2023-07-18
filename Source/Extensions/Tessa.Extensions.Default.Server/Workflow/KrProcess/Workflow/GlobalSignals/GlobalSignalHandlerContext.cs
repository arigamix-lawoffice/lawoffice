using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    public sealed class GlobalSignalHandlerContext : IGlobalSignalHandlerContext
    {
        /// <inheritdoc />
        public GlobalSignalHandlerContext(
            Stage stage,
            IKrProcessRunnerContext krProcessRunnerContext,
            KrProcessRunnerMode runnerMode)
        {
            this.Stage = stage;
            this.RunnerContext = krProcessRunnerContext;
            this.RunnerMode = runnerMode;
        }

        /// <inheritdoc />
        public Stage Stage { get; }

        /// <inheritdoc />
        public IKrProcessRunnerContext RunnerContext { get; }

        /// <inheritdoc />
        public KrProcessRunnerMode RunnerMode { get; }
    }
}