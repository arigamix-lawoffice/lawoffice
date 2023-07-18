#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет информацию о позиции этапа.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    [DebuggerDisplay("{" + nameof(KrStagePositionInfo.ToDebugString) + "(), nq}")]
    public sealed partial class KrStagePositionInfo :
        StorageObject
    {
        #region Fields

        [NonSerialized]
        private CardRow? cardRow;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStagePositionInfo"/>.
        /// </summary>
        /// <param name="stageRow">Строка для которой формируется информация о позиции.</param>
        /// <param name="absoluteOrder">Абсолютный порядок этапа в маршруте.</param>
        /// <param name="shiftedOrder">Сдвинутый порядок с учетом скрытых этапов.</param>
        /// <param name="saveRow">Значение <see langword="true"/>, если необходимо сохранить информацию о строке этапа для которой формируется информация о позиции, иначе - <see langword="false"/>.</param>
        /// <param name="hidden">Значение <see langword="true"/>, если этап является скрытым, иначе - <see langword="false"/>.</param>
        /// <param name="skip">Значение <see langword="true"/>, если этап является пропущенным, иначе - <see langword="false"/>.</param>
        public KrStagePositionInfo(
            CardRow stageRow,
            int absoluteOrder,
            int? shiftedOrder,
            bool saveRow,
            bool hidden,
            bool skip)
            : this(new Dictionary<string, object?>(StringComparer.Ordinal))
        {
            ThrowIfNull(stageRow);

            this.Set(nameof(this.RowID), stageRow.RowID);
            this.Set(nameof(this.GroupOrder), stageRow[KrConstants.KrStages.StageGroupOrder]);
            this.Set(nameof(this.AbsoluteOrder), absoluteOrder);
            this.Set(nameof(this.ShiftedOrder), shiftedOrder);
            this.Set(nameof(this.Hidden), BooleanBoxes.Box(hidden));
            this.Set(nameof(this.Name), stageRow[KrConstants.KrStages.NameField]);
            this.Set(nameof(this.StageGroupID), stageRow[KrConstants.KrStages.StageGroupID]);
            this.Set(nameof(this.GroupPosition), stageRow[KrConstants.KrStages.BasedOnStageTemplateGroupPositionID]);

            if (saveRow)
            {
                this.Set(nameof(this.CardRow), stageRow.GetStorage());
            }

            this.Set(nameof(this.Skip), BooleanBoxes.Box(skip));
        }

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public KrStagePositionInfo(
            Dictionary<string, object?> storage)
            : base(storage)
        {
            this.Init(nameof(this.RowID), GuidBoxes.Empty);
            this.Init(nameof(this.GroupOrder), Int32Boxes.Zero);
            this.Init(nameof(this.AbsoluteOrder), Int32Boxes.Zero);
            this.Init(nameof(this.ShiftedOrder), null);
            this.Init(nameof(this.Hidden), BooleanBoxes.False);
            this.Init(nameof(this.Name), null);
            this.Init(nameof(this.StageGroupID), GuidBoxes.Empty);
            this.Init(nameof(this.GroupPosition), null);
            this.Init(nameof(this.CardRow), null);
            this.Init(nameof(this.Skip), BooleanBoxes.False);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор скрытого этапа (оригинала из шаблона).
        /// </summary>
        public Guid RowID => this.Get<Guid>(nameof(this.RowID));

        /// <summary>
        /// Порядок сортировки группы этапов.
        /// </summary>
        public int GroupOrder => this.Get<int>(nameof(this.GroupOrder));

        /// <summary>
        /// Абсолютный порядок этапа в маршруте.
        /// </summary>
        public int AbsoluteOrder => this.Get<int>(nameof(this.AbsoluteOrder));

        /// <summary>
        /// Сдвинутый порядок с учетом скрытых этапов.
        /// </summary>
        public int? ShiftedOrder => this.Get<int?>(nameof(this.ShiftedOrder));

        /// <summary>
        /// Признак того, что этап является скрытым.
        /// </summary>
        public bool Hidden => this.Get<bool>(nameof(this.Hidden));

        /// <summary>
        /// Название этапа.
        /// </summary>
        public string? Name => this.TryGet<string>(nameof(this.Name));

        /// <summary>
        /// Групповая позиция этапа в рамках одной группы. <see cref="GroupPosition"/>
        /// </summary>
        public int? GroupPosition => this.Get<int?>(nameof(this.GroupPosition));

        /// <summary>
        /// Идентификатор группы этапа.
        /// </summary>
        public Guid StageGroupID => this.Get<Guid>(nameof(this.StageGroupID));

        /// <summary>
        /// Строка этапа.
        /// </summary>
        public CardRow? CardRow =>
            this.cardRow ??= this.CreateCardRow();

        /// <summary>
        /// Признак пропуска этапа.
        /// </summary>
        public bool Skip => this.Get<bool>(nameof(this.Skip));

        #endregion

        #region Private Methods

        private CardRow? CreateCardRow()
        {
            var storage = this.TryGet<Dictionary<string, object?>>(nameof(this.CardRow));
            return storage is null
                ? null
                : new CardRow(storage);
        }

        /// <summary>
        /// Возвращает строковое представление объекта, отображаемое в режиме отладки.
        /// </summary>
        /// <returns>
        /// Строковое представление объекта, отображаемое в режиме отладки.
        /// </returns>
        [UsedImplicitly]
        private string ToDebugString()
        {
            return $"{DebugHelper.GetTypeName(this)}: " +
                $"{nameof(this.RowID)} = {this.RowID}" +
                $", {nameof(this.Name)} = {this.Name}" +
                $", {nameof(this.AbsoluteOrder)} = {this.AbsoluteOrder}" +
                $", {nameof(this.GroupOrder)} = {this.GroupOrder}" +
                $", {nameof(this.GroupPosition)} = {DebugHelper.FormatNullable(this.GroupPosition)}" +
                $", {nameof(this.Skip)} = {this.Skip}" +
                $", {nameof(this.Hidden)} = {this.Hidden}";
        }

        #endregion
    }
}
