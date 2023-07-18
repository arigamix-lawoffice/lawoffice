using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Посещает элементы управления расположенные в типе карточки и формирует словарь содержащий информацию о колонках являющихся источником данных элементов управления расположенным на форме элемента управления типа <see cref="CardTypeTableControl"/>.
    /// </summary>
    public sealed class FindAttachedFieldControlVisitor : CardTypeVisitor
    {
        #region Fields

        private readonly string controlName;
        private readonly CardMetadataSectionCollection metadataSections;
        private readonly ICollection<string> exceptSectionNames;
        private readonly IDictionary<string, ICollection<string>> exceptSectionFieldNames;
        private readonly Dictionary<string, CardMetadataColumnCollection> columnInfoDict;
        private object parentObject;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FindAttachedFieldControlVisitor"/>.
        /// </summary>
        /// <param name="controlName">Имя элемента управления.</param>
        /// <param name="metadataSections">Коллекция, содержащая объекты <see cref="CardMetadataSection"/>.</param>
        /// <param name="exceptSectionNames">Коллекция имён секций, которые не следует включать в результат обхода.</param>
        /// <param name="exceptSectionFieldNames">Словарь содержащий: ключ - имя секции; значение - коллекция имён полей которые не следует включать в результат обхода.</param>
        public FindAttachedFieldControlVisitor(
            string controlName,
            CardMetadataSectionCollection metadataSections,
            ICollection<string> exceptSectionNames = default,
            IDictionary<string, ICollection<string>> exceptSectionFieldNames = default)
        {
            Check.ArgumentNotNull(metadataSections, nameof(metadataSections));

            this.controlName = controlName;
            this.metadataSections = metadataSections;
            this.exceptSectionNames = exceptSectionNames;
            this.exceptSectionFieldNames = exceptSectionFieldNames;

            this.columnInfoDict = new Dictionary<string, CardMetadataColumnCollection>(StringComparer.Ordinal);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает словарь содержащий: ключ - имя секции; значение - коллекция, содержащая объекты <see cref="CardMetadataColumn"/> с информацией о колонках являющихся источниками данных элементов управления.
        /// </summary>
        /// <returns>Словарь содержащий: ключ - имя секции; значение - коллекция, содержащая объекты <see cref="CardMetadataColumn"/> с информацией о колонках являющихся источниками данных элементов управления. Информация об источнике данных коллекционного автокомплита представлена только информацией о секции, информация о полях отсуствует.</returns>
        public Dictionary<string, CardMetadataColumnCollection> GetColumnInfo()
        {
            if (this.exceptSectionFieldNames != null
                && this.columnInfoDict.Any())
            {
                foreach (var exceptSectionName in this.exceptSectionFieldNames.Keys)
                {
                    if (this.columnInfoDict.TryGetValue(exceptSectionName, out var fieldInfos)
                        && !fieldInfos.Any())
                    {
                        this.columnInfoDict.Remove(exceptSectionName);
                    }
                }
            }

            return this.columnInfoDict;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override ValueTask VisitControlAsync(
            CardTypeControl control,
            CardTypeBlock block,
            CardTypeForm form,
            CardType type,
            CancellationToken cancellationToken = default)
        {
            switch (control)
            {
                case CardTypeEntryControl entry when this.parentObject == form:
                    {
                        var metadataSection = this.metadataSections[entry.SectionID];
                        var metadataColumns = metadataSection.Columns;
                        var sectionName = metadataSection.Name;

                        if (this.TryGetColumnInfos(sectionName, metadataColumns.Count, out var columnInfos))
                        {
                            this.AddColumn(
                                columnInfos,
                                sectionName,
                                metadataColumns,
                                entry.ComplexColumnID,
                                entry.PhysicalColumnIDList);
                        }

                        break;
                    }
                case CardTypeTableControl entry:
                    {
                        if (entry.Name == this.controlName)
                        {
                            this.parentObject = entry.Form;
                        }
                        else
                        {
                            if (this.parentObject == form)
                            {
                                if (entry.Type == CardControlTypes.AutoCompleteTable)
                                {
                                    // При нахождении коллекционного автокомплита в результат добавляется только информация о секции являющейся источником данных, информация о полях отсуствует.
                                    _ = this.TryGetColumnInfos(this.metadataSections[entry.SectionID].Name, 0, out _);
                                }
                            }
                        }

                        break;
                    }
            }

            return new ValueTask();
        }

        /// <summary>
        /// Возвращает коллекцию содержащую информацию о колонках являющихся источником данных для элементов управления.
        /// </summary>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="capacity">Начальная ёмкость коллекции.</param>
        /// <param name="fieldInfos">Коллекция содержащая информацию о колонках являющихся источником данных для элементов управления или значение по умолчанию для типа, если секция <paramref name="sectionName"/> не должна быть включена в результат обхода.</param>
        /// <returns>Значение <see langword="true"/>, если информация о секции успешно добавлена, иначе - <see langword="false"/>.</returns>
        private bool TryGetColumnInfos(string sectionName, int capacity, out CardMetadataColumnCollection fieldInfos)
        {
            if (this.exceptSectionNames?.Contains(sectionName) != true)
            {
                if (!this.columnInfoDict.TryGetValue(sectionName, out fieldInfos))
                {
                    fieldInfos = new CardMetadataColumnCollection(capacity);
                    this.columnInfoDict.Add(sectionName, fieldInfos);
                }

                return true;
            }

            fieldInfos = default;
            return false;
        }

        /// <summary>
        /// Добавляет информацию о колонках.
        /// </summary>
        /// <param name="сolumnInfos">Коллекция содержащаю информацию о колонках являющихся источником данных для элементов управления.</param>
        /// <param name="sectionName">Имя секции к которой относятся колонки <paramref name="complexColumnID"/> и <paramref name="physicalColumnIDList"/>.</param>
        /// <param name="metadataColumns">Коллекция, содержащая информацию с метаданными колонок текущей секции.</param>
        /// <param name="complexColumnID">Идентификатор комплексной колонки, в которой содержится значение поля, или <c>null</c>, если поле содержится в физической колонке или составлено из нескольких физических колонок.</param>
        /// <param name="physicalColumnIDList">Список идентификаторов физических колонок, которые определяют значения полей колонки.</param>
        private void AddColumn(
            CardMetadataColumnCollection сolumnInfos,
            string sectionName,
            CardMetadataColumnCollection metadataColumns,
            Guid? complexColumnID,
            ICollection<Guid> physicalColumnIDList)
        {
            ICollection<string> exceptFieldNames = default;
            var hasExceptFields = this.exceptSectionFieldNames?.TryGetValue(sectionName, out exceptFieldNames) == true;

            if (complexColumnID.HasValue)
            {
                var complexColumnIndex = metadataColumns[complexColumnID.Value].ComplexColumnIndex;

                foreach (var metadataColumn in metadataColumns.Where(i => i.ColumnType != CardMetadataColumnType.Complex && i.ComplexColumnIndex == complexColumnIndex))
                {
                    TryAddCoumnInfo(metadataColumn);
                }
            }
            else
            {
                foreach (var physicalColumnID in physicalColumnIDList)
                {
                    TryAddCoumnInfo(metadataColumns[physicalColumnID]);
                }
            }

            void TryAddCoumnInfo(
                CardMetadataColumn metadataColumn)
            {
                if (!hasExceptFields || !exceptFieldNames.Contains(metadataColumn.Name))
                {
                    сolumnInfos.Add(metadataColumn);
                }
            }
        }

        #endregion
    }
}
