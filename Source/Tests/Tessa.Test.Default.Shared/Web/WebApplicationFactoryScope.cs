using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Tessa.Platform;
using Tessa.Platform.Scopes;

namespace Tessa.Test.Default.Shared.Web
{
    /// <summary>
    /// Определяет контекст в котором доступен <see cref="IWebApplicationFactory"/>.
    /// </summary>
    public sealed class WebApplicationFactoryScope
    {
        #region Nested Types

        private sealed class WebApplicationFactoryHolder :
            IWebApplicationFactory
        {
            /// <summary>
            /// Сохранённый объект.
            /// </summary>
            public IWebApplicationFactory Factory { get; init; }

            /// <inheritdoc/>
            public IHost Host =>
                this.Factory.Host;

            /// <inheritdoc/>
            public TestServer Server =>
                this.Factory.Server;

            /// <inheritdoc/>
            public IServiceProvider Services =>
                this.Factory.Services;

            /// <inheritdoc/>
            public WebApplicationFactoryClientOptions ClientOptions =>
                this.Factory.ClientOptions;

            /// <inheritdoc/>
            public HttpClient CreateClient() =>
                this.Factory.CreateClient();

            /// <inheritdoc/>
            public HttpClient CreateClient(WebApplicationFactoryClientOptions options) =>
                this.Factory.CreateClient(options);

            /// <inheritdoc/>
            public HttpClient CreateClient(Uri baseAddress, IList<DelegatingHandler> handlers = null) =>
                this.Factory.CreateClient(baseAddress, handlers);

            /// <inheritdoc/>
            public void InitializeAndStart() =>
                this.Factory.InitializeAndStart();

            /// <inheritdoc/>
            public ValueTask DisposeAsync() =>
                ValueTask.CompletedTask;
        }

        #endregion

        #region Static Members

        /// <summary>
        /// Создаёт область видимости для значения в текущем потоке.
        /// </summary>
        /// <param name="webApplicationFactory">Объект, который должен быть доступен в создаваемой области видимости.</param>
        /// <returns>
        /// Созданная область видимости.
        /// </returns>
        /// <remarks>Объект не следит за временем жизни переданного в него объекта и не вызывает для него <see cref="IDisposable.Dispose"/> или <see cref="IAsyncDisposable.DisposeAsync"/>.</remarks>
        public static IInheritableScopeInstance<IWebApplicationFactory> Create(IWebApplicationFactory webApplicationFactory)
        {
            Check.ArgumentNotNull(webApplicationFactory, nameof(webApplicationFactory));

            return InheritableRetainingScope<IWebApplicationFactory>.Create(() => new WebApplicationFactoryHolder() { Factory = webApplicationFactory });
        }

        /// <summary>
        /// Текущий контекст <see cref="IWebApplicationFactory"/>.
        /// </summary>
        public static IWebApplicationFactory Current => InheritableRetainingScope<IWebApplicationFactory>.Value;

        /// <summary>
        /// Признак того, что текущий код выполняется внутри операции с контекстом <see cref="IWebApplicationFactory"/>,
        /// а свойство <see cref="Current"/> ссылается на действительный контекст.
        /// </summary>
        /// <remarks>
        /// Если текущее свойство возвращает <c>false</c>, то свойство <see cref="Current"/>
        /// возвращает ссылку на пустой контекст.
        /// </remarks>
        public static bool HasCurrent => Current is not null;

        #endregion
    }
}
