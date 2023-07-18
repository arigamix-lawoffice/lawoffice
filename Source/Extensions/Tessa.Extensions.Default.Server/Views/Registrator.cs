using Tessa.Views;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Views
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IViewInterceptor, ViewsInterceptor>(nameof(ViewsInterceptor))
                .RegisterType<IViewInterceptor, ExampleInterceptor>(nameof(ExampleInterceptor))
                .RegisterType<IViewInterceptor, ChangeConnectionInterceptor>(nameof(ChangeConnectionInterceptor))
                .RegisterType<IViewInterceptor, TaskHistoryInterceptor>(nameof(TaskHistoryInterceptor))
                .RegisterType<IViewInterceptor, TopicParticipantsInterceptor>(nameof(TopicParticipantsInterceptor))
                .RegisterType<IViewInterceptor, CardTasksInterceptor>(nameof(CardTasksInterceptor))
                .RegisterType<IViewInterceptor, FileCategoriesViewInterceptor>(nameof(FileCategoriesViewInterceptor))

                .RegisterType<IExtraViewProvider, TransientViewExtraProvider>(
                    nameof(TransientViewExtraProvider),
                    new ContainerControlledLifetimeManager())

                .RegisterType<IExtraViewListProvider, KrPermissionsViewListProvider>(
                    nameof(KrPermissionsViewListProvider),
                    new ContainerControlledLifetimeManager())
                ;
        }
    }
}
