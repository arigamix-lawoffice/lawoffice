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
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Tessa.Views.Workplaces;
using Tessa.Views.Workplaces.Json.Converters;
using Tessa.Views.Workplaces.Json.Metadata;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ConvertWorkplaces))]
    public sealed class ConvertWorkplaces :
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
            var readerBuilderFactory = new QueryBuilderFactory(dbReader.GetDbms());
            var writerBuilderFactory = new QueryBuilderFactory(dbWriter.GetDbms());

            var updateSqlText = writerBuilderFactory
                .Update(tableTo.TableName)
                .C(tableTo.ColumnNames[1]).Equals().P(tableTo.ColumnNames[1])
                .Where().C(tableTo.ColumnNames[0]).Equals().P(tableTo.ColumnNames[0])
                .Build();

            var parameters = new DataParameter[2];
            parameters[0] = dbWriter.Parameter(tableTo.ColumnNames[0], DataType.Guid);
            parameters[1] = dbWriter.Parameter(tableTo.ColumnNames[1], DataType.BinaryJson);

            var workplaceInterpreter = this.Container.Resolve<IWorkplaceInterpreter>();
            var converter = this.Container.Resolve<IConverter<IJsonWorkplaceMetadata, IWorkplaceMetadata>>();

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
                string metadata = await reader.GetSequentialNullableStringAsync(1, cancellationToken: cancellationToken);

                if (string.IsNullOrWhiteSpace(metadata))
                {
                    continue;
                }
                
                logger.Trace("Converting workplace ID={0:D}", id);

                try
                {
                    // конвертируем метаданные
                    var workplaceMetadata = await workplaceInterpreter.InterpretExchangeWorkplaceAsync(metadata, cancellationToken);
                    var jsonMetadata = await converter.ConvertBackAsync(workplaceMetadata.ResultWorkplace);
                    var typedJson = jsonMetadata.ToJsonString(indented: false);

                    // выполнение запроса для записи данных
                    parameters[0].Value = id;
                    parameters[1].Value = typedJson;

                    dbWriter
                        .SetCommand(updateSqlText, parameters)
                        .LogCommand();

                    int count = await dbWriter.ExecuteNonQueryAsync(cancellationToken);
                    if (count != 1 && !noWarn)
                    {
                        await this.Logger.InfoAsync(
                            $"Query expected to update one row but updated {count} rows.{Environment.NewLine}{dbWriter.GetCommandTextWithParameters()}{Environment.NewLine}");
                    }
                }
                catch (Exception ex)
                {
                    await this.Logger.LogExceptionAsync($"Error while converting workplace ID={id:D}. Metadata:{Environment.NewLine}{metadata}", ex);
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
                    ", i.e.: -pp:from=_Workplaces");
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table to convert metadata to" +
                    ", i.e.: -pp:to=_WorkplacesTo");
                this.Result = -2;
                return;
            }

            TableSource fromTableSource = new(from, new List<string>
            {
                "ID", "MetadataLegacy"
            });

            TableSource toTableSource = new(to, new List<string>
            {
                "ID", "Metadata"
            });

            // признак того, что блокируется вывод в консоль предупреждений о том, что в целевой таблице не найдена строка из исходной таблицы
            bool noWarn = this.ParameterIsNullOrEmpty("nowarn");

            await this.ProcessAsync(fromTableSource, toTableSource, noWarn, cancellationToken);
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Converts workplace metadata from exchange format into typed json format.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=_Workplaces - Specifies table with appropriate workplace metadata in exchange format. \"MetadataLegacy\" column is used.");
            await this.Logger.WriteLineAsync("-pp:to=_WorkplacesTo - Specifies table to write converted metadata to. \"Metadata\" column is used.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings when \"from\" table contains rows that are absent in \"to\" table.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertWorkplaces)}" +
                " -pp:from=_Workplaces -pp:to=_WorkplacesTo -pp:nowarn");
        }

        #endregion
    }
}
