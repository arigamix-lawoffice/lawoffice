#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Объект, предоставляющий методы для переноса данных между секциями.
    /// </summary>
    public readonly struct KrProcessSectionMapper
    {
        #region Nested Types

        /// <summary>
        /// Предоставляет информацию о позиции этапа.
        /// </summary>
        [DebuggerDisplay("{" + nameof(StagePosition.ToDebugString) + "(), nq}")]
        private sealed class StagePosition
        {
            #region Constants And Static Fields

            /// <summary>
            /// Порядковый индекс позиции скрытого этапа.
            /// </summary>
            private const int HiddenStagePositionIndex = int.MinValue;

            #endregion

            #region Constructors

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="StagePosition"/>.
            /// </summary>
            /// <param name="krStagePositionInfo">Информация о позиции этапа, полученная с клиента, или значение <see langword="null"/>, если она недоступна. Формируется при загрузке карточки.</param>
            /// <param name="savedRow">Сохранённая в основном сателлите строка этапа или значение <see langword="null"/>, если строка новая (в том числе при создании карточки по шаблону).</param>
            /// <param name="modifiedRow"><inheritdoc cref="ModifiedRow" path="/summary"/></param>
            public StagePosition(
                KrStagePositionInfo? krStagePositionInfo,
                CardRow? savedRow,
                CardRow? modifiedRow)
            {
                if (krStagePositionInfo is null && modifiedRow is null)
                {
                    throw new ArgumentException($"Both {nameof(krStagePositionInfo)} and {nameof(modifiedRow)} is null.");
                }

                if (savedRow is null && modifiedRow is null)
                {
                    throw new ArgumentException($"Both {nameof(savedRow)} and {nameof(modifiedRow)} is null.");
                }

                this.ModifiedRow = modifiedRow;
                this.HiddenRow = krStagePositionInfo?.CardRow;
                this.RowID = krStagePositionInfo?.RowID ?? modifiedRow!.RowID;
                this.StageGroupOrder = krStagePositionInfo?.GroupOrder
                    ?? modifiedRow!.Fields.Get<int>(KrConstants.KrStages.StageGroupOrder);
                this.AbsoluteOrder = krStagePositionInfo?.AbsoluteOrder;
                this.ShiftedOrder = krStagePositionInfo?.ShiftedOrder;
                this.ModifiedOrder = modifiedRow?.TryGet<int?>(KrConstants.KrStages.Order);
                this.CombinedOrder = this.ModifiedOrder ?? this.ShiftedOrder ?? HiddenStagePositionIndex;

                if (modifiedRow is null)
                {
                    this.IsSkip = (savedRow?.TryGet<bool>(KrConstants.KrStages.Skip) ?? false)
                        && KrProcessSharedHelper.CanBeSkipped(savedRow);
                }
                else
                {
                    var isSkip = modifiedRow.TryGet<bool?>(KrConstants.KrStages.Skip);

                    this.IsSkip = isSkip.HasValue
                        ? isSkip.Value
                          && KrProcessSharedHelper.CanBeSkipped(
                              savedRow is null
                              ? modifiedRow
                              : savedRow)
                        : (savedRow?.TryGet<bool>(KrConstants.KrStages.Skip) ?? false)
                          && KrProcessSharedHelper.CanBeSkipped(savedRow);
                }

                this.IsHidden = this.CombinedOrder == HiddenStagePositionIndex; // Этап не отображается в карточке?
                this.IsNew = this.ModifiedOrder.HasValue && krStagePositionInfo is null;
                this.GroupPosition = krStagePositionInfo?.GroupPosition;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Идентификатор строки этапа.
            /// </summary>
            public Guid RowID { get; }

            /// <summary>
            /// Порядок сортировки группы этапов.
            /// </summary>
            public int StageGroupOrder { get; }

            /// <summary>
            /// Абсолютный порядок этапа в маршруте.
            /// </summary>
            public int? AbsoluteOrder { get; }

            /// <summary>
            /// Сдвинутый порядок этапа с учетом скрытых этапов или значение <see langword="null"/>, если этап является новым или скрытым.
            /// </summary>
            public int? ShiftedOrder { get; }

            /// <summary>
            /// Изменённый на клиенте порядок этапа или значение <see langword="null"/>, если порядок этапа не изменялся.
            /// </summary>
            public int? ModifiedOrder { get; }

            /// <summary>
            /// Результат выражения: <see cref="ModifiedOrder"/> ?? <see cref="ShiftedOrder"/> ?? <see cref="HiddenStagePositionIndex"/>.
            /// </summary>
            public int CombinedOrder { get; }

            /// <summary>
            /// Этап пропущен.
            /// </summary>
            public bool IsSkip { get; }

            /// <summary>
            /// Флаг, показывающий, что этап не отображается в карточке.
            /// </summary>
            public bool IsHidden { get; }

            /// <summary>
            /// Этап добавлен пользователем.
            /// </summary>
            public bool IsNew { get; }

            /// <summary>
            /// Позиция в группе.
            /// </summary>
            public int? GroupPosition { get; }

            /// <summary>
            /// Изменённая строка этапа (в том числе строка при создании карточки по шаблону) или значение <see langword="null"/>, если строка этапа не изменялась.
            /// </summary>
            public CardRow? ModifiedRow { get; }

            /// <summary>
            /// Скрытая строка из карточки.
            /// Присутствует, если с клиента приходит скрытая строка на добавление.
            /// </summary>
            public CardRow? HiddenRow { get; }

            #endregion

            #region Private Methods

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
                    $", {nameof(this.AbsoluteOrder)} = {DebugHelper.FormatNullable(this.AbsoluteOrder)}" +
                    $", {nameof(this.ShiftedOrder)} = {DebugHelper.FormatNullable(this.ShiftedOrder)}" +
                    $", {nameof(this.ModifiedOrder)} = {DebugHelper.FormatNullable(this.ModifiedOrder)}" +
                    $", {nameof(this.CombinedOrder)} = {this.CombinedOrder}" +
                    $", {nameof(this.StageGroupOrder)} = {this.StageGroupOrder}" +
                    $", {nameof(this.GroupPosition)} = {DebugHelper.FormatNullable(this.GroupPosition)}" +
                    $", {nameof(this.IsHidden)} = {this.IsHidden}" +
                    $", {nameof(this.IsSkip)} = {this.IsSkip}";
            }

            #endregion
        }

        private sealed class GroupOrderComparer :
            IComparer<StagePosition>
        {
            /// <inheritdoc />
            public int Compare(
                StagePosition? x,
                StagePosition? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (x is null)
                {
                    return -1;
                }

                if (y is null)
                {
                    return 1;
                }

                return x.StageGroupOrder.CompareTo(y.StageGroupOrder);
            }
        }

        private sealed class CombinedOrderComparer :
            IComparer<StagePosition>
        {
            /// <inheritdoc />
            public int Compare(
                StagePosition? x,
                StagePosition? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (x is null)
                {
                    return -1;
                }

                if (y is null)
                {
                    return 1;
                }

                return x.CombinedOrder.CompareTo(y.CombinedOrder);
            }
        }

        #endregion

        #region Constants And Static Fields

        private static readonly string[] serviceFields =
        {
            KrConstants.KrProcessCommonInfo.CurrentApprovalStageRowID,
            KrConstants.KrApprovalCommonInfo.StateChangedDateTimeUTC,
            KrConstants.KrApprovalCommonInfo.StateID,
            KrConstants.KrApprovalCommonInfo.StateName,
            KrConstants.KrApprovalCommonInfo.DisapprovedBy,
            KrConstants.KrApprovalCommonInfo.ApprovedBy,
            KrConstants.KrProcessCommonInfo.MainCardID,
        };

        private static readonly GroupOrderComparer groupOrderComparer = new GroupOrderComparer();

        private static readonly CombinedOrderComparer combinedOrderComparer = new CombinedOrderComparer();

        #endregion

        #region Fields

        private readonly Card source;
        private readonly Card destination;
        private readonly bool defaultAddDestinationSection;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessSectionMapper"/>.
        /// </summary>
        /// <param name="source">Карточка, из которой выполняется перенос данных.</param>
        /// <param name="destination">Карточка, в которую выполняется перенос данных.</param>
        /// <param name="defaultAddDestinationSection">Значение <see langword="true"/>, если секция, в которую переносятся данные, расположенная в <paramref name="destination"/>, должна быть добавлена при её отсутствии, иначе - <see langword="false"/>. Значение может быть переопределено в <see cref="Map(string, string, Action{CardSection, IDictionary{string, object?}}?, bool?)"/>.</param>
        public KrProcessSectionMapper(
            Card source,
            Card destination,
            bool defaultAddDestinationSection = false)
        {
            this.source = NotNullOrThrow(source);
            this.destination = NotNullOrThrow(destination);
            this.defaultAddDestinationSection = defaultAddDestinationSection;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Переносит данные из секции <paramref name="sourceSectionAlias"/> в <paramref name="destinationSectionAlias"/>.
        /// </summary>
        /// <param name="sourceSectionAlias">Алиас секции, из которой выполняется перенос данных.</param>
        /// <param name="destinationSectionAlias">Алиас секции, в которую выполняется перенос данных.</param>
        /// <param name="modifyAction">
        /// Метод, выполняемый после переноса данных, или значение <see langword="null"/>, если дополнительной обработки не требуется.<para/>
        /// Параметры:<br/>
        /// Секция, в которую был выполнен перенос данных.<br/>
        /// Значения полей для строковой секции или строки, если секция коллекционная.
        /// </param>
        /// <param name="addDestinationSection">Значение <see langword="true"/>, если секция, в которую переносятся данные, расположенная в <paramref name="destination"/>, должна быть добавлена при её отсутствии, иначе - <see langword="false"/>. Если значение не задано, то используется значение, указанное в конструкторе <see cref="KrProcessSectionMapper(Card,Card,bool)"/>.</param>
        /// <returns>Объект <see cref="KrProcessSectionMapper"/> для создания цепочки вызовов.</returns>
        public KrProcessSectionMapper Map(
            string sourceSectionAlias,
            string destinationSectionAlias,
            Action<CardSection, IDictionary<string, object?>>? modifyAction = null,
            bool? addDestinationSection = null)
        {
            ThrowIfNullOrEmpty(sourceSectionAlias);
            ThrowIfNullOrEmpty(destinationSectionAlias);

            if (!this.source.Sections.TryGetValue(sourceSectionAlias, out var sourceSection))
            {
                return this;
            }

            CardSection? destinationSection = null;
            if (addDestinationSection ?? this.defaultAddDestinationSection)
            {
                destinationSection = this.destination.Sections.GetOrAdd(destinationSectionAlias);
            }

            if (destinationSection is not null
                || this.destination.Sections.TryGetValue(destinationSectionAlias, out destinationSection))
            {
                StorageHelper.Merge(sourceSection.GetStorage(), destinationSection.GetStorage());

                if (modifyAction is not null)
                {
                    if (destinationSection.Type == CardSectionType.Entry)
                    {
                        modifyAction.Invoke(destinationSection, destinationSection.RawFields);
                    }
                    else if (destinationSection.Type == CardSectionType.Table)
                    {
                        foreach (var row in destinationSection.Rows)
                        {
                            modifyAction.Invoke(destinationSection, row);
                        }
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Переносит несериализуемые данные из виртуальной секции <see cref="KrConstants.KrApprovalCommonInfo.Virtual"/> в физическую <see cref="KrConstants.KrApprovalCommonInfo.Name"/>.
        /// </summary>
        /// <returns>Объект <see cref="KrProcessSectionMapper"/> для создания цепочки вызовов.</returns>
        public KrProcessSectionMapper MapApprovalCommonInfo()
        {
            if (!this.source.TryGetKrApprovalCommonInfoSection(out var aci))
            {
                return this;
            }

            var satelliteAci = this.destination.GetApprovalInfoSection();
            foreach (var field in aci.RawFields)
            {
                if (serviceFields.Contains(field.Key, StringComparer.Ordinal))
                {
                    continue;
                }

                satelliteAci.Fields[field.Key] = field.Value;
            }
            return this;
        }

        /// <summary>
        /// Переносит несериализуемые данные из виртуальной секции <see cref="KrConstants.KrStages.Virtual"/> в физическую <see cref="KrConstants.KrStages.Name"/>.
        /// </summary>
        /// <returns>Объект <see cref="KrProcessSectionMapper"/> для создания цепочки вызовов.</returns>
        public KrProcessSectionMapper MapKrStages()
        {
            if (!this.source.TryGetStagesSection(out var sourceSec, true)
                || !this.destination.TryGetStagesSection(out var destSec))
            {
                return this;
            }

            if (HasOrderChanges(sourceSec)
                && this.source.TryGetStagePositions(out var stagePositions)
                && stagePositions.Count > 0)
            {
                MapKrStagesWithHidden(sourceSec, destSec, stagePositions);
            }
            else
            {
                MapKrStagesSimple(sourceSec, destSec);
            }
            return this;
        }

        #endregion

        #region Private Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasOrderChanges(
            CardSection sec) =>
            sec.Rows.Any(p => p.ContainsKey(KrConstants.KrStages.Order));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MapKrStagesWithHidden(
            CardSection sourceSec,
            CardSection destSec,
            IReadOnlyCollection<KrStagePositionInfo> stagePositionsInfo)
        {
            var srcRows = sourceSec.Rows;
            var destRows = destSec.Rows;

            var stagePositions = TransformToPositionsArray(srcRows, stagePositionsInfo, destRows);
            if (stagePositions.Length == 0)
            {
                return;
            }

            SortStagePositions(stagePositions);

            CardRow? destRow;
            var order = 0;
            for (var i = 0; i < stagePositions.Length; i++)
            {
                var pos = stagePositions[i];
                var srcRow = pos.ModifiedRow;
                if (srcRow is null)
                {
                    if (pos.HiddenRow is not null)
                    {
                        // Новый скрытый этап с клиента
                        var row = destRows.Add();
                        InsertInternal(pos.HiddenRow, row);
                        row.Fields[KrConstants.KrStages.Order] = Int32Boxes.Box(order);
                        row.Fields[KrConstants.KrStages.Hidden] = BooleanBoxes.True;
                    }
                    else
                    {
                        // Скрытый этап, в нем нужно только ордер пофиксать
                        destRow = destRows.FirstOrDefault(p => p.RowID == pos.RowID);
                        if (destRow is not null)
                        {
                            destRow.Fields[KrConstants.KrStages.Order] = Int32Boxes.Box(order);
                        }
                    }
                }
                else
                {
                    switch (srcRow.State)
                    {
                        case CardRowState.Modified:
                            destRow = destRows.FirstOrDefault(p => p.RowID == pos.RowID);
                            if (destRow is not null)
                            {
                                ModifyInternal(srcRow, destRow);
                                if (destRow.TryGetValue(KrConstants.KrStages.Order, out var oldOrder)
                                    && !order.Equals(oldOrder))
                                {
                                    destRow.Fields[KrConstants.KrStages.Order] = Int32Boxes.Box(order);
                                }
                            }
                            break;
                        case CardRowState.Inserted:
                            var row = destRows.Add();
                            InsertInternal(srcRow, row);
                            row.Fields[KrConstants.KrStages.Order] = Int32Boxes.Box(order);
                            row.Fields[KrConstants.KrStages.Hidden] = BooleanBoxes.False;

                            break;
                        case CardRowState.Deleted:
                            var rowToDelete = destRows.FirstOrDefault(p => p.RowID == srcRow.RowID);
                            if (rowToDelete is not null)
                            {
                                rowToDelete.State = CardRowState.Deleted;
                            }

                            // Удаление не меняет порядок
                            order--;
                            break;
                    }
                }

                order++;
            }
        }

        /// <summary>
        /// Сортирует позиции этапов.
        /// </summary>
        /// <param name="stagePositions">Массив, содержащий информацию о позиции этапов.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SortStagePositions(
            StagePosition[] stagePositions)
        {
            // Сортировка этапов по порядковому номеру группы (GroupOrder).
            Array.Sort(stagePositions, 0, stagePositions.Length, groupOrderComparer);

            // Выходит n неупорядоченных подмножеств, которые между собой упорядочены по GroupOrder.
            var currStage = stagePositions[0]; // Текущий этап.
            var currGroupID = currStage.StageGroupOrder; // Идентификатор текущей группы этапов.
            var groupFirstIdx = 0; // Порядковый номер этапа с которого начинается текущая группа.
            var hiddenCount = currStage.IsHidden ? 1 : 0; // Число скрытых этапов.
            for (var currIdx = 1; currIdx < stagePositions.Length; currIdx++)
            {
                currStage = stagePositions[currIdx];
                if (currStage.StageGroupOrder != currGroupID)
                {
                    SortStagePositionInGroup(stagePositions, groupFirstIdx, currIdx, hiddenCount);
                    hiddenCount = 0;
                    groupFirstIdx = currIdx;
                    currGroupID = currStage.StageGroupOrder;
                }
                if (currStage.IsHidden)
                {
                    hiddenCount++;
                }
            }
            // Обработка последней группы.
            SortStagePositionInGroup(stagePositions, groupFirstIdx, stagePositions.Length, hiddenCount);
        }

        /// <summary>
        /// Выполняет сортировку этапов внутри группы.
        /// </summary>
        /// <param name="stagePositions">Массив, содержащий информацию о позиции этапов.</param>
        /// <param name="groupFirstIdx">Порядковый номер этапа с которого начинается рассматриваемая группа.</param>
        /// <param name="groupLastIdx">Порядковый номер последнего этапа в рассматриваемой группе.</param>
        /// <param name="hiddenStageCount">Число скрытых этапов в рассматриваемой группе.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SortStagePositionInGroup(
            StagePosition[] stagePositions,
            int groupFirstIdx,
            int groupLastIdx,
            int hiddenStageCount)
        {
            var groupLength = groupLastIdx - groupFirstIdx;
            if (groupLength <= 1)
            {
                return;
            }

            // Сортировка этапов входящих в groupFirstIdx группу по CombinedOrder.
            Array.Sort(stagePositions, groupFirstIdx, groupLength, combinedOrderComparer);

            if (hiddenStageCount == 0)
            {
                // Нет скрытых этапов в группе.
                return;
            }

            // Теперь необходимо разобраться, где должны быть во всем этом скрытые этапы с помощью ao
            // Все скрытые этапы, которые содержат только ao, будут в начале т.к. co == int.MinValue
            var firstFixedIndex = groupFirstIdx + hiddenStageCount;
            for (var i = firstFixedIndex - 1; i >= groupFirstIdx; i--, firstFixedIndex--)
            {
                // Частичная сортировка (модификация selection sort) для скрытых элементов
                var hiddenStage = stagePositions[i];
                var newIndex = FindHiddenStageIndex(stagePositions, hiddenStage, i, groupLastIdx - 1);
                if (newIndex != i)
                {
                    MoveStageToTheRight(stagePositions, i, newIndex);
                }
                // newIndex == firstFixedIndex оставляет скрытый этап в начале.
            }
            // CombinedOrder + Offset = new absolute order

        }

        /// <summary>
        /// Выполняет поиск позиции по которой должен располагаться указанный скрытый этап.
        /// </summary>
        /// <param name="stagePositions">Коллекция позиций этапов.</param>
        /// <param name="item">Скрытый этап, для которого выполняется поиск позиции.</param>
        /// <param name="from">Индекс элемента, с которого начинается поиск.</param>
        /// <param name="to">Индекс конечного элемента, до которого выполняется поиск.</param>
        /// <returns>Позиция, по которой должен располагаться указанный скрытый этап.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FindHiddenStageIndex(
            IReadOnlyList<StagePosition> stagePositions,
            StagePosition item,
            int from,
            int to)
        {
            // Поиск выполняется в интервале (<paramref name="from"/>; <paramref name="to"/>].
            var i = from + 1;

            // Находим первый не новый (с ао != null)
            for (; i <= to && stagePositions[i].IsNew; i++)
            {
            }

            // Все видимые этапы новые.
            if (i > to)
            {
                return item.GroupPosition == GroupPosition.AtFirst.ID
                    ? from
                    : to;
            }
            var prev = stagePositions[i];

            if (item.AbsoluteOrder < prev.AbsoluteOrder)
            {
                // Место перед prev.
                return item.GroupPosition == GroupPosition.AtFirst.ID
                    ? from // в самом начале, т.е. оставляем на месте.
                    : i - 1; // Внизу, где находятся "в конце группы".
                             // Берем i - 1, т.к. на i-той позиции уже с большим ao, т.е. "в конце группы"
            }

            var newCount = 0;
            for (; i <= to; i++)
            {
                var curr = stagePositions[i];
                if (curr.IsNew)
                {
                    newCount++;
                    continue;
                }

                if (prev.AbsoluteOrder < item.AbsoluteOrder
                    && item.AbsoluteOrder < curr.AbsoluteOrder)
                {
                    break;
                }

                prev = curr;
            }

            return item.GroupPosition == GroupPosition.AtFirst.ID
                ? i - newCount - 1
                : i - 1;
        }

        /// <summary>
        /// Перемещает элемент массива из позиции с индексом <paramref name="startIndex"/> в <paramref name="destIndex"/>.
        /// </summary>
        /// <param name="array">Массив содержащий перемещаемый элемент.</param>
        /// <param name="startIndex">Индекс, по которому расположен перемещаемый элемент.</param>
        /// <param name="destIndex">Индекс, по которому должен располагаться перемещаемый элемент.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveStageToTheRight(
            StagePosition[] array,
            int startIndex,
            int destIndex)
        {
            var tmp = array[startIndex];
            // Согласно документации операция безопасна даже при наложении областей.
            Array.Copy(array, startIndex + 1, array, startIndex, destIndex - startIndex);
            array[destIndex] = tmp;
        }

        /// <summary>
        /// Формирует массив элементов содержащих информацию о позиции этапов.
        /// </summary>
        /// <param name="sourceRows">Коллекция изменённых строк - этапов.</param>
        /// <param name="stagePositionsInfo">Коллекция содержащая информацию о позиции этапов сформированная при загрузке карточки.</param>
        /// <param name="savedRows">Коллекция строк этапов содержащая сохранённую ранее информацию.</param>
        /// <returns>Массив, содержащий информацию о позиции этапов.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static StagePosition[] TransformToPositionsArray(
            IReadOnlyCollection<CardRow> sourceRows,
            IReadOnlyCollection<KrStagePositionInfo> stagePositionsInfo,
            IReadOnlyCollection<CardRow> savedRows)
        {
            var stagePositionSet = new HashSet<Guid, StagePosition>(static i => i.RowID, sourceRows.Count + stagePositionsInfo.Count);
            var stagePositionInfoSet = new HashSet<Guid, KrStagePositionInfo>(static i => i.RowID, stagePositionsInfo);
            var savedRowSet = new HashSet<Guid, CardRow>(static i => i.RowID, savedRows);

            // Обработка изменённых строк.
            foreach (var sourceRow in sourceRows)
            {
                KrStagePositionInfo? stagePositionInfo = null;
                CardRow? savedRow = null;

                // Для нового этапа нет старой информации.
                if (sourceRow.State != CardRowState.Inserted)
                {
                    var rowID = sourceRow.RowID;
                    stagePositionInfoSet.TryGetItem(rowID, out stagePositionInfo);
                    savedRowSet.TryGetItem(rowID, out savedRow);
                }

                stagePositionSet.Add(new StagePosition(
                    stagePositionInfo,
                    savedRow,
                    sourceRow));
            }

            // Обработка старых строк.
            foreach (var savedRow in savedRows)
            {
                var rowID = savedRow.RowID;
                if (stagePositionSet.ContainsKey(rowID))
                {
                    continue;
                }

                stagePositionSet.Add(new StagePosition(
                    stagePositionInfoSet[rowID],
                    savedRow,
                    null));
            }

            return stagePositionSet.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MapKrStagesSimple(CardSection sourceSec, CardSection destSec)
        {
            var rows = destSec.Rows;
            foreach (var stSecRow in sourceSec.Rows)
            {
                switch (stSecRow.State)
                {
                    case CardRowState.Modified:
                        {
                            var row = rows.FirstOrDefault(p => p.RowID == stSecRow.RowID);

                            if (row is not null)
                            {
                                ModifyInternal(stSecRow, row);
                                if (stSecRow.TryGetValue(KrConstants.KrStages.Order, out var order))
                                {
                                    row.Fields[KrConstants.KrStages.Order] = order;
                                }
                            }

                            break;
                        }
                    case CardRowState.Inserted:
                        {
                            var row = rows.Add();
                            InsertInternal(stSecRow, row);
                            if (stSecRow.TryGetValue(KrConstants.KrStages.Order, out var order))
                            {
                                row.Fields[KrConstants.KrStages.Order] = order;
                            }
                            break;
                        }
                    case CardRowState.Deleted:
                        var rowToDelete = rows.FirstOrDefault(p => p.RowID == stSecRow.RowID);
                        if (rowToDelete is not null)
                        {
                            DeleteInternal(rowToDelete);
                        }
                        break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InsertInternal(
            CardRow sourceRow,
            CardRow destRow)
        {
            destRow.RowID = sourceRow.RowID;
            destRow.State = CardRowState.Inserted;
            foreach (var pair in sourceRow
                .Where(k => !k.Key.StartsWith(CardHelper.SystemKeyPrefix, StringComparison.Ordinal)
                    && !k.Key.StartsWith(CardHelper.UserKeyPrefix, StringComparison.Ordinal)
                    && !StageTypeSettingsNaming.IsPlainName(k.Key)
                    && !StageTypeSettingsNaming.IsSectionName(k.Key)
                    && k.Key != KrConstants.KrStages.Order))
            {
                destRow.Fields[pair.Key] = pair.Value;
            }

            destRow.Fields[KrConstants.KrStages.ExtraSources] = null;
            destRow.Fields[KrConstants.KrStages.NestedProcessID] = null;
            destRow.Fields[KrConstants.KrStages.ParentStageRowID] = null;
            destRow.Fields[KrConstants.KrStages.NestedOrder] = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ModifyInternal(
            CardRow sourceRow,
            CardRow destRow)
        {
            destRow.State = CardRowState.Modified;
            foreach (var pair in sourceRow
                .Where(k => !k.Key.StartsWith(CardHelper.SystemKeyPrefix, StringComparison.Ordinal)
                    && !k.Key.StartsWith(CardHelper.UserKeyPrefix, StringComparison.Ordinal)
                    && destRow.ContainsKey(k.Key)
                    && !StageTypeSettingsNaming.IsPlainName(k.Key)
                    && !StageTypeSettingsNaming.IsSectionName(k.Key)
                    && k.Key != KrConstants.KrStages.Order))
            {
                destRow.Fields[pair.Key] = pair.Value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool DeleteInternal(
            CardRow destRow)
        {
            if (KrProcessSharedHelper.CanBeSkipped(destRow))
            {
                destRow.State = CardRowState.Modified;

                destRow.Fields[KrConstants.KrStages.Skip] = BooleanBoxes.True;
                return false;
            }

            destRow.State = CardRowState.Deleted;
            return true;
        }

        #endregion
    }
}
