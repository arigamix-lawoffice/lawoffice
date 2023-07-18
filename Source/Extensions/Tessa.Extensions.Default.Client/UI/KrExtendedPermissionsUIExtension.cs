#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI.CardFiles;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Platform.IO;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using FileInfo = Tessa.UI.Files.FileInfo;

namespace Tessa.Extensions.Default.Client.UI
{
    public sealed class KrExtendedPermissionsUIExtension : CardUIExtension
    {
        #region Constructors

        public KrExtendedPermissionsUIExtension(ICardControlAdditionalInfoRegistry controlRegistry) =>
            this.controlRegistry = NotNullOrThrow(controlRegistry);

        #endregion

        #region Fields

        private readonly ICardControlAdditionalInfoRegistry controlRegistry;

        #endregion

        #region Nested Types

        private record VisibilitySetting(string Text, bool IsHidden, bool IsPattern);

        private class VisibilitySettings
        {
            #region Fields

            public List<VisibilitySetting>? BlockSettings;
            public List<VisibilitySetting>? ControlSettings;
            public List<VisibilitySetting>? TabSettings;

            #endregion

            #region Public Methods

            public void Fill(ICollection<KrPermissionVisibilitySettings> visibilitySettings)
            {
                foreach (var visibilitySetting in visibilitySettings)
                {
                    List<VisibilitySetting> patternList;
                    switch (visibilitySetting.ControlType)
                    {
                        case KrPermissionsHelper.ControlType.Tab:
                            patternList = this.TabSettings ??= new List<VisibilitySetting>();
                            break;

                        case KrPermissionsHelper.ControlType.Block:
                            patternList = this.BlockSettings ??= new List<VisibilitySetting>();
                            break;

                        case KrPermissionsHelper.ControlType.Control:
                            patternList = this.ControlSettings ??= new List<VisibilitySetting>();
                            break;
                        default: continue;
                    }

                    FillSettings(patternList, visibilitySetting);
                }
            }

            #endregion

            #region Private Methods

            private static void FillSettings(List<VisibilitySetting> patternList, KrPermissionVisibilitySettings visibilitySetting)
            {
                string alias = visibilitySetting.Alias;
                int length = alias.Length;
                bool wildStart = alias[0] == '*',
                    wildEnd = alias[length - 1] == '*';

                if (wildStart || wildEnd)
                {
                    string escapedAlias =
                        Regex.Escape(alias.Substring(wildStart ? 1 : 0, Math.Max(0, length - (wildStart ? 1 : 0) - (wildEnd ? 1 : 0))));
                    patternList.Add(new VisibilitySetting(escapedAlias, visibilitySetting.IsHidden, true));
                }
                else
                {
                    patternList.Add(new VisibilitySetting(alias, visibilitySetting.IsHidden, false));
                }
            }

            #endregion
        }

        private class PermissionsControlsVisitor
        {
            #region Constructors

            public PermissionsControlsVisitor(ICardControlAdditionalInfoRegistry controlRegistry) =>
                this.controlRegistry = controlRegistry;

            #endregion

            #region Fields

            private readonly ICardControlAdditionalInfoRegistry controlRegistry;
            private readonly HashSet<IFormWithBlocksViewModel> formsForRearrange = new();
            private readonly HashSet<Guid> hideSections = new();
            private readonly HashSet<Guid> hideFields = new();
            private readonly HashSet<Guid> showFields = new();
            private readonly HashSet<Guid> mandatorySections = new();
            private readonly HashSet<Guid> mandatoryFields = new();
            private readonly HashSet<Guid> disallowedSections = new();

            private VisibilitySettings? visibilitySettings;
            private HashSet<Guid, KrPermissionsFileSettings>? fileSettings;
            private KrPermissionsFilesSettings? ownFilesSettings;
            private KrPermissionsFilesSettings? otherFilesSettings;
            private bool canAddFiles;
            private bool canSignFiles;

            #endregion

            #region Public Methods

            public async Task VisitAsync(ICardModel cardModel, Stack<GridViewModel> parentGrids)
            {
                this.formsForRearrange.Clear();

                bool disableGrids = false;
                if (cardModel.Table is not null
                    && cardModel.Table.Row.State != CardRowState.Inserted)
                {
                    foreach (var grid in parentGrids)
                    {
                        // Если хотя бы одна из родительских таблиц недоступна для редактирования через KrPermissions, то и все таблицы текущей строки должны быть недоступны.
                        var parentGridSource = this.controlRegistry.GetSourceInfo(grid.CardTypeControl);
                        if (this.disallowedSections.Contains(parentGridSource.SectionID))
                        {
                            disableGrids = true;
                            break;
                        }
                    }
                }

                if (this.ownFilesSettings is not null || this.otherFilesSettings is not null)
                {
                    cardModel.FileContainer.ContainerFileAdded += this.OnFileAdded;
                }

                foreach (var control in cardModel.ControlBag)
                {
                    this.VisitControl(control, cardModel);
                    if (disableGrids
                        && control is GridViewModel grid)
                    {
                        grid.IsReadOnly = true;
                    }
                }

                foreach (var block in cardModel.BlockBag)
                {
                    this.VisitBlock(block);
                }

                foreach (var form in cardModel.FormBag)
                {
                    await this.VisitFormAsync(cardModel, form);
                }

                foreach (var form in this.formsForRearrange)
                {
                    form.Rearrange();
                }
            }

            public void Fill(
                KrToken token,
                ICollection<IKrPermissionSectionSettings>? sectionSettings,
                VisibilitySettings visibilitySettings,
                ICollection<KrPermissionsFileSettings>? fileSettings,
                KrPermissionsFilesSettings? ownFilesSettings,
                KrPermissionsFilesSettings? otherFilesSettings)
            {
                if (sectionSettings is not null)
                {
                    foreach (var sectionSetting in sectionSettings)
                    {
                        if (sectionSetting.IsHidden)
                        {
                            this.hideSections.Add(sectionSetting.ID);
                        }
                        else
                        {
                            this.hideFields.AddRange(sectionSetting.HiddenFields);
                        }

                        this.showFields.AddRange(sectionSetting.VisibleFields);

                        if (sectionSetting.IsMandatory)
                        {
                            this.mandatorySections.Add(sectionSetting.ID);
                        }
                        else
                        {
                            this.mandatoryFields.AddRange(sectionSetting.MandatoryFields);
                        }

                        if (sectionSetting.IsDisallowed)
                        {
                            this.disallowedSections.Add(sectionSetting.ID);
                        }
                    }
                }

                this.visibilitySettings = visibilitySettings;
                this.fileSettings = fileSettings is null
                    ? null
                    : new HashSet<Guid, KrPermissionsFileSettings>(x => x.FileID, fileSettings);
                this.ownFilesSettings = ownFilesSettings;
                this.otherFilesSettings = otherFilesSettings;
                this.canAddFiles = token.Permissions.Contains(KrPermissionFlagDescriptors.AddFiles);
                this.canSignFiles = token.Permissions.Contains(KrPermissionFlagDescriptors.SignFiles);
            }

            #endregion

            #region Private Methods

            private async Task VisitFormAsync(ICardModel model, IFormWithBlocksViewModel form)
            {
                if (!string.IsNullOrWhiteSpace(form.Name))
                {
                    var hidden = CheckIsHidden(this.visibilitySettings!.TabSettings, form.Name);
                    if (hidden == true)
                    {
                        await HideFormAsync(model, form);
                    }
                    // Нет возможности добавлять скрытую форму, т.к. она не была сгенерирована
                }
            }

            private void VisitBlock(IBlockViewModel block)
            {
                if (!string.IsNullOrWhiteSpace(block.Name))
                {
                    var hidden = CheckIsHidden(this.visibilitySettings!.BlockSettings, block.Name);
                    if (hidden.HasValue)
                    {
                        if (hidden.Value)
                        {
                            this.HideBlock(block);
                        }
                        else
                        {
                            this.ShowBlock(block);
                        }
                    }
                }
            }

            private void VisitControl(
                IControlViewModel control,
                ICardModel cardModel)
            {
                var sourceInfo = this.controlRegistry.GetSourceInfo(control.CardTypeControl);
                if ((this.hideSections.Contains(sourceInfo.SectionID) && !sourceInfo.ColumnIDs.Any(x => this.showFields.Contains(x)))
                    || sourceInfo.ColumnIDs.Any(x => this.hideFields.Contains(x)))
                {
                    this.HideControl(control);
                }

                if (!string.IsNullOrWhiteSpace(control.Name))
                {
                    var hidden = CheckIsHidden(this.visibilitySettings!.ControlSettings, control.Name);

                    if (hidden.HasValue)
                    {
                        if (hidden.Value)
                        {
                            this.HideControl(control);
                        }
                        else
                        {
                            this.ShowControl(control);
                        }
                    }
                }

                if (this.mandatorySections.Contains(sourceInfo.SectionID)
                    || sourceInfo.ColumnIDs.Any(x => this.mandatoryFields.Contains(x)))
                {
                    MakeControlMandatory(control);
                }

                if (!this.canAddFiles
                    || this.fileSettings is { Count: > 0 }
                    || this.ownFilesSettings is not null
                    || this.otherFilesSettings is not null)
                {
                    if (control is FileListViewModel { FileControl: { } fileControl })
                    {
                        this.SetFileControlActions(fileControl);
                    }

                    if (control is CardViewControlViewModel { Name: not null } viewControl
                        && FilesViewGeneratorBaseUIExtension.TryGetFileControl(cardModel.Info, viewControl.Name) is { } fileControl2)
                    {
                        this.SetFileControlActions(fileControl2);
                    }
                }
            }

            private void SetFileControlActions(IFileControl fileControl)
            {
                var prevModifyFileSelectAsync = fileControl.ModifyFileSelectAsync;
                fileControl.ModifyFileSelectAsync = prevModifyFileSelectAsync is null
                    ? this.ModifyFileSelectAsync
                    : async context =>
                    {
                        await prevModifyFileSelectAsync(context);
                        await this.ModifyFileSelectAsync(context);
                    };

                var prevValidateFileCategoryAsync = fileControl.ValidateFileCategoryAsync;
                fileControl.ValidateFileCategoryAsync = prevValidateFileCategoryAsync is null
                    ? this.ValidateFileCategoryAsync
                    : async context =>
                    {
                        await prevValidateFileCategoryAsync(context);
                        await this.ValidateFileCategoryAsync(context);
                    };

                var prevValidateFileContentAsync = fileControl.ValidateFileContentAsync;
                fileControl.ValidateFileContentAsync = prevValidateFileContentAsync is null
                    ? this.ValidateFileContentAsync
                    : async context =>
                    {
                        await prevValidateFileContentAsync(context);
                        await this.ValidateFileContentAsync(context);
                    };

                var prevCategoryFilterAsync = fileControl.CategoryFilterAsync;
                fileControl.CategoryFilterAsync = prevCategoryFilterAsync is null
                    ? this.CategoryFilterAsync
                    : async context =>
                    {
                        var prevResult = await prevCategoryFilterAsync(context);
                        var newContext = new FileCategoryFilterContext(prevResult.ToArray(), context.FileInfos)
                        {
                            CancellationToken = context.CancellationToken,
                            IsManualCategoriesCreationDisabled = context.IsManualCategoriesCreationDisabled,
                        };
                        var result = await this.CategoryFilterAsync(newContext);
                        context.IsManualCategoriesCreationDisabled = newContext.IsManualCategoriesCreationDisabled;
                        return result;
                    };
            }

            private async ValueTask ModifyFileSelectAsync(IFileSelectContext context)
            {
                var filesSettings = SelectFilesSettings(context.ReplaceFile);

                IEnumerable<string>? allowedExtensions = null;
                if (context.ReplaceFile is null)
                {
                    if (!this.canAddFiles
                        && filesSettings is not null
                        && (filesSettings.GlobalSettings is null
                            || !filesSettings.GlobalSettings.AddAllowed
                            && filesSettings.GlobalSettings.AllowedCategories is not { Count: > 0 }))
                    {
                        allowedExtensions = filesSettings.TryGetExtensionSettings()?
                            .Where(x => x.Value.AddAllowed || x.Value.AllowedCategories is { Count: > 0 })
                            .Select(x => x.Key);
                    }
                }
                else
                {
                    var categoryID = GetCategoryID(context.ReplaceFile.Category);
                    bool needFilter;

                    if (filesSettings?.GlobalSettings is not null)
                    {
                        if (filesSettings.GlobalSettings.AddAllowed)
                        {
                            needFilter = categoryID is not null
                                && filesSettings.GlobalSettings.DisallowedCategories?.Contains(categoryID.Value) == true;
                        }
                        else if (filesSettings.GlobalSettings.AddDisallowed)
                        {
                            needFilter = categoryID is null
                                || filesSettings.GlobalSettings.AllowedCategories?.Contains(categoryID.Value) != true;
                        }
                        else
                        {
                            needFilter = categoryID is null
                                ? !this.canAddFiles
                                : filesSettings.GlobalSettings.AllowedCategories?.Contains(categoryID.Value) != true;
                        }
                    }
                    else
                    {
                        needFilter = !this.canAddFiles;
                    }

                    if (needFilter)
                    {
                        var currentFileExtension = FileHelper.GetExtension(context.ReplaceFile.InitialState.Name).TrimStart('.');
                        if (filesSettings?.TryGetExtensionSettings() is { Count: > 0 } extensionSettings)
                        {
                            var filterFunc =
                                categoryID is null
                                    ? new Func<KrPermissionsFileExtensionSettings, bool>(x => x.AddAllowed)
                                    : (x => x.AddAllowed
                                        && x.DisallowedCategories?.Contains(categoryID.Value) != true
                                        || x.AllowedCategories?.Contains(categoryID.Value) == true);

                            var allowedExtensionsList = extensionSettings
                                .Where(x => filterFunc(x.Value))
                                .Select(x => x.Key)
                                .ToList();

                            if (!allowedExtensionsList.Contains(currentFileExtension))
                            {
                                allowedExtensionsList.Add(currentFileExtension);
                            }

                            allowedExtensions = allowedExtensionsList;
                        }
                        else
                        {
                            allowedExtensions = new[] { currentFileExtension };
                        }
                    }
                }

                if (allowedExtensions is not null)
                {
                    context.SelectFileDialog.Filter = $"{await LocalizeAsync("$KrPermissions_AllowedFilesFilter")}|*." +
                        string.Join(
                            ";*.",
                            allowedExtensions);
                }
            }

            private async ValueTask<IEnumerable<IFileCategory>> CategoryFilterAsync(IFileCategoryFilterContext context)
            {
                if (this.ownFilesSettings is null
                    && this.otherFilesSettings is null)
                {
                    return context.Categories;
                }

                var ownFiles = new List<FileInfo>();
                var otherFiles = new List<FileInfo>();

                foreach (var fileInfo in context.FileInfos)
                {
                    if (IsOwnFile(fileInfo.File))
                    {
                        ownFiles.Add(fileInfo);
                    }
                    else
                    {
                        otherFiles.Add(fileInfo);
                    }
                }

                var checkExtensionSettings = new List<KrPermissionsFileExtensionSettings>();
                if (this.ownFilesSettings is not null
                    && ownFiles.Count > 0)
                {
                    if (this.ownFilesSettings.TryGetExtensionSettings() is { Count: > 0 } extensionsSettings)
                    {
                        var extensions = ownFiles
                            .Select(x => GetExtension(x.FileName))
                            .Distinct(StringComparer.Ordinal)
                            .ToArray();

                        foreach (var extension in extensions)
                        {
                            if (extensionsSettings.TryGetValue(extension, out var extensionSettings))
                            {
                                checkExtensionSettings.Add(extensionSettings);
                            }
                        }
                    }

                    if (this.ownFilesSettings.GlobalSettings is not null)
                    {
                        checkExtensionSettings.Add(this.ownFilesSettings.GlobalSettings);
                    }
                }

                if (this.otherFilesSettings is not null
                    && otherFiles.Count > 0)
                {
                    if (this.otherFilesSettings.TryGetExtensionSettings() is { Count: > 0 } extensionsSettings)
                    {
                        var extensions = otherFiles
                            .Select(x => GetExtension(x.FileName))
                            .Distinct(StringComparer.Ordinal)
                            .ToArray();

                        foreach (var extension in extensions)
                        {
                            if (extensionsSettings.TryGetValue(extension, out var extensionSettings))
                            {
                                checkExtensionSettings.Add(extensionSettings);
                            }
                        }
                    }

                    if (this.otherFilesSettings.GlobalSettings is not null)
                    {
                        checkExtensionSettings.Add(this.otherFilesSettings.GlobalSettings);
                    }
                }

                var filteredCategories = context.Categories
                    .Where(category =>
                    {
                        bool result = this.canAddFiles;
                        foreach (var extensionSettings in checkExtensionSettings)
                        {
                            var categoryAllowed = IsCategoryAllowed(extensionSettings, category);
                            if (categoryAllowed == false)
                            {
                                return false;
                            }
                            else if (categoryAllowed == true)
                            {
                                result = true;
                            }
                        }

                        return result;
                    })
                    .ToArray();

                // Если ручной ввод разрешен и есть хотя бы одна запрещённая категория для добавления, то запрещаем ручной ввод категории
                if (!context.IsManualCategoriesCreationDisabled
                    && context.Categories.Count != filteredCategories.Length)
                {
                    context.IsManualCategoriesCreationDisabled = true;
                }

                return filteredCategories;
            }

            private ValueTask ValidateFileCategoryAsync(IFileValidateCategoryContext context)
            {
                var filesSettings = SelectFilesSettings(context.FileInfo.File);
                if (filesSettings is null)
                {
                    return ValueTask.CompletedTask;
                }

                var fileExtension = GetExtension(context.FileInfo.FileName);
                bool? categoryAllowed = null,
                    sizeChecked = null;
                long sizeLimit = 0;

                if (filesSettings.TryGetExtensionSettings()?.TryGetValue(fileExtension, out var extensionSettings) == true)
                {
                    categoryAllowed = IsCategoryAllowed(extensionSettings, context.Category);
                    sizeChecked = CheckFileSize(extensionSettings, context.Category, context.FileInfo.FileSize, out sizeLimit);
                }

                if (filesSettings.GlobalSettings is { } globalSettings)
                {
                    categoryAllowed ??= IsCategoryAllowed(globalSettings, context.Category);
                    sizeChecked ??= CheckFileSize(globalSettings, context.Category, context.FileInfo.FileSize, out sizeLimit);
                }

                if (!(categoryAllowed ?? this.canAddFiles))
                {
                    KrPermissionsHelper.AddFileValidationError(
                        context.ValidationResult,
                        this,
                        context.FileInfo.File is null
                            ? KrPermissionsHelper.KrPermissionsErrorAction.AddFile
                            : KrPermissionsHelper.KrPermissionsErrorAction.ChangeCategory,
                        KrPermissionsHelper.KrPermissionsErrorType.NotAllowed,
                        context.FileInfo.FileName,
                        fileExtension: fileExtension,
                        categoryCaption: context.Category?.Caption);
                }
                else if (sizeChecked == false)
                {
                    KrPermissionsHelper.AddFileValidationError(
                        context.ValidationResult,
                        this,
                        context.FileInfo.File is null
                            ? KrPermissionsHelper.KrPermissionsErrorAction.AddFile
                            : KrPermissionsHelper.KrPermissionsErrorAction.ChangeCategory,
                        KrPermissionsHelper.KrPermissionsErrorType.FileTooBig,
                        context.FileInfo.FileName,
                        fileExtension: fileExtension,
                        categoryCaption: context.Category?.Caption,
                        sizeLimit: sizeLimit);
                }

                return ValueTask.CompletedTask;
            }

            private ValueTask ValidateFileContentAsync(IFileValidateContentContext context)
            {
                if (context.FileInfo.File is not null)
                {
                    return this.ValidateFileContentReplaceAsync(context);
                }
                else
                {
                    return this.ValidateFileContentAsync(context, false);
                }
            }

            private ValueTask ValidateFileContentReplaceAsync(IFileValidateContentContext context)
            {
                var file = context.FileInfo.File!;
                var fileExtension = GetExtension(context.FileInfo.FileName);
                var oldFileExtension = GetExtension(file.InitialState.Name);
                var fileCategoryID = GetCategoryID(file.Category);
                var oldFileCategoryID = GetCategoryID(file.InitialState.Category);

                // Если при замене файла расширение файла не поменялось, то проверяем только размер файла по настройкам конкретного файла, если они заданы.
                // Иначе делаем проверку по настройкам добавления файла.
                if (fileExtension.Equals(oldFileExtension, StringComparison.OrdinalIgnoreCase)
                    && fileCategoryID == oldFileCategoryID)
                {
                    if (this.fileSettings?.TryFirst(x => x.FileID == context.FileInfo.File!.ID, out var replaceFileSettings) == true
                        && replaceFileSettings.FileSizeLimit.HasValue
                        && context.FileInfo.FileSize > replaceFileSettings.FileSizeLimit.Value)
                    {
                        KrPermissionsHelper.AddFileValidationError(
                            context.ValidationResult,
                            this,
                            KrPermissionsHelper.KrPermissionsErrorAction.ReplaceFile,
                            KrPermissionsHelper.KrPermissionsErrorType.FileTooBig,
                            context.FileInfo.FileName,
                            fileExtension,
                            file.Name,
                            file.Category?.Caption,
                            replaceFileSettings.FileSizeLimit.Value);
                    }
                }
                else
                {
                    return this.ValidateFileContentAsync(context, true);
                }

                return ValueTask.CompletedTask;
            }

            private ValueTask ValidateFileContentAsync(
                IFileValidateContentContext context,
                bool checkOnFileReplace)
            {
                var filesSettings = SelectFilesSettings(context.FileInfo.File);
                if (filesSettings is null)
                {
                    return ValueTask.CompletedTask;
                }

                var fileExtension = GetExtension(context.FileInfo.FileName);
                bool hasAccess;
                long sizeLimit = 0;
                var replaceFile = context.FileInfo.File;

                // Если файл новый и категория не известна, значит проверяем, что есть хотя бы одна возможная настройка добалвения файла.
                if (replaceFile is null)
                {
                    if (filesSettings.TryGetExtensionSettings()?.TryGetValue(fileExtension, out var extensionSettings) != true
                        || extensionSettings is null
                        || (!extensionSettings.AddAllowed
                            && extensionSettings is { AddDisallowed: false, AllowedCategories: null, DisallowedCategories: null }))
                    {
                        extensionSettings = filesSettings.GlobalSettings;
                    }

                    hasAccess =
                        extensionSettings is null
                            ? this.canAddFiles
                            : extensionSettings.AddAllowed
                            || extensionSettings.AllowedCategories is { Count: > 0 }
                            || (this.canAddFiles && !extensionSettings.AddDisallowed);
                }
                else
                {
                    bool? categoryAllowed = null,
                        sizeChecked = null;

                    if (filesSettings.TryGetExtensionSettings()?.TryGetValue(fileExtension, out var extensionSettings) == true)
                    {
                        categoryAllowed = IsCategoryAllowed(extensionSettings, replaceFile.Category);
                        sizeChecked = CheckFileSize(extensionSettings, replaceFile.Category, context.FileInfo.FileSize, out sizeLimit);
                    }

                    if (filesSettings.GlobalSettings is { } globalSettings)
                    {
                        categoryAllowed ??= IsCategoryAllowed(globalSettings, replaceFile.Category);
                        sizeChecked ??= CheckFileSize(globalSettings, replaceFile.Category, context.FileInfo.FileSize, out sizeLimit);
                    }

                    // Доступ есть, если:
                    // 1) Категория доступна для добавления по настройкам или, если настроек для категории нет, добавление доступно глобально.
                    // 2) При проверке ограничений размера не было превышения размера.
                    hasAccess = categoryAllowed ?? this.canAddFiles && sizeChecked != false;
                }

                if (!hasAccess)
                {
                    KrPermissionsHelper.KrPermissionsErrorType errorType;
                    KrPermissionsHelper.KrPermissionsErrorAction errorAction;

                    if (sizeLimit > 0)
                    {
                        errorType = KrPermissionsHelper.KrPermissionsErrorType.FileTooBig;
                        errorAction = KrPermissionsHelper.KrPermissionsErrorAction.ReplaceFile;
                    }
                    else if (checkOnFileReplace)
                    {
                        errorType = KrPermissionsHelper.KrPermissionsErrorType.NotAllowed;
                        errorAction = KrPermissionsHelper.KrPermissionsErrorAction.ReplaceFile;
                    }
                    else
                    {
                        errorType = KrPermissionsHelper.KrPermissionsErrorType.NotAllowed;
                        errorAction = KrPermissionsHelper.KrPermissionsErrorAction.AddFile;
                    }

                    KrPermissionsHelper.AddFileValidationError(
                        context.ValidationResult,
                        this,
                        errorAction,
                        errorType,
                        context.FileInfo.FileName,
                        fileExtension,
                        replaceFile?.Name,
                        replaceFile?.Category?.Caption,
                        sizeLimit);
                }

                return ValueTask.CompletedTask;
            }

            private static bool? CheckFileSize(
                KrPermissionsFileExtensionSettings extensionSettings,
                IFileCategory? category,
                long fileSize,
                out long sizeLimit)
            {
                sizeLimit = 0;
                if (extensionSettings.TryGetSizeLimitSettings() is not { Count: > 0 } fileSizeLimitSettings)
                {
                    return null;
                }

                var categoryID = GetCategoryID(category);
                if ((categoryID is not null
                        && fileSizeLimitSettings.TryFirst(x => x.Categories?.Contains(categoryID.Value) == true, out var fileSizeSetting))
                    || fileSizeLimitSettings.TryFirst(x => x!.Categories is null, out fileSizeSetting))
                {
                    Debug.Assert(fileSizeSetting is not null);
                    sizeLimit = fileSizeSetting.Limit;
                    return fileSize <= fileSizeSetting.Limit;
                }

                return null;
            }

            /// <summary>
            /// Проверяет настройки доступа для данной категории файла.
            /// </summary>
            /// <param name="extensionSettings">Настройки доступа для расширения.</param>
            /// <param name="category">Категория файла.</param>
            /// <returns>
            /// Значение <c>true</c>, если настройки доступа разрешают данную категорию;
            /// <c>false</c>, если настройки доступа запрещают данную категорию;
            /// <c>null</c>, если настройки доступа не регламентируют доступ для данной категории.</returns>
            private static bool? IsCategoryAllowed(
                KrPermissionsFileExtensionSettings extensionSettings,
                IFileCategory? category)
            {
                var categoryID = GetCategoryID(category);

                // Вручную введённая категория доступна только, если все категории доступны.
                if (categoryID is null)
                {
                    if (extensionSettings.AddDisallowed
                        || extensionSettings.DisallowedCategories is { Count: > 0 })
                    {
                        return false;
                    }
                    else if (extensionSettings.AddAllowed)
                    {
                        return true;
                    }
                }
                else if (extensionSettings.AddAllowed)
                {
                    return extensionSettings.DisallowedCategories?.Contains(categoryID.Value) != true;
                }
                else if (extensionSettings.AddDisallowed)
                {
                    return extensionSettings.AllowedCategories?.Contains(categoryID.Value) == true;
                }
                else if (extensionSettings.AllowedCategories?.Contains(categoryID.Value) == true)
                {
                    return true;
                }
                else if (extensionSettings.DisallowedCategories?.Contains(categoryID.Value) == true)
                {
                    return false;
                }

                return null;
            }

            private async void OnFileAdded(object? sender, FileControlEventArgs e)
            {
                var file = e.File;

                var deferral = e.Defer();
                try
                {
                    await file.Permissions.SetCanSignAsync(this.IsSignAllowed(file));

                    PropertyChangedEventManager.AddHandler(
                        file,
                        this.OnFileCategoryOrNameChanged,
                        string.Empty);
                }
                catch (Exception ex)
                {
                    deferral.SetException(ex);
                }
                finally
                {
                    deferral.Dispose();
                }
            }

            private async void OnFileCategoryOrNameChanged(object? sender, PropertyChangedEventArgs e)
            {
                var file = NotNullOrThrow((IFile?) sender);
                if (file.Permissions.IsSealed)
                {
                    return;
                }

                switch (e.PropertyName)
                {
                    case nameof(IFile.Category):
                    case nameof(IFile.Name):
                        await file.Permissions.SetCanSignAsync(this.IsSignAllowed(file));
                        break;
                }
            }

            private bool IsSignAllowed(IFile file)
            {
                var filesSettings = SelectFilesSettings(file);
                if (filesSettings is null)
                {
                    return this.canSignFiles;
                }

                var categoryID = GetCategoryID(file.Category);
                var fileExtension = GetExtension(file.Name);

                if (filesSettings.TryGetExtensionSettings()?.TryGetValue(fileExtension, out var extensionSettings) == true
                    && IsSignAllowed(extensionSettings, categoryID) is { } signAllowed)
                {
                    return signAllowed;
                }

                if (filesSettings.GlobalSettings is { } globalSettings)
                {
                    return IsSignAllowed(globalSettings, categoryID) ?? this.canSignFiles;
                }

                return this.canSignFiles;
            }

            private static bool? IsSignAllowed(
                KrPermissionsFileExtensionSettings extensionSettings,
                Guid? categoryID)
            {
                // Вручную введённая категория доступна только, если все категории доступны.
                if (categoryID is null)
                {
                    return extensionSettings.SignAllowed;
                }
                else if (extensionSettings.SignAllowed)
                {
                    return extensionSettings.SignDisallowedCategories?.Contains(categoryID.Value) != true;
                }
                else if (extensionSettings.SignDisallowed)
                {
                    return extensionSettings.SignAllowedCategories?.Contains(categoryID.Value) == true;
                }
                else if (extensionSettings.SignAllowedCategories?.Contains(categoryID.Value) == true)
                {
                    return true;
                }
                else if (extensionSettings.SignDisallowedCategories?.Contains(categoryID.Value) == true)
                {
                    return false;
                }

                return null;
            }

            private void HideControl(IControlViewModel controlViewModel)
            {
                controlViewModel.ControlVisibility = Visibility.Collapsed;
                this.formsForRearrange.Add(controlViewModel.Block.Form);
            }

            private void ShowControl(IControlViewModel controlViewModel)
            {
                controlViewModel.ControlVisibility = Visibility.Visible;
                this.formsForRearrange.Add(controlViewModel.Block.Form);
            }

            private void HideBlock(IBlockViewModel blockViewModel)
            {
                blockViewModel.BlockVisibility = Visibility.Collapsed;
                this.formsForRearrange.Add(blockViewModel.Form);
            }

            private void ShowBlock(IBlockViewModel blockViewModel)
            {
                blockViewModel.BlockVisibility = Visibility.Visible;
                this.formsForRearrange.Add(blockViewModel.Form);
            }

            private static async Task HideFormAsync(ICardModel model, IFormWithBlocksViewModel formViewModel)
            {
                if (formViewModel.CardTypeForm.IsTopLevelForm())
                {
                    model.Forms.Remove(formViewModel);
                }
                else
                {
                    await formViewModel.CloseAsync();
                }
            }

            private static void MakeControlMandatory(IControlViewModel controlViewModel)
            {
                controlViewModel.IsRequired = true;
                controlViewModel.RequiredText = LocalizationManager.Format("$KrPermissions_MandatoryControlTemplate", controlViewModel.Caption);
            }

            private static bool? CheckIsHidden(
                List<VisibilitySetting>? settings,
                string checkName)
            {
                if (settings is not null)
                {
                    foreach (var setting in settings)
                    {
                        if (setting.IsPattern)
                        {
                            if (Regex.IsMatch(checkName, setting.Text))
                            {
                                return setting.IsHidden;
                            }
                        }
                        else
                        {
                            if (string.Equals(checkName, setting.Text, StringComparison.OrdinalIgnoreCase))
                            {
                                return setting.IsHidden;
                            }
                        }
                    }
                }

                return null;
            }

            private static Guid? GetCategoryID(
                IFileCategory? category)
            {
                // Для файлов без категории используем постоянную константу в качестве идентификатора категории.
                return category is null
                    ? KrPermissionsHelper.NoCategoryFilesCategoryID
                    : category.ID;
            }

            private static string GetExtension(string fileName) =>
                Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant();

            private KrPermissionsFilesSettings? SelectFilesSettings(IFile? file) =>
                this.IsOwnFile(file) ? this.ownFilesSettings : this.otherFilesSettings;

            private bool IsOwnFile(IFile? file) =>
                file is null
                || this.fileSettings?.TryGetItem(file.ID, out var settings) != true
                || settings?.OwnFile == true;

            #endregion
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var token = KrToken.TryGet(context.Card.Info);

            // Если не используется типовое решение
            if (token is null)
            {
                return;
            }

            var cardControlsVisitor = new PermissionsControlsVisitor(this.controlRegistry);

            // Набор визиторов по гридам
            Dictionary<GridViewModel, PermissionsControlsVisitor> visitors = new Dictionary<GridViewModel, PermissionsControlsVisitor>();
            // Стек текущих открытых таблиц
            Stack<GridViewModel> parentGridStack = new Stack<GridViewModel>();

            // Инициализация видимости контролов по визитору
            async Task InitModelAsync(ICardModel initModel, PermissionsControlsVisitor visitor)
            {
                await visitor.VisitAsync(initModel, parentGridStack);

                foreach (var control in initModel.ControlBag)
                {
                    if (control is GridViewModel grid)
                    {
                        visitors[grid] = visitor;
                        grid.RowInitializing += RowInitializing;
                        grid.RowEditorClosed += RowClosed;
                    }
                }
            }

            // Инициализация вдимости контролов по визитору при открытии строки таблицы
            async void RowInitializing(object? sender, GridRowEventArgs e)
            {
                var deferral = e.Defer();
                try
                {
                    if (e.RowModel is not null
                        && sender is GridViewModel grid)
                    {
                        parentGridStack.Push(e.Control);
                        await InitModelAsync(e.RowModel, visitors[grid]);
                    }
                }
                catch (Exception ex)
                {
                    deferral.SetException(ex);
                }
                finally
                {
                    deferral.Dispose();
                }
            }

            // Отписка от созданных подписок при закрытии строки грида
            void RowClosed(object? sender, GridRowEventArgs e)
            {
                if (e.RowModel is not null)
                {
                    parentGridStack.Pop();
                    foreach (var control in e.RowModel.ControlBag)
                    {
                        if (control is GridViewModel grid)
                        {
                            visitors.Remove(grid);
                            grid.RowInitializing -= RowInitializing;
                            grid.RowEditorClosed -= RowClosed;
                        }
                    }
                }
            }

            var extendedSettings = token.ExtendedCardSettings;
            var sectionSettings = extendedSettings?.GetCardSettings();
            var tasksSettings = extendedSettings?.GetTasksSettings();
            var visibilitySettings = extendedSettings?.GetVisibilitySettings();
            var fileSettings = extendedSettings?.GetFileSettings();
            var ownFilesSettings = extendedSettings?.TryGetOwnFilesSettings();
            var otherFilesSettings = extendedSettings?.TryGetOtherFilesSettings();

            var model = context.Model;

            var uiVisibilitySettings = new VisibilitySettings();

            if (visibilitySettings is not null)
            {
                uiVisibilitySettings.Fill(visibilitySettings);
            }

            cardControlsVisitor.Fill(
                token,
                sectionSettings,
                uiVisibilitySettings,
                fileSettings,
                ownFilesSettings,
                otherFilesSettings);

            await InitModelAsync(model, cardControlsVisitor);
            if (tasksSettings is { Count: > 0 })
            {
                await model.ModifyTasksAsync(async (tvm, m) =>
                {
                    if (!tasksSettings.TryGetValue(tvm.TaskModel.CardTask.TypeID, out var taskSettings))
                    {
                        return;
                    }

                    var taskVisitor = new PermissionsControlsVisitor(this.controlRegistry);
                    taskVisitor.Fill(
                        token,
                        taskSettings,
                        uiVisibilitySettings,
                        fileSettings,
                        ownFilesSettings,
                        otherFilesSettings);

                    await tvm.ModifyWorkspaceAsync(async (t, b) => { await InitModelAsync(t.TaskModel, taskVisitor); });
                });
            }
        }

        #endregion
    }
}
