using Tessa.UI.Tiles.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Регистрация расширений, управляющих плитками.
    /// </summary>
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<GetFake1CTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSettingsTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<TestProcessTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrTypesAndCreateBasedOnTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<AcquaintanceTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<CreateMultipleTemplateTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<CheckTasksTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<StageSourceBuildTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrTilesExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<ExtendedDefaultTypeGroupCaptionsTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrShowHiddenStagesTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<NotificationTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<DocLoadPrintBarcodeTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrDocStateTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrShowSkippedStagesTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrHideLanguageAndFormattingSelectionTileExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<FilterViewDialogOverrideTileExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            // Global
            extensionContainer
                .RegisterExtension<ITileGlobalExtension, ExtendedDefaultTypeGroupCaptionsTileExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton())

                .RegisterExtension<ITileGlobalExtension, WfTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())

                .RegisterExtension<ITileGlobalExtension, KrTilesExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, KrShowHiddenStagesTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithSingleton())

                .RegisterExtension<ITileGlobalExtension, GetFake1CTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, KrSettingsTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 6)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, TestProcessTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 7)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, KrEditModeTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 8)
                    .WithSingleton())

                .RegisterExtension<ITileGlobalExtension, KrTypesAndCreateBasedOnTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 9)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, AcquaintanceTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, CheckTasksTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 11)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, StageSourceBuildTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 12)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, DocLoadPrintBarcodeTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 13)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, KrDocStateTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 14)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, KrPermissionsTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 15)
                    .WithSingleton())

                .RegisterExtension<ITileGlobalExtension, NotificationTileExtension>(x => x
                    .WithOrder(ExtensionStage.Initialize, 16)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileGlobalExtension, KrShowSkippedStagesTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 17)
                    .WithSingleton())

                .RegisterExtension<ITileGlobalExtension, KrHideLanguageAndFormattingSelectionTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 18)
                    .WithUnity(this.UnityContainer))
                ;


            // Local
            extensionContainer
                .RegisterExtension<ITileLocalExtension, WfTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())

                .RegisterExtension<ITileLocalExtension, KrTilesExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, KrShowHiddenStagesTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithSingleton())

                .RegisterExtension<ITileLocalExtension, KrEditModeTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithSingleton())

                .RegisterExtension<ITileLocalExtension, CreateMultipleTemplateTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 5)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, AcquaintanceTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 6)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, CheckTasksTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 7)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, StageSourceBuildTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 8)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, DocLoadPrintBarcodeTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 9)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, DefaultProhibitTilesInViewsTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithSingleton())

                .RegisterExtension<ITileLocalExtension, KrDocStateTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 11)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITileLocalExtension, KrShowSkippedStagesTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 12)
                    .WithSingleton())

                .RegisterExtension<ITileLocalExtension, FilterViewDialogOverrideTileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 13)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
