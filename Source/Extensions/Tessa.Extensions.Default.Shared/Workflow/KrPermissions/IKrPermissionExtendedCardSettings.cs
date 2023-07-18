using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Объект с расширенными настройками доступа к карточке.
    /// </summary>
    public interface IKrPermissionExtendedCardSettings
    {
        /// <summary>
        /// Метод для установки доступа к полям секции.
        /// </summary>
        /// <param name="isAllowed">Определяет, должны ли данные поля быть доступными или нет.</param>
        /// <param name="sectionID">Идентификатор секции, для которой устанавливаются права.</param>
        /// <param name="fields">Идентификаторы полей, для которых устанавливаются права. Если не заданы, то доступ устанаваливается для всей секции.</param>
        void SetCardAccess(
            bool isAllowed,
            Guid sectionID,
            ICollection<Guid> fields);

        /// <summary>
        /// Метод для получения расширенных настроек для секций карточки.
        /// </summary>
        /// <returns>Cписок расширенных настроек для секций карточки.</returns>
        ICollection<IKrPermissionSectionSettings> GetCardSettings();

        /// <summary>
        /// Метод для получения расширенных настроек для секций заданий по типам заданий.
        /// </summary>
        /// <returns>Списки расширенных настроек для секций заданий по типам заданий.</returns>
        Dictionary<Guid, ICollection<IKrPermissionSectionSettings>> GetTasksSettings();

        /// <summary>
        /// Метод для получения настроек видимости контролов, блоков и вкладок.
        /// </summary>
        /// <returns>Список настроек видимости контролов, блоков и вкладок.</returns>
        ICollection<KrPermissionVisibilitySettings> GetVisibilitySettings();

        /// <summary>
        /// Метод для получения настроек доступа к файлам.
        /// </summary>
        /// <returns>Список настроек доступа к файлам.</returns>
        ICollection<KrPermissionsFileSettings> GetFileSettings();

        /// <summary>
        /// Метод для получения настроек доступа файлов текущего сотрудника.
        /// </summary>
        /// <returns>Настройки доступа файлов текущего сотрудника или <c>null</c>, если они не заданы.</returns>
        KrPermissionsFilesSettings TryGetOwnFilesSettings();

        /// <summary>
        /// Метод для получения настроек доступа файлов других сотрудников.
        /// </summary>
        /// <returns>Настройки доступа файлов других сотрудников или <c>null</c>, если они не заданы.</returns>
        KrPermissionsFilesSettings TryGetOtherFilesSettings();
    }
}
