using Tessa.Forums;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.UI.Cards;
using Tessa.UI.Controls.Forums;
using Tessa.UI.Views.Extensions;
using Tessa.UI.Windows;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Forums
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<TopicsUIExtension>(new PerResolveLifetimeManager(),
                    new InjectionConstructor(
                        typeof(IForumDialogManager),
                        typeof(IDocumentTabManager),
                        typeof(IWorkplaceInterpreter)))
                .RegisterType<FmNotificationsApplicationExtension>(new PerResolveLifetimeManager())
                .RegisterType<OpenTopicOnDoubleClickExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<OpenForumContextMenuViewExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<IForumPermissionsProvider, KrClientForumPermissionsProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<HideForumTabUIExtension>(new ContainerControlledLifetimeManager())
                ;
        }
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardUIExtension, KrSettingsForumsSettingsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                .RegisterExtension<ICardUIExtension, ForumControlUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton())
                .RegisterExtension<ICardUIExtension, HideForumTabUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithUnity(this.UnityContainer))

                //Важно чтобы это расширение отрабатывало позже HideForumTabUIExtension
                .RegisterExtension<ICardUIExtension, TopicsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<IApplicationExtension, FmNotificationsApplicationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 5)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.TessaClient))
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IWorkplaceExtensionRegistry>()
                ?
                .Register(typeof(OpenTopicOnDoubleClickExtension))
                .RegisterConfiguratorType(
                    typeof(OpenTopicOnDoubleClickExtension),
                    type => this.UnityContainer.Resolve<OpenTopicOnDoubleClickExtensionConfigurator>())

                .Register(typeof(OpenForumContextMenuViewExtension))
                .RegisterConfiguratorType(
                    typeof(OpenForumContextMenuViewExtension),
                    type => this.UnityContainer.Resolve<OpenForumContextMenuViewExtensionConfigurator>())
                
                ; 
        }
    }
}
