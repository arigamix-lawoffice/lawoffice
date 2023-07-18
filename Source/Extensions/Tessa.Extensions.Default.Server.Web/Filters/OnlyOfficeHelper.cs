#nullable enable

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using Tessa.Extensions.Default.Server.OnlyOffice.Token;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Web.Filters
{
    public static class OnlyOfficeHelper
    {
        #region Constants

        public const string ContextSystemKeyPrefix = ".onlyoffice.";

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает значение заголовка <see cref="HeaderNames.Authorization"/> соответствующее используемой схеме проверки подлинности по умолчанию.
        /// </summary>
        /// <param name="headers">Заголовки передаваемые в <see cref="HttpRequest"/> или <see cref="HttpResponse"/>.</param>
        /// <returns>Значение заголовка <see cref="HeaderNames.Authorization"/> соответствующее используемой схеме проверки подлинности по умолчанию или значение по умолчанию для типа, если заголовок <see cref="HeaderNames.Authorization"/> не найден или он не соответствует схеме проверки подлинности по умолчанию <see cref="OnlyOfficeTokenHelper.AuthenticationScheme"/>.</returns>
        public static string? TryGetAuthorizationHeaderValue(this IHeaderDictionary headers)
        {
            const string schemeWithSpace = OnlyOfficeTokenHelper.AuthenticationScheme + " ";

            if (headers is null)
            {
                return null;
            }

            string? tokenHeader;
            return headers.TryGetValue(HeaderNames.Authorization, out var header)
                && (tokenHeader = header.FirstOrDefault(i => i is not null && i.StartsWith(schemeWithSpace, StringComparison.OrdinalIgnoreCase))) is not null
                ? tokenHeader[schemeWithSpace.Length..]
                : null;
        }

        public static void SetJwtToken(this HttpContext httpContext, OnlyOfficeJwtTokenInfo token) => httpContext.Items[ContextSystemKeyPrefix + "Token"] = token;

        public static OnlyOfficeJwtTokenInfo? GetJwtToken(this HttpContext httpContext) => (OnlyOfficeJwtTokenInfo?) (httpContext.Items.TryGetValue(ContextSystemKeyPrefix + "Token", out object? value) ? value : null);

        #endregion
    }
}
