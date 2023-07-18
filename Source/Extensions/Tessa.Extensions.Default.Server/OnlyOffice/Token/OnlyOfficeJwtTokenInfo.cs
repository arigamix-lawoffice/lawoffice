#nullable enable

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.OnlyOffice.Token
{
    /// <summary>
    /// Объект, содержащий информацию о JSON Web Token на выполнение операции с контентом версии файла.
    /// </summary>
    public sealed class OnlyOfficeJwtTokenInfo :
        ValidationStorageObject
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OnlyOfficeJwtTokenInfo"/>.
        /// </summary>
        public OnlyOfficeJwtTokenInfo()
            : this(new Dictionary<string, object?>(StringComparer.Ordinal))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OnlyOfficeJwtTokenInfo"/>.
        /// </summary>
        /// <param name="claims">Перечисление утверждений содержащихся в JSON Web Token на выполнение операции с контентом версии файла.</param>
        public OnlyOfficeJwtTokenInfo(
            IEnumerable<Claim> claims)
            : this(claims?.ToDictionary(
                static i => i.Type,
                static j => ConvertValue(j))
                  ?? throw new ArgumentNullException(nameof(claims)))
        {
        }

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public OnlyOfficeJwtTokenInfo(Dictionary<string, object?> storage)
            : base(storage)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор пользователя для которого был выдан JSON Web Token или значение <see langword="null"/>, если токен его не содержит.
        /// </summary>
        public Guid? ID => this.TryGet<Guid?>(OnlyOfficeClaimNames.ID);

        /// <summary>
        /// Возвращает имя пользователя для которого был выдан JSON Web Token.
        /// </summary>
        public string? UserName => this.Get<string>(JwtRegisteredClaimNames.Aud);

        /// <summary>
        /// Возвращает идентификатор версии файла для которого был выдан JSON Web Token.
        /// </summary>
        public Guid VersionID => this.Get<Guid>(OnlyOfficeClaimNames.VersionID);

        /// <summary>
        /// Возвращает разрешённые операции с контентом файла для которого был выдан JSON Web Token.
        /// </summary>
        public OnlyOfficeTokenPermissionFlags Access => this.Get<OnlyOfficeTokenPermissionFlags>(OnlyOfficeClaimNames.Access);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override void ValidateInternal(IValidationResultBuilder validationResult)
        {
            base.ValidateInternal(validationResult);

            ValidationSequence
                .Begin(validationResult)
                .SetObjectName(this)
                .SetResult(ValidationResultType.Error)
                .SetMessage(PropertyIsInvalidOrNotExists)
                .Validate<Guid>(OnlyOfficeClaimNames.ID, ValidateThat.GuidIsNotEmpty, this.ObjectExistsInStorageByKey)
                .Validate<Guid>(OnlyOfficeClaimNames.VersionID, ValidateThat.GuidIsNotEmpty, this.ObjectExistsInStorageByKey)
                .Validate<string?>(JwtRegisteredClaimNames.Aud, ValidateThat.StringIsNotNullOrEmpty, this.ObjectExistsInStorageByKey)
                .SetMessage(PropertyNotExists)
                .Validate(OnlyOfficeClaimNames.Access, this.ObjectExistsInStorageByKey)
                .End();
        }

        #endregion

        #region Private Methods

        private static object? ConvertValue(Claim claim)
        {
            var value = ConvertValueCore(claim);

            return claim.Type switch
            {
                OnlyOfficeClaimNames.ID or OnlyOfficeClaimNames.VersionID =>
                    Guid.Parse((string) value),
                OnlyOfficeClaimNames.Access => (OnlyOfficeTokenPermissionFlags) (int) value,
                _ => value,
            };
        }

        private static object ConvertValueCore(Claim claim)
        {
            return claim.ValueType switch
            {
                ClaimValueTypes.Integer or ClaimValueTypes.Integer32 => Convert.ToInt32(claim.Value),
                ClaimValueTypes.Integer64 => Convert.ToInt64(claim.Value),
                ClaimValueTypes.UInteger32 => Convert.ToUInt32(claim.Value),
                ClaimValueTypes.UInteger64 => Convert.ToUInt64(claim.Value),
                ClaimValueTypes.Boolean => Convert.ToBoolean(claim.Value),
                _ => claim.Value,
            };
        }

        #endregion
    }
}
