using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using NLog;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.EDS;
using Tessa.Extensions.Default.Server.Web.DeskiMobile;
using Tessa.Extensions.Default.Server.Web.DeskiMobile.Models;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.EDS;
using Tessa.Files;
using Tessa.Json;
using Tessa.Platform;
using Tessa.Platform.EDS;
using Tessa.Platform.IO;
using Tessa.Platform.Operations;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Web;
using Tessa.Web.Serialization;
using FileSignatureResponse = Tessa.Extensions.Default.Server.Web.DeskiMobile.Models.FileSignatureResponse;
using ISession = Tessa.Platform.Runtime.ISession;

namespace Tessa.Extensions.Default.Server.Web.Controllers
{
    [Route("mobile"), AllowAnonymous, ApiController]
    [ProducesErrorResponseType(typeof(PlainValidationResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public sealed class DeskiMobileController : Controller
    {
        #region Constructors

        /// <summary>
        /// Конструктор запрашивает зависимости от контейнера ASP.NET Core, у которого они отличаются от зависимостей в UnityContainer.
        /// </summary>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="cardStreamServerRepository">Репозиторий для потокового управления карточками на сервере.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="cardCache">Кэш карточек настроек.</param>
        /// <param name="edsProvider">Объект, обеспечивающий низкоуровневые функции по работе с электронной подписью.</param>
        /// <param name="operationRepository">Репозиторий, управляющий операциями.</param>
        /// <param name="htmlSanitizer">Объект, выполняющий санитайзинг HTML-документов.</param>
        /// <param name="deskiMobileTokenManager">Manager, управляющий созданием и проверкой Jwt токенов для DeskiMobile.</param>
        /// <param name="sessionServer">Объект, обеспечивающий взаимодействие с сессиями на сервере.</param>
        /// <param name="linkGenerator">Определяет контракт для создания абсолютных и связанных URI на основе маршрутизации конечных точек.</param>
        /// <param name="lockingStrategy">Стратегия по управлению блокировками операций.</param>
        public DeskiMobileController(
            ISession session,
            ICardStreamServerRepository cardStreamServerRepository,
            ICardRepository cardRepository,
            ICardFileManager fileManager,
            ICardCache cardCache,
            IEDSProvider edsProvider,
            IOperationRepository operationRepository,
            IHtmlSanitizer htmlSanitizer,
            IDeskiMobileTokenManager deskiMobileTokenManager,
            ISessionServer sessionServer,
            LinkGenerator linkGenerator,
            IDeskiMobileLockingStrategy lockingStrategy)
        {
            this.session = session;
            this.cardStreamRepository = cardStreamServerRepository;
            this.cardRepository = cardRepository;
            this.fileManager = fileManager;
            this.cardCache = cardCache;
            this.edsProvider = edsProvider;
            this.operationRepository = operationRepository;
            this.htmlSanitizer = htmlSanitizer;
            this.deskiMobileTokenManager = deskiMobileTokenManager;
            this.sessionServer = sessionServer;
            this.linkGenerator = linkGenerator;
            this.lockingStrategy = lockingStrategy;
        }

        #endregion

        #region Fields

        private readonly ISession session;

        private readonly ICardStreamServerRepository cardStreamRepository;

        private readonly ICardRepository cardRepository;

        private readonly ICardFileManager fileManager;

        private readonly ICardCache cardCache;

        private readonly IEDSProvider edsProvider;

        private readonly IOperationRepository operationRepository;

        private readonly IHtmlSanitizer htmlSanitizer;

        private readonly IDeskiMobileTokenManager deskiMobileTokenManager;

        private readonly ISessionServer sessionServer;

        private readonly LinkGenerator linkGenerator;

        private readonly IDeskiMobileLockingStrategy lockingStrategy;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!(await this.IsDeskiMobileEnabledAsync(context.HttpContext.RequestAborted)))
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }

        #endregion

        #region Controller Methods

        /// <summary>
        /// Создание операции для работы с deskiMobile.
        /// Требуется для понимания наличия deskiMobile на устройстве пользователя.
        /// </summary>
        /// <param name="operation">Тип генерируемой операции.</param>
        /// <param name="request">Информация о файле.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Модель MobileInitOperationResponse с необходимыми данными для запуска deskiMobile.</returns>
        // POST mobile/init-operation
        [HttpPost("init-operation"), SessionMethod, TypedJsonBody(ConvertPascalCasing = true)]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InitOperationResponse>> PostInitOperation(
            [FromQuery] string operation,
            [FromBody] InitOperationRequest request,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(request?.VersionRowID);
            Guid versionRowId = request.VersionRowID;

            var serializedToken = StorageHelper.SerializeToTypedJson(NotNullOrThrow(this.session.Token).SerializeToStorage());

            var operationRequest = new OperationRequest
            {
                Info =
                {
                    ["FileRequest"] = request.ToSerializedDictionary(),
                    ["OperationType"] = operation,
                    ["Token"] = serializedToken
                }
            };

            var operationID = await this.operationRepository.CreateAsync(
                OperationTypes.Other,
                OperationCreationFlags.ReportsProgress,
                "DeskiMobile init",
                operationRequest,
                cancellationToken: cancellationToken);

            string url = this.linkGenerator.GetUriByAction(this.HttpContext, nameof(this.PostStartOperation), null, new { operationId = operationID });
            ThrowIfNull(url);

            string base64Url = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Uri.EscapeDataString(url)));

            // создаем Jwt-токен
            var flags = DeskiMobileTokenPermissionFlags.None;
            switch (operation.ToLowerInvariant())
            {
                case "sign":
                    flags = DeskiMobileTokenPermissionFlags.StartOperation |
                        DeskiMobileTokenPermissionFlags.CancelOperation |
                        DeskiMobileTokenPermissionFlags.GetContent |
                        DeskiMobileTokenPermissionFlags.Enhance;
                    break;

                case "verify":
                    flags = DeskiMobileTokenPermissionFlags.StartOperation |
                        DeskiMobileTokenPermissionFlags.CancelOperation |
                        DeskiMobileTokenPermissionFlags.GetContent |
                        DeskiMobileTokenPermissionFlags.GetSignatures |
                        DeskiMobileTokenPermissionFlags.Verify;
                    break;

                case "preview":
                    flags = DeskiMobileTokenPermissionFlags.StartOperation |
                        DeskiMobileTokenPermissionFlags.GetContent;
                    break;
            }

            var jwtLifeTime = await this.GetDeskiMobileJwtLifeTimeAsync(cancellationToken);
            var timespanJwtLifeTime = TimeSpan.FromMinutes((int) jwtLifeTime.TimeOfDay.TotalMinutes);

            var encodedJwt = this.deskiMobileTokenManager.CreateToken(
                timespanJwtLifeTime, this.session.Token.SessionID, operationID, versionRowId, flags);

            string parameters = $"version=1&id={versionRowId}&token={encodedJwt}&url={base64Url}";
            string link = $"{LinkHelper.TessaProtocol}://{operation}?{parameters}";

            return new InitOperationResponse { OperationID = operationID, Link = link };
        }

        /// <summary>
        /// Запрос выполняется со стороны ЛК.
        /// Удаляет созданную операцию.
        /// </summary>
        /// <param name="id">ID операции.</param>
        /// <param name="force">Требуется удалить принудительно.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Возвращает true, если операция удалена, иначе false.</returns>
        // DELETE mobile/created-operation/{id}?force=false
        [HttpDelete("created-operation/{id:guid}"), SessionMethod]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteCreatedOperation(
            [FromRoute] Guid id,
            [FromQuery] bool force = false,
            CancellationToken cancellationToken = default)
        {
            var result = await this.lockingStrategy.ObtainWriterLockAsync(id, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            try
            {
                var state = await this.operationRepository.GetStateAsync(id, cancellationToken);
                ValidateState(state);
                if (state != OperationState.Created && !force)
                {
                    return false;
                }

                await this.operationRepository.DeleteAsync(id, cancellationToken: cancellationToken);
                return true;
            }
            finally
            {
                await this.lockingStrategy.ReleaseWriterLockAsync(id);
            }
        }

        /// <summary>
        /// Запрос выполняется со стороны ЛК.
        /// Возвращает статус операции.
        /// </summary>
        /// <param name="id">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Возвращает статус операции.</returns>
        // GET mobile/operation-status/{id}
        [HttpGet("operation-status/{id:guid}"), SessionMethod]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<OperationState>> GetOperationStatus(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            (bool success, ValidationResult result) = await this.lockingStrategy.TryObtainReaderLockNoWaitAsync(id, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            if (!success)
            {
                return this.NoContent();
            }

            try
            {
                var state = await this.operationRepository.GetStateAsync(id, cancellationToken);
                ValidateState(state);
                return this.Json(state.ToString());
            }
            finally
            {
                await this.lockingStrategy.ReleaseReaderLockAsync(id);
            }
        }

        /// <summary>
        /// Запрос выполняется со стороны ЛК.
        /// Попытка получить полезную нагрузку операции с дальнейшим удалением операции.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns> 
        /// Возвращает полезную нагрузку, заложенную в операцию, если операция была завершена, иначе null.
        /// </returns>
        // POST mobile/try-get-response-and-delete
        [HttpPost("try-get-response-and-delete"), SessionMethod]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteOperationResponse>> TryGetCompletedResponseAndDelete(
            [FromQuery] Guid operationId,
            CancellationToken cancellationToken = default)
        {
            (bool success, ValidationResult result) = await this.lockingStrategy.TryObtainReaderLockNoWaitAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            if (!success)
            {
                return new DeleteOperationResponse();
            }

            var lockEscalated = false;
            try
            {
                var operation = await this.operationRepository.TryGetAsync(operationId, cancellationToken: cancellationToken);
                ValidateOperation(operation);
                if (operation.State != OperationState.Completed)
                {
                    return new DeleteOperationResponse();
                }

                var validationResult = await this.lockingStrategy.EscalateReaderLockAsync(operationId, cancellationToken);
                if (!validationResult.IsSuccessful)
                {
                    throw new ValidationException(validationResult);
                }

                lockEscalated = true;
                await this.operationRepository.DeleteAsync(operationId, operation.TypeID, cancellationToken);
                return new DeleteOperationResponse { Deleted = true, Response = operation.Response };
            }
            finally
            {
                if (lockEscalated)
                {
                    await this.lockingStrategy.ReleaseWriterLockAsync(operationId);
                }
                else
                {
                    await this.lockingStrategy.ReleaseReaderLockAsync(operationId);
                }
            }
        }

        /// <summary>
        /// Запрос выполняется со стороны ЛК.
        /// Удаление операции и возврат полезной нагрузки, заложенной в операцию, если она была указана.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns> 
        /// Возвращает полезную нагрузку, заложенную в операцию, если она была указана,
        /// иначе будет считаться, что операция отменена.
        /// </returns>
        // POST mobile/get-response-or-cancel
        [HttpPost("get-response-or-cancel"), SessionMethod]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CancelOperationResponse>> TryGetCompletedResponseOrCancel(
            [FromQuery] Guid operationId,
            CancellationToken cancellationToken = default)
        {
            var result = await this.lockingStrategy.ObtainWriterLockAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            try
            {
                var operation = await this.operationRepository.TryGetAsync(operationId, cancellationToken: cancellationToken);
                ValidateOperation(operation);

                await this.operationRepository.DeleteAsync(operationId, operation.TypeID, cancellationToken);

                return operation.State == OperationState.Completed
                    ? new CancelOperationResponse { Response = operation.Response }
                    : new CancelOperationResponse { Cancelled = true };
            }
            finally
            {
                await this.lockingStrategy.ReleaseWriterLockAsync(operationId);
            }
        }

        /// <summary>
        /// Запрос выполняется со стороны deskiMobile.
        /// Выполняется один раз, формирует все необходимые ссылки для выполнения операции
        /// и меняет состояние операции с Created на InProgress.
        /// Это делается для того, чтобы со стороны ЛК мы могли отслеживать состояние операции.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>
        /// Запрос возвращает MobileStartOperationResponse с необходимыми ссылками для скачивания файла, загрузки сигнатур подписи и т.д.
        /// </returns>
        // POST mobile/start-operation
        [HttpPost("start-operation")]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StartOperationResponse>> PostStartOperation(
            [FromQuery] Guid operationId,
            CancellationToken cancellationToken = default)
        {
            var tokens = await this.deskiMobileTokenManager.GetTokenAsync(this.Request, operationId, this.sessionServer, cancellationToken);
            if (!tokens.HasAccess(DeskiMobileTokenPermissionFlags.StartOperation))
            {
                return this.Forbid("Not enough permissions to start operation.");
            }

            var result = await this.lockingStrategy.ObtainWriterLockAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            try
            {
                var operation = await this.operationRepository.TryGetAsync(operationId, cancellationToken: cancellationToken);
                ValidateOperation(operation);
                var operationType = GetOperationType(operation);

                if (operation.State == OperationState.Created)
                {
                    logger.Trace("Starting operation with ID={0:B}.", operationId);
                    await this.operationRepository.StartAsync(operationId, operation.TypeID, cancellationToken);
                }

                if (operation.State != OperationState.InProgress && operation.State != OperationState.Created)
                {
                    throw new InvalidOperationException($"Operation ID={operationId:B} isn't active.");
                }

                string getContentUrl = this.linkGenerator.GetUriByAction(this.HttpContext, nameof(this.GetFileContent), null, new { operationId });
                string postCancelUrl = this.linkGenerator.GetUriByAction(this.HttpContext, nameof(this.PostCancel), null, new { operationId });

                string getSignaturesUrl = null;
                string postVerifyUrl = null;
                string postEnhanceUrl = null;
                switch (operationType)
                {
                    case "verify":
                        getSignaturesUrl = this.linkGenerator.GetUriByAction(this.HttpContext, nameof(this.GetFileSignatures), null, new { operationId });
                        postVerifyUrl = this.linkGenerator.GetUriByAction(this.HttpContext, nameof(this.PostVerify), null, new { operationId });
                        break;

                    case "sign":
                        postEnhanceUrl = this.linkGenerator.GetUriByAction(this.HttpContext, nameof(this.PostEnhance), null, new { operationId });
                        break;
                }

                return new StartOperationResponse
                {
                    GetContent = getContentUrl,
                    PostCancel = postCancelUrl,
                    GetSignatures = getSignaturesUrl,
                    PostVerify = postVerifyUrl,
                    PostEnhance = postEnhanceUrl,
                    PostHiddenVerifyWebDialog = null
                };
            }
            finally
            {
                await this.lockingStrategy.ReleaseWriterLockAsync(operationId);
            }
        }

        /// <summary>
        /// Запрос на скачивание потока с бинарными данными файла, указанного в операции.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Поток с бинарными данными файла.</returns>
        // GET mobile/get-file-content
        [HttpGet("get-file-content")]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Stream>> GetFileContent(
            [FromQuery] Guid operationId,
            CancellationToken cancellationToken = default)
        {
            var tokens = await this.deskiMobileTokenManager.GetTokenAsync(this.Request, operationId, this.sessionServer, cancellationToken);
            if (!tokens.HasAccess(DeskiMobileTokenPermissionFlags.GetContent))
            {
                return this.Forbid("Not enough permissions to get file content.");
            }

            await using var _ = SessionContext.Create(tokens.SessionToken);
            tokens.ApplySessionToken?.Invoke();

            var result = await this.lockingStrategy.ObtainReaderLockAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            OperationParams operationParams;
            try
            {
                var operation = await this.operationRepository.TryGetAsync(operationId, cancellationToken: cancellationToken);
                ValidateOperation(operation, OperationState.InProgress);
                operationParams = GetOperationParams(operation);
            }
            finally
            {
                await this.lockingStrategy.ReleaseReaderLockAsync(operationId);
            }

            var contentRequest = new CardGetFileContentRequest
            {
                ServiceType = CardServiceType.Client,
                CardID = operationParams.CardID,
                FileID = operationParams.FileID,
                FileName = operationParams.FileName,
                VersionRowID = operationParams.VersionRowID
            };

            var contentResult = await this.cardStreamRepository.GetFileContentAsync(contentRequest, cancellationToken);
            var contentResponse = contentResult.Response;

            var contentValidationResult = contentResponse.ValidationResult.Build();
            if (!contentValidationResult.IsSuccessful)
            {
                throw new ValidationException(contentValidationResult);
            }

            var fileName = contentResponse.TryGetSuggestedFileName() ?? operationParams.FileName?.Trim();
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                string encodedFileName = Uri.EscapeDataString(
                    FileHelper.RemoveInvalidFileNameChars(
                        FileHelper.GetFileNameWithoutExtension(fileName, true),
                        FileHelper.InvalidCharReplacement)
                    + FileHelper.GetExtension(fileName));
                this.Response.Headers.Add("Content-Disposition", $"attachment; filename*=UTF-8''{encodedFileName}");
            }

            return new FileStreamResult(await contentResult.GetContentOrThrowAsync(cancellationToken), MediaTypeNames.Application.Octet);
        }

        /// <summary>
        /// Запрос на получение информации о подписантах.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Список с информацией о подписантах.</returns>
        // GET mobile/get-file-signatures
        [HttpGet("get-file-signatures")]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FileSignatureResponse>>> GetFileSignatures(
            [FromQuery] Guid operationId,
            CancellationToken cancellationToken = default)
        {
            var tokens = await this.deskiMobileTokenManager.GetTokenAsync(this.Request, operationId, this.sessionServer, cancellationToken);
            if (!tokens.HasAccess(DeskiMobileTokenPermissionFlags.GetSignatures))
            {
                return this.Forbid("Not enough permissions to get file signatures.");
            }

            await using var _ = SessionContext.Create(tokens.SessionToken);
            tokens.ApplySessionToken?.Invoke();

            var result = await this.lockingStrategy.ObtainReaderLockAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            try
            {
                var operation = await this.operationRepository.TryGetAsync(operationId, cancellationToken: cancellationToken);
                ValidateOperation(operation, OperationState.InProgress);
                var operationParams = GetOperationParams(operation);

                // загружаем карточку
                var getRequest = new CardGetRequest { ServiceType = CardServiceType.Client, CardID = operationParams.CardID };
                var getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
                if (!getResponse.ValidationResult.IsSuccessful())
                {
                    throw new InvalidOperationException(getResponse.ValidationResult.ToString(ValidationLevel.Message));
                }

                // ищем файл нужной версии
                await using var container = await this.fileManager.CreateContainerAsync(getResponse.Card, cancellationToken: cancellationToken);
                var file = container.FileContainer.Files[operationParams.FileID];
                var version = file.Versions[operationParams.VersionRowID];

                var signatureResponse = await version.Source.GetSignaturesAsync(version, FileSignatureLoadingMode.WithData, cancellationToken);
                if (!signatureResponse.ValidationResult.IsSuccessful)
                {
                    throw new InvalidOperationException(signatureResponse.ValidationResult.ToString(ValidationLevel.Message));
                }

                var fileResult = await version.EnsureContentDownloadedAsync(cancellationToken: cancellationToken);
                if (!fileResult.IsSuccessful)
                {
                    throw new InvalidOperationException($"File content failed to load. Error: {fileResult.ToString(ValidationLevel.Message)}");
                }

                var response = new List<FileSignatureResponse>();
                foreach (IFileSignature addedItem in signatureResponse.Signatures)
                {
                    IFileSignatureData signatureData = addedItem.Data;
                    if (signatureData.IsEmpty) continue;

                    byte[] fileSignature = await signatureData.GetBytesAsync(cancellationToken);
                    string base64Signature = Convert.ToBase64String(fileSignature, 0, fileSignature.Length);
                    response.Add(new FileSignatureResponse
                    {
                        Signature = base64Signature,
                        ID = addedItem.ID,
                        SubjectName = addedItem.SubjectName,
                        Company = addedItem.Company,
                        SignedDate = addedItem.Signed,
                        EventType = addedItem.EventType,
                        SignatureProfile = addedItem.SignatureProfile,
                        SignatureType = addedItem.SignatureType,
                        UserName = addedItem.UserName
                    });
                }

                return response;
            }
            finally
            {
                await this.lockingStrategy.ReleaseReaderLockAsync(operationId);
            }
        }

        /// <summary>
        /// Запрос на усовершенствование подписи.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="parameters">Подпись в виде base64 строки.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>true - при успешном выполнении усовершенствования подписи, иначе throw.</returns>
        // POST mobile/enhance
        [HttpPost("enhance"), TypedJsonBody(ConvertPascalCasing = true)]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> PostEnhance(
            [FromQuery] Guid operationId,
            [FromBody] EnhanceRequest parameters,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNullOrEmpty(parameters?.Signature);

            var tokens = await this.deskiMobileTokenManager.GetTokenAsync(this.Request, operationId, this.sessionServer, cancellationToken);
            if (!tokens.HasAccess(DeskiMobileTokenPermissionFlags.Enhance))
            {
                return this.Forbid("Not enough permissions to enhance signature.");
            }

            await using var _ = SessionContext.Create(tokens.SessionToken);
            tokens.ApplySessionToken?.Invoke();

            var lockResult = await this.lockingStrategy.ObtainWriterLockAsync(operationId, cancellationToken);
            if (!lockResult.IsSuccessful)
            {
                throw new ValidationException(lockResult);
            }

            try
            {
                var state = await this.operationRepository.GetStateAsync(operationId, cancellationToken);
                ValidateState(state, OperationState.InProgress);

                var signature = Convert.FromBase64String(parameters.Signature);
                var signAttributes = CAdESSignatureServerHelper.GetSignatureAttributesFromSignature(signature);
                ThrowIfNull(signAttributes.Certificate, "Failed to get certificate from signed data.");

                var request = new CardRequest { ServiceType = CardServiceType.Client, RequestType = DefaultRequestTypes.CAdESSignature };
                var cmsSignature = new SignatureData(signature);
                CAdESSignatureHelper.SetSigningInfo(request, cmsSignature.Data, signAttributes.Certificate);

                var response = await this.cardRepository.RequestAsync(request, cancellationToken);
                var result = response.ValidationResult.Build();
                if (!result.IsSuccessful)
                {
                    throw new ValidationException(result);
                }

                var cadesSignature = NotNullOrThrow(CAdESSignatureHelper.GetSignedData(response));
                var b64Signature = Convert.ToBase64String(NotNullOrThrow(cadesSignature.Signature));
                SignatureAttributes signAttrs = await this.edsProvider.GetSignatureAttributesFromSignatureAsync(b64Signature, cancellationToken: cancellationToken);
                ThrowIfNull(signAttrs.Certificate, "Signing certificate not found in signed data.");

                var cert = new X509Certificate2(signAttrs.Certificate);

                var subjectName = EDSCertificateHelper.GetSubjectNameAdvanced(cert.Subject);
                var issuerName = EDSCertificateHelper.ParseSubject(cert.Issuer, EDSCertificateHelper.IssuerNameFindString);
                var company = EDSCertificateHelper.ParseSubject(cert.Subject, EDSCertificateHelper.CompanyFindString);

                var certData = new CertDataInOperationResponse
                {
                    Company = company,
                    SubjectName = subjectName,
                    SerialNumber = cert.SerialNumber,
                    IssuerName = issuerName,
                    ValidFrom = GetUnixTimeSeconds(cert.NotBefore),
                    ValidTo = GetUnixTimeSeconds(cert.NotAfter),
                    Certificate = Convert.ToBase64String(signAttrs.Certificate, 0, signAttrs.Certificate.Length),
                    Thumbprint = cert.Thumbprint,
                    Comment = null
                };

                static string GetUnixTimeSeconds(DateTime dt) =>
                    new DateTimeOffset(DateTime.SpecifyKind(dt, DateTimeKind.Utc)).ToUnixTimeSeconds().ToString();

                var operationResponse = new OperationResponse
                {
                    Info =
                    {
                        ["CertData"] = certData,
                        ["SignedData"] = new SignedDataResult
                        {
                            Signature = b64Signature,
                            Type = cadesSignature.SignatureType,
                            Profile = cadesSignature.SignatureProfile
                        }.ToSerializedDictionary()
                    }
                };

                await this.operationRepository.CompleteAsync(
                    operationId,
                    response: operationResponse,
                    cancellationToken: cancellationToken);

                return true;
            }
            finally
            {
                await this.lockingStrategy.ReleaseWriterLockAsync(operationId);
            }
        }

        /// <summary>
        /// Запрос на проверку подписи.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="parameters">Список подписей с их идентификаторами.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Список с результатами проверки подписи с привязкой к идентификаторам подписей.</returns>
        // POST mobile/verify
        [HttpPost("verify"), TypedJsonBody(ConvertPascalCasing = true)]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<VerifyResponse>>> PostVerify(
            [FromQuery] Guid operationId,
            [FromBody] List<VerifyRequest> parameters,
            CancellationToken cancellationToken = default)
        {
            var tokens = await this.deskiMobileTokenManager.GetTokenAsync(this.Request, operationId, this.sessionServer, cancellationToken);
            if (!tokens.HasAccess(DeskiMobileTokenPermissionFlags.Verify))
            {
                return this.Forbid("Not enough permissions to verify signature.");
            }

            await using var _ = SessionContext.Create(tokens.SessionToken);
            tokens.ApplySessionToken?.Invoke();

            var result = await this.lockingStrategy.ObtainWriterLockAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            try
            {
                var state = await this.operationRepository.GetStateAsync(operationId, cancellationToken);
                ValidateState(state, OperationState.InProgress);

                // TODO #2097 добавить html представление подписи для отображения в deskiMobile.
                var htmlText = "";
                string sanitizedText = string.IsNullOrEmpty(htmlText) ? string.Empty : (this.htmlSanitizer?.SanitizeDocument(htmlText) ?? htmlText);

                var infos = new List<SignatureValidationInfoItemsResult>();
                var response = new List<VerifyResponse>();
                foreach (var item in parameters)
                {
                    ThrowIfNullOrEmpty(item.Signature);

                    var validateResult = await this.edsProvider.ValidateDocumentAsync(
                        item.Signature, SignatureType.None, SignatureProfile.Bes, cancellationToken: cancellationToken);

                    var responseItem = new VerifyResponse
                    {
                        Message = JsonConvert.SerializeObject(validateResult),
                        ID = item.ID,
                        Html = sanitizedText
                    };

                    infos.Add(new SignatureValidationInfoItemsResult { ID = item.ID, ValidationInfo = validateResult });
                    response.Add(responseItem);
                }

                var operationResponse = new OperationResponse
                {
                    Info =
                    {
                        ["SignatureValidationInfo"] = infos,
                        ["SignatureValidationData"] = new SignatureValidationDataResult { ShowValidationDialog = true }.ToSerializedDictionary()
                    }
                };

                await this.operationRepository.CompleteAsync(operationId, response: operationResponse, cancellationToken: cancellationToken);

                return response;
            }
            finally
            {
                await this.lockingStrategy.ReleaseWriterLockAsync(operationId);
            }
        }

        /// <summary>
        /// Запрос на отмену операции.
        /// </summary>
        /// <param name="operationId">ID операции.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>true - при успешном завершении операции, иначе throw.</returns>
        // POST mobile/cancel
        [HttpPost("cancel")]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> PostCancel(
            [FromQuery] Guid operationId,
            CancellationToken cancellationToken = default)
        {
            var tokens = await this.deskiMobileTokenManager.GetTokenAsync(this.Request, operationId, this.sessionServer, cancellationToken);
            if (!tokens.HasAccess(DeskiMobileTokenPermissionFlags.CancelOperation))
            {
                return this.Forbid("Not enough permissions to cancel operation.");
            }

            await using var _ = SessionContext.Create(tokens.SessionToken);
            tokens.ApplySessionToken?.Invoke();

            var result = await this.lockingStrategy.ObtainWriterLockAsync(operationId, cancellationToken);
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            try
            {
                var state = await this.operationRepository.GetStateAsync(operationId, cancellationToken);
                ValidateState(state, OperationState.InProgress);

                var operationResponse = new OperationResponse
                {
                    Info =
                    {
                        ["StatusData"] = new StatusDataResult { Canceled = true }.ToSerializedDictionary()
                    }
                };

                await this.operationRepository.CompleteAsync(operationId, response: operationResponse, cancellationToken: cancellationToken);

                return true;
            }
            finally
            {
                await this.lockingStrategy.ReleaseWriterLockAsync(operationId);
            }
        }

        #endregion

        #region Private methods

        private static void ValidateState([NotNull] OperationState? state, OperationState? expectedState = null)
        {
            if (state is null)
            {
                throw new InvalidOperationException("OperationState is null.");
            }

            if (expectedState.HasValue && state != expectedState.Value)
            {
                const string message = "Server may have canceled the operation earlier. Please, restart the operation.";
                throw new InvalidOperationException($"Operation expected to be in state {expectedState} but was in state {state}. {message}");
            }
        }

        private static void ValidateOperation([NotNull] IOperation? operation, OperationState? expectedState = null)
        {
            if (operation is null)
            {
                throw new InvalidOperationException("Operation is null. Server may have canceled the operation earlier. Please, restart the operation.");
            }

            ThrowIfNull(operation.Request);
            ValidateState(operation.State, expectedState);
        }

        private static string GetOperationType(IOperation operation) =>
            operation.Request?.Info.TryGet<string>("OperationType")?.ToLowerInvariant();

        private static OperationParams GetOperationParams(IOperation operation) =>
            NotNullOrThrow(operation.Request?.Info.Get<Dictionary<string, object>>("FileRequest").FromSerializedDictionary<OperationParams>());

        /// <summary>
        /// Получение всех полей из карточки с настройками сервера.
        /// </summary>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Настройки сервера.</returns>
        private async ValueTask<Dictionary<string, object>> GetServerInstancesFieldsAsync(CancellationToken cancellationToken = default)
        {
            var card = await this.cardCache.Cards.GetAsync(CardHelper.ServerInstanceTypeName, cancellationToken);
            return card.GetValue().Sections["ServerInstances"].RawFields;
        }

        /// <summary>
        /// Получение времени жизни Jwt токена, используемого для взаимодействия с мобильным приложением, из карточки с настройками сервера.
        /// </summary>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Время жизни Jwt токена.</returns>
        private async ValueTask<DateTime> GetDeskiMobileJwtLifeTimeAsync(CancellationToken cancellationToken = default)
        {
            var fields = await this.GetServerInstancesFieldsAsync(cancellationToken);
            return fields.Get<DateTime>("DeskiMobileJwtLifeTime");
        }

        /// <summary>
        /// Получение флага доступности мобильного приложения из карточки с настройками сервера.
        /// </summary>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>true - если флаг для работы с мобильным приложением установлен, иначе - false.</returns>
        private async ValueTask<bool> IsDeskiMobileEnabledAsync(CancellationToken cancellationToken = default)
        {
            var fields = await this.GetServerInstancesFieldsAsync(cancellationToken);
            return fields.Get<bool>("DeskiMobileEnabled");
        }

        #endregion
    }
}
