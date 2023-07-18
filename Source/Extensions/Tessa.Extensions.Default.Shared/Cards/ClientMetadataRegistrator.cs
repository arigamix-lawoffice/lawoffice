using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Licensing;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Cards
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class ClientMetadataRegistrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrSettingsCardMetadataExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(typeof(ICardMetadata), typeof(ILicenseManager)))
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardMetadataExtension, KrSettingsCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 0)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
