using Tessa.Forums.Notifications;
using Unity.Lifetime;
using Unity;

namespace Tessa.Extensions.Default.Server.Forums.Notifications
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ITopicNotificationService, TopicNotificationService>(new ContainerControlledLifetimeManager())
                .RegisterType<ITopicQueryBuilder, TopicQueryBuilder>(new PerResolveLifetimeManager());
        }
    }
}
