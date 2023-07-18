using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Threading;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Placeholders.Extensions;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Контекст обработки расширений <see cref="IPlaceholderReplaceExtension"/> в Excel документах
    /// </summary>
    public sealed class ExcelPlaceholderReplaceExtensionContext : OpenXmlPlaceholderReplaceExtensionContext
    {
        #region Constructors

        public ExcelPlaceholderReplaceExtensionContext(
            IPlaceholderReplacementContext replacementContext,
            CancellationToken cancellationToken = default)
            : base(replacementContext, cancellationToken)
        {
            this.TableElements = new List<Row>();
            this.RowElements = new List<Row>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Текущий документ. Доступен в любом расширении, но структуру документа
        /// рекомендуется изменять только в <see cref="IPlaceholderReplaceExtension.AfterDocumentReplace(IPlaceholderReplaceExtensionContext)"/>
        /// </summary>
        public SpreadsheetDocument Document { get; set; }

        /// <summary>
        /// Текущий лист в Excel, в котором производится замена плейсхолдеров.
        /// Доступен во всех методах обработки таблиц, строк и плейсхолдеров, если замена значения производится в листе.
        /// </summary>
        public Worksheet Worksheet { get; set; }

        /// <summary>
        /// Набор всех строк, добавленных в таблицу. Заполняется по мере генерации этих строк.
        /// Полностью заполнен только в методе <see cref="IPlaceholderReplaceExtension.AfterTableReplace(IPlaceholderReplaceExtensionContext)"/>
        /// </summary>
        public List<Row> TableElements { get; set; }

        /// <summary>
        /// Набор всех строк, определяющих строку с плейсхолдерами. Заполняется по мере генерации строки.
        /// Полностью заполнен только в методе <see cref="IPlaceholderReplaceExtension.AfterRowReplace(IPlaceholderReplaceExtensionContext)"/>
        /// </summary>
        public List<Row> RowElements { get; set; }

        /// <summary>
        /// Текущая обрабатываемая строка. Доступна в методах обработки плейсхолдера.
        /// </summary>
        public Row CurrentRowElement { get; set; }

        /// <summary>
        /// Текущая обрабатываеммая ячейка. Доступна в методах обработки плейсхолдеров.
        /// </summary>
        public Cell Cell { get; set; }

        /// <summary>
        /// Элемент, в котором фактически производится замена плейсхолдера. Может отличаться от <see cref="Cell"/>,
        /// т.к. значение ячейки в Excel может быть записано, например, в <see cref="SharedStringItem"/>
        /// </summary>
        public OpenXmlElement PlaceholderElement { get; set; }

        /// <summary>
        /// Элемент, хранящий замененный текст.
        /// Может быть <see cref="CellValue"/>, <see cref="Text"/> или <see cref="DocumentFormat.OpenXml.Drawing.Text"/>
        /// </summary>
        public OpenXmlLeafTextElement PlaceholderTextElement { get; set; }

        #endregion
    }
}
