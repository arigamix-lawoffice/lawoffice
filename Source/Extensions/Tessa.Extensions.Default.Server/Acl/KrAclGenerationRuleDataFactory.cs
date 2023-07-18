#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Roles.Acl;
using Tessa.Roles.Acl.Extensions;
using Tessa.Roles.Queries;

namespace Tessa.Extensions.Default.Server.Acl
{
    /// <inheritdoc cref="IAclGenerationRuleData"/>
    public class KrAclGenerationRuleDataFactory : AclGenerationRuleDataFactory
    {
        #region Fields

        protected readonly IKrTypesCache KrTypesCache;

        #endregion

        #region Constructors

        /// <inheritdoc cref="AclGenerationRuleDataFactory(IAclGenerationRuleExtensionResolver, IDbScope, IComplexQueryBuilderFactory)"/>
        public KrAclGenerationRuleDataFactory(
            IAclGenerationRuleExtensionResolver extensionsResolver,
            IDbScope dbScope,
            IComplexQueryBuilderFactory getItemsQueryBuilderFactory,
            IKrTypesCache krTypesCache)
            : base(extensionsResolver, dbScope, getItemsQueryBuilderFactory)
        {
            this.KrTypesCache = NotNullOrThrow(krTypesCache);
        }

        #endregion

        #region 

        /// <inheritdoc/>
        public override async ValueTask<IAclGenerationRuleData> CreateAsync(
            AclGenerationRuleDataSource data,
            CancellationToken cancellationToken = default)
        {
            var result = new KrAclGenerationRuleData(
                data,
                this.ExtensionsResolver,
                this.GetItemsQueryBuilderFactory,
                this.DbScope,
                this.KrTypesCache);

            await result.InitializeAsync(cancellationToken);

            return result;
        }

        #endregion
    }
}
