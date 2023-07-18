#nullable enable

using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Правило для проверки доступа к файлу по его идентификатору.
    /// </summary>
    public sealed class KrPermissionsDirectFileRule : IKrPermissionsFileRule
    {
        #region Properties

        /// <summary>
        /// Идентификатор файла, для которого выполняется данное правило.
        /// </summary>
        public Guid FileID { get; init; }

        #endregion

        #region IKrPermissionFileRule Properties

        /// <inheritdoc/>
        public int Priority { get; init; }

        /// <inheritdoc/>
        public int? AddAccessSetting { get; init; }

        /// <inheritdoc/>
        public int? ReadAccessSetting { get; init; }

        /// <inheritdoc/>
        public int? EditAccessSetting { get; init; }

        /// <inheritdoc/>
        public int? DeleteAccessSetting { get; init; }

        /// <inheritdoc/>
        public int? SignAccessSetting { get; init; }

        /// <inheritdoc/>
        public long? FileSizeLimit { get; init; }

        #endregion

        #region IKrPermissionFileRule Methods

        /// <inheritdoc/>
        public ValueTask<bool> CheckFileAsync(IKrPermissionsFilesManagerContext context)
        {
            ThrowIfNull(context);

            // Правила проверки конкретных файлов не учитываются при проверке добавления или замены файла, т.к. при замене файла применимые к нему ранее настройки могут быть не актуальны
            return ValueTask.FromResult(
                context.RequriedAccessFlags.HasNot(KrPermissionsFileAccessSettingFlag.Add)
                && context.FileID == this.FileID);
        }

        #endregion
    }
}
