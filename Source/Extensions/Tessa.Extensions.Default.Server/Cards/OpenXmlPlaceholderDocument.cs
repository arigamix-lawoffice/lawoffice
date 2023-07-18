using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public abstract class OpenXmlPlaceholderDocument :
        PlaceholderDocument
    {
        #region ReplacementStatus Private Enum

        /// <summary>
        /// Перечислимый тип, обозначающий статус замены плейсхолдера в документе.
        /// </summary>
        protected enum ReplacementStatus
        {
            /// <summary>
            /// Плейсхолдер не найден.
            /// </summary>
            None = 0,

            /// <summary>
            /// Найдено начало плейсхолдера.
            /// </summary>
            PartFound = 1,

            /// <summary>
            /// Плейсхолдер найден и успешно заменен.
            /// </summary>
            Replaced = 2,

            /// <summary>
            /// Плейсхолдер найден и не заменен.
            /// </summary>
            NotReplaced = 3,

            /// <summary>
            /// Все плейсхолдеры заменены.
            /// </summary>
            AllReplaced = 4,
        }

        #endregion

        #region Consts

        /// <summary>
        /// Значение определяет принадлежность данного плейсхолдера к гиперссылке
        /// </summary>
        protected const string Hyperlink = "hyper";

        protected const string HyperlinkRemoveHead = "https://tessalink/?link=";

        protected const int HyperlinkRemoveHeadLength = 24;

        #endregion

        #region Fields

        /// <summary>
        /// ID карточки шаблона файла.
        /// </summary>
        protected readonly Guid TemplateID;

        /// <summary>
        /// Регулярное выражение для поиска всех запрещенных в XML символов.
        /// https://stackoverflow.com/questions/397250/unicode-regex-invalid-xml-characters/961504#961504
        /// </summary>
        private static readonly Regex invalidXmlChars = new Regex(
            @"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]",
            RegexOptions.Compiled);

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
        protected OpenXmlPlaceholderDocument(MemoryStream stream, Guid templateID)
        {
            this.Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            this.TemplateID = templateID;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Поток файла документа, в которой должны быть или уже были заменены плейсхолдеры.
        /// </summary>
        public MemoryStream Stream { get; }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Метод для получения информации о плейсхолдерах документа из базы данных
        /// </summary>
        /// <param name="dbScope">Объект dbScope текущего подключения к базе данных</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает список плейсхолдеров документа из базы данных</returns>
        protected abstract Task<List<IPlaceholderText>> GetPlaceholdersFromDatabaseAsync(
            IDbScope dbScope,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для сохранения информации о плейсхолдерах документа в базу данных
        /// </summary>
        /// <param name="dbScope">Объект dbScope текущего подключения к базе данных</param>
        /// <param name="placeholders">Список плейсхолдеров для сохранения</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected abstract Task SavePlaceholdersInDatabaseAsync(
            IDbScope dbScope,
            List<IPlaceholderText> placeholders,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для инициализации документа
        /// </summary>
        /// <returns>Возвращает инициализированный документ</returns>
        protected abstract OpenXmlPackage InitDocument();

        /// <summary>
        /// Метод для подготовки документа к сохранению
        /// </summary>
        protected abstract void PrepareDocumentForSave();

        /// <summary>
        /// Метод для сохранения инициализированного документа
        /// </summary>
        protected abstract void SaveDocument();

        /// <summary>
        /// Метод для получения плейсхолдеров из объекта документа
        /// </summary>
        /// <returns>Возвращает список плейсхолдеров, найденных в документе</returns>
        protected abstract List<IPlaceholderText> GetPlaceholdersFromDocument();

        /// <summary>
        /// Метод для поиска плейсхолдеров внутри элемента документа
        /// </summary>
        /// <param name="baseElement">Элемент, в котором производится поиск плейсхолдеров</param>
        /// <param name="position">Позиция элемента в документе</param>
        /// <returns>Возвращает список плейсхолдеров, найденных в текущем элементе</returns>
        protected abstract IEnumerable<IPlaceholderText> GetPlaceholdersFromElementOverride(OpenXmlElement baseElement, IList position);

        /// <summary>
        /// Метод для замены плейсхолдеров типа Field
        /// </summary>
        /// <param name="context">Контекст замены плейсхолдеров</param>
        /// <returns>Возвращает true, если был заменен хотя бы один плейсхолдер</returns>
        protected abstract Task<bool> ReplaceFieldPlaceholdersAsync(IPlaceholderReplacementContext context);

        /// <summary>
        /// Метод для замены плейсхолдеров типа Table
        /// </summary>
        /// <param name="context">Контекст замены плейсхолдеров</param>
        /// <returns>Возвращает true, если был заменен хотя бы один плейсхолдер</returns>
        protected abstract Task<bool> ReplaceTablePlaceholdersAsync(IPlaceholderReplacementContext context);

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
        protected abstract Task ReplaceTextAsync(
            OpenXmlElement firstBaseElement,
            OpenXmlLeafTextElement firstTextElement,
            OpenXmlElement lastBaseElement,
            OpenXmlLeafTextElement lastTextElement,
            string newText,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод, определяющий правила замены изображения в элементе документа.
        /// Возвращает признак того, что замена выполнена успешно.
        /// </summary>
        /// <param name="baseElement">Базовый элемент, в котором производится замена текста</param>
        /// <param name="replacement">Значение с изображением, которое требуется заменить</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак того, что замена выполнена успешно.</returns>
        protected abstract Task<bool> ReplaceImageAsync(
            OpenXmlElement baseElement,
            IPlaceholderReplacement replacement,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для выполнения постобработки документа после изменений
        /// </summary>
        protected virtual void CleanAfterChanges()
        {
        }

        /// <summary>
        /// Метод производит проверку, что данный текстовый элемент допустим для замены текста в нем
        /// </summary>
        /// <param name="baseElementText">проверяемый текстовый элемент</param>
        /// <returns></returns>
        protected virtual bool CheckTextElement(OpenXmlLeafTextElement baseElementText)
        {
            return true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Метод для удаления их строки всех запрещенных в XML символов
        /// </summary>
        /// <param name="text">Переданная строка</param>
        /// <returns></returns>
        protected string RemoveInvalidChars(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            return invalidXmlChars.Replace(text, string.Empty);
        }

        /// <summary>
        /// Производит замену плейсхолдеров в гиперссылке
        /// </summary>
        /// <param name="mainPart">Объект документа, хранящих список гиперссылок</param>
        /// <param name="relID">ID гиперссылки</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="replacements">Массив плейсхолдеров для замены</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task ReplaceElementsInRelationshipsAsync(
            OpenXmlPart mainPart,
            string relID,
            CancellationToken cancellationToken = default,
            params IPlaceholderReplacement[] replacements)
        {
            ReferenceRelationship relationship = mainPart.HyperlinkRelationships.FirstOrDefault(a => a.Id == relID);
            if (relationship != null)
            {
                StringBuilder sb = new StringBuilder(Uri.UnescapeDataString(relationship.Uri.ToString()));
                sb.Replace(HyperlinkRemoveHead, string.Empty, 0, Math.Min(HyperlinkRemoveHeadLength, sb.Length));

                foreach (IPlaceholderReplacement replacement in replacements)
                {
                    var newValue = replacement.NewValue;
                    string newText;
                    if (this.WithExtensions)
                    {
                        this.ExtensionContext.Placeholder = replacement.Placeholder;
                        this.ExtensionContext.PlaceholderValue = replacement.NewValue;
                        await this.BeforePlaceholderReplaceAsync(this.ExtensionContext.ReplacementContext);
                        newValue = this.ExtensionContext.PlaceholderValue;

                        ((OpenXmlPlaceholderReplaceExtensionContext) this.ExtensionContext).PlaceholderText = newValue.Text;
                        await this.AfterPlaceholderReplaceAsync(this.ExtensionContext.ReplacementContext);
                        newText = ((OpenXmlPlaceholderReplaceExtensionContext) this.ExtensionContext).PlaceholderText;
                    }
                    else
                    {
                        newText = newValue.Text;
                    }

                    sb.Replace(replacement.Placeholder.Text, newText);

                }

                mainPart.DeleteReferenceRelationship(relationship);
                mainPart.AddHyperlinkRelationship(new Uri(Uri.EscapeDataString(sb.ToString()), UriKind.RelativeOrAbsolute), true, relID);
            }
        }

        /// <summary>
        /// Производит замену плейсхолдеров в гиперссылке с ее копированием
        /// </summary>
        /// <param name="mainPart">Объект документа, хранящих список гиперссылок</param>
        /// <param name="relID">ID гиперссылки</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="replacements">Плейсхолдер для замены</param>
        /// <returns>Возвращает новый ID гиперссылки</returns>
        protected async Task<string> ReplaceElementsInRelationshipsWithCopyAsync(
            OpenXmlPart mainPart,
            string relID,
            CancellationToken cancellationToken = default,
            params IPlaceholderReplacement[] replacements)
        {
            string origRelID = (string)replacements[0].Placeholder.Info.Get<IList>(OpenXmlHelper.PositionField).Cast<object>().Last();
            ReferenceRelationship relationship = mainPart.HyperlinkRelationships.FirstOrDefault(a => a.Id == relID);
            if (relationship != null)
            {
                StringBuilder sb = new StringBuilder(Uri.UnescapeDataString(relationship.Uri.ToString()));
                sb.Replace(HyperlinkRemoveHead, string.Empty, 0, Math.Min(HyperlinkRemoveHeadLength, sb.Length));

                foreach (var replacement in replacements)
                {
                    var newValue = replacement.NewValue;
                    string newText;
                    if (this.WithExtensions)
                    {
                        this.ExtensionContext.Placeholder = replacement.Placeholder;
                        this.ExtensionContext.PlaceholderValue = replacement.NewValue;
                        await this.BeforePlaceholderReplaceAsync(this.ExtensionContext.ReplacementContext);
                        newValue = this.ExtensionContext.PlaceholderValue;

                        ((OpenXmlPlaceholderReplaceExtensionContext) this.ExtensionContext).PlaceholderText = newValue.Text;
                        await this.AfterPlaceholderReplaceAsync(this.ExtensionContext.ReplacementContext);
                        newText = ((OpenXmlPlaceholderReplaceExtensionContext) this.ExtensionContext).PlaceholderText;
                    }
                    else
                    {
                        newText = newValue.Text;
                    }

                    sb.Replace(replacement.Placeholder.Text, newText);
                }

                if (!string.Equals(relID, origRelID, StringComparison.OrdinalIgnoreCase))
                {
                    mainPart.DeleteReferenceRelationship(relationship);
                }
                ReferenceRelationship newRelationsip = mainPart.AddHyperlinkRelationship(new Uri(Uri.EscapeDataString(sb.ToString()), UriKind.RelativeOrAbsolute), true);
                return newRelationsip.Id;
            }
            return relID;
        }

        /// <summary>
        /// Производит замену Replacement'ов в заданном элементе
        /// </summary>
        /// <param name="element">Параграф, в котором производится замена плейсхолдеров</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="placeholders">Массив плейсхолдеров для замены</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task ReplaceElementsInCompositeElementAsync(
            OpenXmlElement element,
            CancellationToken cancellationToken = default,
            params IPlaceholderReplacement[] placeholders)
        {
            IPlaceholderReplacement curPlaceholder = placeholders[0];
            if (this.WithExtensions)
            {
                this.ExtensionContext.Placeholder = curPlaceholder.Placeholder;
                this.ExtensionContext.PlaceholderValue = curPlaceholder.NewValue;
                await this.BeforePlaceholderReplaceAsync(this.ExtensionContext.ReplacementContext);
                if (!curPlaceholder.NewValue.Equals(this.ExtensionContext.PlaceholderValue))
                {
                    curPlaceholder = new PlaceholderReplacement(curPlaceholder.Placeholder, this.ExtensionContext.PlaceholderValue);
                }
            }

            int number = 1;

            List<OpenXmlElement> placeholderElements = new List<OpenXmlElement>();
            foreach (OpenXmlElement run in element.ChildElements.ToList())
            {
                ReplacementStatus res;

                do
                {
                    res = await this.ReplaceElementAsync(run, curPlaceholder, cancellationToken, placeholderElements.ToArray());

                    if (res == ReplacementStatus.Replaced)
                    {
                        if (placeholders.Length == number)
                        {
                            res = ReplacementStatus.AllReplaced;
                        }
                        else
                        {
                            curPlaceholder = placeholders[number++];

                            if (this.WithExtensions)
                            {
                                this.ExtensionContext.Placeholder = curPlaceholder.Placeholder;
                                this.ExtensionContext.PlaceholderValue = curPlaceholder.NewValue;
                                await this.BeforePlaceholderReplaceAsync(this.ExtensionContext.ReplacementContext);
                                if (!curPlaceholder.NewValue.Equals(this.ExtensionContext.PlaceholderValue))
                                {
                                    curPlaceholder = new PlaceholderReplacement(curPlaceholder.Placeholder, this.ExtensionContext.PlaceholderValue);
                                }
                            }

                            placeholderElements = new List<OpenXmlElement>();
                        }
                    }
                    if (res == ReplacementStatus.NotReplaced)
                    {
                        placeholderElements = new List<OpenXmlElement>();
                    }
                }
                while (res == ReplacementStatus.Replaced
                    || res == ReplacementStatus.NotReplaced);

                if (res == ReplacementStatus.PartFound)
                {
                    placeholderElements.Add(run);
                }
                else
                {
                    placeholderElements = new List<OpenXmlElement>();
                }

                if (res == ReplacementStatus.AllReplaced)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Производит замену плейсхолдера в baseElement. Если текстовая часть baseElement содержит только начало плейсхолдера,
        /// то возвращаем ReplacementStatus.PartFound.
        /// </summary>
        /// <param name="baseElement">Базовый элемент, в котором производится замена плейсхолдера</param>
        /// <param name="replacement">Объект, содержащий информацию о замене</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="partElements">Объекты, содержащие части плейсхолдера</param>
        /// <returns>Результат замены плейсхолдера</returns>
        protected async Task<ReplacementStatus> ReplaceElementAsync(
            OpenXmlElement baseElement,
            IPlaceholderReplacement replacement,
            CancellationToken cancellationToken = default,
            params OpenXmlElement[] partElements)
        {
            PlaceholderValue newValue = replacement.NewValue;
            if (newValue.Type == PlaceholderValueTypes.Image)
            {
                bool success = await this.ReplaceImageAsync(baseElement, replacement, cancellationToken);
                return success ? ReplacementStatus.AllReplaced : ReplacementStatus.None;
            }

            bool fromStart = partElements == null || !partElements.Any();
            OpenXmlLeafTextElement baseElementText = GetTextElement(baseElement);
            if (baseElementText == null
                || !this.CheckTextElement(baseElementText))
            {
                return fromStart
                    ? ReplacementStatus.None
                    : ReplacementStatus.PartFound;
            }

            if (baseElementText.Text.Contains(replacement.Placeholder.Text, StringComparison.Ordinal))
            {
                await this.ReplaceTextAsync(
                    baseElement, baseElementText, null, null,
                    baseElementText.Text.Replace(replacement.Placeholder.Text, newValue.Text, StringComparison.Ordinal), cancellationToken);

                return ReplacementStatus.Replaced;
            }

            // Если часть начала есть и она не имеет закрывающего тега в этой части текста, правее последнего открывающего тега
            if (fromStart
                && baseElementText.Text.Contains(PlaceholderHelper.LeftBracket, StringComparison.Ordinal)
                && baseElementText.Text.LastIndexOf(PlaceholderHelper.LeftBracket, StringComparison.Ordinal)
                 > baseElementText.Text.LastIndexOf(PlaceholderHelper.RightBracket, StringComparison.Ordinal))
            {
                return ReplacementStatus.PartFound;
            }

            if (!fromStart && baseElementText.InnerXml.Contains(PlaceholderHelper.RightBracket, StringComparison.Ordinal))
            {
                OpenXmlLeafTextElement firstElementText = GetTextElement(partElements[0]);
                int startIndex = firstElementText.Text.LastIndexOf(PlaceholderHelper.LeftBracket, StringComparison.Ordinal);
                int endIndex = baseElementText.Text.IndexOf(PlaceholderHelper.RightBracket, StringComparison.Ordinal);

                string firstElementString = firstElementText.Text;
                StringBuilder foundPlaceholder = StringBuilderHelper.Acquire()
                    .Append(firstElementString, startIndex, firstElementString.Length - startIndex);

                for (int j = 1; j < partElements.Length; j++)
                {
                    var textElement = GetTextElement(partElements[j]);
                    if (textElement != null)
                    {
                        foundPlaceholder
                            .Append(textElement.Text);
                    }
                }

                foundPlaceholder
                    .Append(baseElementText.Text, 0, endIndex + 1);

                if (foundPlaceholder.ToString() != replacement.Placeholder.Text)
                {
                    // Плейсхолдер не совпадает, пропускаем его
                    return ReplacementStatus.NotReplaced;
                }

                // Удаляем часть placeholder из последнего элемента
                baseElementText.Text = baseElementText.Text.Remove(0, endIndex + 1);

                // Удаляем все средние элементы между началом и концом плейсхолдера
                for (int j = 1; j < partElements.Length; j++)
                {
                    partElements[j].Remove();
                }

                // Записываем все в первый элемент с частью плейсхолдера
                await this.ReplaceTextAsync(
                    partElements[0], firstElementText, baseElement, baseElementText,
                    firstElementText.Text.Remove(startIndex) + newValue.Text, cancellationToken);

                return ReplacementStatus.Replaced;
            }

            return fromStart
                ? ReplacementStatus.None
                : ReplacementStatus.PartFound;
        }

        /// <summary>
        /// Находим во всех частях и элементах basePart плейсхолдеры
        /// </summary>
        /// <param name="basePart">Базовая часть документа, в которой производится поиск</param>
        /// <param name="position">Текущая позиция внутри документа. Позиция частей документов записывается с отрицательным знаком, а элементов - с положительным</param>
        /// <returns>Возвращает все найденные в данной части документа плейсхолдеры</returns>
        protected List<IPlaceholderText> GetPlaceholdersFromPart(OpenXmlPart basePart, params object[] position)
        {
            List<IPlaceholderText> result = this.GetPlaceholdersFromElement(basePart.RootElement, position);

            result.AddRange(GetPlaceholdersFromRelationships(basePart.HyperlinkRelationships, position));

            int curPos = -1;
            foreach (IdPartPair part in basePart.Parts)
            {
                var newPosition = new List<object>(position) { curPos-- };
                result.AddRange(this.GetPlaceholdersFromPart(part.OpenXmlPart, newPosition.ToArray()));
            }

            return result;
        }


        /// <summary>
        /// Находим во всех дочерних элементах <value>baseElement</value> плейсхолдеры
        /// </summary>
        /// <param name="baseElement">Базовый элемент, начиная с которого производим поиск</param>
        /// <param name="position">Текущая позиция внутри документа. Позиция частей документов записывается с отрицательным знаком, а элементов - с положительным</param>
        /// <returns>Возвращает все найденные в данном элементе плейсхолдеры</returns>
        protected List<IPlaceholderText> GetPlaceholdersFromElement(OpenXmlElement baseElement, params object[] position)
        {
            var result = new List<IPlaceholderText>();
            if (baseElement == null || baseElement.ChildElements == null)
            {
                return result;
            }

            int curPos = 0;
            foreach (OpenXmlElement element in baseElement.ChildElements)
            {
                var newPosition = new List<object>(position);
                newPosition.Add(curPos++);

                result.AddRange(this.GetPlaceholdersFromElementOverride(element, newPosition));

                if (element.HasChildren)
                {
                    result.AddRange(this.GetPlaceholdersFromElement(element, newPosition.ToArray()));
                }
            }

            return result;
        }

        #endregion

        #region Protected Static Methods

        /// <summary>
        /// Метод для получаения элемента по плейсхолдеру в документе
        /// </summary>
        /// <param name="mainPart">Объект документа, в котором происводится поиск элемента</param>
        /// <param name="placeholder">Плейсхолдер, для которого производится поиск соответствующего элемента в документе</param>
        /// <returns>Возвращает базовый элемент, соответствующий заданному плейсхолдеру</returns>
        protected static OpenXmlElement GetElementByPlaceholder(OpenXmlPart mainPart, IPlaceholder placeholder)
        {
            if (IsRelationship(placeholder))
            {
                return null;
            }

            IList position = placeholder.Info.Get<IList>(OpenXmlHelper.PositionField);
            return OpenXmlHelper.GetElementByPosition(mainPart, position);
        }

        /// <summary>
        /// Получает список плейсхолдеров из списока Relationships
        /// </summary>
        /// <param name="relationships">Список Relationships</param>
        /// <param name="position">Определяет позицию объектов relationships в документе</param>
        /// <returns>Возвращает найденные в relationships плейсхолдеры</returns>
        protected static List<IPlaceholderText> GetPlaceholdersFromRelationships(IEnumerable<ReferenceRelationship> relationships, params object[] position)
        {
            List<IPlaceholderText> result = new List<IPlaceholderText>();

            foreach (ReferenceRelationship referenceRelationship in relationships)
            {
                var relationship = (HyperlinkRelationship)referenceRelationship;

                string codeText = relationship.Uri.ToString();
                string decodedText = Uri.UnescapeDataString(codeText);

                foreach (Match match in PlaceholderHelper.Regex.Matches(decodedText))
                {
                    string text = match.Value;
                    string value = PlaceholderHelper.TryGetValue(text);
                    if (value != null)
                    {
                        var newPlaceholder = new PlaceholderText(text, value);
                        List<object> newPosition = new List<object>();
                        if (position != null)
                        {
                            newPosition.AddRange(position);
                        }

                        newPosition.Add(Hyperlink);
                        newPosition.Add(relationship.Id);

                        newPlaceholder.Info[OpenXmlHelper.PositionField] = newPosition;
                        newPlaceholder.Info[OpenXmlHelper.IndexField] = match.Index;

                        result.Add(newPlaceholder);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Метод для очистки старых ссылок из документа
        /// </summary>
        /// <param name="mainPart">Часть документа, из которой удаляются ссылки.</param>
        /// <param name="relationshipsPlaceholders">Список плейсхолдеров в ссылках для удаления</param>
        protected static void ClearOldRelationships(OpenXmlPart mainPart, IList<IPlaceholder> relationshipsPlaceholders)
        {
            foreach (string relID
                in relationshipsPlaceholders
                    .Where(x => x.Info.TryGet<OpenXmlElement>(OpenXmlHelper.BaseElementField) != null)
                    .Select(x => x.Info.Get<IList>(OpenXmlHelper.PositionField).Cast<object>().Last())
                    .Distinct())
            {
                mainPart.DeleteReferenceRelationship(relID);
            }
        }

        /// <summary>
        /// Возвращает первый дочерний элемент с типом <see cref="OpenXmlLeafTextElement"/> среди всех дочерних элементов объейкта <paramref name="baseElement"/>
        /// </summary>
        /// <param name="baseElement">Базовый элемент, начиная с которого производим поиск</param>
        /// <returns>Возвращает объект типа <see cref="OpenXmlLeafTextElement"/>, или null, если <paramref name="baseElement"/> не содержит дочерних элементов данного типа</returns>
        protected static OpenXmlLeafTextElement GetTextElement(OpenXmlElement baseElement)
        {
            OpenXmlLeafTextElement textElement = baseElement as OpenXmlLeafTextElement;
            if (textElement != null)
            {
                return textElement;
            }

            if (!baseElement.HasChildren)
            {
                return null;
            }

            foreach (OpenXmlElement e in baseElement.ChildElements)
            {
                textElement = e as OpenXmlLeafTextElement ?? GetTextElement(e);

                if (textElement != null)
                {
                    break;
                }
            }

            return textElement;
        }

        /// <summary>
        /// Метод переводит позицию объекта в документе в строку
        /// </summary>
        /// <param name="position">Позиция объекта в документе</param>
        /// <returns>Возвращает строку вида "position[0]->position[1]->...->position[n]"</returns>
        protected static string TextPosition(IList position)
        {
            return OpenXmlHelper.TextPosition(position);
        }

        /// <summary>
        /// Метод для проверки принадлежности плейсхолдера к Relationship
        /// </summary>
        /// <param name="placeholder">Проверяемый плейсхолдер</param>
        /// <returns>Метод возвращает true, если плейсхолдер находится в ссылке</returns>
        protected static bool IsRelationship(IPlaceholder placeholder)
        {
            IList position;
            if (placeholder == null
                || (position = placeholder.Info.TryGet<IList>(OpenXmlHelper.PositionField, null)) == null
                )
            {
                return false;
            }
            return position.Count > 0 && position.Cast<object>().Any(x => x.ToString() == Hyperlink);
        }


        /// <summary>
        /// Получает дочерний элемент newParent, соответствующий элементу baseChild относительно baseParent. Элемент newParent должен быть полной копией элемента baseParent
        /// </summary>
        /// <typeparam name="TType">Тип искомого объекта</typeparam>
        /// <param name="baseParent">Родительский элемент, относительно которого ведется поиск</param>
        /// <param name="baseChild">Дочерний элемент, соответствие которого мы ищем в newParent</param>
        /// <param name="newParent">Копия родительского элемента, дочерний элемент которого мы ищем</param>
        /// <returns>Возвращает дочерний элемент newParent, соответствующий элементу baseChild относительно baseParent, или null, если baseChild отсутствует в baseParent</returns>
        protected static TType GetRelativeElement<TType>(
            OpenXmlElement baseParent,
            OpenXmlElement baseChild,
            OpenXmlElement newParent
            ) where TType : OpenXmlElement
        {
            if (baseParent == baseChild)
            {
                return (TType)newParent;
            }
            int position = baseParent.Descendants<TType>().IndexOf(baseChild);
            if (position >= 0)
            {
                return newParent.Descendants<TType>().ElementAtOrDefault(position);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="IPlaceholderDocument" and @item="FindAsync"]'/>
        protected override async ValueTask<IList<IPlaceholderText>?> FindCoreAsync(IPlaceholderFindingContext context)
        {
            List<IPlaceholderText> placeholders;
            IDbScope dbScope = context.TryGetDbScope();

            if (dbScope != null)
            {
                placeholders = await this.GetPlaceholdersFromDatabaseAsync(dbScope, context.CancellationToken);
                if (placeholders.Count > 0)
                {
                    return placeholders;
                }
            }

            using (this.InitDocument())
            {
                placeholders = this.GetPlaceholdersFromDocument();
                this.PrepareDocumentForSave();
                this.SaveDocument();
            }

            if (placeholders.Count == 0)
            {
                return Array.Empty<IPlaceholderText>();
            }

            if (dbScope != null)
            {
                await this.SavePlaceholdersInDatabaseAsync(dbScope, placeholders, context.CancellationToken);
            }

            return placeholders;
        }

        /// <doc path='info[@type="IPlaceholderDocument" and @item="ReplaceAsync"]'/>
        protected override async ValueTask ReplaceCoreAsync(IPlaceholderReplacementContext context)
        {
            bool hasChanges;

            using (this.InitDocument())
            {
                if (this.WithExtensions)
                {
                    await this.BeforeDocumentReplaceAsync(context);
                }

                bool hasFieldChanges = await this.ReplaceFieldPlaceholdersAsync(context);
                bool hasTableChanges = await this.ReplaceTablePlaceholdersAsync(context);

                hasChanges = hasFieldChanges || hasTableChanges;

                if (hasChanges)
                {
                    this.CleanAfterChanges();
                }

                this.PrepareDocumentForSave();

                if (this.WithExtensions)
                {
                    await this.AfterDocumentReplaceAsync(context);
                }

                this.SaveDocument();
            }

            if (hasChanges)
            {
                await this.OnChangedAsync(cancellationToken: context.CancellationToken);
            }
        }

        #endregion
    }
}
