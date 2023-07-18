using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.OnlyOffice;
using Tessa.FileConverters;
using Tessa.Json;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Объект, ответственный за преобразование файла в формат <see cref="FileConverterFormat.Pdf"/>
    /// посредством внешней программы OnlyOffice Document Builder
    /// </summary>
    /// <remarks>
    /// Наследники класса могут переопределять методы интерфейса, например, добавив к ним обработку файлов других форматов.
    /// Класс может также реализовывать <see cref="IAsyncDisposable"/> для очистки ресурсов,
    /// для этого в наследнике переопределяется метод <see cref="DisposeAsync"/> и вызывается сначала его базовая реализация.
    /// </remarks>
    public class OnlyOfficeServiceConverter :
        IFileConverterWorker,
        IAsyncDisposable
    {
        #region Fields

        private readonly IOnlyOfficeSettingsProvider settingsProvider;
        
        private readonly IOnlyOfficeService onlyOfficeService;
        
        private readonly ICardStreamServerRepository streamServerRepositoryExt;
        
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructor

        public OnlyOfficeServiceConverter(
            IOnlyOfficeSettingsProvider settingsProvider,
            IOnlyOfficeService onlyOfficeService,
            ICardStreamServerRepository streamServerRepositoryExt)
        {
            this.settingsProvider = NotNullOrThrow(settingsProvider);
            this.onlyOfficeService = NotNullOrThrow(onlyOfficeService);
            this.streamServerRepositoryExt = NotNullOrThrow(streamServerRepositoryExt);
        }

        #endregion

        #region IFileConverterWorker members

        public async Task ConvertFileAsync(IFileConverterContext context, CancellationToken cancellationToken = default)
        {
            logger.Trace("Start converting");

            var settings = await this.settingsProvider.GetSettingsAsync(cancellationToken);
            await this.ConvertFileWithServiceAsync(context, settings, cancellationToken);

            // пишем ключ, через который вызывающая сторона поймёт, что конвертация была выполнена посредством unoconv
            context.ResponseInfo[FileConverterWorkerNames.OnlyOfficeServiceToPdf] = BooleanBoxes.True;

            logger.Trace("End converting");
        }

        public Task PerformMaintenanceAsync(CancellationToken cancellationToken = default)
        {
            // does nothing by default
            return Task.CompletedTask;
        }

        public Task PreprocessAsync(CancellationToken cancellationToken = default)
        {
            // does nothing by default
            return Task.CompletedTask;
        }

        #endregion

        #region IAsyncDisposable

        public virtual ValueTask DisposeAsync() => new ValueTask();

        #endregion

        #region Private Methods

        /// <summary>
        /// Конвертирует файл через сервис OnlyOffice
        /// </summary>
        /// <param name="context">Контекст конвертации.</param>
        /// <param name="settings">Настройки для OnlyOffice.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Контент файла</returns>
        private async Task ConvertFileWithServiceAsync(
            IFileConverterContext context,
            IOnlyOfficeSettings settings,
            CancellationToken cancellationToken = default)
        {
            var id = Guid.NewGuid();
            string accessToken;

            var request = context.Request;
            await using (var stream =
                         await await this.GetFileStreamAsync(
                             request.FileName,
                             request.CardID,
                             request.FileID,
                             request.VersionID,
                             request.FileTypeName,
                             request.Info.ToDictionaryStorage(),
                             cancellationToken))
            {
                accessToken = await this.onlyOfficeService.CreateFileAsync(
                    id,
                    request.VersionID,
                    request.FileName,
                    stream,
                    cancellationToken);
            }

            var fileUrl = GetFileUrl(settings, id, accessToken);
            await using (var convertedFileStream =
                         await ConvertFileInternalAsync(
                             settings,
                             settings.ConverterUrl,
                             fileUrl,
                             context.OutputExtension,
                             context.InputExtension,
                             cancellationToken))
            await using (var result = FileHelper.OpenWrite(context.OutputFilePath))
            {
                await convertedFileStream.CopyToAsync(result, cancellationToken);
            }

            await this.onlyOfficeService.DeleteAsync(id, Guid.Empty, true, cancellationToken);
        }

        private async Task<ValueTask<Stream>> GetFileStreamAsync(
            string name,
            Guid cardId,
            Guid fileId,
            Guid versionId,
            string fileTypeName,
            Dictionary<string, object> info = null,
            CancellationToken cancellationToken = default)
        {
            var contentRequest = new CardGetFileContentRequest
            {
                ServiceType = CardServiceType.Default,
                VersionRowID = versionId,
                CardID = cardId,
                FileID = fileId,
                FileName = name,
                FileTypeName = fileTypeName,
                Info = info!
            };

            var contentResult = await this.streamServerRepositoryExt.GetFileContentAsync(contentRequest, cancellationToken);
            var contentResponse = contentResult.Response;

            if (!contentResult.HasContent || !contentResponse.ValidationResult.IsSuccessful())
            {
                // логирование для необработанного исключения
                throw new ValidationException(contentResponse.ValidationResult.Build());
            }

            return contentResult.GetContentOrThrowAsync(cancellationToken);
        }

        private static string GetFileUrl(IOnlyOfficeSettings settings, Guid id, string fileToken) =>
            $"{settings.WebApiBasePath?.TrimEnd('/')}/files/{id}/editor?token={fileToken}";

        /// <summary>
        /// Конвертирует указанный файл в необходимый формат, используя сервер документов.
        /// </summary>
        /// <param name="settings">Настройки OnlyOffice.</param>
        /// <param name="converterUrl">Ссылка на эндпоинт конвертера.</param>
        /// <param name="fileUrl">Путь к исходному файлу.</param>
        /// <param name="outExt">Расширение, в которое необходимо конвертировать.</param>
        /// <param name="inputExt">Возможность передать расширение файла. Необязательный параметр</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает поток сконвертированного файла.</returns>
        private static async Task<Stream> ConvertFileInternalAsync(
            IOnlyOfficeSettings settings,
            string converterUrl,
            string fileUrl,
            string outExt,
            string inputExt,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(converterUrl))
            {
                throw new ArgumentException("ConverterUrl is not specified", nameof(converterUrl));
            }

            var body = new Dictionary<string, object>
            {
                { "filetype", inputExt },
                { "outputtype", outExt },
                { "key", Guid.NewGuid().ToString("N")[..20] },
                { "url", fileUrl }
            };

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaTypeNames.Application.Json);

            using HttpClient httpClient = CreateOnlyOfficeHttpClient(settings);
            HttpResponseMessage convertFileResponse = await httpClient.PostAsync(converterUrl, content, cancellationToken);

            var convertFileData = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                await convertFileResponse.Content.ReadAsStringAsync(cancellationToken));

            string convertedFileUrl = convertFileData.Get<string>("fileUrl");
            return await GetFileStreamByUrlAsync(httpClient, convertedFileUrl, cancellationToken);
        }

        /// <summary>
        /// Получает поток файла по указанному пути.
        /// </summary>
        /// <param name="httpClient">Объект, посредством которого выполняется вызов OnlyOffice.</param>
        /// <param name="fileUrl">URL к файлу.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Контент файла в виде потока.</returns>
        private static async Task<Stream> GetFileStreamByUrlAsync(
            HttpClient httpClient,
            string fileUrl,
            CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = await httpClient.GetAsync(fileUrl, cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// Создаёт объект <see cref="HttpClient"/> для обращения к веб-сервису OnlyOffice.
        /// Используйте конструкцию <c>using</c>, чтобы закрыть соединение с сервисом.
        /// </summary>
        /// <param name="settings">Настройки OnlyOffice, полученные из карточки настроек.</param>
        /// <returns>Созданный объект.</returns>
        private static HttpClient CreateOnlyOfficeHttpClient(IOnlyOfficeSettings settings)
        {
            HttpClient httpClient = null;

            try
            {
                httpClient = new HttpClient { Timeout = settings.LoadTimeout };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

                HttpClient result = httpClient;
                httpClient = null;

                return result;
            }
            finally
            {
                httpClient?.Dispose();
            }
        }

        #endregion
    }
}
