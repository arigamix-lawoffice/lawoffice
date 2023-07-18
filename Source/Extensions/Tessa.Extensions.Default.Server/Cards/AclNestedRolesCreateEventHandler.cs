using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Roles.Acl;
using Tessa.Roles.NestedRoles;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Обработчик события создания вложенной роли, добавляющий записи в Acl при создании новой временной роли.
    /// </summary>
    public sealed class AclNestedRolesCreateEventHandler : INestedRoleEventManagerHandler
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public AclNestedRolesCreateEventHandler(
            IDbScope dbScope,
            IKrTypesCache krTypesCache)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(krTypesCache, nameof(krTypesCache));

            this.dbScope = dbScope;
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region INestedRoleEventManagerHandler Implementation

        ///<inheritdoc/>
        public async ValueTask HandleAsync(INestedRoleEventManagerContext context)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                var types = await this.krTypesCache.GetDocTypesAsync(context.CancellationToken);
                var isDocType = types.Any(x => x.ID == context.NestedRole.ContextID);
                // Вставляем новую вложенную роль в ACL во все записи, где это необходимо.
                await db.SetCommand(
                    builderFactory
                        .InsertInto(AclHelper.AclSection, "ID", "RuleID", "RoleID", "RowID")
                        .Select().C("acl", "ID", "RuleID").P("RoleID").NewGuid()
                        .From(AclHelper.AclSection, "acl").NoLock()
                        .InnerJoin("DocumentCommonInfo", "i").NoLock()
                            .On().C("i", "ID").Equals().C("acl", "ID")
                        .Where().C("RoleID").Equals().P("ParentRoleID")
                            .And().C("i", isDocType ? "DocTypeID" : "CardTypeID").Equals().P("TypeID")
                        .Build(),
                    db.Parameter("RoleID", context.NestedRole.ID),
                    db.Parameter("ParentRoleID", context.NestedRole.ParentID),
                    db.Parameter("TypeID", context.NestedRole.ContextID))
                    .SetCommandTimeout(600)
                    .LogCommand()
                    .ExecuteNonQueryAsync(context.CancellationToken);
            }
        }

        #endregion
    }
}
