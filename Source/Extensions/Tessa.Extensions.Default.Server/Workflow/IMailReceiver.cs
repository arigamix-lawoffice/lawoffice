using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow
{
    /// <summary>
    /// Объект, выполняющий обработку получения и обработки писем мобильного согласования.
    /// </summary>
    public interface IMailReceiver
    {
        /// <summary>
        /// Функция, возвращающая признак того, что запрошена остановка процесса обработки писем.
        /// </summary>
        Func<bool> StopRequestedFunc { get; set; }

        /// <summary>
        /// Обработка сообщений
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task ReceiveMessagesAsync(CancellationToken cancellationToken = default);
    }
}