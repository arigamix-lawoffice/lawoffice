using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Properties.Resharper;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ConvertBson))]
    public sealed class ConvertBson :
        ServerConsoleScriptBase
    {
        #region Private Classes

        private sealed class TableSource
        {
            #region Constructors

            public TableSource(string tableName, IEnumerable<string> columnNames)
            {
                this.TableName = tableName;
                this.ColumnNames = columnNames.ToArray();
            }

            #endregion

            #region Properties

            public string TableName { get; }

            public string[] ColumnNames { get; }

            #endregion

            #region Base Overrides

            public override string ToString() =>
                this.TableName + "." + string.Join(',', this.ColumnNames);

            #endregion
        }

        #endregion

        #region Private Methods

        private static TableSource TryParseTableSource(string tableAndColumns)
        {
            int dot = tableAndColumns.IndexOf('.', StringComparison.Ordinal);
            if (dot <= 0 || dot == tableAndColumns.Length - 1)
            {
                return null;
            }

            string columns = tableAndColumns[(dot + 1)..];
            string[] columnsArray = columns.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (columnsArray.Length == 0)
            {
                return null;
            }

            string tableName = tableAndColumns[..dot];
            return new TableSource(tableName, columnsArray);
        }


        private async Task ProcessAsync(
            TableSource fromSource,
            TableSource toSource,
            [CanBeNull] SchemeTable toTable,
            string[] keys,
            bool noWarn,
            CancellationToken cancellationToken)
        {
            // открытие соединений для чтения и записи данных
            await using var fromDb = await this.CreateDbManagerAsync(cancellationToken);
            Dbms dbms = fromDb.GetDbms();
            IQueryBuilderFactory builderFactory = new QueryBuilderFactory(dbms);

            await using var toDb = await this.CreateDbManagerAsync(cancellationToken);

            // подготовка запроса для чтения данных, не используем грязное чтение для одновременной записи
            // в ту же таблицу на MSSQL, чтобы не было артефактов при чтении из-за параллельных транзакций
            fromDb
                .SetCommand(
                    builderFactory
                        .Select().C(null, fromSource.ColumnNames).C(null, keys)
                        .From(fromSource.TableName)
                        .Build())
                .LogCommand()
                .WithoutTimeout();

            // подготовка запроса для записи данных
            var builder = builderFactory
                .Update(toSource.TableName);

            foreach (string columnName in toSource.ColumnNames)
            {
                builder
                    .C(columnName).Equals().P(columnName);
            }

            builder
                .Where();

            bool first = true;
            foreach (string keyName in keys)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder
                        .And();
                }

                builder
                    .C(keyName).Equals().P(keyName);
            }

            int binaryColumnsCount = toSource.ColumnNames.Length;
            var parameters = new DataParameter[binaryColumnsCount + keys.Length];

            for (int i = 0; i < binaryColumnsCount; i++)
            {
                string columnName = toSource.ColumnNames[i];
                DataType? dataType = toTable is null
                    ? SqlHelper.TryParseDataType(await toDb.SelectColumnTypeAsync(builderFactory, toSource.TableName, columnName), dbms)
                    : toTable.Columns[columnName].Type.DbType.ToDataType();

                parameters[i] = dataType.HasValue
                    ? toDb.Parameter(columnName, dataType.Value)
                    : toDb.Parameter(columnName);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                string columnName = keys[i];
                DataType? dataType = toTable is null
                    ? SqlHelper.TryParseDataType(await toDb.SelectColumnTypeAsync(builderFactory, toSource.TableName, columnName), dbms)
                    : toTable.Columns[columnName].Type.DbType.ToDataType();

                parameters[i + binaryColumnsCount] = dataType.HasValue
                    ? toDb.Parameter(columnName, dataType.Value)
                    : toDb.Parameter(columnName);
            }

            toDb
                .SetCommand(builder.Build(), parameters)
                .WithoutTimeout();

            // выполняются запросы по конвертации данных
            int successCount = 0;
            await using var reader = await fromDb.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i < binaryColumnsCount)
                    {
                        // колонка с данными
                        byte[] bson = await reader.GetSequentialNullableBytesAsync(i, cancellationToken);

                        Dictionary<string, object> storage =
                            bson?.Length switch
                            {
                                null => null,
                                0 => new Dictionary<string, object>(),
                                _ => bson.ToSerializable().GetStorage(),
                            };

                        string typedJson = storage is null
                            ? null
                            : StorageHelper.SerializeToTypedJson(storage, indented: false);

                        parameters[i].Value = typedJson;
                    }
                    else
                    {
                        // ключевая колонка - переносим как есть
                        object value = reader.GetValue(i);
                        parameters[i].Value = value;
                    }
                }

                int count = await toDb.ExecuteNonQueryAsync(cancellationToken);
                if (count == 1)
                {
                    successCount++;
                }
                else if (!noWarn)
                {
                    await this.Logger.InfoAsync(
                        $"Query expected to update one row but updated {count} rows.{Environment.NewLine}{toDb.GetCommandTextWithParameters()}{Environment.NewLine}");
                }
            }

            if (successCount > 0)
            {
                await this.Logger.InfoAsync($"Rows has been updated: {successCount}");
            }
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            string from = this.TryGetParameter("from");
            if (string.IsNullOrEmpty(from))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"from\" parameter specifying table and column(s) names to get bson data from" +
                    ", i.e.: -pp:from=OperationsBson.Request,Response");
                this.Result = -1;
                return;
            }

            TableSource fromSource = TryParseTableSource(from);
            if (fromSource is null)
            {
                await this.Logger.ErrorAsync(
                    "Can't parse column and table names using \"from\" parameter: " + from);
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table and column(s) names to put bson data to" +
                    ", i.e.: -pp:to=OperationsJson.Request,Response");
                this.Result = -2;
                return;
            }

            TableSource toSource = TryParseTableSource(to);
            if (toSource is null)
            {
                await this.Logger.ErrorAsync($"Can't parse column and table names using \"to\" parameter: \"{to}\".");
                this.Result = -2;
                return;
            }

            // признак того, что блокируется вывод в консоль предупреждений о том, что в целевой таблице не найдена строка из исходной таблицы
            bool noWarn = this.ParameterIsNullOrEmpty("nowarn");

            // было проверено, что для одной и той же таблицы не происходит блокировок на MS и PG несмотря на то,
            // что в одном коннекшене выполняется SELECT из таблицы, и в другом коннекшене - UPDATE на те же строки таблицы;
            // поэтому в MS важен WITH (NOLOCK)

            if (fromSource.ColumnNames.Length != toSource.ColumnNames.Length)
            {
                await this.Logger.ErrorAsync($"Invalid columns count: \"{string.Join(',', toSource.ColumnNames)}\".");
                this.Result = -3;
                return;
            }

            var schemeService = this.Container.Resolve<ISchemeService>();

            var fromTable = await schemeService.GetTableAsync(fromSource.TableName, cancellationToken);
            if (fromTable is null)
            {
                await this.Logger.ErrorAsync($"Can't find table specified in \"from\" parameter: \"{fromSource.TableName}\".");
                this.Result = -4;
                return;
            }

            foreach (string columnName in fromSource.ColumnNames)
            {
                if (!fromTable.Columns.TryGetItem(columnName, out SchemeColumn column))
                {
                    await this.Logger.ErrorAsync($"Table \"{fromSource.TableName}\" doesn't contain binary column \"{columnName}\".");
                    this.Result = -4;
                    return;
                }

                if (column.Type.DbType != SchemeDbType.Binary)
                {
                    await this.Logger.ErrorAsync(
                        $"Table \"{fromSource.TableName}\" has column \"{columnName}\"" +
                        $" that is expected to be binary but it has invalid type \"{column.Type}\".");
                    this.Result = -4;
                    return;
                }
            }

            var toTable = await schemeService.GetTableAsync(toSource.TableName, cancellationToken);
            if (toTable != null)
            {
                foreach (string columnName in toSource.ColumnNames)
                {
                    if (!toTable.Columns.TryGetItem(columnName, out SchemeColumn column))
                    {
                        await this.Logger.ErrorAsync($"Table \"{toSource.TableName}\" doesn't contain json column \"{columnName}\"");
                        this.Result = -5;
                        return;
                    }

                    var dbType = column.Type.DbType;
                    if (dbType != SchemeDbType.Json
                        && dbType != SchemeDbType.BinaryJson
                        && dbType != SchemeDbType.String
                        && dbType != SchemeDbType.AnsiString)
                    {
                        await this.Logger.ErrorAsync(
                            $"Table \"{toSource.TableName}\" has column \"{columnName}\" that is expected" +
                            $" to be json compatible but it has invalid type \"{column.Type}\".");
                        this.Result = -5;
                        return;
                    }
                }
            }

            string[] keys = null;

            string key = this.TryGetParameter("key");
            if (!string.IsNullOrEmpty(key))
            {
                keys = key.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }

            if (keys is null || keys.Length == 0)
            {
                keys = fromTable.Constraints.PrimaryKey.Columns.Select(x => x.Column.Name).ToArray();

                if (toTable != null)
                {
                    string[] targetKeys = toTable.Constraints.PrimaryKey.Columns.Select(x => x.Column.Name).ToArray();
                    if (!keys.SequenceEqual(targetKeys))
                    {
                        await this.Logger.ErrorAsync(
                            $"Tables \"{fromSource.TableName}\" and \"{toSource.TableName}\" have varied primary keys:" +
                            $" \"{string.Join(',', keys)}\" and \"{string.Join(',', targetKeys)}\".");
                        this.Result = -6;
                        return;
                    }
                }
            }
            else
            {
                foreach (string columnName in keys)
                {
                    if (!fromTable.Columns.Contains(columnName))
                    {
                        await this.Logger.ErrorAsync($"Table \"{fromSource.TableName}\" doesn't contain key column \"{columnName}\".");
                        this.Result = -7;
                        return;
                    }

                    if (toTable != null
                        && !toTable.Columns.Contains(columnName))
                    {
                        await this.Logger.ErrorAsync($"Table \"{toSource.TableName}\" doesn't contain key column \"{columnName}\".");
                        this.Result = -7;
                        return;
                    }
                }
            }

            await this.ProcessAsync(fromSource, toSource, toTable, keys, noWarn, cancellationToken);
        }


        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Converts binary columns with bson-serialized data into text columns with typed json-data." +
                " Scheme should be updated to current version for tadmin prior to executing the script. Use \"tadmin SchemeUpdateSql\" command in order to do it.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=Table1.Column1[,Column2...] - Specifies table with one or several binary columns containing bson-data" +
                " (Binary type in scheme). Table should be present in scheme.");
            await this.Logger.WriteLineAsync("-pp:to=Table2.Column3[,Column4...] - Specifies table with one or several json columns" +
                " (String, AnsiString, Json or BinaryJson type in scheme). Columns should be specified in same order as in \"from\" parameter." +
                " Table can be absent in scheme, for example, if that table has been created in migration script.");
            await this.Logger.WriteLineAsync("[-pp:key=Column5[,Column6...]] - Specifies column(s) to use as a primary key for searching a row in both" +
                " \"from\" and \"to\" tables. Optional parameter. If absent, then columns from the primary key of \"from\" table are being used.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings when \"from\" table contains rows that are absent in \"to\" table.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Examples:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertBson)}" +
                " -pp:from=ActionHistory.Request -pp:to=_ActionHistory.Request");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertBson)}" +
                " -pp:from=Operations.Request,Response -pp:to=OperationsJson.Request,Response -pp:key=RowID,ID -pp:nowarn");
        }

        #endregion
    }
}