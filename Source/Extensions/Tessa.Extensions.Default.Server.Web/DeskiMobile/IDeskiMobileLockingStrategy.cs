using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    /// <summary>
    /// Стратегия по управлению блокировками операций для Deski Mobile.
    /// </summary>
    public interface IDeskiMobileLockingStrategy
    {
        /// <summary>
        /// Выполняет взятие блокировки по операции на запись.
        /// </summary>
        /// <param name="operationID">Идентификатор операции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Результат выполнения взятия блокировки <see cref="ValidationResult" />.
        /// В случае наличия ошибок, содержит их описание, иначе - свидетельствует об успешном взятии блокировки.
        /// </returns>
        Task<ValidationResult> ObtainWriterLockAsync(
            Guid operationID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Освобождает блокировку по операции на запись.
        /// </summary>
        /// <param name="operationID">Идентификатор операции.</param>
        /// <returns>
        /// Результат освобождения блокировки <see cref="ValidationResult" />.
        /// В случае наличия ошибок, содержит их описание, иначе - свидетельствует об успешном освобождении блокировки.
        /// </returns>
        Task<ValidationResult> ReleaseWriterLockAsync(Guid operationID);

        /// <summary>
        /// Выполняет взятие блокировки по операции на чтение.
        /// </summary>
        /// <param name="operationID">Идентификатор операции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Результат выполнения взятия блокировки <see cref="ValidationResult" />.
        /// В случае наличия ошибок, содержит их описание, иначе - свидетельствует об успешном взятии блокировки.
        /// </returns>
        Task<ValidationResult> ObtainReaderLockAsync(
            Guid operationID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет взятие блокировки по операции на чтение без ожидания, если параллельно взята блокировка на запись.
        /// </summary>
        /// <param name="operationID">Идентификатор операции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>Success</c> - <c>true</c>, если блокировка успешно взята, <c>false</c>, если её взятие не выполнено.<br/>
        /// <c>ValidationResult</c> - содержит ошибки при взятии блокировки. Результат может быть успешен, а в свойстве <c>Success</c> возвращено <c>false</c>, если блокировка не взята, т.к. параллельно была установлена блокировка на запись.
        /// </returns>
        Task<(bool Success, ValidationResult Result)> TryObtainReaderLockNoWaitAsync(
            Guid operationID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Освобождает блокировку по операции на чтение.
        /// </summary>
        /// <param name="operationID">Идентификатор операции.</param>
        /// <returns>
        /// Результат освобождения блокировки <see cref="ValidationResult" />.
        /// В случае наличия ошибок, содержит их описание, иначе - свидетельствует об успешном освобождении блокировки.
        /// </returns>
        Task<ValidationResult> ReleaseReaderLockAsync(Guid operationID);

        /// <summary>
        /// Выполняет эскалацию блокировки на чтение до блокировки на запись.
        /// </summary>
        /// <param name="operationID">Идентификатор операции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Результат выполнения эскалации блокировки <see cref="ValidationResult" />.
        /// В случае наличия ошибок, содержит их описание, иначе - свидетельствует об успешной эскалации блокировки.
        /// </returns>
        Task<ValidationResult> EscalateReaderLockAsync(
            Guid operationID,
            CancellationToken cancellationToken = default);
    }
}
