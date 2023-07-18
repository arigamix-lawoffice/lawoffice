using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public sealed class SkipProcessStateHandler : StateHandlerBase
    {
        /// <inheritdoc />
        public override async Task<IStateHandlerResult> HandleAsync(
            IStateHandlerContext context)
        {
            var workflowProcess = context.RunnerContext.WorkflowProcess;
            var currentStage = context.Stage;
            context.RunnerContext.WorkflowProcess.CurrentApprovalStageRowID = null;
            context.RunnerContext.PreparingGroupStrategy = new DisableRecalcPreparingGroupRecalcStrategy();
            TransitionHelper.SetSkipStateToSubsequentStages(
                currentStage,
                workflowProcess.Stages,
                context.RunnerContext.ProcessHolderSatellite);

            return StateHandlerResult.WithoutContinuationProcessResult;
        }
    }
}