using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Объект, выполняющий проверку возможности выполнения вторичного процесса.
    /// </summary>
    public interface IKrSecondaryProcessExecutionEvaluator
    {
        /// <summary>
        /// Выполняет проверку возможности выполнения вторичного процесса.
        /// </summary>
        /// <param name="context">Контекст содержащий информацию о вторичном процессе.</param>
        /// <returns>Значение <see langword="true"/>, если проверка пройдена успешно, иначе - <see langword="false"/>.</returns>
        Task<bool> EvaluateAsync(
            IKrSecondaryProcessEvaluatorContext context);
    }
}
