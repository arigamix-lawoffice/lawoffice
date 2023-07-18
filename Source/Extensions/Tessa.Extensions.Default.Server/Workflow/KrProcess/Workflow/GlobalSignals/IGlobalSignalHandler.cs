using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    public interface IGlobalSignalHandler
    {
        Task<IGlobalSignalHandlerResult> Handle(
            IGlobalSignalHandlerContext context);
    }
}