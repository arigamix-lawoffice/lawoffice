using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Filters;
using PdfSharp.Pdf.IO;
using Tessa.Extensions.Platform.Client.Scanning;
using Tessa.Platform.IO;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Объект, выполняющий разбор файла PDF на страницы с изображениями PNG посредством библиотеки PdfSharp.
    /// </summary>
    public class PdfSharpPageExtractor :
        IPdfPageExtractor
    {
        // любые зависимости Unity можно получить через конструктор

        #region IPdfPageExtractor Members

        /// <summary>
        /// Выполняет извлечение страниц PDF с изображениями PNG.
        /// </summary>
        /// <param name="context">Контекст операции по разбору файла PDF на страницы.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual async Task ExtractPagesAsync(IPdfPageExtractorContext context, CancellationToken cancellationToken = default)
        {
            using PdfDocument document = PdfReader.Open(context.PdfFilePath, PdfDocumentOpenMode.Import);
            int pageCount = document.PageCount;

            int fileIndex = 0;  // номер картинки, выгруженной в файл
            int pageIndex = 0;  // номер страницы, отображаемый на сплэше

            FlateDecode decoder = null;
            ITempFolder folder = TempFile.AcquireFolder();

            foreach (PdfPage page in document.Pages)
            {
                await context.ReportProgressAsync(100.0 * pageIndex / pageCount, cancellationToken).ConfigureAwait(false);

                pageIndex++;

                PdfDictionary resources = page.Elements.GetDictionary("/Resources");
                if (resources != null)
                {
                    PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");
                    if (xObjects != null)
                    {
                        ICollection<PdfItem> items = xObjects.Elements.Values;
                        foreach (PdfItem item in items)
                        {
                            if (item is PdfReference reference)
                            {
                                PdfDictionary.DictionaryElements xObjectElements;
                                if (reference.Value is PdfDictionary xObject
                                    && (xObjectElements = xObject.Elements).GetString("/Subtype") == "/Image")
                                {
                                    PdfArray filters = xObjectElements.GetArray("/Filter");

                                    // ReSharper disable SuspiciousTypeConversion.Global
                                    if (filters != null
                                        && filters.Elements.Items.Any(x => x.Equals("/DCTDecode"))
                                        || xObjectElements.GetString("/Filter") == "/DCTDecode")
                                    {
                                        // это JPEG
                                        byte[] jpegData = xObject.Stream.Value;

                                        if (filters != null && filters.Elements.Items.Any(x => x.Equals("/FlateDecode")))
                                        {
                                            // это зазипованный JPEG, сначала извлекаем оригинал
                                            if (decoder == null)
                                            {
                                                decoder = new FlateDecode();
                                            }

                                            jpegData = decoder.Decode(jpegData);
                                        }

                                        ITempFile tempFile = folder.AcquireFile(ScanDocumentHelper.GetPageFileName(fileIndex));
                                        context.PngPageFiles.Add(tempFile);

                                        // фактически выгруженные картинки должны нумероваться по порядку
                                        fileIndex++;

                                        await using MemoryStream stream = new MemoryStream(jpegData);
                                        using Image image = Image.FromStream(stream);
                                        image.Save(tempFile.Path, ImageFormat.Png);

                                        // извлекаем не более одной картинки на страницу
                                        break;
                                    }

                                    // ReSharper restore SuspiciousTypeConversion.Global
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
