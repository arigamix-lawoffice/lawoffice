using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NLog.Web;
using Tessa.Platform;
using Tessa.Test.Default.Shared.Web;
using Tessa.Web;
using Tessa.Web.Client;
using Tessa.Web.Services;
using Unity;

namespace Tessa.Test.Default.Client.Web
{
    /// <summary>
    /// Предоставляет методы для создания тестового сервера предназначенного для тестирования web-приложения TESSA.
    /// </summary>
    public class TessaWebApplicationFactory :
        WebApplicationFactoryBase
    {
        #region Fields

        private readonly Func<string, bool, string, IWebContextAccessor, ValueTask<IUnityContainer>> createContainerFunc;

        private readonly IConfigurationManager configurationManagerOverride;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TessaWebApplicationFactory"/>.
        /// </summary>
        /// <param name="createContainerFunc">Метод создающий серверный контейнер для тестов.</param>
        /// <param name="configurationManagerOverride">Объект, управляющий конфигурацией приложений Tessa переопределяющий используемый по умолчанию. Если задано значение по умолчанию для типа, то используется менеджер по умолчанию.</param>
        public TessaWebApplicationFactory(
            Func<string, bool, string, IWebContextAccessor, ValueTask<IUnityContainer>> createContainerFunc,
            IConfigurationManager configurationManagerOverride = default)
        {
            Check.ArgumentNotNull(createContainerFunc, nameof(createContainerFunc));

            this.createContainerFunc = createContainerFunc;
            this.configurationManagerOverride = configurationManagerOverride;

            // Необходимо для предотвращения копирования ответа содержащего поток SuperStream не поддерживающий получение длины потока (Stream.Length).
            this.ClientOptions.AllowAutoRedirect = false;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseNLog()
                .UseTessaConfiguration(Array.Empty<string>())
                .ConfigureServices(services =>
                {
                    services
                        .AddTessaServices()
                        .AddTessaClientServices()
                        .AddTessaResponseCompression();

                    services
                        .AddControllersWithViews()
                        .ConfigureTessaClientMvc()
                        ;

                    if (this.configurationManagerOverride is not null)
                    {
                        services
                            .RemoveAll<IConfigurationManager>()
                            .AddSingleton(this.configurationManagerOverride);
                    }

                    services
                        .RemoveAll<IContainerProvider>()
                        .AddSingleton<IWebBackgroundServiceQueue, WebBackgroundServiceQueue>()
                        .AddSingleton<IWebPeriodicService, WebPeriodicService>()
                        .AddSingleton<IContainerProvider, TestContainerProvider>(s =>
                            new TestContainerProvider(
                                s.GetService<IConfigurationManager>(),
                                s,
                                this.createContainerFunc,
                                s.GetService<IWebBackgroundServiceQueue>(),
                                s.GetService<IOptions<WebUnityContainerOptions>>(),
                                s.GetService<IWebContextAccessor>()))
                        .AddTessaResponseCompression()
                        ;

                    services
                        .AddOptions()
                        .AddTessaHealthChecks()
                        .Configure<MvcOptions>(x => x.AddTessaFormatters())
                        .Configure<FormOptions>(x => x.SetupTessaFormOptions())
                        .ConfigureWebOptions()
                        .ConfigureWebClientOptions()
                        .ConfigureAuthWithoutSaml()
                        ;
                })
                .Configure(app =>
                {
                    var services = app.ApplicationServices;

                    var forwardedHeaders = new ForwardedHeadersOptions
                    {
                        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                        ForwardLimit = 10,
                    };

                    forwardedHeaders.KnownNetworks.Clear();
                    forwardedHeaders.KnownProxies.Clear();

                    var serverOptions = services.GetService<IOptions<WebServerOptions>>();
                    var options = services.GetService<IOptions<WebOptions>>();

                    app
                        .UseForwardedHeaders(forwardedHeaders)
                        .UseTessaHttpsRedirection(serverOptions.Value, environmentIsDevelopment: true)
                        .UsePathBaseIfSpecified(options.Value.PathBase)
                        .UseResponseCompression()
                        ;

                    // вызовы UseRouting, UseAuthorization и UseEndpoints должны быть в указанном порядке
                    // разделены по разным операторам (точкой с запятой), чтобы анализатор .NET Core при билде не кидал ворнинги

                    app
                        .UseTessaClientApplication()
                        .UseRouting()
                        ;

                    app
                        .UseAuthorization() // необходимо в ASP.NET Core 3+, нельзя выключать
                        ;

                    app
                        .UseEndpoints(endpoints =>
                        {
                            endpoints.MapHealthChecks("/hcheck");
                            endpoints.MapControllers();
                        })
                        //.ConfigureTessaApplication()
                        ;

                    // ConfigureTessaApplication инициализирует так же LocalizationManagerServer = LocalizationManagerServer.
                    // При импорте карточек WebHelper.HttpContext is null, что приводит к ошибке: "Can't access current HttpContext. Code should run inside web request.".

                    var contextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
                    WebHelper.Configure(contextAccessor);

                    var applicationLifetime = services.GetService<IHostApplicationLifetime>();

                    applicationLifetime
                        .RegisterTessaLifetime(app)
                        ;

                    app.Run(context => context.HandleNotFoundAsync());
                });
        }

        /// <inheritdoc/>
        protected override void ConfigureTestServer(TestServer testServer) =>
            // Аналог IISServerOptions.AllowSynchronousIO = true.
            testServer.AllowSynchronousIO = true;

        #endregion
    }
}
