using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Views;
using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls.AutoComplete;
using Tessa.UI.Controls.Helpers;
using Tessa.UI.Views.Extensions;
using Tessa.Views;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="CreateCardExtension"/>.
    /// </summary>
    public sealed class CreateCardExtensionConfigurator : ExtensionSettingsConfiguratorBase
    {
        #region Private Fields

        private readonly CreateDialogFormFuncAsync createDialogFormFunc;
        private readonly AutoCompleteDialogProvider autoCompleteDialogProvider;

        private static readonly IReadOnlyDictionary<CardCreationKind, string> CardCreationKindLocalisations =
            new Dictionary<CardCreationKind, string>()
        {
            [CardCreationKind.ByDocTypeIdentifier] = "$CreateCardExtensionSettingsViewModel_CardCreationKind_ByDocTypeIdentifier",
            [CardCreationKind.ByTypeAlias] = "$CreateCardExtensionSettingsViewModel_CardCreationKind_ByTypeAlias",
            [CardCreationKind.ByTypeFromSelection] = "$CreateCardExtensionSettingsViewModel_CardCreationKind_ByTypeFromSelection"
            };

        private static readonly IReadOnlyDictionary<CardOpeningKind, string> CardOpeningKindLocalisations =
            new Dictionary<CardOpeningKind, string>()
        {
            [CardOpeningKind.ApplicationTab] = "$CreateCardExtensionSettingsViewModel_CardOpeningKind_ApplicationTab",
            [CardOpeningKind.ModalDialog] = "$CreateCardExtensionSettingsViewModel_CardOpeningKind_ModalDialog"
        };

        private const string DescriptionLocalization = "$CreateCardExtension_Description";
        private const string NameLocalization = null;

        #endregion

        #region Constructor

        public CreateCardExtensionConfigurator(
            CreateDialogFormFuncAsync createDialogFormFunc,
            AutoCompleteDialogProvider autoCompleteDialogProvider)
            : base(ViewExtensionConfiguratorType.Form, NameLocalization, DescriptionLocalization)
        {
            this.createDialogFormFunc = createDialogFormFunc;
            this.autoCompleteDialogProvider = autoCompleteDialogProvider;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async override ValueTask<(IFormViewModelBase, Action)> GetConfiguratorFormAsync(
            IExtensionConfigurationContext context,
            Action markedAsDirtyAction,
            CancellationToken cancellationToken = default)
        {
            CreateCardExtensionSettings settings = LoadOrCreate(context);

            var (form, cardmodel) = await this.createDialogFormFunc(
                "ViewExtensions",
                "CreateCardExtension",
                cancellationToken: cancellationToken,
                modifyModelAsync: this.ModifyCardModelAsync);

            if (form is null)
            {
                TessaDialog.ShowError("$CardTypes_MetadataEditor_ViewExtensionDialog_NotFound");
                return (null, null);
            }

            var section = cardmodel.Card.Sections["CreateCardExtension"];
            section.Fields["IDParam"] = settings.IDParam;
            section.Fields["CardOpeningKindName"] = CardOpeningKindLocalisations[settings.CardOpeningKind];
            section.Fields["CreateCardKindName"] = CardCreationKindLocalisations[settings.CardCreationKind];
            section.Fields["TypeAlias"] = settings.TypeAlias;
            section.Fields["DocTypeIdentifier"] = settings.DocTypeIdentifier;

            var mainBlock = form switch
            {
                IFormWithBlocksViewModel formWithBlocks => formWithBlocks.Blocks.First(x => x.Name == "MainBlock"),
                IFormWithTabsViewModel formWithTabs => formWithTabs.Tabs.SelectMany(x => x.Blocks).First(x => x.Name == "MainBlock"),
                _ => throw new InvalidOperationException($"Unknown form type created fo form \"ViewExtensions\""),
            };

            section.FieldChanged += (o, e) =>
            {
                markedAsDirtyAction();
                switch (e.FieldName)
                {
                    case "IDParam":
                        settings.IDParam = e.FieldValue?.ToString();
                        break;
                    case "CardOpeningKindName":
                        if(e.FieldValue is not null)
                        {
                            var kind = CardOpeningKindLocalisations.First(x => x.Value == e.FieldValue.ToString()).Key;
                            settings.CardOpeningKind = kind;
                        }
                        break;
                    case "CreateCardKindName":
                        if (e.FieldValue is not null)
                        {
                            var kind = CardCreationKindLocalisations.First(x => x.Value == e.FieldValue.ToString()).Key;
                            settings.CardCreationKind = kind;
                            UpdateConditionalControls(settings, section, mainBlock);
                        }
                        break;
                    case "TypeAlias":
                        settings.TypeAlias = e.FieldValue?.ToString();
                        break;
                    case "DocTypeIdentifier":
                        settings.DocTypeIdentifier = e.FieldValue?.ToString();
                        break;
                }
            };

            UpdateConditionalControls(settings, section, mainBlock);

            void saveChangesAction()
            {
                var serializedSettings = ExtensionSettingsSerializationHelper.SerializeDictionary(settings);
                context.SaveSettings(serializedSettings);
            }
            return (form, saveChangesAction);
        }

        /// <inheritdoc />
        public override void Initialize(IExtensionConfigurationContext context)
        {
            CreateCardExtensionSettings settingsModel = new CreateCardExtensionSettings();
            var serializedSettings = ExtensionSettingsSerializationHelper.SerializeDictionary(settingsModel);
            context.SaveSettings(serializedSettings);
        }

        #endregion

        #region Private Methods

        private static void UpdateConditionalControls(
            CreateCardExtensionSettings settings,
            CardSection section,
            IBlockViewModel block)
        {
            switch (settings.CardCreationKind)
            {
                case CardCreationKind.ByTypeAlias:
                    block.Controls.First(x => x.Name == "TypeAlias").ControlVisibility = Visibility.Visible;
                    block.Controls.First(x => x.Name == "DocTypeIdentifier").ControlVisibility = Visibility.Collapsed;
                    section.Fields["DocTypeIdentifier"] = null;
                    break;
                case CardCreationKind.ByDocTypeIdentifier:
                    block.Controls.First(x => x.Name == "TypeAlias").ControlVisibility = Visibility.Collapsed;
                    block.Controls.First(x => x.Name == "DocTypeIdentifier").ControlVisibility = Visibility.Visible;
                    section.Fields["TypeAlias"] = null;
                    break;
                case CardCreationKind.ByTypeFromSelection:
                    block.Controls.First(x => x.Name == "TypeAlias").ControlVisibility = Visibility.Collapsed;
                    block.Controls.First(x => x.Name == "DocTypeIdentifier").ControlVisibility = Visibility.Collapsed;
                    section.Fields["TypeAlias"] = null;
                    section.Fields["DocTypeIdentifier"] = null;
                    break;
            }
            block.RearrangeSelf();
        }

        private ValueTask ModifyCardModelAsync(ICardModel cardModel, CancellationToken cancellationToken = default)
        {
            cardModel.ControlInitializers.Add(
                (controlViewModel, cm, cr, ct) =>
                {
                    switch (controlViewModel)
                    {
                        case AutoCompleteEntryViewModel autoComplete:
                            switch (autoComplete.Name)
                            {
                                case "CardOpeningKind":
                                    var cardOpeningKindsView = new NamedRecordsView<string>(
                                               "NamedValue",
                                               CardOpeningKindLocalisations.Values,
                                               x => new object[] { Guid.Empty, x },
                                               x => x);
                                    autoComplete.View = cardOpeningKindsView;
                                    autoComplete.ViewComboBox = cardOpeningKindsView;

                                    this.autoCompleteDialogProvider.ChangeAutoCompleteDialog(autoComplete);
                                    break;
                                case "CreateCardKind":
                                    var cardCreateKindsView = new NamedRecordsView<string>(
                                               "NamedValue",
                                               CardCreationKindLocalisations.Values,
                                               x => new object[] { Guid.Empty, x },
                                               x => x);
                                    autoComplete.View = cardCreateKindsView;
                                    autoComplete.ViewComboBox = cardCreateKindsView;

                                    this.autoCompleteDialogProvider.ChangeAutoCompleteDialog(autoComplete);
                                    break;
                            }
                        break;
                    }
                    return new ValueTask();
                });
            return new ValueTask();
        }

        /// <summary>
        /// The load or create.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="CreateCardExtensionSettings"/>.
        /// </returns>
        private static CreateCardExtensionSettings LoadOrCreate([NotNull] IExtensionConfigurationContext context)
        {
            Contract.Requires(context != null);
            var settings = context.GetSettings();
            return settings == null
                       ? new CreateCardExtensionSettings()
                       : (CreateCardExtensionSettings) ExtensionSettingsSerializationHelper.DeserializeDictionary<CreateCardExtensionSettings>(settings);
        }

        #endregion
    }
}
