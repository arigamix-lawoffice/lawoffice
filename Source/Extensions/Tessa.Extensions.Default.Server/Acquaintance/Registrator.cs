using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Acquaintance;
using Tessa.Notices;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Roles;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Acquaintance
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<AcquaintanceGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<AcquaintanceDeleteExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrAcquaintanceManager, KrAcquaintanceManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrAcquaintanceManager, KrAcquaintanceManager>(
                    KrAcquaintanceManagerNames.Default,
                    new ContainerControlledLifetimeManager())
                .RegisterFactory<IKrAcquaintanceManager>(
                    KrAcquaintanceManagerNames.WithoutTransaction,
                    c => new KrAcquaintanceManager(
                        c.Resolve<ICardTransactionStrategy>(CardTransactionStrategyNames.WithoutTransaction),
                        c.Resolve<INotificationManager>(NotificationManagerNames.WithoutTransaction),
                        c.Resolve<IRoleGetStrategy>(),
                        c.Resolve<INotificationRoleAggregator>(),
                        c.Resolve<IDbScope>(),
                        c.Resolve<ISession>(),
                        c.Resolve<IPlaceholderManager>(),
                        c.Resolve<INotificationDefaultLanguagePicker>(),
                        c.Resolve<IFormattingSettingsCache>(),
                        c),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IKrAcquaintanceManager, KrAcquaintanceManager>(new ContainerControlledLifetimeManager())
                .RegisterType<AcquaintanceRequestExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, AcquaintanceGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardDeleteExtension, AcquaintanceDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardRequestExtension, AcquaintanceRequestExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenRequestTypes(DefaultRequestTypes.Acquaintance))
                ;
        }
    }
}
