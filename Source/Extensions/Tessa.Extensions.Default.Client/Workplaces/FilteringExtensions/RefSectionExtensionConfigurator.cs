using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workplaces;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Views.Extensions;
using Tessa.Views;

namespace Tessa.Extensions.Default.Client.Workplaces
{
    /// <summary>
    /// Конфигуратор расширения <see cref="RefSectionExtension"/>.
    /// </summary>
    public class RefSectionExtensionConfigurator : ExtensionSettingsConfiguratorBase
    {
        #region Private Fields

        private readonly CreateDialogFormFuncAsync createDialogFormFunc;
        private const string DescriptionLocalization = "$RefSectionExtension_Description";
        private const string NameLocalization = null;

        #endregion

        #region Constructor 

        public RefSectionExtensionConfigurator(
            CreateDialogFormFuncAsync createDialogFormFunc)
            : base(ViewExtensionConfiguratorType.Form, NameLocalization, DescriptionLocalization)
        {
            this.createDialogFormFunc = createDialogFormFunc;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async override ValueTask<(IFormViewModelBase, Action)> GetConfiguratorFormAsync(
            IExtensionConfigurationContext context,
            Action markedAsDirtyAction,
            CancellationToken cancellationToken = default)
        {
            var settingsDict = context.GetSettings();
            var settings = settingsDict == null ? new TreeItemFilteringSettings() : this.Load(settingsDict);

            var (form, cardmodel) = await this.createDialogFormFunc(
                "ViewExtensions",
                "RefSectionExtension",
                cancellationToken: cancellationToken);

            if (form is null)
            {
                TessaDialog.ShowError("$CardTypes_MetadataEditor_ViewExtensionDialog_NotFound");
                return (null, null);
            }

            var refSectionsSection = cardmodel.Card.Sections["RefSections"];
            foreach(var refSection in settings.RefSections)
            {
                var emptyRow = cardmodel.CreateEmptyRow("RefSections");
                emptyRow.RowID = Guid.NewGuid();
                emptyRow["Value"] = refSection;

                var newRow = refSectionsSection.Rows.Insert(refSectionsSection.Rows.Count);
                newRow.Set(emptyRow);
                newRow.State = CardRowState.Inserted;
            }

            var parametersSection = cardmodel.Card.Sections["Parameters"];
            foreach (var parameter in settings.Parameters)
            {
                var emptyRow = cardmodel.CreateEmptyRow("RefSections");
                emptyRow.RowID = Guid.NewGuid();
                emptyRow["Value"] = parameter;

                var newRow = parametersSection.Rows.Insert(parametersSection.Rows.Count);
                newRow.Set(emptyRow);
                newRow.State = CardRowState.Inserted;
            }

            var mainBlock = form switch
            {
                IFormWithBlocksViewModel formWithBlocks => formWithBlocks.Blocks.First(x => x.Name == "MainBlock"),
                IFormWithTabsViewModel formWithTabs => formWithTabs.Tabs.SelectMany(x => x.Blocks).First(x => x.Name == "MainBlock"),
                _ => throw new InvalidOperationException($"Unknown form type created fo form \"ViewExtensions\""),
            };

            void rowValidating(object sender, GridRowValidationEventArgs e)
            {
                var value = e.Row["Value"]?.ToString() ?? string.Empty;
                var regex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$");
                if (!regex.IsMatch(value))
                {
                    e.ValidationResult.AddError(this, "$WorkplaceFilteringExtension_InvalidName_ErrorMessage");
                }
            }

            var refSectionsGridViewModel = (GridViewModel)mainBlock.Controls.First(x => x.Name == "RefSections");
            refSectionsGridViewModel.RowInvoked += (o, e) => markedAsDirtyAction();
            refSectionsGridViewModel.RowValidating += rowValidating;

            var parametersGridViewModel = (GridViewModel)mainBlock.Controls.First(x => x.Name == "Parameters");
            parametersGridViewModel.RowInvoked += (o, e) => markedAsDirtyAction();
            parametersGridViewModel.RowValidating += rowValidating;

            void saveChangesAction()
            {
                settings.RefSections = refSectionsSection.Rows.Select(x => x["Value"].ToString()).ToList();
                settings.Parameters = parametersSection.Rows.Select(x => x["Value"].ToString()).ToList();
                var settingsDict = ExtensionSettingsSerializationHelper.SerializeDictionary(settings);
                context.SaveSettings(settingsDict);
            }

            return (form, saveChangesAction);
        }

        /// <inheritdoc />
        public override void Initialize(IExtensionConfigurationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            SaveSettings(context, new TreeItemFilteringSettings());
        }

        #endregion

        #region Private methods

        private static void SaveSettings(IExtensionConfigurationContext context, ITreeItemFilteringSettings model)
        {
            var settings = ExtensionSettingsSerializationHelper.SerializeDictionary(model);
            context.SaveSettings(settings);
        }

        private ITreeItemFilteringSettings Load(Dictionary<string, object> settings)
        {
            return (ITreeItemFilteringSettings) ExtensionSettingsSerializationHelper.DeserializeDictionary<TreeItemFilteringSettings>(settings);
        }

        #endregion
    }
}
