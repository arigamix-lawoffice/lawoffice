using Tessa.Cards.Extensions;
using Tessa.UI.Cards;
using Tessa.UI.Tiles.Extensions;
using Tessa.UI.WorkflowViewer.Processors;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void InitializeExtensions(IExtensionContainer extensionContainer)
        {
            // расширения IWfResolutionVisualizationExtension
            extensionContainer
                .RegisterType<IWfResolutionVisualizationExtension>(x => x
                    .MethodAsync<IWfResolutionVisualizationContext>(y => y.OnVisualizationStarted)
                    .MethodAsync<IWfResolutionVisualizationContext>(y => y.OnNodeGenerating)
                    .MethodAsync<IWfResolutionVisualizationContext>(y => y.OnNodeGenerated)
                    .MethodAsync<IWfResolutionVisualizationContext>(y => y.OnVisualizationCompleted))
                ;
        }


        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IWfResolutionVisualizationGenerator, WfResolutionVisualizationGenerator>(new ContainerControlledLifetimeManager())
                .RegisterType<INodeProcessor, ResolutionsNodeProcessor>("ResolutionsNodeProcessor", new PerResolveLifetimeManager())
                ;

            this.UnityContainer
                .RegisterType<WfTaskSatelliteClientGetFileContentExtension>(new ContainerControlledLifetimeManager())

                .RegisterType<WfCardUIExtension>(
                    new ContainerControlledLifetimeManager())

                .RegisterType<WfTaskHistoryViewUIExtension>(
                    new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            // Card Get
            extensionContainer
                .RegisterExtension<ICardGetExtension, WfTasksClientGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                ;

            // Card GetFileContent
            extensionContainer
                .RegisterExtension<ICardGetFileContentExtension, WfTaskSatelliteClientGetFileContentExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                ;

            // Card UI
            extensionContainer
                .RegisterExtension<ICardUIExtension, WfCardUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardUIExtension, WfTypeSettingsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithSingleton())
                .RegisterExtension<ICardUIExtension, WfTaskSatelliteUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithSingleton())
                .RegisterExtension<ICardUIExtension, WfTaskHistoryViewUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))
                
                ;
        }
    }
}
