using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Cards.Workflow;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет информацию для запуска процесса посредством <see cref="WorkflowStoreExtension"/>.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class StartingSecondaryProcessInfo : StorageObject
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="StartingSecondaryProcessInfo"/>.
        /// </summary>
        /// <param name="secondaryProcess">Идентификатор вторичного процесса.</param>
        /// <param name="processInfo">Дополнительная информация по процессу.</param>
        /// <param name="parentStageRowID">Идентификатор строки этапа создавшего этот вложенный процесс.</param>
        /// <param name="parentProcessTypeName">Имя типа родитеского процесса.</param>
        /// <param name="parentProcessID">Идентификатор родительского процесса.</param>
        /// <param name="processHolderID">Идентификатор карточки держателя процесса.</param>
        /// <param name="nestedOrder">Порядковый номер вложенного опроцесса.</param>
        public StartingSecondaryProcessInfo(
            Guid? secondaryProcess,
            IDictionary<string, object> processInfo,
            Guid? parentStageRowID,
            string parentProcessTypeName,
            Guid? parentProcessID,
            Guid? processHolderID,
            int? nestedOrder)
            : base(new Dictionary<string, object>(StringComparer.Ordinal))
        {
            this.Init(nameof(this.SecondaryProcessID), secondaryProcess);
            this.Init(nameof(this.ProcessInfo), processInfo.ToDictionaryStorage());
            this.Init(nameof(this.ParentStageRowID), parentStageRowID);
            this.Init(nameof(this.ParentProcessTypeName), parentProcessTypeName);
            this.Init(nameof(this.ParentProcessID), parentProcessID);
            this.Init(nameof(this.ProcessHolderID), processHolderID);
            this.Init(nameof(this.NestedOrder), Int32Boxes.Box(nestedOrder));
        }

        /// <inheritdoc />
        public StartingSecondaryProcessInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор вторичного процесса.
        /// </summary>
        public Guid? SecondaryProcessID => this.Get<Guid?>(nameof(this.SecondaryProcessID));

        /// <summary>
        /// Возвращает дополнительную информацию по процессу.
        /// </summary>
        public IDictionary<string, object> ProcessInfo => this.Get<IDictionary<string, object>>(nameof(this.ProcessInfo));

        /// <summary>
        /// Возвращает идентификатор карточки - держателя процесса.
        /// </summary>
        public Guid? ProcessHolderID => this.TryGet<Guid?>(nameof(this.ProcessHolderID));

        /// <summary>
        /// Возвращает имя типа родительского процесса.
        /// </summary>
        public string ParentProcessTypeName => this.TryGet<string>(nameof(this.ParentProcessTypeName));

        /// <summary>
        /// Возвращает идентификатор родительского процесса.
        /// </summary>
        public Guid? ParentProcessID => this.TryGet<Guid?>(nameof(this.ParentProcessID));

        /// <summary>
        /// Возвращает идентификатор строки этапа создавшего процесс.
        /// </summary>
        public Guid? ParentStageRowID => this.TryGet<Guid?>(nameof(this.ParentStageRowID));

        /// <summary>
        /// Возвращает порядковый номер процесса.
        /// </summary>
        public int? NestedOrder => this.TryGet<int?>(nameof(this.NestedOrder));

        #endregion
    }
}
