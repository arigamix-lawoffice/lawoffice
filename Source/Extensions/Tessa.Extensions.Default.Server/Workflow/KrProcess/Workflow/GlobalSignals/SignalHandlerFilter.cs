using System.Collections.Generic;
using System.Linq;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    /// <summary>
    /// Фильтр обработчиков сигналов.
    /// </summary>
    public readonly struct SignalHandlerFilter : IKrProcessFilter<SignalFilterItem>
    {
        /// <summary>
        /// Инициализирует новый фильтр обработчиков сигналов заданным массивом объектов <see cref="SignalFilterItem"/> содержащих информацию об исключаемых из обработки обработчиков сигналов.
        /// </summary>
        /// <param name="signalTypes">Массив <see cref="SignalFilterItem"/>.</param>
        /// <returns>Фильтр обработчиков сигналов.</returns>
        public static SignalHandlerFilter Exclude(params SignalFilterItem[] signalTypes) =>
            new SignalHandlerFilter(signalTypes.ToList().AsReadOnly());

        /// <summary>
        /// Инициализирует новый фильтр обработчиков сигналов заданной коллекцией объектов <see cref="SignalFilterItem"/> содержащих информацию об исключаемых из обработки обработчиков сигналов.
        /// </summary>
        /// <param name="signalTypes">Коллекция <see cref="SignalFilterItem"/>.</param>
        /// <returns>Фильтр обработчиков сигналов.</returns>
        public static SignalHandlerFilter Exclude(ICollection<SignalFilterItem> signalTypes) =>
            new SignalHandlerFilter(signalTypes.ToList().AsReadOnly());

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SignalHandlerFilter"/>.
        /// </summary>
        /// <param name="excluded">Доступная только для чтения коллекция исключаемых объектов - обработчиков сигналов.</param>
        private SignalHandlerFilter(
            IReadOnlyCollection<SignalFilterItem> excluded)
        {
            this.Excluded = excluded;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<SignalFilterItem> Excluded { get; }
    }
}