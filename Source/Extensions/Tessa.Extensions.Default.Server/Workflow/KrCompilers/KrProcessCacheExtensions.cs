#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="IKrProcessCache"/>.
    /// </summary>
    public static class KrProcessCacheExtensions
    {
        #region Public Methods

        /// <summary>
        /// Возвращает вторичный процесс по его идентификатору.
        /// </summary>
        /// <param name="krProcessCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="pid">Идентификатор вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Вторичный процесс.</returns>
        /// <exception cref="InvalidOperationException">Процесс с ИД = <paramref name="pid"/> не найден.</exception>
        public static async ValueTask<IKrSecondaryProcess> GetSecondaryProcessAsync(
            this IKrProcessCache krProcessCache,
            Guid pid,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(krProcessCache);

            var process = await krProcessCache.TryGetSecondaryProcessAsync(pid, cancellationToken);

            if (process is null)
            {
                throw new InvalidOperationException($"Process with ID = {pid} doesn't exist.");
            }

            return process;
        }

        #endregion
    }
}
