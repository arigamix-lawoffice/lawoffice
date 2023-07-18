#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workplaces;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Views.Extensions;
using Tessa.Views;

namespace Tessa.Extensions.Default.Client.Workplaces
{
    /// <summary>
    /// Конфигуратор для расширения <see cref="AutomaticNodeRefreshExtension" />.
    /// </summary>
    public class AutomaticNodeRefreshExtensionConfigurator : ExtensionSettingsConfiguratorBase
    {
        #region Private Fields

        private readonly CreateDialogFormFuncAsync createDialogFormFunc;
        private const string DescriptionLocalization = "$AutomaticNodeRefreshExtension_Description";
        private const string? NameLocalization = null;

        #endregion

        #region Constructor

        public AutomaticNodeRefreshExtensionConfigurator(
            CreateDialogFormFuncAsync createDialogFormFunc)
            : base(ViewExtensionConfiguratorType.Form, NameLocalization, DescriptionLocalization) =>
            this.createDialogFormFunc = NotNullOrThrow(createDialogFormFunc);

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override async ValueTask<(IFormViewModelBase?, Action?)> GetConfiguratorFormAsync(
            IExtensionConfigurationContext context,
            Action markedAsDirtyAction,
            CancellationToken cancellationToken = default)
        {
            var settingsDict = context.GetSettings();
            var settings = settingsDict is null ? new AutomaticNodeRefreshSettings() : Load(settingsDict);

            var (form, cardModel) = await this.createDialogFormFunc(
                "ViewExtensions",
                "AutomaticNodeRefreshExtension",
                cancellationToken: cancellationToken);

            if (form is null || cardModel is null)
            {
                TessaDialog.ShowError("$CardTypes_MetadataEditor_ViewExtensionDialog_NotFound");
                return (null, null);
            }

            var section = cardModel.Card.Sections["AutomaticNodeRefreshExtension"];
            section.Fields[nameof(IAutomaticNodeRefreshSettings.RefreshInterval)] = settings.RefreshInterval;
            section.Fields[nameof(IAutomaticNodeRefreshSettings.WithContentDataRefreshing)] = settings.WithContentDataRefreshing;

            section.FieldChanged += (o, e) =>
            {
                markedAsDirtyAction();
                switch (e.FieldName)
                {
                    case nameof(IAutomaticNodeRefreshSettings.RefreshInterval):
                        settings.RefreshInterval = (int) e.FieldValue!;
                        break;
                    case nameof(IAutomaticNodeRefreshSettings.WithContentDataRefreshing):
                        settings.WithContentDataRefreshing = (bool) e.FieldValue!;
                        break;
                }
            };

            void SaveChangesAction()
            {
                var localSettings = ExtensionSettingsSerializationHelper.SerializeDictionary(settings);
                context.SaveSettings(localSettings);
            }

            return (form, SaveChangesAction);
        }

        /// <inheritdoc />
        public override void Initialize(IExtensionConfigurationContext context)
        {
            ThrowIfNull(context);

            var model = new AutomaticNodeRefreshSettings();
            SaveSettings(context, model);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The save settings.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        private static void SaveSettings(IExtensionConfigurationContext context, IAutomaticNodeRefreshSettings model)
        {
            var settings = ExtensionSettingsSerializationHelper.SerializeDictionary(model);
            context.SaveSettings(settings);
        }

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <returns>
        /// The <see cref="IAutomaticNodeRefreshSettings"/>.
        /// </returns>
        private static IAutomaticNodeRefreshSettings Load(Dictionary<string, object?> settings) =>
            settings.Count == 0
                ? new AutomaticNodeRefreshSettings()
                : (IAutomaticNodeRefreshSettings) ExtensionSettingsSerializationHelper.DeserializeDictionary<AutomaticNodeRefreshSettings>(settings);

        #endregion
    }
}
