using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Collections;
using Tessa.Platform.Conditions;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionRuleSettings" />
    public sealed class KrPermissionRuleSettings : IKrPermissionRuleSettings
    {
        #region Constructors

        public KrPermissionRuleSettings(
            Guid id,
            string conditions,
            bool isExtended,
            bool isRequired,
            int priority)
        {
            this.ID = id;
            this.Conditions = string.IsNullOrWhiteSpace(conditions)
                ? Array.Empty<ConditionSettings>()
                : ConditionSettings.GetFromList(StorageHelper.DeserializeListFromTypedJson(conditions));

            this.IsExtended = isExtended;
            this.IsRequired = isRequired;
            this.Priority = priority;
        }

        #endregion

        #region IKrPermissionRuleSettings Implementation

        /// <inheritdoc />
        public Guid ID { get; }

        /// <inheritdoc />
        public IEnumerable<ConditionSettings> Conditions { get; }

        /// <inheritdoc />
        public HashSet<Guid> Types { get; } = new HashSet<Guid>();

        /// <inheritdoc />
        public HashSet<int> States { get; } = new HashSet<int>();

        /// <inheritdoc />
        public bool IsRequired { get; }

        /// <inheritdoc />
        public bool IsExtended { get; }

        /// <inheritdoc />
        public int Priority { get; }

        /// <inheritdoc />
        public HashSet<KrPermissionFlagDescriptor> Flags { get; } = new HashSet<KrPermissionFlagDescriptor>();

        /// <inheritdoc />
        public ICollection<Guid> ContextRoles { get; } = new List<Guid>();

        /// <inheritdoc />
        public HashSet<Guid, KrPermissionSectionSettings> CardSettings { get; }
            = new HashSet<Guid, KrPermissionSectionSettings>(x => x.ID);

        /// <inheritdoc />
        public Dictionary<Guid, HashSet<Guid, KrPermissionSectionSettings>> TaskSettingsByTypes { get; }
            = new Dictionary<Guid, HashSet<Guid, KrPermissionSectionSettings>>();

        /// <inheritdoc />
        public ICollection<KrPermissionMandatoryRule> MandatoryRules { get; }
            = new List<KrPermissionMandatoryRule>();

        /// <inheritdoc />
        public ICollection<KrPermissionVisibilitySettings> VisibilitySettings { get; }
            = new List<KrPermissionVisibilitySettings>();

        /// <inheritdoc />
        public ICollection<KrPermissionsFileRule> FileRules { get; }
            = new List<KrPermissionsFileRule>();

        #endregion
    }
}
