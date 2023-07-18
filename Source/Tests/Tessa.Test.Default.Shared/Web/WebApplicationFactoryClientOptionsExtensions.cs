using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.Testing.Handlers;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.Web
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="WebApplicationFactoryClientOptions"/>.
    /// </summary>
    public static class WebApplicationFactoryClientOptionsExtensions
    {
        /// <summary>
        /// Создаёт массив HTTP-обработчиков в соответствии с заданными параметрами.
        /// </summary>
        /// <param name="options">Параметры в соответствии с которыми создаётся массив обработчиков.</param>
        /// <returns>Массив HTTP-обработчиков.</returns>
        public static DelegatingHandler[] CreateHandlers(
            this WebApplicationFactoryClientOptions options)
        {
            Check.ArgumentNotNull(options, nameof(options));

            var arrLength = Convert.ToInt32(options.AllowAutoRedirect) + Convert.ToInt32(options.HandleCookies);
            var arr = arrLength > 0 ? new DelegatingHandler[arrLength] : Array.Empty<DelegatingHandler>();
            var index = 0;

            if (options.AllowAutoRedirect)
            {
                arr[index++] = new RedirectHandler(options.MaxAutomaticRedirections);
            }

            if (options.HandleCookies)
            {
                arr[index] = new CookieContainerHandler();
            }

            return arr;
        }
    }
}
