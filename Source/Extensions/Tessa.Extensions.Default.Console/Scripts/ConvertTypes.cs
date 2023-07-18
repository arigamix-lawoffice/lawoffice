using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using NLog;
using Tessa.Cards;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ConvertTypes))]
    public class ConvertTypes :
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

        #region Fields

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Private Methods

        private async Task ProcessAsync(
            TableSource tableFrom,
            TableSource tableTo,
            bool noWarn,
            CancellationToken cancellationToken)
        {
            // открытие соединений для чтения и записи данных
            await using var dbReader = await this.CreateDbManagerAsync(cancellationToken);
            await using var dbWriter = await this.CreateDbManagerAsync(cancellationToken);
            IQueryBuilderFactory readerBuilderFactory = new QueryBuilderFactory(dbReader.GetDbms());
            IQueryBuilderFactory writerBuilderFactory = new QueryBuilderFactory(dbWriter.GetDbms());

            IQueryBuilder updateBuilder = writerBuilderFactory
                .Update(tableTo.TableName);

            for (int i = 1; i < tableTo.ColumnNames.Length; i++)
            {
                updateBuilder
                    .C(tableTo.ColumnNames[i]).Equals().P(tableTo.ColumnNames[i]);
            }

            string updateSqlText = updateBuilder
                .Where().C(tableTo.ColumnNames[0]).Equals().P(tableTo.ColumnNames[0])
                .Build();

            var parameters = new DataParameter[5];
            parameters[0] = dbWriter.Parameter(tableTo.ColumnNames[0], DataType.Guid);
            parameters[1] = dbWriter.Parameter(tableTo.ColumnNames[1], DataType.NVarChar);
            parameters[2] = dbWriter.Parameter(tableTo.ColumnNames[2], DataType.Int32);
            parameters[3] = dbWriter.Parameter(tableTo.ColumnNames[3], DataType.Int64);
            parameters[4] = dbWriter.Parameter(tableTo.ColumnNames[4], DataType.BinaryJson);

            // подготовка запроса для чтения данных, не используем грязное чтение для одновременной записи
            // в ту же таблицу на MSSQL, чтобы не было артефактов при чтении из-за параллельных транзакций
            dbReader
                .SetCommand(readerBuilderFactory
                    .Select().C(null, tableFrom.ColumnNames)
                    .From(tableFrom.TableName)
                    .Build())
                .LogCommand()
                .WithoutTimeout();

            // выполняются запросы по конвертации данных
            await using var reader = await dbReader.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                Guid id = reader.GetGuid(0);

                // GetSequentialNullableStringAsync нельзя использовать, т.к. он не работает для типа XML в MSSQL
                string metadata = reader.GetNullableString(1);

                if (string.IsNullOrWhiteSpace(metadata))
                {
                    continue;
                }
                
                logger.Trace("Converting type ID={0:D}", id);

                try
                {
                    // конвертируем метаданные
                    var cardType = new CardType();
                    await cardType.DeserializeFromXmlAsync(metadata, cancellationToken);

                    string json = await cardType.SerializeToJsonAsync(cancellationToken: cancellationToken);

                    // записываем сконвертированные метаданные
                    parameters[0].Value = id;
                    parameters[1].Value = SqlHelper.LimitString(cardType.Group, CardHelper.CardTypeGroupMaxLength);
                    parameters[2].Value = (int) cardType.InstanceType;
                    parameters[3].Value = (long) cardType.Flags;
                    parameters[4].Value = json;

                    int count = await dbWriter
                        .SetCommand(updateSqlText, parameters)
                        .LogCommand()
                        .ExecuteNonQueryAsync(cancellationToken);

                    if (count != 1 && !noWarn)
                    {
                        await this.Logger.InfoAsync(
                            $"Query expected to update one row but updated {count} rows.{Environment.NewLine}{dbWriter.GetCommandTextWithParameters()}{Environment.NewLine}");
                    }
                }
                catch (Exception)
                {
                    await this.Logger.ErrorAsync($"Error while converting type ID={id:D}. Metadata:{Environment.NewLine}{metadata}");
                    throw;
                }
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
                    "Pass \"from\" parameter specifying table to convert metadata from" +
                    ", i.e.: -pp:from=_Types");
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table to convert metadata to" +
                    ", i.e.: -pp:to=_TypesTo");
                this.Result = -2;
                return;
            }

            TableSource fromTableSource = new(from, new List<string>
            {
                "ID", "Definition"
            });

            TableSource toTableSource = new(to, new List<string>
            {
                "ID", "Group", "InstanceTypeID", "Flags", "Metadata"
            });

            // признак того, что блокируется вывод в консоль предупреждений о том, что в целевой таблице не найдена строка из исходной таблицы
            bool noWarn = this.ParameterIsNullOrEmpty("nowarn");

            await this.ProcessAsync(fromTableSource, toTableSource, noWarn, cancellationToken);
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Converts types metadata from xml format into typed json format.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=_Types - Specifies table with appropriate types metadata in xml format. \"Definition\" column is used.");
            await this.Logger.WriteLineAsync("-pp:to=_TypesTo - Specifies table to write converted metadata to. \"Metadata\" column is used.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings when \"from\" table contains rows that are absent in \"to\" table.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertTypes)}" +
                " -pp:from=_Types -pp:to=_TypesTo -pp:nowarn");
        }

        #endregion
    }
}
