using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public interface IKrStageInterrupter
    {
        /// <summary>
        /// Выполнить прерывания этапа.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> InterruptStageAsync(IKrStageInterrupterContext context);
    }
}