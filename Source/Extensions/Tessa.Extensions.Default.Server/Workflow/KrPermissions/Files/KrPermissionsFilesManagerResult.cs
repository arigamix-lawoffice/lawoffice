#nullable enable

using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <inheritdoc cref="IKrPermissionsFilesManagerResult"/>
    public sealed class KrPermissionsFilesManagerResult : IKrPermissionsFilesManagerResult
    {
        #region IKrPermissionsFilesManagerResult Implementation

        /// <inheritdoc/>
        public IDictionary<KrPermissionsFileAccessSettingFlag, int?> AccessSettings { get; } = new Dictionary<KrPermissionsFileAccessSettingFlag, int?>();

        /// <inheritdoc/>
        public long? FileSizeLimit { get; set; }

        /// <inheritdoc/>
        public ValidationResult ValidationResult { get; set; } = ValidationResult.Empty;

        #endregion
    }
}
