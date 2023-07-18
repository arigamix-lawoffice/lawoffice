using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette
{
    public static class ChartColorPaletteHelper
    {
        public static string GetPaletteStr(this ChartColorPalette item) => item switch
        {
            ChartColorPalette.Accent => "7fc97fbeaed4fdc086ffff99386cb0f0027fbf5b17666666",
            ChartColorPalette.Dark2 => "1b9e77d95f027570b3e7298a66a61ee6ab02a6761d666666",
            ChartColorPalette.Paired => "a6cee31f78b4b2df8a33a02cfb9a99e31a1cfdbf6fff7f00cab2d66a3d9affff99b15928",
            ChartColorPalette.Pastel1 => "fbb4aeb3cde3ccebc5decbe4fed9a6ffffcce5d8bdfddaecf2f2f2",
            ChartColorPalette.Pastel2 => "b3e2cdfdcdaccbd5e8f4cae4e6f5c9fff2aef1e2cccccccc",
            ChartColorPalette.Set1 => "e41a1c377eb84daf4a984ea3ff7f00ffff33a65628f781bf999999",
            ChartColorPalette.Set2 => "66c2a5fc8d628da0cbe78ac3a6d854ffd92fe5c494b3b3b3",
            ChartColorPalette.Set3 => "8dd3c7ffffb3bebadafb807280b1d3fdb462b3de69fccde5d9d9d9bc80bdccebc5ffed6f",
            ChartColorPalette.Tableau10 => "4e79a7f28e2ce1575976b7b259a14fedc949af7aa1ff9da79c755fbab0ab",
            ChartColorPalette.Category10 => "1f77b4ff7f0e2ca02cd627289467bd8c564be377c27f7f7fbcbd2217becf",
            _ => string.Empty
        };

        public static List<Brush> GetBrushes(this ChartColorPalette item)
        {
            var colorStr = item.GetPaletteStr();

            var n = colorStr.Length / 6;
            var colors = new string[n]; var i = 0;
            while (i < n) colors[i] = colorStr.Substring(i++ * 6, 6);

            var brushes = new List<Brush>();

            foreach (var color in colors)
            {
                var nc = color.Length / 2;
                var colorsc = new string[nc]; var ic = 0;
                while (ic < nc) colorsc[ic] = color.Substring(ic++ * 2, 2);

                Brush brush = new SolidColorBrush(new Color
                {
                    R = colorsc.Length > 0 ? byte.Parse(colorsc[0], System.Globalization.NumberStyles.HexNumber) : default,
                    G = colorsc.Length > 1 ? byte.Parse(colorsc[1], System.Globalization.NumberStyles.HexNumber) : default,
                    B = colorsc.Length > 2 ? byte.Parse(colorsc[2], System.Globalization.NumberStyles.HexNumber) : default,
                    A = 0xff
                });
                brushes.Add(brush);
            }

            return brushes;
        }
    }
}
