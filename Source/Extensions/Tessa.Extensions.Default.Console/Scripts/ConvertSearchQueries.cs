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
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;
using Tessa.Views.SearchQueries;
using Tessa.Views.Workplaces.Json.Converters;
using Tessa.Views.Workplaces.Json.Metadata;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ConvertSearchQueries))]
    public sealed class ConvertSearchQueries :
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

            var converter = this.Container.Resolve<IConverter<IJsonSearchQueryMetadata, ISearchQueryMetadata>>();
            var interpreter = this.Container.Resolve<IExchangeFormatInterpreter>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var id = reader.GetGuid(0);
                var name = reader.GetNullableString(1);
                var metadata = await reader.GetSequentialNullableStringAsync(2, cancellationToken);

                var viewAlias = reader.GetString(3);
                var isPublic = reader.GetBoolean(4);
                var lastModified = reader.GetDateTime(5);
                var createdByUserId = reader.GetGuid(6);
                var templateCompositionId = reader.GetNullableGuid(7) ?? Guid.Empty;

                logger.Trace("Converting search query ID={0:D}, ViewAlias={1}", id, viewAlias);

                try
                {
                    var sb = StringBuilderHelper.Acquire();
                    sb.AppendLine(
                        "#tessa_exchange_format(Version:1, CreatorName:Admin, CreationTime:2017-02-01T08:15:38){");
                    sb.Append(KeywordNames.ExchangeSearchQuery);
                    sb.Append('(');
                    sb.AppendFormat("ID:{0}, ", id);
                    sb.AppendFormat("Alias:{0},", TessaParserHelper.EscapeSpecialCharacters(name));
                    sb.AppendFormat("CreatedByUser:{0},", createdByUserId);
                    sb.AppendFormat("IsPublic:{0},", isPublic);
                    sb.AppendFormat("ModificationDateTime:{0:s},", lastModified);
                    sb.AppendFormat("ViewAlias:{0},", TessaParserHelper.EscapeSpecialCharacters(viewAlias));
                    sb.AppendFormat("TemplateCompositionID:{0}", templateCompositionId);
                    sb.AppendLine(")");
                    sb.AppendLine("{");
                    sb.AppendLine(metadata);
                    sb.AppendLine("}");
                    sb.Append('}');

                    var context = await interpreter.InterpretAsync(sb.ToStringAndRelease(), cancellationToken);

                    var searchQuery = context.GetSearchQueries().FirstOrDefault();
                    if (searchQuery is null)
                    {
                        await this.Logger.InfoAsync(
                            $"Unable to parse search query.{Environment.NewLine}{metadata}");
                        this.Result = -5;
                        return;
                    }

                    var jsonSearchQuery = await converter.ConvertBackAsync(searchQuery);
                    var jsonMetadata = jsonSearchQuery.ToJsonString(indented: false);

                    // выполнение запроса для записи данных
                    parameters[0].Value = id;
                    parameters[1].Value = jsonMetadata;

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
                    await this.Logger.LogExceptionAsync($"Error while converting search query ID={id:D}, ViewAlias={viewAlias}. Metadata:{Environment.NewLine}{metadata}", ex);
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
                    ", i.e.: -pp:from=_SearchQueries");
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table to convert metadata to" +
                    ", i.e.: -pp:to=_SearchQueriesTo");
                this.Result = -2;
                return;
            }

            TableSource fromTableSource = new(from, new List<string>
            {
                "ID", "Name", "MetadataLegacy", "ViewAlias", "IsPublic",
                "LastModified", "CreatedByUserID", "TemplateCompositionID"
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
            await this.Logger.WriteLineAsync("Converts search query metadata from exchange format into typed json format.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=_SearchQueries - Specifies table with appropriate search query metadata in exchange format. \"MetadataLegacy\" column is used.");
            await this.Logger.WriteLineAsync("-pp:to=_SearchQueriesTo - Specifies table to write converted metadata to. \"Metadata\" column is used.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings when \"from\" table contains rows that are absent in \"to\" table.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertSearchQueries)}" +
                " -pp:from=_SearchQueries -pp:to=_SearchQueriesTo -pp:nowarn");
        }

        #endregion
    }
}
