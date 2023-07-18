using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Объект с расширенными настройками доступа к карточке, записывающий настройки в хранилище <c>Dictionary&lt;string, object&gt;</c>.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class KrPermissionExtendedCardSettingsStorage : StorageObject, IKrPermissionExtendedCardSettings
    {
        #region Fields

        private static readonly IStorageValueFactory<int, IKrPermissionSectionSettings> cardSettingsFactory =
            new DictionaryStorageValueFactory<int, IKrPermissionSectionSettings>(
                (index, storage) => new KrPermissionSectionSettingsStorage(storage));

        private static readonly IStorageValueFactory<int, ListStorage<IKrPermissionSectionSettings>> taskSettingsFactory =
            new ListStorageValueFactory<int, ListStorage<IKrPermissionSectionSettings>>(
                (index, storage) => new ListStorage<IKrPermissionSectionSettings>(storage, cardSettingsFactory));

        private static readonly IStorageValueFactory<int, KrPermissionsFileSettings> fileSettingsFactory =
            new DictionaryStorageValueFactory<int, KrPermissionsFileSettings>(
                (index, storage) => new KrPermissionsFileSettings(storage));

        #endregion

        #region Constructors

        public KrPermissionExtendedCardSettingsStorage(Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(nameof(this.SectionSettings), new List<object>());
            this.Init(nameof(this.TaskSettings), new List<object>());
            this.Init(nameof(this.TaskSettingsTypes), new List<object>());
            this.Init(nameof(this.VisibilitySettings), new List<object>());
            this.Init(nameof(this.FileSettings), new List<object>());
        }

        #endregion

        #region Storage Properties

        /// <summary>
        /// Расширенные настройки доступа к секциям карточки.
        /// </summary>
        public ListStorage<IKrPermissionSectionSettings> SectionSettings
        {
            get
            {
                return this.GetList(
                    nameof(this.SectionSettings),
                    x => new ListStorage<IKrPermissionSectionSettings>(x, cardSettingsFactory));
            }
            set { this.SetStorageValue(nameof(this.SectionSettings), value); }
        }

        /// <summary>
        /// Расширенные настройки доступа к секциям заданий.
        /// </summary>
        public ListStorage<ListStorage<IKrPermissionSectionSettings>> TaskSettings
        {
            get
            {
                return this.GetList(
                    nameof(this.TaskSettings),
                    x => new ListStorage<ListStorage<IKrPermissionSectionSettings>>(x, taskSettingsFactory));
            }
            set { this.SetStorageValue(nameof(this.TaskSettings), value); }
        }

        /// <summary>
        /// Типы заданий, для которых передаются расширеныне настройки заданий.
        /// </summary>
        public IList TaskSettingsTypes
        {
            get { return this.Get<IList>(nameof(this.TaskSettingsTypes)); }
            set { this.Set(nameof(this.TaskSettingsTypes), value); }
        }

        /// <summary>
        /// Настройки видимости контролов, блоков и вкладок.
        /// </summary>
        public IList VisibilitySettings
        {
            get { return this.Get<IList>(nameof(this.VisibilitySettings)); }
            set { this.Set(nameof(this.VisibilitySettings), value); }
        }

        /// <summary>
        /// Настройки доступа к файлам.
        /// </summary>
        public ListStorage<KrPermissionsFileSettings> FileSettings
        {
            get
            {
                return this.GetList(
                    nameof(this.FileSettings),
                    x => new ListStorage<KrPermissionsFileSettings>(x, fileSettingsFactory));
            }
            set { this.SetStorageValue(nameof(this.FileSettings), value); }
        }

        /// <summary>
        /// Настройки доступа создания и замены файлов текущего пользователя.
        /// </summary>
        public KrPermissionsFilesSettings OwnFilesSettings
        {
            get { return this.TryGetDictionary(nameof(this.OwnFilesSettings), static (storage) => new KrPermissionsFilesSettings(storage)); }
            set { this.SetStorageValue(nameof(this.OwnFilesSettings), value); }
        }

        /// <summary>
        /// Настройки доступа замены файлов других пользователей.
        /// </summary>
        public KrPermissionsFilesSettings OtherFilesSettings
        {
            get { return this.TryGetDictionary(nameof(this.OtherFilesSettings), static (storage) => new KrPermissionsFilesSettings(storage)); }
            set { this.SetStorageValue(nameof(this.OtherFilesSettings), value); }
        }

        #endregion

        #region IKrPermissionExtendedCardSettings Implementation

        /// <inheritdoc/>
        public ICollection<IKrPermissionSectionSettings> GetCardSettings()
        {
            return this.SectionSettings;
        }

        /// <inheritdoc/>
        public Dictionary<Guid, ICollection<IKrPermissionSectionSettings>> GetTasksSettings()
        {
            if (this.TaskSettings is not null
                && this.TaskSettings.Count > 0)
            {
                var result = new Dictionary<Guid, ICollection<IKrPermissionSectionSettings>>();
                for (int i = 0; i < this.TaskSettings.Count; i++)
                {
                    result[(Guid) this.TaskSettingsTypes[i]] = this.TaskSettings[i];
                }

                return result;
            }

            return null;
        }

        /// <inheritdoc/>
        public ICollection<KrPermissionVisibilitySettings> GetVisibilitySettings()
        {
            if (this.VisibilitySettings.Count == 0)
            {
                return EmptyHolder<KrPermissionVisibilitySettings>.Collection;
            }

            var result = new KrPermissionVisibilitySettings[this.VisibilitySettings.Count];

            for (int i = 0; i < this.VisibilitySettings.Count; i++)
            {
                result[i] = KrPermissionVisibilitySettings.FromStorage((Dictionary<string, object>) this.VisibilitySettings[i]);
            }

            return result;
        }

        /// <inheritdoc/>
        public ICollection<KrPermissionsFileSettings> GetFileSettings()
        {
            return this.FileSettings;
        }

        /// <inheritdoc/>
        public KrPermissionsFilesSettings TryGetOwnFilesSettings()
        {
            return this.OwnFilesSettings;
        }

        /// <inheritdoc/>
        public KrPermissionsFilesSettings TryGetOtherFilesSettings()
        {
            return this.OtherFilesSettings;
        }

        /// <inheritdoc/>
        public void SetCardAccess(
            bool isAllowed,
            Guid sectionID,
            ICollection<Guid> fields)
        {
            var sectionSettings = this.SectionSettings.FirstOrDefault(x => x.ID == sectionID);
            if (sectionSettings is null)
            {
                sectionSettings = this.SectionSettings.Add();
                sectionSettings.ID = sectionID;

                if (fields is not null
                    && fields.Count > 0)
                {
                    if (isAllowed)
                    {
                        sectionSettings.AllowedFields = fields.ToArray();
                    }
                    else
                    {
                        sectionSettings.DisallowedFields = fields.ToArray();
                    }
                }
                else
                {
                    sectionSettings.IsAllowed = isAllowed;
                    sectionSettings.IsDisallowed = !isAllowed;
                }
            }
            else
            {
                if (fields is not null
                    && fields.Count > 0)
                {
                    var to = isAllowed ? sectionSettings.AllowedFields : sectionSettings.DisallowedFields;
                    var from = isAllowed ? sectionSettings.DisallowedFields : sectionSettings.AllowedFields;

                    var toFields = new HashSet<Guid>(to);
                    var fromFields = new HashSet<Guid>(from);
                    toFields.AddRange(fields);
                    fromFields.RemoveWhere(x => toFields.Contains(x));

                    sectionSettings.AllowedFields = isAllowed ? toFields : fromFields;
                    sectionSettings.DisallowedFields = isAllowed ? fromFields : toFields;
                }
                else
                {
                    sectionSettings.IsAllowed = isAllowed;
                    sectionSettings.IsDisallowed = !isAllowed;
                }
            }
        }

        #endregion
    }
}

