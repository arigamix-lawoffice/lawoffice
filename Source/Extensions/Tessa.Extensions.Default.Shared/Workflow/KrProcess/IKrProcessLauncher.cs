using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Объект, выполняющий запуск процессов.
    /// </summary>
    public interface IKrProcessLauncher
    {
        /// <summary>
        /// Запускает указанный процесс.
        /// </summary>
        /// <param name="krProcess">Запускаемый процесс.</param>
        /// <param name="cardContext">Контекст процесса взаимодействия с карточкой в рамках которого запускается процесс.</param>
        /// <param name="specificParameters">Параметры запуска процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запуска процесса.</returns>
        Task<IKrProcessLaunchResult> LaunchAsync(
            KrProcessInstance krProcess,
            ICardExtensionContext cardContext = null,
            IKrProcessLauncherSpecificParameters specificParameters = null,
            CancellationToken cancellationToken = default);
    }
}
