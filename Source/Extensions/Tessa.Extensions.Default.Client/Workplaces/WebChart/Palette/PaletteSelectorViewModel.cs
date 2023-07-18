using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Properties.Resharper;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette
{
    public class PaletteSelectorViewModel : ViewModel<EmptyModel>
    {
        #region Fields

        private readonly Palettes palettes;

        private readonly Action<Guid> paletteSetter;

        private PaletteBase selectedPalette;

        #endregion

        #region Constructors

        public PaletteSelectorViewModel(Palettes palettes, Guid selectedPalette, Action<Guid> paletteSetter)
        {
            this.palettes = palettes;
            this.paletteSetter = paletteSetter;
            this.selectedPalette = this.Palettes.FirstOrDefault(x => x.TypeId == selectedPalette)
                ?? this.Palettes.FirstOrDefault();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets Список палтир
        /// </summary>
        [NotNull]
        public IEnumerable<PaletteBase> Palettes => this.palettes.Items;

        /// <summary>
        /// Gets or sets Текущий выбранный вид палитры
        /// </summary>
        public PaletteBase SelectedPalette
        {
            get => this.selectedPalette;
            set
            {
                if (!ReferenceEquals(this.selectedPalette, value))
                {
                    this.selectedPalette = value;
                    this.paletteSetter(this.selectedPalette.TypeId);

                    this.OnPropertyChanged(nameof(this.SelectedPalette));
                }
            }
        }

        #endregion
    }
}