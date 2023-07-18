using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.StageTypeRequests
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<EditStageTypeGetRequest>(new ContainerControlledLifetimeManager())
                .RegisterType<ExtraTaskTypesSettingsStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<DialogStoreExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void RegisterExtensions(
            IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, EditStageTypeGetRequest>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithSingleton())
                .RegisterExtension<ICardStoreExtension, ExtraTaskTypesSettingsStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID))
                .RegisterExtension<ICardStoreExtension, DialogStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardRequestExtension, DialogCardExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithSingleton()
                    .WhenRequestTypes(KrConstants.LaunchProcessRequestType));
        }
    }
}
