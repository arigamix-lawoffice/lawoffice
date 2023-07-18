using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workplaces;
using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls.AutoComplete;
using Tessa.UI.Controls.Helpers;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.Extensions;
using Tessa.Views;
using Tessa.Views.Metadata;
using Unity;

namespace Tessa.Extensions.Default.Client.Workplaces.Manager
{
    /// <summary>
    /// Конфигуратор расширения <see cref="ManagerWorkplaceExtension"/>.
    /// </summary>
    public sealed class ManagerWorkplaceExtensionConfigurator : ExtensionSettingsConfiguratorBase
    {
        #region Private Fields

        private readonly CreateDialogFormFuncAsync createDialogFormFunc;
        private readonly AutoCompleteDialogProvider autoCompleteDialogProvider;
        private readonly IUnityContainer container;

        private const string DescriptionLocalization = "$ManagerWorkplaceExtension_Description";
        private const string NameLocalization = null;

        #endregion

        #region Constructor

        public ManagerWorkplaceExtensionConfigurator(
            IUnityContainer container,
            CreateDialogFormFuncAsync createDialogFormFunc,
            AutoCompleteDialogProvider autoCompleteDialogProvider)
            : base(ViewExtensionConfiguratorType.Form, NameLocalization, DescriptionLocalization)
        {
            this.container = container;
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
            using IUnityContainer localContainer = this.container.CreateChildContainer();
            IDataSourceMetadata dataSourceMetadata = context.GetPropertyValue<IDataSourceMetadata>("Metadata");
            IWorkplaceViewComponent item = await this.GetViewComponentAsync(dataSourceMetadata, localContainer, cancellationToken);
            IEnumerable<string> columnNames = Enumerable.Empty<string>();
            if (item != null)
            {
                var metadata = await item.GetViewMetadataAsync(item, cancellationToken);
                if (metadata != null)
                {
                    columnNames = metadata.Columns.Select(c => c.Alias).ToArray();
                }
            }

            var settings = this.LoadOrCreate(context);

            var (form, cardmodel) = await this.createDialogFormFunc(
                "ViewExtensions",
                "ManagerWorkplaceExtension",
                cancellationToken: cancellationToken,
                modifyModelAsync: (cm, ct) =>
                {
                    cm.ControlInitializers.Add(
                        (controlViewModel, cm, cr, ct) =>
                        {
                            switch (controlViewModel)
                            {
                                case AutoCompleteEntryViewModel autoComplete:
                                    switch (autoComplete.Name)
                                    {
                                        case "ActiveImageColumnName"
                                        or "CountColumnName"
                                        or "HoverImageColumnName"
                                        or "InactiveImageColumnName"
                                        or "TileColumnName":
                                            var columnsView = new NamedRecordsView<string>(
                                                        "NamedValue",
                                                        columnNames,
                                                        x => new object[] { Guid.Empty, x },
                                                        x => x,
                                                        localize: false);
                                            autoComplete.View = columnsView;
                                            autoComplete.ViewComboBox = columnsView;

                                            this.autoCompleteDialogProvider.ChangeAutoCompleteDialog(autoComplete);
                                            break;
                                    }
                                    break;
                            }
                            return new ValueTask();
                        });
                    return new ValueTask();
                });

            if (form is null)
            {
                TessaDialog.ShowError("$CardTypes_MetadataEditor_ViewExtensionDialog_NotFound");
                return (null, null);
            }

            var section = cardmodel.Card.Sections["ManagerWorkplaceExtension"];
            section.Fields["CardId"] = settings.CardId.ToString();
            section.Fields["ActiveImageColumnName"] = settings.ActiveImageColumnName;
            section.Fields["CountColumnName"] = settings.CountColumnName;
            section.Fields["HoverImageColumnName"] = settings.HoverImageColumnName;
            section.Fields["InactiveImageColumnName"] = settings.InactiveImageColumnName;
            section.Fields["TileColumnName"] = settings.TileColumnName;


            section.FieldChanged += (o, e) =>
            {
                markedAsDirtyAction();
                switch (e.FieldName)
                {
                    case "CardId":
                        try
                        {
                            settings.CardId = Guid.Parse(e.FieldValue.ToString());
                        }
                        catch (FormatException)
                        {
                            throw new FormatException("$UI_Common_FormatException");
                        }
                        break;
                    case "ActiveImageColumnName":
                        settings.ActiveImageColumnName = e.FieldValue?.ToString();
                        break;
                    case "CountColumnName":
                        settings.CountColumnName = e.FieldValue?.ToString();
                        break;
                    case "HoverImageColumnName":
                        settings.HoverImageColumnName = e.FieldValue?.ToString();
                        break;
                    case "InactiveImageColumnName":
                        settings.InactiveImageColumnName = e.FieldValue?.ToString();
                        break;
                    case "TileColumnName":
                        settings.TileColumnName = e.FieldValue?.ToString();
                        break;
                }
            };

            void saveChangesAction()
            {
                var serializedSettings = ExtensionSettingsSerializationHelper.SerializeDictionary(settings);
                context.SaveSettings(serializedSettings);
            }
            return (form, saveChangesAction);
        }

        /// <summary>
        /// Вызывается для инициализации расширения после добавления его в контейнер
        /// </summary>
        /// <param name="context">
        /// Контекст инициализации
        /// </param>
        public override void Initialize(IExtensionConfigurationContext context)
        {
            ManagerWorkplaceSettings settingsModel = new ManagerWorkplaceSettings
            {
                CardId = new Guid(0x3db19fa0, 0x228a, 0x497f, 0x87, 0x3a, 0x02, 0x50, 0xbf, 0x0a, 0x4c, 0xcb), // 3db19fa0-228a-497f-873a-0250bf0a4ccb
                TileColumnName = "Caption",
                CountColumnName = "Count",
                ActiveImageColumnName = "ActiveImage",
                HoverImageColumnName = "ActiveImage",
                InactiveImageColumnName = "InactiveImage",
            };

            var serializedSettings = ExtensionSettingsSerializationHelper.SerializeDictionary(settingsModel);
            context.SaveSettings(serializedSettings);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Создает и возвращает компонент отображения данных представления.
        /// </summary>
        /// <param name="dataSourceMetadata">
        /// Метаданные источника данных.
        /// </param>
        /// <param name="container">
        /// Контейнер.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Компонент отображения данных.
        /// </returns>
        [CanBeNull]
        private async ValueTask<IWorkplaceViewComponent> GetViewComponentAsync(
            [NotNull] IDataSourceMetadata dataSourceMetadata,
            [NotNull] IUnityContainer container,
            CancellationToken cancellationToken = default)
        {
            Contract.Requires(container != null);
            Contract.Requires(dataSourceMetadata != null);

            return await WorkplaceViewComponentHelper.CreateWorkplaceViewComponentAsync(
                container.Resolve<ContentFactory>(),
                dataSourceMetadata,
                null,
                new List<RequestParameter>(),
                new Dictionary<string, IEnumerable<RequestParameter>>(StringComparer.OrdinalIgnoreCase),
                container.Resolve<IWorkplaceExtensionExecutorFactory>(),
                cancellationToken);
        }

        /// <summary>
        /// Загружает при наличии сохраненных настроек или создает модель для
        ///     настроек рабочего места руководителя по умолчанию.
        /// </summary>
        /// <param name="context">
        /// Контекст редактирования расширения
        /// </param>
        /// <param name="container">
        /// Контейнер приложения
        /// </param>
        /// <exception cref="ApplicationException">
        /// Ошибка загрузки или создания диаграммы
        /// </exception>
        /// <returns>
        /// Модель настроек рабочего места руководителя
        /// </returns>
        [NotNull]
        private ManagerWorkplaceSettings LoadOrCreate(
            [NotNull] IExtensionConfigurationContext context)
        {
            Contract.Requires(context != null);
            var settings = context.GetSettings();
            return settings == null
                ? new ManagerWorkplaceSettings()
                : (ManagerWorkplaceSettings) ExtensionSettingsSerializationHelper.DeserializeDictionary<ManagerWorkplaceSettings>(settings);
        }

        #endregion
    }
}
