using Tessa.Cards.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowViewer
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<GetCardForWorkflowViewerGetExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, GetCardForWorkflowViewerGetExtension>(x => x
                   .WithOrder(ExtensionStage.AfterPlatform, 1)
                   .WithUnity(this.UnityContainer))
                ;
        }
    }
}
