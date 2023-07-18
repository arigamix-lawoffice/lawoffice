// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomButtonWorkplaceComponentExtension.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// <summary>
//   Расширение рабочего места позволяющее добавить кнопку на панель в элемента рабочего места
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;

    using Tessa.Properties.Resharper;
    using Tessa.UI;
    using Tessa.UI.Views;
    using Tessa.UI.Views.Content;

    /// <summary>
    /// Расширение рабочего места позволяющее добавить кнопку на панель в элемент рабочего места
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="CustomButtonWorkplaceComponentExtensionConfigurator"/>
    /// </remarks>
    public sealed class CustomButtonWorkplaceComponentExtension : IWorkplaceViewComponentExtension
    {
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
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/> 
        /// </summary>
        /// <param name="model">
        /// Инициализируемая модель
        /// </param>
        public void Initialize(IWorkplaceViewComponent model)
        {
            model.ContentFactories
                ["Table"] =
                c =>
                new OkButton(
                    new DelegateCommand(o => MessageBox.Show("You pressed Custom Button1")), 
                    ContentPlaceAreas.ContentPlaces, 
                    (area) =>
                        {
                                var dictionary = new ResourceDictionary
                                                     {
                                                         Source =
                                                             new Uri(
                                                             "pack://application:,,,/Arigamix.Extensions.Default.Client;component/Themes/Generic.xaml",
                                                             UriKind.Absolute)
                                                     };
                                return dictionary["CustomButtonTemplate"] as DataTemplate;
                        });
            model.ContentFactories
               ["Table1"] =
               c =>
               new OkButton(
                   new DelegateCommand(o => MessageBox.Show("You pressed Custom Button2")),
                   ContentPlaceAreas.ContentPlaces,
                   (area) =>
                   {
                       var dictionary = new ResourceDictionary
                       {
                           Source =
                               new Uri(
                               "pack://application:,,,/Arigamix.Extensions.Default.Client;component/Themes/Generic.xaml",
                               UriKind.Absolute)
                       };
                       return dictionary["CustomButtonTemplate"] as DataTemplate;
                   });
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

        /// <summary>
        /// The ok button.
        /// </summary>
        private sealed class OkButton : BaseContentItem
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="OkButton"/> class. 
            /// Initializes a new instance of the <see cref="BaseContentItem"/> class. 
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            /// <param name="command">
            /// The command.
            /// </param>
            /// <param name="placeAreas">
            /// Список областей вывода элемента
            /// </param>
            /// <param name="dataTemplateFunc">
            /// Обработка выдачи шаблона в зависимости от области расположения <paramref name="placeAreas"/> элемента
            /// </param>
            /// <param name="ordering">
            /// Порядок вывода элемента
            /// </param>
            public OkButton(
                ICommand command, 
                [CanBeNull] IEnumerable<IPlaceArea> placeAreas, 
                [CanBeNull] Func<IPlaceArea, DataTemplate> dataTemplateFunc = null, 
                int ordering = PlacementOrdering.Middle)
                : base(placeAreas, dataTemplateFunc, ordering)
            {
                this.Command = command;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets Команда кнопки
            /// </summary>
            public ICommand Command { get; private set; }

            #endregion
        }
    }
}