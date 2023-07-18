using System;
using System.Linq;
using Tessa.Properties.Resharper;
using Tessa.UI.Views;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// The custom navigation view extension.
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="CustomNavigationViewExtensionConfigurator"/>
    /// </remarks>
    internal sealed class CustomNavigationViewExtension : IWorkplaceViewComponentExtension
    {
        #region Fields

        private readonly FilterViewModelFactory filterViewModelFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomNavigationViewExtension"/> class.
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="filterViewModelFactory">
        /// Фабрика создания элементов фильтрации
        /// </param>
        public CustomNavigationViewExtension([NotNull] FilterViewModelFactory filterViewModelFactory) =>
            this.filterViewModelFactory = filterViewModelFactory ?? throw new ArgumentNullException(nameof(filterViewModelFactory));

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Вызывается при клонировании модели <paramref name="source"/> в контексте <paramref name="context"/>
        /// </summary>
        /// <param name="source">
        /// Исходная модель
        /// </param>
        /// <param name="cloned">
        /// Клориованная модель
        /// </param>
        /// <param name="context">
        /// Контекст клонирования
        /// </param>
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
            cloned.ContentFactories["CustomView"] = component => new CustomNavigationView(component, this.filterViewModelFactory);

            var content = cloned.Content.OfType<CustomNavigationView>().FirstOrDefault();
            if (content != null)
            {
                cloned.Content.Remove(content);
                cloned.Content.Add(new CustomNavigationView(cloned, this.filterViewModelFactory));
            }
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/>
        /// </summary>
        /// <param name="model">
        /// Инициализируемая модель
        /// </param>
        public void Initialize(IWorkplaceViewComponent model)
        {
            model.ContentFactories.Remove(StandardViewComponentContentItemFactory.Paging);
            model.ContentFactories["CustomView"] = component => new CustomNavigationView(component, this.filterViewModelFactory);
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/> перед отображени в UI
        /// </summary>
        /// <param name="model">
        /// Модель
        /// </param>
        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion
    }
}
