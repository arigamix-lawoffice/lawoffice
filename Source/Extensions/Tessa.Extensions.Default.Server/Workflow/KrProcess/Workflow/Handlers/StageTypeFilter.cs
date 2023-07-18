using System;
using System.Collections.Generic;
using System.Linq;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Фильтр обработчиков типов этапов.
    /// </summary>
    public readonly struct StageTypeFilter : IKrProcessFilter<Guid>
    {
        /// <summary>
        /// Инициализирует новый фильтр обработчиков типов этапов заданным массивом идентификаторов типов этапов исключаемых из обработчки.
        /// </summary>
        /// <param name="handlerIDs">Массив идентификаторов типов этапов.</param>
        /// <returns>Фильтр обработчиков типов этапов.</returns>
        public static StageTypeFilter Exclude(params Guid[] handlerIDs) =>
            new StageTypeFilter(handlerIDs.ToList().AsReadOnly());

        /// <summary>
        /// Инициализирует новый фильтр обработчиков типов этапов заданной коллекцией идентификаторов типов этапов исключаемых из обработчки.
        /// </summary>
        /// <param name="handlerIDs">Коллекция идентификаторов типов этапов.</param>
        /// <returns>Фильтр обработчиков типов этапов.</returns>
        public static StageTypeFilter Exclude(ICollection<Guid> handlerIDs) =>
            new StageTypeFilter(handlerIDs.ToList().AsReadOnly());

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StageTypeFilter"/>.
        /// </summary>
        /// <param name="excluded">Доступная только для чтения коллекция исключаемых объектов - идентификаторов типов этапов.</param>
        private StageTypeFilter(
            IReadOnlyCollection<Guid> excluded)
        {
            this.Excluded = excluded;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<Guid> Excluded { get; }
    }
}