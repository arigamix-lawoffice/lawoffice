using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Forums.Satellite;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Forums;
using Tessa.Views.Workplaces;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Forums
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<InjectForumCardMetadataExtension>(
                    new ContainerControlledLifetimeManager())
                .RegisterType<ForumGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<IForumPermissionsProvider, KrForumPermissionsProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IForumPermissionsDependencies, ForumPermissionsDependencies>(new ContainerControlledLifetimeManager())
                .RegisterWorkplaceInitializationRule<ForumWorkplaceInitialization>(new PerResolveLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardMetadataExtension, InjectForumCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardGetExtension, ForumGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardGetMethod.Default))
                .RegisterExtension<ICardStoreExtension, ForumSatelliteStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithSingleton()
                    .WhenCardTypes(ForumHelper.ForumSatelliteTypeID))
                ;
        }
    }
}
