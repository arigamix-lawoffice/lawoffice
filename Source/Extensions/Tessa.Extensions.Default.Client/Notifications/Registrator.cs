using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Notifications
{
    [Registrator]
    public sealed class Registrator :
        RegistratorBase
    {
        #region Base Overrides

        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrNotificationManager, KrNotificationManager>(new ContainerControlledLifetimeManager())
                .RegisterType<KrNotificationsApplicationExtension>(new PerResolveLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IApplicationExtension, KrNotificationsApplicationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.TessaClient))
                ;
        }

        #endregion
    }
}
