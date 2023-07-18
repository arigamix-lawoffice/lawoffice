using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette;
using Tessa.Extensions.Default.Shared.Workplaces;
using Tessa.Platform.Collections;
using Tessa.Properties.Resharper;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart
{
    /// <summary>
    ///     Модель-представление для редактирования настроек веб-диаграмм
    /// </summary>
    public sealed class WebChartSettingsViewModel : ViewModel<EmptyModel>
    {
        #region Fields

        private WebChartDiagramType diagramType;

        private WebChartDiagramDirection diagramDirection;

        private WebChartLegendPosition legendPosition;

        private string caption;

        private string captionColumn = string.Empty;

        private PaletteSelectorViewModel paletteViewModel;

        private WebChartColorPickerViewModel selectedColorViewModel;

        private string xColumn;

        private string yColumn;

        private int? legendItemMinWidth;

        private int? columnCount;

        private bool legendNotWrap;

        private bool doesntShowZeroValues;

        private string selectedColor;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public WebChartSettingsViewModel([NotNull] IEnumerable<string> columnNames, Guid? paletteTypeId, string colorText)
        {
            if (columnNames is null)
            {
                throw new ArgumentNullException(nameof(columnNames));
            }

            this.ColumnNames = columnNames.ToObservableCollection();
            this.ColumnNames.Insert(0, string.Empty);

            this.DiagramTypes = Enum.GetValues(typeof(WebChartDiagramType)).Cast<WebChartDiagramType>().ToObservableCollection();
            this.DiagramDirections = Enum.GetValues(typeof(WebChartDiagramDirection)).Cast<WebChartDiagramDirection>().ToObservableCollection();
            this.LegendPositions = Enum.GetValues(typeof(WebChartLegendPosition)).Cast<WebChartLegendPosition>().ToObservableCollection();
            this.SelectedPaletteTypeId = paletteTypeId ?? PaletteConstants.AccentPalette.TypeId;
            this.selectedColor = colorText;
            this.PaletteViewModel = new PaletteSelectorViewModel(new Palettes(), paletteTypeId ?? PaletteConstants.AccentPalette.TypeId, (selectedPalette) => this.SelectedPaletteTypeId = selectedPalette);
            this.SelectedColorViewModel = new WebChartColorPickerViewModel(colorText, (selectedColor) => this.SelectedColor = selectedColor);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets Тип диаграммы
        /// </summary>
        public WebChartDiagramType DiagramType
        {
            get => this.diagramType;
            set
            {
                if (this.diagramType != value)
                {
                    this.diagramType = value;
                    this.OnPropertyChanged(nameof(this.DiagramType));
                }
            }
        }

        public WebChartDiagramDirection DiagramDirection
        {
            get => this.diagramDirection;
            set
            {
                if (this.diagramDirection != value)
                {
                    this.diagramDirection = value;
                    this.OnPropertyChanged(nameof(this.DiagramDirection));
                }
            }
        }

        public WebChartLegendPosition LegendPosition
        {
            get => this.legendPosition;
            set
            {
                if (this.legendPosition != value)
                {
                    this.legendPosition = value;
                    this.OnPropertyChanged(nameof(this.LegendPosition));
                }
            }
        }

        /// <summary>
        ///     Gets Список доступных для выборки столбцов
        /// </summary>
        [NotNull]
        public ObservableCollection<string> ColumnNames { get; }

        [NotNull]
        public ObservableCollection<WebChartDiagramType> DiagramTypes { get; }

        [NotNull]
        public ObservableCollection<WebChartDiagramDirection> DiagramDirections { get; }

        [NotNull]
        public ObservableCollection<WebChartLegendPosition> LegendPositions { get; }

        [CanBeNull]
        public string XColumn
        {
            get => this.xColumn;
            set
            {
                value ??= string.Empty;

                if (this.xColumn != value)
                {
                    this.xColumn = value;
                    this.OnPropertyChanged(nameof(XColumn));
                }
            }
        }

        [CanBeNull]
        public string YColumn
        {
            get => this.yColumn;
            set
            {
                value ??= string.Empty;

                if (this.yColumn != value)
                {
                    this.yColumn = value;
                    this.OnPropertyChanged(nameof(YColumn));
                }
            }
        }

        [CanBeNull]
        public int? LegendItemMinWidth
        {
            get => this.legendItemMinWidth;
            set
            {
                if (this.legendItemMinWidth != value)
                {
                    this.legendItemMinWidth = value;
                    this.OnPropertyChanged(nameof(this.LegendItemMinWidth));
                }
            }
        }

        [CanBeNull]
        public int? ColumnCount
        {
            get => this.columnCount;
            set
            {
                if (this.columnCount != value)
                {
                    this.columnCount = value;
                    this.OnPropertyChanged(nameof(this.ColumnCount));
                }
            }
        }

        public bool LegendNotWrap
        {
            get => this.legendNotWrap;
            set
            {
                if (this.legendNotWrap != value)
                {
                    this.legendNotWrap = value;
                    this.OnPropertyChanged(nameof(this.LegendNotWrap));
                }
            }
        }

        public bool DoesntShowZeroValues
        {
            get => this.doesntShowZeroValues;
            set
            {
                if (this.doesntShowZeroValues != value)
                {
                    this.doesntShowZeroValues = value;
                    this.OnPropertyChanged(nameof(this.DoesntShowZeroValues));
                }
            }
        }

        [CanBeNull]
        public string SelectedColor
        {
            get => this.selectedColor;
            set
            {
                if (this.selectedColor != value)
                {
                    this.selectedColor = value;
                    this.OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }

        [CanBeNull]
        public string Caption
        {
            get => this.caption;
            set
            {
                if (this.caption != value)
                {
                    this.caption = value;
                    this.OnPropertyChanged(nameof(Caption));
                }
            }
        }

        public string CaptionColumn
        {
            get => this.captionColumn;
            set
            {
                value ??= string.Empty;

                if (this.captionColumn != value)
                {
                    this.captionColumn = value;
                    this.OnPropertyChanged(nameof(this.CaptionColumn));
                }
            }
        }

        public PaletteSelectorViewModel PaletteViewModel
        {
            get => this.paletteViewModel;
            set
            {
                if (this.paletteViewModel != value)
                {
                    this.paletteViewModel = value;
                    this.OnPropertyChanged(nameof(PaletteViewModel));
                }
            }
        }

        public WebChartColorPickerViewModel SelectedColorViewModel
        {
            get => this.selectedColorViewModel;
            set
            {
                if (this.selectedColorViewModel != value)
                {
                    this.selectedColorViewModel = value;
                    this.OnPropertyChanged(nameof(this.SelectedColorViewModel));
                }
            }
        }

        public Guid SelectedPaletteTypeId { get; private set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Создает модель-представление и инициализирует ее свойства из настроек
        ///     задаваемых в параметре <paramref name="settings"/>
        /// </summary>
        /// <param name="settings">
        /// Настройки рабочего web-диаграмм
        /// </param>
        /// <param name="columnNames">
        /// Список доступных для выборки имен столбцов
        /// </param>
        /// <returns>
        /// Модель-представление редактирования настроек рабочего места руководителя
        /// </returns>
        [NotNull]
        public static WebChartSettingsViewModel Create([NotNull] WebChartWorkplaceSettings settings, [NotNull] IEnumerable<string> columnNames)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (columnNames is null)
            {
                throw new ArgumentNullException(nameof(columnNames));
            }

            if (!Guid.TryParse(settings.PaletteTypeId, out Guid paletteId))
            {
                paletteId = Guid.Empty;
            }

            return new WebChartSettingsViewModel(columnNames, paletteId == default ? (Guid?) null : paletteId, settings.SelectedColor)
            {
                DiagramType = settings.DiagramType,
                DiagramDirection = settings.DiagramDirection,
                LegendPosition = settings.LegendPosition,
                XColumn = settings.XColumn,
                YColumn = settings.YColumn,
                LegendItemMinWidth = settings.LegendItemMinWidth,
                ColumnCount = settings.ColumnCount,
                LegendNotWrap = settings.LegendNotWrap,
                DoesntShowZeroValues = settings.DoesntShowZeroValues,
                CaptionColumn = settings.CaptionColumn,
                Caption = settings.Caption
            };
        }

        #endregion
    }
}