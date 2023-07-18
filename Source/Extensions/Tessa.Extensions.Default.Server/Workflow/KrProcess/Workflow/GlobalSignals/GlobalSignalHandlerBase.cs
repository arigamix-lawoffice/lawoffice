using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    public abstract class GlobalSignalHandlerBase: IGlobalSignalHandler
    {
        /// <inheritdoc />
        public virtual Task<IGlobalSignalHandlerResult> Handle(
            IGlobalSignalHandlerContext context)
        {
            return Task.FromResult(GlobalSignalHandlerResult.EmptyHandlerResult);
        }
    }
}