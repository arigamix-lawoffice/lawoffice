using System;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public sealed class KrStageInterrupterContext: IKrStageInterrupterContext
    {
        public KrStageInterrupterContext(
            DirectionAfterInterrupt directionAfterInterrupt,
            Stage stage,
            IKrProcessRunnerContext runnerContext,
            KrProcessRunnerMode runnerMode,
            Func<bool, KrProcessState> setupNextState)
        {
            this.DirectionAfterInterrupt = directionAfterInterrupt;
            this.Stage = stage;
            this.RunnerContext = runnerContext;
            this.RunnerMode = runnerMode;
            this.SetupNextState = setupNextState;
        }

        /// <inheritdoc />
        public DirectionAfterInterrupt DirectionAfterInterrupt { get; }

        /// <inheritdoc />
        public Stage Stage { get; }

        /// <inheritdoc />
        public IKrProcessRunnerContext RunnerContext { get; }

        /// <inheritdoc />
        public KrProcessRunnerMode RunnerMode { get; }

        /// <inheritdoc />
        public Func<bool, KrProcessState> SetupNextState { get; }
    }
}