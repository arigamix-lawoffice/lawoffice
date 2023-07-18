using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    [Registrator]
    public sealed class Registrator: RegistratorBase
    {
        public override void InitializeExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterKrEventExtensionTypes()
                ;
        }


        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrEventManager, KrEventManager>(new ContainerControlledLifetimeManager())
                .RegisterType<DefaultEventExtension>(new ContainerControlledLifetimeManager());
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IKrEventExtension, DefaultEventExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenEventType("DefaultEventType"))
                ;
        }

    }
}