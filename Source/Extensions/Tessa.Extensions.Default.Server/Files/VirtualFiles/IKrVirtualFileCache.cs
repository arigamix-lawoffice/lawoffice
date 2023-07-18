using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    /// <summary>
    /// Кеш виртуальных файлов
    /// </summary>
    public interface IKrVirtualFileCache
    {
        /// <summary>
        /// Метод для получения всех виртуальных файлов из кеша. Загружает данные из базы при первом вызове.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает все виртуальные файлы из кеша</returns>
        ValueTask<IKrVirtualFile[]> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для получения виртуального файла по его ID. Загружает данные из базы при первом вызове.
        /// Если файл отсутствует по идентификатору, то возвращает <c>null</c>.
        /// </summary>
        /// <param name="virtualFileID">Идентификатор виртуального файла</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает виртуальный файл из кеша</returns>
        ValueTask<IKrVirtualFile> TryGetAsync(Guid virtualFileID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для получения списка идентификаторов типов карточек, которые могут иметь виртуальные файлы
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает список идентификаторов типов карточек, которые могут иметь виртуальные файлы</returns>
        ValueTask<Guid[]> GetAllowedTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для сброса кеша виртуальных файлов
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task InvalidateAsync(CancellationToken cancellationToken = default);
    }
}
