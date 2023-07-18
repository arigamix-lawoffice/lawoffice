using Tessa.Cards.Extensions;
using Tessa.Cards.Validation;
using Tessa.UI.Cards;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrTokenToTaskHistoryUIExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrTokenToTaskHistoryViewUIExtension>(new ContainerControlledLifetimeManager())


                .RegisterType<ICardValidationManager, KrCardValidationManager>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, KrDontSkipEditModeGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton())

                .RegisterExtension<ICardStoreExtension, KrKeepReadCardPermissionStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithSingleton())

                .RegisterExtension<ICardGetExtension, KrKeepReadCardPermissionGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 3)
                    .WithSingleton())

                .RegisterExtension<ICardUIExtension, KrTokenToTaskHistoryUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrTokenToTaskHistoryViewUIExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithSingleton())
                ;
        }
        
    }
}
