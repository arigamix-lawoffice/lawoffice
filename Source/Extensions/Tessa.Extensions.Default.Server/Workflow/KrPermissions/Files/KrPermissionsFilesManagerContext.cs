#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <inheritdoc cref="IKrPermissionsFilesManagerContext"/>
    public sealed class KrPermissionsFilesManagerContext : IKrPermissionsFilesManagerContext
    {
        #region Fields

        private Guid fileID;
        private Dictionary<string, object?>? info;
        private Dictionary<string, object?>? fileInfo;

        #endregion

        #region Constructors

        public KrPermissionsFilesManagerContext(
            ISession session,
            KrPermissionsFileAccessSettingFlag requriedAccessFlags,
            IEnumerable<IKrPermissionsFileRule> rules,
            bool writeDisallowedErrors = false)
        {
            this.Session = NotNullOrThrow(session);
            this.RequriedAccessFlags = requriedAccessFlags;
            this.OrderedRules = NotNullOrThrow(rules).OrderByDescending(x => x.Priority).ToImmutableArray();
            this.WriteValidationResult = writeDisallowedErrors;
        }

        #endregion

        #region IKrPermissionsFilesManagerContext Implementation

        /// <inheritdoc/>
        public ISession Session { get; }

        /// <inheritdoc/>
        public KrPermissionsFileAccessSettingFlag RequriedAccessFlags { get; private set; }

        /// <inheritdoc/>
        public bool WriteValidationResult { get; }

        /// <inheritdoc/>
        public IReadOnlyList<IKrPermissionsFileRule> OrderedRules { get; }

        /// <inheritdoc/>
        public CardFile? File { get; private set; }

        /// <inheritdoc/>
        public Guid FileID => this.File?.RowID ?? this.fileID;

        /// <inheritdoc/>
        public Guid? VersionID { get; private set; }

        /// <inheritdoc/>
        public CardFile? StoreFile { get; private set; }

        /// <inheritdoc/>
        public Dictionary<string, object?> FileInfo => this.fileInfo ??= new Dictionary<string, object?>(StringComparer.Ordinal);

        /// <inheritdoc/>
        public Dictionary<string, object?> Info => this.info ??= new Dictionary<string, object?>(StringComparer.Ordinal);

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; init; }

        /// <inheritdoc/>
        public void SetFile(
            CardFile file,
            Guid? versionID = null,
            CardFile? storeFile = null,
            KrPermissionsFileAccessSettingFlag? permissionFileAccessFlags = null)
        {
            this.File = NotNullOrThrow(file);
            this.fileID = file.RowID;
            this.VersionID = versionID;
            this.StoreFile = storeFile;
            this.UpdateForStoreFile();
            if (permissionFileAccessFlags is not null)
            {
                this.RequriedAccessFlags = permissionFileAccessFlags.Value;
            }
            this.fileInfo?.Clear();
        }

        /// <inheritdoc/>
        public void SetFile(
            Guid fileID,
            Guid? versionID = null,
            CardFile? storeFile = null,
            KrPermissionsFileAccessSettingFlag? permissionFileAccessFlags = null)
        {
            this.File = null;
            this.fileID = fileID;
            this.VersionID = versionID;
            this.StoreFile = storeFile;
            this.UpdateForStoreFile();
            if (permissionFileAccessFlags is not null)
            {
                this.RequriedAccessFlags = permissionFileAccessFlags.Value;
            }
            this.fileInfo?.Clear();
        }

        #endregion

        #region Private Methods

        private void UpdateForStoreFile()
        {
            if (this.StoreFile is null)
            {
                return;
            }

            KrPermissionsFileAccessSettingFlag newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.None;
            switch (this.StoreFile.State)
            {
                case CardFileState.Inserted:
                    newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.Add;
                    if (CardSignatureHelper.AnySignatureRow(this.StoreFile, (file, signatureRow) => signatureRow.State == CardRowState.Inserted))
                    {
                        newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.Sign;
                    }
                    this.File = this.StoreFile;
                    break;

                case CardFileState.Deleted:
                    newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.Delete;
                    break;

                case CardFileState.Replaced:
                    newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.Edit;
                    break;

                case CardFileState.Modified:
                    newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.None;
                    if (CardSignatureHelper.AnySignatureRow(this.StoreFile, (file, signatureRow) => signatureRow.State == CardRowState.Inserted))
                    {
                        newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.Sign;
                    }
                    if (this.StoreFile.Flags.HasAny(CardFileFlags.UpdateCategory | CardFileFlags.UpdateName))
                    {
                        newRequriedAccessFlags |= KrPermissionsFileAccessSettingFlag.Edit;
                    }
                    break;

                case CardFileState.ModifiedAndReplaced:
                    newRequriedAccessFlags = KrPermissionsFileAccessSettingFlag.Edit;
                    if (CardSignatureHelper.AnySignatureRow(this.StoreFile, (_, _) => true))
                    {
                        newRequriedAccessFlags |= KrPermissionsFileAccessSettingFlag.Sign;
                    }
                    break;
            }

            this.RequriedAccessFlags = newRequriedAccessFlags;
        }

        #endregion
    }
}
