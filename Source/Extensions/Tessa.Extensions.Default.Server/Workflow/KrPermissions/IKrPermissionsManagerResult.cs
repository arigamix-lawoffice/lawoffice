using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Результат расчёта прав доступа в <see cref="IKrPermissionsManager"/>.
    /// </summary>
    public interface IKrPermissionsManagerResult
    {
        /// <summary>
        /// Версия правил доступа
        /// </summary>
        long Version { get; }

        /// <summary>
        /// Определяет, были ли запрошены права на редактирование.
        /// </summary>
        bool WithExtendedSettings { get; }

        /// <summary>
        /// Набор рассчитанных прав
        /// </summary>
        ICollection<KrPermissionFlagDescriptor> Permissions { get; }

        /// <summary>
        /// Набор прав доступа к секциям карточки
        /// </summary>
        HashSet<Guid, IKrPermissionSectionSettings> ExtendedCardSettings { get; }

        /// <summary>
        /// Набор прав доступа к секциям заданий, разбитых по типам заданий
        /// </summary>
        Dictionary<Guid, HashSet<Guid, IKrPermissionSectionSettings>> ExtendedTasksSettings { get; }

        /// <summary>
        /// Набор правил доступа для файлов
        /// </summary>
        ICollection<IKrPermissionsFileRule> FileRules { get; }

        /// <summary>
        /// Определяет, что в результате есть данный флаг
        /// </summary>
        /// <param name="krPermission">Проверяемый флаг доступа. Разворачивает виртуальные флаги</param>
        /// <returns>Возвращает true, если доступ есть, иначе false</returns>
        bool Has(KrPermissionFlagDescriptor krPermission);

        /// <summary>
        /// Создает расширенные настройки прав карточки по результату расчета прав доступа.
        /// Если при расчете прав не использовались расширенные настройки проверки прав доступа, то метод вернет <c>null</c>.
        /// </summary>
        /// <param name="userID">Идентификатор сотрудника, для которого создаются расширенные настройки доступа.</param>
        /// <param name="card">Карточка, для которой создаются расширенные настройки доступа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Возвращает расширенные настройки прав доступа по карточке или <c>null</c>, если при расчете прав доступа не запрашивались расширенные настройки прав доступа.
        /// </returns>
        ValueTask<IKrPermissionExtendedCardSettings> CreateExtendedCardSettingsAsync(
            Guid userID,
            Card card,
            CancellationToken cancellationToken = default);
    }
}
