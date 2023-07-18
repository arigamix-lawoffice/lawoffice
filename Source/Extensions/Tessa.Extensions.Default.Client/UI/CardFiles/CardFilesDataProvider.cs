#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Views;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Formatting;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// File data provider.
    /// </summary>
    public class CardFilesDataProvider : IDataProvider
    {
        #region Nested Types

        /// <summary>
        /// Column description for sorting.
        /// </summary>
        /// <param name="SortDirection">Sorting direction.</param>
        /// <param name="Alias">Column name.</param>
        protected readonly record struct SortingColumn(ListSortDirection SortDirection, string Alias);
        
        /// <summary>
        /// File sorter.
        /// </summary>
        protected class FileSorter : IAsyncInitializable, IComparer<IDictionary<string, object?>>
        {
            #region Constuctor

            /// <summary>
            /// Creates new sorter object.
            /// </summary>
            /// <param name="dataProvider">Data provider.</param>
            /// <param name="request">Data request.</param>
            public FileSorter(CardFilesDataProvider dataProvider, IGetDataRequest request)
            {
                this.DataProvider = NotNullOrThrow(dataProvider);
                this.Request = NotNullOrThrow(request);
            }
            
            #endregion
            
            #region Properties

            /// <summary>
            /// Columns for sorting data.
            /// </summary>
            public List<SortingColumn> SortingColumns { get; } = new();
            
            /// <summary>
            /// Data provider.
            /// </summary>
            protected CardFilesDataProvider DataProvider { get; }

            /// <summary>
            /// Data request.
            /// </summary>
            protected IGetDataRequest Request { get; }

            #endregion

            #region IAsyncInitializable Implementation
            
            /// <inheritdoc />
            public virtual ValueTask InitializeAsync(CancellationToken cancellationToken = default)
            {
                foreach (var column in this.Request.SortingColumns ?? Enumerable.Empty<ISortingColumn>())
                {
                    var sortingColumn = new SortingColumn(
                        column.SortDirection,
                        NotNullOrThrow(this.DataProvider.ViewMetadata.Columns.FindByName(column.Alias)).SortBy);
                    this.SortingColumns.Add(sortingColumn);
                }
                
                return ValueTask.CompletedTask;
            }

            #endregion
            
            #region IComparer Implementation

            /// <inheritdoc />
            public virtual int Compare(IDictionary<string, object?>? lhv, IDictionary<string, object?>? rhv)
            {
                if (lhv is null && rhv is null)
                {
                    return 0;
                }

                if (lhv is null)
                {
                    return -1;
                }

                if (rhv is null)
                {
                    return 1;
                }

                var lvm = NotNullOrThrow(lhv.Get<IFileViewModel>(ColumnsConst.FileViewModel));
                var rvm = NotNullOrThrow(rhv.Get<IFileViewModel>(ColumnsConst.FileViewModel));

                // оригинал всегда "выше" чем копия файла независимо от направления сортировки
                if (lvm.Model.Origin == rvm.Model)
                {
                    return 1;
                }

                if (rvm.Model.Origin == lvm.Model)
                {
                    return -1;
                }

                foreach (SortingColumn sortingColumn in this.SortingColumns)
                {
                    int comparison = Comparer.Default.Compare(lhv.Get<object>(sortingColumn.Alias), rhv.Get<object>(sortingColumn.Alias));
                    if (comparison == 0)
                    {
                        continue;
                    }
                    
                    if (sortingColumn.SortDirection == ListSortDirection.Descending)
                    {
                        comparison = -comparison;
                    }

                    return comparison;
                }

                return 0;
            }

            #endregion
        }

        /// <summary>
        /// Parameter filter criteria joined by logical OR (for single parameter).
        /// </summary>
        /// <param name="CriteriaFilters">Filters for given parameter.</param>
        protected sealed record ParameterFilterItem(List<Func<IFileViewModel, bool>> CriteriaFilters)
        {
            /// <summary>
            /// Creates filter for single parameter joined by logical OR from given criteria.
            /// </summary>
            /// <param name="criteriaFilters">Filtering criteria.</param>
            public ParameterFilterItem(params Func<IFileViewModel, bool>[] criteriaFilters)
                : this(new List<Func<IFileViewModel, bool>>(criteriaFilters))
            {
            }
        }

        /// <summary>
        /// Filter for files in view.
        /// </summary>
        protected class FileFilter : IAsyncInitializable
        {
            #region Constructor

            /// <summary>
            /// Create new filter for files in view.
            /// </summary>
            /// <param name="dataProvider">Data provider.</param>
            /// <param name="request">Data request.</param>
            public FileFilter(
                CardFilesDataProvider dataProvider,
                IGetDataRequest request)
            {
                this.DataProvider = NotNullOrThrow(dataProvider);
                this.Request = NotNullOrThrow(request);
            }

            #endregion

            #region Properties

            /// <summary>
            /// Filtering parameters. One item for one parameter joined by logical AND.
            /// </summary>
            public List<ParameterFilterItem> ParameterFilters { get; } = new();

            /// <summary>
            /// Data provider.
            /// </summary>
            protected CardFilesDataProvider DataProvider { get; }

            /// <summary>
            /// Data request.
            /// </summary>
            protected IGetDataRequest Request { get; }

            #endregion

            #region IAsyncInitializable Implementation

            /// <inheritdoc />
            public virtual ValueTask InitializeAsync(CancellationToken cancellationToken = default)
            {
                var alwaysTrueBlock = new ParameterFilterItem(AlwaysTrue);
                this.ParameterFilters.Clear();
                this.ParameterFilters.Add(alwaysTrueBlock);
                var requestParameters = this.DataProvider.BuildParametersCollectionFromRequest(this.Request);
                foreach (var parameter in requestParameters)
                {
                    if (parameter.Name == ColumnsConst.Caption)
                    {
                        var filterBlock = new ParameterFilterItem();
                        foreach (var criteria in parameter.CriteriaValues)
                        {
                            this.AppendCriteriaToCaptionFilterFunc(criteria, filterBlock);
                        }

                        this.ParameterFilters.Add(filterBlock);
                    }
                }

                return ValueTask.CompletedTask;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Make filtration.
            /// </summary>
            /// <param name="fileObject">Target view model.</param>
            /// <returns>Whether given view model must be in resulting data set.</returns>
            public bool Filter(IFileViewModel fileObject)
            {
                return this.ParameterFilters
                    .Select(block =>
                        block.CriteriaFilters.Aggregate(false, (current, func) => current | func(fileObject)))
                    .Aggregate(true, (filterResult, blockResult) => filterResult & blockResult);
            }

            #endregion

            #region Protected Methods

            /// <summary>
            /// Build up criteria for name filtration.
            /// </summary>
            /// <param name="criteria">Source criteria.</param>
            /// <param name="filterBlock">Target filtering block.</param>
            /// <exception cref="ArgumentOutOfRangeException">Given criterion is unsupported.</exception>
            protected virtual void AppendCriteriaToCaptionFilterFunc(
                RequestCriteria criteria, ParameterFilterItem filterBlock)
            {
                switch (criteria.CriteriaName)
                {
                    case CriteriaOperatorConst.Contains:
                        filterBlock.CriteriaFilters.Add(f => f.Caption.Contains(
                            ((string) criteria.Values[0].Value) ?? string.Empty,
                            StringComparison.OrdinalIgnoreCase));
                        break;

                    case CriteriaOperatorConst.Equality:
                        filterBlock.CriteriaFilters.Add(f => string.Equals(f.Caption, (string) criteria.Values[0].Value,
                            StringComparison.OrdinalIgnoreCase));
                        break;

                    case CriteriaOperatorConst.StartWith:
                        filterBlock.CriteriaFilters.Add(f => f.Caption.StartsWith(
                            (string) criteria.Values[0].Value ?? string.Empty,
                            StringComparison.OrdinalIgnoreCase));
                        break;

                    case CriteriaOperatorConst.EndWith:
                        filterBlock.CriteriaFilters.Add(f => f.Caption.EndsWith(
                            (string) criteria.Values[0].Value ?? string.Empty,
                            StringComparison.OrdinalIgnoreCase));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(criteria.CriteriaName), criteria.CriteriaName,
                            $"Unsupported criteria: '{criteria.CriteriaName}'");
                }
            }

            /// <summary>
            /// Always true criteria filter.
            /// </summary>
            /// <param name="fileObject">Target view model.</param>
            /// <returns>True.</returns>
            protected static bool AlwaysTrue(IFileViewModel? fileObject) => true;

            #endregion
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates data provider for files in view.
        /// </summary>
        /// <param name="viewMetadata">Source view metadata.</param>
        /// <param name="fileControl">Target file control.</param>
        public CardFilesDataProvider(IViewMetadata viewMetadata, IFileControl fileControl)
        {
            this.ViewMetadata = NotNullOrThrow(viewMetadata);
            this.FileControl = NotNullOrThrow(fileControl);
        }

        #endregion

        #region IDataProvider Implementation

        /// <inheritdoc />
        public async Task<IGetDataResponse> GetDataAsync(
            IGetDataRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = new GetDataResponse(new ValidationResultBuilder());
            this.AddFieldDescriptions(result);
            await this.PopulateDataRowsAsync(request, result, cancellationToken).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Source view metadata.
        /// </summary>
        protected IViewMetadata ViewMetadata { get; }
        
        /// <summary>
        /// Target file control.
        /// </summary>
        protected IFileControl FileControl { get; }

        #endregion

        #region Protected Methods
        
        /// <summary>
        /// Make base description of data response.
        /// </summary>
        /// <param name="result">Data response.</param>
        protected virtual void AddFieldDescriptions(IGetDataResponse result)
        {
            result.Columns.Add(new(ColumnsConst.GroupCaption, SchemeType.NullableString));
            result.Columns.Add(new(ColumnsConst.Caption, SchemeType.NullableString));
            result.Columns.Add(new(ColumnsConst.CategoryCaption, SchemeType.NullableString));
            result.Columns.Add(new(ColumnsConst.Size, SchemeType.String));
            result.Columns.Add(new(ColumnsConst.SizeAbsolute, SchemeType.Int64));
        }

        /// <summary>
        /// Fill in the data.
        /// </summary>
        /// <param name="request">Data request</param>
        /// <param name="result">Data response.</param>
        /// <param name="cancellationToken">Object for cancelling asynchronous operation.</param>
        protected virtual async ValueTask PopulateDataRowsAsync(
            IGetDataRequest request,
            IGetDataResponse result,
            CancellationToken cancellationToken = default)
        {
            var filter = await this.GetFilterAsync(request, cancellationToken).ConfigureAwait(false);
            IEnumerable<IFileViewModel> files = this.GetItems();

            var requestParameters = this.BuildParametersCollectionFromRequest(request);

            // если фильтры сбросили через кнопку в представлении, то нужно сбросить фильтрацию в файловом контроле.
            if (this.FileControl.SelectedFiltering != null &&
                requestParameters.All(x => x.Name != ColumnsConst.FilterParameter))
            {
                await this.FileControl.SelectFilteringAsync(null, cancellationToken);
            }

            if (this.FileControl.SelectedFiltering != null)
            {
                files = files.Where(x => this.FileControl.SelectedFiltering.IsVisible(x));
            }

            var filteredFiles = files.Where(filter).ToArray();
            var rows = new List<IDictionary<string, object?>>(filteredFiles.Length);

            foreach (var file in filteredFiles)
            {
                var row = this.MapFileToRow(file);
                rows.Add(row);
            }

            var sorter = await this.GetSorterAsync(request, cancellationToken).ConfigureAwait(false);
            rows.Sort(sorter);
            result.Rows.AddRange(rows);
        }

        /// <summary>
        /// Provide data filter.
        /// </summary>
        /// <param name="request">Data request.</param>
        /// <param name="cancellationToken">Object for cancelling asynchronous operation.</param>
        /// <returns>Filter.</returns>
        protected virtual async ValueTask<Func<IFileViewModel, bool>> GetFilterAsync(
            IGetDataRequest request,
            CancellationToken cancellationToken = default)
        {
            var filter = new FileFilter(this, request);
            await filter.InitializeAsync(cancellationToken).ConfigureAwait(false);
            return filter.Filter;
        }
        
        /// <summary>
        /// Provide data sorter.
        /// </summary>
        /// <param name="request">Data request.</param>
        /// <param name="cancellationToken">Object for cancelling asynchronous operation.</param>
        /// <returns>Sorter.</returns>
        protected virtual async ValueTask<IComparer<IDictionary<string, object?>>> GetSorterAsync(
            IGetDataRequest request,
            CancellationToken cancellationToken = default)
        {
            var sorter = new FileSorter(this, request);
            await sorter.InitializeAsync(cancellationToken).ConfigureAwait(false);
            return sorter;
        }
        
        /// <summary>
        /// Provide data items.
        /// </summary>
        /// <returns>File view model data items.</returns>
        protected virtual IEnumerable<IFileViewModel> GetItems() => this.FileControl.Items;
        
        /// <summary>
        /// Build request parameters.
        /// </summary>
        /// <param name="request">Data request.</param>
        /// <returns>Final request parameters.</returns>
        protected virtual IEnumerable<RequestParameter> BuildParametersCollectionFromRequest(IGetDataRequest request)
        {
            var parametersCollection = new List<RequestParameter>();
            foreach (var action in request.ParametersActions
                     ?? Enumerable.Empty<Action<ICollection<RequestParameter>>>())
            {
                action(parametersCollection);
            }

            return parametersCollection;
        }

        /// <summary>
        /// Make mapping between file view model and target view.
        /// </summary>
        /// <param name="file">Source file view model.</param>
        /// <returns>Data row.</returns>
        protected virtual Dictionary<string, object?> MapFileToRow(IFileViewModel file)
        {
            string size = FormattingHelper.FormatSize(file.Model.Size, SizeUnit.Kilobytes)
                + FormattingHelper.FormatUnit(SizeUnit.Kilobytes);

            return new Dictionary<string, object?>
            {
                [ColumnsConst.FileViewModel] = file,
                [ColumnsConst.GroupCaption] = file.GroupCaption,

                // если в "( Без категории )" не заменять обычные пробелы на неделимые, то автосайз начинает колбасить
                // при наличии нескольких таких строк, и расчёт ширины происходит заметно длительнее
                [ColumnsConst.CategoryCaption] = file.Model.Category?.Caption
                    ?? LocalizationManager.Localize("$UI_Cards_FileNoCategory").ReplaceSpacesToNonBreakable(),

                [ColumnsConst.Caption] = file.Caption,
                [ColumnsConst.SizeAbsolute] = file.Model.Size,
                [ColumnsConst.Size] = size,
            };
        }

        #endregion
    }
}
