using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Предоставляет информацию о вложенном процессе.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class NestedProcessCommonInfo : ProcessCommonInfo
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NestedProcessCommonInfo"/>.
        /// </summary>
        /// <param name="currentStageRowID">Идентификатор текущего этапа.</param>
        /// <param name="info">Дополнительная информация по процессу.</param>
        /// <param name="secondaryProcessID">Идентификатор вторичного процесса.</param>
        /// <param name="nestedProcessID">Идентификатор дочернего процесса.</param>
        /// <param name="parentStageRowID">Идентификатор родительского этапа.</param>
        /// <param name="nestedOrder">Порядковый номер дочернего процесса.</param>
        public NestedProcessCommonInfo(
            Guid? currentStageRowID,
            IDictionary<string, object> info,
            Guid? secondaryProcessID,
            Guid nestedProcessID,
            Guid parentStageRowID,
            int nestedOrder)
            : base(
                  currentStageRowID,
                  info,
                  secondaryProcessID,
                  default,
                  default,
                  default,
                  default)
        {
            this.Init(nameof(this.NestedProcessID), GuidBoxes.Box(nestedProcessID));
            this.Init(nameof(this.ParentStageRowID), GuidBoxes.Box(parentStageRowID));
            this.Init(nameof(this.NestedOrder), Int32Boxes.Box(nestedOrder));
        }

        /// <inheritdoc />
        public NestedProcessCommonInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задаёт идентификатор дочернего процесса.
        /// </summary>
        public Guid NestedProcessID
        {
            get => this.Get<Guid>(nameof(this.NestedProcessID));
            set => this.Set(nameof(this.NestedProcessID), value);
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор родительского этапа.
        /// </summary>
        public Guid ParentStageRowID
        {
            get => this.Get<Guid>(nameof(this.ParentStageRowID));
            set => this.Set(nameof(this.ParentStageRowID), value);
        }

        /// <summary>
        /// Возвращает или задаёт порядковый номер дочернего процесса.
        /// </summary>
        public int NestedOrder
        {
            get => this.Get<int>(nameof(this.NestedOrder));
            set => this.Set(nameof(this.NestedOrder), value);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Guid? AuthorID
        {
            get => this.Info.TryGet<Guid?>(nameof(NestedProcessCommonInfo) + "." + nameof(this.AuthorID));
            set => this.Info[nameof(NestedProcessCommonInfo) + "." + nameof(this.AuthorID)] = value;
        }

        /// <inheritdoc/>
        public override string AuthorName
        {
            get => this.Info.TryGet<string>(nameof(NestedProcessCommonInfo) + "." + nameof(this.AuthorName));
            set => this.Info[nameof(NestedProcessCommonInfo) + "." + nameof(this.AuthorName)] = value;
        }

        /// <inheritdoc/>
        public override long AuthorTimestamp
        {
            get => this.Info.TryGet<long>(nameof(NestedProcessCommonInfo) + "." + nameof(this.AuthorTimestamp));
            set => this.Info[nameof(NestedProcessCommonInfo) + "." + nameof(this.AuthorTimestamp)] = Int64Boxes.Box(value);
        }

        /// <inheritdoc/>
        public override Guid? ProcessOwnerID
        {
            get => this.Info.TryGet<Guid?>(nameof(NestedProcessCommonInfo) + "." + nameof(this.ProcessOwnerID));
            set => this.Info[nameof(NestedProcessCommonInfo) + "." + nameof(this.ProcessOwnerID)] = value;
        }

        /// <inheritdoc/>
        public override string ProcessOwnerName
        {
            get => this.Info.TryGet<string>(nameof(NestedProcessCommonInfo) + "." + nameof(this.ProcessOwnerName));
            set => this.Info[nameof(NestedProcessCommonInfo) + "." + nameof(this.ProcessOwnerName)] = value;
        }

        /// <inheritdoc/>
        public override long ProcessOwnerTimestamp
        {
            get => this.Info.TryGet<long>(nameof(NestedProcessCommonInfo) + "." + nameof(this.ProcessOwnerTimestamp));
            set => this.Info[nameof(NestedProcessCommonInfo) + "." + nameof(this.ProcessOwnerTimestamp)] = Int64Boxes.Box(value);
        }

        #endregion
    }
}
