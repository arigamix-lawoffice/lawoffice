using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Files
{
    /// <summary>
    /// Объект, выполняющий извлечение страницы для предпросмотра из многостраничного документа,
    /// тип которого определяется автоматически по расширению.
    /// </summary>
    public sealed class DefaultPreviewPageExtractor :
        IPreviewPageExtractor
    {
        #region Constructors

        public DefaultPreviewPageExtractor(
            Func<PdfPreviewPageExtractor> getPdfFunc,
            Func<TiffPreviewPageExtractor> getTiffFunc)
        {
            this.getPdfFunc = getPdfFunc ?? throw new ArgumentNullException(nameof(getPdfFunc));
            this.getTiffFunc = getTiffFunc ?? throw new ArgumentNullException(nameof(getTiffFunc));
        }

        #endregion

        #region Fields

        private readonly Func<PdfPreviewPageExtractor> getPdfFunc;

        private readonly Func<TiffPreviewPageExtractor> getTiffFunc;

        #endregion

        #region IPreviewPageExtractor Members

        /// <summary>
        /// Выполняет извлечение страницы для предпросмотра.
        /// </summary>
        /// <param name="context">
        /// Контекст, содержащий параметры извлечения. В этот объект должен быть записан результат извлечения.
        /// </param>
        public ValueTask ExtractPageAsync(IPreviewPageExtractorContext context)
        {
            string extension = Path.GetExtension(context.FilePath);
            if (string.IsNullOrEmpty(extension))
            {
                throw new InvalidOperationException(
                    "File without extension can't be previewed by " + this.GetType().FullName);
            }

            extension = extension.ToLowerInvariant();

            return extension switch
            {
                ".pdf" => this.getPdfFunc().ExtractPageAsync(context),
                ".tif" => this.getTiffFunc().ExtractPageAsync(context),
                ".tiff" => this.getTiffFunc().ExtractPageAsync(context),
                _ => throw new InvalidOperationException($"File with extension + {extension} can't be previewed by {this.GetType().FullName}")
            };
        }

        #endregion
    }
}
