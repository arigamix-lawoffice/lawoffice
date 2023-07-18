using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Shared.Info;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Server.Cards.Law
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        /// <inheritdoc />
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<LawCaseGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<LawCaseStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<LawCaseNewExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<LawFileSourceRequestExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        /// <inheritdoc />
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                // Get
                .RegisterExtension<ICardGetExtension, LawCaseGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(TypeInfo.LawCase.ID))

                // Store
                .RegisterExtension<ICardStoreExtension, LawCaseStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(TypeInfo.LawCase.ID))

                // New
                .RegisterExtension<ICardNewExtension, LawCaseNewExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(TypeInfo.LawCase.ID))

                .RegisterExtension<ICardRequestExtension, LawFileSourceRequestExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenRequestTypes(CardRequestTypes.GetFileSource)
                    .WhenCardTypes(TypeInfo.LawCase.ID))
                ;
        }
    }
}