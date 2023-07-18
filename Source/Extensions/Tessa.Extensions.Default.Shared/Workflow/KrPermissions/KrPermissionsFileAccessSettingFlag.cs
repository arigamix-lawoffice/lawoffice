using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Перечисление настроек доступа к файлам.
    /// </summary>
    [Flags]
    public enum KrPermissionsFileAccessSettingFlag
    {
        /// <summary>
        /// Пустая настройка доступа.
        /// </summary>
        None = 0,

        /// <summary>
        /// Доступ на добавление файла.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Доступ на чтение файла.
        /// </summary>
        Read = 2,

        /// <summary>
        /// Доступ на редактирование файла.
        /// </summary>
        Edit = 4,

        /// <summary>
        /// Доступ на удаление файла.
        /// </summary>
        Delete = 8,

        /// <summary>
        /// Доступ на подпись файла.
        /// </summary>
        Sign = 16,

        /// <summary>
        /// Все настройки доступа.
        /// </summary>
        All = Read | Edit | Delete | Sign,
    }
}
