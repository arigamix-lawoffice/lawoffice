using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Platform.Collections;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using ParagraphProperties = DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Объект для парсинга документа Word на группы по закладкам документа Word.
    /// </summary>
    public sealed class WordDocumentTableGroupParser : IWordDocumentTableGroupParser
    {
        #region IWordDocumentTableGroupParser Implementation

        /// <summary>
        /// Метод для поиска табличных групп в документе <paramref name="wordDocument"/>. Возвращает сквозной список групп.
        /// </summary>
        /// <param name="wordDocument">Документ Word, в котором идет поиск табличных групп.</param>
        public List<WordDocumentTableGroup> FindTableGroups(WordprocessingDocument wordDocument)
        {
            List<WordDocumentTableGroup> tableGroups = new List<WordDocumentTableGroup>();

            void CheckPart(OpenXmlPart part, List<object> position)
            {
                CheckElement(part.RootElement, position);

                if (part.Parts != null)
                {
                    var curPosition = -1;
                    foreach (var childPart in part.Parts)
                    {
                        CheckPart(childPart.OpenXmlPart, new List<object>(position) { curPosition-- });
                    }
                }
            }

            void CheckElement(OpenXmlElement element, List<object> position)
            {
                if (element is null)
                {
                    return;
                }
                else if (element is BookmarkStart bookStart)
                {
                    var bookName = bookStart.Name.Value;
                    if (bookName.Length < 3)
                    {
                        return;
                    }

                    WordDocumentTableGroupType? type = null;
                    switch (bookName.Substring(0, 2))
                    {
                        case "r_":
                            type = WordDocumentTableGroupType.Row;
                            break;
                        case "g_":
                            type = WordDocumentTableGroupType.Group;
                            break;
                        case "t_":
                            type = WordDocumentTableGroupType.Table;
                            break;
                    }

                    if (type.HasValue)
                    {
                        var startPosition = position.ToList();
                        var lastPoint = (int)startPosition[^1];
                        OpenXmlElement checkElement = bookStart;
                        while ((checkElement = checkElement.NextSibling()) != null
                            && (checkElement is BookmarkStart || checkElement is BookmarkEnd))
                        {
                            lastPoint++;
                        }
                        startPosition[^1] = lastPoint;
                        tableGroups.Add(new WordDocumentTableGroup()
                        {
                            GroupType = type.Value,
                            StartIndex = OpenXmlHelper.GetIndex(bookStart),
                            StartPosition = startPosition,
                            ID = bookStart.Id,
                            Name = bookStart.Name,
                        });
                    }
                }
                else if (element is BookmarkEnd bookEnd)
                {
                    var info = tableGroups.FirstOrDefault(x => bookEnd.Id.Value == x.ID);
                    if (info != null)
                    {
                        var endPosition = position.ToList();
                        var lastPoint = (int)endPosition[^1];
                        OpenXmlElement checkElement = bookEnd;
                        while ((checkElement = checkElement.PreviousSibling()) != null
                            && (checkElement is BookmarkStart || checkElement is BookmarkEnd))
                        {
                            lastPoint--;
                        }
                        endPosition[^1] = lastPoint;
                        info.EndPosition = endPosition;
                        info.EndIndex = OpenXmlHelper.GetIndex(bookEnd);
                    }
                }

                if (element.HasChildren)
                {
                    int curPosition = 0;
                    foreach (var child in element.ChildElements)
                    {
                        var newPosition = new List<object>(position);
                        newPosition.Add(curPosition++);
                        CheckElement(child, newPosition);
                    }
                }
            };

            CheckPart(wordDocument.MainDocumentPart, new List<object>());
            return tableGroups;
        }

        /// <inheritdoc/>
        public void FillTableGroups(WordprocessingDocument wordDocument, List<WordDocumentTableGroup> tableGroups)
        {
            // Заполняем таблицы-по закладкам элементами документа
            // Закладка может целиком лежать внутри одного параграфа, тогда базовыми элементами будут являться объекты внутри параграфа.
            // Если закладка лежит между несколькими параграфами, то базовыми элементами считаем все параграфы и таблицы от первого до последнего
            // Поиском ведем на самом верхнем уровне среди этих двух элементов
            foreach (var table in tableGroups)
            {
                var startPosition = table.StartPosition;
                var endPosition = table.EndPosition;
                var minCount = Math.Min(startPosition.Count, endPosition.Count);
                int depth = 0;
                // Берем глубину, как индекс первого различия в позициях начала и конца позиции
                while (depth < minCount
                    && (int)startPosition[depth] == (int)endPosition[depth])
                {
                    depth++;
                }

                depthRecalc:
                table.TableStartPosition = startPosition.Cast<object>().Take(depth + 1).ToList();
                table.TableEndPosition = endPosition.Cast<object>().Take(depth + 1).ToList();

                var firstElement = OpenXmlHelper.GetElementByPosition(wordDocument.MainDocumentPart, table.TableStartPosition);

                if (startPosition.Count == depth + 1
                    && endPosition.Count == depth + 1
                    && firstElement.Parent is Paragraph)
                {
                    table.InParagraph = true;
                    table.TableStartPosition.RemoveAt(table.TableStartPosition.Count - 1);
                    table.TableEndPosition.RemoveAt(table.TableEndPosition.Count - 1);

                    var startBaseElement = firstElement.Parent;
                    table.TableElement = startBaseElement;

                    bool needAdd = false;
                    for (int i = 0; i < startBaseElement.ChildElements.Count; i++)
                    {
                        var element = startBaseElement.ChildElements[i];
                        if (element is BookmarkStart start)
                        {
                            if (start.Id.Value == table.ID)
                            {
                                needAdd = true;
                            }
                        }
                        else if (element is BookmarkEnd end)
                        {
                            if (end.Id.Value == table.ID)
                            {
                                break;
                            }
                        }
                        else if (needAdd)
                        {
                            table.BaseElements.Add(element);
                        }
                    }
                }
                else
                {
                    // Число дополнительных элементов, из которых состоит строка таблицы, помимо firstElement
                    var nextElementsCount = (int)endPosition[depth] - (int)startPosition[depth];

                    if (nextElementsCount > 0)
                    {
                        // Последний элемент нужно добавлять только в ситуации, когда перед окончанием закладки есть что-нибудь полезное
                        var lastElementIsValueable = false;
                        var endIndex = (int)endPosition[^1];
                        var endBaseElement = OpenXmlHelper.GetElementByPosition(wordDocument.MainDocumentPart, endPosition.Cast<object>().Take(endPosition.Count - 1).ToArray());

                        // Если первый элемент входит в коллекцию базового последнего элемента, то начинаем отсчет от следующего элемента
                        int i = endBaseElement.IndexOf(firstElement) + 1;

                        for (; i <= endIndex; i++)
                        {
                            var childElement = endBaseElement.ChildElements[i];
                            if (!(childElement is TableProperties
                                || childElement is ParagraphProperties
                                || childElement is BookmarkStart
                                || childElement is BookmarkEnd))
                            {
                                lastElementIsValueable = true;
                                break;
                            }
                        }

                        if (!lastElementIsValueable)
                        {
                            // Если нет значимых элементов, есть 2 варианта развития событий:
                            // 1) Просто уменьшаем позицию окончания таблицы на 1. Возникает, когда выделяется параграф целиком. Метка в таком случае ставится начале в следующего параграфа.
                            // 2) Word очень странно размещает конец метки внутри таблицы в ситуации, если при сохранении курсор был указан на абзац сразу после таблицы.
                            // или в последнем абзаце есть текст. В первом случае метка будет внутри параграфа сразу за таблицей. Во втором случае будет как отдельный элемент за таблицей,
                            // но перед параграфом с текстом
                            // Из-за этих особенностей, расчет глубины метки некорректен, т.к. общим родителем начала и конца всегда будет именно элемент Body.
                            // Поэтому при такой ситуации мы увеличиваем depth на 1, обновляем позицию окончания метки и перерасчитываем положение таблицы.

                            bool isBody = endBaseElement is Body;

                            // Если первый элемент таблица целиком, а всего их 2 (последний элемент - это внешний параграф или внешняя метка), то ситуация (2)
                            if ((isBody || nextElementsCount == 1) && firstElement is Table)
                            {
                                depth++;
                                if (!isBody)
                                {
                                    endPosition.RemoveAt(endPosition.Count - 1);
                                }
                                endPosition[^1] = (int)endPosition[^1] - nextElementsCount;
                                endPosition.Add(firstElement.ChildElements.Count - 1);
                                goto depthRecalc;
                            }
                            else // иначе ситуация (1)
                            {
                                table.TableEndPosition[^1] = (int)table.TableEndPosition[^1] - 1;
                                nextElementsCount--;
                            }
                        }
                    }

                    table.TableElement = firstElement.Parent;
                    table.BaseElements.Add(firstElement);

                    while (nextElementsCount-- > 0)
                    {
                        firstElement = firstElement.NextSibling();
                        if (firstElement is BookmarkStart
                            || firstElement is BookmarkEnd)
                        {
                            continue;
                        }

                        table.BaseElements.Add(firstElement);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public List<WordDocumentTableGroup> FillTableRelationships(
            IEnumerable<WordDocumentTableGroup> newTables,
            List<WordDocumentTableGroup> tables = null)
        {
            tables ??= new List<WordDocumentTableGroup>();

            foreach (var info in newTables)
            {
                this.FillTableRelationshipsRecursive(tables, info);
            }

            return tables;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Метод для рекурсивного обхода и добавления таблицы в другие таблицы.
        /// </summary>
        /// <param name="tables">Добавляемые таблицы.</param>
        /// <param name="checkTable">Таблица, добавление которой производится.</param>
        private void FillTableRelationshipsRecursive(
            List<WordDocumentTableGroup> tables,
            WordDocumentTableGroup checkTable)
        {
            if (!checkTable.IsPrepared)
            {
                throw new InvalidOperationException($"{nameof(this.FillTableGroups)} call is required for tableGroups before {nameof(this.FillTableRelationships)} call.");
            }

            bool isInnerTable = false;
            foreach (var table in tables)
            {
                if (table.Contains(checkTable))
                {
                    isInnerTable = true;
                    this.FillTableRelationshipsRecursive(
                        table.InnerTableGroups,
                        checkTable);
                }
                // Обратную проверку не делаем, т.к. их добавление производится по дереву
            }

            if (!isInnerTable)
            {
                tables.Add(checkTable);
            }
        }

        #endregion
    }
}
