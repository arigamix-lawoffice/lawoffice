using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.EDS;
using Tessa.Platform.EDS;
using Tessa.UI.Cards;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.EDS
{
    /// <summary>
    /// Регистратор должен выполняться после <see cref="Tessa.Extensions.Default.Shared.EDS.Registrator"/>,
    /// чтобы переопределить регистрацию интерфейса без имени <see cref="ICAdESManager"/>.
    /// </summary>
    [Registrator(Order = 1)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ICAdESManager, CryptoProEDSManager>(nameof(CryptoProEDSManager), new ContainerControlledLifetimeManager())
                .RegisterType<ICAdESManager, ServiceEDSManagerForCMS>(nameof(ServiceEDSManagerForCMS), new ContainerControlledLifetimeManager())
                .RegisterType<ICAdESManager, ServiceEDSManagerForCAdES>(nameof(ServiceEDSManagerForCAdES), new ContainerControlledLifetimeManager())
                .RegisterFactory<ICAdESManager>(
                    c => new AggregateEDSManager(
                        c.Resolve<ICardRepository>(),
                        c.Resolve<ICardCache>(),
                        c,
                        resolveFallbackEdsManagerFunc: () => c.Resolve<ICAdESManager>(nameof(DefaultEDSManager))),
                    new ContainerControlledLifetimeManager())
                .RegisterFactory<IEDSManager>(
                    c => c.Resolve<ICAdESManager>(),
                    new ContainerControlledLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardUIExtension, SignatureSettingsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(SignatureHelper.SignatureSettingsType))

                .RegisterExtension<ICardStoreExtension, SignatureSettingsStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(SignatureHelper.SignatureSettingsType))
                    ;
        }
    }
}
