using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает объект определяющий возможность отображения вторичных процессов работающих в режиме "Кнопка".
    /// </summary>
    public interface IKrProcessButtonVisibilityEvaluator
    {
        /// <summary>
        /// Возвращает список отображаемых глобальных вторичных процессов работающих в режиме "Кнопка".
        /// </summary>
        /// <param name="context">Контекст используемый при определении видимости тайла вторичного процесса работающего в режиме "Кнопка".</param>
        /// <returns>Список отображаемых глобальных вторичных процессов работающих в режиме "Кнопка".</returns>
        Task<IList<IKrProcessButton>> EvaluateGlobalButtonsAsync(
            IKrProcessButtonVisibilityEvaluatorContext context);

        /// <summary>
        /// Возвращает список отображаемых локальных вторичных процессов работающих в режиме "Кнопка".
        /// </summary>
        /// <param name="context">Контекст используемый при определении видимости тайла вторичного процесса работающего в режиме "Кнопка".</param>
        /// <returns>Список доступных локальных вторичных процессов работающих в режиме "Кнопка".</returns>
        Task<IList<IKrProcessButton>> EvaluateLocalButtonsAsync(
            IKrProcessButtonVisibilityEvaluatorContext context);
    }
}