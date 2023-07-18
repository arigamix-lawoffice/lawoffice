using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tessa.Platform;
using Tessa.Web.Services;
using Unity;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Предоставляет методы для создания Unity-контейнера используемого на сервере в тестах с настраиваемым сервером приложений.
    /// </summary>
    public sealed class TestContainerProvider :
        ContainerProvider
    {
        #region Fields

        private readonly Func<string, bool, string, IWebContextAccessor, ValueTask<IUnityContainer>> createContainerFuncAsync;
        private readonly IWebContextAccessor webContextAccessor;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestContainerProvider"/>.
        /// </summary>
        /// <param name="configurationManager">Объект, управляющий конфигурацией приложений Tessa.</param>
        /// <param name="serviceProvider">Предоставляет методы для получения зависимостей.</param>
        /// <param name="createContainerFunc">Метод создающий серверный контейнер. Параметры соответствуют методу <see cref="IContainerProvider.GetContainerAsync(string, bool, string, CancellationToken)"/>.</param>
        /// <param name="backgroundServiceQueue">Очередь действий для асинхронной обработки в фоновом режиме веб-сервером.</param>
        /// <param name="unityContainerOptions">Опции по работе с Unity контейнером при его создании/удалении.</param>
        /// <param name="webContextAccessor">Объект, который предоставляет доступ к текущему объекту контекста обработки веб-запроса.</param>
        public TestContainerProvider(
            IConfigurationManager configurationManager,
            IServiceProvider serviceProvider,
            Func<string, bool, string, IWebContextAccessor, ValueTask<IUnityContainer>> createContainerFunc,
            IWebBackgroundServiceQueue backgroundServiceQueue,
            IOptions<WebUnityContainerOptions> unityContainerOptions,
            IWebContextAccessor webContextAccessor)
            : base(configurationManager, serviceProvider, backgroundServiceQueue, unityContainerOptions, webContextAccessor)
        {
            this.webContextAccessor = NotNullOrThrow(webContextAccessor);
            this.createContainerFuncAsync = NotNullOrThrow(createContainerFunc);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask<ContainerDescriptor> CreateContainerAsync(
            string instanceName,
            bool multipleInstances,
            string serverRootPath,
            CancellationToken cancellationToken = default)
        {
            var container = await this.createContainerFuncAsync(instanceName, multipleInstances, serverRootPath, this.webContextAccessor);
            var options = new WebContainerCreationOptions(instanceName, multipleInstances, serverRootPath);
            // это нужно сделать, чтобы сохранить консистентность с DisposeContainerAsync, который не переопределён и выполняет вызов BeforeContainerDisposedActionsAsync.
            await this.AfterContainerCreatedActionsAsync(container, options);
            return new ContainerDescriptor(container, options);
        }

        #endregion
    }
}
