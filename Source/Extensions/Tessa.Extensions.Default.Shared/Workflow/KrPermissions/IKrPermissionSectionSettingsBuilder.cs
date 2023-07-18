using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Билдер настроек секции с учетом приоритетов правил доступа.
    /// </summary>
    public interface IKrPermissionSectionSettingsBuilder
    {
        /// <summary>
        /// Идентификатор секции, для которой строятся настройки.
        /// </summary>
        Guid SectionID { get; }

        /// <summary>
        /// Метод для добавления настроек секции с указанием приоритета.
        /// </summary>
        /// <param name="settings">Настройки секции.</param>
        /// <param name="prioroty">Приоритет добавляемых настроек.</param>
        /// <returns>Текущий билдер.</returns>
        IKrPermissionSectionSettingsBuilder Add(IKrPermissionSectionSettings settings, int prioroty = 0);

        /// <summary>
        /// Возвращает результирующие настройки секций с учетом всех настроек и приоритетов.
        /// </summary>
        /// <returns>Результирующие настройки секции.</returns>
        IKrPermissionSectionSettings Build();
    }
}
