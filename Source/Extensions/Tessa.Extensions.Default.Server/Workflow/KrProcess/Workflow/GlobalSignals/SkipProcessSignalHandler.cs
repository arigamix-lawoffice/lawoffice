using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    public sealed class SkipProcessSignalHandler : GlobalSignalHandlerBase
    {
        private readonly IKrStageInterrupter interrupter;

        public SkipProcessSignalHandler(
            IKrStageInterrupter interrupter)
        {
            this.interrupter = interrupter;
        }

        /// <inheritdoc />
        public override async Task<IGlobalSignalHandlerResult> Handle(
            IGlobalSignalHandlerContext context)
        {
            var currentStage = context.Stage;
            if (!currentStage.StageTypeID.HasValue)
            {
                context.RunnerContext.WorkflowProcess.CurrentApprovalStageRowID = null;
                context.RunnerContext.PreparingGroupStrategy = new DisableRecalcPreparingGroupRecalcStrategy();
                TransitionHelper.SetSkipStateToSubsequentStages(
                    currentStage,
                    context.RunnerContext.WorkflowProcess.Stages,
                    context.RunnerContext.ProcessHolderSatellite);
                return GlobalSignalHandlerResult.WithoutContinuationProcessResult;
            }

            var completelyInterrupted = await this.interrupter.InterruptStageAsync(new KrStageInterrupterContext(
                DirectionAfterInterrupt.Forward,
                context.Stage,
                context.RunnerContext,
                context.RunnerMode,
                ci => ci 
                    ? KrProcessState.Default 
                    : new KrProcessState(
                        KrConstants.SkipProcessState,
                        new Dictionary<string, object>
                        {
                            [KrConstants.Keys.DirectionAfterInterruptParam] = DirectionAfterInterrupt.Forward,
                        })));

            if (completelyInterrupted)
            {
                context.RunnerContext.WorkflowProcess.CurrentApprovalStageRowID = null;
                context.RunnerContext.PreparingGroupStrategy = new DisableRecalcPreparingGroupRecalcStrategy();
                TransitionHelper.SetSkipStateToSubsequentStages(
                    currentStage, 
                    context.RunnerContext.WorkflowProcess.Stages,
                    context.RunnerContext.ProcessHolderSatellite);
            }
            return GlobalSignalHandlerResult.WithoutContinuationProcessResult;
        }
    }
}