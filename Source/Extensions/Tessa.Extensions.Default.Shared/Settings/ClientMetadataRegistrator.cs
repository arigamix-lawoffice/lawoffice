using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Settings
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class ClientMetadataRegistrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer

                .RegisterType<PartnersCardMetadataExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(typeof(ICardMetadata), typeof(ICardCache)))
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                // Отключение ручного ввода и авто-создания контрагентов, если в настройках это выключено
                .RegisterExtension<ICardMetadataExtension, PartnersCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
