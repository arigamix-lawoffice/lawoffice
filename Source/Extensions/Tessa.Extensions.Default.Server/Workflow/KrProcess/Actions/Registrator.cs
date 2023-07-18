using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Actions
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<EventCollector>(new ContainerControlledLifetimeManager())
                .RegisterType<CardNewEventEmitter>(new ContainerControlledLifetimeManager())
                .RegisterType<CardStoreEventEmitter>(new ContainerControlledLifetimeManager())
                .RegisterType<TaskStoreEventEmitter>(new ContainerControlledLifetimeManager())

                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IKrEventExtension, EventCollector>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer))

                // Выполняется после всего остального
                .RegisterExtension<ICardNewExtension, CardNewEventEmitter>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 20)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardStoreExtension, CardStoreEventEmitter>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 20)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardStoreTaskExtension, TaskStoreEventEmitter>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 20)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}