#nullable enable
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Tessa.Platform.Runtime;
using IHttpClientFactory = Tessa.Platform.Runtime.IHttpClientFactory;

namespace Tessa.Test.Default.Shared.Web
{
    /// <summary>
    /// Фабрика объектов <see cref="HttpClient"/> настроенных для подключения к <see cref="TestServer"/>.
    /// </summary>
    public class TestServerHttpClientFactory :
        IHttpClientFactory
    {
        #region Fields

        private readonly IWebApplicationFactory webApplicationFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestServerHttpClientFactory"/>.
        /// </summary>
        /// <param name="webApplicationFactory">
        /// Класс, предоставляющий методы для создания тестового сервера, предназначенного для тестирования web-приложений.
        /// </param>
        public TestServerHttpClientFactory(IWebApplicationFactory webApplicationFactory)
        {
            this.webApplicationFactory = NotNullOrThrow(webApplicationFactory);
        }

        #endregion

        #region IHttpClientFactory Members

        /// <inheritdoc/>
        public HttpClient CreateHttpClient(IConnectionSettings connectionSettings, bool windowsAuth = false)
        {
            var client = this.webApplicationFactory.CreateClient();
            client.Timeout = connectionSettings.SendTimeout;
            return client;
        }

        #endregion
    }
}
