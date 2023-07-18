#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Data;
using Tessa.Roles.Queries;
using Tessa.Roles.SmartRoles;

namespace Tessa.Extensions.Default.Server.Roles
{
    /// <summary>
    /// Фабрика для создания объектов данных для генераторов умных ролей по данным карточки "Генератор умных ролей",
    /// которые учитывают обработку триггеров по типам документов.
    /// </summary>
    public class KrSmartRoleGeneratorDataFactory : SmartRoleGeneratorDataFactory
    {
        #region Constructors

        /// <inheritdoc cref=" SmartRoleGeneratorDataFactory(IDbScope, IComplexQueryBuilderFactory)"/>
        public KrSmartRoleGeneratorDataFactory(
            IDbScope dbScope,
            IComplexQueryBuilderFactory getItemsQueryBuilderFactory)
            : base(dbScope, getItemsQueryBuilderFactory)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask<ISmartRoleGeneratorData> CreateAsync(
            SmartRoleGeneratorDataSource source,
            CancellationToken cancellationToken = default)
        {
            var data = new KrSmartRoleGeneratorData(
                source,
                this.DbScope,
                this.GetItemsQueryBuilderFactory);

            await data.InitializeAsync(cancellationToken);

            return data;
        }

        #endregion
    }
}
