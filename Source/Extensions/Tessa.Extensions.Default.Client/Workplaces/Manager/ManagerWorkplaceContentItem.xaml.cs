// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerWorkplaceContentItem.xaml.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Workplaces.Manager
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    using Tessa.Properties.Resharper;
    using Tessa.UI.Views;
    using Tessa.UI.Views.Content;
    using Tessa.Extensions.Default.Shared.Workplaces;

    #endregion

    /// <summary>
    ///     Interaction logic for ManagerWorkplaceContentItem.xaml
    /// </summary>
    public partial class ManagerWorkplaceContentItem : UserControl, IContentItem, INotifyPropertyChanged
    {
        [NotNull]
        private readonly IWorkplaceViewComponent component;

        [NotNull]
        private readonly ImageCache imageCache;

        /// <summary>
        ///     The settings.
        /// </summary>
        [NotNull]
        private readonly ManagerWorkplaceSettings settings;

        private ManagerWorkplaceTilesViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerWorkplaceContentItem"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="imageCache">
        /// The imageCache.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="placeAreas">
        /// The place areas.
        /// </param>
        /// <param name="dataTemplateFunc">
        /// The data template func.
        /// </param>
        /// <param name="ordering">
        /// The ordering.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ManagerWorkplaceContentItem(
            [NotNull] IWorkplaceViewComponent component,
            [NotNull] ImageCache imageCache,
            [NotNull] ManagerWorkplaceSettings settings,
            [CanBeNull] IEnumerable<IPlaceArea> placeAreas = null,
            [CanBeNull] Func<IPlaceArea, DataTemplate> dataTemplateFunc = null,
            int ordering = PlacementOrdering.BeforeAll)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }

            if (imageCache == null)
            {
                throw new ArgumentNullException("imageCache");
            }

            if (settings == null)
            {
                throw new ArgumentNullException("model");
            }

            this.InitializeComponent();
            this.PlaceAreas = placeAreas ?? ContentPlaceAreas.ContentPlaces;
            this.DataTemplateFunc = dataTemplateFunc;
            this.Order = ordering;
            this.component = component;
            this.imageCache = imageCache;
            this.settings = settings;
            this.viewModel = new ManagerWorkplaceTilesViewModel(this.component, this.settings, this.imageCache);
            this.DataContext = this.viewModel;
        }

        /// <summary>
        ///     The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets the data template func.
        /// </summary>
        public Func<IPlaceArea, DataTemplate> DataTemplateFunc { get; set; }

        /// <inheritdoc />
        public int Order { get; private set; }

        /// <inheritdoc />
        public IEnumerable<IPlaceArea> PlaceAreas { get; private set; }

        /// <inheritdoc />
        public DataTemplate GetTemplate(IPlaceArea area)
        {
            return this.DataTemplateFunc != null ? this.DataTemplateFunc(area) : null;
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}