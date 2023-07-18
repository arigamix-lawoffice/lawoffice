using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Tessa.Extensions.Platform.Client.Scanning;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Объект, выполняющий формирование файла PDF из страниц с изображениями PNG.
    /// По умолчанию используется библиотека PdfSharp.
    /// </summary>
    public class DefaultPdfGenerator :
        IPdfGenerator
    {
        // любые зависимости Unity можно получить через конструктор

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей из Unity.
        /// </summary>
        /// <param name="extensionContainer">Контейнер с расширениями для выполнения расширений на штампы.</param>
        public DefaultPdfGenerator(IExtensionContainer extensionContainer) =>
            this.extensionContainer = extensionContainer;

        #endregion

        #region Fields

        private readonly IExtensionContainer extensionContainer;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создаёт объект <see cref="PdfStampWriter"/>, используемый для наложения штампа.
        /// </summary>
        /// <param name="context">Контекст операции по разбору файла PDF на страницы.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Созданный объект <see cref="PdfStampWriter"/>.</returns>
        protected virtual ValueTask<PdfStampWriter> CreateStampWriterAsync(
            IPdfGeneratorContext context,
            CancellationToken cancellationToken = default) =>
            new ValueTask<PdfStampWriter>(new PdfStampWriter());

        #endregion

        #region IPdfGenerator Members

        /// <summary>
        /// Формирует документ PDF в виде <see cref="ScanDocument"/>.
        /// Возвращает <c>null</c>, если формирование документа не удалось.
        /// </summary>
        /// <param name="context">Контекст операции по разбору файла PDF на страницы.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Объект, содержащий сформированный документ PDF, или <c>null</c>, если формирование документа не удалось.
        /// </returns>
        public virtual async Task<ScanDocument> TryGenerateAsync(IPdfGeneratorContext context, CancellationToken cancellationToken = default)
        {
            IFileControl fileControl = context.ExternalContext is IFileExtensionContextBase controlContext ? controlContext.Control : null;

            // для PDF на входе нужен файл формата JPEG, который был создан через GDI+ (для успешного ресайза на странице);
            // для экономии памяти (существенной) мы сначала всё перекодируем в JPEG, а затем сгенерим из всех JPEG-ов файл PDF

            // считаем, что в среднем на кодирование JPEG уходит jpegPercentage % времени, а на генерацию PDF - оставшиеся pdfPercentage %
            const double jpegPercentage = 33.3;
            const double pdfPercentage = 100.0 - jpegPercentage;

            int pageCount = context.PngPageFiles.Count;

            ITempFolder folder = TempFile.AcquireFolder();
            context.TemporaryFolders.Add(folder);

            int[] processed = { 0 };
            var results = new ConcurrentBag<(ITempFile jpegFile, int order)>();

            await context.PngPageFiles
                .Select((pngFile, order) => new { PngFile = pngFile, Order = order })
                .RunWithMaxDegreeOfParallelismAsync(
                    UIHelper.MaxImageProcessingParallelThreads,
                    async (x, ct) =>
                    {
                        // мы не сможем освободить временный файл, пока не будет освобождён document, а также выполнена полная сборка мусора
                        string jpegName = Path.GetFileNameWithoutExtension(x.PngFile.Name) + "." + x.Order + ".jpg";
                        ITempFile jpegFile = folder.AcquireFile(jpegName);

                        await ScanDocumentHelper.EncodeInJpegAsync(x.PngFile.Path, jpegFile.Path);

                        int index = Interlocked.Increment(ref processed[0]) - 1; // постикремент
                        await context.ReportProgressAsync(jpegPercentage * index / pageCount, ct).ConfigureAwait(false);

                        results.Add((jpegFile, x.Order));
                    },
                    cancellationToken).ConfigureAwait(false);

            ITempFile[] jpegFiles = results.OrderBy(x => x.order).Select(x => x.jpegFile).ToArray();

            // из-за генерации JPEG памяти могло быть занято много;
            // поэтому дефрагментируем Large Object Heap, чтобы уменьшить шансы на OutOfMemoryException при генерации PDF
            GCHelper.CollectAll(compactLargeObjectHeap: true);

            // здесь начинается формирование документа из созданных страниц JPEG
            PdfDocument document = null;
            IExtensionExecutor stampExecutor = null;

            try
            {
                document = new PdfDocument();
                document.Info.Author = PdfHelper.TessaGeneratedPdfAuthor;

                PdfStampExtensionContext stampContext = null;

                // если требуется штамп и мы выполняемся в UIContext карточки, то выполняем расширения на штампы
                ICardEditorModel editor;
                ICardModel model;
                if (context.AddStamp
                    && (editor = UIContext.Current.CardEditor) != null
                    && (model = editor.CardModel) != null)
                {
                    PdfStampWriter stampWriter = await this.CreateStampWriterAsync(context, cancellationToken).ConfigureAwait(false)
                        ?? new PdfStampWriter();

                    stampContext = new PdfStampExtensionContext(
                        editor,
                        model,
                        model.Card,
                        model.FileContainer,
                        fileControl,
                        context.PngPageFiles.Count,
                        context,
                        document,
                        stampWriter,
                        cancellationToken);

                    stampContext.GeneratorInfo.SetStorage(context.GeneratorInfo.GetStorage());
                    stampContext.GeneratorInfo.Seal();

                    stampContext.GeneratorContextInfo.SetStorage(context.Info.GetStorage());
                    stampContext.GeneratorContextInfo.Seal();

                    stampExecutor = await this.extensionContainer.ResolveExecutorAsync<IPdfStampExtension>(cancellationToken).ConfigureAwait(false);
                    await stampExecutor.ExecuteAsync(nameof(IPdfStampExtension.OnGenerationStarted), stampContext).ConfigureAwait(false);
                }

                // формируем каждую страницу
                for (int i = 0; i < context.PngPageFiles.Count; i++)
                {
                    await context.ReportProgressAsync(jpegPercentage + pdfPercentage * i / context.PngPageFiles.Count, cancellationToken).ConfigureAwait(false);

                    PdfPage page = document.AddPage();

                    XImage image = null;
                    XGraphics graphics = null;

                    try
                    {
                        image = XImage.FromFile(jpegFiles[i].Path);

                        // изображение со страницей требуется пропорционально пожать для PDF
                        double pageRatio = page.Height.Value / page.Width.Value;
                        double imageRatio = image.PointHeight / image.PointWidth;

                        double width;
                        double height;
                        if (imageRatio <= pageRatio)
                        {
                            width = page.Width.Value;
                            height = page.Width.Value * imageRatio;
                        }
                        else
                        {
                            width = page.Height.Value / imageRatio;
                            height = page.Height.Value;
                        }

                        // выводим JPEG на страницу
                        graphics = XGraphics.FromPdfPage(page);
                        graphics.DrawImage(image, 0.0, 0.0, width, height);

                        // выводим штамп, если требуется
                        if (stampContext != null)
                        {
                            stampContext.SetPage(page, graphics, i + 1);
                            await stampExecutor.ExecuteAsync(nameof(IPdfStampExtension.GenerateForPage), stampContext).ConfigureAwait(false);

                            stampContext.StampWriter.Draw(graphics);
                        }
                    }
                    finally
                    {
                        graphics?.Dispose();
                        image?.Dispose();
                    }

                    page.Close();
                }

                if (stampContext != null)
                {
                    stampContext.ClearPage();
                    await stampExecutor.ExecuteAsync(nameof(IPdfStampExtension.OnGenerationEnded), stampContext).ConfigureAwait(false);
                }

                // документ сформирован
                ScanDocument result = new PdfScanDocument(document);
                document = null;
                return result;
            }
            finally
            {
                if (stampExecutor != null)
                {
                    await stampExecutor.DisposeAsync().ConfigureAwait(false);
                }

                // ReSharper disable once ConstantConditionalAccessQualifier
                document?.Dispose();
            }
        }

        #endregion
    }
}
