using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ConvertCardTypesFormatVersion))]
    public class ConvertCardTypesFormatVersion :
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

        private async Task ProcessAsync(
            CancellationToken cancellationToken)
        {
            // открытие соединений для чтения и записи данных
            await using var dbReader = await this.CreateDbManagerAsync(cancellationToken);
            await using var dbWriter = await this.CreateDbManagerAsync(cancellationToken);
            IQueryBuilderFactory readerBuilderFactory = new QueryBuilderFactory(dbReader.GetDbms());
            IQueryBuilderFactory writerBuilderFactory = new QueryBuilderFactory(dbWriter.GetDbms());

            TableSource tableSource = new("Types", new List<string>
            {
                "ID", "Metadata"
            });
            
            IQueryBuilder updateBuilder = writerBuilderFactory
                .Update(tableSource.TableName);

            for (int i = 1; i < tableSource.ColumnNames.Length; i++)
            {
                updateBuilder
                    .C(tableSource.ColumnNames[i]).Equals().P(tableSource.ColumnNames[i]);
            }

            string updateSqlText = updateBuilder
                .Where().C(tableSource.ColumnNames[0]).Equals().P(tableSource.ColumnNames[0])
                .Build();

            var parameters = new DataParameter[2];
            parameters[0] = dbWriter.Parameter(tableSource.ColumnNames[0], DataType.Guid);
            parameters[1] = dbWriter.Parameter(tableSource.ColumnNames[1], DataType.BinaryJson);

            // подготовка запроса для чтения данных, не используем грязное чтение для одновременной записи
            // в ту же таблицу на MSSQL, чтобы не было артефактов при чтении из-за параллельных транзакций
            dbReader
                .SetCommand(readerBuilderFactory
                    .Select().C(null, tableSource.ColumnNames)
                    .From(tableSource.TableName).NoLock()
                    .Build())
                .LogCommand()
                .WithoutTimeout();

            var counter = 0;

            // выполняются запросы по конвертации данных
            await using var reader = await dbReader.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                Guid id = reader.GetGuid(0);

                string metadata = await reader.GetSequentialNullableStringAsync(1, cancellationToken: cancellationToken); // GetNullableString(1)

                if (string.IsNullOrWhiteSpace(metadata))
                {
                    continue;
                }

                try
                {
                    // конвертируем метаданные
                    var cardType = await CardSerializableObject.DeserializeFromJsonAsync<CardType>(metadata, cancellationToken);

                    string json = await cardType.SerializeToJsonAsync(cancellationToken: cancellationToken);
                    // записываем сконвертированные метаданные
                    parameters[0].Value = id;
                    parameters[1].Value = json;

                    await dbWriter
                        .SetCommand(updateSqlText, parameters)
                        .LogCommand()
                        .ExecuteNonQueryAsync(cancellationToken);

                    await this.Logger.InfoAsync($"Converted type ID {id:D}");
                    counter++;
                }
                catch (Exception)
                {
                    await this.Logger.ErrorAsync($"Error while converting type ID={id:D}. Metadata:{Environment.NewLine}{metadata}");
                    throw;
                }
            }

            await this.Logger.InfoAsync($"{counter} types converted.");
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            await this.ProcessAsync(cancellationToken);
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Converts card types metadata to latest format version.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ConvertCardTypesFormatVersion)}");
        }

        #endregion
    }
}
