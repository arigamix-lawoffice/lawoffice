#nullable enable

using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.TextRecognition.Constants;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.TextRecognition
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity() => this.UnityContainer
            .RegisterType<OcrSourceCardStoreExtension>(new ContainerControlledLifetimeManager())
            .RegisterType<OcrOperationStoreExtension>(new ContainerControlledLifetimeManager())
            .RegisterType<OcrOperationPermissionsGetExtension>(new ContainerControlledLifetimeManager());

        public override void RegisterExtensions(IExtensionContainer extensionContainer) => extensionContainer
            // Store
            .RegisterExtension<ICardStoreExtension, OcrSourceCardStoreExtension>(x => x
                .WithOrder(ExtensionStage.AfterPlatform)
                .WithUnity(this.UnityContainer)
                .WhenAnyCardType()
                .WhenMethod(CardStoreMethod.Default))
            .RegisterExtension<ICardStoreExtension, OcrOperationStoreExtension>(x => x
                .WithOrder(ExtensionStage.AfterPlatform)
                .WithUnity(this.UnityContainer)
                .WhenCardTypes(OcrCardTypes.OcrOperationTypeID))
            // Get
            .RegisterExtension<ICardGetExtension, OcrOperationPermissionsGetExtension>(x => x
                .WithOrder(ExtensionStage.AfterPlatform)
                .WithUnity(this.UnityContainer)
                .WhenCardTypes(OcrCardTypes.OcrOperationTypeID))
            ;
    }
}
