using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tessa.Platform.Collections;
using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.Filtering;
using Tessa.UI.Views.Parameters;
using Tessa.UI.Views.Workplaces.Tree;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Parser;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Interaction logic for CustomNavigationView.xaml
    /// </summary>
    public partial class CustomNavigationView : INotifyPropertyChanged, IContentItem
    {
        #region Fields

        private ICollection<RequestParameter> parameters;

        private readonly IWorkplaceViewComponent component;

        private readonly FilterViewModelFactory filterFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomNavigationView"/> class.
        /// </summary>
        /// <param name="component">
        /// Обрабатываемый компонент
        /// </param>
        /// <param name="filterFactory">Фабрика создания элементов фильтрации</param>
        public CustomNavigationView([NotNull] IWorkplaceViewComponent component, [NotNull] FilterViewModelFactory filterFactory)
        {
            this.InitializeComponent();
            this.DataContext = this;

            this.component = component;
            this.filterFactory = filterFactory;

            this.ApplyFilterCommand = new DelegateCommand(this.ApplyFilterExecute, ApplyFilterCanExecute);
            this.ClearFilterCommand = new DelegateCommand(this.ClearFilterExecuteAsync, this.ClearFilterCanExecute);

            this.parameters = GetParametersForFiltering(component.Parameters);
            this.FilterModel = this.filterFactory(this.parameters);
            this.FilterModel.Accepted += this.AcceptFilter;

            CollectionChangedEventManager.AddHandler(component.Parameters, this.ParametersChanged);
        }

        #endregion

        #region Private Methods

        private void ParametersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.parameters = GetParametersForFiltering(component.Parameters);
            this.FilterModel = this.filterFactory(this.parameters);
            this.FilterModel.Accepted += this.AcceptFilter;
            this.OnPropertyChanged(nameof(FilterModel));
        }

        /// <summary>
        /// Возвращает список параметров для отображения в окне фильтрации
        /// </summary>
        /// <param name="parameters">
        /// Параметры
        /// </param>
        /// <returns>
        /// Список параметров
        /// </returns>
        private static ICollection<RequestParameter> GetParametersForFiltering(IViewParameters parameters)
        {
            var list = new List<RequestParameter>();
            foreach (IViewParameterMetadata metadata in parameters.Metadata)
            {
                RequestParameter param = parameters
                    .FirstOrDefault(x => ParserNames.IsEquals(metadata.Alias, x.Name))
                    ?.Clone()
                    ?? new RequestParameterBuilder()
                        .WithMetadata(metadata)
                        .CreateDefaultValues()
                        .AsRequestParameter();

                list.Add(param);
            }

            return list;
        }

        private void AcceptFilter(object sender, EventArgs e)
        {
            var appliedParameters = this.FilterModel.GetAppliedParameters() ?? new List<RequestParameter>();
            using (this.component.Parameters.SuspendChangesNotification())
            {
                this.component.Parameters.Clear();
                this.component.Parameters.AddRange(appliedParameters);
            }
        }

        private void ApplyFilterExecute(object obj)
        {
            var appliedParameters = this.FilterModel.GetAppliedParameters() ?? new List<RequestParameter>();
            using (this.component.Parameters.SuspendChangesNotification())
            {
                this.component.Parameters.Clear();
                this.component.Parameters.AddRange(appliedParameters);
            }
        }

        private bool ClearFilterCanExecute(object arg) => this.component.CanClearFilter(this.component.Parameters);

        private async void ClearFilterExecuteAsync(object obj) => await this.component.ClearFilterAsync(this.component.Parameters);

        private static bool ApplyFilterCanExecute(object arg) => true;

        #endregion

        #region Properties

        public FilterViewModel FilterModel { get; private set; }

        public ICommand ApplyFilterCommand { get; }

        public ICommand ClearFilterCommand { get; }

        #endregion

        #region IContentItem Members

        /// <summary>
        ///     Gets возвращает область отображения
        /// </summary>
        public IEnumerable<IPlaceArea> PlaceAreas { get; } = ContentPlaceAreas.HeaderContentPlaceAreas;

        /// <summary>
        ///     Gets Порядок вывода элемента
        /// </summary>
        public int Order => 1;

        /// <summary>
        /// Возвращает шаблон данных для указанной области
        /// </summary>
        /// <param name="area">
        /// Область расположения
        /// </param>
        /// <returns>
        /// Шаблон данных или null
        /// </returns>
        public DataTemplate GetTemplate(IPlaceArea area) => null;

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}