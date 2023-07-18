using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Test.Default.Client.Web;
using Tessa.Test.Default.Shared;
using Tessa.Test.Default.Shared.GC;
using Tessa.Test.Default.Shared.GC.Handlers;
using Tessa.Test.Default.Shared.Web;
using Tessa.Web;
using Tessa.Web.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Абстрактный базовый класс, предоставляющий методы для выполнения клиентских тестов 
    /// без поддержки пользовательского интерфейса на специально подготовленном сервере приложений.
    /// </summary>
    public abstract class HybridClientTestBase :
        ClientTestBase
    {
        #region Constants And Static Fields

        private static readonly AsyncSynchronizedOneTimeRegistrator initializeWebServerRegistrator =
            new AsyncSynchronizedOneTimeRegistrator(() => WebHelper.InitializeWebServerAsync(initializeLocalization: false));

        /// <summary>
        /// Базовый адрес сервера приложений по умолчанию.
        /// </summary>
        public const string DefaultBaseAddress = "http://localhost/";

        #endregion

        #region Fields

        /// <summary>
        /// Unity контейнер, используемый при инициализации серверного контейнера.<para/>
        /// Необходим из-за недоступности контекста в <see cref="ContainerProvider.CreateContainer(string, bool, string)"/>.
        /// </summary>
        private IUnityContainer unityContainerForInitializeServerContainer;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает фабрику, предназначенную для создания объектов, с помощью которых можно реализовать функциональные тесты для web-приложений.
        /// </summary>
        public IWebApplicationFactory WebApplicationFactory { get; private set; }

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли в качестве источника файлов по умолчанию использовать базу данных или нет.
        /// </summary>
        public virtual bool UseDatabaseAsDefault { get; }

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли использовать коммуникация между процессами или нет.
        /// </summary>
        public virtual bool EnableInterprocessCommunication { get; }

        /// <summary>
        /// Возвращает Unity-контейнер, используемый на сервере.
        /// </summary>
        /// <remarks>
        /// Не используйте значение этого свойства для регистрации зависимостей. Для регистрации зависимостей, используемых на сервере, необходимо переопределить метод <see cref="InitializeContainerServerAsync(IUnityContainer, IWebContextAccessor)"/>. Для изменения создаваемого серверного контейнера необходимо переопределить метод <see cref="CreateContainerServerAsync()"/>.
        /// </remarks>
        public IUnityContainer UnityContainerServer
        {
            get
            {
                if (this.WebApplicationFactory is not { } factory)
                {
                    throw new InvalidOperationException($"{nameof(HybridClientTestBase)}.{nameof(this.WebApplicationFactory)} is not initialized.");
                }

                var containerProvider = factory.Server.Services.GetRequiredService<IContainerProvider>();
                var valueTask = containerProvider.GetContainerAsync(RuntimeHelper.DefaultInstanceName);
                return valueTask.IsCompletedSuccessfully ? valueTask.Result : valueTask.AsTask().GetAwaiter().GetResult();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="HybridClientTestBase"/>.
        /// </summary>
        protected HybridClientTestBase() =>
            this.PlannedInitializeTestServer();

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override string BaseAddressOverride => DefaultBaseAddress;

        /// <inheritdoc/>
        protected override string UserNameOverride => "admin";

        /// <inheritdoc/>
        protected override string PasswordOverride => "admin";

        /// <inheritdoc/>
        protected override async ValueTask InitializeContainerAsync(IUnityContainer container)
        {
            await base.InitializeContainerAsync(container);

            var applicationFolders = container.Resolve<IApplicationFolders>();
            applicationFolders.LocalData = Path.Combine(await this.GetFileStoragePathAsync(), "local_data");
            applicationFolders.RoamingData = Path.Combine(await this.GetFileStoragePathAsync(), "roaming_data");
        }

        /// <inheritdoc/>
        protected override void BeforeRegisterExtensionsOnClient(IUnityContainer unityContainer)
        {
            base.BeforeRegisterExtensionsOnClient(unityContainer);

            unityContainer
                .RegisterType<IHttpClientFactory, TestServerHttpClientFactory>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        new InjectionParameter<IWebApplicationFactory>(this.WebApplicationFactory)));
        }

        /// <inheritdoc/>
        protected override async ValueTask<string> GetBaseAddressAsync()
        {
            var address = await base.GetBaseAddressAsync();

            if (!this.GetType().IsDefined(typeof(SetupTempDbAttribute), true))
            {
                return address;
            }

            if (string.IsNullOrEmpty(address))
            {
                address = DefaultBaseAddress;
            }

            var builder = new UriBuilder(address);
            builder.Host = $"{builder.Host}_{(await this.GetFixtureDateTimeAsync()).FormatDateTimeCode()}_{await this.GetFixtureNameAsync()}";
            return builder.ToString();
        }

        /// <inheritdoc/>
        protected override async Task OneTimeTearDownCoreAsync()
        {
            if (this.WebApplicationFactory is not null)
            {
                await this.WebApplicationFactory.Host.StopAsync();
                await this.WebApplicationFactory.DisposeAsync();
            }

            await base.OneTimeTearDownCoreAsync();
        }

        /// <inheritdoc/>
        protected override void SetServerUtcNow(DateTime? utcNow) =>
            this.UnityContainerServer
                .Resolve<MutableClock>()
                .MutableUtcNow = utcNow;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создаёт Unity-контейнер, используемый на сервере.
        /// </summary>
        /// <param name="instanceName">Имя экземпляра сервера. Может быть равно пустой строке или значению <see langword="null"/>, если используется имя по умолчанию.</param>
        /// <param name="multipleInstances">Признак того, что активен режим работы с несколькими экземплярами сервера. При этом в запросах к серверу обязательно передаётся <c>InstanceName</c>, в т.ч. для ссылок на web-клиент.</param>
        /// <param name="serverRootPath">Путь к папке с конфигурационным файлами, или <see langword="null"/>, если путь определяется по умолчанию как значение свойства <see cref="RuntimeHelper.ConfigRootPath"/>.</param>
        /// <returns>Созданный Unity-контейнер.</returns>
        protected virtual async ValueTask<IUnityContainer> CreateContainerServerAsync(
            string instanceName,
            bool multipleInstances,
            string serverRootPath,
            IWebContextAccessor webContextAccessor)
        {
            var container = await this.CreateContainerServerAsync();
            await this.InitializeContainerServerAsync(container, webContextAccessor);
            return container;
        }

        /// <summary>
        /// Создаёт серверный Unity контейнер.
        /// </summary>
        /// <returns>Созданный серверный Unity контейнер.</returns>
        protected virtual ValueTask<IUnityContainer> CreateContainerServerAsync() =>
            new(new UnityContainer());

        /// <summary>
        /// Инициализирует серверный Unity контейнер.
        /// </summary>
        /// <param name="container">Инициализируемый серверный Unity контейнер.</param>
        /// <param name="webContextAccessor"><inheritdoc cref="IWebContextAccessor" path="/summary"/></param>
        /// <returns>Асинхронная задача.</returns>
        protected virtual async ValueTask InitializeContainerServerAsync(
            IUnityContainer container,
            IWebContextAccessor webContextAccessor)
        {
            var fileSourceSettings = await this.CreateDefaultFileSourceSettingsAsync(
                randomizeFileBasePath: true,
                useDatabaseAsDefault: this.UseDatabaseAsDefault);

            var serverCodeOverride = await TestHelper.GetServerCodeAsync(this);

            // Планирование удаления ключей из Redis.
            var obj = RedisExternalObjectHandler.CreateObjectInfo(
                $"{serverCodeOverride}*",
                this.GetHashCode());

            var externalObjectManager = this.unityContainerForInitializeServerContainer.Resolve<IExternalObjectManager>();
            externalObjectManager.RegisterForFinalize(obj);

            await TestHelper.InitializeServerContainerAsync(
                container,
                this.DbFactory is null ? null : this.DbFactory.Create,
                this.DbScope,
                () => webContextAccessor.TryGetWebContext()?.TryGetSessionToken(),
                this.EnableInterprocessCommunication,
                fileSourceSettings,
                this.BeforeRegisterExtensionsOnServer,
                this.BeforeFinalizeServerRegistration,
                serverCodeOverride);
        }

        /// <summary>
        /// Выполняет действия перед поиском и выполнением серверных регистраторов расширений в папке приложения.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        protected virtual void BeforeRegisterExtensionsOnServer(IUnityContainer unityContainer)
        {

        }

        /// <summary>
        /// Выполняет действия перед завершением регистрации сервера приложений.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        protected virtual void BeforeFinalizeServerRegistration(IUnityContainer unityContainer)
        {

        }

        #endregion

        #region Private Methods

        private void PlannedInitializeTestServer()
        {
            this.GetTestActions(ActionStage.BeforeInitialize).Add(
                new TestAction(
                    this,
                    static async sender =>
                    {
                        await initializeWebServerRegistrator.RegisterAsync();

                        var senderT = (HybridClientTestBase) sender;

                        var factory = new TessaWebApplicationFactory(senderT.CreateContainerServerAsync);
                        factory.InitializeAndStart();
                        senderT.WebApplicationFactory = factory;
                    }));

            this.GetTestActions(ActionStage.BeforeInitializeContainer).Add(
                new TestAction(
                    this,
                    static sender =>
                    {
                        var senderT = (HybridClientTestBase) sender;

                        senderT.unityContainerForInitializeServerContainer = senderT.UnityContainer;

                        return ValueTask.CompletedTask;
                    }));
        }

        #endregion
    }
}
