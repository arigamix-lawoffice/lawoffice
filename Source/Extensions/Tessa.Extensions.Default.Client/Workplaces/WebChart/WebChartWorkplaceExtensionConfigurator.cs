using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette;
using Tessa.Extensions.Default.Shared.Workplaces;
using Tessa.Platform.Storage;
using Tessa.Properties.Resharper;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.Extensions;
using Tessa.Views;
using Tessa.Views.Metadata;
using Unity;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart
{
    /// <summary>
    /// Конфигуратор расширения <see cref="WebChartWorkplaceExtension"/>.
    /// </summary>
    public sealed class WebChartWorkplaceExtensionConfigurator : ExtensionSettingsConfiguratorBase
    {
        /// <summary>
        ///     The container.
        /// </summary>
        [NotNull]
        private readonly IUnityContainer container;

        private const string DescriptionLocalization = "$WebChartWorkplaceExtension_Description";
        private const string NameLocalization = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebChartWorkplaceExtensionConfigurator"/> class.
        /// </summary>
        /// <param name="container">
        /// Контейнер приложения
        /// </param>
        public WebChartWorkplaceExtensionConfigurator([NotNull] IUnityContainer container)
            : base(ViewExtensionConfiguratorType.Custom, NameLocalization, DescriptionLocalization)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <inheritdoc />
        public override async ValueTask<bool> ConfigureAsync(IExtensionConfigurationContext context, CancellationToken cancellationToken = default)
        {
            using IUnityContainer localContainer = this.container.CreateChildContainer();
            IDataSourceMetadata dataSourceMetadata = context.GetPropertyValue<IDataSourceMetadata>("Metadata");
            IWorkplaceViewComponent item = await GetViewComponentAsync(dataSourceMetadata, localContainer, cancellationToken);
            IEnumerable<string> columnNames = Enumerable.Empty<string>();
            if (item != null)
            {
                var metadata = await item.GetViewMetadataAsync(item, cancellationToken);
                if (metadata != null)
                {
                    columnNames = metadata.Columns.Select(c => c.Alias).ToArray();
                }
            }

            var settings = LoadOrCreate(context, localContainer);
            var settingsDictionaryBefore = new Dictionary<string, object>();
            settings.Serialize(settingsDictionaryBefore);
            var settingsViewModel = WebChartSettingsViewModel.Create(settings, columnNames);
            var settingsDialog = new WebChartSettingsDialog { DataContext = settingsViewModel };
            if (settingsDialog.ShowDialog() != true)
            {
                return false;
            }

            settings.DiagramType = settingsViewModel.DiagramType;
            settings.DiagramDirection = settingsViewModel.DiagramDirection;
            settings.LegendPosition = settingsViewModel.LegendPosition;
            settings.YColumn = settingsViewModel.YColumn?.Length == 0 ? null : settingsViewModel.YColumn;
            settings.LegendItemMinWidth = settingsViewModel.LegendItemMinWidth;
            settings.ColumnCount = settingsViewModel.ColumnCount;
            settings.LegendNotWrap = settingsViewModel.LegendNotWrap;
            settings.DoesntShowZeroValues = settingsViewModel.DoesntShowZeroValues;
            settings.SelectedColor = settingsViewModel.SelectedColor;
            settings.XColumn = settingsViewModel.XColumn?.Length == 0 ? null : settingsViewModel.XColumn;
            settings.CaptionColumn = settingsViewModel.CaptionColumn?.Length == 0 ? null : settingsViewModel.CaptionColumn;
            settings.Caption = settingsViewModel.Caption;
            settings.PaletteTypeId = settingsViewModel.SelectedPaletteTypeId.ToString();

            // TODO сериализация настроек в JSON после мержа с веткой json-workplace
            var dict = ExtensionSettingsSerializationHelper.SerializeDictionary(settings);
            if (!StorageHelper.Equals(settingsDictionaryBefore, dict))
            {
                context.SaveSettings(dict);
                context.ResetViewComponent();
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public override void Initialize(IExtensionConfigurationContext context)
        {
            WebChartWorkplaceSettings settingsModel = new WebChartWorkplaceSettings
            {
                XColumn = string.Empty,
                DiagramType = Enum.GetValues(typeof(WebChartDiagramType)).Cast<WebChartDiagramType>().First(),
                DiagramDirection = Enum.GetValues(typeof(WebChartDiagramDirection)).Cast<WebChartDiagramDirection>().First(),
                LegendPosition = WebChartLegendPosition.Bottom,
                YColumn = string.Empty,
                LegendItemMinWidth = null,
                ColumnCount = 1,
                LegendNotWrap = false,
                DoesntShowZeroValues = false,
                SelectedColor = string.Empty,
                CaptionColumn = string.Empty,
                Caption = string.Empty,
                PaletteTypeId = PaletteConstants.AccentPalette.TypeId.ToString()
            };

            var dict = ExtensionSettingsSerializationHelper.SerializeDictionary(settingsModel);
            context.SaveSettings(dict);
        }

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
        /// Задача на создание компонента отображения данных представления.
        /// </returns>
        [CanBeNull]
        private static async ValueTask<IWorkplaceViewComponent> GetViewComponentAsync(
            [NotNull] IDataSourceMetadata dataSourceMetadata,
            [NotNull] IUnityContainer container,
            CancellationToken cancellationToken = default)
        {
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
        private static WebChartWorkplaceSettings LoadOrCreate(
            [NotNull] IExtensionConfigurationContext context,
            [NotNull] IUnityContainer container)
        {
            var settings = context.GetSettings();
            return settings == null
                ? new WebChartWorkplaceSettings()
                : ExtensionSettingsSerializationHelper.DeserializeDictionary<WebChartWorkplaceSettings>(settings) as WebChartWorkplaceSettings;
        }
    }
}