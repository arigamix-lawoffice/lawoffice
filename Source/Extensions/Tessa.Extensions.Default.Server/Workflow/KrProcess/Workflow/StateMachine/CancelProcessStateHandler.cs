using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public sealed class CancelProcessStateHandler : StateHandlerBase
    {
        /// <inheritdoc />
        public override async Task<IStateHandlerResult> HandleAsync(
            IStateHandlerContext context)
        {
            var workflowProcess = context.RunnerContext.WorkflowProcess;
            context.RunnerContext.WorkflowProcess.CurrentApprovalStageRowID = null;
            context.RunnerContext.PreparingGroupStrategy = new DisableRecalcPreparingGroupRecalcStrategy();
            TransitionHelper.SetInactiveStateToAllStages(
                workflowProcess.Stages,
                context.RunnerContext.ProcessHolderSatellite);
            return StateHandlerResult.WithoutContinuationProcessResult;
        }
    }
}