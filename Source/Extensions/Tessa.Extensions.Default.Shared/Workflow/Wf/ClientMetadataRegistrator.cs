using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class ClientMetadataRegistrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<WfCardMetadataExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(typeof(ICardMetadata)))
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardMetadataExtension, WfCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
