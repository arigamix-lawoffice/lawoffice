using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает раннер используемый для выполнения процессов маршрутов документов.
    /// </summary>
    public interface IKrProcessRunner
    {
        /// <summary>
        /// Начинает обработку процесса информация о котором задана в контексте.
        /// </summary>
        /// <param name="context">Контекст <see cref="IKrProcessRunner"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        Task RunAsync(IKrProcessRunnerContext context);
    }
}