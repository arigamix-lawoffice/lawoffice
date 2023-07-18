using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public abstract class StateHandlerBase: IStateHandler
    {
        /// <inheritdoc />
        public virtual async Task<IStateHandlerResult> HandleAsync(
            IStateHandlerContext context) => StateHandlerResult.EmptyResult;
    }
}