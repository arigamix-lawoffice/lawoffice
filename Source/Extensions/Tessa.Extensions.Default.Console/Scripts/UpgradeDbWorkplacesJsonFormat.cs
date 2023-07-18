using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using NLog;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;
using Tessa.Views.Workplaces.Json.Metadata;

namespace Tessa.Extensions.Default.Console.Scripts
{
    /// <summary>
    /// Скрипт для апгрейда версии JSON рабочего места в БД.
    /// </summary>
    [ConsoleScript(nameof(UpgradeDbWorkplacesJsonFormat))]
    public sealed class UpgradeDbWorkplacesJsonFormat :
        ServerConsoleScriptBase
    {
        #region Fields

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            // открытие соединений для чтения и записи данных
            await using var dbReader = await this.CreateDbManagerAsync(cancellationToken);
            await using var dbWriter = await this.CreateDbManagerAsync(cancellationToken);
            var readerBuilderFactory = new QueryBuilderFactory(dbReader.GetDbms());
            var writerBuilderFactory = new QueryBuilderFactory(dbWriter.GetDbms());

            dbReader
                .SetCommand(readerBuilderFactory
                    .Select()
                    .C("w", "ID", "Metadata")
                    .From("Workplaces", "w")
                    .NoLock()
                    .Build())
                .LogCommand()
                .WithoutTimeout();

            await using var reader = await dbReader.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                var id = reader.GetGuid(0);
                var metadata = await reader.GetSequentialNullableStringAsync(1, cancellationToken);
                JsonWorkplaceMetadata jsonWorkplace = null;
                try
                {
                    jsonWorkplace = metadata.FromJsonString<JsonWorkplaceMetadata>();
                }
                catch (Exception)
                {
                    await this.Logger.WriteLineAsync($"Workplace with ID {id} is not in JSON format.");
                    continue;
                }
                var jsonFormat = jsonWorkplace.FormatVersion;
                if (jsonFormat < 2)
                {
                    await this.Logger.WriteLineAsync($"Upgrading JSON format metadata for workplace with ID {id}.");

                    var order = jsonWorkplace.OrderPos;
                    var updateSqlText = writerBuilderFactory
                               .Update("Workplaces")
                               .C("Order").Equals().P("Order")
                               .C("Metadata").Equals().P("Metadata")
                               .Where().C("ID").Equals().P("ID")
                               .Build();

                    var parameters = new DataParameter[3];
                    parameters[0] = dbWriter.Parameter("ID", DataType.Guid);
                    parameters[1] = dbWriter.Parameter("Metadata", DataType.BinaryJson);
                    parameters[2] = dbWriter.Parameter("Order", DataType.Int32);
                    jsonWorkplace.FormatVersion = TessaJsonSerializationContext.WorkplaceJsonVersion;
                    parameters[0].Value = id;
                    parameters[1].Value = jsonWorkplace.ToJsonString(indented: false);
                    parameters[2].Value = order;
                    dbWriter
                        .SetCommand(updateSqlText, parameters)
                        .LogCommand();
                    await dbWriter.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Upgrades workplace metadata JSON format in the table 'Workplaces'. No params required.");
        }

        #endregion
    }
}
