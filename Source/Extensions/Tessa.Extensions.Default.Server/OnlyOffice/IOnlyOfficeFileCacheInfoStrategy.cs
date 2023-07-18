#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.FileConverters;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// Объект, управляющий информацией по файлу в кэше.
    /// </summary>
    public interface IOnlyOfficeFileCacheInfoStrategy
    {
        /// <summary>
        /// Добавляет указанную информацию о файле в кэш.
        /// </summary>
        /// <param name="info">Информация о закэшированном файле.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task InsertAsync(IOnlyOfficeFileCacheInfo info, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает информацию по файлу в кэше или <c>null</c>, если файл не найден по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл был сохранён в кэш.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Информация по файлу в кэше
        /// или <c>null</c>, если файл не найден по указанному идентификатору.
        /// </returns>
        Task<IOnlyOfficeFileCacheInfo?> TryGetInfoAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет информацию по файлу в кэше. Выбрасывает исключение, если файл не найден по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл был сохранён в кэш.</param>
        /// <param name="newModifiedFileUrl">
        /// Новый URL к отредактированному файлу на сервере документов
        /// или <c>null</c>, если URL не изменяется.
        /// </param>
        /// <param name="hasChangesAfterClose">
        /// Признак того, что после закрытия редактора файл был изменён,
        /// или <c>null</c>, если редактор не был закрыт.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <exception cref="InvalidOperationException">Файл не найден по указанному идентификатору.</exception>
        Task UpdateInfoAsync(
            Guid id,
            string? newModifiedFileUrl,
            bool? hasChangesAfterClose,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Устанавливает флаг "Редактор был открыт" <see cref="IOnlyOfficeFileCacheInfo.EditorWasOpen"/>
        /// для файла в кэше. Выбрасывает исключение, если файл не найден по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл был сохранён в кэш.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <exception cref="InvalidOperationException">Файл не найден по указанному идентификатору.</exception>
        Task UpdateInfoOnEditorOpenedAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет информацию о файле из кэша.
        /// </summary>
        /// <param name="id">Уникальный идентификатор, по которому файл был сохранён в кэш.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Очищает кэш от информации по файлам, доступ к содержимому которых выполнялся раньше заданной даты.
        /// Не удаляет сами файлы, для этого также вызовите <see cref="IFileConverterCache.CleanCacheAsync"/>.
        /// </summary>
        /// <param name="oldestPreviewRequestTime">
        /// Самая поздняя разрешённая дата, когда выполнялось обращение к файлу в кэше.
        /// Вся информация по файлам, к которым обращались раньше это даты, будут удалены из кэша.
        /// Значение <c>null</c> указывает, что из кэша будут удалены все файлы.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task CleanCacheInfoAsync(
            DateTime? oldestPreviewRequestTime = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Создание информации о совместном редактировании.
        /// </summary>
        /// <param name="versionID">Идентификатор файла</param>
        /// <param name="name">Название файла</param>
        /// <param name="userID">Идентификатор текущего пользователя</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task<(IOnlyOfficeFileCacheInfo, bool)> CreateCoeditInfoAsync(Guid versionID, string name, Guid userID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает информацию по файлу в кэше или <c>null</c>, если файл не найден по указанному идентификатору.
        /// </summary>
        /// <param name="coeditKey">Ключ передачи в OnlyOffice</param>
        /// <param name="userID">Идентификатор текущего пользователя</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task<Guid?> TryGetInfoAsync(string? coeditKey, Guid? userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает идентификатор карточки и файла по идентификатору версии файла
        /// </summary>
        /// <param name="fileVersionID">Идентификатор версии файла</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор карточки, идентификатор файла</returns>
        Task<(Guid cardID, Guid fileID)?> TryGetCardIDFileIDByFileVersionIDAsync(Guid fileVersionID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает Список всех версий для файла переданной версии
        /// </summary>
        /// <param name="fileVersionID">Идентификатор версии для совместного редактирования</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Список версий</returns>
        Task<List<OnlyOfficeFileVersion>> GetFileVersionsAsync(Guid fileVersionID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает имеющиеся записи по совместному редактированию для представленных версий файлов
        /// </summary>
        /// <param name="fileVersionIDs">Идентификаторы версии для совместного редактирования</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Список информаций</returns>
        Task<List<OnlyOfficeCurrentCoedit>> GetCurrentCoeditInfosAsync(IEnumerable<Guid> fileVersionIDs, CancellationToken cancellationToken = default);
    }
}
