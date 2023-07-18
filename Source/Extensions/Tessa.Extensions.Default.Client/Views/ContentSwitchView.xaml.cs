using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    ///     Представление с кнопками переключения режима <see cref="ContentViewMode" /> отображения данных модели
    ///     представления <see cref="IWorkplaceViewComponent" />
    /// </summary>
    public partial class ContentSwitchView : IContentItem, INotifyPropertyChanged
    {
        /// <summary>
        /// The icon container factory.
        /// </summary>
        private readonly Func<IIconContainer> iconContainerFactory;

        /// <summary>
        ///     The model.
        /// </summary>
        private readonly IWorkplaceViewComponent model;

        private readonly TableViewFactory tableViewFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSwitchView"/> class.
        /// </summary>
        /// <param name="model">
        /// Модель представления рабочего места
        /// </param>
        /// <param name="tableViewFactory"></param>
        /// <param name="iconContainerFactory">
        /// Фабрика получения контейнера значков приложения
        /// </param>
        /// <param name="contentViewMode">
        /// Режим отображения контента
        /// </param>
        /// <param name="placeAreas">
        /// Области отображения данного представления
        /// </param>
        /// <param name="ordering">
        /// Порядок вывода
        /// </param>
        public ContentSwitchView(
            IWorkplaceViewComponent model,
            TableViewFactory tableViewFactory,
            Func<IIconContainer> iconContainerFactory,
            ContentViewMode contentViewMode = ContentViewMode.TableView,
            IEnumerable<IPlaceArea> placeAreas = null,
            int ordering = PlacementOrdering.BeforeAll)
        {
            this.InitializeComponent();
            this.model = model;
            this.tableViewFactory = tableViewFactory;
            this.iconContainerFactory = iconContainerFactory;
            this.ContentViewMode = contentViewMode;
            this.Order = ordering;
            this.PlaceAreas = placeAreas ?? ContentPlaceAreas.ToolbarPlaces;
            this.DataContext = this;
        }

        /// <summary>
        ///     The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets Режим отображения контента
        /// </summary>
        public ContentViewMode ContentViewMode { get; private set; }

        public ValueTask SetContentViewModeAsync(ContentViewMode value)
        {
            if (this.ContentViewMode == value)
            {
                return new ValueTask();
            }

            this.ContentViewMode = value;
            this.OnPropertyChanged("ContentViewMode");
            this.OnPropertyChanged("RecordMode");
            this.OnPropertyChanged("TableMode");

            return this.UpdateContentAsync();
        }

        /// <summary>
        ///     Gets Порядок вывода элемента
        /// </summary>
        public int Order { get; }

        /// <summary>
        ///     Gets возвращает область отображения
        /// </summary>
        public IEnumerable<IPlaceArea> PlaceAreas { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether Признак отображения строк
        /// </summary>
        public bool RecordMode
        {
            get => this.ContentViewMode == ContentViewMode.RecordView;
            set
            {
                var _ = this.SetContentViewModeAsync(!value ? ContentViewMode.TableView : ContentViewMode.RecordView);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether Признак отображения таблицы
        /// </summary>
        public bool TableMode
        {
            get => this.ContentViewMode == ContentViewMode.TableView;
            set
            {
                var _ = this.SetContentViewModeAsync(value ? ContentViewMode.TableView : ContentViewMode.RecordView);
            }
        }

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

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// The delete content item.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        private async ValueTask DeleteContentItemAsync(Type type)
        {
            var contentItem = this.model.Content.FirstOrDefault(c => c.GetType() == type);
            if (contentItem != null)
            {
                this.model.Content.Remove(contentItem);

                switch (contentItem)
                {
                    case IAsyncDisposable asyncDisposable:
                        await asyncDisposable.DisposeAsync();
                        break;

                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                }
            }
        }

        /// <summary>
        ///     The update content.
        /// </summary>
        private async ValueTask UpdateContentAsync()
        {
            if (this.TableMode)
            {
                await this.DeleteContentItemAsync(typeof(RecordView));
                this.model.ContentFactories[StandardViewComponentContentItemFactory.Table] =
                    c => this.tableViewFactory(c, ContentPlaceAreas.ContentPlaces, null, PlacementOrdering.BeforeAll); //new ViewContentItem(c, this.iconContainerFactory);
                this.model.Content.Add(this.tableViewFactory(this.model, ContentPlaceAreas.ContentPlaces, null, PlacementOrdering.BeforeAll));
                return;
            }

            if (this.RecordMode)
            {
                await this.DeleteContentItemAsync(typeof(TableView));
                this.model.ContentFactories[StandardViewComponentContentItemFactory.Table] = c => new RecordView(c);
                this.model.Content.Add(new RecordView(this.model));
            }
        }
    }
}