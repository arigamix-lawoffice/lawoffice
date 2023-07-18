#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <inheritdoc />
    public sealed class OnlyOfficeFileCacheInfo :
        IOnlyOfficeFileCacheInfo
    {
        #region IOnlyOfficeFileCacheInfo Members

        /// <inheritdoc />
        public Guid ID { get; set; }

        /// <inheritdoc />
        public Guid SourceFileVersionID { get; set; }

        /// <inheritdoc />
        public Guid CreatedByID { get; set; }

        /// <inheritdoc />
        public string SourceFileName { get; set; } = String.Empty;

        /// <inheritdoc />
        public string? ModifiedFileUrl { get; set; }

        /// <inheritdoc />
        public DateTime? LastModifiedFileUrlTime { get; set; }

        /// <inheritdoc />
        public DateTime LastAccessTime { get; set; }

        /// <inheritdoc />
        public bool? HasChangesAfterClose { get; set; }

        /// <inheritdoc />
        public bool EditorWasOpen { get; set; }

        /// <inheritdoc />
        public string? CoeditKey { get; set; }

        #endregion
    }
}
