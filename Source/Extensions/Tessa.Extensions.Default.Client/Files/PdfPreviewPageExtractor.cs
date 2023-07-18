using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using PDFiumSharp;
using PDFiumSharp.Enums;
using Tessa.Platform.IO;
using Tessa.UI;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Files
{
    /// <summary>
    /// Объект, выполняющий извлечение страницы для предпросмотра из многостраничного документа PDF.
    /// </summary>
    public sealed class PdfPreviewPageExtractor :
        IPreviewPageExtractor
    {
        // любые зависимости Unity можно получить через конструктор

        #region IPreviewPageExtractor Members

        /// <summary>
        /// Выполняет извлечение страницы для предпросмотра.
        /// </summary>
        /// <param name="context">
        /// Контекст, содержащий параметры извлечения. В этот объект должен быть записан результат извлечения.
        /// </param>
        public async ValueTask ExtractPageAsync(IPreviewPageExtractorContext context)
        {
            byte[] imageBytes;
            using (var document = new PdfDocument(context.FilePath))
            {
                context.ResultPageCount = document.Pages.Count;

                const int dpiTarget = 150;
                const double pdfDpiDefault = 72d;

                double systemDpiX = DpiHelpers.DeviceDpiX;
                double cX = systemDpiX / pdfDpiDefault;
                int dpiX = (int)(dpiTarget * cX);
                double factorX = dpiX / systemDpiX;

                double systemDpiY = DpiHelpers.DeviceDpiY;
                double cY = systemDpiY / pdfDpiDefault;
                int dpiY = (int)(dpiTarget * cY);
                double factorY = dpiY / systemDpiY;

                var currentPage = document.Pages[context.PageIndex];
                (double pageWidth, double pageHeight) = currentPage.Size;

                int renderWidthPixels = (int)Math.Round(pageWidth * factorX, 0, MidpointRounding.AwayFromZero);
                int renderHeightPixels = (int)Math.Round(pageHeight * factorY, 0, MidpointRounding.AwayFromZero);

                int width;
                int height;

                var coerceRenderSizeFuncAsync = context.CoerceRenderSizeFuncAsync;
                if (coerceRenderSizeFuncAsync is not null)
                {
                    (width, height) = await coerceRenderSizeFuncAsync((renderWidthPixels, renderHeightPixels), context.CancellationToken).ConfigureAwait(false);
                }
                else
                {
                    switch (context.Quality)
                    {
                        case PreviewPageQuality.Low:
                            width = renderWidthPixels / 2;
                            height = renderHeightPixels / 2;
                            break;

                        case PreviewPageQuality.Normal:
                            width = renderWidthPixels;
                            height = renderHeightPixels;
                            break;

                        case PreviewPageQuality.High:
                            width = renderWidthPixels * 2;
                            height = renderHeightPixels * 2;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(PreviewPageQuality));
                    }
                }

                var encodeBitmapStreamFuncAsync = context.EncodeBitmapStreamFuncAsync;
                var createDestinationStreamFuncAsync = context.CreateDestinationStreamFuncAsync;
                await using var targetStream =
                    encodeBitmapStreamFuncAsync is not null
                        ? null
                        : createDestinationStreamFuncAsync is not null
                            ? await createDestinationStreamFuncAsync(context) ?? StreamHelper.AcquireMemoryStream()
                            : StreamHelper.AcquireMemoryStream();
                {
                    using var bitmap = new PDFiumBitmap(width, height, true);
                    bitmap.Fill(bitmap.Format != BitmapFormats.BGRx ? 0xFFFFFFFF : 0x00FFFFFF);
                    currentPage.Render(bitmap, flags: RenderingFlags.Annotations | RenderingFlags.LcdText);
                    await using Stream bmpStream = bitmap.AsBmpStream(dpiX, dpiY);

                    if (encodeBitmapStreamFuncAsync is null)
                    {
                        using var bmpBitmap = new Bitmap(bmpStream);
                        bmpBitmap.Save(targetStream, ImageFormat.Png);
                    }
                    else
                    {
                        await encodeBitmapStreamFuncAsync(bmpStream, context).ConfigureAwait(false);
                    }
                }

                imageBytes = (targetStream as MemoryStream)?.ToArray();
            }

            if (imageBytes is not null)
            {
                context.ResultImageBytes = imageBytes;
            }
        }

        #endregion
    }
}
