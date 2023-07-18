using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workplaces
{
    /// <summary>
    /// Настройки вывода диаграммы вместо представления в лёгком клиенте.
    /// </summary>
    [Serializable]
    public class WebChartWorkplaceSettings :
        StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Тип диаграммы.
        /// </summary>
        public WebChartDiagramType DiagramType { get; set; }

        /// <summary>
        /// Расположение диаграммы.
        /// </summary>
        public WebChartDiagramDirection DiagramDirection { get; set; }

        /// <summary>
        /// Положение подписи диаграммы.
        /// </summary>
        public WebChartLegendPosition LegendPosition { get; set; }

        /// <summary>
        /// Имя колонки с названием диаграммы.
        /// </summary>
        public string CaptionColumn { get; set; }

        /// <summary>
        /// Название диаграммы.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Имя колонки, содержащей значения по оси X.
        /// </summary>
        public string XColumn { get; set; }

        /// <summary>
        /// Имя колонки, содержащей значения по оси Y.
        /// </summary>
        public string YColumn { get; set; }

        /// <summary>
        /// Минимальная ширина элемента в подписи к графику.
        /// </summary>
        public int? LegendItemMinWidth { get; set; }

        /// <summary>
        /// Флаг запрещающий перенос слов на новую строку, если фраза не умещается в одну строку.
        /// </summary>
        public bool LegendNotWrap { get; set; }

        /// <summary>
        /// Не показывать нулевые значения.
        /// </summary>
        public bool DoesntShowZeroValues { get; set; }

        /// <summary>
        /// Цвет выбора фрагмента диаграммы.
        /// </summary>
        public string SelectedColor { get; set; }

        /// <summary>
        /// Идентификатор палитры цветов.
        /// </summary>
        public string PaletteTypeId { get; set; }

        /// <summary>
        /// Количество столбцов диаграмм в строке.
        /// </summary>
        public int? ColumnCount { get; set; }

        #endregion

        #region IStorageSerializable Members

        /// <inheritdoc/>
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            base.SerializeCore(storage);
        
            storage[nameof(this.DiagramType)] = this.DiagramType.ToString();
            storage[nameof(this.DiagramDirection)] = this.DiagramDirection.ToString();
            storage[nameof(this.LegendPosition)] = this.LegendPosition.ToString();
            storage[nameof(this.XColumn)] = this.XColumn;
            storage[nameof(this.YColumn)] = this.YColumn;
            storage[nameof(this.LegendItemMinWidth)] = this.LegendItemMinWidth;
            storage[nameof(this.ColumnCount)] = this.ColumnCount;
            storage[nameof(this.LegendNotWrap)] = this.LegendNotWrap;
            storage[nameof(this.DoesntShowZeroValues)] = this.DoesntShowZeroValues;
            storage[nameof(this.SelectedColor)] = this.SelectedColor;
            storage[nameof(this.CaptionColumn)] = this.CaptionColumn;
            storage[nameof(this.Caption)] = this.Caption;
            storage[nameof(this.PaletteTypeId)] = this.PaletteTypeId;
        }

        /// <inheritdoc/>
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            base.DeserializeCore(storage);
        
            this.DiagramType = storage.GetSerializedEnum(nameof(this.DiagramType), WebChartDiagramType.Bar);
            this.DiagramDirection = storage.GetSerializedEnum(nameof(this.DiagramDirection), WebChartDiagramDirection.Horizontal);
            this.LegendPosition = storage.GetSerializedEnum(nameof(this.LegendPosition), WebChartLegendPosition.Bottom);
            this.XColumn = storage.TryGet<string>(nameof(this.XColumn));
            this.YColumn = storage.TryGet<string>(nameof(this.YColumn));
            this.LegendItemMinWidth = storage.TryGet<int?>(nameof(this.LegendItemMinWidth));
            this.ColumnCount = storage.TryGet<int?>(nameof(this.ColumnCount));
            this.LegendNotWrap = storage.TryGet<bool>(nameof(this.LegendNotWrap));
            this.DoesntShowZeroValues = storage.TryGet<bool>(nameof(this.DoesntShowZeroValues));
            this.SelectedColor = storage.TryGet<string>(nameof(this.SelectedColor));
            this.CaptionColumn = storage.TryGet<string>(nameof(this.CaptionColumn));
            this.Caption = storage.TryGet<string>(nameof(this.Caption));
            this.PaletteTypeId = storage.TryGet<string>(nameof(this.PaletteTypeId));
        }

        #endregion
    }
}
