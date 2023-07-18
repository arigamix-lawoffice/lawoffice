using System;
using System.Collections.Generic;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionExtendedCardSettings"/>
    public sealed class KrPermissionExtendedCardSettings : IKrPermissionExtendedCardSettings
    {
        #region Fields

        private HashSet<Guid, IKrPermissionSectionSettings> cardSettings;
        private Dictionary<Guid, ICollection<IKrPermissionSectionSettings>> tasksSettings;

        #endregion

        #region Constructors

        public KrPermissionExtendedCardSettings()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Списки расширенных настроек для секций заданий по типам заданий.
        /// </summary>
        public Dictionary<Guid, ICollection<IKrPermissionSectionSettings>> TasksSettings
            => this.tasksSettings ??= new Dictionary<Guid, ICollection<IKrPermissionSectionSettings>>();

        /// <summary>
        /// Cписок расширенных настроек для секций карточки.
        /// </summary>
        public ICollection<IKrPermissionSectionSettings> CardSettings
            => this.cardSettings ??= new HashSet<Guid, IKrPermissionSectionSettings>(x => x.ID);

        #endregion

        #region IKrPermissionExtendedCardSettings Implementation

        /// <inheritdoc/>
        public ICollection<IKrPermissionSectionSettings> GetCardSettings()
        {
            return this.cardSettings;
        }

        /// <inheritdoc/>
        public Dictionary<Guid, ICollection<IKrPermissionSectionSettings>> GetTasksSettings()
        {
            return this.tasksSettings;
        }

        /// <inheritdoc/>
        public ICollection<KrPermissionVisibilitySettings> GetVisibilitySettings()
        {
            return EmptyHolder<KrPermissionVisibilitySettings>.Collection;
        }

        /// <inheritdoc/>
        public ICollection<KrPermissionsFileSettings> GetFileSettings()
        {
            return EmptyHolder<KrPermissionsFileSettings>.Collection;
        }

        /// <inheritdoc/>
        public KrPermissionsFilesSettings TryGetOwnFilesSettings()
        {
            return null;
        }

        /// <inheritdoc/>
        public KrPermissionsFilesSettings TryGetOtherFilesSettings()
        {
            return null;
        }

        /// <inheritdoc/>
        public void SetCardAccess(bool isAllowed, Guid sectionID, ICollection<Guid> fields)
        {
            if (this.cardSettings is null)
            {
                this.cardSettings = new HashSet<Guid, IKrPermissionSectionSettings>(x => x.ID);
            }
            else if (this.cardSettings.TryGetItem(sectionID, out var isectionSettingsExisted)
                && isectionSettingsExisted is KrPermissionSectionSettings sectionSettingsExisted)
            {
                if (fields is not null
                    && fields.Count > 0)
                {
                    if (isAllowed)
                    {
                        sectionSettingsExisted.AllowedFields.AddRange(fields);
                        sectionSettingsExisted.DisallowedFields.RemoveRange(fields);
                    }
                    else
                    {
                        sectionSettingsExisted.DisallowedFields.AddRange(fields);
                        sectionSettingsExisted.AllowedFields.RemoveRange(fields);
                    }
                }
                else
                {
                    sectionSettingsExisted.IsAllowed = isAllowed;
                    sectionSettingsExisted.IsDisallowed = !isAllowed;
                }

                return;
            }

            var sectionSettings = new KrPermissionSectionSettings
            {
                ID = sectionID,
            };
            this.cardSettings.Add(sectionSettings);

            if (fields is not null
                    && fields.Count > 0)
            {
                if (isAllowed)
                {
                    sectionSettings.AllowedFields = new HashSet<Guid>(fields);
                }
                else
                {
                    sectionSettings.DisallowedFields = new HashSet<Guid>(fields);
                }
            }
            else
            {
                sectionSettings.IsAllowed = isAllowed;
                sectionSettings.IsDisallowed = !isAllowed;
            }
        }

        #endregion
    }
}
