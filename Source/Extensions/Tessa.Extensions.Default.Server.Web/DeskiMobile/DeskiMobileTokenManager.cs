using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using Tessa.Extensions.Default.Server.Web.DeskiMobile.Models;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Operations;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    /// <inheritdoc cref="IDeskiMobileTokenManager" />
    public class DeskiMobileTokenManager : IDeskiMobileTokenManager
    {
        #region Constructors

        /// <summary>
        /// Конструктор запрашивает зависимости от контейнера ASP.NET Core, у которого они отличаются от зависимостей в UnityContainer.
        /// </summary>
        /// <param name="tessaServerSettings">Серверные настройки.</param>
        /// <param name="operationRepository">Репозиторий, управляющий операциями.</param>
        /// <param name="sessionServer">Объект, обеспечивающий взаимодействие с сессиями на сервере.</param>
        public DeskiMobileTokenManager(
            ITessaServerSettings tessaServerSettings,
            IOperationRepository operationRepository,
            ISessionServer sessionServer)
        {
            this.tessaServerSettings = tessaServerSettings;
            this.operationRepository = operationRepository;
            this.sessionServer = sessionServer;
        }

        #endregion

        #region Constants

        /// <summary>
        /// Имя используемой схемы проверки подлинности.
        /// </summary>
        public const string AuthenticationScheme = "Bearer";

        #endregion

        #region Fields

        private readonly IOperationRepository operationRepository;

        private readonly ITessaServerSettings tessaServerSettings;

        private readonly ISessionServer sessionServer;

        #endregion

        #region Public methods

        /// <inheritdoc />
        public string CreateToken(
            TimeSpan jwtLifeTime, Guid sessionID,
            Guid operationID, Guid fileVersionID, DeskiMobileTokenPermissionFlags flags)
        {
            // Формирование кастомных клеймов.
            var claims = new List<Claim>(4)
            {
                new Claim(DeskiMobileClaimNames.SID, sessionID.ToString("N"), ClaimValueTypes.String),
                new Claim(DeskiMobileClaimNames.OperationID, operationID.ToString("N"), ClaimValueTypes.String),
                new Claim(DeskiMobileClaimNames.VersionID, fileVersionID.ToString("N"), ClaimValueTypes.String),
                new Claim(DeskiMobileClaimNames.Access, flags.ToString("D"), ClaimValueTypes.Integer32),
            };
            var identity = new ClaimsIdentity(new GenericIdentity("deski_mobile"), claims);

            // Подписание ключом SignatureKey.
            var securityKey = new SymmetricSecurityKey(this.tessaServerSettings.SignatureKey);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Формирование токена.
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(
                issuer: this.tessaServerSettings.ServerCode,
                audience: "deski",
                subject: identity,
                expires: DateTime.UtcNow.Add(jwtLifeTime),
                signingCredentials: signingCredentials);

            // создаем JWT-токен
            return handler.WriteToken(token);
        }

        /// <inheritdoc />
        public async Task<MobileTokens> GetTokenAsync(
            HttpRequest request,
            Guid operationID,
            ISessionServer sessionServer,
            CancellationToken cancellationToken = default)
        {
            const string schemeWithSpace = AuthenticationScheme + " ";
            if (request.Headers is null)
            {
                throw new InvalidOperationException("Request header is null.");
            }

            string? tokenHeader;
            var jwtHeaderToken = request.Headers.TryGetValue(HeaderNames.Authorization, out var header)
                && (tokenHeader = header.FirstOrDefault(i => i is not null && i.StartsWith(schemeWithSpace, StringComparison.OrdinalIgnoreCase))) is not null
                    ? tokenHeader[schemeWithSpace.Length..]
                    : null;

            if (string.IsNullOrWhiteSpace(jwtHeaderToken))
            {
                throw new InvalidOperationException("JWT is null or empty.");
            }

            TokenInfo jwtToken;
            ISessionToken sessionToken;
            await using (SessionContext.Create(Session.CreateSystemToken(this.tessaServerSettings)))
            {
                jwtToken = this.VerifyJwtToken(jwtHeaderToken);
                if (jwtToken.OperationID != operationID)
                {
                    throw new InvalidOperationException("JWT does not match this operation.");
                }

                var operation = await this.GetOperationAsync(operationID, cancellationToken);
                var sessionTokenStr = operation.Request?.Info.TryGet<string>("Token");
                sessionToken = await this.ValidateSessionTokenAsync(NotNullOrThrow(sessionTokenStr), jwtToken.SID);
            }

            return new MobileTokens(jwtToken, sessionToken)
            {
                ApplySessionToken = () => sessionServer.ApplyTokenParameters(sessionToken)
            };
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Метод валидирует JWT токен.
        /// </summary>
        private TokenInfo VerifyJwtToken(string token)
        {
            ThrowIfNullOrWhiteSpace(token);

            var securityKey = new SymmetricSecurityKey(this.tessaServerSettings.SignatureKey);

            // Параметры для проверки токена
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = this.tessaServerSettings.ServerCode,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                ClockSkew = TimeSpan.Zero
            };

            // Проверяем токен
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out _);
            return new TokenInfo(claimsPrincipal.Claims);
        }

        /// <summary>
        /// Метод проверяет наличие операции с идентификатором <paramref name="operationID"/> и возвращает его.
        /// </summary>
        private async Task<IOperation> GetOperationAsync(Guid operationID, CancellationToken cancellationToken = default)
        {
            IOperation operation = await this.operationRepository.TryGetAsync(operationID, cancellationToken: cancellationToken);
            if (operation is null)
            {
                var validationResult = new ValidationResultBuilder();
                validationResult.Add(
                    DeskiMobileValidationKeys.OperationNotFound,
                    ValidationResultType.Error,
                    "Operation with operationID=" + operationID + " not found.");
                throw new ValidationException(validationResult.Build());
            }

            if (operation.Request is null)
            {
                throw new InvalidOperationException("OperationRequest is null.");
            }

            return operation;
        }

        /// <summary>
        /// Метод проверяет сессионный токен и возвращает его.
        /// </summary>
        private async Task<ISessionToken> ValidateSessionTokenAsync(string typedJsonToken, Guid sessionID)
        {
            var storage = SessionToken.DeserializeFromStorage(NotNullOrThrow(StorageHelper.DeserializeFromTypedJson(typedJsonToken)));
            var sessionToken = await this.sessionServer.ValidateAndGetSessionAsync(storage);
            if (sessionToken.SessionID != sessionID)
            {
                throw new InvalidOperationException(
                    $"SessionID from JWT ID={sessionToken.SessionID:B} doesn't equal to SessionID from operation.");
            }

            return sessionToken;
        }

        #endregion
    }
}
