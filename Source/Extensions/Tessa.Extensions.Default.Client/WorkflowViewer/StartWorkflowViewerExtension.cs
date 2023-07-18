using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Licensing;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Controls;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Helpful;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Processors;

namespace Tessa.Extensions.Default.Client.WorkflowViewer
{
    public sealed class StartWorkflowViewerExtension : CardUIExtension
    {
        #region Fields

        private readonly ICardRepository extendedRepository;

        private readonly ILicenseManager licenseManager;

        private readonly ICardDialogManager dialogManager;

        private readonly IDialogService dialogService;

        private readonly Func<INodeProcessor> processorFactory;

        #endregion

        #region Constructors

        public StartWorkflowViewerExtension(
            ILicenseManager licenseManager,
            ICardDialogManager dialogManager,
            IDialogService dialogService,
            Func<INodeProcessor> processorFactory,
            ICardRepository extendedRepository)
        {
            licenseManager.ValidateTypeOnClient();

            this.extendedRepository = extendedRepository;
            this.licenseManager = licenseManager;
            this.dialogManager = dialogManager;
            this.dialogService = dialogService;
            this.processorFactory = processorFactory;
        }

        #endregion

        #region Private Methods

        private async Task ShowWorkflowAsync(IUIContext uiContext)
        {
            ICardEditorModel editor = uiContext.CardEditor;
            ICardModel model;
            if (editor == null
                || (model = editor.CardModel) == null)
            {
                return;
            }

            Card card = model.Card;
            if (card.StoreMode == CardStoreMode.Insert)
            {
                TessaDialog.ShowNotEmpty(ValidationResult.FromText("$KrMessages_WarnCantVizualizeRouteBeforeSaving"));
                return;
            }

            //TODO: предложить обновить карточку
            CardGetResponse response;
            using (TessaSplash.Create(TessaSplashMessage.OpeningCard))
            {
                // Есть смысл переписать как кастомный Request, который грузит нужную информацию на сервере без расширений.
                // Историю заданий лучше не копировать из текущей карточки, т.к. она уже могла устареть, когда кто-то параллельно сделал некоторые действия
                CardGetRequest request = new CardGetRequest
                {
                    CardID = card.ID,
                    CardTypeID = model.CardType.ID,
                    GetMode = CardGetMode.ReadOnly,
                    CompressionMode = CardCompressionMode.Full,
                    RestrictionFlags = CardGetRestrictionFlags.RestrictFiles
                                       | CardGetRestrictionFlags.RestrictTaskSections
                                       | CardGetRestrictionFlags.RestrictTaskCalendar,
                    Info =
                    {
                        { "GetCurrentApprovalStageTasks", BooleanBoxes.True },
                    },
                };

                KrToken.TryGet(card.Info)?.Set(request.Info);

                response = await this.extendedRepository.GetAsync(request);
            }

            ValidationResult result = response.ValidationResult.Build();
            TessaDialog.ShowNotEmpty(
                result.IsSuccessful
                || result.Items.All(x => x.Type == ValidationResultType.Error && x.ObjectType == "KrCheckPermissionsGetExtension")
                || result.Items.Any(x => x.Key == ValidationKeys.Maintenance && !string.IsNullOrEmpty(x.Details))
                    ? result
                    : ValidationResult.FromText("$KrMessages_HaveNoPermissionsToVizualizeRoute"));

            if (!result.IsSuccessful)
            {
                return;
            }

            //Карточка со всеми заданиями согласования (только заданиями согласования)
            Card loadedCard = response.Card;

            //Создаем пустой макет ВФ
            NodeLayout layout = new NodeLayout(dialogService);

            //Создаем фабрику узлов ВФ
            NodeFactory factory = new NodeFactory();

            WorkflowViewerHelper.CreateWorkflow(factory, layout, loadedCard);

            INodeProcessor processor = this.processorFactory();
            processor.Layout = layout;

            this.dialogManager.ShowVisualizer(layout, processor);
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            if (KrProcessSharedHelper.DesignTimeCard(context.Model.CardType.ID)
                || context.Model.Flags.Has(CardModelFlags.Disabled)
                || !NodeProcessorHelper.CheckLicense(await this.licenseManager.GetLicenseAsync(context.CancellationToken), out var _))
            {
                return;
            }

            if (context.Model.Controls.TryGet(KrConstants.Ui.KrApprovalStagesControlAlias, out var control))
            {
                IUIContext uiContext = context.UIContext;

                ((GridViewModel)control).LeftButtons.Insert(
                    0,
                    new UIButton(
                        "$CardTypes_Blocks_Visualize",
                        async btn =>
                        {
                            await btn.CloseAsync();
                            await this.ShowWorkflowAsync(uiContext);
                        }));
            }
        }

        #endregion
    }
}
