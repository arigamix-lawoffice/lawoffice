using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ObjectLocking;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Стратегия по управлению блокировками правил доступа.
    /// </summary>
    public interface IKrPermissionsObjectLockingStrategy :
        IObjectLockingStrategy
    {
        /// <summary>
        /// Удаляет все блокировки для заданного объекта.
        /// </summary>
        /// <param name="objectID">Идентификатор объекта, для которого удаляется блокировка.</param>
        /// <param name="objectPrefix">
        /// Обязательный ненулевой строковый префикс, характеризующий тип объекта.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task ClearLocksAsync(
            Guid objectID,
            string objectPrefix,
            CancellationToken cancellationToken = default);
    }
}
