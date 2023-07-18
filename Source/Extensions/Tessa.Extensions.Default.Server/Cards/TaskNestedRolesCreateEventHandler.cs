using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Roles;
using Tessa.Roles.NestedRoles;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Обработчик события создания вложенной роли, который записывает созданную вложенную роль как дополнительную роль в ФРЗ задания к основной роли.
    /// </summary>
    public sealed class TaskNestedRolesCreateEventHandler : INestedRoleEventManagerHandler
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public TaskNestedRolesCreateEventHandler(
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
            // Данный обработчик должен добавить вложенную роль как дополнительную роль в ФРЗ во все задания, где в качестве основной роли указана родительская роль.
            await using var s = this.dbScope.Create();

            var db = this.dbScope.Db;
            var builder = this.dbScope.BuilderFactory;

            var types = await this.krTypesCache.GetDocTypesAsync(context.CancellationToken);
            var isDocType = types.Any(x => x.ID == context.NestedRole.ContextID);
            await db.SetCommand(
                builder
                    .InsertInto("TaskAssignedRoles",
                        "ID", "RowID",
                        "TaskRoleID",
                        "RoleID", "RoleName", "RoleTypeID",
                        "ParentRowID")
                    .Select()
                        .C("tar", "ID")
                        .NewGuid()
                        .C("tar", "TaskRoleID")
                        .P("RoleID", "RoleName", "RoleTypeID")
                        .C("tar", "RowID")
                    .From("TaskAssignedRoles", "tar").NoLock()
                    .InnerJoin("Tasks", "t").NoLock()
                        .On().C("t", "RowID").Equals().C("tar", "ID")
                    .InnerJoin("DocumentCommonInfo", "i").NoLock()
                        .On().C("i", "ID").Equals().C("t", "ID")
                    .Where().C("tar", "RoleID").Equals().P("ParentRoleID")
                        .And().C("i", isDocType ? "DocTypeID" : "CardTypeID").Equals().P("TypeID")
                        .Build(),
                    db.Parameter("RoleID", context.NestedRole.ID),
                    db.Parameter("RoleName", context.NestedRole.Name),
                    db.Parameter("RoleTypeID", RoleHelper.NestedRoleTypeID),
                    db.Parameter("ParentRoleID", context.NestedRole.ParentID),
                    db.Parameter("TypeID", context.NestedRole.ContextID))
                    .SetCommandTimeout(600)
                    .LogCommand()
                    .ExecuteNonQueryAsync(context.CancellationToken);
        }

        #endregion
    }
}
