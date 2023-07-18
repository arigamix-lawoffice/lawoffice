using Tessa.Platform;
using Tessa.Test.Default.Shared.GC.Handlers;
using Unity;
using Unity.Lifetime;

namespace Tessa.Test.Default.Shared.GC
{
    /// <summary>
    /// Предоставляет методы расширения для пространства имён <see cref="Tessa.Test.Default.Shared.GC"/>.
    /// </summary>
    public static class UnityContainerExtensions
    {
        #region Public Methods

        /// <summary>
        /// Регистрирует в Unity-контейнере типы для обработки внешних объектов.
        /// </summary>
        /// <param name="container">Unity-контейнер.</param>
        /// <returns>Unity-контейнер для создания цепочки вызовов.</returns>
        public static IUnityContainer RegisterExternalObjects(
            this IUnityContainer container)
        {
            Check.ArgumentNotNull(container, nameof(container));

            container
                .RegisterType<IExternalObjectHandlerRegistry, ExternalObjectHandlerRegistry>(new ContainerControlledLifetimeManager())
                .RegisterExternalObjectHandlers()
                .RegisterFactory<IExternalObjectManager>(
                    static c =>
                        new ExternalObjectManager(
                            TestSettings.GCDbConnectionString,
                            c.Resolve<IExternalObjectHandlerRegistry>(),
                            c.TryResolve<IUnityDisposableContainer>()),
                    new ContainerControlledLifetimeManager());

            return container;
        }

        /// <summary>
        /// Регистрирует обработчики внешних объектов в <see cref="IExternalObjectHandlerRegistry"/>.
        /// </summary>
        /// <param name="container">Unity-контейнер.</param>
        /// <returns>Unity-контейнер для создания цепочки вызовов.</returns>
        public static IUnityContainer RegisterExternalObjectHandlersInRegistry(
            this IUnityContainer container)
        {
            ThrowIfNull(container);

            if (container.TryResolve<IExternalObjectHandlerRegistry>() is { } registry)
            {
                foreach (var item in container.ResolveAll<IExternalObjectHandler>())
                {
                    registry.Register(item);
                }
            }

            return container;
        }

        #endregion

        #region Private Methods

        private static IUnityContainer RegisterExternalObjectHandlers(this IUnityContainer container) =>
            container
                .RegisterType<IExternalObjectHandler, DbExternalObjectHandler>(nameof(DbExternalObjectHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IExternalObjectHandler, FileExternalObjectHandler>(nameof(FileExternalObjectHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IExternalObjectHandler, FolderExternalObjectHandler>(nameof(FolderExternalObjectHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IExternalObjectHandler, RedisExternalObjectHandler>(nameof(RedisExternalObjectHandler), new ContainerControlledLifetimeManager());

        #endregion
    }
}
