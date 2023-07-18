#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tessa.UI;
using Tessa.UI.Controls;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Модель кнопки в представлении для добавления тега.
    /// </summary>
    public sealed class TagsWorkplaceViewDemoActionExtensionViewModel :
        BaseContentItem
    {
        #region Constructors

        public TagsWorkplaceViewDemoActionExtensionViewModel(
            Func<Task> addTagAction,
            IconViewModel icon,
            IEnumerable<IPlaceArea> placeAreas,
            Func<IPlaceArea, DataTemplate>? dataTemplateFunc = null,
            int ordering = PlacementOrdering.Middle)
            : base(placeAreas, dataTemplateFunc, ordering)
        {
            this.Icon = icon;
            this.AddTagsCommand = new DelegateCommand((obj) => addTagAction(), p => this.isEnabled);
        }

        #endregion

        #region Properties

        private bool isEnabled;

        public bool IsEnabled
        {
            get => this.isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.OnPropertyChanged(nameof(this.IsEnabled));
                }
            }
        }
        public IconViewModel Icon { get; }

        public ICommand AddTagsCommand { get; }

        #endregion
    }
}
