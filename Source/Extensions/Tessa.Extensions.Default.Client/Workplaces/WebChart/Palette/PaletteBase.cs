using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette
{
    /// <summary>
    /// The palette base.
    /// </summary>
    public class PaletteBase : ITypeIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteBase"/> class.
        /// Called when instance created for ChartColorModel with single arguments
        /// </summary>
        /// <param name="typeId">
        /// Идентификатор типа
        /// </param>
        /// <param name="name">
        /// Название
        /// </param>
        /// <param name="palette">
        /// Вид палитры
        /// </param>
        public PaletteBase(Guid typeId, [NotNull] string name, ChartColorPalette palette)

        {
            this.Palette = palette;
            this.TypeId = typeId;
            this.Name = name;
            this.Colors = palette.GetBrushes();
        }

        /// <summary>
        /// Gets or sets Палетка
        /// </summary>
        [NotNull]
        public ChartColorPalette Palette { get; protected set; }

        /// <summary>
        /// Gets or sets Список цветов палитры
        /// </summary>
        [NotNull]
        public List<Brush> Colors { get; protected set; }

        /// <summary>
        /// Gets or sets Название палитры
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the preview colors.
        /// </summary>
        public IEnumerable<Brush> PreviewColors => this.Colors.Take(6);

        /// <summary>
        ///     Gets Идентификатор типа
        /// </summary>
        public Guid TypeId { get; }
    }
}