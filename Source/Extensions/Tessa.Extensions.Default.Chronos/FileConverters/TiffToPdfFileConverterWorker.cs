using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BitMiracle.LibTiff.Classic;
using NLog;
using Tessa.FileConverters;
using Tessa.PdfSharp.Drawing;
using Tessa.PdfSharp.Pdf;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;

#pragma warning disable 1584,1711,1572,1581,1580

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Объект, ответственный за преобразование файла в формат <see cref="FileConverterFormat.Pdf"/>
    /// из формата TIFF. Обычно не используется как самостоятельный Worker, а применяется в составе <see cref="PdfFileConverterWorker"/>.
    /// Регистрация выполняется по константе <see cref="FileConverterWorkerNames.TiffToPdf"/>.
    /// </summary>
    /// <remarks>
    /// Наследники класса могут переопределять методы интерфейса, например, добавив к ним обработку файлов других форматов.
    /// Класс может также реализовывать <see cref="IAsyncDisposable"/> для очистки ресурсов,
    /// для этого в наследнике переопределяется метод <see cref="Dispose"/> и вызывается сначала его базовая реализация.
    /// </remarks>
    public class TiffToPdfFileConverterWorker :
        IFileConverterWorker,
        IAsyncDisposable
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Private Methods

        private static async Task ConvertFileCoreAsync(string inputFilePath, string outputFilePath)
        {
            // определяем количество страниц и подтверждаем, что файл вообще существует
            int pageCount;
            using (Tiff tiff = Tiff.Open(inputFilePath, "r"))
            {
                pageCount = tiff.NumberOfDirectories();
            }

            if (pageCount <= 0)
            {
                return;
            }

            int maxDegreeOfParallelism = RuntimeHelper.MaxServerRecommendedParallelThreads;
            logger.Trace("Tiff file has {0} pages. Preparing for conversion, using {1} max threads...", pageCount, maxDegreeOfParallelism);

            using (ITempFolder folder = TempFile.AcquireFolder())
            {
                var pageFilePathArray = new string[pageCount];

                if (pageCount == 1)
                {
                    // одностраничный TIFF можно добавить в PDF as-is, нет нужды его разбивать на страницы
                    pageFilePathArray[0] = inputFilePath;
                }
                else
                {
                    var tiffFiles = new ITempFile[pageCount];
                    for (int i = 0; i < tiffFiles.Length; i++)
                    {
                        ITempFile file = folder.AcquireFile(i + ".tif");
                        tiffFiles[i] = file;
                    }

                    var input = new ConcurrentQueue<int>(Enumerable.Range(0, pageCount));

                    Task[] tasks = Enumerable
                        .Range(1, maxDegreeOfParallelism)
                        .Select(async _ =>
                        {
                            while (input.TryDequeue(out int pageIndex))
                            {
                                ITempFile file = tiffFiles[pageIndex];

                                Image image = null;
                                Tiff tiff = null;

                                try
                                {
                                    tiff = Tiff.Open(inputFilePath, "r");
                                    tiff.SetDirectory((short) pageIndex);

                                    image = GetBitmapFromTiff(tiff);

                                    tiff.Dispose();
                                    tiff = null;

                                    string path = file.Path;
                                    image.Save(path, ImageFormat.Tiff);

                                    // если где-то выше была ошибка, то в соответствующей позиции в pageFilePathArray будет null;
                                    // при рендеринге такая страница оставляется пустой
                                    pageFilePathArray[pageIndex] = path;
                                }
                                catch (Exception ex)
                                {
                                    if (logger.IsErrorEnabled)
                                    {
                                        logger.LogException($"Skipping Tiff page {pageIndex + 1}/{pageCount}. Error occured when extracting page:", ex);
                                    }
                                }
                                finally
                                {
                                    tiff?.Dispose();
                                    image?.Dispose();
                                }
                            }
                        })
                        .ToArray();

                    await Task.WhenAll(tasks);
                }

                logger.Trace("Tiff pages are prepared. Building PDF file...");
                GeneratePdf(pageFilePathArray, outputFilePath);

                // при выходе из using-а будет удалён folder со всеми созданными файлами
            }

            logger.Trace("PDF file is generated from Tiff.");
        }


        private static Bitmap GetBitmapFromTiff(Tiff tiff)
        {
            FieldValue[] value = tiff.GetField(TiffTag.IMAGEWIDTH);
            int width = value[0].ToInt();

            value = tiff.GetField(TiffTag.IMAGELENGTH);
            int height = value[0].ToInt();

            //Read the image into the memory buffer
            int[] raster = new int[height * width];
            if (!tiff.ReadRGBAImage(width, height, raster))
            {
                return null;
            }

            Bitmap bmp = null;

            try
            {
                bmp = new Bitmap(width, height, PixelFormat.Format32bppRgb);

                var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                byte[] bits = new byte[bmpdata.Stride * bmpdata.Height];

                for (int y = 0; y < bmp.Height; y++)
                {
                    int rasterOffset = y * bmp.Width;
                    int bitsOffset = (bmp.Height - y - 1) * bmpdata.Stride;

                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int rgba = raster[rasterOffset++];
                        bits[bitsOffset++] = (byte) ((rgba >> 16) & 0xff);
                        bits[bitsOffset++] = (byte) ((rgba >> 8) & 0xff);
                        bits[bitsOffset++] = (byte) (rgba & 0xff);
                        bits[bitsOffset++] = (byte) ((rgba >> 24) & 0xff);
                    }
                }

                Marshal.Copy(bits, 0, bmpdata.Scan0, bits.Length);
                bmp.UnlockBits(bmpdata);

                Bitmap result = bmp;
                bmp = null;
                return result;
            }
            finally
            {
                bmp?.Dispose();
            }
        }


        /// <summary>
        /// Получение pdf из набора картинок
        /// </summary>
        /// <param name="pageFilePathArray">
        /// Массив путей для файлов, в каждом из которых содержится ровно одна страница исходного многостраничного TIFF.
        /// В массиве могут встречаться <c>null</c> - значит на этой странице ошибка, тогда мы оставляем её пустой.
        /// </param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        /// <remarks>
        /// Похожий алгоритм есть в <see cref="DefaultPdfGenerator.TryGenerate"/>.
        /// </remarks>
        private static void GeneratePdf(string[] pageFilePathArray, string outputFilePath)
        {
            // для PDF на входе нужен файл формата JPEG, который был создан через GDI+ (для успешного ресайза на странице);
            // для экономии памяти (существенной) мы сначала всё перекодируем в JPEG, а затем сгенерим из всех JPEG-ов файл PDF

            PdfDocument document = null;

            // поэтому дефрагментируем Large Object Heap, чтобы уменьшить шансы на OutOfMemoryException при генерации PDF
            GCHelper.CollectAll(compactLargeObjectHeap: true);

            try
            {
                // здесь начинается формирование документа из созданных страниц JPEG
                document = new PdfDocument();

                // формируем каждую страницу
                for (int i = 0; i < pageFilePathArray.Length; i++)
                {
                    PdfPage page = document.AddPage();

                    if (pageFilePathArray[i] != null)
                    {
                        XImage image = null;
                        XGraphics graphics = null;

                        try
                        {
                            image = XImage.FromFile(pageFilePathArray[i]);

                            // сохраняем изначальный размер страницы
                            double width = image.PointWidth;
                            double height = image.PointHeight;

                            page.Width = width;
                            page.Height = height;

                            // выводим JPEG на страницу
                            graphics = XGraphics.FromPdfPage(page);
                            graphics.DrawImage(image, 0.0, 0.0, width, height);
                        }
                        finally
                        {
                            if (graphics != null)
                            {
                                graphics.Dispose();
                            }

                            if (image != null)
                            {
                                image.Dispose();
                            }
                        }
                    }

                    page.Close();
                }

                // документ сформирован
                document.Save(outputFilePath);

                document.Dispose();
                document = null;

                // если не вызвать сборку мусора, то временные файлы нельзя удалить из-за PdfSharp, который их блокирует
                GCHelper.CollectAll(compactLargeObjectHeap: true);
            }
            finally
            {
                document?.Dispose();
            }
        }

        #endregion

        #region IFileConverterWorker Members

        /// <summary>
        /// Преобразует файл в заданный формат.
        /// </summary>
        /// <param name="context">Контекст, содержащий информацию по выполняемому преобразованию.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public virtual async Task ConvertFileAsync(IFileConverterContext context, CancellationToken cancellationToken = default)
        {
            // конвертируем файл
            await ConvertFileCoreAsync(context.InputFilePath, context.OutputFilePath);

            // пишем ключ, через который вызывающая сторона поймёт, что конвертация была выполнена через наш конвертер
            context.ResponseInfo[FileConverterWorkerNames.TiffToPdf] = BooleanBoxes.True;
        }


        /// <summary>
        /// Выполняет обработку перед запуском цикла обслуживания для очереди на конвертацию файлов.
        /// Метод запускается единственный раз при старте сервиса конвертации.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <remarks>
        /// Реализация по умолчанию не выполняет действий.
        /// </remarks>
        public virtual Task PreprocessAsync(CancellationToken cancellationToken = default)
        {
            // does nothing by default
            return Task.CompletedTask;
        }


        /// <summary>
        /// Выполняет обработку в процессе выполнения цикла обслуживания для очереди на конвертацию файлов.
        /// Метод запускается множество раз внутри цикла с периодичностью, заданной в конфигурационном файле.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <remarks>
        /// Реализация по умолчанию не выполняет действий.
        /// </remarks>
        public virtual Task PerformMaintenanceAsync(CancellationToken cancellationToken = default)
        {
            // does nothing by default
            return Task.CompletedTask;
        }

        #endregion

        #region IAsyncDisposable Members

        /// <summary>
        /// Освобождение занятых ресурсов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>
        /// Реализация по умолчанию не выполняет действий.
        /// </remarks>
        public virtual ValueTask DisposeAsync() => new ValueTask();

        #endregion
    }
}