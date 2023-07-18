#nullable enable

using System;
using System.Security.Claims;

namespace Tessa.Extensions.Default.Server.OnlyOffice.Token
{
    /// <summary>
    /// Предоставляет имена дополнительных утверждений используемые в JSON Web Token.
    /// </summary>
    /// <seealso href="https://tools.ietf.org/html/rfc7519#section-4" alt="JWT Claims"/>
    public static class OnlyOfficeClaimNames
    {
        /// <summary>
        /// Имя утверждения содержащего идентификатор операции OnlyOffice для которой был выдан JSON Web Token. Тип значения: <see cref="Guid"/>. Тип значения JWT: <see cref="ClaimValueTypes.String"/>.
        /// </summary>
        public const string ID = "opid";

        /// <summary>
        /// Имя утверждения содержащего идентификатор версии файла для которого был выдан JSON Web Token. Тип значения: <see cref="Guid"/>. Тип значения JWT: <see cref="ClaimValueTypes.String"/>.
        /// </summary>
        public const string VersionID = "fvid";

        /// <summary>
        /// Имя утверждения содержащего разрешённые операции OnlyOffice для которой был выдан JSON Web Token. Тип значения: <see cref="int"/> (<see cref="OnlyOfficeTokenPermissionFlags"/>). Тип значения JWT: <see cref="ClaimValueTypes.Integer32"/>.
        /// </summary>
        public const string Access = "fvacs";
    }
}
