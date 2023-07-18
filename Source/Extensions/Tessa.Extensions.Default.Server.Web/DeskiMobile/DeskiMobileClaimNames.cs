using System;
using System.Security.Claims;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    /// <summary>
    /// Предоставляет имена дополнительных утверждений используемые в JSON Web Token.
    /// </summary>
    /// <seealso href="https://tools.ietf.org/html/rfc7519#section-4" alt="JWT Claims"/>
    public static class DeskiMobileClaimNames
    {
        /// <summary>
        /// Имя утверждения содержащего идентификатор сессии пользователя в системе для которой был выдан JSON Web Token. Тип значения: <see cref="Guid"/>. Тип значения JWT: <see cref="ClaimValueTypes.String"/>.
        /// </summary>
        public const string SID = "sid";

        /// <summary>
        /// Имя утверждения содержащего идентификатор операции для которой был выдан JSON Web Token. Тип значения: <see cref="Guid"/>. Тип значения JWT: <see cref="ClaimValueTypes.String"/>.
        /// </summary>
        public const string OperationID = "opid";

        /// <summary>
        /// Имя утверждения содержащего идентификатор версии файла для которого был выдан JSON Web Token. Тип значения: <see cref="Guid"/>. Тип значения JWT: <see cref="ClaimValueTypes.String"/>.
        /// </summary>
        public const string VersionID = "fvid";

        /// <summary>
        /// Имя утверждения содержащего разрешённые операции для которой был выдан JSON Web Token. Тип значения: <see cref="int"/> (<see cref="DeskiMobileTokenPermissionFlags"/>). Тип значения JWT: <see cref="ClaimValueTypes.Integer32"/>.
        /// </summary>
        public const string Access = "fvacs";
    }
}
