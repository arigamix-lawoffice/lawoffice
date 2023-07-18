#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.OnlyOffice.Token
{
    /// <summary>
    /// Перечисление разрешений на операцию для сервиса OnlyOffice.
    /// </summary>
    [Flags]
    public enum OnlyOfficeTokenPermissionFlags
    {
        /// <summary>
        /// Разрешения отсутствуют.
        /// </summary>
        None = 0,

        /// <summary>
        /// Разрешение на загрузку файла.
        /// </summary>
        Get = 1,

        /// <summary>
        /// Разрешение обращение к обработчику статусов.
        /// </summary>
        /// <remarks>
        /// Не используется
        /// </remarks>
        Callback = 2
    }
}
