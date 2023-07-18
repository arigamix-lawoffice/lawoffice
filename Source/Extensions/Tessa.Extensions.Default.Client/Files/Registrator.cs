using Tessa.Cards.Extensions;
using Tessa.UI.Files;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Files
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            // Card UI
            extensionContainer
                .RegisterExtension<ICardGetFileContentExtension, ClientKrPermissionsGetFileContentExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())

                .RegisterExtension<ICardGetFileVersionsExtension, ClientKrPermissionsGetFileVersionsExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                ;

            // Files
            extensionContainer
                .RegisterExtension<ICardGetFileContentExtension, ClientFileTemplatePermissionsGetFileContentExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton())
                .RegisterExtension<IFileControlExtension, KrAddCycleGroupingFileControlExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<IFileExtension, WfCardFileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                .RegisterExtension<IFileControlExtension, KrCurrentCycleFileControlExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithSingleton())
                ;
        }


        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<PdfPreviewPageExtractor>(new PerResolveLifetimeManager())
                .RegisterType<TiffPreviewPageExtractor>(new ContainerControlledLifetimeManager())
                .RegisterType<IPreviewPageExtractor, DefaultPreviewPageExtractor>(new ContainerControlledLifetimeManager())
                .RegisterType<KrAddCycleGroupingFileControlExtension>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
