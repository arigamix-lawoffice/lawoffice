#nullable enable

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Tessa.Platform;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.OnlyOffice.Token
{
    /// <inheritdoc cref="IOnlyOfficeTokenManager"/>
    public sealed class OnlyOfficeTokenManager :
        IOnlyOfficeTokenManager
    {
        #region Fields

        private readonly ITessaServerSettings tessaServerSettings;

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр объекта с указанием значений его свойств.
        /// </summary>
        /// <param name="tessaServerSettings">Серверные настройки.</param>
        /// <param name="session">Сессия текущего сотрудника.</param>
        /// <param name="tdnKeyProvider">Сервис, обеспечивающий работу с ключами в PEM формате.</param>
        /// <param name="tdnKeyManager">Объект предоставляющий доступ к ключам TDN сервера приложений TESSA.</param>
        /// <param name="cardTdnNodeCache">Кэш карточек узлов распределённого хранилища контента.</param>
        public OnlyOfficeTokenManager(
            ITessaServerSettings tessaServerSettings)
        {
            this.tessaServerSettings = tessaServerSettings ?? throw new ArgumentNullException(nameof(tessaServerSettings));
        }

        #endregion

        #region IOnlyOfficeTokenManager Members

        /// <inheritdoc/>
        public string CreateToken(
            Guid ID,
            Guid versionID,
            int expiresPeriod,
            OnlyOfficeTokenPermissionFlags flags)
        {
            // Формирование кастомных клеймов.
            var claims = new List<Claim>(6)
            {
                new Claim(OnlyOfficeClaimNames.ID, ID.ToString(OnlyOfficeTokenHelper.GuidFormat), ClaimValueTypes.String),
                new Claim(OnlyOfficeClaimNames.VersionID, versionID.ToString(OnlyOfficeTokenHelper.GuidFormat), ClaimValueTypes.String),
                new Claim(OnlyOfficeClaimNames.Access, flags.ToString(OnlyOfficeTokenHelper.EnumFormat), ClaimValueTypes.Integer32),
            };

            var identity = new ClaimsIdentity(
                new GenericIdentity(OnlyOfficeTokenHelper.UserName),
                claims);

            // Подписание ключом SignatureKey.
            var securityKey = new SymmetricSecurityKey(this.tessaServerSettings.SignatureKey);
            var signingCredentials = new SigningCredentials(securityKey, OnlyOfficeTokenHelper.FileTokenSignatureAlgorithm);

            // Формирование токена.
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(
                issuer: this.tessaServerSettings.ServerCode,
                audience: OnlyOfficeTokenHelper.UserName,
                subject: identity,
                expires: DateTime.UtcNow.AddMinutes(expiresPeriod),
                signingCredentials: signingCredentials);

            return handler.WriteToken(token);
        }

        /// <inheritdoc/>
        public OnlyOfficeJwtTokenInfo? VerifyToken(
            string token)
        {
            Check.ArgumentNotNullOrWhiteSpace(token, nameof(token));

            var securityKey = new SymmetricSecurityKey(this.tessaServerSettings.SignatureKey);

            // Проверяем код сервера и время жизни токена.
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = this.tessaServerSettings.ServerCode,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidAlgorithms = new[] { OnlyOfficeTokenHelper.FileTokenSignatureAlgorithm },
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var validatedToken);

                var tokenInfo = new OnlyOfficeJwtTokenInfo(claimsPrincipal.Claims);

                return tokenInfo.IsValid() ? tokenInfo : null;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }

        #endregion
    }
}
