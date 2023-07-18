using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Views;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.Extensions;
using Tessa.Views;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="CreateCardExtensionConfigurator"/>
    /// </remarks>
    public sealed class CreateCardExtension :
        IWorkplaceViewComponentExtension,
        IWorkplaceExtensionSettingsRestore
    {
        #region Constructors

        public CreateCardExtension(
            IUIHost uiHost,
            IAdvancedCardDialogManager advancedCardDialogManager,
            ICardRepository cardRepository,
            IIconContainer iconContainer,
            IKrTypesCache krTypesCache)
        {
            this.uiHost = uiHost;
            this.advancedCardDialogManager = advancedCardDialogManager;
            this.cardRepository = cardRepository;
            this.iconContainer = iconContainer;
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region Fields

        private readonly IUIHost uiHost;

        private readonly IAdvancedCardDialogManager advancedCardDialogManager;

        private readonly ICardRepository cardRepository;

        private readonly IIconContainer iconContainer;

        private readonly IKrTypesCache krTypesCache;

        private CreateCardExtensionSettings settings = new CreateCardExtensionSettings();

        #endregion

        #region IWorkplaceViewComponentExtension Members

        /// <inheritdoc />
        public void Initialize(IWorkplaceViewComponent model) =>
            model.ContentFactories[nameof(CreateCardExtension)] = component =>
                new CreateCardExtensionViewModel(
                    this.settings,
                    this.uiHost,
                    this.advancedCardDialogManager,
                    this.cardRepository,
                    this.iconContainer,
                    this.krTypesCache,
                    component,
                    ContentPlaceAreas.ToolbarPlaces);


        /// <inheritdoc />
        public void Initialized(IWorkplaceViewComponent model)
        {
        }


        /// <inheritdoc />
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }


        /// <inheritdoc />
        public void Restore(Dictionary<string, object> metadata) =>
            this.settings = (CreateCardExtensionSettings) ExtensionSettingsSerializationHelper.DeserializeDictionary<CreateCardExtensionSettings>(metadata);

        #endregion
    }
}