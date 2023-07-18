using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(MigrateTaskHistoryRoles))]
    public class MigrateTaskHistoryRoles :
        ServerConsoleScriptBase
    {
        #region Fields

        private static readonly SchemeDbType[] SchemeTypes = new[] { SchemeDbType.Guid, SchemeDbType.String, SchemeDbType.BinaryJson };

        private static readonly Regex ContextRoleNameRegex = new(".+\\((.+)\\)$", RegexOptions.Compiled);

        #endregion

        #region Private Methods

        private async ValueTask ProcessAsync(
            string tableName,
            CancellationToken cancellationToken)
        {
            const int bulkSize = 1000;

            // открытие соединений для чтения и записи данных
            await using var fromDb = await this.CreateDbManagerAsync(cancellationToken);
            await using var toDb = await this.CreateDbManagerAsync(cancellationToken);
            await using var contextRoleCheckDb = await this.CreateDbManagerAsync(cancellationToken);
            var bulkInsertExecutor = this.Container.Resolve<IBulkInsertExecutor>();

            IQueryBuilderFactory builderFactory = new QueryBuilderFactory(fromDb.GetDbms());

            fromDb.SetCommand(builderFactory
                .Select()
                    .C("th", "RowID", "ProcessID", "ProcessKind", "ProcessName", "RoleID", "RoleName", "RoleTypeID")
                .From("TaskHistory", "th").NoLock()
                .Build())
                .LogCommand();

            var bulkInsertParameters = bulkInsertExecutor.PrepareBulkInsert(
                toDb,
                builderFactory,
                tableName,
                new[] { "RowID", "RoleName", "Settings" },
                SchemeTypes);

            Dictionary<string, object> settings = new Dictionary<string, object>(StringComparer.Ordinal);
            await using var reader = await fromDb.ExecuteReaderAsync(cancellationToken);
            object[][] parameters = new object[3][];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object[bulkSize];
            }

            int count = 0;
            Dictionary<string, Guid?> contextRoleDictionary = null;
            string performerName = null;
            while (await reader.ReadAsync(cancellationToken))
            {
                var rowID = reader.GetGuid(0);
                var processID = reader.GetNullableGuid(1);
                if (processID is not null)
                {
                    settings[".ProcessID"] = processID;
                    settings[".ProcessKind"] = reader.GetString(2);
                    settings[".ProcessName"] = reader.GetString(3);
                }

                // Помимо обычного переноса данных в Settings также восстанавливаем идентификатор контекстной роли из имени контексной роли для починки связи с визуализатором ТК.
                var performerID = reader.GetNullableGuid(4);
                if (performerID is not null)
                {
                    performerName = reader.GetString(5);
                    Guid performerTypeID = reader.GetGuid(6);

                    // Если роль - контекстная и содержит скобки с потенциальным именем контексной роли, то пытаемся её найти.
                    // Если имя роли уже было изменено, то связь, увы, уже была потеряна ещё в 3.6 и такую визуализацию не получится восстановить автоматически.
                    if (performerTypeID == RoleHelper.ContextRoleTypeID
                        && ContextRoleNameRegex.Match(performerName) is { Success: true } match)
                    {
                        contextRoleDictionary ??= new Dictionary<string, Guid?>(StringComparer.OrdinalIgnoreCase);
                        var contextRoleName = match.Groups[1].Value;

                        if (!contextRoleDictionary.TryGetValue(contextRoleName, out var contextRoleID))
                        {
                            contextRoleID = await this.TryGetContextRoleByNameAsync(contextRoleCheckDb, builderFactory, contextRoleName, cancellationToken);
                            contextRoleDictionary[contextRoleName] = contextRoleID;
                        }

                        if (contextRoleID is not null)
                        {
                            performerID = contextRoleID;
                            performerName = contextRoleName;
                        }
                    }

                    settings[".PerformerID"] = performerID;
                    settings[".PerformerName"] = performerName;
                    settings[".PerformerRoleTypeID"] = performerTypeID;
                }

                if (settings.Count > 0)
                {
                    parameters[0][count] = rowID;
                    parameters[1][count] = performerName;
                    parameters[2][count] = StorageHelper.SerializeToTypedJson(settings);
                    count++;

                    settings.Clear();

                    if (count == bulkSize)
                    {
                        await bulkInsertExecutor.BulkInsertAsync(
                            bulkInsertParameters,
                            count,
                            parameters,
                            cancellationToken);
                        count = 0;
                    }
                }
            }

            // Догружаем остаток
            if (count > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Array.Resize(ref parameters[i], count);
                }
                await bulkInsertExecutor.BulkInsertAsync(
                    bulkInsertParameters,
                    count,
                    parameters,
                    cancellationToken);
            }
        }

        private Task<Guid?> TryGetContextRoleByNameAsync(DbManager db, IQueryBuilderFactory builderFactory, string contextRoleName, CancellationToken cancellationToken)
        {
            return db.SetCommand(
                builderFactory
                    .Cached(nameof(this.TryGetContextRoleByNameAsync), b => b
                        .Select().Top(1).C("ID").From("Roles").NoLock()
                        .Where().C("Name").Equals().P("Name")
                            .And().C("TypeID").Equals().V((int) RoleType.Context)
                        .Limit(1)
                        .Build()),
                db.Parameter("Name", contextRoleName, LinqToDB.DataType.NVarChar))
                .LogCommand()
                .ExecuteAsync<Guid?>(cancellationToken);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Migrate information about task history roles to task history settings." +
                " TESSA scheme should be updated to current version for tadmin prior to executing the script. Use \"tadmin SchemeUpdateSql\" command in order to do it.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:to=Table - Specifies table to store task history settings data." +
                " Table should contains \"RowID\" column with Guid type, \"RoleName\" column with string type and \"Settings\" column with JSON type (nvarchar(max) in SQL Server or jsonb in Postgresql)." +
                " Table can be absent in scheme, for example, if that table has been created in migration script.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Examples:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(MigrateTaskHistoryRoles)} --pp:to=_TaskHistory");
        }

        /// <inheritdoc/>
        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            string table = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(table))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table to store task history settings data." +
                    ", i.e.: -pp:to=_TaskHistory");
                this.Result = -1;
                return;
            }

            await this.ProcessAsync(table, cancellationToken);
        }

        #endregion
    }
}
