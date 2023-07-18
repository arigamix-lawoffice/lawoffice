using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Перехватчики представлений правил доступа для обработки параметров фильтрации по настройкам прав доступа.
    /// </summary>
    public sealed class KrPermissionsViewInterceptor
        : ViewInterceptorBase
    {
        #region Constructors

        public KrPermissionsViewInterceptor(
            IDbScope dbScope)
            : base(new[] { "KrPermissions", "KrPermissionsReport" })
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            this.dbScope = dbScope;
        }

        #endregion

        #region Private Fields

        private readonly IDbScope dbScope;

        #endregion

        #region Private Methods

        private static void RemoveParameter(ITessaViewRequest request, string paramName)
        {
            var expressionParameter = request.Values.TryGetParameter(paramName);
            if (expressionParameter is not null)
            {
                request.Values.Remove(expressionParameter);
            }
        }

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

            // Удаляемый любую попытку из вне прокинуть этот параметр в представление
            RemoveParameter(request, "PermissionExpression");
            RemoveParameter(request, "ByPermissionExpression");

            await using (this.dbScope.Create())
            {
                var permissionsParameter = request.TryGetParameter("Permission");
                if (permissionsParameter is not null)
                {
                    string permissionExpressionValue = string.Empty;
                    var flagTuples = permissionsParameter.CriteriaValues.Select(x => (
                        CriteriaName: x.CriteriaName,
                        Flag: KrPermissionFlagDescriptors.Full.IncludedPermissions.FirstOrDefault(y => y.ID == ((Guid) x.Values[0].Value))
                    )).Where(x => x.Flag is not null).ToArray();

                    if (flagTuples.Any())
                    {
                        var builder = this.dbScope.BuilderFactory.Create().And().E(b =>
                        {
                            bool first = true;
                            foreach (var (criteriaName, flag) in flagTuples)
                            {
                                if (flag is not null
                                    && !flag.IsVirtual)
                                {
                                    if (first)
                                    {
                                        first = false;
                                    }
                                    else
                                    {
                                        b.Or();
                                    }

                                    if (criteriaName == CriteriaOperatorConst.Equality)
                                    {
                                        b.C("t", flag.SqlName).Equals().V(true);
                                    }
                                    else if (criteriaName == CriteriaOperatorConst.NonEquality)
                                    {
                                        b.C("t", flag.SqlName).Equals().V(false);
                                    }
                                }
                            }
                        });

                        permissionExpressionValue = builder.ToString();
                        // Освобождаем билдер
                        builder.Build();
                    }

                    request.Values.Add(
                        new RequestParameterBuilder()
                            .WithMetadata(new ViewParameterMetadata() { Alias = "PermissionExpression", SchemeType = SchemeType.String })
                            .AddCriteria(new EqualsCriteriaOperator(), string.Empty, permissionExpressionValue)
                            .AsRequestParameter());
                }

                if (request.SubsetName == "ByPermission")
                {
                    var builder = this.dbScope.BuilderFactory.Create();
                    var first = true;

                    foreach (var flag in KrPermissionFlagDescriptors.Full.IncludedPermissions)
                    {
                        if (flag.IsVirtual)
                        {
                            continue;
                        }

                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            builder.UnionAll();
                        }

                        builder
                            .Select()
                                .V(flag.ID).As("PermissionID")
                                .V(flag.Description).As("PermissionName")
                                .C("ID")
                            .From("KrPermissions").NoLock()
                            .Where().C(flag.SqlName).Equals().V(true);
                    }

                    request.Values.Add(
                        new RequestParameterBuilder()
                            .WithMetadata(new ViewParameterMetadata() { Alias = "ByPermissionExpression", SchemeType = SchemeType.String })
                            .AddCriteria(new EqualsCriteriaOperator(), string.Empty, builder.ToString())
                            .AsRequestParameter());

                    // Освобождаем билдер
                    builder.Build();
                }

                return await view.GetDataAsync(
                    request,
                    cancellationToken);
            }
        }

        #endregion
    }
}
