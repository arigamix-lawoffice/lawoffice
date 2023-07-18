using Tessa.UI.Cards;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.WorkflowViewer
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<StartWorkflowViewerExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardUIExtension, StartWorkflowViewerExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
