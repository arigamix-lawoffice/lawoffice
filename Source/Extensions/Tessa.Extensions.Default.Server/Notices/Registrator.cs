using Tessa.Notices;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Notices
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<INotificationDefaultLanguagePicker, KrNotificationDefaultLanguagePicker>(new ContainerControlledLifetimeManager())
                .RegisterType<INotificationSubscriptionPermissionManager, KrNotificationSubscriptionPermissionManagerServer>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
