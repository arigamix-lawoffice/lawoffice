using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Tessa.PreviewHandlers;
using Tessa.Properties.Resharper;
using Tessa.UI.Files;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    ///     Отображает предварительный просмотр файла.
    /// </summary>
    public partial class PreviewView :
        IContentItem,
        INotifyPropertyChanged,
        IPreviewPageExtractorProvider,
        IWeakEventListener
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="component">Компонент области с представлением. Не равен <c>null</c>.</param>
        /// <param name="previewHandlersPool">Пул обработчиков предварительного просмотра.</param>
        /// <param name="previewInfoCache">Кэш, предоставляющий информацию по предпросмотру файлов.</param>
        /// <param name="previewPageExtractorProvider">
        /// Поставщик, предоставляющий доступ к объекту, используемому для
        /// извлечения страниц для просмотра из многостраничных документов.
        /// </param>
        /// <param name="fileNameColumnName">Имя столбца представления, в котором содержится путь к просматриваемому файлу.</param>
        /// <param name="placeAreas">Область отображения для предпросмотра файлов.</param>
        /// <param name="dataTemplateFunc">
        /// Функция, возвращающая шаблон отображения для области предпросмотра,
        /// или <c>null</c>, если используется отображение по умолчанию.
        /// </param>
        public PreviewView(
            IWorkplaceViewComponent component,
            IPreviewHandlersSelectorPool previewHandlersPool = null,
            IFilePreviewInfoCache previewInfoCache = null,
            IPreviewPageExtractorProvider previewPageExtractorProvider = null,
            string fileNameColumnName = null,
            IEnumerable<IPlaceArea> placeAreas = null,
            Func<IPlaceArea, DataTemplate> dataTemplateFunc = null)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            this.InitializeComponent();

            this.previewHandlersPool = previewHandlersPool;
            this.previewInfoCache = previewInfoCache;
            this.previewPageExtractorProvider = previewPageExtractorProvider;
            this.component = component;
            this.dataTemplateFunc = dataTemplateFunc;
            this.PlaceAreas = placeAreas ?? ContentPlaceAreas.ContentPlaces;
            this.Order = 0;
            this.FileNameColumnName = fileNameColumnName;
            this.DataContext = this;

            PropertyChangedEventManager.AddHandler(this.component, this.OnDataChanged, nameof(IWorkplaceViewComponent.Data));
            SelectionStateChangedEventManager.AddListener(this.component.Selection, this);

            this.UpdatePreviewPath();
        }

        #endregion

        #region Fields

        private readonly IWorkplaceViewComponent component;

        private readonly Func<IPlaceArea, DataTemplate> dataTemplateFunc;

        private readonly IPreviewPageExtractorProvider previewPageExtractorProvider;

        private IPreviewHandlersSelectorPool previewHandlersPool;

        private IFilePreviewInfoCache previewInfoCache;

        private string previewPath;

        #endregion

        #region Private Methods

        private void OnDataChanged(object sender, PropertyChangedEventArgs e) => this.UpdatePreviewPath();

        private void UpdatePreviewPath()
        {
            IDictionary<string, object> row = this.component.SelectedRow ?? this.component.Data?.FirstOrDefault();

            if (row is null)
            {
                this.PreviewPath = string.Empty;
            }
            else if (!string.IsNullOrWhiteSpace(this.FileNameColumnName)
                     && row.TryGetValue(this.FileNameColumnName, out object filePath))
            {
                this.PreviewPath = filePath as string;
            }
            else
            {
                this.PreviewPath = row.Values.FirstOrDefault() as string;
            }
        }

        #endregion

        #region Protected Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
            this.PropertyChanged?.Invoke(this, e ?? throw new ArgumentNullException(nameof(e)));

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Properties

        /// <summary>
        /// Имя столбца представления, в котором содержится путь к просматриваемому файлу.
        /// </summary>
        public string FileNameColumnName { get; }

        /// <summary>
        /// Пул обработчиков предварительного просмотра.
        /// </summary>
        public IPreviewHandlersSelectorPool PreviewHandlersPool
        {
            get => this.previewHandlersPool;
            set
            {
                if (this.previewHandlersPool != value)
                {
                    this.previewHandlersPool = value;
                    this.OnPropertyChanged(nameof(this.PreviewHandlersPool));
                }
            }
        }

        /// <summary>
        /// Кэш, предоставляющий информацию по предпросмотру файлов.
        /// </summary>
        public IFilePreviewInfoCache PreviewInfoCache
        {
            get => this.previewInfoCache;
            set
            {
                if (this.previewInfoCache != value)
                {
                    this.previewInfoCache = value;
                    this.OnPropertyChanged(nameof(this.PreviewInfoCache));
                }
            }
        }

        /// <summary>
        /// Путь к отображаемому файлу.
        /// </summary>
        public string PreviewPath
        {
            get => this.previewPath;
            set
            {
                if (this.previewPath != value)
                {
                    this.previewPath = value;
                    this.OnPropertyChanged(nameof(this.PreviewPath));
                }
            }
        }

        #endregion

        #region IContentItem Members

        /// <inheritdoc />
        public int Order { get; }

        /// <inheritdoc />
        public IEnumerable<IPlaceArea> PlaceAreas { get; }

        /// <inheritdoc />
        public DataTemplate GetTemplate(IPlaceArea area) => this.dataTemplateFunc?.Invoke(area);

        #endregion

        #region INotifyPropertyChanged Members

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IPreviewPageExtractorProvider Members

        /// <inheritdoc />
        public IPreviewPageExtractor TryGetPageExtractor(string filePath) =>
            this.previewPageExtractorProvider?.TryGetPageExtractor(filePath);


        private IPreviewPageOptions defaultPageOptions;

        /// <inheritdoc />
        public IPreviewPageOptions PageOptions =>
            this.previewPageExtractorProvider?.PageOptions ?? (this.defaultPageOptions ??= new PreviewPageOptions());

        #endregion

        #region IWeakEventListener Members

        /// <inheritdoc />
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(SelectionStateChangedEventManager))
            {
                this.UpdatePreviewPath();
                return true;
            }

            return false;
        }

        #endregion
    }
}