using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform.Licensing;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class ClientMetadataRegistrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrPermissionsExtensionMetadataExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(ICardMetadata),
                        typeof(ICardCache),
                        typeof(ILicenseManager)))
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                // Модификация типа карточки правил доступа
                .RegisterExtension<ICardMetadataExtension, KrPermissionsExtensionMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
