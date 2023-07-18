using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.MvcCore;
using ITfoxtec.Identity.Saml2.Schemas;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using Microsoft.AspNetCore.WebUtilities;
using NLog;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Roles;
using Tessa.Web.Client;
using Tessa.Web.Client.Services;

namespace Tessa.Extensions.Default.Server.Web.Services
{
    /// <summary>
    /// Предоставляет методы для SSO-авторизации (Single Sign On) по протоколу SAML.
    /// Может использоваться для интеграции с доменом Active Directory Foundation Services.
    /// Используется в web-клиенте TESSA.
    /// </summary>
    public class SamlService : ISamlService
    {
        #region Constructors

        public SamlService(
            IDbScope dbScope,
            ISessionServer sessionServer,
            ICardRepository cardRepository,
            ITessaServerSettings serverSettings)
        {
            this.dbScope = dbScope;
            this.sessionServer = sessionServer;
            this.cardRepository = cardRepository;
            this.serverSettings = serverSettings;
        }

        #endregion

        #region Fields

        private readonly IDbScope dbScope;

        private readonly ISessionServer sessionServer;

        private readonly ICardRepository cardRepository;

        private readonly ITessaServerSettings serverSettings;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Protected Methods

        protected static Saml2Configuration GetConfiguration(ISamlContext context)
        {
            return context.SamlConfig;
        }

        private static void AppendTokenCookie(ISamlContext context, string value, DateTime expires) =>
            WebClientHelper.AppendTokenCookie(
                value,
                expires,
                context.Scope.InstanceName,
                context.Scope.MultipleInstances,
                context.ClientOptions.GuyFawkesAuth,
                context.Options.CookiesSameSite,
                context.Request,
                context.Response,
                !context.IsDevelopmentHotEnvironment);

        #endregion

        #region Public Methods

        /// <summary>
        /// Запрос, возвращающий метаинформацию SAML.
        /// </summary>
        /// <param name="context">Объект, содержающий информацию о текущем контексте выполнения запроса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Метаинформация SAML.</returns>
        public ValueTask<Saml2Metadata> GetMetadataAsync(ISamlContext context, CancellationToken cancellationToken = default)
        {
            Saml2Configuration samlConfig = GetConfiguration(context);
            string siteUrl = context.SiteUrl;
            var entityDescriptor = new EntityDescriptor(samlConfig)
            {
                SPSsoDescriptor = new SPSsoDescriptor
                {
                    AuthnRequestsSigned = true,
                    WantAssertionsSigned = true,
                    SigningCertificates = new[] { samlConfig.SigningCertificate },
                    EncryptionCertificates = new[] { samlConfig.DecryptionCertificate },
                    NameIDFormats = new[] { NameIdentifierFormats.Unspecified, NameIdentifierFormats.Transient },
                    AssertionConsumerServices = new[]
                    {
                        new AssertionConsumerService
                        {
                            Binding = ProtocolBindings.HttpPost,
                            Location = new Uri($"{siteUrl}/SAML/AssertionConsumerService")
                        }
                    },
                    SingleLogoutServices = new[]
                    {
                        new SingleLogoutService
                        {
                            Binding = ProtocolBindings.HttpPost,
                            Location = new Uri($"{siteUrl}/SAML/SingleLogout"),
                            ResponseLocation = new Uri($"{siteUrl}/SAML/LoggedOut")
                        }
                    },
                }
            };
            return new (new Saml2Metadata(entityDescriptor).CreateMetadata());
        }

        /// <summary>
        /// Окно входа в систему для использования совместно с SAML-авторизацией.
        /// </summary>
        /// <param name="context">Объект, содержающий информацию о текущем контексте выполнения SAML-запроса.</param>
        /// <param name="returnUrl">Обратная ссылка, по которой выполняется переход после успешного входа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Объект, содержащий информацию о redirect действии.</returns>
        public ValueTask<Saml2RedirectBinding> LoginAsync(ISamlContext context, string returnUrl, CancellationToken cancellationToken = default)
        {
            Saml2Configuration samlConfig = GetConfiguration(context);
            string base64ReturnUrl = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(returnUrl));
            var binding = new Saml2RedirectBinding();
            binding.SetRelayStateQuery(new Dictionary<string, string> { { "ReturnUrl", base64ReturnUrl } });
            var request = new Saml2AuthnRequest(samlConfig)
            {
                NameIdPolicy = new NameIdPolicy { AllowCreate = true }
            };
            return new (binding.Bind(request));
        }

        /// <summary>
        /// Метод, вызываемый при входе в систему после подтверждения авторизации SAML.
        /// Возвращает результат входа, обычно это редирект на определённую страницу.
        /// </summary>
        /// <param name="context">Объект, содержающий информацию о текущем контексте выполнения SAML-запроса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Redirect URL или <c>null</c> для дефолтного перехода.</returns>
        /// <exception cref="InvalidOperationException">
        /// <para>Возникла одна из следующих ошибок (текст исключения содержит точное описание ошибки):</para>
        /// <para>1) Нельзя найти LoginClaim или EmailClaim в ответе на запрос по авторизации SAML.</para>
        /// <para>2) Пользователь определён как неавторизованный в SAML, несмотря на вызов метода, т.е. свойство ClaimsPrincipal.Identity.IsAuthenticated вернуло false.</para>
        /// <para>3) Нельзя найти пользователя по LoginClaim в справочнике системы, если автоматическое создание пользователей запрещено.</para>
        /// <para>4) Нельзя создать нового пользователя, который отсутствует в справочнике системы по LoginClaim, если автоматическое создание пользователей разрешено.</para>
        /// </exception>
        public async ValueTask<string> AssertionConsumerServiceAsync(ISamlContext context, CancellationToken cancellationToken = default)
        {
            Saml2Configuration samlConfig = GetConfiguration(context);
            var binding = new Saml2PostBinding();
            var saml2AuthnResponse = new Saml2AuthnResponse(samlConfig);

            binding.Unbind(context.Request.ToGenericHttpRequest(), saml2AuthnResponse);

            var principal = await saml2AuthnResponse.CreateSession(
                context.HttpContext,
                lifetime: context.ClientOptions.ExpireTimeSpan,
                isPersistent: context.ClientOptions.SetSAMLCookie,
                claimsTransform: WebClientHelper.TransformPrincipalForSaml);

            if (principal.Identity.IsAuthenticated)
            {
                var loginClaim = principal.Claims.FirstOrDefault(c => c.Type == context.ClientOptions.LoginClaimType);
                if (loginClaim == null || string.IsNullOrEmpty(loginClaim.Value))
                {
                    // не смогли найти login в claims
                    throw new InvalidOperationException("Can't find login claim in SAML authentication response.");
                }

                string login = loginClaim.Value;
                bool isUserExists;

                await using (this.dbScope.Create())
                {
                    DbManager db = this.dbScope.Db;
                    db
                        .SetCommand(
                            this.dbScope.BuilderFactory
                                .Select().C("Login")
                                .From("PersonalRoles").NoLock()
                                .Where().LowerC("Login").Equals().LowerP("Login")
                                .Build(),
                            db.Parameter("Login", login))
                        .LogCommand();

                    await using DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken);
                    isUserExists = await reader.ReadAsync(cancellationToken);
                }

                // пытаемся найти пользователя по email (fallback для старых версий)
                if (!isUserExists && context.ClientOptions.UpdateEmailLoginUsers)
                {
                    var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == context.ClientOptions.EmailClaimType);
                    if (emailClaim == null || string.IsNullOrEmpty(emailClaim.Value))
                    {
                        throw new InvalidOperationException("Can't find email claim in SAML authentication response.");
                    }

                    string email = emailClaim.Value;

                    await using (this.dbScope.Create())
                    {
                        DbManager db = this.dbScope.Db;
                        db
                            .SetCommand(
                                this.dbScope.BuilderFactory
                                    .Select().C("Login")
                                    .From("PersonalRoles").NoLock()
                                    .Where().LowerC("Login").Equals().LowerP("Email")
                                    .Build(),
                                db.Parameter("Email", email))
                            .LogCommand();

                        await using DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken);
                        isUserExists = await reader.ReadAsync(cancellationToken);
                    }

                    // если нашли, то обновляем login пользователя с email на актуальный login
                    if (isUserExists)
                    {
                        logger.Error("ADFS auth: login was changed from \"{0}\" to \"{1}\".", email, login);

                        await using (this.dbScope.Create())
                        {
                            DbManager db = this.dbScope.Db;
                            await db
                                .SetCommand(
                                    this.dbScope.BuilderFactory
                                        .Update("PersonalRoles")
                                        .C("Login").Assign().P("Login")
                                        .Where().LowerC("Login").Equals().LowerP("Email")
                                        .Build(),
                                    db.Parameter("Login", login),
                                    db.Parameter("Email", email))
                                .LogCommand()
                                .ExecuteNonQueryAsync(cancellationToken);
                        }
                    }
                }

                if (!isUserExists)
                {
                    if (!context.ClientOptions.CreateUserAfterAuthenticationIfNotExists)
                    {
                        throw new InvalidOperationException(
                            "Can't find user by login claim from SAML authentication response.");
                    }

                    // проверяем, что все нужные данные есть в ответе
                    Claim emailClaim = principal.Claims.FirstOrDefault(c => c.Type == context.ClientOptions.EmailClaimType);
                    Claim nameClaim = principal.Claims.FirstOrDefault(c => c.Type == context.ClientOptions.NameClaimType);

                    string email = emailClaim != null ? emailClaim.Value : string.Empty;
                    string name = nameClaim != null ? nameClaim.Value : login;

                    // создаем юзера если его нет
                    await using (SessionContext.Create(Session.CreateSystemToken(this.serverSettings)))
                    {
                        var newResponse = await this.cardRepository.NewAsync(
                            new CardNewRequest
                            {
                                ServiceType = CardServiceType.Client,
                                CardTypeID = RoleHelper.PersonalRoleTypeID,
                                CardTypeName = RoleHelper.PersonalRoleTypeName
                            },
                            cancellationToken);

                        if (!newResponse.ValidationResult.IsSuccessful())
                        {
                            logger.LogResult(newResponse.ValidationResult, "Can't create user during ADFS auth (New request): {0:D}");
                            throw new InvalidOperationException("Can't create user during ADFS auth (New request).");
                        }

                        Card card = newResponse.Card;
                        card.ID = Guid.NewGuid();
                        card.Sections["PersonalRoles"].Fields["FirstName"] = name;
                        card.Sections["PersonalRoles"].Fields["Email"] = email;
                        card.Sections["PersonalRoles"].Fields["Login"] = login;

                        var request = new CardStoreRequest
                        {
                            ServiceType = CardServiceType.Client,
                            Card = card,
                            Info = new Dictionary<string, object>()
                        };

                        request.SetADFSAuthenticationResponse(saml2AuthnResponse.ToXml().OuterXml);
                        request.SetAddToRolesIDList(context.ClientOptions.AddNewSAMLUserToRoles);

                        CardStoreResponse storeResponse = await this.cardRepository.StoreAsync(request, cancellationToken);
                        if (!storeResponse.ValidationResult.IsSuccessful())
                        {
                            logger.LogResult(storeResponse.ValidationResult, "Can't create user during ADFS auth (Store request): {0:D}");
                            throw new InvalidOperationException("Can't create user during ADFS auth (Store request).");
                        }
                    }
                }

                var sessionToken = await this.sessionServer.OpenSessionAsync(
                    login,
                    applicationID: ApplicationIdentifiers.WebClient,
                    applicationLicenseType: ApplicationLicenseType.Client,
                    serviceType: SessionServiceType.WebClient,
                    expectedLoginType: UserLoginType.Windows,
                    parameters: WebClientHelper.GetSessionClientParameters(context.HttpContext),
                    skipWindowsLoginValidation: true,
                    cancellationToken: cancellationToken);

                // сервер сообщает версию и токен только после того, как сессия была успешно открыта, т.е. это пользователь системы, а не случайный человек
                context.Response.Headers[SessionHttpRequestHeader.Version] = BuildInfo.VersionObject.ToString();
                context.Response.Headers[SessionHttpRequestHeader.Session] = sessionToken.SerializeToXml(SessionSerializationOptions.Auth);

                AppendTokenCookie(
                    context,
                    WebClientHelper.SerializeToCookie(sessionToken),
                    DateTime.UtcNow.AddYears(1));
            }
            else
            {
                // если пользователь не авторизован, то что-то пошло не так
                throw new InvalidOperationException("User isn't authenticated in AssertionConsumerService call.");
            }

            binding.GetRelayStateQuery().TryGetValue("ReturnUrl", out string returnUrl);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(returnUrl));
            }

            return returnUrl;
        }

        /// <summary>
        /// Выход из системы при авторизации SAML.
        /// </summary>
        /// <param name="context">Объект, содержающий информацию о текущем контексте выполнения SAML-запроса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Объект, содержащий информацию о post действии.</returns>
        public async ValueTask<Saml2PostBinding> LogoutAsync(ISamlContext context, CancellationToken cancellationToken = default)
        {
            Saml2Configuration samlConfig = GetConfiguration(context);
            var binding = new Saml2PostBinding();
            var saml2LogoutRequest = await new Saml2LogoutRequest(samlConfig, context.User)
                .DeleteSession(context.HttpContext);
            return binding.Bind(saml2LogoutRequest);
        }

        /// <summary>
        /// Метод, вызываемый после закрытия сессии, при этом удаляется привязка SAML,
        /// производится запись в cookies о факте закрытия и редирект на основную страницу.
        /// </summary>
        /// <param name="context">Объект, содержающий информацию о текущем контексте выполнения SAML-запроса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Redirect URL или <c>null</c> для дефолтного перехода.</returns>
        public ValueTask<string> LoggedOutAsync(ISamlContext context, CancellationToken cancellationToken = default)
        {
            Saml2Configuration samlConfig = GetConfiguration(context);
            AppendTokenCookie(
                context,
                "closed",
                DateTime.UtcNow.AddDays(-1));

            var binding = new Saml2PostBinding();
            binding.Unbind(context.Request.ToGenericHttpRequest(), new Saml2LogoutResponse(samlConfig));

            // default redirect
            return new (null as string);
        }

        /// <summary>
        /// Выход из системы в SAML, запрошенный пользователем для удаления информации по входу во всех SSO-приложениях,
        /// в т.ч. в системе TESSA.
        /// </summary>
        /// <param name="context">Объект, содержающий информацию о текущем контексте выполнения SAML-запроса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Объект, содержащий информацию о post действии.</returns>
        public async ValueTask<Saml2PostBinding> SingleLogoutAsync(ISamlContext context, CancellationToken cancellationToken = default)
        {
            Saml2Configuration samlConfig = GetConfiguration(context);
            Saml2StatusCodes status;
            var requestBinding = new Saml2PostBinding();
            var logoutRequest = new Saml2LogoutRequest(samlConfig, context.User);

            try
            {
                requestBinding.Unbind(context.Request.ToGenericHttpRequest(), logoutRequest);
                status = Saml2StatusCodes.Success;
                await logoutRequest.DeleteSession(context.HttpContext);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                status = Saml2StatusCodes.RequestDenied;
            }

            var responsebinding = new Saml2PostBinding { RelayState = requestBinding.RelayState };
            var saml2LogoutResponse = new Saml2LogoutResponse(samlConfig)
            {
                InResponseToAsString = logoutRequest.IdAsString,
                Status = status,
            };

            return responsebinding.Bind(saml2LogoutResponse);
        }

        #endregion
    }
}
