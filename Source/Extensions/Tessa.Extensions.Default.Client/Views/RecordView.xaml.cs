using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Tessa.Extensions.Default.Client.Views
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    using Tessa.UI.Views;
    using Tessa.UI.Views.Content;

    /// <summary>
    /// Interaction logic for RecordView.xaml
    /// </summary>
    public partial class RecordView : UserControl, IContentItem
    {
        private readonly IWorkplaceViewComponent model;

        public RecordView(IWorkplaceViewComponent model, IEnumerable<IPlaceArea> areas = null, int ordering = PlacementOrdering.AfterAll)
        {
            this.InitializeComponent();
            this.PlaceAreas = areas ?? ContentPlaceAreas.ContentPlaces;
            this.Order = ordering;
            this.model = model;
            this.Records = new ObservableCollection<RecordItem>();
            this.UpdateRecords(model);
            this.DataContext = this;

            PropertyChangedEventManager.AddHandler(this.model, this.ModelPropertyChanged, "CurrentRow");
        }

        private void UpdateRecords(IWorkplaceViewComponent model)
        {
            this.Records.Clear();
            if (model.Selection.SelectedRow == null)
            {
                return;
            }

            if (model.SelectedRow != null)
            {
                foreach (var column in model.SelectedRow)
                {
                    this.Records.Add(new RecordItem
                    {
                        ColumnName = column.Key,
                        Value = column.Value != null ? column.Value.ToString() : "(empty)"
                    });
                }
            }
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentRow")
            {
                this.UpdateRecords(this.model);
            }
        }

        /// <summary>
        ///     Gets Порядок вывода элемента
        /// </summary>
        public int Order { get; }

        /// <summary>
        ///     Gets возвращает область отображения
        /// </summary>
        public IEnumerable<IPlaceArea> PlaceAreas { get; }

        public ObservableCollection<RecordItem> Records { get; }

        /// <summary>
        /// Возвращает шаблон данных для указанной области
        /// </summary>
        /// <param name="area">
        /// Область расположения
        /// </param>
        /// <returns>
        /// Шаблон данных или null
        /// </returns>
        public DataTemplate GetTemplate(IPlaceArea area)
        {
            return null;
        }
    }

    public sealed class RecordItem
    {
        public string ColumnName { get; set; }

        public string Value { get; set; }
    }
}
