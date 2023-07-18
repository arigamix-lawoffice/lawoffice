using Tessa.Cards.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrScope, KrScope>(new ContainerControlledLifetimeManager())
                .RegisterType<KrLifecycleScopeStoreExtension>(new PerResolveLifetimeManager())
                .RegisterType<KrScopeStoreExtension>(new PerResolveLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardStoreExtension, KrLifecycleScopeStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, -100)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardStoreExtension, KrScopeStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 100)
                    .WithUnity(this.UnityContainer));
        }
    }
}