using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Объект для получения блокировок на чтение и запись правил доступа.
    /// </summary>
    public interface IKrPermissionsLockStrategy
    {
        /// <summary>
        /// Выполняет взятие блокировки на чтение правил доступа.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат взятия блокировки.</returns>
        Task<ValidationResult> ObtainReaderLockAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет взятие блокировки на запись правил доступа.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат взятия блокировки.</returns>
        Task<ValidationResult> ObtainWriterLockAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет все блокировки.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task ClearLocksAsync(
            CancellationToken cancellationToken = default);
    }
}
