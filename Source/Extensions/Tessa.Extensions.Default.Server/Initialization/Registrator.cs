using Tessa.Platform.Initialization;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Initialization
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrServerInitializationExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrWebServerInitializationExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrWebDownloadServerInitializationExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IServerInitializationExtension, KrServerInitializationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.TessaClient, ApplicationIdentifiers.TessaAdminConsole))

                .RegisterExtension<IServerInitializationExtension, KrWebServerInitializationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.WebClient))

                .RegisterExtension<IServerInitializationExtension, KrWebDownloadServerInitializationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.WebClient))
                ;
        }
    }
}
