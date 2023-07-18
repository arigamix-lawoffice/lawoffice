using System.Collections.Generic;
using System.Linq;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    /// <summary>
    /// Фильтр типов сигналов.
    /// </summary>
    public readonly struct SignalFilter : IKrProcessFilter<string>
    {
        /// <summary>
        /// Инициализирует новый фильтр типов сигналов заданным массивом имён исключаемых типов сигналов.
        /// </summary>
        /// <param name="signalTypes">Массив имён исключаемых типов сигналов.</param>
        /// <returns>Фильтр типов сигналов.</returns>
        public static SignalFilter Exclude(params string[] signalTypes) =>
            new SignalFilter(signalTypes.ToList().AsReadOnly());

        /// <summary>
        /// Инициализирует новый фильтр типов сигналов заданной коллекцией имён исключаемых типов сигналов.
        /// </summary>
        /// <param name="signalTypes">Коллекция имён исключаемых типов сигналов.</param>
        /// <returns>Фильтр типов сигналов.</returns>
        public static SignalFilter Exclude(ICollection<string> signalTypes) =>
            new SignalFilter(signalTypes.ToList().AsReadOnly());

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SignalFilter"/>.
        /// </summary>
        /// <param name="excluded">Доступная только для чтения коллекция исключаемых объектов - типов сигналов.</param>
        private SignalFilter(
            IReadOnlyCollection<string> excluded)
        {
            this.Excluded = excluded;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<string> Excluded { get; }
    }
}