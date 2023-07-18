#nullable enable

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, агрегирующий информацию о событиях в ограниченном объёме оперативной памяти.
    /// </summary>
    [DebuggerDisplay($"Count = {{{nameof(Count)}}}")]
    public sealed class BoundedInMemoryEventDataCollector :
        IEventDataCollector
    {
        #region Fields

        private readonly Channel<EventData> channel;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="capacity">Максимальное число элементов, которое может содержаться в этом объекте.</param>
        /// <param name="fullMode">Поведение, используемое при достижении максимального числа элементов в объекте.</param>
        public BoundedInMemoryEventDataCollector(
            int capacity = 1000,
            BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropOldest)
        {
            this.channel = Channel.CreateBounded<EventData>(
                new BoundedChannelOptions(
                    capacity)
                {
                    SingleReader = false,
                    SingleWriter = false,
                    FullMode = fullMode,
                });
        }

        #endregion

        #region IEventDataCollector Members

        /// <inheritdoc/>
        public int Count => this.channel.Reader.Count;

        /// <inheritdoc/>
        public bool TryRead(out EventData eventData) =>
            this.channel.Reader.TryRead(out eventData);

        /// <inheritdoc/>
        public IEnumerable<EventData> GetEvents()
        {
            while (this.TryRead(out var eventData))
            {
                yield return eventData;
            }
        }

        /// <inheritdoc/>
        public ValueTask WriteAsync(
            EventData evendData,
            CancellationToken cancellationToken = default)
        {
            return this.channel.Writer.WriteAsync(
                evendData,
                cancellationToken);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            while (this.channel.Reader.TryRead(out _))
            {
            }
        }

        #endregion
    }
}
