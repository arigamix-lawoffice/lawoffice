// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordViewExtension.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Views
{
    using System;
    using System.Linq;

    using Tessa.UI;
    using Tessa.UI.Views;
    using Tessa.UI.Views.Content;

    /// <summary>
    ///     Расширение позволяющее отображать данные в модели-представлении в виде таблицы или записей
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="RecordViewExtensionConfigurator"/>
    /// </remarks>
    public sealed class RecordViewExtension : IWorkplaceViewComponentExtension
    {
        /// <summary>
        ///     The icon container factory.
        /// </summary>
        private readonly Func<IIconContainer> iconContainerFactory;

        /// <summary>
        /// The table view factory.
        /// </summary>
        private readonly TableViewFactory tableViewFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordViewExtension"/> class.
        ///     Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="iconContainerFactory">
        /// The icon Container Factory.
        /// </param>
        /// <param name="tableViewFactory">
        /// The table View Factory.
        /// </param>
        public RecordViewExtension(Func<IIconContainer> iconContainerFactory, TableViewFactory tableViewFactory)
        {
            this.iconContainerFactory = iconContainerFactory;
            this.tableViewFactory = tableViewFactory;
        }

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
            var contentType = source.Content.Any(c => c is ViewContentItem)
                                  ? ContentViewMode.TableView
                                  : ContentViewMode.RecordView;
            cloned.ContentFactories["ContentViewSwitcher"] =
                component =>
                new ContentSwitchView(component, this.tableViewFactory, this.iconContainerFactory, contentType);
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/>
        /// </summary>
        /// <param name="model">
        /// Инициализируемая модель
        /// </param>
        public void Initialize(IWorkplaceViewComponent model)
        {
            model.ContentFactories["ContentViewSwitcher"] =
                component => new ContentSwitchView(component, this.tableViewFactory, this.iconContainerFactory);
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
    }
}