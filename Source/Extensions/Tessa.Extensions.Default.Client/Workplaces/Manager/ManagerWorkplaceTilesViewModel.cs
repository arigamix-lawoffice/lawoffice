// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerWorkplaceTilesViewModel.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Workplaces.Manager
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Tessa.Properties.Resharper;
    using Tessa.UI;
    using Tessa.UI.Controls;
    using Tessa.UI.Views;
    using Tessa.Extensions.Default.Shared.Workplaces;

    #endregion

    /// <summary>
    ///     Модель представление кнопок
    /// </summary>
    public sealed class ManagerWorkplaceTilesViewModel : ViewModel<EmptyModel>
    {
        /// <summary>
        ///     The component.
        /// </summary>
        [NotNull]
        private readonly IWorkplaceViewComponent component;

        /// <summary>
        ///     The settings.
        /// </summary>
        private readonly ManagerWorkplaceSettings settings;

        [NotNull]
        private readonly ImageCache imageCache;

        /// <summary>
        ///     The models.
        /// </summary>
        private readonly List<IDictionary<string, object>> models;

        /// <summary>
        ///     The selected items.
        /// </summary>
        private List<IDictionary<string, object>> selectedItems;

        /// <summary>
        ///     The selector.
        /// </summary>
        private readonly SelectorViewModel<IDictionary<string, object>, ManagerWorkplaceTileViewModel> selector;

        /// <inheritdoc />
        public ManagerWorkplaceTilesViewModel(
            [NotNull] IWorkplaceViewComponent component,
            [NotNull] ManagerWorkplaceSettings settings,
            [NotNull] ImageCache imageCache)
            : base()
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (imageCache == null)
            {
                throw new ArgumentNullException("imageCache");
            }

            this.component = component;
            this.settings = settings;
            this.imageCache = imageCache;
            this.models = new List<IDictionary<string, object>>();
            this.selectedItems = new List<IDictionary<string, object>>();
            this.selector = new SelectorViewModel<IDictionary<string, object>, ManagerWorkplaceTileViewModel>(
                this.models,
                this.selectedItems,
                ViewModelScope.Global);
            PropertyChangedEventManager.AddHandler(this.component, this.OnRefreshAsync, "Data");
            PropertyChangedEventManager.AddHandler(this.selector, this.SelectedItemChanged, "SelectedItem");
            this.OnRefreshAsync(this.component, new PropertyChangedEventArgs("Data"));
        }

        /// <summary>
        ///     Gets the tiles.
        /// </summary>
        [NotNull]
        public SelectorViewModel<IDictionary<string, object>, ManagerWorkplaceTileViewModel> Selector
        {
            get
            {
                return this.selector;
            }
        }

        /// <summary>
        /// Вызывается при изменении данных в таблице
        /// </summary>
        /// <param name="sender">
        /// Отправитель события
        /// </param>
        /// <param name="e">
        /// Параметры события
        /// </param>
        private async void OnRefreshAsync(object sender, PropertyChangedEventArgs e)
        {
            var data = this.component.Data;
            var metadata = await this.component.GetViewMetadataAsync(this.component);
            this.models.Clear();
            this.selector.Items.Clear();
            if (data == null)
            {
                return;
            }

            // this.models.AddRange(data);
            foreach (var row in data)
            {
                var tile = await ManagerWorkplaceTileViewModel.Create(row, metadata, this.settings, this.imageCache);
                this.selector.Items.Add(tile);

                tile.IsSelected = ReferenceEquals(this.component.Selection.SelectedRow, tile.Model);
            }
        }

        /// <summary>
        /// The selected item changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.component.Selection.SetSelection(
                this.selector.SelectedItem != null ? this.selector.SelectedItem.Model : null,
                null);
        }
    }
}