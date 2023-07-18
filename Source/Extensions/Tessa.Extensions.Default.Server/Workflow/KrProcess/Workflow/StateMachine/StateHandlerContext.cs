using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public sealed class StateHandlerContext : IStateHandlerContext
    {
        public StateHandlerContext(
            KrProcessState state,
            Stage stage,
            KrProcessRunnerMode runnerMode,
            IKrProcessRunnerContext runnerContext)
        {
            this.State = state;
            this.Stage = stage;
            this.RunnerMode = runnerMode;
            this.RunnerContext = runnerContext;
        }

        /// <inheritdoc />
        public KrProcessState State { get; }

        /// <inheritdoc />
        public Stage Stage { get; }

        /// <inheritdoc />
        public KrProcessRunnerMode RunnerMode { get; }

        /// <inheritdoc />
        public IKrProcessRunnerContext RunnerContext { get; }

    }
}