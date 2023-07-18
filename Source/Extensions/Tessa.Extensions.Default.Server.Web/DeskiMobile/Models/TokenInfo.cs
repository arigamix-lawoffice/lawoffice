using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Объект, содержит информацию о JSON Web Token при работе с deskiMobile.
    /// </summary>
    public sealed class TokenInfo : StorageSerializable
    {
        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="claims">Полезная нагрузка JWT токена.</param>
        public TokenInfo(IEnumerable<Claim> claims)
        {
            var dict = NotNullOrThrow(claims).ToDictionary(static i => i.Type, static j => (object)j.Value);
            this.DeserializeCore(dict);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор сессии пользователя для которого был выдан JSON Web Token.
        /// </summary>
        public Guid SID { get; set; }

        /// <summary>
        /// Идентификатор операции для которого был выдан JSON Web Token.
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// Идентификатор версии файла для которого был выдан JSON Web Token.
        /// </summary>
        public Guid VersionID { get; set; }

        /// <summary>
        /// Разрешённые операции для токена для которого был выдан JSON Web Token.
        /// </summary>
        public DeskiMobileTokenPermissionFlags Access { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[DeskiMobileClaimNames.SID] = this.SID;
            storage[DeskiMobileClaimNames.OperationID] = this.OperationID;
            storage[DeskiMobileClaimNames.VersionID] = this.VersionID;
            storage[DeskiMobileClaimNames.Access] = this.Access;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.SID = TryGetGuid(storage, DeskiMobileClaimNames.SID) ??
                throw new InvalidOperationException("Claim have not '" + DeskiMobileClaimNames.SID + "' param.");
            this.OperationID = TryGetGuid(storage, DeskiMobileClaimNames.OperationID) ??
                throw new InvalidOperationException("Claim have not '" + DeskiMobileClaimNames.OperationID + "' param.");
            this.VersionID = TryGetGuid(storage, DeskiMobileClaimNames.VersionID) ??
                throw new InvalidOperationException("Claim have not '" + DeskiMobileClaimNames.VersionID + "' param.");
            this.Access = Enum.TryParse<DeskiMobileTokenPermissionFlags>(storage.TryGet<string>(DeskiMobileClaimNames.Access), out var access)
                ? access
                : DeskiMobileTokenPermissionFlags.None;
        }

        #endregion
    }
}
