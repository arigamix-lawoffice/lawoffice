#nullable enable

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Themes;
using Tessa.UI;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Расширение для вывода надписи "Выберите тег в дереве слева", вместо таблицы, при открытии представления "TagCards".
    /// </summary>
    public sealed class TagCardsViewExtension :
        IWorkplaceViewComponentExtension
    {
        #region Constructors

        public TagCardsViewExtension()
        {
        }

        #endregion

        #region IWorkplaceViewComponentExtension Implementation

        /// <inheritdoc/>
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        /// <inheritdoc/>
        public void Initialize(IWorkplaceViewComponent model)
        {
        }

        /// <inheritdoc/>
        public void Initialized(IWorkplaceViewComponent model)
        {
            var parameters = model.Parameters;

            if (parameters.Any(x => x.Name == "Tag"))
            {
                return;
            }

            var tableView = model.Content.OfType<TableView>().FirstOrDefault();

            if (tableView is null)
            {
                return;
            }

            tableView.Content = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 27, 0, 0),
                Text = LocalizationManager.GetString("Tags_SelectTag_Text"),
                FontSize = 25,
                Foreground = ThemeManager.Current.Theme.CreateSolidColorBrush(ThemeProperty.ViewToolbarButtonDisabledBrush)
            };
            model.Content.RemoveAll(x => !(x is TableView));
        }

        #endregion
    }
}
