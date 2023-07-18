using Tessa.Extensions.Default.Shared.Views;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Properties.Resharper;
using Tessa.Scheme;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    public static class FilesViewMetadata
    {
        /// <summary>
        ///     Создает метаданные для поставщика данных предоставляющего данные о файлах
        /// </summary>
        /// <returns>Метаданные</returns>
        [NotNull]
        public static IViewMetadata Create()
        {
            var viewMetadata = new ViewMetadata { Alias = "__FilesView", MultiSelect = true, EnableAutoWidth = true, DefaultSortColumn = ColumnsConst.Caption };
            AddDefaultColumns(viewMetadata);
            AddDefaultParameters(viewMetadata);
            return viewMetadata;
        }

        private static void AddDefaultParameters([NotNull] IViewMetadata viewMetadata)
        {
            var nameParameter = new ViewParameterMetadata
            {
                Alias = ColumnsConst.Caption,
                Caption = LocalizationManager.Localize(ColumnsConst.CaptionLocalization),
                SchemeType = SchemeType.String,
                Multiple = true,
                AllowedOperands = new[]
                    { CriteriaOperatorConst.Contains, CriteriaOperatorConst.StartWith, CriteriaOperatorConst.EndWith, CriteriaOperatorConst.Equality }
            };

            viewMetadata.Parameters.Add(nameParameter);

        }

        private static void AddDefaultColumns([NotNull] IViewMetadata viewMetadata)
        {
            var groupCaptionColumn = new ViewColumnMetadata
            {
                Caption = LocalizationManager.Localize(ColumnsConst.GroupLocalization),
                Alias = ColumnsConst.GroupCaption,
                SchemeType = SchemeType.NullableString
            };

            var categoryCaptionColumn = new ViewColumnMetadata
            {
                Caption = LocalizationManager.Localize(ColumnsConst.CategoryLocalization),
                Alias = ColumnsConst.CategoryCaption,
                SchemeType = SchemeType.NullableString,
                DisableGrouping = true,
                Localizable = true
            };

            var captionColumn = new ViewColumnMetadata
            {
                Caption = LocalizationManager.Localize(ColumnsConst.CaptionLocalization),
                Alias = ColumnsConst.Caption,
                SchemeType = SchemeType.String,
                SortBy = ColumnsConst.Caption,
                DisableGrouping = true,
                HasTag = true
            };

            var sizeAbsoluteColumn = new ViewColumnMetadata
            {
                Caption = ColumnsConst.SizeAbsolute,
                Alias = ColumnsConst.SizeAbsolute,
                SchemeType = SchemeType.UInt64,
                SortBy = ColumnsConst.SizeAbsolute,
                DisableGrouping = true,
                Hidden = true
            };

            var sizeColumn = new ViewColumnMetadata
            {
                Caption = LocalizationManager.Localize(ColumnsConst.SizeLocalization),
                Alias = ColumnsConst.Size,
                SchemeType = SchemeType.String,
                DisableGrouping = true,
                SortBy = ColumnsConst.SizeAbsolute,
            };

            viewMetadata.Columns.AddRange(
                groupCaptionColumn,
                captionColumn,
                categoryCaptionColumn,
                sizeAbsoluteColumn,
                sizeColumn
                );
        }
    }
}
