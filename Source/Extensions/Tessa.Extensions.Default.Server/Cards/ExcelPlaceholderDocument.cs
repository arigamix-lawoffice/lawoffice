using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Placeholders.Extensions;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using A = DocumentFormat.OpenXml.Drawing;
using Hyperlink = DocumentFormat.OpenXml.Spreadsheet.Hyperlink;
using Text = DocumentFormat.OpenXml.Spreadsheet.Text;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Объект, определяющий способы хранения и изменения текста с заменяемыми плейсхолдерами
    /// для документа Excel.
    /// </summary>
    public sealed class ExcelPlaceholderDocument :
        OpenXmlPlaceholderDocument
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класс с указанием потока файла документа, в котором должны быть заменены плейсхолдеры.
        /// </summary>
        /// <param name="stream">
        /// Поток файла документа, в котором должны быть заменены плейсхолдеры.
        /// Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="templateID">
        /// ID карточки шаблона файла.
        /// </param>
        /// <param name="parser">Парсер документа Excel.</param>
        public ExcelPlaceholderDocument(MemoryStream stream, Guid templateID, IExcelDocumentParser parser)
            : base(stream, templateID)
        {
            Check.ArgumentNotNull(parser, nameof(parser));

            this.parser = parser;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Парсер документа Excel.
        /// </summary>
        private readonly IExcelDocumentParser parser;

        /// <summary>
        /// Контекст расширений
        /// </summary>
        private ExcelPlaceholderReplaceExtensionContext extensionContext;

        /// <summary>
        /// Документ Excel
        /// </summary>
        private SpreadsheetDocument excelDocument;

        /// <summary>
        /// Элемент Stylesheet текущего документа. Хранит информацию о стилях ячеек
        /// </summary>
        private Stylesheet stylesheet;

        /// <summary>
        /// Элемент <see cref="SharedStringTableContainer"/>, являющийся оберткой над <see cref="SharedStringTable"/> текущего документа
        /// </summary>
        private SharedStringTableContainer sharedStringTable;

        /// <summary>
        /// Хеш всех WorksheetElement, содержащий таблицы.
        /// </summary>
        private HashSet<string, WorksheetElement> worksheetHash;

        /// <summary>
        /// Справочник со всеми объектами Worksheet
        /// </summary>
        private HashSet<Worksheet, WorksheetElement> worksheetsDictionary;

        #endregion

        #region Private Methods

        /// <summary>
        /// Производит замену Replacement'ов в заданном элементе Cell
        /// </summary>
        /// <param name="cell">Cell, в котором производится замена плейсхолдеров</param>
        /// <param name="createNew">Определяет, создается ли ноая ячейка или заменется значение в существующей</param>
        /// <param name="replacements">Массив плейсхолдеров для замены</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task ReplaceElementsInCellAsync(
            Cell cell,
            bool createNew,
            CancellationToken cancellationToken = default,
            params IPlaceholderReplacement[] replacements)
        {
            if (this.WithExtensions)
            {
                this.extensionContext.Cell = cell;
            }

            if (cell.DataType != null
                && cell.DataType.HasValue
                && cell.DataType.Value == CellValues.SharedString)
            {
                int sharedStringID = int.Parse(cell.InnerText);

                IPlaceholderReplacement firstReplacement = replacements.First();
                Type valueType = firstReplacement.NewValue.NetType;

                // Если формат ячейки задан явно (в его стиле NumberFormat отличен от 0) и в ячейке находится только данный плейсхолдер, то устанавливаем полученное значение в value
                if (valueType != typeof(string)
                    && valueType != typeof(DBNull)
                    && this.GetCellNumberFormat(cell) != 0
                    && this.GetSharedString(sharedStringID).Length == firstReplacement.Placeholder.Text.Length)
                {
                    if (this.WithExtensions)
                    {
                        this.extensionContext.PlaceholderValue = firstReplacement.NewValue;
                        this.extensionContext.Placeholder = firstReplacement.Placeholder;
                        await this.BeforePlaceholderReplaceAsync(this.extensionContext.ReplacementContext);
                    }

                    ReadOnlyCollection<PlaceholderField> fields = firstReplacement.NewValue.Fields;
                    object value = fields.Count > 0 ? fields[0].Value : null;

                    string newCellValue;
                    if (value == null)
                    {
                        newCellValue = firstReplacement.NewValue.Text;
                    }
                    else if (valueType == typeof(DateTime))
                    {
                        newCellValue = ((DateTime) value).ToOADate().ToString(ExcelHelper.DoubleExcelFormat);
                    }
                    else if ((valueType == typeof(decimal) || valueType == typeof(double) || valueType == typeof(float))
                        && value is IFormattable formattable)
                    {
                        newCellValue = formattable.ToString(null, ExcelHelper.DoubleExcelFormat);
                    }
                    else
                    {
                        newCellValue = firstReplacement.NewValue.Text;
                    }

                    cell.DataType = null;

                    cell.CellValue.Text = this.RemoveInvalidChars(newCellValue);
                    if (this.WithExtensions)
                    {
                        this.extensionContext.PlaceholderTextElement = cell.CellValue;
                        this.extensionContext.PlaceholderElement = cell;
                        await this.AfterPlaceholderReplaceAsync(this.extensionContext.ReplacementContext);
                    }
                }
                else // Иначе заменяем значение в SharedString
                {
                    SharedStringItem sharedString;
                    if (createNew)
                    {
                        (sharedString, sharedStringID) = this.CopySharedString(sharedStringID);
                    }
                    else
                    {
                        sharedString = this.sharedStringTable.ElementAt(sharedStringID);
                    }

                    await this.ReplaceElementsInSharedStringAsync(sharedString, cancellationToken, replacements);

                    cell.CellValue.Text = this.RemoveInvalidChars(sharedStringID.ToString());
                }
            }
            else if (cell.CellFormula != null)
            {
                foreach (var replacement in replacements)
                {
                    await this.ReplaceElementAsync(
                        cell.CellFormula,
                        replacement,
                        cancellationToken);
                }
                // Вместе с формулой может идти готовый текст, который при конвертации в PDF не будет перерасчитываться по формуле
                cell.CellValue = null;
            }
            else
            {
                await this.ReplaceElementsInCompositeElementAsync(cell, cancellationToken, replacements);
            }

            if (this.WithExtensions)
            {
                this.extensionContext.Cell = null;
            }
        }

        /// <summary>
        /// Метод для получения текста ячейки
        /// </summary>
        /// <param name="cell">Элемент ячейки</param>
        /// <returns>Возвращает текст ячейки</returns>
        private string GetCellText(Cell cell)
        {
            return cell.DataType != null
                && cell.DataType.HasValue
                && cell.DataType.Value == CellValues.SharedString
                && int.TryParse(cell.InnerText, out var num)
                    ? this.GetSharedString(num)
                    : cell.InnerText;
        }

        /// <summary>
        /// Возвращает все дочерние элементы с типом Text среди всех дочерних элементов объекта <c>baseElement</c>
        /// </summary>
        /// <param name="baseElement">Базовый элемент, начиная с которого производим поиск</param>
        /// <returns>Список элементов типа Text, или null, если <c>baseElement</c> не содержит дочерних элементов типа Text</returns>
        private static List<A.Text> GetTextElements(OpenXmlElement baseElement)
        {
            var result = new List<A.Text>();

            foreach (OpenXmlElement e in baseElement.ChildElements)
            {
                Type eType = e.GetType();
                if (eType == typeof(A.Text))
                {
                    result.Add((A.Text) e);
                }
                else if (eType != typeof(A.Hyperlink) && eType != typeof(A.Paragraph) && e.HasChildren)
                {
                    result.AddRange(GetTextElements(e));
                }
            }

            return result;
        }

        /// <summary>
        /// Метод для подготовки объектов Worksheet. Вызывается после инициалиации Worksheets и TableGroups.
        /// Цель метода - заполнить свойства источников данных формул и организовать связи между элементами с учетом таблиц.
        /// </summary>
        private bool PrepareWorksheetFormulas()
        {
            bool hasFormulas = false;
            foreach (var worksheet in this.worksheetHash)
            {
                int tableGroupIndex = 0;
                // Игнорируем листы, у которых нет формул.
                if (worksheet.HasFormulas)
                {
                    for (int rowIndex = 0; rowIndex < worksheet.Rows.Count; rowIndex++)
                    {
                        var row = worksheet.Rows[rowIndex];
                        if (row.Formulas is null
                            || row.Formulas.Count == 0)
                        {
                            continue;
                        }

                        hasFormulas = true;
                        for (int formulaIndex = 0; formulaIndex < row.Formulas.Count; formulaIndex++)
                        {
                            var formula = row.Formulas[formulaIndex];
                            var formulaTableGroup = GetTableGroup(worksheet.Tables, ref tableGroupIndex, formula.Reference);

                            for (int sourceIndex = 0; sourceIndex < formula.FormulaSources.Count; sourceIndex++)
                            {
                                var source = formula.FormulaSources[sourceIndex].Item3;
                                var sourceTableGroup = GetTableGroup(source.Worksheet.Tables, source);
                                if (source.InFormulaWorksheet)
                                {
                                    // Установка свойства InFormulaTableGroup
                                    if (formulaTableGroup is null
                                        || formulaTableGroup == sourceTableGroup
                                        || (sourceTableGroup != null && formulaTableGroup.IsInclude(sourceTableGroup)))
                                    {
                                        source.InFormulaTableGroup = true;
                                    }
                                }

                                // Установка свойства IsExpandable
                                if (sourceTableGroup != null
                                    && formulaTableGroup != sourceTableGroup
                                    && source.Top == sourceTableGroup.Top
                                    && source.Bottom == sourceTableGroup.Bottom)
                                {
                                    source.TableGroup = sourceTableGroup;
                                }

                                // Установка связи сорса со строкой
                                // Если строка уже есть в текущей таблице, ищем в ней
                                if (sourceTableGroup != null
                                    && source.Bottom >= sourceTableGroup.Bottom
                                    && source.Bottom <= sourceTableGroup.Top)
                                {
                                    var sourceRow = sourceTableGroup.Rows[source.Bottom - sourceTableGroup.Bottom];
                                    source.Row = sourceRow;
                                }
                                // Если нет, ищем в Worksheet
                                else if (source.Worksheet.Rows.TryFirst(x => x.Bottom == source.Bottom, out var sourceRow))
                                {
                                    source.Row = sourceRow;
                                }
                            }
                        }
                    }
                }
            }

            return hasFormulas;
        }

        private static TableGroup GetTableGroup(IList<TableGroup> tables, ref int from, string reference)
        {
            for (; from < tables.Count; from++)
            {
                var table = tables[from];
                if (table.IsInclude(reference))
                {
                    int innerFrom = 0;
                    return GetTableGroup(table.InnerGroups, ref innerFrom, reference) ?? table;
                }
            }

            return null;
        }

        private static TableGroup GetTableGroup(IList<TableGroup> tables, ICellsGroup cellsGroup)
        {
            for (int from = 0; from < tables.Count; from++)
            {
                var table = tables[from];
                if (table.IsInclude(cellsGroup))
                {
                    return GetTableGroup(table.InnerGroups, cellsGroup) ?? table;
                }
            }

            return null;
        }

        /// <summary>
        /// Метод производит обновление позиций всех внутренных объектов Worksheets
        /// </summary>
        private void UpdateWorksheets()
        {
            foreach (WorksheetElement worksheet in this.worksheetHash)
            {
                worksheet.Update();
            }
        }

        /// <summary>
        /// Метод для инициализации структуры Excel.
        /// </summary>
        /// <returns></returns>
        private ValidationResult InitializeWorksheets()
        {
            var (result, worksheetHash) = this.parser.ParseDocument(this.excelDocument);

            if (worksheetHash != null)
            {
                this.worksheetHash = worksheetHash;
                this.worksheetsDictionary = new HashSet<Worksheet, WorksheetElement>(x => x.Element, worksheetHash);
            }

            return result;
        }

        /// <summary>
        /// Метод производит привязку плейсхолдеров к таблицам
        /// </summary>
        /// <param name="placeholders">Плейсхолдеры</param>
        private void AttachPlaceholders(IList<IPlaceholder> placeholders)
        {
            foreach (IPlaceholder placeholder in placeholders)
            {
                WorksheetElement worksheet = this.GetWorksheetByPlaceholder(placeholder);
                OpenXmlElement baseElement = GetElementByPlaceholder(this.excelDocument.WorkbookPart, placeholder);

                // Если плейсхолдер относится к Relationship, то baseElement == null
                if (baseElement == null)
                {
                    string elementId = placeholder.Info.Get<IList>(OpenXmlHelper.PositionField).Cast<object>().Last().ToString();

                    HyperlinkCellGroup hyperlink = worksheet.Hyperlinks.FirstOrDefault(x => x.Element.Id == elementId);
                    if (hyperlink == null)
                    {
                        throw new InvalidOperationException("Can't find Hyperlink with position " +
                            TextPosition(placeholder.Info.Get<IList>(OpenXmlHelper.PositionField)));
                    }

                    baseElement = hyperlink.Element;

                    this.AddPlaceholderToTable(
                        worksheet.Tables,
                        hyperlink.Reference,
                        hyperlink,
                        placeholder,
                        (t, r, p) => t.AddHyperlinkPlaceholder(r, p));
                }
                else
                {
                    Type type = baseElement.GetType();
                    if (type == typeof(Cell))
                    {
                        // плейсхолдер в ячейке с текстом
                        Cell cell = (Cell) baseElement;

                        RowCellGroup row = worksheet.Rows.FirstOrDefault(x => x.IsInclude(cell.CellReference.Value));
                        if (row == null)
                        {
                            throw new InvalidOperationException("Can't find Row with position " +
                                TextPosition(placeholder.Info.Get<IList>(OpenXmlHelper.PositionField)));
                        }

                        this.AddPlaceholderToTable(
                            worksheet.Tables,
                            cell.CellReference.Value,
                            row,
                            placeholder,
                            (t, r, p) => t.AddRowPlaceholder(r, p));
                    }
                    else if (type == typeof(A.Paragraph))
                    {
                        // плейсхолдер в надписи
                        A.Paragraph paragraph = (A.Paragraph) baseElement;
                        AnchorCellGroup anchor = worksheet.Anchors.FirstOrDefault(x => x.HasChildElement(paragraph));
                        if (anchor == null)
                        {
                            throw new InvalidOperationException("Can't find Anchor with position " +
                                TextPosition(placeholder.Info.Get<IList>(OpenXmlHelper.PositionField)));
                        }

                        baseElement = anchor.Element;

                        this.AddPlaceholderToTable(
                            worksheet.Tables,
                            anchor.Reference,
                            anchor,
                            placeholder,
                            (t, r, p) => t.AddAnchorPlaceholder(r, p));
                    }
                }

                placeholder.Info.Add(OpenXmlHelper.BaseElementField, baseElement);
            }
        }

        private void AddPlaceholderToTable<SourceType>(
            IList<TableGroup> tables,
            string reference,
            SourceType source,
            IPlaceholder placeholder,
            Action<TableGroup, SourceType, IPlaceholder> addPlaceholderFunc)
        {
            TableGroup tableToAdd = null;
            bool continueSearch;

            do
            {
                continueSearch = false;
                foreach (var table in tables)
                {
                    if (table.IsInclude(reference))
                    {
                        tableToAdd = table;
                        if (table.InnerGroups.Count > 0)
                        {
                            tables = table.InnerGroups;
                            continueSearch = true;
                        }

                        break;
                    }
                }
            } while (continueSearch);

            if (tableToAdd != null
                && tableToAdd.Type != TableGroupType.Table)
            {
                addPlaceholderFunc(tableToAdd, source, placeholder);
            }
        }

        /// <summary>
        /// Метод получает объект Worksheet по плейсхолдеру
        /// </summary>
        /// <param name="placeholder">Плейсхолдер</param>
        /// <returns>Объект Worksheet, к которому принадлежит данный плейсхолдер</returns>
        private WorksheetElement GetWorksheetByPlaceholder(IPlaceholder placeholder)
        {
            if (this.worksheetsDictionary == null)
            {
                return null;
            }

            IList position = placeholder.Info.TryGet<IList>(OpenXmlHelper.PositionField);
            if (position == null)
            {
                return null;
            }

            OpenXmlPart mainPart = this.excelDocument.WorkbookPart;
            for (int i = 0; i < position.Count; i++)
            {
                if (position[i] is int index)
                {
                    if (index < 0)
                    {
                        mainPart = mainPart.Parts.ToArray()[~index].OpenXmlPart;

                        if (mainPart.RootElement is Worksheet worksheet)
                        {
                            return this.worksheetsDictionary[worksheet];
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return null;
        }

        private WorksheetPart GetWorksheetPartByPlaceholder(IPlaceholder placeholder)
        {
            IList position = placeholder.Info.TryGet<IList>(OpenXmlHelper.PositionField);
            if (position == null)
            {
                return null;
            }

            OpenXmlPart mainPart = this.excelDocument.WorkbookPart;
            for (int i = 0; i < position.Count; i++)
            {
                if (position[i] is int index)
                {
                    if (index < 0)
                    {
                        mainPart = mainPart.Parts.ToArray()[~index].OpenXmlPart;

                        if (mainPart is WorksheetPart worksheetPart)
                        {
                            return worksheetPart;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return null;
        }

        private async Task ReplacePlaceholdersInRowAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance tableGroupInstance,
            IPlaceholderRow row)
        {
            Row prevRow = null;
            for (int i = 0; i < tableGroupInstance.Rows.Length; i++)
            {
                var newRow = tableGroupInstance.Rows[i];
                var copyRow = tableGroup.Rows[i];

                prevRow = newRow.Element;

                if (this.WithExtensions)
                {
                    this.extensionContext.CurrentRowElement = prevRow;
                }

                // Берем все плейсхолдеры, относящиеся к текущей строке внутренней таблицы
                foreach (IGrouping<OpenXmlElement, IPlaceholder> placeholders in
                    tableGroup.GetRowPlaceholders(copyRow)
                        .GroupBy(x => x.Info.Get<OpenXmlElement>(OpenXmlHelper.BaseElementField)))
                {
                    OpenXmlElement baseElement = placeholders.Key;
                    if (baseElement == null)
                    {
                        continue;
                    }

                    Cell newCell = GetRelativeElement<Cell>(copyRow.Element, baseElement, newRow.Element);

                    var replacements = new List<IPlaceholderReplacement>();
                    foreach (IPlaceholder placeholder in placeholders)
                    {
                        PlaceholderValue newValue = await ((ITablePlaceholderType) placeholder.Type)
                                .ReplaceAsync(context, placeholder, row, context.CancellationToken)
                            ?? PlaceholderValue.Empty;

                        replacements.Add(new PlaceholderReplacement(placeholder, newValue));
                    }

                    await this.ReplaceElementsInCellAsync(newCell, true, context.CancellationToken, replacements.ToArray());
                }
            }

            await this.ReplaceRowPlaceholdersForHyperlinksAsync(context, tableGroup, tableGroupInstance, row);
            await this.ReplaceRowPlaceholdersForAnchorsAsync(context, tableGroup, tableGroupInstance, row);
        }

        private async Task ReplaceRowPlaceholdersForHyperlinksAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance tableGroupInstance,
            IPlaceholderRow row)
        {
            for (int i = 0; i < tableGroup.Hyperlinks.Count; i++)
            {
                var newHyperlink = tableGroupInstance.Hyperlinks[i];
                var hyperlink = tableGroup.Hyperlinks[i];

                Hyperlink hypElem = hyperlink.Element;
                Hyperlink newHypElem = newHyperlink.Element;
                IList<IPlaceholder> placeholders = tableGroup.GetHyperlinkPlaceholders(hyperlink);

                if (placeholders.Count > 0)
                {
                    WorksheetPart worksheetPart = tableGroup.Worksheet.Element.WorksheetPart;
                    IPlaceholderReplacement[] replacements = await ReplaceRowPlaceholdersAsync(context, row, placeholders);
                    newHypElem.Id = await this.ReplaceElementsInRelationshipsWithCopyAsync(worksheetPart, hypElem.Id, context.CancellationToken, replacements);
                }
            }
        }

        private async Task ReplaceRowPlaceholdersForAnchorsAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance tableGroupInstance,
            IPlaceholderRow row)
        {
            for (int i = 0; i < tableGroup.Anchors.Count; i++)
            {
                var newAnchor = tableGroupInstance.Anchors[i];
                var anchor = tableGroup.Anchors[i];

                IList<IPlaceholder> placeholders = tableGroup.GetAnchorPlaceholders(anchor);

                if (placeholders.Count > 0)
                {
                    IPlaceholderReplacement[] replacements = await ReplaceRowPlaceholdersAsync(context, row, placeholders);
                    await this.ReplaceElementsInCompositeElementAsync(newAnchor.GetPlaceholderBaseElement(), context.CancellationToken, replacements);
                }
            }
        }

        private static async Task<IPlaceholderReplacement[]> ReplaceRowPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            IPlaceholderRow row,
            IList<IPlaceholder> placeholders)
        {
            var result = new List<IPlaceholderReplacement>(placeholders.Count);
            foreach (IPlaceholder placeholder in placeholders)
            {
                result.Add(
                    new PlaceholderReplacement(
                        placeholder,
                        await ((ITablePlaceholderType) placeholder.Type)
                            .ReplaceAsync(context, placeholder, row, context.CancellationToken)
                        ?? PlaceholderValue.Empty));
            }

            return result.ToArray();
        }

        private uint GetCellNumberFormat(Cell cell)
        {
            if (cell.StyleIndex == null)
            {
                return 0;
            }

            return this.stylesheet.CellFormats.Elements<CellFormat>().ElementAt(Convert.ToInt32(cell.StyleIndex.Value)).NumberFormatId ?? 0;
        }

        /// <summary>
        /// Создать объект Picture, который оборачивает. Бинарные данные при этом добавляются в страницу Excel.
        /// </summary>
        /// <param name="worksheetPart">Объект, который содержит страницу Excel (вкладка документа).</param>
        /// <param name="imageBytes">Содержимое изображения в байтах.</param>
        /// <param name="imagePartType">Тип изображения.</param>
        /// <param name="existingProperties">
        /// Существующие настройки формата фигуры, которые надо перенести, или <c>null</c>, если таких настроек нет.
        /// Обычно это настройки объекта "Надпись".
        /// </param>
        /// <param name="width">Ширина изображения в пикселях для 96dpi, заданная в плейсхолдере.</param>
        /// <param name="height">Высота изображения в пикселях для 96dpi, заданная в плейсолдере.</param>
        /// <param name="reformat">
        /// Признак, заданный в плейсхолдере, который указывает на то, что исходные настройки форматирования
        /// <paramref name="existingProperties"/> придётся по большей части выбросить.
        /// </param>
        /// <param name="alternativeText">Замещающий текст.</param>
        private static Xdr.Picture CreatePictureFromImage(
            WorksheetPart worksheetPart,
            byte[] imageBytes,
            ImagePartType imagePartType,
            Xdr.ShapeProperties existingProperties,
            double width,
            double height,
            bool reformat,
            string alternativeText)
        {
            // добавляем изображение в документ и получаем его relationshipId
            DrawingsPart drawingsPart = worksheetPart.DrawingsPart ?? worksheetPart.AddNewPart<DrawingsPart>();
            if (!worksheetPart.Worksheet.ChildElements.OfType<Drawing>().Any())
            {
                worksheetPart.Worksheet.AppendChild(new Drawing { Id = worksheetPart.GetIdOfPart(drawingsPart) });
            }

            Xdr.WorksheetDrawing worksheetDrawing = drawingsPart.WorksheetDrawing;
            if (worksheetDrawing == null)
            {
                worksheetDrawing = new Xdr.WorksheetDrawing();
                drawingsPart.WorksheetDrawing = worksheetDrawing;
            }

            var imagePart = drawingsPart.AddImagePart(imagePartType);
            imagePart.FeedData(new MemoryStream(imageBytes));

            // берём из надписи или генерим свойства фигуры для картинки
            Xdr.ShapeProperties actualProperties;
            if (!reformat && existingProperties != null)
            {
                actualProperties = (Xdr.ShapeProperties) existingProperties.CloneNode(deep: true);
            }
            else
            {
                actualProperties = new Xdr.ShapeProperties(
                    new A.Transform2D(
                        new A.Offset { X = 0L, Y = 0L },
                        new A.Extents { Cx = OpenXmlHelper.DefaultExtentsCx, Cy = OpenXmlHelper.DefaultExtentsCy }
                    ),
                    new A.PresetGeometry { Preset = A.ShapeTypeValues.Rectangle });
            }

            // определяем размеры картинки
            A.Extents extents = actualProperties.Transform2D?.Extents;
            if (extents != null)
            {
                if (width > 0.0)
                {
                    extents.Cx = OpenXmlHelper.PixelsToEmu(width);
                }

                if (height > 0.0)
                {
                    extents.Cy = OpenXmlHelper.PixelsToEmu(height);
                }

                if (reformat && (width <= 0.0 || height <= 0.0))
                {
                    // нужно вычислить актуальные размеры изображения
                    using Image image = Image.FromStream(new MemoryStream(imageBytes));
                    if (width <= 0.0)
                    {
                        extents.Cx = OpenXmlHelper.GetImageCx(image);
                    }

                    if (height <= 0.0)
                    {
                        extents.Cy = OpenXmlHelper.GetImageCy(image);
                    }
                }
            }

            // определяем уникальный идентификатор картинки
            Xdr.NonVisualDrawingProperties[] nvps = worksheetDrawing
                .Descendants<Xdr.NonVisualDrawingProperties>()
                .ToArray();

            uint nvpId = nvps.Length > 0 ? nvps.Max(p => p.Id.Value) + 1u : 1u;

            // генерим объект Picture, в который картинка обёрнута
            return new Xdr.Picture(
                new Xdr.NonVisualPictureProperties(
                    new Xdr.NonVisualDrawingProperties { Id = nvpId, Name = "Image " + nvpId, Title = LocalizationManager.Format(alternativeText) },
                    new Xdr.NonVisualPictureDrawingProperties(new A.PictureLocks { NoChangeAspect = true })
                ),
                new Xdr.BlipFill(
                    new A.Blip
                    {
                        Embed = drawingsPart.GetIdOfPart(imagePart),
                        CompressionState = A.BlipCompressionValues.Print
                    },
                    new A.Stretch(new A.FillRectangle())
                ),
                actualProperties);
        }

        private ValueTask<(bool, int)> ReplaceGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance parentTableGroup = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null,
            int groupLevel = 0)
        {
            switch (tableGroup.Type)
            {
                case TableGroupType.Jump:
                    return this.ReplaceJumpGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentTableGroup,
                        placeholderTable,
                        rows);

                case TableGroupType.Row:
                    return this.ReplaceRowGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentTableGroup,
                        placeholderTable,
                        rows);

                case TableGroupType.Group:
                    return this.ReplaceGroupGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentTableGroup,
                        placeholderTable,
                        rows,
                        groupLevel);

                case TableGroupType.Table:
                    return this.ReplaceTableGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentTableGroup);

                default:
                    throw new ArgumentException("TableGroup.Type has invalid value");
            }
        }

        private async ValueTask<(bool, int)> ReplaceTableGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance parentTableGroup = null)
        {
            bool hasTableRows = false;
            // Определяет, насколько была смещена таблица
            int moveByLocal = 0;
            TableGroupInstance tableGroupInstance = null;
            if (parentTableGroup != null)
            {
                // Не клонируем строки
                tableGroupInstance = new TableGroupInstance(tableGroup, parentTableGroup, Array.Empty<RowCellGroup>());
                tableGroupInstance.CreateClone();
            }

            if (tableGroup.InnerGroups.Count > 0)
            {
                foreach (var innerGroup in tableGroup.InnerGroups)
                {
                    var (hasGroupRows, moveByGroup) = await this.ReplaceGroupPlaceholdersAsync(
                        context,
                        innerGroup,
                        parentTableGroup);

                    moveByLocal += moveByGroup;
                    hasTableRows |= hasGroupRows;

                    if (parentTableGroup != null
                        && moveByGroup != 0)
                    {
                        tableGroupInstance.Move(moveByGroup, innerGroup.Top);

                        int from = innerGroup.Top - parentTableGroup.Rows[0].Bottom;
                        int to = tableGroup.Bottom - parentTableGroup.Rows[0].Bottom + tableGroup.Height;
                        for (int i = to - 1; i >= from; i--)
                        {
                            parentTableGroup.Rows[i]?.Move(moveByGroup);
                        }
                    }
                }
            }

            if (hasTableRows)
            {
                tableGroupInstance?.InsertClone();
            }
            // Если в таблице нет строк, удаляем таблицу целиком.
            else
            {
                // Если нет строк с результатом, то удаляем строки таблицы
                if (parentTableGroup is null)
                {
                    // Удаляем исходную таблицу
                    tableGroup.Clear();
                }
                else
                {
                    // Удаляем строки таблицы из копии
                    int from = tableGroup.Bottom - parentTableGroup.Rows[0].Bottom;
                    int to = from + tableGroup.Height;
                    int expandBy = 0;
                    for (int i = from; i < to; i++)
                    {
                        if (parentTableGroup.Rows[i] != null)
                        {
                            parentTableGroup.Rows[i] = null;
                            expandBy--;
                        }
                    }

                    // Дополнительно смещаем на число удаленных таблицей строк
                    if (expandBy != 0)
                    {
                        tableGroupInstance.ExpandFormulaSources(expandBy);
                    }
                }

                moveByLocal = -tableGroup.Height;
            }

            return (hasTableRows, moveByLocal);
        }

        private async ValueTask<(bool, int)> ReplaceGroupGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance parentTableGroup = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null,
            int groupLevel = 0)
        {
            bool needTableExtensions = false;
            List<Row> allElements = this.WithExtensions
                ? new List<Row>()
                : null;
            List<Row> currentRowElements = null;

            if (placeholderTable is null)
            {
                placeholderTable = await this.FillTableAsync(
                    context,
                    tableGroup);

                if (this.WithExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    this.extensionContext.Worksheet = tableGroup.Worksheet.Element;
                    await this.BeforeTableReplaceAsync(context);
                    needTableExtensions = true;
                }

                rows = placeholderTable?.Rows;
            }

            bool hasRows = rows != null
                && rows.Any();

            // Насколько текущая таблица сдвинула остальные
            int moveTotal = 0;

            TableGroupInstance tableGroupInstance = new TableGroupInstance(tableGroup, parentTableGroup);
            if (hasRows)
            {
                // Насколько нужно сдвигать текущую строку
                int moveLoсal = 0;
                await placeholderTable.FillHorizontalGroupsAsync(
                    context,
                    rows,
                    groupLevel);

                if (this.WithExtensions)
                {
                    currentRowElements = new List<Row>();
                }

                foreach (var rowGroup in rows.GroupBy(x => x.HorizontalGroup).ToArray())
                {
                    var row = rowGroup.First();
                    context.SetPerformingRow(placeholderTable.Name, row);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = true;
                        this.extensionContext.GroupLevel = groupLevel;
                        await this.BeforeRowReplaceAsync(context);
                    }

                    tableGroupInstance.CreateClone();
                    tableGroupInstance.Move(moveLoсal);

                    await this.ReplacePlaceholdersInRowAsync(
                        context,
                        tableGroup,
                        tableGroupInstance,
                        row);

                    for (int i = 0; i < tableGroup.InnerGroups.Count; i++)
                    {
                        var innerGroup = tableGroup.InnerGroups[i];

                        var (_, moveGroup) = await this.ReplaceGroupPlaceholdersAsync(
                            context,
                            innerGroup,
                            tableGroupInstance,
                            placeholderTable,
                            rowGroup,
                            groupLevel + 1);

                        moveTotal += moveGroup;
                        tableGroupInstance.Move(moveGroup, innerGroup.Top);

                        if (this.WithExtensions)
                        {
                            currentRowElements.AddRange(this.extensionContext.TableElements);
                        }
                    }

                    tableGroupInstance.InsertClone();
                    if (this.WithExtensions)
                    {
                        currentRowElements.AddRange(tableGroupInstance.Rows.Where(x => x != null).Select(x => x.Element));
                    }

                    moveTotal += tableGroup.Height;
                    moveLoсal = moveTotal;

                    if (this.WithExtensions)
                    {
                        this.extensionContext.RowElements = currentRowElements;
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = true;
                        this.extensionContext.GroupLevel = groupLevel;
                        await this.AfterRowReplaceAsync(context);
                        allElements.AddRange(currentRowElements);
                        currentRowElements.Clear();
                    }
                }
            }

            tableGroupInstance.ExpandFormulaSources();

            // Вычитаем число строк текущей таблицы, т.к. всегда есть первая строка.
            moveTotal -= tableGroup.Height;

            if (this.WithExtensions)
            {
                this.extensionContext.TableElements = allElements;
                if (needTableExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    await this.AfterTableReplaceAsync(context);
                }
            }

            // Очищаем старые элементы
            if (parentTableGroup is null)
            {
                tableGroup.Clear();
            }

            return (hasRows, moveTotal);
        }

        private async ValueTask<(bool, int)> ReplaceRowGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance parentTableGroup = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null)
        {
            bool needTableExtensions = false;
            List<Row> allElements = this.WithExtensions
                ? new List<Row>()
                : null;
            List<Row> currentRowElements = null;

            if (placeholderTable is null)
            {
                placeholderTable = await this.FillTableAsync(
                    context,
                    tableGroup);

                if (this.WithExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    this.extensionContext.Worksheet = tableGroup.Worksheet.Element;
                    await this.BeforeTableReplaceAsync(context);
                    needTableExtensions = true;
                }

                rows = placeholderTable?.Rows;
            }

            bool hasRows = rows != null
                && rows.Any();

            // Насколько текущая таблица сдвинула остальные
            int moveTotal = 0;

            if (this.WithExtensions)
            {
                currentRowElements = new List<Row>();
            }

            TableGroupInstance tableGroupInstance = new TableGroupInstance(tableGroup, parentTableGroup);
            if (hasRows)
            {
                int number = placeholderTable.Info.TryGet<int>(PlaceholderHelper.NumberKey);

                // Насколько нужно сдвигать текущую строку
                int moveLoсal = 0;

                // для каждой строки заменяем плейсхолдеры в строке-шаблоне rowText
                foreach (IPlaceholderRow row in rows)
                {
                    row.Number = ++number;
                    context.SetPerformingRow(placeholderTable.Name, row);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = false;
                        await this.BeforeRowReplaceAsync(context);
                    }

                    tableGroupInstance.CreateClone();
                    tableGroupInstance.Move(moveLoсal);

                    await this.ReplacePlaceholdersInRowAsync(
                        context,
                        tableGroup,
                        tableGroupInstance,
                        row);

                    for (int i = 0; i < tableGroup.InnerGroups.Count; i++)
                    {
                        var innerGroup = tableGroup.InnerGroups[i];
                        if (innerGroup.Type == TableGroupType.Table)
                        {
                            var (_, moveGroup) = await this.ReplaceTableGroupPlaceholdersAsync(
                                context,
                                innerGroup,
                                tableGroupInstance);

                            moveTotal += moveGroup;
                            tableGroupInstance.Move(moveGroup, innerGroup.Top);

                            if (this.WithExtensions)
                            {
                                currentRowElements.AddRange(this.extensionContext.TableElements);
                            }
                        }
                        else
                        {
                            // TODO Placeholders - строки не содержат группы и другие строки
                        }
                    }

                    tableGroupInstance.InsertClone();
                    if (this.WithExtensions)
                    {
                        currentRowElements.AddRange(tableGroupInstance.Rows.Where(x => x != null).Select(x => x.Element));
                    }

                    moveTotal += tableGroup.Height;
                    moveLoсal = moveTotal;

                    if (this.WithExtensions)
                    {
                        this.extensionContext.RowElements = currentRowElements;
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = false;
                        await this.AfterRowReplaceAsync(context);
                        allElements.AddRange(currentRowElements);
                        currentRowElements.Clear();
                    }
                }

                placeholderTable.Info[PlaceholderHelper.NumberKey] = number;
            }

            tableGroupInstance.ExpandFormulaSources();
            // Вычитаем число строк текущей таблицы, т.к. всегда есть первая строка.
            moveTotal -= tableGroup.Height;

            if (this.WithExtensions)
            {
                this.extensionContext.TableElements = allElements;
                if (needTableExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    await this.AfterTableReplaceAsync(context);
                }
            }

            // Очищаем старые элементы
            if (parentTableGroup is null)
            {
                tableGroup.Clear();
            }

            return (hasRows, moveTotal);
        }

        private async ValueTask<(bool, int)> ReplaceJumpGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            TableGroup tableGroup,
            TableGroupInstance parentTableGroup = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null)
        {
            bool needTableExtensions = false;
            List<Row> allElements = this.WithExtensions
                ? new List<Row>()
                : null;
            List<Row> currentRowElements = null;

            if (placeholderTable is null)
            {
                placeholderTable = await this.FillTableAsync(
                    context,
                    tableGroup);

                if (this.WithExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    this.extensionContext.Worksheet = tableGroup.Worksheet.Element;
                    await this.BeforeTableReplaceAsync(context);
                    needTableExtensions = true;
                }

                rows = placeholderTable?.Rows;
            }

            bool hasRows = rows != null
                && rows.Any();

            // Список строк, которы будут клонироваться для генерации строки текущей таблицы. Если null, то будут использоваться строки текущей таблицы
            IList<RowCellGroup> tableRows = parentTableGroup is null
                ? (IList<RowCellGroup>) tableGroup.Rows
                : parentTableGroup.Rows.Where(x => x.Bottom >= tableGroup.Bottom && x.Top <= tableGroup.Top).ToArray();

            // Если нет строк для замены, то заменяем текст ячеек с табличными плейсхолдерами на String.Empty
            if (hasRows)
            {
                int number = placeholderTable.Info.TryGet<int>(PlaceholderHelper.NumberKey);

                // Индекс смещения внутри таблицы
                int moveInTable = 0;

                Row[] currentCopyRowElements = new Row[tableRows.Count];
                for (int i = 0; i < tableRows.Count; i++)
                {
                    currentCopyRowElements[i] = (Row) tableRows[i].Element.CloneNode(true);
                }

                TableGroupInstance tableGroupInstance = new TableGroupInstance(tableGroup);

                if (this.WithExtensions)
                {
                    currentRowElements = new List<Row>();
                }

                bool notFirstRow = false;
                foreach (IPlaceholderRow row in rows)
                {
                    row.Number = ++number;

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = false;
                        await this.BeforeRowReplaceAsync(context);
                    }

                    // Обработку ведем по элементам строк
                    for (int i = 0; i < tableRows.Count; i++)
                    {
                        // Строка, в которую производится вставка
                        RowCellGroup copyRow = tableRows[i];
                        RowCellGroup originalRow = tableGroup.Rows[i];
                        Row currentCopyRowElement = currentCopyRowElements[i];

                        // Если не первая строка, то ищем строку дальше в таблице
                        if (notFirstRow)
                        {
                            copyRow = parentTableGroup is null
                                ? tableGroup.Worksheet.GetRow(copyRow.Top + moveInTable)
                                : parentTableGroup.Rows.FirstOrDefault(x => x.Top == copyRow.Top + moveInTable);

                            // Если в таблице отсутствует элемент строки с данным индексом, то делаем копию оригинального элемента
                            if (copyRow == null)
                            {
                                // Создаем строку как копию оригинальной строки с плейсхолдерами.
                                // Сдвигаем строку на "смещение внутри jump таблицы"+"ссмещение jump таблицы"
                                copyRow = new RowCellGroup(
                                    (Row) currentCopyRowElement.CloneNode(false),
                                    tableGroup.Worksheet);
                                copyRow.ParseElement();
                                copyRow.Move(moveInTable);
                                copyRow.Update(); // Делаем Update сразу для новой строки в таблице Jump, чтобы другие Jump таблицы видели эту строку
                                copyRow.Move(tableRows[i].MoveBy);
                                copyRow.Insert();
                            }
                        }

                        if (this.WithExtensions)
                        {
                            this.extensionContext.CurrentRowElement = copyRow.Element;
                            currentRowElements.Add(copyRow.Element);
                        }

                        // Берем все плейсхолдеры, относящиеся к текущей строке
                        foreach (var placeholders in
                            tableGroup.GetRowPlaceholders(originalRow)
                                .GroupBy(x => x.Info.Get<OpenXmlElement>(OpenXmlHelper.BaseElementField)))
                        {
                            OpenXmlElement baseElement = placeholders.Key;
                            if (baseElement == null)
                            {
                                continue;
                            }

                            var replacements = new List<IPlaceholderReplacement>();
                            foreach (IPlaceholder placeholder in placeholders.ToArray())
                            {
                                PlaceholderValue newValue = await ((ITablePlaceholderType) placeholder.Type)
                                        .ReplaceAsync(context, placeholder, row, context.CancellationToken)
                                    ?? PlaceholderValue.Empty;

                                replacements.Add(new PlaceholderReplacement(placeholder, newValue));
                            }

                            if (notFirstRow)
                            {
                                Cell originalCell = GetRelativeElement<Cell>(originalRow.Element, baseElement, currentCopyRowElement);
                                Cell newCell = (Cell) originalCell.CloneNode(true);
                                await this.ReplaceElementsInCellAsync(newCell, true, context.CancellationToken, replacements.ToArray());
                                copyRow.SetCell(newCell);
                            }
                            else
                            {
                                // Если это отдельная таблица, меняем оригинальную ячейку, иначе ищем ее копию в копии строки
                                Cell newCell = parentTableGroup is null
                                    ? (Cell) baseElement
                                    : GetRelativeElement<Cell>(originalRow.Element, baseElement, copyRow.Element);
                                await this.ReplaceElementsInCellAsync(newCell, true, context.CancellationToken, replacements.ToArray());
                            }
                        }
                    }

                    // Вручную копируем каждую гиперссылку и якорь, чтобы произвести в них замену плейсхолдеров.
                    for (int i = 0; i < tableGroupInstance.Hyperlinks.Length; i++)
                    {
                        var hyperlink = (HyperlinkCellGroup) tableGroup.Hyperlinks[i].Clone();
                        hyperlink.Insert();
                        hyperlink.Move(moveInTable);
                        tableGroupInstance.Hyperlinks[i] = hyperlink;
                    }

                    for (int i = 0; i < tableGroupInstance.Anchors.Length; i++)
                    {
                        var anchor = (AnchorCellGroup) tableGroup.Anchors[i].Clone();
                        anchor.Insert();
                        anchor.Move(moveInTable);
                        tableGroupInstance.Anchors[i] = anchor;
                    }

                    await this.ReplaceRowPlaceholdersForHyperlinksAsync(context, tableGroup, tableGroupInstance, row);
                    await this.ReplaceRowPlaceholdersForAnchorsAsync(context, tableGroup, tableGroupInstance, row);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = false;
                        this.extensionContext.RowElements = currentRowElements;
                        await this.AfterRowReplaceAsync(context);
                        allElements.AddRange(currentRowElements);
                        currentRowElements.Clear();
                    }

                    moveInTable += tableRows.Count;
                    notFirstRow = true;
                }

                placeholderTable.Info[PlaceholderHelper.NumberKey] = number;
            }
            else
            {
                // Если нет строк для замены, то заменяем текст ячеек с табличными плейсхолдерами на String.Empty
                // Обработку ведем по элементам строк
                for (int i = 0; i < tableRows.Count; i++)
                {
                    var row = tableRows[i];
                    var originalRow = tableGroup.Rows[i];
                    foreach (IGrouping<OpenXmlElement, IPlaceholder> placeholders in
                        tableGroup.GetRowPlaceholders(originalRow)
                            .GroupBy(x => x.Info.Get<OpenXmlElement>(OpenXmlHelper.BaseElementField)))
                    {
                        OpenXmlElement baseElement = placeholders.Key;
                        if (baseElement == null)
                        {
                            continue;
                        }

                        Cell cell = (Cell) baseElement;
                        (SharedStringItem sharedString, int sharedStringID) = this.CopySharedString(int.Parse(cell.InnerText));
                        ClearTextInSharedString(sharedString);
                        cell.CellValue.Text = sharedStringID.ToString();
                    }
                }
            }

            if (this.WithExtensions)
            {
                this.extensionContext.TableElements = allElements;
                if (needTableExtensions)
                {
                    await this.AfterTableReplaceAsync(context);
                }
            }

            // Очищаем старые элементы
            if (parentTableGroup is null)
            {
                tableGroup.Clear();
            }

            // Данный вид групп никогда не свдигает остальные группы
            return (hasRows, 0);
        }

        private async ValueTask<IEditablePlaceholderTable> FillTableAsync(
            IPlaceholderReplacementContext context,
            TableGroup group,
            IEditablePlaceholderTable table = null,
            int groupLevel = 0)
        {
            var tablePlaceholders = group.GetAllPlaceholders();
            tablePlaceholders.Sort((x, y) =>
            {
                int result = string.Compare(
                    TextPosition(x.Info.Get<IList>(OpenXmlHelper.PositionField)),
                    TextPosition(y.Info.Get<IList>(OpenXmlHelper.PositionField)),
                    StringComparison.Ordinal);

                return result == 0
                    ? x.Info.TryGet<int>(OpenXmlHelper.OrderField).CompareTo(y.Info.TryGet<int>(OpenXmlHelper.OrderField))
                    : result;
            });

            for (int i = 0; i < tablePlaceholders.Count; i++)
            {
                var placeholder = tablePlaceholders[i];
                if (placeholder.Type is ITablePlaceholderType tableType)
                {
                    table = await tableType.FillTableAsync(
                        context,
                        placeholder,
                        table);

                    if (group.Type == TableGroupType.Group)
                    {
                        table.AddHorizontalGroupPlaceholder(
                            placeholder,
                            groupLevel);
                    }
                }
            }

            foreach (var innerGroup in group.InnerGroups)
            {
                if (innerGroup.Type != TableGroupType.Table)
                {
                    await this.FillTableAsync(
                        context,
                        innerGroup,
                        table,
                        innerGroup.Type == TableGroupType.Group ? groupLevel + 1 : 0);
                }
            }

            return table;
        }

        #endregion

        #region SharedStrings Private Methods

        /// <summary>
        /// Метод для получения значения SharedString по его ID
        /// </summary>
        /// <param name="id">ID искомой SharedString</param>
        /// <returns></returns>
        private string GetSharedString(int id)
        {
            return this.sharedStringTable.ElementAt(id).InnerText;
        }

        /// <summary>
        /// Метод для замены плейсхолдеров в SharedString
        /// </summary>
        /// <param name="sharedString">Объект SharedString</param>
        /// <param name="replacements">Плейсхолдеры для замены</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private Task ReplaceElementsInSharedStringAsync(
            SharedStringItem sharedString,
            CancellationToken cancellationToken = default,
            params IPlaceholderReplacement[] replacements)
        {
            return this.ReplaceElementsInCompositeElementAsync(sharedString, cancellationToken, replacements);
        }

        /// <summary>
        /// Метод для очистки текста в в SharedString
        /// </summary>
        /// <param name="sharedString">SharedString, в которой очищается текст</param>
        private static void ClearTextInSharedString(SharedStringItem sharedString)
        {
            foreach (Text text in sharedString.Descendants<Text>())
            {
                text.Text = string.Empty;
            }
        }

        /// <summary>
        /// Метод производит копирование SharedString с указанным ID и возвращает новый объект SharedString с его ID
        /// </summary>
        /// <param name="id">ID копируемой SharedString</param>
        /// <returns></returns>
        private Tuple<SharedStringItem, int> CopySharedString(int id)
        {
            SharedStringItem oldItem = this.sharedStringTable.ElementAt(id);
            SharedStringItem newItem = oldItem.CloneNode(true) as SharedStringItem;
            this.sharedStringTable.Add(newItem);

            return new Tuple<SharedStringItem, int>(newItem, this.sharedStringTable.Count - 1);
        }

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="PlaceholderDocument" and @item="ExtensionContext"]'/>
        protected override IPlaceholderReplaceExtensionContext ExtensionContext => this.extensionContext;

        /// <doc path='info[@type="PlaceholderDocument" and @item="CreateExtensionContext"]'/>
        protected override IPlaceholderReplaceExtensionContext CreateExtensionContext(IPlaceholderReplacementContext context) =>
            this.extensionContext = new ExcelPlaceholderReplaceExtensionContext(context, context.CancellationToken);

        /// <summary>
        /// Метод для получения информации о плейсхолдерах документа из базы данных
        /// </summary>
        /// <param name="dbScope">Объект dbScope текущего подключения к базе данных</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает список плейсхолдеров документа из базы данных</returns>
        protected override async Task<List<IPlaceholderText>> GetPlaceholdersFromDatabaseAsync(
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            List<IPlaceholderText> placeholders = new List<IPlaceholderText>();
            // Пытаемся получить плейсхолдеры из базы
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;

                string json = await db
                    .SetCommand(
                        builderFactory
                            .Select().Top(1).C("PlaceholdersInfo")
                            .From("FileTemplates").NoLock()
                            .Where().C("ID").Equals().P("ID")
                            .And().C("PlaceholdersInfo").IsNotNull()
                            .Limit(1)
                            .Build(),
                        db.Parameter("ID", this.TemplateID))
                    .LogCommand()
                    .ExecuteStringAsync(cancellationToken);

                var dictionary = string.IsNullOrEmpty(json)
                    ? new Dictionary<string, object>()
                    : StorageHelper.DeserializeFromTypedJson(json);

                if (!dictionary.TryGetValue(nameof(ExcelPlaceholderDocument), out object listObj)
                    || !(listObj is IList list && !(listObj is byte[])))
                {
                    return placeholders;
                }

                foreach (Dictionary<string, object> values in list)
                {
                    if (values.Count == 0)
                    {
                        continue;
                    }

                    string text = values.TryGet<string>(OpenXmlHelper.TextField);
                    object position = values.TryGet<object>(OpenXmlHelper.PositionField);
                    string value = PlaceholderHelper.TryGetValue(text);

                    if (value != null && position != null)
                    {
                        var newPlaceholder = new PlaceholderText(text, value);
                        newPlaceholder.Info[OpenXmlHelper.PositionField] = position;
                        newPlaceholder.Info[OpenXmlHelper.OrderField] = values.TryGet<int>(OpenXmlHelper.OrderField);

                        placeholders.Add(newPlaceholder);
                    }
                }
            }

            return placeholders;
        }

        /// <summary>
        /// Метод для инициализации документа
        /// </summary>
        /// <returns>Возвращает инициализированный документ</returns>
        protected override OpenXmlPackage InitDocument()
        {
            this.excelDocument = SpreadsheetDocument.Open(this.Stream, true);

            var sharedStringTablePart = this.excelDocument.WorkbookPart.SharedStringTablePart ??
                this.excelDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();

            this.sharedStringTable = new SharedStringTableContainer(sharedStringTablePart.SharedStringTable ??
                (this.excelDocument.WorkbookPart.SharedStringTablePart.SharedStringTable = new SharedStringTable()));

            this.stylesheet = this.excelDocument.WorkbookPart.WorkbookStylesPart.Stylesheet ??
                (this.excelDocument.WorkbookPart.WorkbookStylesPart.Stylesheet = new Stylesheet());

            if (this.WithExtensions)
            {
                this.extensionContext.Document = this.excelDocument;
            }

            return this.excelDocument;
        }

        /// <summary>
        /// Метод для получения плейсхолдеров из объекта документа
        /// </summary>
        /// <returns>Возвращает список плейсхолдеров, найденных в документе</returns>
        protected override List<IPlaceholderText> GetPlaceholdersFromDocument() =>
            this.GetPlaceholdersFromPart(this.excelDocument.WorkbookPart);

        /// <summary>
        /// Метод для подготовки документа к сохранению
        /// </summary>
        protected override void PrepareDocumentForSave() => this.sharedStringTable.Save();

        /// <summary>
        /// Метод для сохранения инициализированного документа
        /// </summary>
        protected override void SaveDocument()
        {
            FixMyOffice(this.excelDocument);
            this.excelDocument.Dispose();
            this.excelDocument = null;
        }

        /// <summary>
        /// Метод для сохранения информации о плейсхолдерах документа в базу данных
        /// </summary>
        /// <param name="dbScope">Объект dbScope текущего подключения к базе данных</param>
        /// <param name="placeholders">Список плейсхолдеров для сохранения</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override async Task SavePlaceholdersInDatabaseAsync(
            IDbScope dbScope,
            List<IPlaceholderText> placeholders,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                List<object> list = new List<object>(placeholders.Count);
                Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    [nameof(ExcelPlaceholderDocument)] = list,
                };

                foreach (IPlaceholderText placeholder in placeholders)
                {
                    var pDictionary = new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        { OpenXmlHelper.PositionField, placeholder.Info[OpenXmlHelper.PositionField] },
                        { OpenXmlHelper.TextField, placeholder.Text },
                        { OpenXmlHelper.OrderField, placeholder.Info.TryGet<int>(OpenXmlHelper.OrderField) }
                    };

                    list.Add(pDictionary);
                }

                string json = StorageHelper.SerializeToTypedJson(dictionary);

                var executor = dbScope.Executor;
                var builderFactory = dbScope.BuilderFactory;

                await executor
                    .ExecuteNonQueryAsync(
                        builderFactory
                            .Update("FileTemplates").C("PlaceholdersInfo").Assign().P("Data")
                            .Where().C("ID").Equals().P("ID")
                            .Build(),
                        cancellationToken,
                        executor.Parameter("ID", this.TemplateID),
                        executor.Parameter("Data", json, DataType.BinaryJson));
            }
        }

        /// <summary>
        /// Производит замену плейсхолдеров типа Field в документе
        /// </summary>
        /// <param name="context">Контекст операции замены плейсхолдеров</param>
        /// <returns>Возвращает true, если в документе производились изменения</returns>
        protected override async Task<bool> ReplaceFieldPlaceholdersAsync(IPlaceholderReplacementContext context)
        {
            bool hasChanges = false;

            foreach (IGrouping<string, IPlaceholderReplacement> replacements in
                context.Replacements
                    .GroupBy(x => TextPosition(x.Placeholder.Info.Get<IList>(OpenXmlHelper.PositionField)))
                    .OrderByDescending(x => x.Key))
            {
                OpenXmlElement element = this.excelDocument.WorkbookPart.Workbook;
                OpenXmlPart part = this.excelDocument.WorkbookPart;
                IList position = replacements.First().Placeholder.Info.Get<IList>(OpenXmlHelper.PositionField);

                for (int i = 0; i < position.Count; i++)
                {
                    if (position[i].ToString() == Hyperlink)
                    {
                        if (position.Count > i + 1)
                        {
                            await this.ReplaceElementsInRelationshipsAsync(part, position[i + 1] as string, context.CancellationToken, replacements.ToArray());
                        }

                        break;
                    }

                    int index = (int) position[i];
                    if (index < 0)
                    {
                        part = part.Parts.ToArray()[~index].OpenXmlPart;
                        element = part.RootElement;
                    }
                    else
                    {
                        element = element.ChildElements[index];

                        if (element is Worksheet worksheet
                            && this.WithExtensions)
                        {
                            this.extensionContext.Worksheet = worksheet;
                        }
                    }
                }

                if (element != null)
                {
                    Type elementType = element.GetType();
                    if (elementType == typeof(Cell))
                    {
                        // ReSharper disable once PossibleInvalidCastException
                        await this.ReplaceElementsInCellAsync((Cell) element, false, context.CancellationToken, replacements.ToArray());
                    }
                    else if (elementType == typeof(A.Paragraph))
                    {
                        await this.ReplaceElementsInCompositeElementAsync(element, context.CancellationToken, replacements.ToArray());
                    }
                    else if (elementType == typeof(HeaderFooter))
                    {
                        await this.ReplaceElementsInCompositeElementAsync(element, context.CancellationToken, replacements.ToArray());
                    }
                }

                hasChanges = true;
            }

            return hasChanges;
        }

        /// <summary>
        /// Производит замену плейсхолдеров типа Table в документе
        /// </summary>
        /// <param name="context">Контекст операции замены плейсхолдеров</param>
        /// <returns>Возвращает true, если в документе производились изменения</returns>
        protected override async Task<bool> ReplaceTablePlaceholdersAsync(IPlaceholderReplacementContext context)
        {
            IList<IPlaceholder> tablePlaceholders = context.TablePlaceholders;
            if (tablePlaceholders.Count == 0)
            {
                return false;
            }

            context.ValidationResult.Add(this.InitializeWorksheets());

            if (!context.ValidationResult.IsSuccessful())
            {
                return false;
            }

            bool hasFormulas = this.PrepareWorksheetFormulas();

            bool hasChanges = false;
            this.AttachPlaceholders(tablePlaceholders);

            foreach (var worksheetElement in this.worksheetHash)
            {
                worksheetElement.Tables.Reverse();
                foreach (TableGroup tableGroup in worksheetElement.Tables)
                {
                    var (hasTableChanges, moveTotal) = await this.ReplaceGroupPlaceholdersAsync(
                        context,
                        tableGroup);

                    hasChanges |= hasTableChanges;

                    // Добавление всем строкам, гиперссылкам, группам и смерженным ячейкам ниже данной таблицы нового индекса строки
                    tableGroup.Worksheet.Move(moveTotal, tableGroup.Top);
                    tableGroup.Remove();

                    foreach (HyperlinkCellGroup hyperlink in tableGroup.Hyperlinks)
                    {
                        ClearOldRelationships(tableGroup.Worksheet.Element.WorksheetPart, tableGroup.GetHyperlinkPlaceholders(hyperlink));
                    }
                }
            }

            this.UpdateWorksheets();

            if (hasFormulas)
            {
                // Запускаем полный перерасчет формул при загрузке документа
                this.excelDocument.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                this.excelDocument.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

                // Очищаем старую цепочку вычисления формул, она расчитается заного при загрузке документа
                this.excelDocument.WorkbookPart.DeletePart(this.excelDocument.WorkbookPart.CalculationChainPart);
            }

            return hasChanges;
        }

        /// <summary>
        /// Метод для поиска плейсхолдеров внутри элемента документа
        /// </summary>
        /// <param name="baseElement">Элемент, в котором производится поиск плейсхолдеров</param>
        /// <param name="position">Позиция элемента в документе</param>
        /// <returns>Возвращает список плейсхолдеров, найденных в текущем элементе</returns>
        protected override IEnumerable<IPlaceholderText> GetPlaceholdersFromElementOverride(OpenXmlElement baseElement, IList position)
        {
            List<IPlaceholderText> result = new List<IPlaceholderText>();

            switch (baseElement)
            {
                case Cell cell:
                {
                    if (cell.CellFormula is null)
                    {
                        string allTextString = this.GetCellText(cell);
                        OpenXmlHelper.AddPlaceholdersFromText(result, allTextString, position);
                    }
                    else
                    {
                        string formulaText = cell.CellFormula.Text;
                        OpenXmlHelper.AddPlaceholdersFromText(result, formulaText, position);
                    }

                    break;
                }

                case A.Paragraph _:
                {
                    StringBuilder allText = StringBuilderHelper.Acquire();

                    List<A.Text> textElements = GetTextElements(baseElement);
                    if (textElements != null)
                    {
                        foreach (A.Text element in textElements)
                        {
                            allText.Append(element.Text);
                        }
                    }

                    string allTextString = allText.ToStringAndRelease();
                    OpenXmlHelper.AddPlaceholdersFromText(result, allTextString, position);
                    break;
                }

                case HeaderFooter _:
                {
                    OpenXmlHelper.AddPlaceholdersFromText(result, baseElement.InnerText, position);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Метод, определяющий правила замены текста в элементе документа
        /// </summary>
        /// <param name="firstBaseElement">Первый базовый элемент, в котором производится замена текста. Обычно это Run или сам Text</param>
        /// <param name="firstTextElement">Первый объект текста для замены</param>
        /// <param name="lastBaseElement">Последний базовый элемент, в котором производится замена текста. Обычно это Run или сам Text</param>
        /// <param name="lastTextElement">Последний элемент текста для замены</param>
        /// <param name="newText">Текст, на который выполняется замена</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override async Task ReplaceTextAsync(
            OpenXmlElement firstBaseElement,
            OpenXmlLeafTextElement firstTextElement,
            OpenXmlElement lastBaseElement,
            OpenXmlLeafTextElement lastTextElement,
            string newText,
            CancellationToken cancellationToken = default)
        {
            // Text (Spreadsheet.Text): текст в ячейке Excel
            // A.Text (Drawing.Text): текст в объекте "Надпись"
            newText = this.RemoveInvalidChars(newText);

            switch (firstTextElement)
            {
                case Text text:
                {
                    text.Text = newText.Replace(Environment.NewLine, "\n", StringComparison.Ordinal);
                    text.Space = SpaceProcessingModeValues.Preserve;

                    if (lastTextElement != null)
                    {
                        var lastText = (Text) lastTextElement;
                        if (string.IsNullOrEmpty(lastText.Text))
                        {
                            lastBaseElement.Remove();
                        }
                        else
                        {
                            lastText.Space = SpaceProcessingModeValues.Preserve;
                        }
                    }

                    break;
                }

                case A.Text atext:
                {
                    atext.Text = newText.Replace(Environment.NewLine, "\n", StringComparison.Ordinal);

                    if (lastTextElement != null)
                    {
                        var lastText = (A.Text) lastTextElement;
                        if (string.IsNullOrEmpty(lastText.Text))
                        {
                            lastBaseElement.Remove();
                        }
                    }

                    break;
                }

                case CellFormula cellFormula:
                {
                    cellFormula.Text = newText.Replace(Environment.NewLine, "\n", StringComparison.Ordinal);
                    break;
                }

                case OddHeader:
                case OddFooter:
                {
                    firstTextElement.Text = newText.Replace(Environment.NewLine, "\n", StringComparison.Ordinal);
                    break;
                }
            }

            if (this.WithExtensions)
            {
                this.extensionContext.PlaceholderElement = firstBaseElement;
                this.extensionContext.PlaceholderTextElement = firstTextElement;
                await this.AfterPlaceholderReplaceAsync(this.extensionContext.ReplacementContext);
            }

            // Если в текстовом элементе текст пустой, то удаляем базовый элемент, в котором хранится этот текст (обычно это Run или сам Text)
            if (string.IsNullOrEmpty(firstTextElement.Text))
            {
                firstBaseElement.Remove();
            }
        }

        /// <summary>
        /// Метод, определяющий правила замены изображения в элементе документа.
        /// Возвращает признак того, что замена выполнена успешно.
        /// </summary>
        /// <param name="baseElement">Базовый элемент, в котором производится замена текста</param>
        /// <param name="replacement">Значение с изображением, которое требуется заменить</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак того, что замена выполнена успешно.</returns>
        protected override Task<bool> ReplaceImageAsync(
            OpenXmlElement baseElement,
            IPlaceholderReplacement replacement,
            CancellationToken cancellationToken = default)
        {
            OpenXmlElement shapeBase;

            var existingShape = OpenXmlHelper.FindParent<Xdr.Shape>(baseElement);
            if (existingShape == null
                || (shapeBase = existingShape.Parent) == null)
            {
                return TaskBoxes.False;
            }

            PlaceholderValue value = replacement.NewValue;
            byte[] data = value.Data;

            if (data == null || data.Length == 0)
            {
                // пустое изображение при замене удаляет надпись
                var anchor = OpenXmlHelper.FindParent<Xdr.TwoCellAnchor>(existingShape);
                if (anchor != null)
                {
                    WorksheetElement element = this.GetWorksheetByPlaceholder(replacement.Placeholder);

                    AnchorCellGroup elementAnchor;
                    if (element != null && (elementAnchor = element.Anchors.FirstOrDefault(x => ReferenceEquals(x.Element, anchor))) != null)
                    {
                        // удаляем "надпись" из строки таблицы плейсхолдеров: {t:...}, {tv:...}, ...
                        elementAnchor.Remove();
                    }
                    else
                    {
                        // удаляем "надпись" снаружи таблицы плейсхолдеров: {f:...}, {fv:...}, ....
                        anchor.Remove();
                    }

                    return TaskBoxes.True;
                }

                return TaskBoxes.False;
            }

            WorksheetPart worksheetPart = this.GetWorksheetPartByPlaceholder(replacement.Placeholder);
            if (worksheetPart == null)
            {
                return TaskBoxes.False;
            }

            IPlaceholderImageParameters imageParameters = value.FormatResult.GetImageParameters();
            ImagePartType imagePartType = OpenXmlHelper.GetImagePartType(imageParameters);

            var existingProperties = existingShape.Descendants<Xdr.ShapeProperties>().FirstOrDefault();

            var picture = CreatePictureFromImage(
                worksheetPart,
                data,
                imagePartType,
                existingProperties,
                imageParameters.Width,
                imageParameters.Height,
                imageParameters.Reformat,
                imageParameters.AlternativeText);

            // мы должны расположить элемент Picture в ту же позицию, где был Shape,
            // здесь особенно важно положение относительно ClientData
            int index = shapeBase.IndexOf(existingShape);

            existingShape.Remove();
            shapeBase.InsertAt(picture, index);

            return TaskBoxes.True;
        }

        #endregion

        #region Fix Methods

        private static void FixMyOffice(SpreadsheetDocument document)
        {
            // МойОфис Таблица добавляет пространство имён с данным алиасом, что приводит к полной поломке сохранения документа через DocumentFormat.OpenXml.
            // Актуально на момент версий:
            // МойОфис 2022.01 Сборка 21
            // DocumentFormat.OpenXml 2.18.0
            const string badMyOfficeNamespaceAlias = "x";

            static void FixParts(IEnumerable<IdPartPair> parts)
            {
                foreach (var idPartPair in parts)
                {
                    var part = idPartPair.OpenXmlPart;
                    var root = part.RootElement;
                    if (!string.IsNullOrEmpty(root?.LookupNamespace(badMyOfficeNamespaceAlias)))
                    {
                        root.RemoveNamespaceDeclaration(badMyOfficeNamespaceAlias);
                    }

                    FixParts(part.Parts);
                }
            }

            FixParts(document.Parts);

        }

        #endregion
    }
}
