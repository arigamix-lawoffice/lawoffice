#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.MessagingServices.Commands;
using Tessa.UI.Views.MessagingServices.Queries;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Расширение, переопределяющее диалог фильтрации представления.
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="FilterViewDialogOverrideWorkplaceComponentExtensionConfigurator"/>.
    /// </remarks>
    public class FilterViewDialogOverrideWorkplaceComponentExtension :
        IWorkplaceViewComponentExtension
    {
        #region Fields

        private readonly ISession session;

        private readonly IAdvancedFilterViewDialogManager advancedFilterViewDialogManager;

        private readonly IFilterViewDialogDescriptorRegistry filterViewDialogDescriptorRegistry;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FilterViewDialogOverrideWorkplaceComponentExtension"/>.
        /// </summary>
        /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
        /// <param name="advancedFilterViewDialogManager"><inheritdoc cref="IAdvancedFilterViewDialogManager" path="/summary"/></param>
        /// <param name="filterViewDialogDescriptorRegistry"><inheritdoc cref="IFilterViewDialogDescriptorRegistry" path="/summary"/></param>
        public FilterViewDialogOverrideWorkplaceComponentExtension(
            ISession session,
            IAdvancedFilterViewDialogManager advancedFilterViewDialogManager,
            IFilterViewDialogDescriptorRegistry filterViewDialogDescriptorRegistry)
        {
            this.session = NotNullOrThrow(session);
            this.advancedFilterViewDialogManager = NotNullOrThrow(advancedFilterViewDialogManager);
            this.filterViewDialogDescriptorRegistry = NotNullOrThrow(filterViewDialogDescriptorRegistry);
        }

        #endregion

        #region IWorkplaceViewComponentExtension Members

        /// <inheritdoc/>
        public void Initialize(
            IWorkplaceViewComponent model)
        {
            // В TessaAdmin специально созданный диалог не будет работать из-за отсутствия рабочей реализации IUIHost (используется FakeUIHost).
            // Для тестирования представление в TessaAdmin в режиме "Просмотр", не переопределяем делегат
            // отвечающий за создание кнопки отображения диалога настройки параметров фильтрации.
            if (this.session.ApplicationID == ApplicationIdentifiers.TessaAdmin)
            {
                return;
            }

            if (this.filterViewDialogDescriptorRegistry.TryGet(model.Id) is not { } descriptor)
            {
                return;
            }

            // Замена стандартного диалога настройки параметров фильтрации, вызываемого при нажатии на соответствующую кнопку расположенную над представлением, на заданный.
            model.ContentFactories[StandardViewComponentContentItemFactory.FilterButton] = c =>
                this.CreateFilterButtonContent(
                    descriptor,
                    c);

            // Замена стандартного диалога настройки параметров фильтрации, вызываемого при нажатии на соответствующую кнопку расположенную над представлением при применённом фильтре (отображается при наведении указателя на область текущих параметров фильтрации), на заданный.
            model.ContentFactories[StandardViewComponentContentItemFactory.FilterTextView] = c =>
                this.CreateFilterTextViewContent(
                    descriptor,
                    c);
        }

        /// <inheritdoc/>
        public void Initialized(
            IWorkplaceViewComponent model)
        {
        }

        /// <inheritdoc/>
        public void Clone(
            IWorkplaceViewComponent source,
            IWorkplaceViewComponent cloned,
            ICloneableContext context)
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Создает и инициализирует экземпляр <see cref="FilterButtonContent" />.
        /// </summary>
        /// <param name="descriptor"><inheritdoc cref="FilterViewDialogDescriptor" path="/summary"/></param>
        /// <param name="component"><inheritdoc cref="IWorkplaceViewComponent" path="/summary"/></param>
        /// <param name="placeAreas">Список областей вывода элемента.</param>
        /// <param name="dataTemplateFunc">Обработка выдачи шаблона в зависимости от области расположения <paramref name="placeAreas" /> элемента.</param>
        /// <param name="ordering">Порядок вывода элемента.</param>
        /// <returns>Экземпляр кнопки фильтрации.</returns>
        private FilterButtonContent CreateFilterButtonContent(
            FilterViewDialogDescriptor descriptor,
            IWorkplaceViewComponent component,
            IEnumerable<IPlaceArea>? placeAreas = null,
            Func<IPlaceArea, DataTemplate>? dataTemplateFunc = null,
            int ordering = PlacementOrdering.BeforeAll)
        {
            var parameters = component.Parameters;

            var command = new DelegateCommand(
                async _ => await this.advancedFilterViewDialogManager.OpenAsync(
                    descriptor,
                    parameters,
                    CancellationToken.None),
                _ => component.SubmitQuery(
                    new CanFilterQuery(
                        component.Parameters.Metadata)));

            return new FilterButtonContent(
                command,
                parameters,
                placeAreas,
                dataTemplateFunc,
                ordering);
        }

        /// <summary>
        /// Создает и инициализирует экземпляр <see cref="FilterTextViewContent" />.
        /// </summary>
        /// <param name="descriptor"><inheritdoc cref="FilterViewDialogDescriptor" path="/summary"/></param>
        /// <param name="component"><inheritdoc cref="IWorkplaceViewComponent" path="/summary"/></param>
        /// <param name="parametersConverter"><inheritdoc cref="IParameterContentConverter" path="/summary"/></param>
        /// <param name="placeAreas">Список областей вывода элемента.</param>
        /// <param name="dataTemplateFunc">Обработка выдачи шаблона в зависимости от области расположения <paramref name="placeAreas" /> элемента.</param>
        /// <param name="ordering">Порядок вывода элемента.</param>
        /// <returns>Экземпляр кнопки фильтрации.</returns>
        private FilterTextViewContent CreateFilterTextViewContent(
            FilterViewDialogDescriptor descriptor,
            IWorkplaceViewComponent component,
            IParameterContentConverter? parametersConverter = null,
            IEnumerable<IPlaceArea>? placeAreas = null,
            Func<IPlaceArea, DataTemplate>? dataTemplateFunc = null,
            int ordering = PlacementOrdering.BeforeAll)
        {
            var parameters = component.Parameters;

            var filteringCommand = new DelegateCommand(
                async _ => await this.advancedFilterViewDialogManager.OpenAsync(
                    descriptor,
                    parameters,
                    CancellationToken.None),
                _ => component.SubmitQuery(new CanFilterQuery(parameters.Metadata)));

            var clearFilterCommand = new DelegateCommand(
                async _ => await component.SubmitCommandAsync(new ClearParametersCommand(parameters)),
                _ => component.SubmitQuery(new CanClearParametersQuery(parameters)));

            return new FilterTextViewContent(
                filteringCommand,
                clearFilterCommand,
                parameters,
                parametersConverter ?? DefaultParameterContentConverter.Instance,
                placeAreas,
                dataTemplateFunc,
                ordering);
        }

        #endregion
    }
}
