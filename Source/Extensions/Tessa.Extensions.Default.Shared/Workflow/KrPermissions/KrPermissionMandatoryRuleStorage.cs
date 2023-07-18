using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Настройки обязательности полей.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class KrPermissionMandatoryRuleStorage : StorageObject
    {
        #region Constructors

        public KrPermissionMandatoryRuleStorage(
            Guid sectionID,
            IEnumerable<Guid> columnIDs = null)
            : base(new Dictionary<string, object>(DefaultCapacity, StringComparer.Ordinal))
        {
            this.Set(nameof(this.SectionID), sectionID);
            this.Set(nameof(this.ColumnIDs), columnIDs?.Cast<object>() ?? EmptyHolder<object>.Collection);
        }

        public KrPermissionMandatoryRuleStorage(Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(nameof(this.SectionID), GuidBoxes.Empty);
            this.Init(nameof(this.ColumnIDs), EmptyHolder<object>.Collection);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Секция, для которой применяется данная настройка.
        /// </summary>
        public Guid SectionID
        {
            get => this.Get<Guid>(nameof(this.SectionID));
        }

        /// <summary>
        /// Список полей, для которых применяется данная настройка.
        /// </summary>
        public IReadOnlyCollection<object> ColumnIDs
        {
            get => this.Get<IReadOnlyCollection<object>>(nameof(this.ColumnIDs));
        }

        #endregion
    }
}
