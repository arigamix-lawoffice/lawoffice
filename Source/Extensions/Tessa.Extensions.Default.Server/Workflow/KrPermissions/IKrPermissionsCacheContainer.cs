using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Контейнер кеша правил доступа.
    /// </summary>
    public interface IKrPermissionsCacheContainer
    {
        /// <summary>
        /// Метод для получения объекта кеша правил доступа. Возвращает <see langword="null"/> и пишет ошибку в <paramref name="validationResult"/>, если не удалось получить объект кеша.
        /// </summary>
        /// <param name="validationResult">Результат валидации, куда пишется ошибка, если неудалось загрузить кеш.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает объект кеша правил доступа, или <see langword="null"/>, если не удалось получить объект кеша.</returns>
        ValueTask<IKrPermissionsCache> TryGetCacheAsync(IValidationResultBuilder validationResult, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для получения номера текущей версии кеша правил доступа.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает номер текущей версии кеша правил доступа.</returns>
        ValueTask<long> GetVersionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для обновления версии кеша.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task UpdateVersionAsync(CancellationToken cancellationToken = default);
    }
}
