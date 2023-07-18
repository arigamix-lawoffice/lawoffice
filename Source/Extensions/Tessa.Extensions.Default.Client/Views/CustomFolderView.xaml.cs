using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Views.Workplaces.Tree;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Interaction logic for CustomFolderView.xaml
    /// </summary>
    public partial class CustomFolderView
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFolderView"/> class.
        /// </summary>
        /// <param name="folder">
        /// The folder.
        /// </param>
        public CustomFolderView([NotNull] IFolderTreeItem folder)
        {
            this.InitializeComponent();

            this.DataContext = folder;
            this.Resources = UIHelper.Generic;
        }

        #endregion
    }
}