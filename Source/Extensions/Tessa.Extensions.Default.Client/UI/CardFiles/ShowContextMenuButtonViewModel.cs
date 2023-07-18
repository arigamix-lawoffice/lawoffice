using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Tessa.Properties.Resharper;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using Tessa.UI.Menu;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Вью-модель кнопки вызова меню файлов.
    /// </summary>
    [UsedImplicitly]
    public sealed class ShowContextMenuButtonViewModel : CommandViewModel<object>
    {
        #region Fields

        private IFileControl fileControl;

        [CanBeNull]
        private CardViewControlViewModel viewModel;

        #endregion

        #region Properties

        [CanBeNull]
        public IFileControl FileControl
        {
            get => this.fileControl;
            set
            {
                if (Equals(value, this.fileControl))
                {
                    return;
                }

                this.fileControl = value;
                this.OnPropertyChanged(nameof(this.FileControl));
            }
        }

        [CanBeNull]
        public CardViewControlViewModel ViewModel
        {
            get => this.viewModel;
            set
            {
                if (Equals(value, this.viewModel))
                {
                    return;
                }

                this.viewModel = value;
                this.OnPropertyChanged(nameof(this.ViewModel));
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override bool CanExecuteOverride(object commandParameter)
        {
            return this.fileControl != null && this.viewModel != null;
        }

        /// <inheritdoc />
        protected override async void ExecuteOverride(object commandParameter)
        {
            (IMenuActionCollection actions, _, _) = await this.fileControl.GenerateControlMenuAsync();
            var excludedActions = new[] {FileMenuActionNames.Sortings};
            IMenuActionCollectionVisual visual = this.fileControl.ActionGenerator.GenerateActions(
                new MenuActionCollection(actions.Where(x => !excludedActions.Contains(x.Name))), this.fileControl.ExecuteInContextAsync);

            var menuManager = new MenuManager(new ContextMenu { HasDropShadow = true }, visual)
            {
                Placement = PlacementMode.MousePoint,
            };

            menuManager.Initialize();
            menuManager.ShowContextMenu();
        }

        /// <inheritdoc />
        public override bool Focusable => true;

        #endregion
    }
}
