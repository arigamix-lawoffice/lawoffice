using Tessa.Platform.Initialization;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Initialization
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<GlobalButtonsInitializationExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(
            IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IServerInitializationExtension, GlobalButtonsInitializationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.TessaClient, ApplicationIdentifiers.WebClient))
                ;
        }
    }
}