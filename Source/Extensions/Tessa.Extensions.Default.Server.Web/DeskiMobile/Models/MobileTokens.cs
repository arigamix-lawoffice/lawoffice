#nullable enable
using System;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    public sealed record MobileTokens(TokenInfo JwtToken, ISessionToken SessionToken)
    {
        #region Properties

        /// <summary>
        /// <para>Устанавливает настройки сессии для текущего AsyncLocal-контекста.</para>
        /// <para>Будут действовать до конца текущего асинхронного метода, поэтому нельзя вызвать в асинхронном хэлпере,
        /// а потом продолжить выполнение в вызывающем коде - в нём настройки будут снова сброшены к предыдущим.</para>
        /// </summary>
        public Action? ApplySessionToken { get; set; }

        #endregion

        #region Methods

        public bool HasAccess(DeskiMobileTokenPermissionFlags flag) => 
            (this.JwtToken.Access & flag) == flag;

        #endregion
    }
}
