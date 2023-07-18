#nullable enable

using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Результат проверки доступа к файлам через <see cref="IKrPermissionsFilesManager"/>.
    /// </summary>
    public interface IKrPermissionsFilesManagerResult
    {
        /// <summary>
        /// Рассчитанные настройки доступа к файлу.
        /// </summary>
        IDictionary<KrPermissionsFileAccessSettingFlag, int?> AccessSettings { get; }

        /// <summary>
        /// Ограничение на размер файла в байтах.
        /// </summary>
        long? FileSizeLimit { get; }

        /// <summary>
        /// Результат проверки доступа к файлу. Заполняется, если при проверке у контекста установлен флаг <see cref="IKrPermissionsFilesManagerContext.WriteValidationResult"/>.
        /// </summary>
        ValidationResult ValidationResult { get; }
    }
}
