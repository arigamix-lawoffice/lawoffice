using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Реализация объекта исполнителя для этапа с множественными исполнителями.
    /// Работает поверх хранилища <see cref="KrConstants.KrPerformersVirtual"/>.
    /// </summary>
    public sealed class MultiPerformer : Performer
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MultiPerformer"/>.
        /// Запечатанность не переносится.
        /// </summary>
        /// <param name="performer">Копируемый объект.</param>
        public MultiPerformer(
            MultiPerformer performer)
            : this(
                  performer.RowID,
                  performer.PerformerID,
                  performer.PerformerName,
                  performer.StageRowID,
                  performer.IsSql)
        {
        }

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public MultiPerformer(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MultiPerformer"/>.
        /// </summary>
        /// <param name="rowID">Идентификатор строки исполнителя.</param>
        /// <param name="performerID">Идентификатор роли исполнителя.</param>
        /// <param name="performerName">Название роли исполнителя.</param>
        /// <param name="stageRowID">Идентификатор строки этапа к которому относится информация о исполнителе.</param>
        /// <param name="isSql">Значение <see langword="true"/>, если исполнитель является SQL исполнителем, иначе - <see langword="false"/>.</param>
        public MultiPerformer(
            Guid rowID,
            Guid performerID,
            string performerName,
            Guid stageRowID,
            bool isSql = false)
            : base(new Dictionary<string, object>(6, StringComparer.Ordinal))
        {
            this.Init(KrConstants.KrPerformersVirtual.RowID, rowID);
            this.Init(KrConstants.KrPerformersVirtual.PerformerID, performerID);
            this.Init(KrConstants.KrPerformersVirtual.PerformerName, performerName);
            this.Init(KrConstants.KrPerformersVirtual.StageRowID, stageRowID);
            this.Init(KrConstants.KrPerformersVirtual.SQLApprover, BooleanBoxes.Box(isSql));
            this.Init(KrConstants.KrPerformersVirtual.Order, Int32Boxes.Zero);
        }

        /// <summary>
        /// ДИнициализирует новый экземпляр класса <see cref="MultiPerformer"/>.
        /// </summary>
        /// <param name="id">Идентификатор роли исполнителя.</param>
        /// <param name="name">Название роли исполнителя.</param>
        /// <param name="stageRowID">Идентификатор строки этапа к которому относится информация о исполнителе.</param>
        /// <param name="isSql">Значение <see langword="true"/>, если исполнитель является SQL исполнителем, иначе - <see langword="false"/>.</param>
        public MultiPerformer(
            Guid id,
            string name,
            Guid stageRowID,
            bool isSql = false)
            : this(Guid.NewGuid(), id, name, stageRowID, isSql)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MultiPerformer"/>.
        /// </summary>
        /// <param name="id">Идентификатор роли исполнителя.</param>
        /// <param name="name">Название роли исполнителя.</param>
        /// <param name="stageRowID">Идентификатор строки этапа к которому относится информация о исполнителе.</param>
        /// <param name="isSql">Значение <see langword="true"/>, если исполнитель является SQL исполнителем, иначе - <see langword="false"/>.</param>
        public MultiPerformer(
            string id,
            string name,
            Guid stageRowID,
            bool isSql = false)
            : this(Guid.Parse(id), name, stageRowID, isSql)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор строки исполнителя. Используется только для представления в виртуальных секциях.
        /// </summary>
        public override Guid RowID => this.Get<Guid>(KrConstants.KrPerformersVirtual.RowID);

        /// <summary>
        /// Значение, показывающее, что исполнитель является SQL исполнителем.
        /// </summary>
        public override bool IsSql => this.Get<bool>(KrConstants.KrPerformersVirtual.SQLApprover);

        /// <summary>
        /// Идентификатор роли исполнителя.
        /// </summary>
        public override Guid PerformerID => this.Get<Guid>(KrConstants.KrPerformersVirtual.PerformerID);

        /// <summary>
        /// Имя роли исполнителя.
        /// </summary>
        public override string PerformerName => this.Get<string>(KrConstants.KrPerformersVirtual.PerformerName);

        /// <summary>
        /// Идентификатор строки этапа.
        /// </summary>
        public override Guid StageRowID => this.Get<Guid>(KrConstants.KrPerformersVirtual.StageRowID);

        #endregion
    }
}