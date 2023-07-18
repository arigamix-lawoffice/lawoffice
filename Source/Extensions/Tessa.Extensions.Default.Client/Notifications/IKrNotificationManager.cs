using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.Notifications
{
    /// <summary>
    /// Объект, управляющий уведомлениями по новым заданиям.
    /// </summary>
    public interface IKrNotificationManager
    {
        /// <summary>
        /// Подготавливаем инфраструктуру для периодического затягивания информации по новым заданиям.
        /// При этом сам запрос <see cref="CheckTasksAsync"/> выполнять не требуется.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask InitializeAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Освобождает инфраструктуру для периодического затягивания информации по новым заданиям.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask ShutdownAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает признак того, что уведомления по заданиям включены.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если уведомления по заданиям включены;
        /// <c>false</c> в противном случае.
        /// </returns>
        ValueTask<bool> CanCheckTasksAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Проверяет новые задания и отображает уведомления, если они есть.
        /// Метод вызывается в потоке UI, но фактическое отображение должно быть асинхронное.
        /// </summary>
        /// <param name="manualCheck">
        /// Признак того, что проверка выполняется вручную. При этом на экране отображаются дополнительные сообщения.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task CheckTasksAsync(bool manualCheck = false, CancellationToken cancellationToken = default);
    }
}
