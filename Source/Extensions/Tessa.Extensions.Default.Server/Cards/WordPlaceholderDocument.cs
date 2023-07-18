using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Placeholders.Extensions;
using Tessa.Platform.Storage;
using A = DocumentFormat.OpenXml.Drawing;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;
using DataType = LinqToDB.DataType;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Hyperlink = DocumentFormat.OpenXml.Wordprocessing.Hyperlink;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using ParagraphProperties = DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using Wp14 = DocumentFormat.OpenXml.Office2010.Word.Drawing;
using WPS = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Объект, определяющий способы хранения и изменения текста с заменяемыми плейсхолдерами
    /// для документа Word.
    /// </summary>
    public sealed class WordPlaceholderDocument :
        OpenXmlPlaceholderDocument
    {
        #region Consts

        /// <summary>
        /// Определяет константу для хранения в Info табличного плейсхолдера элемента строки таблицы, в которой расположен плейсхолдер
        /// </summary>
        private const string TableRowField = "TableRow";

        #endregion

        #region Fields

        /// <summary>
        /// Парсер групп в документе Word.
        /// </summary>
        private readonly IWordDocumentTableGroupParser parser;

        /// <summary>
        /// Контекст расширений
        /// </summary>
        private WordPlaceholderReplaceExtensionContext extensionContext;

        /// <summary>
        /// Документ Word
        /// </summary>
        private WordprocessingDocument wordDocument;

        /// <summary>
        /// Идентификатор очередного объекта docPr, который должен быть уникален в пределах документа
        /// </summary>
        private uint docPropertiesNextId = 1u;

        /// <summary>
        /// Определяет, был ли проведен поиск <see cref="tableGroups"/> в документе.
        /// </summary>
        private bool tableGroupsFounded;

        /// <summary>
        /// Информация о таблицах, созданных закладками
        /// </summary>
        private readonly List<WordDocumentTableGroup> tableGroups = new List<WordDocumentTableGroup>();

        #endregion

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
        public WordPlaceholderDocument(MemoryStream stream, Guid templateID, IWordDocumentTableGroupParser parser)
            : base(stream, templateID)
        {
            this.parser = parser;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает все дочерние элементы с типом Text среди всех дочерних элементов объекта <c>baseElement</c>
        /// </summary>
        /// <param name="baseElement">Базовый элемент, начиная с которого производим поиск</param>
        /// <returns>Список элементов типа Text, или null, если <c>baseElement</c> не содержит дочерних элементов типа Text</returns>
        private static List<Text> GetTextElements(OpenXmlElement baseElement)
        {
            List<Text> result = new List<Text>();
            foreach (OpenXmlElement e in baseElement.ChildElements)
            {
                Type eType = e.GetType();
                if (eType == typeof(Text))
                {
                    result.Add((Text) e);
                }
                else if (eType != typeof(Hyperlink) && eType != typeof(Paragraph) && e.HasChildren)
                {
                    result.AddRange(GetTextElements(e));
                }
            }

            return result;
        }

        /// <summary>
        /// Метод для получаения ближайшего родительского элемента типа строка таблицы или строка списка, или null, если такого элемента нет
        /// </summary>
        /// <param name="element">Элемент, относительно которого ведется поиск строки таблицы или списка</param>
        /// <returns>Возвращает ближайший родительский элемент типа строка таблицы или строку списка, или null, если такого элемента нет</returns>
        private static OpenXmlElement GetTableRowElement(OpenXmlElement element)
        {
            if (element == null)
            {
                return null;
            }

            ParagraphProperties prop;
            OpenXmlElement tableRowElement = element;

            while (tableRowElement != null
                && (tableRowElement.GetType() != typeof(TableRow))
                && (tableRowElement.GetType() != typeof(Paragraph) || (prop = tableRowElement.GetFirstChild<ParagraphProperties>()) == null || prop.GetFirstChild<NumberingProperties>() == null))
            {
                tableRowElement = tableRowElement.Parent;
            }

            return tableRowElement;
        }

        /// <summary>
        /// Метод возвращает все плейсхолдеры из Relationships, относящиеся к данной строке таблицы.
        /// </summary>
        /// <param name="tableGroup">Таблица, в элементах которой производится поиск плейсхолдеров из Relationships</param>
        /// <param name="relationshipsPlaceholders">Список табличных плейсхолдеров из Relationships</param>
        /// <returns></returns>
        private static void CheckForRelationshipsPlaceholders(WordDocumentTableGroup tableGroup, IList<IPlaceholder> relationshipsPlaceholders)
        {
            if (relationshipsPlaceholders.Count == 0)
            {
                return;
            }

            foreach (var innerGroup in tableGroup.InnerTableGroups)
            {
                CheckForRelationshipsPlaceholders(innerGroup, relationshipsPlaceholders);
            }

            if (relationshipsPlaceholders.Count == 0)
            {
                return;
            }

            foreach (var tableRow in tableGroup.BaseElements)
            {
                foreach (Hyperlink hyperlinkElement in tableRow.Descendants<Hyperlink>())
                {
                    for (int i = relationshipsPlaceholders.Count - 1; i >= 0; i--)
                    {
                        var placeholder = relationshipsPlaceholders[i];
                        if ((string) placeholder.Info.Get<IList>(OpenXmlHelper.PositionField)[1] == hyperlinkElement.Id)
                        {
                            placeholder.Info[OpenXmlHelper.BaseElementField] = hyperlinkElement;
                            relationshipsPlaceholders.RemoveAt(i);
                            tableGroup.TablePlaceholders.Add(placeholder);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Производит замену Replacement'ов в заданном элементе FieldCode
        /// </summary>
        /// <param name="fieldCode">FieldCode, в котором производится замена плейсхолдеров</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="replacements">Массив плейсхолдеров для замены</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task ReplaceElementsInFieldCodeAsync(
            FieldCode fieldCode,
            CancellationToken cancellationToken = default,
            params IPlaceholderReplacement[] replacements)
        {
            string codeText = fieldCode.Text;

            StringBuilder decodedText = new StringBuilder(Uri.UnescapeDataString(codeText));
            decodedText.Replace(HyperlinkRemoveHead, string.Empty, 0, Math.Min(HyperlinkRemoveHeadLength, decodedText.Length));

            foreach (IPlaceholderReplacement replacement in replacements)
            {
                var newValue = replacement.NewValue;
                string newText;
                if (this.WithExtensions)
                {
                    this.extensionContext.Placeholder = replacement.Placeholder;
                    this.extensionContext.PlaceholderValue = replacement.NewValue;
                    await this.BeforePlaceholderReplaceAsync(this.extensionContext.ReplacementContext);
                    newValue = this.extensionContext.PlaceholderValue;

                    this.extensionContext.PlaceholderText = newValue.Text;
                    await this.AfterPlaceholderReplaceAsync(this.extensionContext.ReplacementContext);
                    newText = this.extensionContext.PlaceholderText;
                }
                else
                {
                    newText = newValue.Text;
                }

                decodedText.Replace(replacement.Placeholder.Text, newText);
            }

            fieldCode.Text = decodedText.ToString();
        }

        /// <summary>
        /// Получает дочерний элемент newParent, соответствующий элементу baseChild относительно baseParent. Элемент newParent должен быть полной копией элемента baseParent
        /// </summary>
        /// <param name="baseChildType">Тип искомого элемента</param>
        /// <param name="baseParent">Родительский элемент, относительно которого ведется поиск</param>
        /// <param name="baseChild">Дочерний элемент, соответствие которого мы ищем в newParent</param>
        /// <param name="newParent">Копия родительского элемента, дочерний элемент которого мы ищем</param>
        /// <returns></returns>
        private static OpenXmlElement GetRelativeElement(
            OpenXmlElement baseParent,
            OpenXmlElement baseChild,
            Type baseChildType,
            OpenXmlElement newParent)
        {
            if (baseChildType == typeof(Paragraph))
            {
                return GetRelativeElement<Paragraph>(baseParent, baseChild, newParent);
            }

            if (baseChildType == typeof(Hyperlink))
            {
                return GetRelativeElement<Hyperlink>(baseParent, baseChild, newParent);
            }

            if (baseChildType == typeof(FieldCode))
            {
                return GetRelativeElement<FieldCode>(baseParent, baseChild, newParent);
            }

            if (baseChildType == typeof(Run))
            {
                return GetRelativeElement<Run>(baseParent, baseChild, newParent);
            }

            if (baseChildType == typeof(TableRow))
            {
                return GetRelativeElement<TableRow>(baseParent, baseChild, newParent);
            }

            if (baseChildType == typeof(TableCell))
            {
                return GetRelativeElement<TableCell>(baseParent, baseChild, newParent);
            }

            return null;
        }

        private A.Graphic CreateGraphicFromImage(
            byte[] imageBytes,
            ImagePartType imagePartType,
            OpenXmlPart partElement,
            WPS.ShapeProperties existingProperties,
            DW.Extent existingExtent,
            double width,
            double height,
            bool reformat)
        {
            // добавляем изображение в документ и получаем его relationshipId
            ImagePart imagePart = partElement.AddImagePart(imagePartType);
            imagePart.FeedData(new MemoryStream(imageBytes));

            string relationshipId = partElement.GetIdOfPart(imagePart);

            // берём из надписи или генерим свойства фигуры для картинки
            PIC.ShapeProperties actualProperties;
            if (!reformat && existingProperties != null)
            {
                OpenXmlElement[] childElements = existingProperties.ChildElements
                    .Select(x => x.CloneNode(deep: true))
                    .ToArray();

                var transform2D = (A.Transform2D) existingProperties.Transform2D.CloneNode(deep: true);

                actualProperties = new PIC.ShapeProperties(childElements)
                {
                    BlackWhiteMode = existingProperties.BlackWhiteMode,
                    Transform2D = transform2D,
                };
            }
            else
            {
                actualProperties = new PIC.ShapeProperties(
                    new A.Transform2D(
                        new A.Offset { X = 0L, Y = 0L },
                        new A.Extents { Cx = OpenXmlHelper.DefaultExtentsCx, Cy = OpenXmlHelper.DefaultExtentsCy }),
                    new A.PresetGeometry(new A.AdjustValueList())
                        { Preset = A.ShapeTypeValues.Rectangle });
            }

            // определяем и проставляем размеры картинки
            A.Extents extents = actualProperties.Transform2D?.Extents;
            if (extents != null || existingExtent != null)
            {
                Int64Value cx = OpenXmlHelper.DefaultExtentsCx;
                Int64Value cy = OpenXmlHelper.DefaultExtentsCy;

                if (width > 0.0)
                {
                    cx = OpenXmlHelper.PixelsToEmu(width);
                }

                if (height > 0.0)
                {
                    cy = OpenXmlHelper.PixelsToEmu(height);
                }

                if (reformat && (width <= 0.0 || height <= 0.0))
                {
                    // нужно вычислить актуальные размеры изображения
                    using Image image = Image.FromStream(new MemoryStream(imageBytes));
                    if (width <= 0.0)
                    {
                        cx = OpenXmlHelper.GetImageCx(image);
                    }

                    if (height <= 0.0)
                    {
                        cy = OpenXmlHelper.GetImageCy(image);
                    }
                }

                if (extents != null)
                {
                    extents.Cx = cx;
                    extents.Cy = cy;
                }

                if (existingExtent != null)
                {
                    existingExtent.Cx = cx;
                    existingExtent.Cy = cy;
                }
            }

            // генерим объект Graphic, в который картинка обёрнута
            return new A.Graphic(
                new A.GraphicData(
                    new PIC.Picture(
                        new PIC.NonVisualPictureProperties(
                            new PIC.NonVisualDrawingProperties { Id = 0U, Name = "Image" },
                            new PIC.NonVisualPictureDrawingProperties()),
                        new PIC.BlipFill(
                            new A.Blip(
                                new A.BlipExtensionList(
                                    new A.BlipExtension { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" }))
                            {
                                Embed = relationshipId,
                                CompressionState = A.BlipCompressionValues.Print
                            },
                            new A.Stretch(new A.FillRectangle())),
                        actualProperties))
                {
                    Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                });
        }

        /// <summary>
        /// Метод для распределения табличных плейсхолдеров по таблицам.
        /// Может создавать типовые таблиц без групп, если плейсхолдер записан в строке таблицы или в перечислении
        /// </summary>
        /// <param name="tables">Таблицы</param>
        /// <param name="tablePlaceholders">Табличные плейсхолдеры</param>
        private void PrepareTablePlaceholders(List<WordDocumentTableGroup> tables, IList<IPlaceholder> tablePlaceholders, out List<IPlaceholder> relationshipsPlaceholders)
        {
            relationshipsPlaceholders = new List<IPlaceholder>();
            // Определяем таблицу/список, к которому относится плейсхолдер
            for (int i = tablePlaceholders.Count - 1; i >= 0; i--)
            {
                var placeholder = tablePlaceholders[i];
                if (IsRelationship(placeholder))
                {
                    relationshipsPlaceholders.Add(placeholder);
                    tablePlaceholders.RemoveAt(i);
                }
            }

            foreach (var placeholder in tablePlaceholders)
            {
                OpenXmlElement baseElement = GetElementByPlaceholder(this.wordDocument.MainDocumentPart, placeholder);
                placeholder.Info.Add(OpenXmlHelper.BaseElementField, baseElement);

                var placeholderPosition = placeholder.Info.Get<IList>(OpenXmlHelper.PositionField);
                bool tableFounded = false;

                foreach (var table in tables)
                {
                    tableFounded = this.CheckPlaceholderInTable(table, placeholder, placeholderPosition);
                    if (tableFounded)
                    {
                        break;
                    }
                }

                if (!tableFounded)
                {
                    var rowElement = GetTableRowElement(baseElement);
                    if (rowElement != null)
                    {
                        var startPosition = new List<object>();
                        OpenXmlElement root = this.wordDocument.MainDocumentPart.RootElement;

                        for (int i = 0; i < placeholderPosition.Count; i++)
                        {
                            var index = (int) placeholderPosition[i];
                            if (index < 0)
                            {
                                startPosition.Add(Int32Boxes.Box(index));
                            }
                            else
                            {
                                startPosition.Add(Int32Boxes.Box(index));
                                root = root.ChildElements[index];
                                if (root == rowElement)
                                {
                                    break;
                                }
                            }
                        }

                        var endPosition = startPosition.ToList();

                        // Если не нашли таблицу, пытаемся создать новую
                        var newTable = new WordDocumentTableGroup()
                        {
                            GroupType = WordDocumentTableGroupType.Row,
                            TableElement = rowElement.Parent,
                            StartPosition = startPosition,
                            EndPosition = endPosition,
                            TableStartPosition = startPosition,
                            TableEndPosition = endPosition,
                        };

                        newTable.BaseElements.Add(rowElement);
                        newTable.TablePlaceholders.Add(placeholder);

                        parser.FillTableRelationships(
                            new[] { newTable },
                            tables);
                    }
                }
            }

            if (relationshipsPlaceholders.Count > 0)
            {
                var checkRelationshipsPlaceholders = new List<IPlaceholder>(relationshipsPlaceholders);
                // Заполняем RelationShipsPlaceholders
                foreach (var table in tables)
                {
                    CheckForRelationshipsPlaceholders(table, checkRelationshipsPlaceholders);

                    if (relationshipsPlaceholders.Count == 0)
                    {
                        break;
                    }
                }
            }
        }

        private bool CheckPlaceholderInTable(
            WordDocumentTableGroup table,
            IPlaceholder placeholder,
            IList placeholderPosition)
        {
            if (table.Contains(placeholderPosition)
                && (!table.InParagraph || table.ContainsPlaceholder(placeholder)))
            {
                foreach (var innerTable in table.InnerTableGroups)
                {
                    if (this.CheckPlaceholderInTable(
                        innerTable,
                        placeholder,
                        placeholderPosition))
                    {
                        return true;
                    }
                }

                // Если плейсхолдер в таблице, то считаем его не найденным
                if (table.GroupType == WordDocumentTableGroupType.Table)
                {
                    return false;
                }

                table.TablePlaceholders.Add(placeholder);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод для создания и добавления новых элементов, созданных из копий элементов таблицы <paramref name="currentTableGroup"/>
        /// </summary>
        /// <param name="currentTableGroup">Таблица</param>
        /// <param name="insertBeforeElement">Объект, перед которым происходит вставка новых элементов</param>
        /// <param name="parentElement">
        /// Родительский объект, в который производится вставка новых элементов. Испрользуется, если <paramref name="insertBeforeElement"/> равен <c>null</c>
        /// </param>
        /// <returns>Возвращает список новых элементов</returns>
        private static List<OpenXmlElement> CreateNewElements(
            WordDocumentTableGroup currentTableGroup,
            OpenXmlElement insertBeforeElement,
            OpenXmlElement parentElement)
        {
            var newTableRows = currentTableGroup.BaseElements.Select(x => x.CloneNode(true)).ToList();

            foreach (OpenXmlElement newTableRow in newTableRows)
            {
                // Добавляем новые строки в таблицу
                if (insertBeforeElement != null)
                {
                    insertBeforeElement.InsertBeforeSelf(newTableRow);
                }
                else
                {
                    // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                    parentElement.Append(newTableRow);
                }
            }

            return newTableRows;
        }

        /// <summary>
        /// Метод для замены плейсхолдеров строки для заданной таблицы
        /// </summary>
        /// <param name="context">Контекст замены плейсхолдеров</param>
        /// <param name="currentTableGroup">Таблица</param>
        /// <param name="newTableRows">Новые элементы строки таблицы, где производится замена</param>
        /// <param name="row">Строка с данными расчета плейсхолдеров</param>
        /// <param name="isGroup">Определяет, производится ли замена группы или строки</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task ReplaceInNewElementsAsync(
            IPlaceholderReplacementContext context,
            WordDocumentTableGroup currentTableGroup,
            IReadOnlyCollection<OpenXmlElement> newTableRows,
            IPlaceholderRow row)
        {
            var rowPlaceholders = currentTableGroup.TablePlaceholders;
            (OpenXmlElement baseElement, OpenXmlElement rowElement)[] newBaseElementArray = rowPlaceholders
                .Select(placeholder =>
                {
                    OpenXmlElement baseElement = placeholder.Info.Get<OpenXmlElement>(OpenXmlHelper.BaseElementField);
                    if (baseElement == null)
                    {
                        return default;
                    }

                    Type baseElementType = baseElement.GetType();

                    if (currentTableGroup.InParagraph)
                    {
                        return (currentTableGroup.TableElement, currentTableGroup.TableElement);
                    }

                    var i = 0;
                    foreach (var newTableRow in newTableRows)
                    {
                        var relativeElement = GetRelativeElement(currentTableGroup.BaseElements[i++], baseElement, baseElementType, newTableRow);
                        if (relativeElement != null)
                        {
                            return (relativeElement, newTableRow);
                        }
                    }

                    return default;
                })
                .ToArray();

            int index = 0;
            foreach (IPlaceholder placeholder in rowPlaceholders)
            {
                (OpenXmlElement newBaseElement, OpenXmlElement currentTableRow) = newBaseElementArray[index++];
                if (newBaseElement == null)
                {
                    continue;
                }

                if (this.WithExtensions)
                {
                    this.extensionContext.CurrentRowElement = currentTableRow;
                }

                // заменяем текст, который может быть равен null в результате замены
                PlaceholderValue newValue = await ((ITablePlaceholderType) placeholder.Type)
                        .ReplaceAsync(context, placeholder, row, context.CancellationToken)
                    ?? PlaceholderValue.Empty;

                if (IsRelationship(placeholder))
                {
                    Hyperlink hyperlink = (Hyperlink) newBaseElement;
                    hyperlink.Id = await this.ReplaceElementsInRelationshipsWithCopyAsync(
                        this.wordDocument.MainDocumentPart,
                        hyperlink.Id,
                        context.CancellationToken,
                        new PlaceholderReplacement(placeholder, newValue));
                }
                else
                {
                    // Заменяем плейсхолдер в новом базовом элементе
                    Type baseElementType = newBaseElement.GetType();
                    if (baseElementType == typeof(Paragraph)
                        || baseElementType == typeof(Hyperlink))
                    {
                        await this.ReplaceElementsInCompositeElementAsync(newBaseElement, context.CancellationToken,
                            new PlaceholderReplacement(placeholder, newValue));
                    }
                    else if (baseElementType == typeof(FieldCode))
                    {
                        await this.ReplaceElementsInFieldCodeAsync((FieldCode) newBaseElement, context.CancellationToken,
                            new PlaceholderReplacement(placeholder, newValue));
                    }
                }
            }
        }

        private ValueTask<bool> ReplaceGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            WordDocumentTableGroup tableGroup,
            IList<OpenXmlElement> parentElements = null,
            IList<OpenXmlElement> originalParentElements = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null,
            int groupLevel = 0)
        {
            switch (tableGroup.GroupType)
            {
                case WordDocumentTableGroupType.Row:
                    return this.ReplaceRowGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentElements,
                        originalParentElements,
                        placeholderTable,
                        rows);

                case WordDocumentTableGroupType.Group:
                    return this.ReplaceGroupGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentElements,
                        originalParentElements,
                        placeholderTable,
                        rows,
                        groupLevel);

                case WordDocumentTableGroupType.Table:
                    return this.ReplaceTableGroupPlaceholdersAsync(
                        context,
                        tableGroup,
                        parentElements,
                        originalParentElements);

                default:
                    throw new ArgumentException("TableGroup.GroupType has invalid value");
            }
        }

        private async ValueTask<bool> ReplaceTableGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            WordDocumentTableGroup tableGroup,
            IList<OpenXmlElement> parentElements = null,
            IList<OpenXmlElement> originalParentElements = null)
        {
            bool hasTableRows = false;
            foreach (var innerGroup in tableGroup.InnerTableGroups)
            {
                hasTableRows |= await this.ReplaceGroupPlaceholdersAsync(
                    context,
                    innerGroup,
                    parentElements,
                    originalParentElements);
            }

            // Если нет строк, удаляем таблицу целиком.
            if (!hasTableRows)
            {
                this.DeleteTableGroupElements(
                    tableGroup,
                    parentElements,
                    originalParentElements);
            }

            return hasTableRows;
        }

        private async ValueTask<bool> ReplaceGroupGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            WordDocumentTableGroup tableGroup,
            IList<OpenXmlElement> parentElements = null,
            IList<OpenXmlElement> originalParentElements = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null,
            int groupLevel = 0)
        {
            bool needTableExtensions = false;

            if (placeholderTable is null)
            {
                placeholderTable = await this.FillTableAsync(
                    context,
                    tableGroup);

                if (this.WithExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    this.extensionContext.TableElement = tableGroup.TableElement;
                    await this.BeforeTableReplaceAsync(context);
                    needTableExtensions = true;
                }

                rows = placeholderTable?.Rows;
            }

            bool hasRows = rows != null
                && rows.Any();

            if (hasRows)
            {
                OpenXmlElement originalLastElement = tableGroup.BaseElements[^1],
                    lastElement = null;

                Type originalLastElementType = originalLastElement.GetType();

                if (parentElements != null
                    && originalParentElements != null)
                {
                    for (int i = 0; i < parentElements.Count; i++)
                    {
                        var parentElement = parentElements[i];
                        var originalParentElement = originalParentElements[i];

                        lastElement = GetRelativeElement(originalParentElement, originalLastElement, originalLastElementType, parentElement);
                        if (lastElement != null)
                        {
                            break;
                        }
                    }
                }

                lastElement ??= originalLastElement;

                OpenXmlElement insertBeforeElement = lastElement.NextSibling(),
                    lastElementParent = lastElement.Parent;

                await placeholderTable.FillHorizontalGroupsAsync(
                    context,
                    rows,
                    groupLevel);

                foreach (var rowGroup in rows.GroupBy(x => x.HorizontalGroup).ToArray())
                {
                    var row = rowGroup.First();
                    context.SetPerformingRow(placeholderTable.Name, row);

                    var rowElements = CreateNewElements(
                        tableGroup,
                        insertBeforeElement,
                        lastElementParent);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = true;
                        this.extensionContext.GroupLevel = groupLevel;
                        await this.BeforeRowReplaceAsync(context);
                    }

                    for (int i = 0; i < tableGroup.InnerTableGroups.Count; i++)
                    {
                        var innerGroup = tableGroup.InnerTableGroups[i];

                        await this.ReplaceGroupPlaceholdersAsync(
                            context,
                            innerGroup,
                            rowElements,
                            tableGroup.BaseElements,
                            placeholderTable,
                            rowGroup,
                            groupLevel + 1);
                    }

                    await this.ReplaceInNewElementsAsync(
                        context,
                        tableGroup,
                        rowElements,
                        row);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.RowElements.Clear();
                        this.extensionContext.RowElements.AddRange(rowElements);
                        this.extensionContext.GroupLevel = groupLevel;
                        this.extensionContext.IsGroup = true;
                        await this.AfterRowReplaceAsync(context);
                    }
                }
            }

            // Удаляем оригинальные элементы группы
            this.DeleteTableGroupElements(tableGroup, parentElements, originalParentElements);

            if (needTableExtensions)
            {
                this.extensionContext.Table = placeholderTable;
                this.extensionContext.TableElement = tableGroup.TableElement;
                await this.AfterTableReplaceAsync(context);
            }

            return hasRows;
        }

        private async ValueTask<bool> ReplaceRowGroupPlaceholdersAsync(
            IPlaceholderReplacementContext context,
            WordDocumentTableGroup tableGroup,
            IList<OpenXmlElement> parentElements = null,
            IList<OpenXmlElement> originalParentElements = null,
            IEditablePlaceholderTable placeholderTable = null,
            IEnumerable<IPlaceholderRow> rows = null)
        {
            bool needTableExtensions = false;

            if (placeholderTable is null)
            {
                placeholderTable = await this.FillTableAsync(
                    context,
                    tableGroup);

                if (this.WithExtensions)
                {
                    this.extensionContext.Table = placeholderTable;
                    this.extensionContext.TableElement = tableGroup.TableElement;
                    await this.BeforeTableReplaceAsync(context);
                    needTableExtensions = true;
                }

                rows = placeholderTable?.Rows;
            }

            bool hasRows = rows != null
                && rows.Any();

            if (hasRows)
            {
                int number = placeholderTable.Info.TryGet<int>(PlaceholderHelper.NumberKey);
                OpenXmlElement originalLastElement = tableGroup.BaseElements[^1],
                    lastElement = null;

                Type originalLastElementType = originalLastElement.GetType();

                if (parentElements != null
                    && originalParentElements != null)
                {
                    for (int i = 0; i < parentElements.Count; i++)
                    {
                        var parentElement = parentElements[i];
                        var originalParentElement = originalParentElements[i];

                        lastElement = GetRelativeElement(originalParentElement, originalLastElement, originalLastElementType, parentElement);
                        if (lastElement != null)
                        {
                            break;
                        }
                    }
                }

                lastElement ??= originalLastElement;

                OpenXmlElement insertBeforeElement = lastElement.NextSibling(),
                    lastElementParent = lastElement.Parent;

                // для каждой строки заменяем плейсхолдеры в строке-шаблоне rowText
                foreach (IPlaceholderRow row in rows)
                {
                    row.Number = ++number;
                    context.SetPerformingRow(placeholderTable.Name, row);

                    var rowElements = CreateNewElements(
                        tableGroup,
                        insertBeforeElement,
                        lastElementParent);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.IsGroup = false;
                        await this.BeforeRowReplaceAsync(context);
                    }

                    for (int i = 0; i < tableGroup.InnerTableGroups.Count; i++)
                    {
                        var innerGroup = tableGroup.InnerTableGroups[i];
                        if (innerGroup.GroupType == WordDocumentTableGroupType.Table)
                        {
                            await this.ReplaceTableGroupPlaceholdersAsync(
                                context,
                                innerGroup,
                                rowElements,
                                tableGroup.BaseElements);
                        }
                        else
                        {
                            // TODO Placeholders - строки не содержат группы и другие строки
                        }
                    }

                    await this.ReplaceInNewElementsAsync(
                        context,
                        tableGroup,
                        rowElements,
                        row);

                    if (this.WithExtensions)
                    {
                        this.extensionContext.Row = row;
                        this.extensionContext.RowElements.Clear();
                        this.extensionContext.RowElements.AddRange(rowElements);
                        this.extensionContext.IsGroup = false;
                        await this.AfterRowReplaceAsync(context);
                    }
                }

                placeholderTable.Info[PlaceholderHelper.NumberKey] = number;
            }

            // Удаляем оригинальные элементы строки
            this.DeleteTableGroupElements(tableGroup, parentElements, originalParentElements);

            if (needTableExtensions)
            {
                this.extensionContext.Table = placeholderTable;
                this.extensionContext.TableElement = tableGroup.TableElement;
                await this.AfterTableReplaceAsync(context);
            }

            return hasRows;
        }

        private void DeleteTableGroupElements(
            WordDocumentTableGroup tableGroup,
            IList<OpenXmlElement> parentElements,
            IList<OpenXmlElement> originalParentElements)
        {
            // Если нет родителей, то удаляем оригинальные элементы
            if (parentElements is null)
            {
                foreach (var element in tableGroup.BaseElements)
                {
                    element.SafeRemove();
                }
            }
            else // Если таблица вложенная (есть родители), удаляем копии оригинальных элементов
            {
                OpenXmlElement[] elementsToDelete = new OpenXmlElement[tableGroup.BaseElements.Count];
                for (int i = 0; i < tableGroup.BaseElements.Count; i++)
                {
                    var element = tableGroup.BaseElements[i];
                    var elementType = element.GetType();
                    for (int j = 0; j < parentElements.Count; j++)
                    {
                        var parentElement = parentElements[j];
                        var originalParentElement = originalParentElements[j];

                        var elementToDelete = GetRelativeElement(originalParentElement, element, elementType, parentElement);
                        if (elementToDelete != null)
                        {
                            elementsToDelete[i] = elementToDelete;
                            break;
                        }
                    }
                }

                for (int i = 0; i < elementsToDelete.Length; i++)
                {
                    var elementToDelete = elementsToDelete[i];
                    if (elementToDelete != null)
                    {
                        elementToDelete.SafeRemove();
                    }
                }
            }
        }

        private async ValueTask<IEditablePlaceholderTable> FillTableAsync(
            IPlaceholderReplacementContext context,
            WordDocumentTableGroup group,
            IEditablePlaceholderTable table = null,
            int groupLevel = 0)
        {
            group.TablePlaceholders.Sort((x, y) =>
            {
                int result = string.Compare(
                    TextPosition(x.Info.Get<IList>(OpenXmlHelper.PositionField)),
                    TextPosition(y.Info.Get<IList>(OpenXmlHelper.PositionField)),
                    StringComparison.Ordinal);

                if (result == 0)
                {
                    return x.Info.TryGet<int>(OpenXmlHelper.OrderField).CompareTo(y.Info.TryGet<int>(OpenXmlHelper.OrderField));
                }

                return result;
            });

            for (int i = 0; i < group.TablePlaceholders.Count; i++)
            {
                var placeholder = group.TablePlaceholders[i];
                if (placeholder.Type is ITablePlaceholderType tableType)
                {
                    table = await tableType.FillTableAsync(
                        context,
                        placeholder,
                        table);

                    if (group.GroupType == WordDocumentTableGroupType.Group)
                    {
                        table.AddHorizontalGroupPlaceholder(
                            placeholder,
                            groupLevel);
                    }
                }
            }

            foreach (var innerGroup in group.InnerTableGroups)
            {
                if (innerGroup.GroupType != WordDocumentTableGroupType.Table)
                {
                    await this.FillTableAsync(
                        context,
                        innerGroup,
                        table,
                        innerGroup.GroupType == WordDocumentTableGroupType.Group ? groupLevel + 1 : 0);
                }
            }

            return table;
        }

        #endregion

        #region Base Overrides

        protected override IPlaceholderReplaceExtensionContext ExtensionContext => this.extensionContext;

        protected override IPlaceholderReplaceExtensionContext CreateExtensionContext(IPlaceholderReplacementContext context)
        {
            return this.extensionContext = new WordPlaceholderReplaceExtensionContext(context, context.CancellationToken);
        }

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
            var placeholders = new List<IPlaceholderText>();

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

                if (dictionary.TryGetValue(nameof(WordPlaceholderDocument), out var listObj)
                    && listObj is IList list && !(listObj is byte[]))
                {
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
                            newPlaceholder.Info[OpenXmlHelper.IndexField] = values.TryGet<int>(OpenXmlHelper.IndexField);
                            newPlaceholder.Info[OpenXmlHelper.OrderField] = values.TryGet<int>(OpenXmlHelper.OrderField);

                            placeholders.Add(newPlaceholder);
                        }
                    }
                }

                if (!dictionary.TryGetValue(nameof(WordDocumentTableGroup), out var bookmarkListObj)
                    || !(bookmarkListObj is IList bookmarkList))
                {
                    return placeholders;
                }

                this.tableGroupsFounded = true;
                foreach (Dictionary<string, object> bookmarkStorage in bookmarkList)
                {
                    var bookmarkTable = new WordDocumentTableGroup(bookmarkStorage);
                    this.tableGroups.Add(bookmarkTable);
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
            this.wordDocument = WordprocessingDocument.Open(this.Stream, true);

            if (this.WithExtensions)
            {
                this.extensionContext.Document = this.wordDocument;
            }

            return this.wordDocument;
        }

        /// <summary>
        /// Метод для получения плейсхолдеров из объекта документа
        /// </summary>
        /// <returns>Возвращает список плейсхолдеров, найденных в документе</returns>
        protected override List<IPlaceholderText> GetPlaceholdersFromDocument()
        {
            var result = this.GetPlaceholdersFromPart(this.wordDocument.MainDocumentPart);

            // Производим поиск табличных групп тут, т.к. часть информации о них может быть сохранена, и тогда повторный поиск по элементам уже не потребуется.
            this.tableGroups.AddRange(this.parser.FindTableGroups(this.wordDocument));
            this.tableGroupsFounded = true;

            return result;
        }

        /// <summary>
        /// Метод для подготовки документа к сохранению
        /// </summary>
        protected override void PrepareDocumentForSave()
        {
        }

        /// <summary>
        /// Метод для сохранения инициализированного документа
        /// </summary>
        protected override void SaveDocument()
        {
            this.wordDocument.Dispose();
            this.wordDocument = null;
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
                List<object> bookmarkList = new List<object>(this.tableGroups.Count);
                Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    [nameof(WordPlaceholderDocument)] = list,
                    [nameof(WordDocumentTableGroup)] = bookmarkList,
                };

                foreach (IPlaceholderText placeholder in placeholders)
                {
                    var pDictionary = new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        { OpenXmlHelper.PositionField, placeholder.Info[OpenXmlHelper.PositionField] },
                        { OpenXmlHelper.TextField, placeholder.Text },
                        { OpenXmlHelper.IndexField, placeholder.Info[OpenXmlHelper.IndexField] },
                        { OpenXmlHelper.OrderField, placeholder.Info.TryGet<int>(OpenXmlHelper.OrderField) }
                    };

                    list.Add(pDictionary);
                }

                this.tableGroups.RemoveAll(info => info.EndPosition == null);
                foreach (var info in this.tableGroups)
                {
                    if (info.EndPosition != null)
                    {
                        bookmarkList.Add(info.GetStorage());
                    }
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
        /// Производит замену плейсхолдеров типа Field в Word документе
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
                OpenXmlElement paragraph = this.wordDocument.MainDocumentPart.Document;
                OpenXmlPart part = this.wordDocument.MainDocumentPart;
                IList position = replacements.First().Placeholder.Info.Get<IList>(OpenXmlHelper.PositionField);

                if (position[0].ToString() == Hyperlink)
                {
                    if (position.Count > 1)
                    {
                        await this.ReplaceElementsInRelationshipsAsync(part, position[1] as string, context.CancellationToken, replacements.ToArray());
                    }
                }
                else
                {
                    for (int i = 0; i < position.Count; i++)
                    {
                        int index = (int) position[i];
                        if (index < 0)
                        {
                            part = part.Parts.ToArray()[~index].OpenXmlPart;
                            paragraph = part.RootElement;
                        }
                        else
                        {
                            paragraph = paragraph.ChildElements[index];
                        }
                    }

                    Type elemType = paragraph.GetType();
                    if (elemType == typeof(Paragraph) || elemType == typeof(Hyperlink))
                    {
                        await this.ReplaceElementsInCompositeElementAsync(paragraph, context.CancellationToken, replacements.ToArray());
                    }

                    if (elemType == typeof(FieldCode))
                    {
                        // ReSharper disable once PossibleInvalidCastException
                        await this.ReplaceElementsInFieldCodeAsync((FieldCode) paragraph, context.CancellationToken, replacements.ToArray());
                    }
                }

                hasChanges = true;
            }

            return hasChanges;
        }

        /// <summary>
        /// Производит замену плейсхолдеров типа Table в Word документе
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

            if (!this.tableGroupsFounded)
            {
                this.tableGroups.AddRange(this.parser.FindTableGroups(this.wordDocument));
            }

            this.parser.FillTableGroups(this.wordDocument, this.tableGroups);
            var tables = this.parser.FillTableRelationships(this.tableGroups);

            this.PrepareTablePlaceholders(
                tables,
                tablePlaceholders,
                out List<IPlaceholder> relationshipsPlaceholders);

            if (tables.Count == 0)
            {
                return false;
            }

            // Для каждой таблицы организуем получение данных и производим замену
            foreach (var tableGroup in tables)
            {
                await this.ReplaceGroupPlaceholdersAsync(
                    context,
                    tableGroup);
            }

            ClearOldRelationships(this.wordDocument.MainDocumentPart, relationshipsPlaceholders);
            return true;
        }

        /// <summary>
        /// Метод для поиска плейсхолдеров внутри элемента документа
        /// </summary>
        /// <param name="baseElement">Элемент, в котором производится поиск плейсхолдеров</param>
        /// <param name="position">Позиция элемента в документе</param>
        /// <returns>Возвращает список плейсхолдеров, найденных в текущем элементе</returns>
        protected override IEnumerable<IPlaceholderText> GetPlaceholdersFromElementOverride(OpenXmlElement baseElement, IList position)
        {
            Type elementType = baseElement.GetType();
            if (elementType == typeof(Paragraph) || elementType == typeof(Hyperlink))
            {
                List<IPlaceholderText> result = new List<IPlaceholderText>();

                StringBuilder allText = StringBuilderHelper.Acquire();

                List<Text> textElements = GetTextElements(baseElement);
                if (textElements != null)
                {
                    foreach (Text element in textElements)
                    {
                        allText.Append(element.Text);
                    }
                }

                string allTextString = allText.ToStringAndRelease();
                OpenXmlHelper.AddPlaceholdersFromText(result, allTextString, position);
                return result;
            }
            else if (elementType == typeof(FieldCode))
            {
                List<IPlaceholderText> result = new List<IPlaceholderText>();
                string codeText = ((FieldCode) baseElement).Text;
                string decodedText = Uri.UnescapeDataString(codeText);

                OpenXmlHelper.AddPlaceholdersFromText(result, decodedText, position, hasOrder: false);
                return result;
            }

            return Array.Empty<IPlaceholderText>();
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
            newText = this.RemoveInvalidChars(newText);

            string[] textRows = newText.Replace(Environment.NewLine, "\n", StringComparison.Ordinal).Split('\n');

            var text = (Text) firstTextElement;
            text.Text = textRows[0];
            text.Space = SpaceProcessingModeValues.Preserve;

            if (lastTextElement != null)
            {
                var lastText = (Text) lastTextElement;
                if (string.IsNullOrEmpty(lastText.Text))
                {
                    lastBaseElement.SafeRemove();
                }
                else
                {
                    lastText.Space = SpaceProcessingModeValues.Preserve;
                }
            }

            if (this.WithExtensions
                && firstBaseElement is Run run)
            {
                this.extensionContext.PlaceholderElements.Add(run);
            }

            for (int i = textRows.Length - 1; i > 0; i--)
            {
                if (string.IsNullOrEmpty(textRows[i]))
                {
                    var nextRow = new Run(new Break());
                    if (this.WithExtensions)
                    {
                        this.extensionContext.PlaceholderElements.Add(nextRow);
                    }

                    firstBaseElement.InsertAfterSelf(nextRow);
                }
                else
                {
                    OpenXmlElement nextElement = firstBaseElement.CloneNode(true);

                    Text nextTextElement = (Text) GetTextElement(nextElement);
                    nextTextElement.Text = textRows[i];
                    firstBaseElement.InsertAfterSelf(nextElement);

                    if (this.WithExtensions
                        && nextElement is Run nextRun)
                    {
                        this.extensionContext.PlaceholderElements.Add(nextRun);
                    }

                    // Т.к. внутри объекта Run может хранится и Text и Break одновременно, то при наличии Break, добавлять новый Break не нужно
                    if (!firstBaseElement.Descendants<Break>().Any())
                    {
                        var nextRow = new Run(new Break());
                        if (this.WithExtensions)
                        {
                            this.extensionContext.PlaceholderElements.Add(nextRow);
                        }

                        firstBaseElement.InsertAfterSelf(nextRow);
                    }
                }
            }

            if (this.WithExtensions)
            {
                await this.AfterPlaceholderReplaceAsync(this.extensionContext.ReplacementContext);
                this.extensionContext.PlaceholderElements.Clear();
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
        protected override async Task<bool> ReplaceImageAsync(
            OpenXmlElement baseElement,
            IPlaceholderReplacement replacement,
            CancellationToken cancellationToken = default)
        {
            var graphicBase = OpenXmlHelper.FindInParentChildren<A.Graphic>(baseElement)?.Parent;
            if (graphicBase == null)
            {
                return false;
            }

            PlaceholderValue value = replacement.NewValue;
            byte[] data = value.Data;

            if (data == null || data.Length == 0)
            {
                // пустое изображение при замене удаляет надпись
                var run = OpenXmlHelper.FindParent<Run>(graphicBase);
                if (run != null)
                {
                    run.SafeRemove();
                    return true;
                }

                return false;
            }

            IPlaceholderImageParameters imageParameters = value.FormatResult.GetImageParameters();
            ImagePartType imagePartType = OpenXmlHelper.GetImagePartType(imageParameters);

            var existingGraphic = graphicBase.GetFirstChild<A.Graphic>();
            var existingExtent = graphicBase.GetFirstChild<DW.Extent>();
            var existingProperties = existingGraphic?.GraphicData?.Descendants<WPS.ShapeProperties>().FirstOrDefault();

            A.Graphic graphic = this.CreateGraphicFromImage(
                data,
                imagePartType,
                graphicBase.GetPart(),
                existingProperties,
                existingExtent,
                imageParameters.Width,
                imageParameters.Height,
                imageParameters.Reformat);

            if (existingGraphic != null)
            {
                existingGraphic.Remove();
            }

            graphicBase.AppendChild(graphic);

            // замещающий текст
            var docProperties = graphicBase.GetFirstChild<DW.DocProperties>();
            if (docProperties != null)
            {
                docProperties.Title = LocalizationManager.Format(imageParameters.AlternativeText);
            }

            // теперь магия, если надпись была на якоре
            if (graphicBase is DW.Anchor anchor)
            {
                // обнуляем свойства distT="0" distB="0" distL="0" distR="0"
                anchor.DistanceFromTop = 0u;
                anchor.DistanceFromBottom = 0u;
                anchor.DistanceFromLeft = 0u;
                anchor.DistanceFromRight = 0u;

                // удаляем детей <wp14:sizeRelH/> и <wp14:sizeRelV/>
                anchor.RemoveAllChildren<Wp14.RelativeWidth>();
                anchor.RemoveAllChildren<Wp14.RelativeHeight>();
            }

            var alternateContent = OpenXmlHelper.FindParent<AlternateContent>(graphicBase);
            if (alternateContent != null)
            {
                // мы внутри т.н. альтернативного контента, где есть наша надпись/картинка,
                // а ещё некое Fallback-значение, которое мы хотим выпилить, потому что для картинок оно бесполезно,
                // и Word по умолчанию его не генерит

                OpenXmlElement alternateContentBase = alternateContent.Parent;
                OpenXmlElement choiceChild;

                if (alternateContentBase != null
                    && (choiceChild = alternateContent.GetFirstChild<AlternateContentChoice>()?.FirstChild) != null)
                {
                    // было: alternateContentBase -> alternateContent -> choice -> choiceChild -> ... -> graphicBase -> graphic
                    // нужно: alternateContentBase -> choiceChild -> ... -> graphicBase -> graphic

                    choiceChild.Remove();
                    alternateContent.Remove();

                    alternateContentBase.AppendChild(choiceChild);
                }
            }

            return true;
        }

        protected override void CleanAfterChanges()
        {
            // исправляем объекты "Надпись": идентификаторы DocProperties для всех строк должны быть уникальны
            foreach (DW.DocProperties docProperties
                in this.wordDocument.MainDocumentPart.Document.Descendants<DW.DocProperties>())
            {
                docProperties.Id = this.docPropertiesNextId++;
            }
        }

        protected override bool CheckTextElement(OpenXmlLeafTextElement baseElementText)
        {
            return !(baseElementText is FieldCode);
        }

        #endregion
    }
}