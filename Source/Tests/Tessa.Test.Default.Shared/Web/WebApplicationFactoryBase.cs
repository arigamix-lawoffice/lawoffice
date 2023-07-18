using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.Web
{
    /// <summary>
    /// Базовый абстрактный класс предоставляющий методы для создания тестового сервера предназначенного для тестирования web-приложений.
    /// </summary>
    public abstract class WebApplicationFactoryBase :
        IWebApplicationFactory
    {
        #region Fields

        private IHost host;

        private TestServer server;

        private bool isDisposed;

        #endregion

        #region IWebApplicationFactory Members

        /// <inheritdoc/>
        public IHost Host
        {
            get
            {
                this.CheckInitializeServer();
                this.CheckDisposed();

                return this.host;
            }
        }

        /// <inheritdoc/>
        public TestServer Server
        {
            get
            {
                this.CheckInitializeServer();
                this.CheckDisposed();

                return this.server;
            }
        }

        /// <inheritdoc/>
        public IServiceProvider Services
        {
            get
            {
                this.CheckInitializeServer();
                this.CheckDisposed();

                return this.host?.Services ?? this.server.Host.Services;
            }
        }

        /// <inheritdoc/>
        public WebApplicationFactoryClientOptions ClientOptions { get; } = new WebApplicationFactoryClientOptions();

        /// <inheritdoc/>
        public HttpClient CreateClient() =>
            this.CreateClient(this.ClientOptions);

        /// <inheritdoc/>
        public HttpClient CreateClient(
            WebApplicationFactoryClientOptions options)
        {
            Check.ArgumentNotNull(options, nameof(options));

            return this.CreateClient(options.BaseAddress, options.CreateHandlers());
        }

        /// <inheritdoc/>
        public HttpClient CreateClient(
            Uri baseAddress,
            IList<DelegatingHandler> handlers = default)
        {
            Check.ArgumentNotNull(baseAddress, nameof(baseAddress));

            this.CheckInitializeServer();
            this.CheckDisposed();

            HttpClient httpClient;

            if (handlers is null || handlers.Count == 0)
            {
                httpClient = this.server.CreateClient();
            }
            else
            {
                for (var i = handlers.Count - 1; i > 0; i--)
                {
                    handlers[i - 1].InnerHandler = handlers[i];
                }

                var innerHandler = this.server.CreateHandler();
                handlers[^1].InnerHandler = innerHandler;
                httpClient = new HttpClient(handlers[0]);
            }

            httpClient.BaseAddress = baseAddress;

            this.ConfigureClient(httpClient);

            return httpClient;
        }

        /// <inheritdoc/>
        public void InitializeAndStart()
        {
            if (this.server is not null)
            {
                return;
            }

            this.CheckDisposed();

            var hostBuilder = this.CreateHostBuilder();

            if (hostBuilder is null)
            {
                throw new InvalidOperationException("The " + nameof(this.CreateHostBuilder) + " method should return not null value.");
            }

            hostBuilder.ConfigureWebHost(this.ConfigureWebHostInternal);

            var hostLocal = hostBuilder.Build();
            hostLocal.Start();

            var serverLocal = hostLocal.GetTestServer();

            this.ConfigureTestServer(serverLocal);

            this.host = hostLocal;
            this.server = serverLocal;
        }

        #endregion

        #region IAsyncDisposable Members

        /// <summary>
        /// Освобождает ресурсы, занимаемые объектом.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (this.isDisposed)
            {
                return;
            }

            await this.DisposeCoreAsync();

            // Обращение выполняется в полю для предотвращения исключения при обращении к соответствующему свойству.
            this.server?.Dispose();
            this.host?.Dispose();

            this.isDisposed = true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создаёт <see cref="IHostBuilder"/> используемый для настройки <see cref="TestServer"/>.
        /// </summary>
        /// <returns>Созданный экземпляр объекта <see cref="IHostBuilder"/>.</returns>
        protected virtual IHostBuilder CreateHostBuilder()
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();
        }

        /// <summary>
        /// Настраивает <see cref="IWebHostBuilder"/> до выполнения <see cref="IWebHostBuilder.Build"/> и запуска сервера.
        /// </summary>
        /// <param name="builder">Настраиваемый <see cref="IWebHostBuilder"/>.</param>
        protected virtual void ConfigureWebHost(IWebHostBuilder builder)
        {

        }

        /// <summary>
        /// Настраивает <see cref="TestServer"/> после создания.
        /// </summary>
        /// <param name="testServer">Настраиваемый экземпляр <see cref="TestServer"/>.</param>
        protected virtual void ConfigureTestServer(TestServer testServer)
        {

        }

        /// <summary>
        /// Настраивает <see cref="HttpClient"/> создаваемый данной фабрикой.
        /// </summary>
        /// <param name="client">Настраиваемый <see cref="HttpClient"/>.</param>
        protected virtual void ConfigureClient(HttpClient client)
        {

        }

        /// <summary>
        /// Проверяет, инициализирован ли сервер. Если нет, то выбрасывает исключение <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Сервер не инициализирован. Пожалуйста, вызовите метод <see cref="InitializeAndStart"/>.</exception>
        protected void CheckInitializeServer()
        {
            if (this.server is null)
            {
                throw new InvalidOperationException($"Server is not initialized. Please call {nameof(this.InitializeAndStart)} method.");
            }
        }

        /// <doc path='info[@type="IDisposable" and @item="CheckDisposed"]'/>
        /// <doc path='exception[@type="ObjectDisposedException"]'/>
        protected void CheckDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Вызывается для освобождения ресурсов в дочерних классах.
        /// </summary>
        /// <remarks>
        /// По умолчанию не выполняет действий, что может быть изменено в дочерних классах.
        /// </remarks>
        protected virtual ValueTask DisposeCoreAsync() => ValueTask.CompletedTask;

        #endregion

        #region Private Methods

        private void ConfigureWebHostInternal(IWebHostBuilder webHostBuilder)
        {
            this.ConfigureWebHost(webHostBuilder);
            webHostBuilder.UseTestServer();
        }

        #endregion
    }
}
