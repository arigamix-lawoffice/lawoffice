using System;
using System.Collections.Generic;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Notices
{
    public static class NoticesExtensions
    {
        #region IUnityContainer Extensions

        public static IUnityContainer RegisterNoticesMessageProcessor(this IUnityContainer unityContainer)
        {
            return unityContainer
                    .RegisterType<IMessageProcessor, MessageProcessor>(new ContainerControlledLifetimeManager())
                    .RegisterFactory<Func<IEnumerable<IMessageHandler>>>(
                        c => new Func<IEnumerable<IMessageHandler>>(
                            () => c.ResolveAll<IMessageHandler>()),
                        new ContainerControlledLifetimeManager())
                    .RegisterSingleton<IMessageHandler, KrMessageHandler>(nameof(KrMessageHandler))
                ;
        }

        #endregion
    }
}
