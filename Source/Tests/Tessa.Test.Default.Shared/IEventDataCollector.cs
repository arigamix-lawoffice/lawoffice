#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, агрегирующий информацию о событиях.
    /// </summary>
    public interface IEventDataCollector
    {
        /// <summary>
        /// Число записанных событий.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Возвращает информацию о событии.
        /// </summary>
        /// <param name="eventData">Информация о событии или значение по умолчанию для типа, если она не доступна.</param>
        /// <returns>Значение <see langword="true"/>, если информация о событии была успешно получена, иначе - <see langword="false"/>.</returns>
        bool TryRead(out EventData eventData);

        /// <summary>
        /// Возвращает информацию о событиях.
        /// </summary>
        IEnumerable<EventData> GetEvents();

        /// <summary>
        /// Записывает информацию о событии.
        /// </summary>
        /// <param name="evendData"><inheritdoc cref="EventData" path="/summary"/></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask WriteAsync(
            EventData evendData,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет все записанные события.
        /// </summary>
        void Clear();
    }
}
