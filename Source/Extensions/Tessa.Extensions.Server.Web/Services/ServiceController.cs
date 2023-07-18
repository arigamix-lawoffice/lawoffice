using System;
using System.Net;
using System.Net.Mime;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tessa.Cards;
using Tessa.Extensions.Shared.Services;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Operations;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Web;
using Tessa.Web.Client;
using Tessa.Web.Serialization;
using Tessa.Web.Services;
using Unity;

namespace Tessa.Extensions.Server.Web.Services
{
    /// <summary>
    /// Контроллер, для которого задан базовый путь "service". Является примером реализации сервисов в рамках приложения TESSA.
    /// </summary>
    /// <remarks>
    /// ВАЖНО: Это просто пример того, как можно написать контроллер с какой-то кастомной логикой.
    /// Вы можете его удалить, и добавить сколько угодно своих классов-контроллеров, наследуемых от <see cref="Controller"/>
    /// или <see cref="Controller"/>, имеющих разные маршруты (адреса) в атрибуте <see cref="RouteAttribute"/>, разные зависимости в конструкторе.
    ///
    /// Ваши контроллеры можно расположить в разных сборках, помимо Tessa.Extensions.Server.Web, путь к сборкам указывается
    /// в <c>app.json</c> веб-сервиса по ключу <c>WebControllers</c>.
    /// </remarks>
    [Route("service"), AllowAnonymous, ApiController]
    [ProducesErrorResponseType(typeof(PlainValidationResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public sealed class ServiceController :
        Controller
    {
        #region Constructors

        /// <summary>
        /// Конструктор запрашивает зависимости от контейнера ASP.NET Core, у которого они отличаются от зависимостей в UnityContainer.
        /// </summary>
        /// <param name="serverSettings">Настройки TESSA на сервере, которые выносятся в конфигурационный файл.</param>
        /// <param name="sessionServer">Объект, обеспечивающий взаимодействие с сессиями на сервере.</param>
        /// <param name="sessionService">Сервис, управляющий открытыми сессиями.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="unityContainer">Контейнер Unity.</param>
        /// <param name="hostEnvironment">Информация по среде выполнения ASP.NET Core. Запрошена для примера, обычно не требуется.</param>
        /// <param name="options">Настройки веб-сервиса.</param>
        public ServiceController(
            ITessaServerSettings serverSettings,
            ISessionServer sessionServer,
            ISessionService sessionService,
            IDbScope dbScope,
            [Dependency(CardRepositoryNames.Extended)]
            ICardRepository cardRepository,
            IUnityContainer unityContainer,
            IWebHostEnvironment hostEnvironment,
            IOptions<WebOptions> options)
        {
            this.serverSettings = NotNullOrThrow(serverSettings);
            this.sessionServer = NotNullOrThrow(sessionServer);
            this.sessionService = NotNullOrThrow(sessionService);
            this.dbScope = NotNullOrThrow(dbScope);
            this.cardRepository = NotNullOrThrow(cardRepository);
            this.unityContainer = NotNullOrThrow(unityContainer);
            this.hostEnvironment = NotNullOrThrow(hostEnvironment);
            this.options = NotNullOrThrow(options);
        }

        #endregion

        #region Fields

        private readonly ITessaServerSettings serverSettings;

        private readonly ISessionServer sessionServer;

        private readonly ISessionService sessionService;

        private readonly IDbScope dbScope;

        private readonly ICardRepository cardRepository;

        private readonly IUnityContainer unityContainer;

        private readonly IWebHostEnvironment hostEnvironment;

        private readonly IOptions<WebOptions> options;

        #endregion

        #region Private Methods

        private string GetLoadedExtensions() => string.Join(Environment.NewLine, this.unityContainer.ResolveAssemblyInfo().ServerExtensions);

        #endregion

        #region Controller Methods

        /*
         * Метод доступен по базовому адресу контроллера, не требует авторизации и не обращается к сессии.
         * Для проверки функционирования сервиса перейдите по URL вида: https://localhost/tessa/web/service
         *
         * Указываем, что метод может вернуть Json, если возникнет исключение - оно будет упаковано в PlainValidationResult.
         */
        /// <summary>
        /// Возвращает текстовое описание для конфигурации веб-сервиса, если в конфигурации
        /// установлена настройка <c>HealthCheckIsEnabled</c> равной <c>true</c>.
        /// </summary>
        /// <returns>Текстовое описание для конфигурации веб-сервиса.</returns>
        // GET service
        [HttpGet, Produces(MediaTypeNames.Text.Plain, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Get() =>
            this.options.Value.HealthCheckIsEnabled
                ? $"ARIGAMIX, build {BuildInfo.Version} of {FormattingHelper.FormatDate(BuildInfo.Date, convertToLocal: false)}{Environment.NewLine}" +
                $"Environment: \"{this.hostEnvironment.EnvironmentName}\".{Environment.NewLine}{Environment.NewLine}" +
                $"Running on {EnvironmentHelper.OSQualifiedFriendlyName}{Environment.NewLine}" +
                $"{EnvironmentHelper.NetRuntimeFriendlyName}{Environment.NewLine}{Environment.NewLine}" +
                $"Server extensions:{Environment.NewLine}{this.GetLoadedExtensions()}"
                : "Health check is disabled in configuration";


        /*
         * Метод для входа по паре логин/пароль. Здесь используется клиентская информация по умолчанию и идентификатор неизвестного приложения;
         * обычно вместо этого задают конкретное приложение и передают параметры.
         *
         * Метод возвращает строку с токеном, которую надо передавать в следующие методы
         * или же использоваться API TESSA для проброса токена в HTTP-заголовок "Tessa-Session".
         *
         * Указываем TypedJsonBody, чтобы можно было получать как типизированный, так и нетипизированный json в качестве параметра.
         * ConvertPascalCasing конвертирует в названиях свойств объектов первую строчную букву в прописную. Например: userName => UserName.
         *
         * Рекомендуется использовать метод открытия сессии из REST API, если это возможно: /api/v1/sessions/open
         */
        /// <summary>
        /// Открывает сессию для входа пользователя по паре логин/пароль. Возвращает строку, содержащую токен сессии,
        /// который должен передаваться во все другие запросы к веб-сервисам.
        /// </summary>
        /// <param name="parameters">Параметры входа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Строка, содержащая токен сессии.</returns>
        // POST service/login
        [HttpPost("login"), TypedJsonBody(ConvertPascalCasing = true)]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Text.Xml, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> PostLogin(
            [FromBody] IntegrationLoginParameters parameters,
            CancellationToken cancellationToken = default)
        {
            ISessionToken token = await this.sessionServer
                .OpenSessionAsync(
                    NotEmptyOrThrow(parameters.Login),
                    parameters.Password,
                    ApplicationIdentifiers.Other,
                    ApplicationLicenseType.Client,
                    SessionServiceType.WebClient,
                    SessionClientParameters.CreateCurrent(),
                    cancellationToken: cancellationToken);

            var authToken = token.SerializeToXml(SessionSerializationOptions.Auth);

            // сервер сообщает версию и токен только после того, как сессия была успешно открыта, т.е. это пользователь системы, а не случайный человек
            this.Response.Headers[SessionHttpRequestHeader.Version] = BuildInfo.VersionObject.ToString();
            this.Response.Headers[SessionHttpRequestHeader.Session] = authToken;

            return this.Ok(authToken);
        }


        /*
         * Метод для входа, используя windows-аутентификацию. Здесь используется клиентская информация по умолчанию и идентификатор неизвестного приложения;
         * обычно вместо этого задают конкретное приложение и передают параметры.
         *
         * Метод возвращает строку с токеном, которую надо передавать в следующие методы
         * или же использоваться API TESSA для проброса токена в HTTP-заголовок "Tessa-Session".
         *
         * Рекомендуется использовать метод открытия сессии из REST API, если это возможно: /api/v1/sessions/open
         */
        /// <summary>
        /// Открывает сессию для входа пользователя, используя windows-аутентификацию. Возвращает строку, содержащую токен сессии,
        /// который должен передаваться во все другие запросы к веб-сервисам.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Строка, содержащая токен сессии.</returns>
        // POST service/winlogin
        [HttpPost("winlogin"), Produces(MediaTypeNames.Text.Xml, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> PostWinLogin(CancellationToken cancellationToken = default)
        {
            IIdentity? identity = this.HttpContext.User.Identity;
            if (identity is null)
            {
                if (this.options.Value.KerberosIsEnabled)
                {
                    this.Response.Headers.Add("WWW-Authenticate", new[] { "Negotiate" });
                }

                throw new SessionException(
                    SessionExceptionCode.WindowsAuthFailed,
                    "Windows authentication failed to link with user account.");
            }

            string? accountName;
            if (!identity.IsAuthenticated || string.IsNullOrEmpty(accountName = identity.Name))
            {
                if (this.options.Value.KerberosIsEnabled)
                {
                    this.Response.Headers.Add("WWW-Authenticate", new[] { "Negotiate" });
                }

                throw new SessionException(
                    SessionExceptionCode.ExpectedWindowsAuth,
                    "Use Windows authentication instead of anonymous.");
            }

            ISessionToken token = await this.sessionServer
                .OpenSessionAsync(
                    accountName,
                    null,
                    ApplicationIdentifiers.Other,
                    ApplicationLicenseType.Client,
                    SessionServiceType.WebClient,
                    WebClientHelper.GetSessionClientParameters(this.HttpContext),
                    UserLoginType.Windows,
                    true,
                    cancellationToken);

            var authToken = token.SerializeToXml(SessionSerializationOptions.Auth);

            // сервер сообщает версию и токен только после того, как сессия была успешно открыта, т.е. это пользователь системы, а не случайный человек
            this.Response.Headers[SessionHttpRequestHeader.Version] = BuildInfo.VersionObject.ToString();
            this.Response.Headers[SessionHttpRequestHeader.Session] = authToken;

            return this.Ok(authToken);
        }


        /*
         * Метод для закрытия сессии. Токен может содержаться как в HTTP-заголовке "Tessa-Session",
         * так и в параметре адресной строки <c>?token=...</c>.
         */
        /// <summary>
        /// Закрывает сессию с указанием строки с токеном сессии. Токен возвращается методом открытия сессии <see cref="PostLogin"/>.
        /// Методу не требуется наличие информации по сессии в HTTP-заголовке, если указан токен <paramref name="token"/>.
        /// </summary>
        /// <param name="token">Токен закрываемой сессии.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        // POST service/logout?token=...
        [HttpPost("logout"), SessionMethod]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PostLogout(
            [FromQuery, SessionToken] string? token = null,
            CancellationToken cancellationToken = default)
        {
            await this.sessionService.CloseSessionWithTokenAsync(token, cancellationToken);
            return this.NoContent();
        }


        /*
         * Атрибут SessionMethod нужен для того, чтобы выполнять действия в пределах сессии.
         * Если в атрибуте указать UserAccessLevel.Administrator, то метод сможет вызвать только администратор Tessa.
         *
         * Если убрать атрибут, то любая внешняя система может вызвать метод от любого пользователя,
         * а также не будет доступна информация по текущей сессии. См. метод GetDataWithoutCheckingToken.
         *
         * В необязательном параметре "token" содержится строка с сериализованным токеном сессии.
         * Это задаётся атрибутом [SessionToken]. Если строка пустая, то используется HTTP-заголовок "Tessa-Session".
         *
         * Здесь и ниже указываем необходимость сессии администратора UserAccessLevel.Administrator,
         * чтобы обычные пользователи не могли получить к нему доступ. В реальных сценариях к серверу могут
         * подключаться различные клиенты, которые могут быть не объявлены администраторами системы.
         */
        /// <summary>
        /// Выполняет некоторый запрос для заданного параметра и возвращает результат.
        /// Требует наличия токена сессии в HTTP-заголовке <c>Tessa-Session</c>.
        /// Это метод для тестирования возможностей REST веб-сервиса. Метод требует наличия сессии.
        /// </summary>
        /// <param name="parameter">Параметр запроса.</param>
        /// <param name="token">Токен текущей сессии.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        /// <remarks>
        /// Информация по HTTP-заголовкам, используемым платформой, доступна в методе <see cref="SessionHttpRequestHeader"/>.
        /// </remarks>
        // GET service/data?p=...&token=...
        [HttpGet("data"), SessionMethod(UserAccessLevel.Administrator), Produces(MediaTypeNames.Text.Plain, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetData(
            [FromQuery(Name = "p")] string parameter,
            [FromQuery, SessionToken] string? token = null,
            CancellationToken cancellationToken = default)
        {
            // максимум первые десять символов
            string data = string.IsNullOrEmpty(parameter)
                ? parameter
                : parameter[..Math.Min(10, parameter.Length)];

            await using (this.dbScope.Create())
            {
                DbManager db = this.dbScope.Db;

                // по умолчанию возвращается this.Ok(...)
                return await db
                    .SetCommand(
                        // запрос "SELECT @Data"
                        this.dbScope.BuilderFactory
                            .Select().P("Data")
                            .Build(),
                        db.Parameter("Data", data))
                    .LogCommand()
                    .ExecuteAsync<string>(cancellationToken) ?? string.Empty;
            }
        }


        /*
         * Метод не использует сессию через атрибут [SessionMethod]
         * и выполняет любые действия от пользователя "System".
         *
         * Для таких методов крайне рекомендуется реализовать другой механизм аутентификации или ограничить сетевую доступность метода.
         */
        /// <summary>
        /// Выполняет некоторый запрос для заданного параметра и возвращает результат.
        /// Это метод для тестирования возможностей REST веб-сервиса. Метод не требует наличия сессии.
        /// </summary>
        /// <param name="parameter">Параметр запроса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // GET service/data-without-login
        [HttpGet("data-without-login"), Produces(MediaTypeNames.Text.Plain, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetDataWithoutCheckingToken(
            [FromQuery(Name = "p")] string parameter,
            CancellationToken cancellationToken = default)
        {
            await using (SessionContext.Create(Session.CreateSystemToken(this.serverSettings)))
            {
                // код внутри using будет выполняться от имени пользователя System, например:
                //return await this.GetData(parameter, null, cancellationToken);

                // но по умолчанию вернём захардкоженную строку, чтобы нельзя было за-DoS-ить сервер запросами,
                // каждый из которых обращается в базу
                return "Not supported";
            }
        }


        /*
         * Метод для загрузки карточки, используя сериализацию типизированного JSON.
         * Метод нельзя сделать GET, поскольку он содержит тело запроса, поэтому это POST.
         *
         * ConvertPascalCasing не используется, поскольку в request.Info могут быть ключи,
         * первые буквы которых должны остаться строчными.
         *
         * И сериализация, и передача сессии обычно выполняются средствами API.
         */
        /// <summary>
        /// Загружает карточку по заданному запросу для desktop-клиента.
        /// Метод идентичен типовому методу загрузки карточки в контроллере <c>CardsController</c>.
        /// Это метод для тестирования возможностей REST веб-сервиса. Метод требует наличия сессии.
        /// </summary>
        /// <param name="request">Запрос на загрузку карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Ответ на запрос на загрузку карточки.</returns>
        // POST service/cards/get
        [HttpPost("cards/get"), SessionMethod(UserAccessLevel.Administrator), TypedJsonBody]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CardGetResponse>> PostGetCard(
            [FromBody] CardGetRequest request,
            CancellationToken cancellationToken = default)
        {
            // если не установить ServiceType, то запрос выполнится с пропуском ряда проверок на валидность запроса и прав пользователя
            request.ServiceType = CardServiceType.Client;

            var response = await this.cardRepository.GetAsync(request, cancellationToken);

            // для того, чтобы ответ на запрос содержал информацию по типам данных, надо вызвать метод-расширение TypedJsonAsync
            return await this.TypedJsonAsync(response, cancellationToken: cancellationToken);
        }


        /// <summary>
        /// Открывает карточку и возвращает JSON с типизированной структурой объекта <see cref="CardGetResponse"/>.
        /// Токен сессии передаётся в HTTP-заголовке "Tessa-Session".
        /// </summary>
        /// <param name="id">Идентификатор карточки <see cref="Guid"/>. Передаётся в адресной строке.</param>
        /// <param name="type">Алиас типа карточки. Передаётся как параметр в адресной строке. Необязательный параметр.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Ответ на запрос на загрузку карточки, сериализованный как типизированный JSON.</returns>
        // GET service/cards/{id}?type=...
        [HttpGet("cards/{id:guid}"), SessionMethod(UserAccessLevel.Administrator), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CardGetResponse>> GetCard(
            [FromRoute] Guid id,
            [FromQuery] string? type = null,
            CancellationToken cancellationToken = default)
        {
            // если не установить ServiceType, то запрос выполнится с пропуском ряда проверок на валидность запроса и прав пользователя
            var request = new CardGetRequest { CardID = id, CardTypeName = type, ServiceType = CardServiceType.Client };

            var response = await this.cardRepository.GetAsync(request, cancellationToken);

            // для того, чтобы ответ на запрос содержал информацию по типам данных, надо вызвать метод-расширение TypedJsonAsync
            return await this.TypedJsonAsync(response, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Возвращает <c>ValidationResult</c> с <c>ValidationKeys.OperationIsUnavailable</c>
        /// и кодом <c>HttpStatusCode.Forbidden</c>  - 403.
        /// </summary>
        /// <returns>Ошибка.</returns>
        // GET service
        [HttpGet("get-validation-result-error")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public void GetValidationResultError()
        {
            ValidationResult result = ValidationSequence.Begin()
                .SetObjectName(this)
                .Error(ValidationKeys.OperationIsUnavailable, OperationTypes.Other)
                .End().Build();
            throw new ValidationException(result) { StatusCode = HttpStatusCode.Forbidden };
        }

        #endregion
    }
}
