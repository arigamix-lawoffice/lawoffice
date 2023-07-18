using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.OnlyOffice.Token;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    [Registrator]
    public sealed class Registrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IOnlyOfficeSettingsProvider, OnlyOfficeSettingsProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IOnlyOfficeFileCacheInfoStrategy, OnlyOfficeFileCacheInfoStrategy>(new ContainerControlledLifetimeManager())
                .RegisterType<IOnlyOfficeFileCache, OnlyOfficeFileCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IOnlyOfficeTokenManager, OnlyOfficeTokenManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IOnlyOfficeService, OnlyOfficeService>(new ContainerControlledLifetimeManager())
                .RegisterType<OnlyOfficeGetExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, OnlyOfficeGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardGetMethod.Default))
                ;
        }
    }
}
