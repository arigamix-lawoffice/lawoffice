#nullable enable

using System.Collections.Generic;
using Tessa.Extensions.Default.Client.ExternalFiles;
using Tessa.Scheme;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Custom data provider for files in view extension.
    /// </summary>
    public sealed class CustomCardFilesDataProvider :
        CardFilesDataProvider
    {
        #region Constructor

        /// <summary>
        /// Create new instance of custom data provider.
        /// </summary>
        /// <param name="viewMetadata"><inheritdoc cref="IViewMetadata" path="/summary"/></param>
        /// <param name="fileControl"><inheritdoc cref="IFileControl" path="/summary"/></param>
        public CustomCardFilesDataProvider(IViewMetadata viewMetadata, IFileControl fileControl)
            : base(viewMetadata, fileControl)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override void AddFieldDescriptions(IGetDataResponse result)
        {
            base.AddFieldDescriptions(result);
            result.Columns.Add(new("Date", SchemeType.DateTime));
            result.Columns.Add(new("Description", SchemeType.NullableString));
        }

        /// <inheritdoc/>
        protected override Dictionary<string, object?> MapFileToRow(IFileViewModel file)
        {
            var row = base.MapFileToRow(file);
            row["Date"] = file.Model.Created;
            row["Description"] = (file.Model as ExternalFile)?.Description;
            return row;
        }

        #endregion
    }
}
