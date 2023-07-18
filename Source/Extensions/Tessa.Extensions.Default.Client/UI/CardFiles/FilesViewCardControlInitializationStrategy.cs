#nullable enable

using System.Threading.Tasks;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using Tessa.UI.Menu;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Files view model initialization strategy.
    /// </summary>
    public class FilesViewCardControlInitializationStrategy : ViewCardControlInitializationStrategy
    {
        #region Constructor

        public FilesViewCardControlInitializationStrategy(
            IViewService viewService,
            CreateMenuContextFunc createMenuContextFunc,
            IViewCardControlContentItemsFactory contentItemsFactory)
            : base(viewService, createMenuContextFunc, contentItemsFactory)
        {
        }

        #endregion

        #region Base overrides

        public override async ValueTask InitializeMetadataAsync(CardViewControlInitializationContext context)
        {
            context.ControlViewModel.ViewMetadata =
                await this.CreateViewMetadataAsync(context).ConfigureAwait(false);
        }

        public override async ValueTask InitializeDataProviderAsync(CardViewControlInitializationContext context)
        {
            context.ControlViewModel.DataProvider =
                await this.CreateDataProviderAsync(
                    context,
                    NotNullOrThrow(context.ControlViewModel.ViewMetadata),
                    FilesViewGeneratorBaseUIExtension.TryGetFileControl(
                        context.Model.Info,
                        context.ControlViewModel.Name)).ConfigureAwait(false);
        }

        /// <summary>
        /// Create view metadata for files view.
        /// </summary>
        /// <param name="context">Initialization context.</param>
        /// <returns>View metadata.</returns>
        protected virtual ValueTask<IViewMetadata> CreateViewMetadataAsync(
            CardViewControlInitializationContext context) =>
            new(FilesViewMetadata.Create());

        /// <summary>
        /// Create data provider for files view.
        /// </summary>
        /// <param name="context">Initialization context.</param>
        /// <param name="viewMetadata">Metadata of source view.</param>
        /// <param name="fileControl">Target control.</param>
        /// <returns>Data provider.</returns>
        protected virtual ValueTask<IDataProvider> CreateDataProviderAsync(
            CardViewControlInitializationContext context,
            IViewMetadata viewMetadata,
            IFileControl fileControl) =>
            new(new CardFilesDataProvider(viewMetadata, fileControl));

        #endregion
    }
}
