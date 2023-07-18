using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI.CardFiles;
using Tessa.Extensions.Default.Client.UI.KrProcess;
using Tessa.Extensions.Default.Client.UI.WorkflowEngine;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Регистрация расширений для UI карточки.
    /// </summary>
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterSingleton<KrGetCycleFileInfoUIExtension>()
                .RegisterSingleton<CarUIExtension>()
                .RegisterSingleton<KrFilesUIExtension>()
                .RegisterSingleton<KrUIExtension>()
                .RegisterSingleton<KrHideCardTypeSettingsUIExtension>()
                .RegisterSingleton<KrHideApprovalTabOrDocStateBlockUIExtension>()
                .RegisterSingleton<KrHideApprovalStagePermissionsDisclaimer>()
                .RegisterSingleton<KrDocumentWorkspaceInfoUIExtension>()
                .RegisterSingleton<OutgoingPartnerUIExtension>()
                .RegisterSingleton<KrExtendedPermissionsUIExtension>()
                .RegisterSingleton<CalendarUIExtension>()
                .RegisterType<KrAdditionalApprovalCardUIExtension>(new PerResolveLifetimeManager())
                .RegisterSingleton<KrRecalcStagesUIExtension>()
                .RegisterSingleton<KrStageSourceUIExtension>()
                .RegisterSingleton<KrStageTemplateUIExtension>()
                .RegisterType<KrStageUIExtension>(new PerResolveLifetimeManager())
                .RegisterSingleton<KrTilesUIExtension>()
                .RegisterType<KrSecondaryProcessUIExtension>(new PerResolveLifetimeManager())
                .RegisterSingleton<KrTemplateUIExtension>()
                .RegisterSingleton<KrEditModeToolbarUIExtension>()
                .RegisterSingleton<CreateAndSelectToolbarUIExtension>()
                .RegisterSingleton<KrVirtualFilesUIExtension>()
                .RegisterSingleton<KrPermissionsUIExtension>()
                .RegisterSingleton<IViewCardControlInitializationStrategy, FilesViewCardControlInitializationStrategy>(
                    nameof(FilesViewCardControlInitializationStrategy))
                .RegisterSingleton<CustomFilesViewCardControlInitializationStrategy>()
                .RegisterType<InitializeFilesViewUIExtension>(new PerResolveLifetimeManager())
                .RegisterSingleton<KrRoutesInWorkflowEngineUIExtension>()
                .RegisterSingleton<OpenCardInViewUIExtension>()
                .RegisterSingleton<TagDemoCardActionExtension>()
                .RegisterSingleton<KrCardTasksEditorUIExtension>()
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardUIExtension, KrGetCycleFileInfoUIExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, CarUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.CarTypeID))

                .RegisterExtension<ICardUIExtension, KrFilesUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 3)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, CalendarUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.CalendarTypeID))

                .RegisterExtension<ICardUIExtension, KrAdditionalApprovalCardUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 5)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrHideCardTypeSettingsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 7)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID, DefaultCardTypes.KrDocTypeTypeID))

                .RegisterExtension<ICardUIExtension, KrHideApprovalTabOrDocStateBlockUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 8)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrRecalcStagesUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 9)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrStageSourceUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(KrConstants.CompiledCardTypes))

                .RegisterExtension<ICardUIExtension, KrStageTemplateUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 11)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID, DefaultCardTypes.KrSecondaryProcessTypeID))

                .RegisterExtension<ICardUIExtension, KrStageUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 12)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrTilesUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 13)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrSecondaryProcessUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 14)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID))

                .RegisterExtension<ICardUIExtension, KrTemplateUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 15)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrEditModeToolbarUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 16)
                    .WithSingleton()
                    .WhenDefaultDialog())

                .RegisterExtension<ICardUIExtension, CreateAndSelectToolbarUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 17)
                    .WithSingleton()
                    .WhenDefaultDialog())

                .RegisterExtension<ICardUIExtension, KrDocStateUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 18)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrDocStateTypeID))

                .RegisterExtension<ICardUIExtension, KrVirtualFilesUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 19)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrVirtualFileTypeID))

                .RegisterExtension<ICardUIExtension, KrPermissionsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 20)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrPermissionsTypeID))

                .RegisterExtension<ICardUIExtension, KrSettingsForumsLicenseUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 20)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID, DefaultCardTypes.KrDocTypeTypeID))

                .RegisterExtension<ICardUIExtension, KrHideApprovalStagePermissionsDisclaimer>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 50)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, KrDocumentWorkspaceInfoUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 101)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, OutgoingPartnerUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 102)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.OutgoingTypeID))

                .RegisterExtension<ICardUIExtension, KrRequestCommentUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 103)
                    .WithSingleton())

                .RegisterExtension<ICardUIExtension, KrExtendedPermissionsUIExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 104)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ICardUIExtension, InitializeFilesViewUIExtension>(x => x
                    .WithUnity(this.UnityContainer)
                    .WithOrder(ExtensionStage.AfterPlatform, 106))

                .RegisterExtension<ICardUIExtension, KrRoutesInWorkflowEngineUIExtension>(x => x
                    .WithUnity(this.UnityContainer)
                    .WithOrder(ExtensionStage.AfterPlatform, 107))

                .RegisterExtension<ICardUIExtension, OpenCardInViewUIExtension>(x => x
                    .WithUnity(this.UnityContainer)
                    .WithOrder(ExtensionStage.AfterPlatform, 108))

                .RegisterExtension<ICardUIExtension, TagDemoCardActionExtension>(x => x
                    .WithUnity(this.UnityContainer)
                    .WithOrder(ExtensionStage.AfterPlatform, 109)
                    .WhenCardTypes(DefaultCardTypes.CarTypeID))

                .RegisterExtension<ICardUIExtension, KrCardTasksEditorUIExtension>(x => x
                    .WithUnity(this.UnityContainer)
                    .WhenDialog("ModifyCardTasksDialog")
                    .WithOrder(ExtensionStage.AfterPlatform, 109)
                    .WhenCardTypes(CardHelper.CardTasksEditorDialogTypeID))
                ;
        }
    }
}
