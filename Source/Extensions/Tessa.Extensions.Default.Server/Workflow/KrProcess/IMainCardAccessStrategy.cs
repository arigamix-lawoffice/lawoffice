using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Описывает стратегию доступа к карточке.
    /// </summary>
    public interface IMainCardAccessStrategy : IAsyncDisposable
    {
        /// <summary>
        /// Признак того, что стратегия использовалась, т.е. вызывались методы доступа к карточке.
        /// </summary>
        bool WasUsed { get; }

        /// <summary>
        /// Возвращает признак использования файлового контейнера.
        /// </summary>
        /// <seealso cref="GetFileContainerAsync(IValidationResultBuilder,CancellationToken)"/>
        bool WasFileContainerUsed { get; }

        /// <summary>
        /// Получение объекта карточки в соответствии с правилами стратегии.
        /// </summary>
        /// <param name="validationResult">Результат валидации.</param>
        /// <param name="withoutTransaction">Признак того, что карточка будет загружена без транзакции и без взятия блокировки на чтение карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого может быть отменена асинхронная задача.</param>
        /// <returns>Объект карточки полученный в соответствии с правилами стратегии.</returns>
        ValueTask<Card> GetCardAsync(
            IValidationResultBuilder validationResult = null,
            bool withoutTransaction = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает файловый контейнер карточки полученный в соответствии с правилами стратегии.
        /// </summary>
        /// <param name="validationResult">Результат валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого может быть отменена асинхронная задача.</param>
        /// <returns>Файловый контейнер карточки или значение null, если при создании произошли ошибки.</returns>
        ValueTask<ICardFileContainer> GetFileContainerAsync(
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Загрузка (при необходимости) истории заданий в карточку в соответствии с правилами стратегии.
        /// </summary>
        /// <param name="validationResult">Результат валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        Task EnsureTaskHistoryLoadedAsync(
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);
    }
}