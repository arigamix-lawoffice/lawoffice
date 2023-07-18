#nullable enable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// Кэш файлов, используемый для взаимодействия с OnlyOffice.
    /// </summary>
    public interface IOnlyOfficeFileCache
    {
        /// <summary>
        /// Сохраняет содержимое нового файла в кэш.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл будет доступен в кэше.</param>
        /// <param name="sourceFileVersionID">Идентификатор исходной версии файла в карточке.</param>
        /// <param name="sourceFileName">Исходное имя файла в карточке.</param>
        /// <param name="stream">Поток с содержимым файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<ValidationResult> CreateAsync(
            Guid id,
            Guid sourceFileVersionID,
            string sourceFileName,
            Stream stream,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает содержимое файла из кэша. Выбрасывает исключение, если файл не найден в кэше.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл был сохранён в кэш.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат получения контента, функция, возвращающая контент, и размер содержимого.</returns>
        ValueTask<(ValidationResult Result, Func<CancellationToken, ValueTask<Stream>>? GetContentStreamFunc, long Size)> GetContentAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет содержимое файла из кэша по указанному идентификатору. Выбрасывает исключение, если файл не найден в кэше.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл был сохранён в кэш.</param>
        /// <param name="sourceFileVersionID">
        /// Идентификатор исходной версии файла в карточке
        /// или <c>null</c>, если идентификатор вычисляется внутри метода.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<ValidationResult> DeleteAsync(
            Guid id,
            Guid? sourceFileVersionID = null,
            CancellationToken cancellationToken = default);
    }
}
