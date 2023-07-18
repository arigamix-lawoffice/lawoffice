using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public interface IOutboxManager
    {
        Task<ConcurrentQueue<OutboxMessage>> GetTopMessagesAsync(
            int topCount,
            int retryIntervalMinutes,
            CancellationToken cancellationToken = default);

        Task MarkAsBadMessageAsync(
            Guid id,
            long attemptNum,
            string exceptionMessage,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}