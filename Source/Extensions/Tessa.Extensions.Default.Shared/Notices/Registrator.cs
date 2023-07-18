using Tessa.Notices;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Notices
{
    [Registrator(Tag = RegistratorTag.Client)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<INotificationSubscriptionPermissionManager, KrNotificationSubscriptionPermissionManager>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
