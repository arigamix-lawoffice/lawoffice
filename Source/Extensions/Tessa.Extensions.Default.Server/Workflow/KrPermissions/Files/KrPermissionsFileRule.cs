#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Правило для проверки доступа к файлу по расширенным настройкам карточки "Правило доступа".
    /// </summary>
    public sealed class KrPermissionsFileRule : IKrPermissionsFileRule
    {
        #region Fields

        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его свойств.
        /// </summary>
        /// <param name="dbScope"><inheritdoc cref="IDbScope" path="/summary"/></param>
        /// <param name="extensions">
        /// <inheritdoc cref="Extensions" path="/summary"/>
        /// Если задан пустой список или <c>null</c>, то правило не выполняет фильтрацию по расширениям.
        /// </param>
        /// <param name="fileCategories">
        /// <inheritdoc cref="Categories" path="/summary"/>
        /// Если задан пустой список или <c>null</c>, то правило не выполняет фильтрацию по категории файла.
        /// </param>
        public KrPermissionsFileRule(
            IDbScope dbScope,
            IEnumerable<string>? extensions,
            IEnumerable<Guid>? fileCategories)
        {
            this.dbScope = NotNullOrThrow(dbScope);

            this.Extensions = extensions is null
                ? EmptyHolder<string>.Collection
                : extensions.ToImmutableHashSet();
            this.Categories = fileCategories is null
                ? EmptyHolder<Guid>.Collection
                : fileCategories.ToImmutableHashSet();
        }

        /// <summary>
        /// Создаёт экземпляр класса с указанием его свойств.
        /// </summary>
        /// <param name="dbScope"><inheritdoc cref="IDbScope" path="/summary"/></param>
        /// <param name="extensions">
        /// Строка, содержащая список расширений, разделённых пробелом.
        /// Если задана пустая строка или <c>null</c>, то правило не выполняет фильтрацию по расширениям.
        /// </param>
        /// <param name="fileCategories">
        /// <inheritdoc cref="Categories" path="/summary"/>
        /// Если задан пустой список или <c>null</c>, то правило не выполняет фильтрацию по категории файла.
        /// </param>
        public KrPermissionsFileRule(
            IDbScope dbScope,
            string? extensions,
            IEnumerable<Guid>? fileCategories)
            : this(dbScope, ParseExtensions(extensions), fileCategories)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Список расширений файлов, для которых выполняется данное правило.
        /// </summary>
        public ICollection<string> Extensions { get; }

        /// <summary>
        /// Список категорий файлов, для которых выполняется данное правило.
        /// </summary>
        public ICollection<Guid> Categories { get; }

        /// <summary>
        /// Флаг определяет, что правило также должно выполняться для собственных файлов пользователя.
        /// </summary>
        public int FileCheckRule { get; init; }

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

            if (context.File is not null)
            {
                return this.CheckWithFileAsync(context);
            }
            else
            {
                return this.CheckWithFileIDAsync(context);
            }
        }

        #endregion

        #region Private Methods

        private ValueTask<bool> CheckWithFileAsync(IKrPermissionsFilesManagerContext context)
        {
            // При проверке категории:
            // 1) Вариант категории "Без категории" рассчитываем как вариант с идентификатором Guid.Empty.
            // 2) Вариант категории, введённый вручную (идентификатор категории null) подходит под правила только при наличии всех категорий
            //    и в случае проверки на добавление с запретом добавления. Запрещено добавлять вручную введённые категории, если есть запрет добавления хотя бы на одну категорию.

            Guid? categoryID = null;
            var result =
                CheckFileCheckRule(this.FileCheckRule, context.File!.Card.CreatedByID == context.Session.User.ID)
                && (this.Extensions.Count == 0
                    || this.Extensions.Contains(GetOrAdd(context.FileInfo, "FileExtension", () => FileHelper.GetExtension(context.File.Name).TrimStart('.'))))
                && (this.Categories.Count == 0
                    || ((categoryID = GetCategoryID(context.File.CategoryID, context.File.CategoryCaption)) is null
                        && context.RequriedAccessFlags.Has(KrPermissionsFileAccessSettingFlag.Add)
                        && this.AddAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Disallowed)
                    || (categoryID is not null && this.Categories.Contains(categoryID.Value)));

            return ValueTask.FromResult(result);
        }

        /// <inheritdoc/>
        private async ValueTask<bool> CheckWithFileIDAsync(IKrPermissionsFilesManagerContext context)
        {
            var (fileExists, isOwnFile, categoryID, extension) = await this.GetFileDataAsync(context);

            return fileExists
                && CheckFileCheckRule(this.FileCheckRule, isOwnFile)
                && (this.Extensions.Count == 0
                    || this.Extensions.Contains(extension))
                && (this.Categories.Count == 0
                    || (categoryID is null
                        && context.RequriedAccessFlags.Has(KrPermissionsFileAccessSettingFlag.Add)
                        && this.AddAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Disallowed)
                    || (categoryID is not null && this.Categories.Contains(categoryID.Value)));
        }

        private async Task<(bool fileExists, bool isOwnFile, Guid? categoryID, string extension)> GetFileDataAsync(IKrPermissionsFilesManagerContext context)
        {
            var fileInfo = context.FileInfo;
            if (fileInfo.TryGetValue("FileExists", out var fileExistsObj)
                && fileExistsObj is bool fileExists)
            {
                return (fileExists, fileInfo.TryGet<bool>("IsOwnFile"), fileInfo.TryGet<Guid?>("CategoryID"), fileInfo.TryGet<string>("FileExtension") ?? string.Empty);
            }

            await using (this.dbScope.Create())
            {

                var db = this.dbScope.Db;
                var builder =
                    this.dbScope.BuilderFactory
                        .Select().Top(1)
                            .C("f", "Name", "CreatedByID", "CategoryID", "CategoryCaption")
                        .From("Files", "f").NoLock()
                        .Where().C("f", "RowID").Equals().P("FileID");

                db.SetCommand(
                    builder.Limit(1).Build(),
                    db.Parameter("FileID", context.FileID, LinqToDB.DataType.Guid))
                    .LogCommand();

                await using var reader = await db.ExecuteReaderAsync(context.CancellationToken);
                if (await reader.ReadAsync(context.CancellationToken))
                {
                    var extension = FileHelper.GetExtension(reader.GetString(0)).TrimStart('.').ToLower();
                    var isOwnFile = reader.GetGuid(1) == context.Session.User.ID;
                    var categoryID = reader.GetNullableGuid(2);
                    var categoryCaption = reader.GetNullableString(3);
                    categoryID = GetCategoryID(categoryID, categoryCaption);

                    fileInfo["FileExists"] = BooleanBoxes.True;
                    fileInfo["IsOwnFile"] = BooleanBoxes.Box(isOwnFile);
                    fileInfo["CategoryID"] = categoryID;
                    fileInfo["FileExtension"] = extension;

                    return (true, isOwnFile, categoryID, extension);
                }
            }

            fileInfo["FileExists"] = BooleanBoxes.False;
            return default;
        }

        private static bool CheckFileCheckRule(
            int fileCheckRule,
            bool isOwnFile)
        {
            return fileCheckRule switch
            {
                KrPermissionsHelper.FileCheckRules.AllFiles => true,
                KrPermissionsHelper.FileCheckRules.FilesOfOtherUsers => !isOwnFile,
                KrPermissionsHelper.FileCheckRules.OwnFiles => isOwnFile,
                _ => false,
            };
        }

        private static T GetOrAdd<T>(
            Dictionary<string, object?> fileInfo,
            string key,
            Func<T> getValueFunc)
        {
            if (fileInfo.TryGetValue(key, out var value)
                && value is T typedValue)
            {
                return typedValue;
            }
            else
            {
                T result = getValueFunc();
                fileInfo[key] = result;
                return result;
            }
        }

        private static IReadOnlyCollection<string>? ParseExtensions(string? extensions)
        {
            if (!string.IsNullOrWhiteSpace(extensions))
            {
                var extensionsHash = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var extension in extensions.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    extensionsHash.Add(extension);
                }

                return extensionsHash;
            }

            return null;
        }

        private static Guid? GetCategoryID(Guid? categoryID, string? categoryCaption)
        {
            return categoryCaption is null
                ? KrPermissionsHelper.NoCategoryFilesCategoryID
                : categoryID;
        }

        #endregion
    }
}
