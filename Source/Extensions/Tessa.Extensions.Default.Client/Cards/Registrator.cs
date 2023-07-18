using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.UI.Cards;
using Tessa.UI.Views.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Cards
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<OpenFromKrDocStatesOnDoubleClickExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<CalendarStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<CalendarTypeStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrPermissionsMandatoryStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<ICardTaskAssignedRolesAccessProvider, KrCardTaskAssignedRolesAccessProvider>(new PerResolveLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer

                // Store
                .RegisterExtension<ICardStoreExtension, AcquaintanceClientStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                .RegisterExtension<ICardStoreExtension, KrPermissionsMandatoryStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardStoreExtension, CalendarStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.CalendarTypeID))
                .RegisterExtension<ICardStoreExtension, CalendarTypeStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.DefaultCalendarTypeTypeID))

                // Delete
                .RegisterExtension<ICardDeleteExtension, KrDocStateClientDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrDocStateTypeID))
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IWorkplaceExtensionRegistry>()
                ?

                .Register(typeof(OpenFromKrDocStatesOnDoubleClickExtension))
                .RegisterConfiguratorType(
                    typeof(OpenFromKrDocStatesOnDoubleClickExtension),
                    type => this.UnityContainer.Resolve<OpenFromKrDocStatesOnDoubleClickExtensionConfigurator>())
                ;
        }
    }
}