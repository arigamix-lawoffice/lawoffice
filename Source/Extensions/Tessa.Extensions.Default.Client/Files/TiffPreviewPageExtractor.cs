using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.UI;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Files
{
    /// <summary>
    /// Объект, выполняющий извлечение страницы для предпросмотра из многостраничного документа TIFF.
    /// </summary>
    public sealed class TiffPreviewPageExtractor :
        IPreviewPageExtractor
    {
        #region Private Methods

        private static Bitmap ResizeImage(Image image, int width, int height, PreviewPageQuality quality)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;

            switch (quality)
            {
                case PreviewPageQuality.Low:
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    break;

                case PreviewPageQuality.Normal:
                    graphics.CompositingQuality = CompositingQuality.Default;
                    graphics.InterpolationMode = InterpolationMode.Default;
                    graphics.SmoothingMode = SmoothingMode.Default;
                    graphics.PixelOffsetMode = PixelOffsetMode.Default;
                    break;

                case PreviewPageQuality.High:
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(quality), quality, null);
            }


            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return destImage;
        }

        #endregion

        #region IPreviewPageExtractor Members

        /// <summary>
        /// Выполняет извлечение страницы для предпросмотра.
        /// </summary>
        /// <param name="context">
        /// Контекст, содержащий параметры извлечения. В этот объект должен быть записан результат извлечения.
        /// </param>
        public async ValueTask ExtractPageAsync(IPreviewPageExtractorContext context)
        {
            // используем файл в формате Bmp, т.к. методом случайного научного выбора было определено, что это быстрее всего
            Image image = Image.FromFile(context.FilePath);
            // если необходимо, поворачиваем изображение в соответствии с указанной в свойствах ориентацией снимка.
            image.TransformForOrientation();
            try
            {
                context.ResultPageCount = image.GetFrameCount(FrameDimension.Page);
                image.SelectActiveFrame(FrameDimension.Page, context.PageIndex);

                var coerceRenderSizeFuncAsync = context.CoerceRenderSizeFuncAsync;
                if (coerceRenderSizeFuncAsync is not null)
                {
                    int width = image.Width;
                    int height = image.Height;

                    (int newWidth, int newHeight) = await coerceRenderSizeFuncAsync((width, height), context.CancellationToken).ConfigureAwait(false);
                    if (newWidth != width || newHeight != height)
                    {
                        Bitmap resizedImage = ResizeImage(image, newWidth, newHeight, context.Quality);
                        image.Dispose();

                        image = resizedImage;
                    }
                }

                // по умолчанию используем Stream размером меньше 80 Кб
                var createDestinationStreamFuncAsync = context.CreateDestinationStreamFuncAsync;
                await using var targetStream = createDestinationStreamFuncAsync is not null
                    ? await createDestinationStreamFuncAsync(context) ?? StreamHelper.AcquireMemoryStream()
                    : StreamHelper.AcquireMemoryStream();

                image.Save(targetStream, ImageFormat.Png);
                image.Dispose();

                context.ResultImageBytes = (targetStream as MemoryStream)?.ToArray();
            }
            finally
            {
                image.Dispose();
            }
        }

        #endregion
    }
}
