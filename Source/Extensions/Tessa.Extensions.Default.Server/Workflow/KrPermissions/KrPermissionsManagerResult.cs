using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionsManagerResult" />
    public sealed class KrPermissionsManagerResult : IKrPermissionsManagerResult
    {
        #region Fields

        private readonly ISession session;
        private readonly IKrPermissionsFilesManager krPermissionsFilesManager;
        private readonly KrPermissionsDescriptor descriptor;
        private readonly bool withExtendedPermissions;

        private HashSet<Guid, IKrPermissionSectionSettings> extendedCardSettings;
        private Dictionary<Guid, HashSet<Guid, IKrPermissionSectionSettings>> extendedTasksSettings;

        #endregion

        #region Constructors

        public KrPermissionsManagerResult(
            ISession session,
            IKrPermissionsFilesManager krPermissionsFilesManager,
            KrPermissionsDescriptor descriptor,
            bool withExtendedPermissions,
            long version)
        {
            Check.ArgumentNotNull(session, nameof(session));
            Check.ArgumentNotNull(krPermissionsFilesManager, nameof(krPermissionsFilesManager));
            Check.ArgumentNotNull(descriptor, nameof(descriptor));

            this.session = session;
            this.krPermissionsFilesManager = krPermissionsFilesManager;
            this.descriptor = descriptor;
            this.withExtendedPermissions = withExtendedPermissions;
            this.Version = version;
        }

        private KrPermissionsManagerResult()
        {
            this.descriptor = new KrPermissionsDescriptor();
        }

        #endregion

        #region IKrPermissionsManagerResult Implementation

        /// <inheritdoc />
        public long Version { get; }

        /// <inheritdoc />
        public bool WithExtendedSettings => this.withExtendedPermissions;

        /// <inheritdoc />
        public ICollection<KrPermissionFlagDescriptor> Permissions => this.descriptor.Permissions;

        /// <inheritdoc />
        public HashSet<Guid, IKrPermissionSectionSettings> ExtendedCardSettings => this.extendedCardSettings
            ??= new HashSet<Guid, IKrPermissionSectionSettings>(x => x.ID, this.descriptor.ExtendedCardSettings.Select(x => x.Build()));

        /// <inheritdoc />
        public Dictionary<Guid, HashSet<Guid, IKrPermissionSectionSettings>> ExtendedTasksSettings => this.extendedTasksSettings
            ??= this.descriptor.ExtendedTasksSettings.ToDictionary(
                x => x.Key,
                x => new HashSet<Guid, IKrPermissionSectionSettings>(y => y.ID, this.descriptor.ExtendedTasksSettings[x.Key].Select(y => y.Build())));

        /// <inheritdoc />
        public ICollection<IKrPermissionsFileRule> FileRules
            => this.descriptor.FileRules;

        /// <inheritdoc />
        public bool Has(KrPermissionFlagDescriptor krPermission)
        {
            return this.descriptor.Has(krPermission);
        }

        /// <inheritdoc />
        public async ValueTask<IKrPermissionExtendedCardSettings> CreateExtendedCardSettingsAsync(
            Guid userID,
            Card card,
            CancellationToken cancellationToken = default)
        {
            KrPermissionExtendedCardSettingsStorage result = null;

            if (this.descriptor.ExtendedCardSettings.Count > 0)
            {
                result ??= new KrPermissionExtendedCardSettingsStorage();
                foreach (var settings in this.ExtendedCardSettings)
                {
                    var newSettings = result.SectionSettings.Add();

                    newSettings.ID = settings.ID;
                    newSettings.IsAllowed = settings.IsAllowed;
                    newSettings.IsDisallowed = settings.IsDisallowed;
                    newSettings.IsHidden = settings.IsHidden;
                    newSettings.IsVisible = settings.IsVisible;
                    newSettings.IsMandatory = settings.IsMandatory;
                    newSettings.DisallowRowAdding = settings.DisallowRowAdding;
                    newSettings.DisallowRowDeleting = settings.DisallowRowDeleting;
                    if (settings.AllowedFields.Count > 0)
                    {
                        newSettings.AllowedFields = settings.AllowedFields;
                    }
                    if (settings.DisallowedFields.Count > 0)
                    {
                        newSettings.DisallowedFields = settings.DisallowedFields;
                    }
                    if (settings.HiddenFields.Count > 0)
                    {
                        newSettings.HiddenFields = settings.HiddenFields;
                    }
                    if (settings.VisibleFields.Count > 0)
                    {
                        newSettings.VisibleFields = settings.VisibleFields;
                    }
                    if (settings.MandatoryFields.Count > 0)
                    {
                        newSettings.MandatoryFields = settings.MandatoryFields;
                    }
                    if (settings.MaskedFields.Count > 0)
                    {
                        newSettings.MaskedFields = settings.MaskedFields;
                    }
                }
            }

            if (this.descriptor.ExtendedTasksSettings.Count > 0)
            {
                result ??= new KrPermissionExtendedCardSettingsStorage();
                foreach (var settingsByTaskType in this.ExtendedTasksSettings)
                {
                    if (settingsByTaskType.Value.Count > 0)
                    {
                        result.TaskSettingsTypes.Add(settingsByTaskType.Key);
                        var taskSettings = result.TaskSettings.Add();
                        foreach (var settings in settingsByTaskType.Value)
                        {
                            var newSettings = taskSettings.Add();

                            newSettings.ID = settings.ID;
                            newSettings.IsAllowed = settings.IsAllowed;
                            newSettings.IsDisallowed = settings.IsDisallowed;
                            newSettings.IsHidden = settings.IsHidden;
                            newSettings.IsVisible = settings.IsVisible;
                            newSettings.DisallowRowAdding = settings.DisallowRowAdding;
                            newSettings.DisallowRowDeleting = settings.DisallowRowDeleting;
                            if (settings.AllowedFields.Count > 0)
                            {
                                newSettings.AllowedFields = settings.AllowedFields;
                            }
                            if (settings.DisallowedFields.Count > 0)
                            {
                                newSettings.DisallowedFields = settings.DisallowedFields;
                            }
                            if (settings.HiddenFields.Count > 0)
                            {
                                newSettings.HiddenFields = settings.HiddenFields;
                            }
                            if (settings.VisibleFields.Count > 0)
                            {
                                newSettings.VisibleFields = settings.VisibleFields;
                            }
                        }
                    }
                }
            }

            var visibilitySettings = this.descriptor.VisibilitySettings.Build();
            if (visibilitySettings is not null && visibilitySettings.Count > 0)
            {
                result ??= new KrPermissionExtendedCardSettingsStorage();
                foreach (var setting in visibilitySettings)
                {
                    result ??= new KrPermissionExtendedCardSettingsStorage();
                    result.VisibilitySettings.Add(
                        setting.ToStorage());
                }
            }

            if (this.descriptor.FileRules.Count > 0)
            {
                result ??= new KrPermissionExtendedCardSettingsStorage();
                bool canEditOtherUserFile = this.Permissions.Contains(KrPermissionFlagDescriptors.EditFiles);

                if (card.Files.Count > 0)
                {
                    var filesContext = new KrPermissionsFilesManagerContext(
                        this.session,
                        KrPermissionsFileAccessSettingFlag.All & ~KrPermissionsFileAccessSettingFlag.Add,
                        this.descriptor.FileRules);

                    for (var i = card.Files.Count - 1; i >= 0; i--)
                    {
                        var file = card.Files[i];

                        // Данные правила не распространяются на виртуальные файлы
                        if (file.IsVirtual)
                        {
                            continue;
                        }

                        filesContext.SetFile(file);

                        var fileCheckResult = await this.krPermissionsFilesManager.CheckPermissionsAsync(filesContext);

                        var fileSetting = result.FileSettings.Add();
                        fileSetting.FileID = file.RowID;
                        fileSetting.OwnFile = file.Card.CreatedByID == this.session.User.ID;

                        if (fileCheckResult.FileSizeLimit.HasValue)
                        {
                            fileSetting.FileSizeLimit = fileCheckResult.FileSizeLimit;
                        }

                        foreach (var (flag, accessSetting) in fileCheckResult.AccessSettings)
                        {
                            switch (flag)
                            {
                                case KrPermissionsFileAccessSettingFlag.Read:
                                    fileSetting.ReadAccessSetting = accessSetting;
                                    break;

                                case KrPermissionsFileAccessSettingFlag.Edit:
                                    fileSetting.EditAccessSetting = accessSetting;
                                    if (!canEditOtherUserFile
                                        && accessSetting == KrPermissionsHelper.AccessSettings.AllowEdit
                                        && !fileSetting.OwnFile)
                                    {
                                        canEditOtherUserFile = true;
                                    }
                                    break;

                                case KrPermissionsFileAccessSettingFlag.Delete:
                                    fileSetting.DeleteAccessSetting = accessSetting;
                                    break;

                                case KrPermissionsFileAccessSettingFlag.Sign:
                                    fileSetting.SignAccessSetting = accessSetting;
                                    break;
                            }
                        }

                        // Ограничиваем доступ к файлам на редактирование, подписание и удаление в случаях, когда нет доступа на чтение файла.
                        if (fileSetting.ReadAccessSetting.HasValue
                            && fileSetting.ReadAccessSetting.Value <= KrPermissionsHelper.FileReadAccessSettings.ContentNotAvailable)
                        {
                            fileSetting.EditAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Disallowed;
                            fileSetting.SignAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Disallowed;

                            if (fileSetting.ReadAccessSetting.Value == KrPermissionsHelper.FileReadAccessSettings.FileNotAvailable)
                            {
                                fileSetting.DeleteAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Disallowed;
                            }
                        }
                    }
                }

                var ownFileSettingsBuilder = new KrPermissionsFilesSettingsBuilder();

                // Сохраняем настройки изменения
                var otherFileSettingsBuilder = canEditOtherUserFile
                     ? new KrPermissionsFilesSettingsBuilder()
                     : new KrPermissionsFilesSettingsBuilder();
                foreach (var fileRule in this.descriptor.FileRules.OfType<KrPermissionsFileRule>())
                {
                    switch (fileRule.FileCheckRule)
                    {
                        case KrPermissionsHelper.FileCheckRules.OwnFiles:
                            ownFileSettingsBuilder.Add(fileRule);
                            break;

                        case KrPermissionsHelper.FileCheckRules.AllFiles:
                            ownFileSettingsBuilder.Add(fileRule);
                            otherFileSettingsBuilder?.Add(fileRule);
                            break;

                        case KrPermissionsHelper.FileCheckRules.FilesOfOtherUsers:
                            otherFileSettingsBuilder?.Add(fileRule);
                            break;
                    }
                }

                result.OwnFilesSettings = ownFileSettingsBuilder.Build();
                result.OtherFilesSettings = otherFileSettingsBuilder?.Build();
            }

            return result;
        }

        #endregion

        #region Static

        /// <summary>
        /// Пустой результат расчёта прав доступа.
        /// </summary>
        public static readonly IKrPermissionsManagerResult Empty = new KrPermissionsManagerResult();

        #endregion
    }
}
