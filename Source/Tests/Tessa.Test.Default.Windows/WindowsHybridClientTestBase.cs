using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Data;
using Tessa.Test.Default.Client;
using Tessa.UI;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Unity;
using Unity.Lifetime;

namespace Tessa.Test.Default.Windows
{
    /// <summary>
    /// Абстрактный базовый класс, предоставляющий методы для выполнения тестов 
    /// на специально подготовленном сервере приложений с поддержкой пользовательского интерфейса.
    /// </summary>
    public abstract class WindowsHybridClientTestBase : HybridClientTestBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        protected override ValueTask<IUnityContainer> InitializeClientContainerBaseAsync(
            IUnityContainer unityContainer,
            Func<DbManager> createDbManagerFunc = null,
            IDbScope dbScope = null,
            string baseAddress = null,
            Action<IUnityContainer> beforeRegisterExtensionsOnClientAction = null,
            Action<IUnityContainer> beforeFinalizeClientRegistrationAction = null,
            CancellationToken cancellationToken = default)
        {
            unityContainer
                .RegisterClient(
                    baseAddress: baseAddress,
                    entryAssembly: Assembly.GetExecutingAssembly())
                .RegisterViews()
                .RegisterType<IJsonViewModelImporter, JsonViewModelImporter>(new ContainerControlledLifetimeManager())
                .RegisterType<IJsonViewModelExporter, JsonViewModelExporter>(new ContainerControlledLifetimeManager())
                .RegisterType<IJsonViewModelConverter, JsonViewModelConverter>(new ContainerControlledLifetimeManager())
                .RegisterType<IJsonViewModelUpgrader, JsonViewModelUpgrader>(new ContainerControlledLifetimeManager());

            return this.RegisterClientContainerBaseAsync(
                unityContainer,
                createDbManagerFunc,
                dbScope,
                beforeRegisterExtensionsOnClientAction,
                beforeFinalizeClientRegistrationAction,
                cancellationToken);
        }

        #endregion
    }
}
