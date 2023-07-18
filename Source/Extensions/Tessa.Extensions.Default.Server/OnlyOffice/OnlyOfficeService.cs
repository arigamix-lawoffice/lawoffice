#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NLog;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.OnlyOffice.Token;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Files;
using Tessa.Json;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    public class OnlyOfficeService :
        IOnlyOfficeService
    {
        #region Fields

        private readonly IOnlyOfficeSettingsProvider settingsProvider;
        private readonly IOnlyOfficeFileCacheInfoStrategy fileCacheInfoStrategy;
        private readonly IOnlyOfficeTokenManager tokenManager;
        private readonly IOnlyOfficeFileCache fileCache;
        private readonly ICardRepository cardRepository;
        private readonly ICardFileManager fileManager;
        private readonly IKrTokenProvider krTokenProvider;
        private readonly IErrorManager errorManager;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public OnlyOfficeService(
            IOnlyOfficeSettingsProvider settingsProvider,
            IOnlyOfficeFileCacheInfoStrategy fileCacheInfoStrategy,
            IOnlyOfficeTokenManager tokenManager,
            IOnlyOfficeFileCache fileCache,
            ICardRepository cardRepository,
            ICardFileManager fileManager,
            IKrTokenProvider krTokenProvider,
            IErrorManager errorManager)
        {
            this.settingsProvider = settingsProvider;
            this.fileCacheInfoStrategy = fileCacheInfoStrategy;
            this.tokenManager = tokenManager;
            this.fileCache = fileCache;
            this.cardRepository = cardRepository;
            this.fileManager = fileManager;
            this.krTokenProvider = krTokenProvider;
            this.errorManager = errorManager;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Создаёт объект <see cref="HttpClient"/> для обращения к веб-сервису OnlyOffice.
        /// Используйте конструкцию <c>using</c>, чтобы закрыть соединение с сервисом.
        /// </summary>
        /// <param name="settings">Настройки OnlyOffice, полученные из карточки настроек.</param>
        /// <returns>Созданный объект.</returns>
        private static HttpClient CreateOnlyOfficeHttpClient(IOnlyOfficeSettings settings)
        {
            HttpClient? httpClient = null;

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


        /// <summary>
        /// Получает поток изменного файла в редакторе документов.
        /// </summary>
        /// <param name="info">Информация о файле.</param>
        /// <param name="convertToSourceExtension">Признак того, что следует произвести конвертацию в исходный формат файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Поток файла.</returns>
        private async Task<(Stream stream, string fileName)> GetModifiedFileFromOnlyOfficeAsync(
            IOnlyOfficeFileCacheInfo info,
            bool convertToSourceExtension,
            CancellationToken cancellationToken = default)
        {
            if (info.ModifiedFileUrl is null)
            {
                throw new InvalidOperationException("Modified file doesn't exist.");
            }

            IOnlyOfficeSettings settings = await this.settingsProvider.GetSettingsAsync(cancellationToken);
            using HttpClient httpClient = CreateOnlyOfficeHttpClient(settings);

            if (convertToSourceExtension)
            {
                var targetExtension = FileExtensionWithoutDot(info.SourceFileName);
                var converterUrl = settings.ConverterUrl;

                if (string.IsNullOrWhiteSpace(converterUrl))
                {
                    throw new ArgumentException("ConverterUrl is not specified", nameof(converterUrl));
                }

                if (string.IsNullOrWhiteSpace(targetExtension))
                {
                    throw new ArgumentException("Output extension is not identified", nameof(targetExtension));
                }

                Stream convertedFileStream = await ConvertFileInternalAsync(
                    httpClient,
                    converterUrl,
                    info.ModifiedFileUrl,
                    targetExtension,
                    cancellationToken: cancellationToken);

                return (convertedFileStream, info.SourceFileName);
            }

            string rawFileName = Path.GetFileNameWithoutExtension(info.SourceFileName) + "." +
                ExtractFileExtensionFromUrl(info.ModifiedFileUrl);

            Stream rawStream = await GetFileStreamByUrlAsync(httpClient, info.ModifiedFileUrl, cancellationToken);
            return (rawStream, rawFileName);
        }

        /// <summary>
        /// Конвертирует указанный файл в необходимый формат, используя сервер документов.
        /// </summary>
        /// <param name="httpClient">Объект, посредством которого выполняется вызов OnlyOffice.</param>
        /// <param name="converterUrl">Ссылка на эндпоинт конвертера.</param>
        /// <param name="fileUrl">Путь к исходному файлу.</param>
        /// <param name="toExt">Расширение, в которое необходимо конвертировать.</param>
        /// <param name="inputExt">Возможность передать расширение файла. Необязательный параметр</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает поток сконвертированного файла.</returns>
        private static async Task<Stream> ConvertFileInternalAsync(
            HttpClient httpClient,
            string converterUrl,
            string fileUrl,
            string toExt,
            string? inputExt = null,
            CancellationToken cancellationToken = default)
        {
            var fileType = inputExt ?? ExtractFileExtensionFromUrl(fileUrl);

            if (string.IsNullOrWhiteSpace(fileType))
            {
                throw new InvalidDataException("Input file type is not identified");
            }

            var body = new Dictionary<string, object>
            {
                { "filetype", fileType },
                { "outputtype", toExt },
                { "key", Guid.NewGuid().ToString("N")[..20] },
                { "url", fileUrl }
            };

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage convertFileResponse = await httpClient.PostAsync(converterUrl, content, cancellationToken);

            var convertFileData = JsonConvert.DeserializeObject<Dictionary<string, object?>>(
                await convertFileResponse.Content.ReadAsStringAsync(cancellationToken));

            string convertedFileUrl = NotNullOrThrow(convertFileData.Get<string>("fileUrl"));
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
        /// Извлекает из URL-пути к файлу расширение файла без точки.
        /// </summary>
        private static string? ExtractFileExtensionFromUrl(string fileUrl) =>
            FileExtensionWithoutDot(fileUrl[..fileUrl.IndexOf('?', StringComparison.Ordinal)]);

        /// <summary>
        /// Извлекает из полного имени файла расширение файла без точки.
        /// </summary>
        private static string? FileExtensionWithoutDot(string? fileName) => fileName is null ? null : Path.GetExtension(fileName)[1..];

        private async Task StoreNewVersionAsync(Guid infoID, string modifiedUrl, int repeatOnErrorTimes = 1, CancellationToken cancellationToken = default)
        {
            bool success = false;
            Card? card = null;
            var validationResultBuilder = new ValidationResultBuilder();
            await using var _ = SessionContext.Create(new SessionToken(Session.SystemID, Session.SystemName));

            for (var i = 0; i < repeatOnErrorTimes; i++)
            {
                Stream? fileStream = null;
                card = null;
                try
                {
                    var info = await this.fileCacheInfoStrategy.TryGetInfoAsync(infoID, cancellationToken);
                    if (info is null)
                    {
                        var message = $"Not found cacheInfo for infoID = {infoID}";
                        validationResultBuilder.AddError(message);
                        logger.Error(message);
                        break;
                    }

                    // урл ещё не сохранен
                    info.ModifiedFileUrl = modifiedUrl;

                    (fileStream, string _) = await GetModifiedFileFromOnlyOfficeAsync(info, false, cancellationToken: cancellationToken);
                    var cardAndFileIDs = await this.fileCacheInfoStrategy.TryGetCardIDFileIDByFileVersionIDAsync(info.SourceFileVersionID, cancellationToken);

                    if (cardAndFileIDs is null)
                    {
                        var message = $"Not found card for infoID = {infoID}";
                        validationResultBuilder.AddError(message);
                        logger.Error(message);
                        break;
                    }

                    var (cardID, fileID) = cardAndFileIDs.Value;

                    // если тип карточки включён в типовое решение, то необходимо указать токен с правами
                    var token = this.krTokenProvider.CreateToken(cardID);

                    // загружаем карточку
                    var getRequest = new CardGetRequest { CardID = cardID };
                    token.Set(getRequest.Info);

                    var getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
                    if (!getResponse.ValidationResult.IsSuccessful())
                    {
                        validationResultBuilder.Add(getResponse.ValidationResult);
                        logger.LogResult(getResponse.ValidationResult);
                        continue;
                    }

                    card = getResponse.Card;

                    // добавляем или заменяем файл, а затем сохраняем карточку
                    await using var container = await this.fileManager.CreateContainerAsync(card, cancellationToken: cancellationToken);
                    var file = container.FileContainer.Files.FirstOrDefault(x => x.ID == fileID);

                    if (file is null)
                    {
                        var message = $"Not found file for infoID = {infoID}";
                        validationResultBuilder.AddError(message);
                        logger.Error(message);
                        break;
                    }

                    // файл уже есть, заменяем его
                    await file.ReplaceAsync(fileStream, cancellationToken);

                    // сохраняем карточку с файлами
                    var storeResponse = await container.StoreAsync(async (c, request, ct) =>
                    {
                        // для типового решения надо указать токен с правами
                        token.Set(request.Card.Info);
                    }, cancellationToken: cancellationToken);

                    if (!storeResponse.ValidationResult.IsSuccessful())
                    {
                        validationResultBuilder.Add(storeResponse.ValidationResult);
                        break;
                    }

                    // all good so stop
                    success = true;
                    break;
                }
                finally
                {
                    if (fileStream is not null)
                    {
                        await fileStream.DisposeAsync();
                    }
                }
            }

            if (!success)
            {
                await this.errorManager.ReportErrorSafeAsync(
                    card?.TypeID ?? Guid.Empty,
                    card?.ID ?? Guid.Empty,
                    card?.TryGetDigest() ?? card?.TypeCaption,
                    new ErrorDescription(
                        validationResultBuilder.Build(),
                        "Web_CoeditSaveFile_Failed"));
            }
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public Task<IOnlyOfficeFileCacheInfo?> GetFileStateAsync(
            Guid id,
            CancellationToken cancellationToken = default) => this.fileCacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);

        /// <inheritdoc/>
        public async Task<(Stream stream, string fileName)> GetEditableFileAsync(
            Guid id,
            OnlyOfficeJwtTokenInfo token,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (token is null || id != token.ID)
                {
                    throw new InvalidOperationException("Token is not valid");
                }

                // если у нас уже есть отредактированный файл этим пользователем, возвращаем его
                IOnlyOfficeFileCacheInfo? fileInfo = await this.fileCacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);
                if (fileInfo is null)
                {
                    throw new InvalidOperationException("Specified file isn't found.");
                }

                Stream? stream;
                string fileName;
                if (fileInfo.ModifiedFileUrl is not null)
                {
                    (Stream modifiedFileStream, string fileNameOut) =
                        await this.GetModifiedFileFromOnlyOfficeAsync(fileInfo, false, cancellationToken);

                    stream = modifiedFileStream;
                    fileName = fileNameOut;
                }
                else
                {
                    // это обращение к fileCache будет на том же соединении dbScope, что и вышележащее
                    (ValidationResult result, Func<CancellationToken, ValueTask<Stream>>? getContentStreamFunc, long _) =
                        await this.fileCache.GetContentAsync(id, cancellationToken);

                    if (getContentStreamFunc is null || !result.IsSuccessful)
                    {
                        throw new ValidationException(result);
                    }

                    stream = await getContentStreamFunc(cancellationToken);
                    fileName = fileInfo.SourceFileName;
                }

                return (stream, fileName);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                // Файл не открылся - всё понятно, можно финалить статус
                await this.fileCacheInfoStrategy.UpdateInfoAsync(id, null, false, CancellationToken.None);
                throw;
            }
        }


        /// <inheritdoc/>
        public async Task<(Stream stream, string fileName)> GetFinalFileAsync(
            Guid id,
            bool originalFormat = false,
            CancellationToken cancellationToken = default)
        {
            IOnlyOfficeFileCacheInfo? fileInfo = await this.fileCacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);
            if (fileInfo is null)
            {
                throw new InvalidOperationException("Specified file isn't found.");
            }

            (Stream stream, string fileName) = await this.GetModifiedFileFromOnlyOfficeAsync(fileInfo, originalFormat, cancellationToken);

            return (stream, fileName);
        }

        /// <inheritdoc/>
        public async Task<string> CreateFileAsync(
            Guid id,
            Guid versionId,
            string name,
            Stream contentStream,
            CancellationToken cancellationToken = default)
        {
            ValidationResult result = await this.fileCache.CreateAsync(id, versionId, name, contentStream, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            var settings = await this.settingsProvider.GetSettingsAsync(cancellationToken);

            return this.tokenManager.CreateToken(id, versionId, settings.TokenLifetimePeriod, OnlyOfficeTokenPermissionFlags.Get);
        }

        /// <inheritdoc/>
        public async Task CallbackAsync(
            Guid id,
            OnlyOfficeJwtTokenInfo token,
            IDictionary<string, object?> data,
            CancellationToken cancellationToken = default)
        {
            if (token is null || id != token.ID)
            {
                throw new InvalidOperationException("Token is not valid");
            }

            var coeditKey = data.TryGet<string>("key");
            OnlyOfficeActionType? actionType = null;
            Guid? userId = null;

            var actions = data.TryGet<JArray>("actions");
            if (actions is not default(JArray))
            {
                try
                {
                    actionType = (OnlyOfficeActionType?) actions.First?.Value<int>("type");
                    var userIdStr = actions.First?.Value<string>("userid");
                    if (userIdStr is not null)
                    {
                        userId = Guid.Parse(userIdStr);
                    }
                }
                catch
                {
                    // Do you care? cause i'm don't
                }
            }

            var realId = await this.fileCacheInfoStrategy.TryGetInfoAsync(coeditKey, userId, cancellationToken) ?? id;

            var status = (OnlyOfficeCallbackDocumentStatus) data.Get<long>("status");
            switch (status)
            {
                // received every user connection to or disconnection from document co-editing
                case OnlyOfficeCallbackDocumentStatus.BeingEdited:
                    if (coeditKey is not null && actionType == OnlyOfficeActionType.CoeditDisconnect)
                    {
                        await this.fileCacheInfoStrategy.UpdateInfoAsync(realId, null, false, cancellationToken);
                    }
                    else
                    {
                        await this.fileCacheInfoStrategy.UpdateInfoOnEditorOpenedAsync(id, cancellationToken);
                    }

                    break;

                // document is ready for saving
                case OnlyOfficeCallbackDocumentStatus.ReadyForSaving:
                    var newFileUrlAfterClose = data.Get<string>("url");

                    if (coeditKey is not null && newFileUrlAfterClose is not null)
                    {
                        await StoreNewVersionAsync(realId, newFileUrlAfterClose, 3, cancellationToken);
                    }

                    await this.fileCacheInfoStrategy.UpdateInfoAsync(realId, newFileUrlAfterClose, true, cancellationToken);
                    break;

                // document is being edited, but the current document state is saved
                case OnlyOfficeCallbackDocumentStatus.BeingEditedButStateIsSaved:
                    var newFileUrlAfterForceSave = data.Get<string>("url");
                    await this.fileCacheInfoStrategy.UpdateInfoAsync(realId, newFileUrlAfterForceSave, null, cancellationToken);
                    break;

                // document saving error has occurred
                case OnlyOfficeCallbackDocumentStatus.SavingError:
                    // do nothing
                    break;

                // document is closed with no changes
                case OnlyOfficeCallbackDocumentStatus.ClosedWithNoChanges:
                    await this.fileCacheInfoStrategy.UpdateInfoAsync(realId, null, false, cancellationToken);
                    break;

                // error has occurred while force saving the document
                case OnlyOfficeCallbackDocumentStatus.ForceSavingError:
                    // do nothing
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(
            Guid id,
            Guid userID,
            bool isAdministrator,
            CancellationToken cancellationToken = default)
        {
            IOnlyOfficeFileCacheInfo? fileInfo = null;

            bool canDelete = isAdministrator;
            if (!canDelete)
            {
                // если это не администратор, разрешаем удалять только автору записи
                fileInfo = await this.fileCacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);
                if (fileInfo is null)
                {
                    return;
                }

                canDelete = userID == fileInfo.CreatedByID;
            }

            if (!canDelete)
            {
                throw new InvalidOperationException($"User ID={userID:B} can't delete info for file ID={id:B}.");
            }

            ValidationResult result = await this.fileCache.DeleteAsync(id, fileInfo?.SourceFileVersionID, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }
        }

        /// <inheritdoc/>
        public async Task<(IOnlyOfficeFileCacheInfo FileInfo, bool IsNew, string? AccessToken)> InitiateCoeditAsync(
            Guid versionID,
            string name,
            Guid userID,
            CancellationToken cancellationToken = default)
        {
            var (fileInfo, isNew) = await this.fileCacheInfoStrategy.CreateCoeditInfoAsync(versionID, name, userID, cancellationToken);

            // Если файл новый, то токен будет получен при кешировании
            string? accessToken = null;
            if (!isNew)
            {
                var settings = await this.settingsProvider.GetSettingsAsync(cancellationToken);

                accessToken = this.tokenManager.CreateToken(fileInfo.ID, versionID, settings.TokenLifetimePeriod, OnlyOfficeTokenPermissionFlags.Get);
            }

            return (fileInfo, isNew, accessToken);
        }

        /// <inheritdoc/>
        public async Task<(Guid? CurrentVersionID, string? UserNames, DateTime? LastAccessTime)> TryGetCurrentCoeditWithLastVersionAsync(
            Guid fileVersionID,
            CancellationToken cancellationToken = default)
        {
            var lastVersionResult = await this.fileCacheInfoStrategy.GetFileVersionsAsync(fileVersionID, cancellationToken);

            var lastVersionID = lastVersionResult.MaxBy(x => x.Number)?.RowID;

            var results = await TryGetCurrentCoeditAsync(new List<Guid> { fileVersionID }, cancellationToken);
            if (results.Count == 0)
            {
                return (lastVersionID, null, null);
            }

            var result = results[0];
            return (lastVersionID, result.Item2, result.Item3);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<(Guid CurrentVersionID, string? UserNames, DateTime? LastAccessTime)>> TryGetCurrentCoeditAsync(
            IEnumerable<Guid> fileVersionIDs,
            CancellationToken cancellationToken = default)
        {
            var result = await this.fileCacheInfoStrategy.GetCurrentCoeditInfosAsync(fileVersionIDs, cancellationToken);
            if (result.Count == 0)
            {
                return Array.Empty<(Guid, string?, DateTime?)>();
            }

            var newg = result
                .GroupBy(x => x.SourceFileVersionID)
                .Select(x =>
                    x.GroupBy(y => y.CoeditKey)
                        .OrderByDescending(z => z.Max(y => y.LastAccessTime))
                        .First());

            return newg
                .Select(x =>
                    (x.First().SourceFileVersionID,
                        (string?) string.Join(",", x.Select(z => z.Name).Distinct()),
                        (DateTime?) x.Max(z => z.LastAccessTime)))
                .ToArray();
        }

        /// <inheritdoc/>
        public async Task CloseEditorAsync(
            Guid id,
            Guid userID,
            bool isAdministrator,
            CancellationToken cancellationToken = default)
        {
            IOnlyOfficeFileCacheInfo? fileInfo = null;

            bool canClose = isAdministrator;
            if (!canClose)
            {
                // если это не администратор, разрешаем закрывать только автору записи
                fileInfo = await this.fileCacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);
                if (fileInfo is null)
                {
                    return;
                }

                canClose = userID == fileInfo.CreatedByID;
            }

            if (!canClose)
            {
                throw new InvalidOperationException($"User ID={userID:B} can't close editor for file ID={id:B}.");
            }

            await this.fileCacheInfoStrategy.UpdateInfoAsync(id, null, false, cancellationToken);
        }

        #endregion
    }
}
