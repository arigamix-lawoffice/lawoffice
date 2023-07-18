using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает стратегию подготавливающую данные для пересчёта маршрута.
    /// </summary>
    public interface IPreparingGroupRecalcStrategy
    {
        /// <summary>
        /// Возвращает признак, показывающий, что данная стратегия была применена.
        /// </summary>
        bool Used { get; }

        /// <summary>
        /// Возвращает коллекцию объектов выполнения.
        /// </summary>
        IList<Guid> ExecutionUnits { get; }

        /// <summary>
        /// Возвращает первый этап удовлетворяющий стратегии.
        /// </summary>
        /// <param name="stages">Рассматриваемая коллекция этапов.</param>
        /// <returns>Первый этап удовлетворяующий стратегии или значение по умолчанию для типа, если его не удалось найти.</returns>
        Stage GetSuitableStage(
            IList<Stage> stages);

        /// <summary>
        /// Применяет текущую стратегию.
        /// </summary>
        /// <param name="context">Контекст <see cref="IKrProcessRunner"/>.</param>
        /// <param name="stage">Текущий этап.</param>
        /// <param name="prevStage">Предыдущий этап.</param>
        /// <returns>Асинхронная задача.</returns>
        Task ApplyAsync(
            IKrProcessRunnerContext context,
            Stage stage,
            Stage prevStage);
    }
}