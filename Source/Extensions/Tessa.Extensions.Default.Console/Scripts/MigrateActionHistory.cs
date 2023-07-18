#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(MigrateActionHistory))]
    public sealed class MigrateActionHistory :
        ServerConsoleScriptBase
    {
        #region Private Fields

        private const string ActionHistoryTable = "ActionHistory";

        private const string FromParamName = "FromDate";

        private const string ToParamName = "ToDate";

        private const string RowIDFieldName = "RowID";

        private const string ModifiedFieldName = "Modified";

        private const string RequestFieldName = "Request";

        private static readonly Dictionary<string, DataType> columns = new()
        {
            ["ID"] = DataType.Guid,
            [RowIDFieldName] = DataType.Guid,
            ["ActionID"] = DataType.Int16,
            ["TypeID"] = DataType.Guid,
            ["TypeCaption"] = DataType.NVarChar,
            ["Digest"] = DataType.NVarChar,
            [ModifiedFieldName] = DataType.DateTime,
            ["ModifiedByID"] = DataType.Guid,
            ["ModifiedByName"] = DataType.NVarChar,
            ["SessionID"] = DataType.Guid,
            ["ApplicationID"] = DataType.Guid,
            [RequestFieldName] = DataType.BinaryJson,
        };

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            DateTime? from = default;
            string? fromString = this.TryGetParameter("from");
            if (!string.IsNullOrEmpty(fromString))
            {
                if (!DateTime.TryParseExact(fromString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fromDateTime))
                {
                    await this.Logger.ErrorAsync("Pass \"from\" parameter in the form yyyy-MM-dd, i.e.: -pp:from=2022-12-31");
                    this.Result = -1;
                    return;
                }

                from = DateTime.SpecifyKind(fromDateTime, DateTimeKind.Utc);
            }

            DateTime? to = default;
            string? toString = this.TryGetParameter("to");
            if (!string.IsNullOrEmpty(toString))
            {
                if (!DateTime.TryParseExact(toString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var toDateTime))
                {
                    await this.Logger.ErrorAsync("Pass \"to\" parameter in the form yyyy-MM-dd, i.e.: -pp:to=2022-12-31");
                    this.Result = -1;
                    return;
                }

                to = DateTime.SpecifyKind(toDateTime, DateTimeKind.Utc).AddDays(1);
            }

            string? target = this.TryGetParameter("target");
            if (string.IsNullOrEmpty(target))
            {
                target = "migration";
            }

            if (string.IsNullOrEmpty(this.Options.ConfigurationString)
                && target.Equals(RuntimeHelper.DefaultConfigurationString, StringComparison.OrdinalIgnoreCase)
                || string.Equals(this.Options.ConfigurationString, target, StringComparison.OrdinalIgnoreCase))
            {
                await this.Logger.ErrorAsync("Source and target configuration strings must have different values");
                this.Result = -1;
                return;
            }

            await this.ProcessAsync(target, from, to, cancellationToken);
        }

        /// <inheritdoc/>
        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Copies action history records, starting from the oldest record, from one database to another." +
                " If the target database is not configured, use the command \"tadmin UpdateActionHistory\".");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("[-pp:target=NAME] - Specifies the connection string to the target database to where action history is copied to. \"migration\" if unspecified.");
            await this.Logger.WriteLineAsync("[-pp:from=yyyy-MM-dd] - Specifies date in the form yyyy-MM-dd (example: 2022-12-31), from which to start the migration.");
            await this.Logger.WriteLineAsync("[-pp:to=yyyy-MM-dd] - Specifies date in the form yyyy-MM-dd, from which to stop the migration.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Examples:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(MigrateActionHistory)}" +
                " -cs:default -pp:target=targetName -pp:from=2022-12-13");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(MigrateActionHistory)}" +
                " -pp:target=targetName -pp:from=2022-12-12 -pp:to=2022-12-24 -pp:nowarn");
        }

        #endregion

        #region Private Methods

        private async ValueTask ProcessAsync(
            string target,
            DateTime? from,
            DateTime? to,
            CancellationToken cancellationToken = default)
        {
            await using DbManager fromDb = await this.CreateDbManagerAsync(cancellationToken);

            Dbms fromDbms = fromDb.GetDbms();
            IQueryBuilderFactory fromBuilderFactory = new QueryBuilderFactory(fromDbms);

            await using var toDb = await ConsoleAppHelper.CreateDbManagerAsync(
                this.Logger,
                target,
                null,
                cancellationToken);

            Dbms toDbms = toDb.GetDbms();
            IDbmsErrorCodeProvider? errorCodeProvider = this.Container.TryResolve<IDbmsErrorCodeProvider>(toDbms.ToString());
            IQueryBuilderFactory toBuilderFactory = new QueryBuilderFactory(toDbms);

            var keys = columns.Keys
                .Where(x => !string.Equals(x, RequestFieldName, StringComparison.OrdinalIgnoreCase))
                .Concat(new[] { RequestFieldName })
                .ToArray();

            fromDb
                .SetCommand(
                    GetSelectQuery(fromBuilderFactory, from, to, keys),
                    fromDb.Parameter(FromParamName, from),
                    fromDb.Parameter(ToParamName, to))
                .LogCommand()
                .WithoutTimeout();

            var parameters = columns
                .Select(c => toDb.Parameter(c.Key, c.Value))
                .ToArray();

            toDb
                .SetCommand(
                    toBuilderFactory
                        .InsertInto(ActionHistoryTable, keys)
                        .Values(v => v.P(keys))
                        .If(Dbms.PostgreSql, b => b.AppendLineIfRequired().Q("ON CONFLICT DO NOTHING")).EndIf()
                        .Build(),
                    parameters)
                .LogCommand()
                .WithoutTimeout();

            await using var reader = await fromDb.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            int successCount = 0;
            while (await reader.ReadAsync(cancellationToken))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    parameters[i].Value = i == reader.FieldCount - 1
                        ? await reader.GetSequentialStringAsync(i, cancellationToken)
                        : reader.GetValue(i);
                }

                int count = 0;
                try
                {
                    count = await toDb.ExecuteNonQueryAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (errorCodeProvider is null)
                    {
                        throw;
                    }

                    // Если при вставке получили исключение нарушения PK - игнорируем строку, иначе кидаем исключение
                    DbmsErrorCode errorCode = await errorCodeProvider.GetErrorCodeAsync(ex, cancellationToken);
                    if (errorCode != DbmsErrorCode.UniqueViolation)
                    {
                        throw;
                    }
                }

                if (count == 1)
                {
                    successCount++;
                }
            }

            if (successCount > 0)
            {
                await this.Logger.InfoAsync($"Rows has been inserted: {successCount}");
            }
        }

        private static string GetSelectQuery(
            IQueryBuilderFactory builderFactory,
            DateTime? from,
            DateTime? to,
            string[] keys)
        {
            // подготовка запроса для чтения данных, не используем грязное чтение для одновременной записи
            // в ту же таблицу на MSSQL, чтобы не было артефактов при чтении из-за параллельных транзакций

            var builder = builderFactory
                .Select().C(null, keys)
                .From(ActionHistoryTable);

            if (from.HasValue
                || to.HasValue)
            {
                builder = builder.Where();

                if (from.HasValue)
                {
                    builder = builder.C(ModifiedFieldName).GreaterOrEquals().P(FromParamName);

                    if (to.HasValue)
                    {
                        builder = builder.And();
                    }
                }

                if (to.HasValue)
                {
                    builder = builder.C(ModifiedFieldName).LessOrEquals().P(ToParamName);
                }
            }

            builder = builder.OrderBy(ModifiedFieldName, SortOrder.Ascending);

            return builder.Build();
        }

        #endregion
    }
}
