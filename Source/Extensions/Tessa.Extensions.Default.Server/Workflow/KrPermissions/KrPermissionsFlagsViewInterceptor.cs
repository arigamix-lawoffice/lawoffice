using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Types;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Перехватчик представления для формирования списка настроек прав доступа.
    /// </summary>
    public sealed class KrPermissionsFlagsViewInterceptor
        : ViewInterceptorBase
    {
        #region Constants

        private const string InterceptedViewAlias = "KrPermissionFlags";

        #endregion

        #region Constructors

        public KrPermissionsFlagsViewInterceptor(
            ISchemeTypeConverter schemeTypeConverter,
            IDbScope dbScope)
            : base(new[] { InterceptedViewAlias })
        {
            Check.ArgumentNotNull(schemeTypeConverter, nameof(schemeTypeConverter));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            this.schemeTypeConverter = schemeTypeConverter;
            this.dbScope = dbScope;
        }

        #endregion

        #region Private Fields

        private readonly ISchemeTypeConverter schemeTypeConverter;

        private readonly IDbScope dbScope;

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!this.InterceptedViews.TryGetValue(
                    request.ViewAlias ?? throw new InvalidOperationException("View alias isn't specified."),
                    out ITessaView view))
            {
                throw new InvalidOperationException($"Can't find view with alias:'{request.ViewAlias}'");
            }

            var dbms = await this.dbScope.GetDbmsAsync(cancellationToken);

            var preparedData = KrPermissionFlagDescriptors.Full.IncludedPermissions
                .Select(x => (ID: x.ID, Name: x.Description, LocalizedName: LocalizationManager.Localize(x.Description)));

            var sortDirection = request.SortDirection("FlagCaption");
            switch (sortDirection)
            {
                case "asc":
                    preparedData = preparedData.OrderBy(x => x.LocalizedName);
                    break;

                case "desc":
                    preparedData = preparedData.OrderByDescending(x => x.LocalizedName);
                    break;
            }

            await request.IterateParameterCriteriasAsync("CaptionParam",
                async (crit, ct) =>
                {
                    if (crit.Values.Count > 0)
                    {
                        preparedData = preparedData.Where(x => x.LocalizedName.Contains((string) crit.Values[0].Value, StringComparison.OrdinalIgnoreCase));
                    }
                },
                cancellationToken: cancellationToken);

            IViewMetadata viewMetadata = await view.GetMetadataAsync(cancellationToken);

            return
                new TessaViewResult
                {
                    SchemeTypes = (from c in viewMetadata.Columns select c.SchemeType).ToList(),
                    Columns = (from c in viewMetadata.Columns select (object) c.Alias).ToList(),
                    DataTypes = (from c in viewMetadata.Columns select (object) this.schemeTypeConverter.TryGetSqlTypeName(c.SchemeType, dbms)).ToList(),
                    Rows = preparedData.Select(x => (object) new List<object> { x.ID, x.Name }).ToList(),
                };
        }

        #endregion
    }
}
