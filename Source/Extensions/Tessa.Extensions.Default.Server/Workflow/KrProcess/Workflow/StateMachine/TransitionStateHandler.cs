using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public sealed class TransitionStateHandler : StateHandlerBase
    {
        /// <inheritdoc />
        public override async Task<IStateHandlerResult> HandleAsync(
            IStateHandlerContext context)
        {
            var transitTo = context.State.Parameters.TryGet<Guid?>(KrConstants.Keys.FinalStageRowIDParam);
            var forceStartGroup = context.State.Parameters.TryGet<bool?>(KrConstants.Keys.ForceStartGroupParam) ?? false;
            
            var keepStageStates = context.State.Parameters.TryGet<bool?>(KrConstants.Keys.KeepStageStatesParam) ?? false;
            var preparingStrategy = context.State.Parameters
                    .TryGet<IPreparingGroupRecalcStrategy>(KrConstants.Keys.PreparingGroupRecalcStrategyParam)
                ?? new ExplicitlySelectedPreparingGroupRecalcStrategy();

            context.RunnerContext.WorkflowProcess.CurrentApprovalStageRowID = transitTo;
            context.RunnerContext.PreparingGroupStrategy = preparingStrategy;

            if (!keepStageStates)
            {
                if (transitTo.HasValue)
                {
                    TransitionHelper.ChangeStatesTransition(
                        context.RunnerContext.WorkflowProcess.Stages,
                        context.Stage.RowID,
                        transitTo.Value,
                        context.RunnerContext.ProcessHolderSatellite);
                }
                else
                {
                    TransitionHelper.SetSkipStateToSubsequentStages(
                        context.Stage,
                        context.RunnerContext.WorkflowProcess.Stages,
                        context.RunnerContext.ProcessHolderSatellite);
                }
            }
            
            return forceStartGroup
                ? StateHandlerResult.ContinueCurrentRunWithStartingNewGroupResult
                : StateHandlerResult.ContinueCurrentRunResult;
        }
    }
}