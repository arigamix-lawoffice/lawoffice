#nullable enable

using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Roles.SmartRoles;

namespace Tessa.Extensions.Default.Server.Roles
{
    /// <summary>
    /// Фабрика для создания объектов экземпляра кеша генератора умных ролей,
    /// которые учитывают обработку триггеров по типам документов.
    /// </summary>
    public class KrSmartRoleGeneratorCacheObjectFactory : SmartRoleGeneratorCacheObjectFactory
    {
        #region Fields

        /// <inheritdoc cref="IDbScope"/>
        protected readonly IDbScope DbScope;

        /// <inheritdoc cref="IKrTypesCache"/>
        protected readonly IKrTypesCache TypesCache;

        #endregion

        #region Constructors

        public KrSmartRoleGeneratorCacheObjectFactory(
            ISmartRoleGeneratorProvider provider,
            IDbScope dbScope,
            IKrTypesCache typesCache)
            : base(provider)
        {
            this.DbScope = NotNullOrThrow(dbScope);
            this.TypesCache = NotNullOrThrow(typesCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override ISmartRoleGeneratorCacheObject CreateCacheObject()
        {
            return new KrSmartRoleGeneratorCacheObject(
                this.DbScope,
                this.TypesCache);
        }

        #endregion
    }
}
