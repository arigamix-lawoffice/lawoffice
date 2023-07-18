using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Флаг доступа новых файлов.
    /// </summary>
    [Flags]
    public enum KrPermissionsFilesAccessFlag
    {
        /// <summary>
        /// Нет настроек доступа.
        /// </summary>
        None = 0,

        /// <summary>
        /// Добавление разрешено.
        /// </summary>
        AddAllowed = 1,

        /// <summary>
        /// Добавление запрещено.
        /// </summary>
        AddProhibited = 2,

        /// <summary>
        /// Подписание разрешено.
        /// </summary>
        SignAllowed = 4,

        /// <summary>
        /// Подписание запрещено.
        /// </summary>
        SignProhibited = 8,
    }
}
