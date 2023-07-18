using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    /// <summary>
    /// Скрипт для апгрейда версии JSON представлений в БД.
    /// </summary>
    [ConsoleScript(nameof(UpgradeDbViewsJsonFormat))]
    public sealed class UpgradeDbViewsJsonFormat :
        ServerConsoleScriptBase
    {
        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            // открытие соединений для чтения и записи данных
            await using var dbReader = await this.CreateDbManagerAsync(cancellationToken);
            await using var dbWriter = await this.CreateDbManagerAsync(cancellationToken);
            var builderFactory = new QueryBuilderFactory(dbReader.GetDbms());
            string updateSqlText = null;
            DataParameter[] parameters = null;

            dbReader
                .SetCommand(builderFactory
                    .Select()
                    .C("v", "ID", "JsonMetadataSource")
                    .From("Views", "v").NoLock()
                    .Build())
                .LogCommand()
                .WithoutTimeout();

            await using var reader = await dbReader.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            var viewDataAccessor = this.Container.Resolve<ViewDataAccessor>();
            var viewModelConverter = this.Container.Resolve<IJsonViewModelConverter>();
            var jsonViewModelUpgrader = this.Container.Resolve<IJsonViewModelUpgrader>();

            var views = await viewDataAccessor.GetViewsAsync(false, cancellationToken);
            foreach (var view in views)
            {
                var jsonViewModel = viewModelConverter.ConvertToJsonViewModel(view);
                if (string.IsNullOrEmpty(jsonViewModel.JsonMetadataSource))
                {
                    await this.Logger.WriteLineAsync($"View \"{jsonViewModel.Alias}\" is not in JSON format.");
                }

                if (await jsonViewModelUpgrader.UpgradeAsync(jsonViewModel, cancellationToken))
                {
                    await this.Logger.WriteLineAsync($"Upgrading JSON format metadata for view \"{jsonViewModel.Alias}\".");
                    var viewMetadataJson = jsonViewModel.JsonMetadataSource;

                    updateSqlText ??= builderFactory
                        .Update("Views")
                        .C("JsonMetadataSource").Equals().P("JsonMetadataSource")
                        .Where().C("ID").Equals().P("ID")
                        .Build();
                    parameters ??= new DataParameter[2];

                    parameters[0] = dbWriter.Parameter("ID", jsonViewModel.ID, DataType.Guid);
                    parameters[1] = dbWriter.Parameter("JsonMetadataSource", viewMetadataJson, DataType.BinaryJson);

                    await dbWriter
                        .SetCommand(updateSqlText, parameters)
                        .LogCommand()
                        .ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Upgrades views metadata JSON format in the table 'Views'. No params required.");
        }

        #endregion
    }
}
