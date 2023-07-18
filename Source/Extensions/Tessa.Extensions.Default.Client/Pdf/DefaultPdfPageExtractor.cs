using System;
using System.Threading;
using System.Threading.Tasks;
using PDFiumSharp;
using Tessa.Extensions.Platform.Client.Scanning;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Объект, выполняющий разбор файла PDF на страницы с изображениями PNG.
    /// Выбор подходящей библиотеки (Pdfium или PdfSharp) определяется автоматически.
    /// </summary>
    public class DefaultPdfPageExtractor :
        IPdfPageExtractor
    {
        // любые зависимости Unity можно получить через конструктор

        #region Constructors

        public DefaultPdfPageExtractor(
            Func<PdfiumPageExtractor> getPdfiumFunc,
            Func<PdfSharpPageExtractor> getPdfSharpFunc)
        {
            this.getPdfiumFunc = getPdfiumFunc;
            this.getPdfSharpFunc = getPdfSharpFunc;
        }

        #endregion

        #region Fields

        private readonly Func<PdfiumPageExtractor> getPdfiumFunc;

        private readonly Func<PdfSharpPageExtractor> getPdfSharpFunc;

        #endregion

        #region IPdfPageExtractor Members

        /// <summary>
        /// Выполняет извлечение страниц PDF с изображениями PNG.
        /// </summary>
        /// <param name="context">Контекст операции по разбору файла PDF на страницы.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual async Task ExtractPagesAsync(IPdfPageExtractorContext context, CancellationToken cancellationToken = default)
        {
            string pdfAuthor;
            using (var document = new PdfDocument(context.PdfFilePath))
            {
                var metadata = new PdfiumDocumentMetadata(document);
                pdfAuthor = metadata.Author;
            }

            if (string.Equals(pdfAuthor, PdfHelper.TessaGeneratedPdfAuthor, StringComparison.Ordinal))
            {
                // это документ, сгенерированный в Tessa, рендерим его через PdfSharp
                PdfSharpPageExtractor pdfSharp = this.getPdfSharpFunc();
                await pdfSharp.ExtractPagesAsync(context, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                // это не документ, сгенерированный в Tessa, рендерим его через Pdfium
                PdfiumPageExtractor pdfium = this.getPdfiumFunc();
                await pdfium.ExtractPagesAsync(context, cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
