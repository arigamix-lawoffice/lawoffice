using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Интерфейс типа, позволяющего выполнять методы шаблонов,
    ///  определенные в IKrStageActions (Before, Condition, After)
    /// </summary>
    public interface IKrExecutor
    {
        /// <summary>
        /// Выполнение методов (Before, Condition, After) для объектов, указанных в контексте.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<IKrExecutionResult> ExecuteAsync(IKrExecutionContext context);
    }
}
