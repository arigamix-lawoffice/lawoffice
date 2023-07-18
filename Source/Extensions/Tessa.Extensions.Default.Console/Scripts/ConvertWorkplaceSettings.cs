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
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Tessa.Views.Workplaces.Json.Converters;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ConvertWorkplaceSettings))]
    public sealed class ConvertWorkplaceSettings :
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

            var parser = this.Container.Resolve<LexemeParser>();
            var syntaxNodeConverter = this.Container.Resolve<ISyntaxNodeConverter<IWorkplaceSyntaxNode>>();
            var contextFactory = this.Container.Resolve<Func<IWorkplaceEvaluatingContext>>();
            var converter = this.Container.Resolve<JsonWorkplaceUserExtensionMetadataConverter>();

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
                var id = reader.GetGuid(0);
                var settings = await reader.GetSequentialNullableStringAsync(1, cancellationToken: cancellationToken);

                if (string.IsNullOrWhiteSpace(settings))
                {
                    continue;
                }
                
                logger.Trace("Converting workplace settings ID={0:D}", id);

                try
                {
                    // конвертируем метаданные
                    var context = contextFactory();
                    var lexemes = parser.Parse(settings);
                    var nodes = syntaxNodeConverter.Convert(lexemes, SyntaxConverterOptions.WorkplaceConvertingOptions);
                    foreach (var node in nodes)
                    {
                        await node.InterpretAsync(context, null, cancellationToken);
                    }

                    var resultSettings = context.ResultSettings;
                    var jsonSettings = await converter.ConvertBackAsync(resultSettings);
                    var jsonString = jsonSettings.ToJsonString(indented: false);

                    // выполнение запроса для записи данных
                    parameters[0].Value = id;
                    parameters[1].Value = jsonString;

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
                    await this.Logger.LogExceptionAsync($"Error while converting workplace settings ID={id:D}. Settings:{Environment.NewLine}{settings}", ex);
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
                    ", i.e.: -pp:from=_PersonalRoleSatellite");
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table to convert metadata to" +
                    ", i.e.: -pp:to=_PersonalRoleSatelliteTo");
                this.Result = -2;
                return;
            }

            TableSource fromTableSource = new(from, new List<string>
            {
                "ID", "WorkplaceExtensionsLegacy"
            });

            TableSource toTableSource = new(to, new List<string>
            {
                "ID", "WorkplaceExtensions"
            });

            // признак того, что блокируется вывод в консоль предупреждений о том, что в целевой таблице не найдена строка из исходной таблицы
            bool noWarn = this.ParameterIsNullOrEmpty("nowarn");

            await this.ProcessAsync(fromTableSource, toTableSource, noWarn, cancellationToken);
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Converts workplace settings from exchange format into typed json format.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=_PersonalRoleSatellite - Specifies table with appropriate workplace settings metadata in exchange format." +
                " \"WorkplaceExtensionsLegacy\" column is used.");
            await this.Logger.WriteLineAsync("-pp:to=_PersonalRoleSatelliteTo - Specifies table to write converted metadata to. \"WorkplaceExtensions\" column is used.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings when \"from\" table contains rows that are absent in \"to\" table.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertWorkplaceSettings)}" +
                " -pp:from=_PersonalRoleSatellite -pp:to=_PersonalRoleSatelliteTo -pp:nowarn");
        }

        #endregion
    }
}
