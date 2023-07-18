using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Описывает методы определяющие условие выполнимости процесса.
    /// </summary>
    public interface IKrProcessExecutionScript
    {
        /// <summary>
        /// Выполняет условие выполнимости процесса.
        /// </summary>
        ValueTask<bool> RunExecutionAsync();

        /// <summary>
        /// Метод содержащий условие выполнимости процесса.
        /// </summary>
        ValueTask<bool> ExecutionAsync();
    }
}