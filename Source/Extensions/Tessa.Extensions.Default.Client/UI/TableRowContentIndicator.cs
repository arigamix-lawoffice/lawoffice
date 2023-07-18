using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Metadata;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Предоставляет методы модифицирующие отображаемое значение в строке табличного контрола, если существует хотя бы одно контролируемое поле содержащее значение отличное от значения по умолчанию.
    /// </summary>
    public sealed class TableRowContentIndicator
    {
        #region Nested types

        private sealed class EventHandlerManager<T>
        {
            private readonly Action<object, string, T> handler;

            public string SectionName { get; }

            public EventHandlerManager(
                string sectionName,
                Action<object, string, T> handler)
            {
                this.SectionName = sectionName;
                this.handler = handler;
            }

            public void EventHandler(object sender, T e)
            {
                this.handler(sender, this.SectionName, e);
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Карточка содержащая источник данных для элемента управления <see cref="control"/>.
        /// </summary>
        private readonly Card card;

        /// <summary>
        /// Элемент управления, отображаемое значение ячеек которого необходимо модифицировать, если контролиремые поля содержат значения отличные от значений по умолчанию.
        /// </summary>
        private readonly GridViewModel control;

        /// <summary>
        /// Коллекция содержащая метаинформацию о секциях карточки <see cref="card"/>.
        /// </summary>
        private readonly CardMetadataSectionCollection cardMetadataSections;

        /// <summary>
        /// Порядковый номер столбца, в ячейках которого должно выполняться дополнительное форматирование отображаемых значений в соответствии с <see cref="cellFormat"/>.
        /// </summary>
        private readonly int columnOrder;

        /// <summary>
        /// Формат строки применяемый к отображаемому значению ячейки таблицы, если существует хотя бы одно контролируемое поле, привязанное к элементу управления расположенному на форме редактирования строки, содержащее значение отличное от значения по умолчанию.
        /// </summary>
        private readonly string cellFormat;

        /// <summary>
        /// Словарь содержащий информацию о контролируемых значениях. Ключ - имя секции; значение - коллекция содержащая информацию о контролируемых полях.
        /// </summary>
        private readonly Dictionary<string, CardMetadataColumnCollection> columnInfoDict;

        private HashSet<string, EventHandlerManager<ListStorageItemEventArgs<CardRow>>> itemChangedHandlerManagers;
        private Dictionary<string, Dictionary<CardRow, EventHandlerManager<CardFieldChangedEventArgs>>> fieldChangedHandlerManagers;

        /// <summary>
        /// Словарь содержащий: ключ - имя ссылочной секции; значение - имя ссылочного поля.
        /// </summary>
        private Dictionary<string, string> referenceColumnNames;

        /// <summary>
        /// Словарь содержащий значения полей. Предназначен для установления факта изменения значения поля.
        /// </summary>
        private readonly Dictionary<Guid, Dictionary<string, Dictionary<string, object>>> oldRowFieldValues;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TableRowContentIndicator"/>.
        /// </summary>
        /// <param name="card">Карточка содержащая источник данных для элемента управления <paramref name="control"/>.</param>
        /// <param name="control">Элемент управления, отображаемое значение ячеек которого необходимо модифицировать, если контролиремые поля содержат значения отличные от значений по умолчанию.</param>
        /// <param name="cardMetadataSections">Коллекция содержащая метаинформацию о секциях карточки <paramref name="card"/>.</param>
        /// <param name="columnInfoDict">Словарь содержащий информацию о контролируемых значениях. Ключ - имя секции; значение - коллекция содержащая информацию о контролируемых полях.</param>
        /// <param name="columnOrder">Порядковый номер столбца, в ячейках которого должно выполняться дополнительное форматирование отображаемых значений в соответствии с <paramref name="cellFormat"/>.</param>
        /// <param name="cellFormat">Формат строки применяемый к отображаемому значению ячейки таблицы, если существует хотя бы одно контролируемое поле, привязанное к элементу управления расположенному на форме редактирования строки, содержащее значение отличное от значения по умолчанию.</param>
        public TableRowContentIndicator(
            Card card,
            GridViewModel control,
            CardMetadataSectionCollection cardMetadataSections,
            Dictionary<string, CardMetadataColumnCollection> columnInfoDict,
            int columnOrder = default,
            string cellFormat = "$KrActions_RowContentIndicatorFormat")
        {
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(control, nameof(control));
            Check.ArgumentNotNull(cardMetadataSections, nameof(cardMetadataSections));
            Check.ArgumentNotNull(columnInfoDict, nameof(columnInfoDict));
            Check.ArgumentNotNullOrEmpty(cellFormat, nameof(cellFormat));

            if (control.Columns.All(i => i.Index != columnOrder))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(columnOrder),
                    columnOrder,
                    LocalizationManager.Format("$KrActions_OutOfRangeColumnOrder", control.Name, columnOrder.ToString()));
            }

            this.card = card;
            this.control = control;
            this.cardMetadataSections = cardMetadataSections;
            this.columnInfoDict = columnInfoDict;
            this.columnOrder = columnOrder;
            this.cellFormat = cellFormat;

            this.oldRowFieldValues = new Dictionary<Guid, Dictionary<string, Dictionary<string, object>>>();
        }

        #endregion

        #region Public methods
        
        /// <summary>
        /// Начинает отслеживание значений контролируемых полей.
        /// </summary>
        public void StartTracking()
        {
            if (!this.columnInfoDict.Any())
            {
                return;
            }

            var referenceColumnNamesLocal = new Dictionary<string, string>(StringComparer.Ordinal);
            var controlSectionID = ((CardTypeTableControl)this.control.CardTypeControl).SectionID;

            foreach (var sectionName in this.columnInfoDict.Where(i => !i.Value.Any()).Select(i => i.Key))
            {
                var parentColumnSection =
                    this.cardMetadataSections[sectionName]
                    .Columns.FirstOrDefault(
                        i => i.ColumnType == CardMetadataColumnType.Physical
                        && i.ParentRowSection?.ID == controlSectionID);

                if (parentColumnSection != null)
                {
                    referenceColumnNamesLocal.Add(sectionName, parentColumnSection.Name);
                }
            }

            this.referenceColumnNames = referenceColumnNamesLocal;

            var itemChangedHandlerManagersLocal = new HashSet<string, EventHandlerManager<ListStorageItemEventArgs<CardRow>>>(i => i.SectionName, StringComparer.Ordinal);
            var fieldChangedHandlerManagersLocal = new Dictionary<string, Dictionary<CardRow, EventHandlerManager<CardFieldChangedEventArgs>>>(StringComparer.Ordinal);

            foreach (var columnInfoPair in this.columnInfoDict)
            {
                var sectionName = columnInfoPair.Key;
                var rows = this.card.Sections[sectionName].Rows;

                if (columnInfoPair.Value.Any())
                {
                    if (!itemChangedHandlerManagersLocal.TryGetItem(sectionName, out var itemChangedHandlerManager))
                    {
                        itemChangedHandlerManager = new EventHandlerManager<ListStorageItemEventArgs<CardRow>>(sectionName, this.SectionRows_ItemChanged);
                        itemChangedHandlerManagersLocal.Add(itemChangedHandlerManager);
                    }

                    rows.ItemChanged += itemChangedHandlerManager.EventHandler;

                    if (!fieldChangedHandlerManagersLocal.TryGetValue(sectionName, out var eventHandlerManagers))
                    {
                        eventHandlerManagers = new Dictionary<CardRow, EventHandlerManager<CardFieldChangedEventArgs>>(rows.Count);
                    }

                    foreach (var row in rows)
                    {
                        var manager = new EventHandlerManager<CardFieldChangedEventArgs>(sectionName, this.Row_FieldChanged);
                        eventHandlerManagers.Add(row, manager);
                        row.FieldChanged += manager.EventHandler;
                    }
                }
                else
                {
                    // Обработка коллекционных полей являющихся источником данных для коллекционного автокомплита.
                    var itemChangedHandlerManager = new EventHandlerManager<ListStorageItemEventArgs<CardRow>>(sectionName, this.SubSectionRows_ItemChanged);
                    itemChangedHandlerManagersLocal.Add(itemChangedHandlerManager);
                    rows.ItemChanged += itemChangedHandlerManager.EventHandler;
                }
            }

            this.itemChangedHandlerManagers = itemChangedHandlerManagersLocal;
            this.fieldChangedHandlerManagers = fieldChangedHandlerManagersLocal;

            this.control.CellFormatFunc = context =>
            {
                if (context.Column.ColumnID != this.columnOrder.ToString() ||
                    !this.HasModify(context.Card, context.Row.RowID))
                {
                    return context.FormattedValue;
                }

                return LocalizationManager.Format(this.cellFormat, context.FormattedValue);
            };
        }

        /// <summary>
        /// Прекращает отслеживание контролируемых полей, устанавливает свойство <see cref="GridViewModel.CellFormatFunc"/> в значение по умолчанию.
        /// </summary>
        public void StopTracking()
        {
            foreach (var columnInfoPair in this.columnInfoDict)
            {
                var sectionName = columnInfoPair.Key;
                var rows = this.card.Sections[sectionName].Rows;

                if (columnInfoPair.Value.Any())
                {
                    if (this.fieldChangedHandlerManagers.TryGetValue(sectionName, out var eventHandlerManagers))
                    {
                        foreach (var manager in eventHandlerManagers)
                        {
                            manager.Key.FieldChanged -= manager.Value.EventHandler;
                        }
                    }
                }
                
                if (this.itemChangedHandlerManagers.TryGetItem(sectionName, out var eventHandlerManager))
                {
                    rows.ItemChanged -= eventHandlerManager.EventHandler;
                }
            }

            this.control.CellFormatFunc = default;
            this.columnInfoDict.Clear();
            this.oldRowFieldValues.Clear();
        }

        /// <summary>
        /// Инициализирует объект типа <see cref="TableRowContentIndicator"/> и начинает отслеживание контролируемых полей.
        /// </summary>
        /// <param name="cardModel">Модель карточки, доступная в UI.</param>
        /// <param name="cardMetadata">Репозиторий с метаинформацией.</param>
        /// <param name="controlName">Имя контролируемого элемента управления.</param>
        /// <param name="columnOrder">Порядковый номер столбца в ячейках которого должно выполняться дополнительное форматирование отображаемых значений.</param>
        /// <param name="cellFormat">Формат строки применяемый к отображаемому значению ячейки таблицы, если существует хотя бы одно контролируемое поле являющееся источником данных для элемента управления, расположенному на форме редактирования строки, содержащее значение отличное от значения по умолчанию.</param>
        /// <param name="exceptSectionNames">Коллекция имён секций, которые не надо контролировать.</param>
        /// <param name="exceptSectionFieldNames">Словарь содержащий: ключ - имя секции; значение - коллекция имён полей которые не надо контролировать.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Объект <see cref="TableRowContentIndicator"/>.</returns>
        /// <remarks>После завершения работы следует вызвать метод <see cref="StopTracking"/> для остановки отслеживания контролируемых полей и задания свойству <see cref="GridViewModel.CellFormatFunc"/> значения по умолчанию.</remarks>
        public static async ValueTask<TableRowContentIndicator> InitializeAndStartTrackingAsync(
            ICardModel cardModel,
            ICardMetadata cardMetadata,
            string controlName,
            int columnOrder = default,
            string cellFormat = "$KrActions_RowContentIndicatorFormat",
            ICollection<string> exceptSectionNames = default,
            IDictionary<string, ICollection<string>> exceptSectionFieldNames = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardModel, nameof(cardModel));

            var metadataSections = await cardMetadata.GetSectionsAsync(cancellationToken).ConfigureAwait(false);
            CardType targetType = await CardComponentHelper.TryGetCardTypeAsync(cardModel.Card.TypeID, cardMetadata, cancellationToken);

            var visitor = new FindAttachedFieldControlVisitor(
                            controlName,
                            metadataSections,
                            exceptSectionNames: exceptSectionNames,
                            exceptSectionFieldNames: exceptSectionFieldNames);

            await targetType.DepthFirstVisitAsync(visitor, cancellationToken).ConfigureAwait(false);
            var actionOptionsGrid = (GridViewModel)cardModel.Controls[controlName];

            var indicator = new TableRowContentIndicator(
                cardModel.Card,
                actionOptionsGrid,
                metadataSections,
                visitor.GetColumnInfo(),
                columnOrder: columnOrder,
                cellFormat: cellFormat);

            indicator.StartTracking();
            return indicator;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Обработчик события <see cref="IListItemContainer{T}.ItemChanged"/>.
        /// </summary>
        /// <param name="_">Не используется.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="e">Информация о событии.</param>
        private void SectionRows_ItemChanged(
            object _,
            string sectionName,
            ListStorageItemEventArgs<CardRow> e)
        {
            switch (e.Action)
            {
                case ListStorageAction.Insert:
                    e.Item.StateChanged += StateChanged;
                    break;
                case ListStorageAction.Remove:
                    {
                        if (this.fieldChangedHandlerManagers.TryGetValue(sectionName, out var eventHandlerManagers))
                        {
                            var row = e.Item;
                            if (eventHandlerManagers.Remove(row, out var manager))
                            {
                                row.FieldChanged -= manager.EventHandler;
                            }
                        }

                        break;
                    }
                case ListStorageAction.Clear:
                    {
                        if (this.fieldChangedHandlerManagers.TryGetValue(sectionName, out var eventHandlerManagers))
                        {
                            foreach (var eventHandlerManager in eventHandlerManagers)
                            {
                                eventHandlerManager.Key.FieldChanged -= eventHandlerManager.Value.EventHandler;
                            }

                            eventHandlerManagers.Clear();
                        }

                        break;
                    }
            }

            void StateChanged(object sender, CardRowStateEventArgs _)
            {
                ((CardRow)sender).StateChanged -= StateChanged;

                if (!this.fieldChangedHandlerManagers.TryGetValue(sectionName, out var eventHandlerManagers))
                {
                    eventHandlerManagers = new Dictionary<CardRow, EventHandlerManager<CardFieldChangedEventArgs>>();
                    this.fieldChangedHandlerManagers.Add(sectionName, eventHandlerManagers);
                }

                var manager = new EventHandlerManager<CardFieldChangedEventArgs>(sectionName, this.Row_FieldChanged);
                var row = e.Item;
                eventHandlerManagers.Add(row, manager);
                row.FieldChanged += manager.EventHandler;

                // Уведомляем о добавлении новой строки.
                this.NotifyValueChanged(row.RowID, default, sectionName, default, default);
            }
        }

        /// <summary>
        /// Обработчик события изменения полей строки. Вызывает обновление ячейки содержащей индикатор.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="sectionName">Имя секции в которой произошли изменения.</param>
        /// <param name="e">Информация о событии.</param>
        private void Row_FieldChanged(
            object sender,
            string sectionName,
            CardFieldChangedEventArgs e)
        {
            this.NotifyValueChanged(((CardRow)sender).RowID, default, sectionName, e.FieldName, e.FieldValue);
        }
        
        /// <summary>
        /// Обработчик события <see cref="IListItemContainer{T}.ItemChanged"/>. Вызывает обновление ячейки содержащей индикатор при изменении коллекции строк дочерних секций. Например, коллекции строк привязанной к табличному автокомплиту.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="sectionName">Имя секции, изменение набора строк которой вызвало событие.</param>
        /// <param name="e">Информация о событии.</param>
        private void SubSectionRows_ItemChanged(
            object sender,
            string sectionName,
            ListStorageItemEventArgs<CardRow> e)
        {
            switch (e.Action)
            {
                case ListStorageAction.Insert:
                    e.Item.StateChanged += StateChanged;
                    break;
                case ListStorageAction.Remove:
                    this.OwnedRowNotifyValueChanged(sectionName, e.Item);
                    break;
                case ListStorageAction.Clear:
                    foreach (var row in this.control.Rows)
                    {
                        this.NotifyValueChanged(row, default, sectionName, default, default);
                    }
                    break;
            }

            void StateChanged(object sender, CardRowStateEventArgs _)
            {
                ((CardRow)sender).StateChanged -= StateChanged;

                this.OwnedRowNotifyValueChanged(sectionName, e.Item);
            }
        }

        /// <summary>
        /// Уведомляет ячейку, содержащую индикатор, о необходимости обновить своё значение.
        /// </summary>
        /// <param name="sectionName">Имя секции к которой относится изменяемая строка.</param>
        /// <param name="row">Изменяемая строка.</param>
        private void OwnedRowNotifyValueChanged(string sectionName, CardRow row)
        {
            var referenceColumnName = this.referenceColumnNames[sectionName];
            this.NotifyValueChanged((Guid)row[referenceColumnName], row, sectionName, default, default);
        }

        /// <summary>
        /// Уведомляет ячейку, содержащую индикатор, о необходимости обновить своё значение.
        /// </summary>
        /// <param name="rowID">Идентификатор строки содержащей ячейку, значение которой требуется обновить.</param>
        /// <param name="ownedRow">Дочерняя строка.</param>
        /// <param name="sectionName">Имя секции, в которой произошли изменения.</param>
        /// <param name="fieldName">Имя поля, в котором произошли изменения.</param>
        /// <param name="fieldValue">Новое значение поля.</param>
        private void NotifyValueChanged(
            Guid rowID,
            CardRow ownedRow,
            string sectionName,
            string fieldName,
            object fieldValue)
        {
            var cardRowViewModel = this.control.Rows.First(i => i.Model.RowID == rowID);
            this.NotifyValueChanged(cardRowViewModel, ownedRow, sectionName, fieldName, fieldValue);
        }

        /// <summary>
        /// Уведомляет ячейку, содержащую индикатор, о необходимости обновить своё значение.
        /// </summary>
        /// <param name="cardRowViewModel">Модель представления для строки таблицы содержащая ячейку в которой необходимо обновить отображаемое значение.</param>
        /// <param name="ownedRow">Дочерняя строка.</param>
        /// <param name="sectionName">Имя секции, в которой произошли изменения.</param>
        /// <param name="fieldName">Имя поля, в котором произошли изменения.</param>
        /// <param name="fieldValue">Новое значение поля.</param>
        private void NotifyValueChanged(
            CardRowViewModel cardRowViewModel,
            CardRow ownedRow,
            string sectionName,
            string fieldName,
            object fieldValue)
        {
            // Секция в которой произошли изменения не отслеживается?
            if (!this.columnInfoDict.TryGetValue(sectionName, out var columnInfos))
            {
                return;
            }

            var row = cardRowViewModel.Model;
            var rowID = row.RowID;

            if (fieldName != null)
            {
                // Поле, значение которого было изменено, не отслеживается?
                if (!columnInfos.Contains(fieldName))
                {
                    return;
                }

                // Информация о предыдущем значении отслеживаемого поля в строке не найдена?
                if (!this.oldRowFieldValues.TryGetValue(rowID, out var oldSectionFieldValues))
                {
                    oldSectionFieldValues = new Dictionary<string, Dictionary<string, object>>(StringComparer.Ordinal);
                    this.oldRowFieldValues.Add(rowID, oldSectionFieldValues);
                }

                // Информация о предыдущем значении поля секции sectionName добавляется первый раз?
                if (!oldSectionFieldValues.TryGetValue(sectionName, out var oldFieldValues))
                {
                    oldFieldValues = new Dictionary<string, object>(StringComparer.Ordinal);
                    oldSectionFieldValues.Add(sectionName, oldFieldValues);
                }

                // Значение не изменилось?
                if (Equals(oldFieldValues.TryGet<object>(fieldName), fieldValue))
                {
                    return;
                }
                else
                {
                    oldFieldValues[fieldName] = fieldValue;
                }
            }

            var isSetOwnedRow = ownedRow != null;
            var isSetFieldName = fieldName != null;
            
            if(isSetOwnedRow && isSetFieldName)
            {
                throw new InvalidOperationException("Error state. Parameters: " + nameof(ownedRow) + " != null and " + nameof(fieldName) + " != null.");
            }

            var formattedCell = cardRowViewModel.Cells[this.columnOrder];

            if (isSetOwnedRow && !isSetFieldName || !isSetOwnedRow && isSetFieldName)
            {
                var column = formattedCell.Column;

                // От изменённого поля зависит значение отображаемое в колонке с дополнительным форматированием?
                if (isSetOwnedRow
                    ? column.IsDependantOnOwnedRow(ownedRow, row)
                    : column.IsDependantOnRowField(fieldName))
                {
                    return;
                }
            }

            formattedCell.NotifyValueChanged();
        }

        /// <summary>
        /// Возвращает значение, показывающее, что существует хотя бы одно контролируемое поле содержащее значение отличное от значения по умолчанию.
        /// </summary>
        /// <param name="card">Карточка содержащая контролируемые поля.</param>
        /// <param name="rowID">Идентификатор текущей строки.</param>
        /// <returns>Значение <see langword="true"/>, если форма редактирования строки с указанным идентификатором содержит хотя бы одно значение, в контролируемом поле, отличное от значения по умолчанию, иначе - <see langword="false"/>.</returns>
        private bool HasModify(
            Card card,
            Guid rowID)
        {
            foreach (var columnInfoPair in this.columnInfoDict)
            {
                var sectionName = columnInfoPair.Key;

                IEnumerable<CardRow> rows;
                CardRow row;

                if (card.Sections.TryGetValue(sectionName, out var section)
                    && (rows = section.TryGetRows()) != null)
                {
                    if (this.referenceColumnNames.TryGetValue(sectionName, out var referenceColumnName))
                    {
                        if ((row = rows.FirstOrDefault(i => i.Get<Guid>(referenceColumnName) == rowID)) != null)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if ((row = rows.FirstOrDefault(i => i.RowID == rowID)) != null)
                        {
                            foreach (var columnInfo in columnInfoPair.Value)
                            {
                                if (row.Fields.TryGetValue(columnInfo.Name, out var value)
                                    && !Equals(value, columnInfo.DefaultValue))
                                {
                                    // Дополнительная проверка необходима для учёта значения string.Empty, которое получается, если в текстовом поле стереть ранее введённый текст.
                                    if (columnInfo.MetadataType.RuntimeType == typeof(string)
                                        && string.IsNullOrEmpty(columnInfo.DefaultValue as string)
                                        && value is string valueStr
                                        && string.IsNullOrEmpty(valueStr))
                                    {
                                        continue;
                                    }

                                    // Дополнительная проверка необходима для учёта значения null, которое получается после удаления привязки WE.
                                    if (columnInfo.MetadataType.RuntimeType == typeof(bool)
                                        && (columnInfo.DefaultValue is null || (columnInfo.DefaultValue is bool defaultValueT && !defaultValueT))
                                        && (value is null || value is bool valueT && !valueT))
                                    {
                                        continue;
                                    }

                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
