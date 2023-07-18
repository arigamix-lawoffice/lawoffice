using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет информацию об экземпляре процесса.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class KrProcessInstance : StorageObject
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="KrProcessInstance"/>.
        /// </summary>
        /// <param name="processID">Идентификатор процесса.</param>
        /// <param name="cardID">Идентификатор карточки в которой запущен процесс.</param>
        /// <param name="serializedWorkflowProcess">Сериализованная объектная модель процесса.</param>
        /// <param name="signature">Подпись <paramref name="serializedWorkflowProcess"/>.</param>
        public KrProcessInstance(
            Guid processID,
            Guid? cardID,
            string serializedWorkflowProcess,
            byte[] signature
            )
            : base(new Dictionary<string, object>(StringComparer.Ordinal))
        {
            this.Init(nameof(this.ProcessID), processID);
            this.Init(nameof(this.CardID), cardID);
            this.Init(nameof(this.ProcessInfo), null);
            this.Init(nameof(this.ParentStageRowID), null);
            this.Init(nameof(this.ParentProcessTypeName), null);
            this.Init(nameof(this.ParentProcessID), null);
            this.Init(nameof(this.ProcessHolderID), null);
            this.Init(nameof(this.NestedOrder), null);
            this.Init(nameof(this.SerializedProcess), serializedWorkflowProcess);
            this.Init(nameof(this.SerializedProcessSignature), signature);
        }

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="KrProcessInstance"/>.
        /// </summary>
        /// <param name="processID">Идентификатор процесса.</param>
        /// <param name="cardID">Идентификатор карточки в которой запущен процесс.</param>
        /// <param name="processInfo">Дополнительная информация по процессу.</param>
        /// <param name="parentStageRowID">Идентификатор строки этапа создавшего этот вложенный процесс.</param>
        /// <param name="parentProcessTypeName">Имя типа родитеского процесса.</param>
        /// <param name="parentProcessID">Идентификатор родительского процесса.</param>
        /// <param name="processHolderID">Идентификатор карточки держателя процесса.</param>
        /// <param name="nestedOrder">Порядковый номер вложенного опроцесса.</param>
        public KrProcessInstance(
            Guid processID,
            Guid? cardID,
            IDictionary<string, object> processInfo,
            Guid? parentStageRowID,
            string parentProcessTypeName,
            Guid? parentProcessID,
            Guid? processHolderID,
            int? nestedOrder)
            : base(new Dictionary<string, object>(StringComparer.Ordinal))
        {
            this.Init(nameof(this.ProcessID), processID);
            this.Init(nameof(this.CardID), cardID);
            this.Init(nameof(this.ProcessInfo), processInfo);
            this.Init(nameof(this.ParentStageRowID), parentStageRowID);
            this.Init(nameof(this.ParentProcessTypeName), parentProcessTypeName);
            this.Init(nameof(this.ParentProcessID), parentProcessID);
            this.Init(nameof(this.ProcessHolderID), processHolderID);
            this.Init(nameof(this.NestedOrder), Int32Boxes.Box(nestedOrder));
            this.Init(nameof(this.SerializedProcess), null);
            this.Init(nameof(this.SerializedProcessSignature), null);
        }

        /// <inheritdoc />
        public KrProcessInstance(
            Dictionary<string, object> storage)
            : base(storage)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор процесса.
        /// </summary>
        public Guid ProcessID => this.Get<Guid>(nameof(this.ProcessID));

        /// <summary>
        /// Возвращает идентификатор карточки в которой запущен процесс.
        /// </summary>
        public Guid? CardID => this.Get<Guid?>(nameof(this.CardID));

        /// <summary>
        /// Возвращает дополнительную информацию по процессу.
        /// </summary>
        public IDictionary<string, object> ProcessInfo =>
            this.GetDictionary(nameof(this.ProcessInfo), o => new Dictionary<string, object>(StringComparer.Ordinal));

        /// <summary>
        /// Возвращает идентифкатор карточки держателя процесса.
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
        /// Возвращает идентификатор строки этапа создавшего этот вложенный процесс.
        /// </summary>
        public Guid? ParentStageRowID => this.TryGet<Guid?>(nameof(this.ParentStageRowID));

        /// <summary>
        /// Возвращает порядковый номер вложенного процесса.
        /// </summary>
        public int? NestedOrder => this.TryGet<int?>(nameof(this.NestedOrder));

        /// <summary>
        /// Возвращает сериализованную объектную модель процесса.
        /// </summary>
        public string SerializedProcess => this.TryGet<string>(nameof(this.SerializedProcess));

        /// <summary>
        /// Возвращает подпись для сериализованной объектной модели процесса.
        /// </summary>
        public byte[] SerializedProcessSignature => this.TryGet<byte[]>(nameof(this.SerializedProcessSignature));

        #endregion

    }
}
