using Tessa.Cards;
using Tessa.Cards.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.BusinessCalendar
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<CalendarStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<CalendarTypeStoreExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                // New
                .RegisterExtension<ICardNewExtension, CalendarNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(CardHelper.CalendarTypeID)
                    .WhenAnyNewMethod())
                .RegisterExtension<ICardNewExtension, CalendarTypeNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WhenCardTypes(CardHelper.DefaultCalendarTypeTypeID)
                    .WhenAnyNewMethod())

                // Get
                .RegisterExtension<ICardGetExtension, CalendarNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(CardHelper.CalendarTypeID)
                    .WhenAnyGetMethod())
                .RegisterExtension<ICardGetExtension, CalendarTypeNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WhenCardTypes(CardHelper.DefaultCalendarTypeTypeID)
                    .WhenAnyGetMethod())

                // Store
                .RegisterExtension<ICardStoreExtension, CalendarStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.CalendarTypeID))
                .RegisterExtension<ICardStoreExtension, CalendarTypeStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.DefaultCalendarTypeTypeID))
                ;
        }
    }
}
