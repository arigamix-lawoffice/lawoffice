#nullable enable

using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Roles.Acl;

namespace Tessa.Extensions.Default.Server.Acl
{
    /// <summary>
    /// Фабрика объектов кеша правил расчёта ACL, которая учитывает разбиение триггеров по типу документа.
    /// </summary>
    public class KrAclGenerationRuleCacheObjectFactory : AclGenerationRuleCacheObjectFactory
    {
        #region Fields

        /// <inheritdoc cref="IDbScope"/>
        protected readonly IDbScope DbScope;

        /// <inheritdoc cref="IKrTypesCache"/>
        protected readonly IKrTypesCache TypesCache;

        #endregion

        #region Constructors

        public KrAclGenerationRuleCacheObjectFactory(
            IAclGenerationRuleProvider aclGenerationRuleProvider,
            IDbScope dbScope,
            IKrTypesCache typesCache)
            : base(aclGenerationRuleProvider)
        {
            this.DbScope = NotNullOrThrow(dbScope);
            this.TypesCache = NotNullOrThrow(typesCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override IAclGenerationRuleCacheObject CreateCacheObject()
        {
            return new KrAclGenerationRuleCacheObject(
                this.DbScope,
                this.TypesCache);
        }

        #endregion
    }
}
