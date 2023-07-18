#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    /// Возвращает список программных представлений правил доступа, построенных на основе базовых реализаций представлений,
    /// в метаданных которых удаляются колонки, параметры и сабсеты, относящиеся к ACL, если отсутствует модуль лицензии <see cref="LicenseModules.AclID"/>.
    /// </summary>
    public sealed class KrPermissionsViewListProvider : IExtraViewListProvider
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly Func<ViewDataAccessor> viewDataAccessorFunc;
        private readonly TessaViewFactory viewFactory;
        private readonly ILicenseManager licenseManager;

        private static readonly string[] ViewNames = new[]
        {
            "KrPermissions",
            "KrPermissionsReport",
        };

        #endregion

        #region Constructors

        public KrPermissionsViewListProvider(
            IDbScope dbScope,
            Func<ViewDataAccessor> viewDataAccessorFunc,
            TessaViewFactory viewFactory,
            ILicenseManager licenseManager)
        {
            this.dbScope = NotNullOrThrow(dbScope);
            this.viewDataAccessorFunc = NotNullOrThrow(viewDataAccessorFunc);
            this.viewFactory = NotNullOrThrow(viewFactory);
            this.licenseManager = NotNullOrThrow(licenseManager);
        }

        #endregion

        #region IExtraViewListProvider Implementation

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyList<ITessaView>> GetExtraViewsAsync(CancellationToken cancellationToken = default)
        {
            var license = await this.licenseManager.GetLicenseAsync(cancellationToken);
            var useAcl = license.Modules.HasEnterpriseOrContains(LicenseModules.AclID);
            if (useAcl)
            {
                return Array.Empty<ITessaView>();
            }

            await using var _ = this.dbScope.Create();

            var dbms = await this.dbScope.GetDbmsAsync(cancellationToken);
            var viewDataAccessor = this.viewDataAccessorFunc();
            List<ITessaView> extraViews = new(ViewNames.Length);
            foreach (var viewName in ViewNames)
            {
                var originalViewModel = await viewDataAccessor.GetViewByAliasAsync(viewName, cancellationToken);
                if (originalViewModel is not null)
                {
                    var originalView = this.viewFactory(dbms, originalViewModel);

                    extraViews.Add(
                        new KrPermissionsViewDecorator(originalView));
                }
            }

            return new ReadOnlyCollection<ITessaView>(extraViews);
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Декоратор для представлений правил доступа, которое удаляет все упоминания об ACL из метаданных и результата, если данный модуль не включён в лицензию.
        /// </summary>
        private sealed class KrPermissionsViewDecorator : ITessaView
        {
            #region Fields

            private readonly ITessaView originalView;
            private IViewMetadata? metadata;

            #endregion

            #region Constructors

            public KrPermissionsViewDecorator(
                ITessaView originalView)
            {
                this.originalView = NotNullOrThrow(originalView);
            }

            #endregion

            #region ITessaView Implementation

            /// <inheritdoc/>
            public Task<ITessaViewResult> GetDataAsync(
                ITessaViewRequest request,
                CancellationToken cancellationToken = default)
            {
                return this.originalView.GetDataAsync(request, cancellationToken);
            }

            /// <inheritdoc/>
            public async ValueTask<IViewMetadata> GetMetadataAsync(
                CancellationToken cancellationToken = default)
            {
                return this.metadata ??= await this.InitializeMetadataAsync(cancellationToken);
            }

            #endregion

            #region Private Methods

            private async ValueTask<IViewMetadata> InitializeMetadataAsync(
                CancellationToken cancellationToken)
            {
                var originalMetadata = await this.originalView.GetMetadataAsync(cancellationToken);
                var cloneMetadata = (IViewMetadata) originalMetadata.Clone();

                if (cloneMetadata.Columns.FindByName("KrPermissionsAclGenerationRules") is { } column)
                {
                    column.Hidden = true;
                }

                if (cloneMetadata.Parameters.FindByName("AclGenerationRule") is { } parameter)
                {
                    cloneMetadata.Parameters.Remove(parameter);
                }

                if (cloneMetadata.Subsets.FindByName("ByAclGenerationRule") is { } subset)
                {
                    cloneMetadata.Subsets.Remove(subset);
                }

                cloneMetadata.Seal();

                return cloneMetadata;
            }

            #endregion
        }

        #endregion
    }
}
