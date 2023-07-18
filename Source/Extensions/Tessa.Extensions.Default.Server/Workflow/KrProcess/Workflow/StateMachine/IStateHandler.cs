using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public interface IStateHandler
    {
        Task<IStateHandlerResult> HandleAsync(
            IStateHandlerContext context);
    }
}