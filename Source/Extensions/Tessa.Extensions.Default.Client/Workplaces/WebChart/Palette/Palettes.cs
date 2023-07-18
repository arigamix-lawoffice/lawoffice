using System;
using System.Collections.Generic;
using System.Text;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette
{
    public class Palettes : Registry<PaletteBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Palettes"/> class. Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Palettes()
            : base(
                new[]
                    {
                        new PaletteBase(
                            PaletteConstants.AccentPalette.TypeId,
                            PaletteConstants.AccentPalette.Name,
                            ChartColorPalette.Accent),
                        new PaletteBase(
                            PaletteConstants.Dark2Palette.TypeId,
                            PaletteConstants.Dark2Palette.Name,
                            ChartColorPalette.Dark2),
                        new PaletteBase(
                            PaletteConstants.PairedPalette.TypeId,
                            PaletteConstants.PairedPalette.Name,
                            ChartColorPalette.Paired),
                        new PaletteBase(
                            PaletteConstants.Pastel1Palette.TypeId,
                            PaletteConstants.Pastel1Palette.Name,
                            ChartColorPalette.Pastel1),
                        new PaletteBase(
                            PaletteConstants.Pastel2Palette.TypeId,
                            PaletteConstants.Pastel2Palette.Name,
                            ChartColorPalette.Pastel2),
                        new PaletteBase(
                            PaletteConstants.Set1Palette.TypeId,
                            PaletteConstants.Set1Palette.Name,
                            ChartColorPalette.Set1),
                        new PaletteBase(
                            PaletteConstants.Set2Palette.TypeId,
                            PaletteConstants.Set2Palette.Name,
                            ChartColorPalette.Set2),
                        new PaletteBase(
                            PaletteConstants.Set3Palette.TypeId,
                            PaletteConstants.Set3Palette.Name,
                            ChartColorPalette.Set3),
                        new PaletteBase(
                            PaletteConstants.Tableau10Palette.TypeId,
                            PaletteConstants.Tableau10Palette.Name,
                            ChartColorPalette.Tableau10),
                        new PaletteBase(
                            PaletteConstants.Category10Palette.TypeId,
                            PaletteConstants.Category10Palette.Name,
                            ChartColorPalette.Category10),
                    })
        { }
    }
}
