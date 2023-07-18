using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Threading;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Placeholders.Extensions;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Контекст обработки расширений <see cref="IPlaceholderReplaceExtension"/> в Word документах
    /// </summary>
    public sealed class WordPlaceholderReplaceExtensionContext : OpenXmlPlaceholderReplaceExtensionContext
    {
        #region Constructors

        public WordPlaceholderReplaceExtensionContext(
            IPlaceholderReplacementContext replacementContext,
            CancellationToken cancellationToken = default)
            :base(replacementContext, cancellationToken)
        {
            this.PlaceholderElements = new List<Run>();
            this.RowElements = new List<OpenXmlElement>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Текущий документ. Доступен в любом расширении, но структуру документа
        /// рекомендуется изменять только в <see cref="IPlaceholderReplaceExtension.AfterDocumentReplace(IPlaceholderReplaceExtensionContext)"/>
        /// </summary>
        public WordprocessingDocument Document { get; set; }

        /// <summary>
        /// Текущая обрабатываемая таблица. Доступна в методах обработки таблиц, строк и плейсхолдеров внутри строк.
        /// Может иметь тип <see cref="Table"/> для таблиц или любой другой тип, если используется перечисление.
        /// В виду особенностей Word, перечесление может быть внутри различных элементов, при том не все элементы будут к нему относится.
        /// </summary>
        public OpenXmlElement TableElement { get; set; }

        /// <summary>
        /// Набор элементов обрабатываемой строки. Доступен в методах обработки строк и плейсхолдеров внутри строк.
        /// Может иметь объекты типов <see cref="TableRow"/> для строк таблиц, <see cref="Paragraph"/> для строк перечисления и для кастомных строк, которые оперируют всем параграфом,
        /// или <see cref="Run"/> для кастомных строк, которые находятся внутри параграфа.
        /// </summary>
        public List<OpenXmlElement> RowElements { get; }

        /// <summary>
        /// Текущий элемент обрабатываемой строки. Доступен в методах обработки плейсхолдеров внутри строк.
        /// Может иметь тип <see cref="TableRow"/> для строк таблиц, <see cref="Paragraph"/> для строк перечисления и для кастомных строк.
        /// </summary>
        public OpenXmlElement CurrentRowElement { get; set; }

        /// <summary>
        /// Список элементов, в которых произошла замена плейсхолдера. Доступен в методах замены плейсхолдеров.
        /// Элементов может быть несколько, т.к. перенос строк в Word сохраняется как отдельный объект.
        /// Каждый элемент является <see cref="Run"/> с <see cref="Text"/> или <see cref="Break"/> внутри.
        /// </summary>
        public List<Run> PlaceholderElements { get; }

        #endregion
    }
}
