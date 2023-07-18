using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tessa.Extensions.Default.Server.Web.DeskiMobile.Models;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    /// <summary>
    /// Manager, управляющий созданием и проверкой JWT токенов для DeskiMobile.
    /// </summary>
    public interface IDeskiMobileTokenManager
    {
        /// <summary>
        /// Создание JWT токена.
        /// </summary>
        /// <param name="jwtLifeTime">Интервал жизни токена.</param>
        /// <param name="sessionID">Идентификатор сессии, к которой привязывается JWT токен.</param>
        /// <param name="operationID">Идентификатор операции, к которой привязывается JWT токен.</param>
        /// <param name="fileVersionID">Идентификатор версии файла, к которой привязывается JWT токен.</param>
        /// <param name="flags">Указывает доступные операции для создаваемого JWT токена.</param>
        /// <returns>JWT токен для взаимодействия с deskiMobile.</returns>
        string CreateToken(
            TimeSpan jwtLifeTime, Guid sessionID, Guid operationID,
            Guid fileVersionID, DeskiMobileTokenPermissionFlags flags);

        /// <summary>
        /// Получение JWT токена из <see cref="HttpRequest" />.
        /// </summary>
        /// <param name="request">Объект, в котором хранится информация о запросе.</param>
        /// <param name="operationID">Идентификатор операции, к которой привязан JWT токен.</param>
        /// <param name="sessionServer">Объект, обеспечивающий взаимодействие с сессиями на сервере.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>JWT токен для взаимодействия с deskiMobile.</returns>
        Task<MobileTokens> GetTokenAsync(
            HttpRequest request, Guid operationID,
            ISessionServer sessionServer, CancellationToken cancellationToken = default);
    }
}
