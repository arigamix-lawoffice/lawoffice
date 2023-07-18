#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <inheritdoc cref="IKrPermissionsFilesManager"/>
    public sealed class KrPermissionsFilesManager : IKrPermissionsFilesManager
    {
        #region Fields

        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        public KrPermissionsFilesManager(
            IDbScope dbScope)
        {
            this.dbScope = NotNullOrThrow(dbScope);
        }

        #endregion

        #region IKrPermissionsFilesManager Implementation

        /// <inheritdoc/>
        public async ValueTask<IKrPermissionsFilesManagerResult> CheckPermissionsAsync(IKrPermissionsFilesManagerContext context)
        {
            var result = new KrPermissionsFilesManagerResult();

            var stillRequried = ParseAccessFlags(context.RequriedAccessFlags);

            if (stillRequried.Count == 0
                || context.OrderedRules.Count == 0)
            {
                return result;
            }

            bool withReplace = await this.HasCheckAttributeChangesAsync(context);
            if (withReplace)
            {
                stillRequried.Remove(KrPermissionsFileAccessSettingFlag.Sign);
            }

            await CheckRulesAsync(
                context,
                result,
                stillRequried);

            if (withReplace
                && context.StoreFile is not null
                && context.RequriedAccessFlags.HasAny(KrPermissionsFileAccessSettingFlag.Edit | KrPermissionsFileAccessSettingFlag.Sign)
                && result.AccessSettings.All(x => x.Value != KrPermissionsHelper.FileEditAccessSettings.Disallowed))
            {
                var newRequiredFlag = context.RequriedAccessFlags;
                if (context.RequriedAccessFlags.Has(KrPermissionsFileAccessSettingFlag.Edit))
                {
                    newRequiredFlag |= KrPermissionsFileAccessSettingFlag.Add;
                    newRequiredFlag &= ~KrPermissionsFileAccessSettingFlag.Edit;
                }

                context.SetFile(context.StoreFile, context.VersionID, context.StoreFile, newRequiredFlag);
                result.FileSizeLimit = null;

                await CheckRulesAsync(
                    context,
                    result,
                    ParseAccessFlags(newRequiredFlag));
            }

            if (context.WriteValidationResult)
            {
                result.ValidationResult = await this.GetValidationResultAsync(context, result, withReplace);
            }

            return result;
        }

        #endregion

        #region Private Methods

        private static async ValueTask CheckRulesAsync(
            IKrPermissionsFilesManagerContext context,
            KrPermissionsFilesManagerResult result,
            IList<KrPermissionsFileAccessSettingFlag> stillRequried)
        {
            var currentPriority = context.OrderedRules[0].Priority;

            // Определяем лимит файла только, если было запрошено добавление или изменение файла.
            var sizeChecked = !context.RequriedAccessFlags.HasAny(KrPermissionsFileAccessSettingFlag.Add | KrPermissionsFileAccessSettingFlag.Edit);
            foreach (var fileRule in context.OrderedRules)
            {
                if (currentPriority != fileRule.Priority)
                {
                    currentPriority = fileRule.Priority;
                    for (var i = stillRequried.Count - 1; i >= 0; i--)
                    {
                        if (result.AccessSettings.ContainsKey(stillRequried[i]))
                        {
                            stillRequried.RemoveAt(i);
                        }
                    }

                    sizeChecked |= result.FileSizeLimit.HasValue;

                    // Если все настройки уже определены, то нет смысла проверять правила с более низким приоритетом.
                    if (stillRequried.Count == 0
                        && sizeChecked)
                    {
                        break;
                    }
                }

                if (!await fileRule.CheckFileAsync(context))
                {
                    continue;
                }

                foreach (var requiredFlag in stillRequried)
                {
                    var ruleSettings = requiredFlag switch
                    {
                        KrPermissionsFileAccessSettingFlag.Add => fileRule.AddAccessSetting,
                        KrPermissionsFileAccessSettingFlag.Read => fileRule.ReadAccessSetting,
                        KrPermissionsFileAccessSettingFlag.Edit => fileRule.EditAccessSetting,
                        KrPermissionsFileAccessSettingFlag.Delete => fileRule.DeleteAccessSetting,
                        KrPermissionsFileAccessSettingFlag.Sign => fileRule.SignAccessSetting,
                        _ => null,
                    };

                    if (ruleSettings is not null
                        && (!result.AccessSettings.TryGetValue(requiredFlag, out var currentSettings)
                            || currentSettings is null
                            || ruleSettings <= currentSettings.Value))
                    {
                        result.AccessSettings[requiredFlag] = ruleSettings.Value;
                    }
                }


                if (!sizeChecked
                    && fileRule.FileSizeLimit.HasValue
                    && (result.FileSizeLimit is null
                        || fileRule.FileSizeLimit.Value < result.FileSizeLimit.Value))
                {
                    result.FileSizeLimit = fileRule.FileSizeLimit;
                }
            }

            for (int i = 0; i < stillRequried.Count; i++)
            {
                result.AccessSettings.TryAdd(stillRequried[i], null);
            }
        }

        private static IList<KrPermissionsFileAccessSettingFlag> ParseAccessFlags(KrPermissionsFileAccessSettingFlag requriedAccessFlags)
        {
            if (requriedAccessFlags == KrPermissionsFileAccessSettingFlag.None)
            {
                return Array.Empty<KrPermissionsFileAccessSettingFlag>();
            }

            var result = new List<KrPermissionsFileAccessSettingFlag>();

            var requriedAccessFlagsInt = Math.Min((int) requriedAccessFlags, (int) KrPermissionsFileAccessSettingFlag.All);

            var currentFlag = 1;
            while (requriedAccessFlagsInt > 0)
            {
                if (requriedAccessFlagsInt % 2 == 1)
                {
                    result.Add((KrPermissionsFileAccessSettingFlag) currentFlag);
                }
                requriedAccessFlagsInt >>= 1;
                currentFlag *= 2;
            }

            return result;
        }

        private async ValueTask<ValidationResult> GetValidationResultAsync(
            IKrPermissionsFilesManagerContext context,
            IKrPermissionsFilesManagerResult result,
            bool withReplace)
        {
            ValidationResultBuilder? validationResult = null;
            string? fileName = null;

            foreach (var (accessFlag, accessSetting) in result.AccessSettings)
            {
                switch (accessFlag, accessSetting)
                {
                    case (KrPermissionsFileAccessSettingFlag.Add, KrPermissionsHelper.FileEditAccessSettings.Disallowed):
                        (validationResult ??= new()).AddError(
                            this,
                            withReplace ? "$KrPermissions_Messages_FailToEditFile" : "$KrPermissions_Messages_FailToAddFile",
                            fileName ??= await this.GetFileNameAsync(context));
                        break;

                    case (KrPermissionsFileAccessSettingFlag.Edit, KrPermissionsHelper.FileEditAccessSettings.Disallowed):
                        (validationResult ??= new()).AddError(
                            this,
                            "$KrPermissions_Messages_FailToEditFile",
                            fileName ??= await this.GetFileNameAsync(context));
                        break;

                    case (KrPermissionsFileAccessSettingFlag.Delete, KrPermissionsHelper.FileEditAccessSettings.Disallowed):
                        (validationResult ??= new()).AddError(
                            this,
                            "$KrPermissions_Messages_FailToDeleteFile",
                            fileName ??= await this.GetFileNameAsync(context));
                        break;

                    case (KrPermissionsFileAccessSettingFlag.Sign, KrPermissionsHelper.FileEditAccessSettings.Disallowed):
                        (validationResult ??= new()).AddError(
                            this,
                            "$KrPermissions_Messages_FailToSignFile",
                            fileName ??= await this.GetFileNameAsync(context));
                        break;

                    case (KrPermissionsFileAccessSettingFlag.Read, _):
                        switch (accessSetting)
                        {
                            case KrPermissionsHelper.FileReadAccessSettings.FileNotAvailable:
                                (validationResult ??= new()).AddError(
                                    this,
                                    "$KrPermissions_Messages_FailToLoadFile",
                                    context.FileID.ToString());
                                break;

                            case KrPermissionsHelper.FileReadAccessSettings.ContentNotAvailable:
                                (validationResult ??= new()).AddError(
                                    this,
                                    "$KrPermissions_Messages_FailToLoadFile",
                                    fileName ??= await this.GetFileNameAsync(context));
                                break;

                            case KrPermissionsHelper.FileReadAccessSettings.OnlyLastVersion:
                            case KrPermissionsHelper.FileReadAccessSettings.OnlyLastAndOwnVersions:
                                if (context.VersionID.HasValue
                                    && !await this.CheckVersionOwnerAsync(
                                        context.FileID,
                                        context.VersionID.Value,
                                        context.Session.User.ID,
                                        accessSetting == KrPermissionsHelper.FileReadAccessSettings.OnlyLastAndOwnVersions,
                                        context.CancellationToken))
                                {
                                    (validationResult ??= new()).AddError(
                                        this,
                                        "$KrPermissions_Messages_FailToLoadFile",
                                        fileName ??= await this.GetFileNameAsync(context));
                                }
                                break;
                        }
                        break;
                }
            }

            if (validationResult is null
                && result.FileSizeLimit.HasValue
                && context.StoreFile is not null)
            {
                if (context.StoreFile.Size > result.FileSizeLimit)
                {
                    KrPermissionsHelper.AddFileValidationError(
                        validationResult ??= new(),
                        this,
                        withReplace
                            ? KrPermissionsHelper.KrPermissionsErrorAction.ReplaceFile
                            : context.StoreFile.State == CardFileState.Inserted
                                ? KrPermissionsHelper.KrPermissionsErrorAction.AddFile
                                : KrPermissionsHelper.KrPermissionsErrorAction.EditFile,
                        KrPermissionsHelper.KrPermissionsErrorType.FileTooBig,
                        context.StoreFile.Name,
                        replacedFileName: withReplace ? await this.GetFileNameFromDbAsync(context) : null,
                        categoryCaption: context.StoreFile.CategoryCaption,
                        sizeLimit: result.FileSizeLimit);
                }
            }

            return validationResult?.Build() ?? ValidationResult.Empty;
        }

        private ValueTask<string?> GetFileNameAsync(IKrPermissionsFilesManagerContext context)
        {
            if (context.VersionID is not null)
            {
                return this.GetFileVersionNameAsync(context, context.VersionID.Value);
            }

            var fileName = context.StoreFile?.Name ?? context.File?.Name;
            if (!string.IsNullOrEmpty(fileName))
            {
                return new(fileName);
            }

            return this.GetFileNameFromDbAsync(context);
        }

        private async ValueTask<string?> GetFileNameFromDbAsync(IKrPermissionsFilesManagerContext context)
        {
            await using var _ = this.dbScope.Create();

            var db = this.dbScope.Db;
            var builder = this.dbScope.BuilderFactory;

            return await db
                .SetCommand(
                    builder
                        .Select().C("Name").From("Files").NoLock().Where().C("RowID").Equals().P("FileID")
                        .Build(),
                    db.Parameter("FileID", context.FileID))
                .LogCommand()
                .ExecuteAsync<string>(context.CancellationToken);
        }

        private async ValueTask<string?> GetFileVersionNameAsync(IKrPermissionsFilesManagerContext context, Guid versionID)
        {
            var fileName = context.StoreFile?.Versions.FirstOrDefault(x => x.RowID == versionID)?.Name
                ?? context.StoreFile?.Name
                ?? context.File?.Versions.FirstOrDefault(x => x.RowID == versionID)?.Name;

            if (!string.IsNullOrEmpty(fileName))
            {
                return fileName;
            }

            await using var _ = this.dbScope.Create();

            var db = this.dbScope.Db;
            var builder = this.dbScope.BuilderFactory;

            return await db
                .SetCommand(
                    builder
                        .Select().C("Name").From("FileVersions").NoLock().Where().C("RowID").Equals().P("VersionID")
                        .Build(),
                    db.Parameter("VersionID", versionID))
                .LogCommand()
                .ExecuteAsync<string>(context.CancellationToken);
        }

        private async Task<bool> CheckVersionOwnerAsync(
            Guid fileID,
            Guid fileVersionID,
            Guid userID,
            bool checkOwnVersions,
            CancellationToken cancellationToken)
        {
            await using var _ = this.dbScope.Create();

            var db = this.dbScope.Db;
            var builder = this.dbScope.BuilderFactory
                    .Select().V(true)
                    .From("Files", "f").NoLock()
                    .If(checkOwnVersions, b1 => b1
                    .InnerJoin("FileVersions", "fv").NoLock()
                        .On().C("f", "RowID").Equals().C("fv", "ID")
                            .And().C("fv", "RowID").Equals().P("FileVersionID"))
                    .Where().C("f", "RowID").Equals().P("FileID")
                        .And().E(b => b
                            .C("f", "VersionRowID").Equals().P("FileVersionID")
                            .If(checkOwnVersions, b1 => b1
                            .Or().C("fv", "CreatedByID").Equals().P("UserID")));

            return await db.SetCommand(
                builder.Build(),
                db.Parameter("FileID", fileID, LinqToDB.DataType.Guid),
                db.Parameter("FileVersionID", fileVersionID, LinqToDB.DataType.Guid),
                db.Parameter("UserID", userID, LinqToDB.DataType.Guid))
                .ExecuteAsync<bool>(cancellationToken);
        }

        private async ValueTask<bool> HasCheckAttributeChangesAsync(IKrPermissionsFilesManagerContext context)
        {
            var storeFile = context.StoreFile;
            if (storeFile is null)
            {
                return false;
            }
            if (storeFile.Flags.Has(CardFileFlags.UpdateCategory))
            {
                return true;
            }
            else if (storeFile.Flags.Has(CardFileFlags.UpdateName))
            {
                var newExtension = FileHelper.GetExtension(storeFile.Name).TrimStart('.').ToLower();
                var oldExtension = context.File is null
                    ? context.FileInfo.TryGet<string>("FileExtension")
                        ?? await this.GetFileExtensionAsync(context.FileID, context.CancellationToken)
                    : System.IO.Path.GetExtension(storeFile.Name);

                if (!newExtension.Equals(oldExtension, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private async ValueTask<string> GetFileExtensionAsync(Guid fileID, CancellationToken cancellationToken)
        {
            await using var _ = this.dbScope.Create();

            var db = this.dbScope.Db;
            var builder = this.dbScope.BuilderFactory;

            var fileName = await db.SetCommand(
                builder
                    .Select().C("Name")
                    .From("Files").NoLock()
                    .Where().C("RowID").Equals().P("FileID")
                    .Build(),
                db.Parameter("FileID", fileID, LinqToDB.DataType.Guid))
                .ExecuteAsync<string>(cancellationToken);

            return string.IsNullOrEmpty(fileName) ? string.Empty : System.IO.Path.GetExtension(fileName);
        }

        #endregion
    }
}
