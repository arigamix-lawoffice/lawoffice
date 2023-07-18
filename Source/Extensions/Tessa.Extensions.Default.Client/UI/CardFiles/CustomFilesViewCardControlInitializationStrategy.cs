#nullable enable

using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Scheme;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using Tessa.UI.Menu;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Customized file view initialization strategy.
    /// </summary>
    public sealed class CustomFilesViewCardControlInitializationStrategy :
        FilesViewCardControlInitializationStrategy
    {
        #region Constructor

        /// <summary>
        /// Create new instance of custom files in view extension card control initialization strategy. 
        /// </summary>
        /// <param name="viewService"><inheritdoc cref="IViewService" path="/summary"/></param>
        /// <param name="createMenuContextFunc"><inheritdoc cref="CreateMenuContextFunc" path="/summary"/></param>
        /// <param name="contentItemsFactory"><inheritdoc cref="IViewCardControlContentItemsFactory" path="/summary"/></param>
        public CustomFilesViewCardControlInitializationStrategy(
            IViewService viewService,
            CreateMenuContextFunc createMenuContextFunc,
            IViewCardControlContentItemsFactory contentItemsFactory)
            : base(viewService, createMenuContextFunc, contentItemsFactory)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask<IViewMetadata> CreateViewMetadataAsync(CardViewControlInitializationContext context)
        {
            var viewMetadata = await base.CreateViewMetadataAsync(context).ConfigureAwait(false);
            var dateColumnMetadata = new ViewColumnMetadata
            {
                Caption = LocalizationManager.Localize("$CardTypes_Columns_Controls_Date"),
                Alias = "Date",
                SchemeType = SchemeType.DateTime,
                DisableGrouping = true,
                SortBy = "Date",
            };
            var descriptionColumnMetadata = new ViewColumnMetadata
            {
                Caption = LocalizationManager.Localize("$CardTypes_Controls_Description"),
                Alias = "Description",
                SchemeType = SchemeType.NullableString,
                DisableGrouping = true,
                SortBy = "Description",
            };
            viewMetadata.Columns.AddRange(dateColumnMetadata, descriptionColumnMetadata);
            return viewMetadata;
        }

        /// <inheritdoc/>
        protected override ValueTask<IDataProvider> CreateDataProviderAsync(
            CardViewControlInitializationContext context, IViewMetadata viewMetadata, IFileControl fileControl) =>
            new(new CustomCardFilesDataProvider(viewMetadata, fileControl));

        #endregion
    }
}
