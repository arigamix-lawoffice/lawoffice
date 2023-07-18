using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.EDS;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;
using Tessa.Views.Parser;

namespace Tessa.Extensions.Default.Server.EDS
{
    /// <summary>
    /// Перехватчик для представления <c>EdsManagers</c>, которое возвращает список имён объектов <see cref="ICAdESManager"/>
    /// из метаинформации по таблице <c>SignatureManagerVirtual</c>.
    /// </summary>
    public sealed class EdsManagerInterceptor :
        ViewInterceptorBase
    {
        #region Constructors

        public EdsManagerInterceptor(
            ICardMetadata cardMetadata,
            Func<IViewService> viewService)
            : base(new[] { InterceptedViewAlias })
        {
            this.cardMetadata = cardMetadata ?? throw new ArgumentNullException(nameof(cardMetadata));
            this.viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
        }

        #endregion

        #region Private Classes

        private sealed class DataRow
        {
            public DataRow(string name) => this.Name = name;

            public string Name { get; }

            public object GetRow() => new object[] { this.Name };
        }


        private abstract class FilterOperation
        {
            public abstract bool IsSatisfied(DataRow row);
        }


        private sealed class NoFilterOperation : FilterOperation
        {
            public override bool IsSatisfied(DataRow row) => true;
        }


        private sealed class EqualsFilterOperation : FilterOperation
        {
            public string Text { get; set; }

            public override bool IsSatisfied(DataRow row) =>
                string.Equals(row.Name, this.Text, StringComparison.OrdinalIgnoreCase);
        }


        private sealed class ContainsFilterOperation : FilterOperation
        {
            public string Text { get; set; }

            public override bool IsSatisfied(DataRow row) =>
                row.Name?.Contains(this.Text, StringComparison.OrdinalIgnoreCase) == true;
        }

        #endregion

        #region Constants and Fields

        private const string InterceptedViewAlias = "EdsManagers";

        private readonly ICardMetadata cardMetadata;

        private readonly Func<IViewService> viewService;

        #endregion

        #region Private Methods

        private async ValueTask<IReadOnlyList<DataRow>> GenerateDataAsync(CancellationToken cancellationToken = default) =>
            (await this.cardMetadata.GetEnumerationsAsync(cancellationToken))["SignatureManagerVirtual"].Records
            .Select(record => record["Name"] as string)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => new DataRow(x))
            .OrderBy(x => x.Name)
            .ToArray();


        private static FilterOperation GetFilterOperation(ITessaViewRequest request)
        {
            RequestParameter parameter = request.TryGetParameter("Name");
            if (parameter is null)
            {
                return new NoFilterOperation();
            }

            RequestCriteria criteria = parameter.CriteriaValues.FirstOrDefault();
            return criteria?.CriteriaName switch
            {
                CriteriaOperatorConst.Equality => new EqualsFilterOperation { Text = criteria.Values[0].Value?.ToString() },
                CriteriaOperatorConst.Contains => new ContainsFilterOperation { Text = criteria.Values[0].Value?.ToString() },
                _ => new NoFilterOperation()
            };
        }


        private static ITessaViewResult GetDataCore(IViewMetadata metadata, FilterOperation filterOperation, IEnumerable<DataRow> data) =>
            new TessaViewResult
            {
                Columns = metadata.Columns.Select(c => c.Alias).Cast<object>().ToList(),
                SchemeTypes = new[] { SchemeType.String },
                Rows = data.Where(filterOperation.IsSatisfied).Select(d => d.GetRow()).ToList()
            };

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!ParserNames.IsEquals(request.View?.Alias, InterceptedViewAlias))
            {
                throw new InvalidOperationException("Unknown view");
            }

            ITessaView view = await this.viewService().GetByNameAsync(InterceptedViewAlias, cancellationToken);
            if (view is null)
            {
                throw new InvalidOperationException("Unknown view");
            }

            return GetDataCore(
                await view.GetMetadataAsync(cancellationToken),
                GetFilterOperation(request),
                await this.GenerateDataAsync(cancellationToken));
        }

        #endregion
    }
}