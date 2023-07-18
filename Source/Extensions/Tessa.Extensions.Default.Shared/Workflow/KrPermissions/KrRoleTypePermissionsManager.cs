using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <inheritdoc cref="IRoleTypePermissionsManager"/>
    /// <remarks>
    /// Тип может быть зарегистрирован для консольной утилиты tadmin, например, для команды tadmin MigrateFiles,
    /// где поднимается серверное API, но только для расширений Tessa.Extensions.Default.Shared.
    /// В этом случае будет отсутствовать зависимость IKrTypesCache, которая здесь помечена как [OptionalDependency]
    /// </remarks>
    public sealed class KrRoleTypePermissionsManager : IRoleTypePermissionsManager
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;
        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        public KrRoleTypePermissionsManager(
            [OptionalDependency] IKrTypesCache krTypesCache = null,
            [OptionalDependency] IDbScope dbScope = null)
        {
            this.krTypesCache = krTypesCache;
            this.dbScope = dbScope;
        }

        #endregion

        #region IRoleTypePermissionsManager Implementation

        /// <inheritdoc/>
        public async ValueTask<bool> RoleTypeUseCustomPermissionsAsync(Guid roleTypeID, CancellationToken cancellationToken = default)
        {
            return this.krTypesCache is not null && await KrComponentsHelper.HasBaseAsync(roleTypeID, this.krTypesCache, cancellationToken);
        }

        /// <inheritdoc/>
        public async ValueTask<bool> RoleTypeUseCustomPermissionsOnMetadataAsync(Guid roleTypeID, CancellationToken cancellationToken = default)
        {
            // если krTypesCache равен null, то мы в консольной tadmin, для которой нет смысла вычислять расширенные пермишены
            if (this.dbScope is not null && this.krTypesCache is not null)
            {
                await using (this.dbScope.Create())
                {
                    var db = this.dbScope.Db;

                    return
                        await db.SetCommand(
                            this.dbScope.BuilderFactory
                                .Select().Top(1).V(true)
                                .From("KrSettingsCardTypes").NoLock()
                                .Where().C("CardTypeID").Equals().P("RoleTypeID")
                                .Limit(1)
                                .Build(),
                            db.Parameter("RoleTypeID", roleTypeID))
                            .LogCommand()
                            .ExecuteAsync<bool>(cancellationToken).ConfigureAwait(false);
                }
            }

            return await this.RoleTypeUseCustomPermissionsAsync(roleTypeID, cancellationToken);
        }

        #endregion
    }
}
