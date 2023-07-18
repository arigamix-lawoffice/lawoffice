#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Билдер для построению оптимального списка настроек доступа новых файлов по общим настройкам доступа к файлам <see cref="KrPermissionsFileRule"/>.
    /// </summary>
    public sealed class KrPermissionsFilesSettingsBuilder
    {
        #region Nested Types

        private class FileExtensionsAccessSettingsMerger
        {
            #region Fields

            private bool isNew = true;
            private bool isFinished;

            private HashSet<Guid>? allowedCategories;
            private HashSet<Guid>? disallowedCategories;

            public bool? isAllowed;
            public HashSet<Guid>? lockedAllowedCategories;
            public HashSet<Guid>? lockedDisallowedCategories;

            #endregion

            #region Public Methods

            public void AddCategories(
                bool isAllowed,
                ICollection<Guid> categories)
            {
                if (this.isFinished)
                {
                    return;
                }

                if (categories is null or { Count: 0 })
                {
                    this.isAllowed = isAllowed
                        ? this.isAllowed ?? isAllowed
                        : isAllowed;

                    if (isAllowed)
                    {
                        this.allowedCategories = null;
                    }
                    else
                    {
                        this.disallowedCategories = null;
                        this.allowedCategories = null;
                    }
                }
                else if (!isAllowed)
                {
                    this.disallowedCategories ??= new HashSet<Guid>();
                    this.disallowedCategories.UnionWith(categories);
                    this.allowedCategories?.ExceptWith(categories);
                }
                else if (this.isAllowed is null)
                {
                    var categoriesToAdd =
                        this.disallowedCategories is null
                        ? categories
                        : categories.Where(x => !this.disallowedCategories.Contains(x)).ToArray();

                    this.allowedCategories ??= new HashSet<Guid>();
                    this.allowedCategories.UnionWith(categoriesToAdd);
                }
            }

            public void LockCurrentSettings(FileExtensionsAccessSettingsMerger? withoutExtensionMerger)
            {
                if (this.isFinished)
                {
                    return;
                }

                if (withoutExtensionMerger is not null)
                {
                    if (withoutExtensionMerger.isAllowed == false)
                    {
                        // Если у билдера для всех расширений стоит полный запрет, то для текущего правила удаляем все текущие настройки и ставим полный запрет.
                        // Уже рассчитанные разрешения (если таковы имеются) останутся и будут предоставлять доступ.
                        this.isAllowed = false;
                        this.allowedCategories = null;
                        this.disallowedCategories = null;
                        this.isFinished = true;
                    }
                    else if (withoutExtensionMerger.isAllowed == true)
                    {
                        // Если у билдера для всех расширений стоит полное разрешение, то для текущего правила удаляем все текущие настройки разрешения и ставим полное разрешение, если не стоит полный запрет.
                        // Уже рассчитанные разрешения (если таковы имеются) останутся и будут предоставлять доступ.
                        if (this.isAllowed is null)
                        {
                            this.isAllowed = true;
                        }
                        this.allowedCategories = null;
                        this.isFinished = true;
                    }
                    else
                    {
                        // Если у билдера для всех расширений нет полной настройки разрешения, то просто очищаем списки с текущими настройками по условиям глобальных настроек.
                        // Если текущий билдер переходит в режим полного разрешения, то все актуальные глобальные запреты нужно перенести в текущий билдер.
                        // Если текущий билдер переходит в режим полного запрета, то все актуальные глобальные разрешения нужно перенести в текущий билдер.
                        if (withoutExtensionMerger.lockedDisallowedCategories is not null)
                        {
                            this.allowedCategories?.ExceptWith(withoutExtensionMerger.lockedDisallowedCategories);
                            if (this.isAllowed == true)
                            {
                                this.disallowedCategories ??= new HashSet<Guid>();
                                this.disallowedCategories.UnionWith(withoutExtensionMerger.lockedDisallowedCategories);
                            }
                        }
                        if (withoutExtensionMerger.lockedAllowedCategories is not null)
                        {
                            this.disallowedCategories?.ExceptWith(withoutExtensionMerger.lockedAllowedCategories);
                            if (this.isAllowed == false)
                            {
                                this.allowedCategories ??= new HashSet<Guid>();
                                this.allowedCategories.UnionWith(withoutExtensionMerger.lockedAllowedCategories);
                            }
                        }
                    }
                }

                if (this.isNew)
                {
                    this.isNew = false;
                    this.lockedAllowedCategories = this.allowedCategories;
                    this.lockedDisallowedCategories = this.disallowedCategories;
                    if (this.isAllowed.HasValue)
                    {
                        this.isFinished = true;
                    }
                }
                else if (this.isAllowed is null)
                {
                    if (this.disallowedCategories is not null)
                    {
                        this.lockedDisallowedCategories ??= new HashSet<Guid>(this.disallowedCategories.Count);
                        this.lockedDisallowedCategories.UnionWith(
                            this.lockedAllowedCategories is null
                            ? this.disallowedCategories
                            : this.disallowedCategories.Where(x => !this.lockedAllowedCategories.Contains(x)));
                    }
                    if (this.allowedCategories is not null)
                    {
                        this.lockedAllowedCategories ??= new HashSet<Guid>(this.allowedCategories.Count);
                        this.lockedAllowedCategories.UnionWith(
                            this.lockedDisallowedCategories is null
                            ? this.allowedCategories
                            : this.allowedCategories.Where(x => !this.lockedDisallowedCategories.Contains(x)));
                    }
                }
                else if (this.isAllowed.Value)
                {
                    if (this.disallowedCategories is not null)
                    {
                        this.lockedDisallowedCategories?.UnionWith(
                            this.lockedAllowedCategories is null
                            ? this.disallowedCategories
                            : this.disallowedCategories.Where(x => !this.lockedAllowedCategories.Contains(x)));
                    }
                    this.lockedAllowedCategories = null;
                    this.isFinished = true;
                }
                else
                {
                    this.lockedDisallowedCategories = null;
                    this.isFinished = true;
                }

                this.allowedCategories = null;
                this.disallowedCategories = null;
            }


            #endregion
        }

        private class FileExtensionsSizeSettingsMerger
        {
            #region Fields

            private bool isFinished;
            private Dictionary<Guid, long>? sizeLimits;

            public long? globalSizeLimit;
            public Dictionary<Guid, long>? lockedSizeLimits;

            #endregion

            #region Public Methods

            public void AddCategories(
                long sizeLimit,
                ICollection<Guid> categories)
            {
                if (this.isFinished)
                {
                    return;
                }

                if (categories is null or { Count: 0 })
                {
                    if (this.globalSizeLimit is null
                        || this.globalSizeLimit > sizeLimit)
                    {
                        this.globalSizeLimit = sizeLimit;
                    }
                }
                else
                {
                    foreach (var category in categories)
                    {
                        this.sizeLimits ??= new(categories.Count);
                        if (!this.sizeLimits.TryGetValue(category, out long currentSizeLimit)
                            || currentSizeLimit > sizeLimit)
                        {
                            this.sizeLimits[category] = sizeLimit;
                        }
                    }
                }
            }

            public void LockCurrentSettings(FileExtensionsSizeSettingsMerger? withoutExtensionMerger)
            {
                if (this.isFinished)
                {
                    return;
                }

                if (withoutExtensionMerger is not null)
                {
                    if (withoutExtensionMerger.globalSizeLimit is not null)
                    {
                        this.isFinished = true;

                        if (this.globalSizeLimit is not null
                            && this.globalSizeLimit.Value > withoutExtensionMerger.globalSizeLimit.Value)
                        {
                            this.globalSizeLimit = null;

                            this.RemoveExceedSizeLimits(withoutExtensionMerger.globalSizeLimit.Value);
                        }
                    }

                    if (withoutExtensionMerger.lockedSizeLimits is { Count: > 0 }
                        && this.sizeLimits is { Count: > 0 })
                    {
                        foreach (var (category, withoutExtensionSizeLimit) in withoutExtensionMerger.lockedSizeLimits)
                        {
                            if (this.sizeLimits.TryGetValue(category, out var sizeLimit)
                                && sizeLimit > withoutExtensionSizeLimit)
                            {
                                this.sizeLimits.Remove(category);
                            }
                        }
                    }
                }

                if (this.globalSizeLimit is not null)
                {
                    this.isFinished = true;

                    this.RemoveExceedSizeLimits(this.globalSizeLimit.Value);
                }

                if (this.sizeLimits is { Count: > 0 })
                {
                    if (this.lockedSizeLimits is null)
                    {
                        this.lockedSizeLimits = this.sizeLimits;
                    }
                    else
                    {
                        foreach (var (category, size) in this.sizeLimits)
                        {
                            this.lockedSizeLimits.TryAdd(category, size);
                        }
                    }
                }

                this.sizeLimits = null;
            }

            private void RemoveExceedSizeLimits(long sizeLimitToCheck)
            {
                if (this.sizeLimits is not { Count: > 0 })
                {
                    return;
                }

                List<Guid>? categoriesToDelete = null;
                foreach (var (category, sizeLimit) in this.sizeLimits)
                {
                    if (sizeLimit >= sizeLimitToCheck)
                    {
                        categoriesToDelete ??= new();
                        categoriesToDelete.Add(category);
                    }
                }

                if (categoriesToDelete is not null)
                {
                    for (int i = 0; i < categoriesToDelete.Count; i++)
                    {
                        this.sizeLimits.Remove(categoriesToDelete[i]);
                    }
                }
            }

            #endregion
        }

        private class FileExtenionSettingsBuilder
        {
            #region Fields

            private FileExtensionsAccessSettingsMerger? addMerger;
            private FileExtensionsAccessSettingsMerger? signMerger;
            private FileExtensionsSizeSettingsMerger? sizeLimitMerger;

            #endregion

            #region Constructors

            public FileExtenionSettingsBuilder(
                string extension)
            {
                this.Extension = extension;
            }

            #endregion

            #region Properties

            public string Extension { get; }

            #endregion

            #region Public Methods

            public void AddCategories(
                bool? addAllowed,
                bool? signAllowed,
                ICollection<Guid> categories,
                long? fileSizeLimit)
            {
                if (addAllowed is not null)
                {
                    this.addMerger ??= new();
                    this.addMerger.AddCategories(
                        addAllowed.Value,
                        categories);
                }

                if (signAllowed is not null)
                {
                    this.signMerger ??= new();
                    this.signMerger.AddCategories(
                        signAllowed.Value,
                        categories);
                }

                if (fileSizeLimit is not null)
                {
                    this.sizeLimitMerger ??= new();
                    this.sizeLimitMerger.AddCategories(
                        fileSizeLimit.Value,
                        categories);
                }
            }

            public void LockCurrentSettings(FileExtenionSettingsBuilder? withoutExtensionBuilder)
            {
                this.addMerger?.LockCurrentSettings(withoutExtensionBuilder?.addMerger);
                this.signMerger?.LockCurrentSettings(withoutExtensionBuilder?.signMerger);
                this.sizeLimitMerger?.LockCurrentSettings(withoutExtensionBuilder?.sizeLimitMerger);
            }

            public KrPermissionsFileExtensionSettings BuildTo(KrPermissionsFilesSettings settings)
            {
                KrPermissionsFileExtensionSettings result;
                KrPermissionsFilesAccessFlag flag = KrPermissionsFilesAccessFlag.None;
                HashSet<Guid>? disallowedCategories = null;

                if (this.Extension == withoutExtension)
                {
                    settings.GlobalSettings = result = new KrPermissionsFileExtensionSettings(string.Empty);
                }
                else
                {
                    result = settings.ExtensionSettings.Add(this.Extension.ToLower());
                }

                if (this.addMerger is not null)
                {
                    if (this.addMerger.lockedAllowedCategories is { Count: > 0 })
                    {
                        result.AllowedCategories = this.addMerger.lockedAllowedCategories.ToArray();
                    }

                    if (this.addMerger.lockedDisallowedCategories is { Count: > 0 })
                    {
                        disallowedCategories = this.addMerger.lockedDisallowedCategories;
                        result.DisallowedCategories = this.addMerger.lockedDisallowedCategories.ToArray();
                    }

                    switch (this.addMerger.isAllowed)
                    {
                        case true:
                            flag = KrPermissionsFilesAccessFlag.AddAllowed;
                            break;

                        case false:
                            flag = KrPermissionsFilesAccessFlag.AddProhibited;
                            break;
                    }
                }

                if (this.signMerger is not null)
                {
                    if (this.signMerger.lockedAllowedCategories is { Count: > 0 })
                    {
                        var signAllowedCategories = this.signMerger.lockedAllowedCategories.ToArray();

                        if (signAllowedCategories.Length > 0)
                        {
                            result.SignAllowedCategories = signAllowedCategories;
                        }
                    }

                    if (this.signMerger.lockedDisallowedCategories is { Count: > 0 })
                    {
                        var signDisallowedCategories = this.signMerger.lockedDisallowedCategories.ToArray();

                        if (signDisallowedCategories.Length > 0)
                        {
                            result.SignDisallowedCategories = signDisallowedCategories;
                        }
                    }

                    if (this.signMerger.isAllowed == true)
                    {
                        flag |= KrPermissionsFilesAccessFlag.SignAllowed;
                    }
                    else if (this.signMerger.isAllowed == false)
                    {
                        flag |= KrPermissionsFilesAccessFlag.SignProhibited;
                    }
                }

                if (this.sizeLimitMerger is not null)
                {
                    if (this.sizeLimitMerger.globalSizeLimit is not null)
                    {
                        var globalSizeSettings = result.SizeLimitSettings.Add();
                        globalSizeSettings.Limit = this.sizeLimitMerger.globalSizeLimit.Value;
                    }

                    if (this.sizeLimitMerger.lockedSizeLimits is { Count: > 0 })
                    {
                        IEnumerable<KeyValuePair<Guid, long>> fileSizeLimitsToAdd =
                            disallowedCategories is null
                                ? this.sizeLimitMerger.lockedSizeLimits
                                : this.sizeLimitMerger.lockedSizeLimits.Where(x => !disallowedCategories.Contains(x.Key));

                        foreach (var limitWithCategories in fileSizeLimitsToAdd.GroupBy(x => x.Value, x => x.Key))
                        {
                            var sizeSettings = result.SizeLimitSettings.Add();
                            sizeSettings.Limit = limitWithCategories.Key;
                            sizeSettings.Categories = limitWithCategories.ToArray();
                        }
                    }
                }

                result.Flag = flag;

                return result;
            }

            #endregion
        }

        #endregion

        #region Fields

        private Dictionary<int, List<KrPermissionsFileRule>>? rulesByPriority;

        private const string withoutExtension = "*";

        #endregion

        #region Public Methods

        /// <summary>
        /// Добавляет правило проверки доступа к файлам для построения настроек новых файлов.
        /// </summary>
        /// <param name="fileRule"><inheritdoc cref="KrPermissionsFileRule" path="/summary"/></param>
        public void Add(KrPermissionsFileRule fileRule)
        {
            ThrowIfNull(fileRule);

            if (fileRule.AddAccessSetting is null
                && fileRule.SignAccessSetting is null
                && fileRule.FileSizeLimit is null)
            {
                return;
            }

            this.rulesByPriority ??= new();
            if (this.rulesByPriority.TryGetValue(fileRule.Priority, out var rules))
            {
                rules.Add(fileRule);
            }
            else
            {
                this.rulesByPriority[fileRule.Priority] = new List<KrPermissionsFileRule> { fileRule };
            }
        }

        /// <summary>
        /// Выполняет построение настроек доступа на добавление файлов по добавленным правилам.
        /// </summary>
        /// <returns>Настройки доступа на добавление файлов или <c>null</c>, если в правилах проверки доступа файлов нет настроек доступа на добавление файлов.</returns>
        public KrPermissionsFilesSettings? Build()
        {
            if (this.rulesByPriority is null)
            {
                return null;
            }

            HashSet<string, FileExtenionSettingsBuilder> extensionsBuilders = new HashSet<string, FileExtenionSettingsBuilder>(x => x.Extension, StringComparer.Ordinal);
            foreach (var (_, rules) in this.rulesByPriority.OrderByDescending(x => x.Key))
            {
                foreach (var rule in rules)
                {
                    var addAllowed = rule.AddAccessSetting is null
                        ? (bool?) null
                        : rule.AddAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Allowed;
                    var signAllowed = rule.SignAccessSetting is null
                        ? (bool?) null
                        : rule.SignAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Allowed;
                    var sizeLimit = rule.FileSizeLimit;

                    if (rule.Extensions is null or { Count: 0 })
                    {
                        AddExtension(
                            addAllowed,
                            signAllowed,
                            extensionsBuilders,
                            withoutExtension,
                            rule.Categories,
                            sizeLimit);
                    }
                    else
                    {
                        foreach (var extension in rule.Extensions)
                        {
                            AddExtension(
                                addAllowed,
                                signAllowed,
                                extensionsBuilders,
                                extension,
                                rule.Categories,
                                sizeLimit);
                        }
                    }
                }

                if (extensionsBuilders.TryGetItem(withoutExtension, out var withoutExtensionBuilder))
                {
                    withoutExtensionBuilder.LockCurrentSettings(null);
                }
                foreach (var builder in extensionsBuilders)
                {
                    if (builder != withoutExtensionBuilder)
                    {
                        builder.LockCurrentSettings(withoutExtensionBuilder);
                    }
                }
            }

            var result = new KrPermissionsFilesSettings();
            foreach (var builder in extensionsBuilders)
            {
                builder.BuildTo(result);
            }

            return result;
        }

        #endregion

        #region Private Methods

        private static void AddExtension(
            bool? addAllowed,
            bool? signAllowed,
            HashSet<string, FileExtenionSettingsBuilder> extensionBuilders,
            string extension,
            ICollection<Guid> categories,
            long? fileSizeLimit)
        {
            if (!extensionBuilders.TryGetItem(extension, out var builder))
            {
                extensionBuilders[extension] = builder = new FileExtenionSettingsBuilder(extension);
            }
            builder.AddCategories(addAllowed, signAllowed, categories, fileSizeLimit);
        }

        #endregion
    }
}
