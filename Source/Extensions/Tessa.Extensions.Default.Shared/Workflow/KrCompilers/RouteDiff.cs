using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Объект, предоставляющий информацию об изменении этапа маршрута после расчёта.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class RouteDiff :
        StorageObject
    {
        #region Constructors

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public RouteDiff(Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(nameof(this.ActualName), null);
            this.Init(nameof(this.OldName), null);
            this.Init(nameof(this.HasPlainChanges), BooleanBoxes.False);
            this.Init(nameof(this.HiddenStage), BooleanBoxes.False);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Тип действия с этапом маршрута.
        /// </summary>
        public RouteDiffAction Action
        {
            get => (RouteDiffAction) this.Get<int>(nameof(this.Action));
            private set => this.Set(nameof(this.Action), Int32Boxes.Box((int) value));
        }

        /// <summary>
        /// Идентификатор строки этапа.
        /// </summary>
        public Guid RowID
        {
            get => this.Get<Guid>(nameof(this.RowID));
            private set => this.Set(nameof(this.RowID), GuidBoxes.Box(value));
        }

        /// <summary>
        /// Текущее имя этапа или значение <see langword="null"/>, если он был удален.
        /// </summary>
        public string ActualName
        {
            get => this.Get<string>(nameof(this.ActualName));
            private set => this.Set(nameof(this.ActualName), value);
        }

        /// <summary>
        /// Предыдущее имя этапа или значение <see langword="null"/>, если он был добавлен.
        /// </summary>
        /// <remarks>
        /// Если имя этапа не изменилось, то <see cref="ActualName"/> равно <see cref="OldName"/>.
        /// </remarks>
        public string OldName
        {
            get => this.Get<string>(nameof(this.OldName));
            private set => this.Set(nameof(this.OldName), value);
        }

        /// <summary>
        /// Значение, показывающее, наличие изменений полей этапа.
        /// </summary>
        public bool HasPlainChanges
        {
            get => this.Get<bool>(nameof(this.HasPlainChanges));
            private set => this.Set(nameof(this.HasPlainChanges), BooleanBoxes.Box(value));
        }

        /// <summary>
        /// Значение, показывающее, является ли этап скрытым.
        /// </summary>
        public bool HiddenStage
        {
            get => this.Get<bool>(nameof(this.HiddenStage));
            private set => this.Set(nameof(this.HiddenStage), BooleanBoxes.Box(value));
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RouteDiff"/> данными нового этапа.
        /// </summary>
        /// <param name="rowID">Идентификатор строки этапа.</param>
        /// <param name="name">Название этапа.</param>
        /// <param name="hiddenStage">Значение <see langword="true"/>, если этап является скрытым, иначе - <see langword="false"/>.</param>
        /// <returns>Инициализированный объект.</returns>
        public static RouteDiff NewStage(
            Guid rowID,
            string name,
            bool hiddenStage)
        {
            return new RouteDiff()
            {
                Action = RouteDiffAction.Insert,
                RowID = rowID,
                ActualName = name,
                HiddenStage = hiddenStage,
                HasPlainChanges = true,
            };
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RouteDiff"/> данными изменённого этапа.
        /// </summary>
        /// <param name="rowID">Идентификатор строки этапа.</param>
        /// <param name="actualName">Актуальное название этапа.</param>
        /// <param name="oldName">Предыдущее название этапа.</param>
        /// <param name="hiddenStage">Значение <see langword="true"/>, если этап является скрытым, иначе - <see langword="false"/>.</param>
        /// <param name="hasPlainChanges">Значение <see langword="true"/>, если были изменены поля этапа, иначе - <see langword="false"/>.</param>
        /// <returns>Инициализированный объект.</returns>
        public static RouteDiff ModifyStage(
            Guid rowID,
            string actualName,
            string oldName,
            bool hiddenStage,
            bool hasPlainChanges = true)
        {
            return new RouteDiff()
            {
                Action = RouteDiffAction.Modify,
                RowID = rowID,
                ActualName = actualName,
                OldName = string.Equals(actualName, oldName, StringComparison.Ordinal)
                    ? actualName
                    : oldName,
                HiddenStage = hiddenStage,
                HasPlainChanges = hasPlainChanges,
            };
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RouteDiff"/> данными удалённого этапа.
        /// </summary>
        /// <param name="rowID">Идентификатор строки этапа.</param>
        /// <param name="name">Название удалённого этапа.</param>
        /// <param name="hiddenStage">Значение <see langword="true"/>, если этап является скрытым, иначе - <see langword="false"/>.</param>
        /// <returns>Инициализированный объект.</returns>
        public static RouteDiff DeleteStage(
            Guid rowID,
            string name,
            bool hiddenStage)
        {
            return new RouteDiff()
            {
                Action = RouteDiffAction.Delete,
                RowID = rowID,
                OldName = name,
                HiddenStage = hiddenStage,
            };
        }

        #endregion
    }
}
