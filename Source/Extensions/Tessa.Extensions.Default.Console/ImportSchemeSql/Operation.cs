using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Scheme;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.ImportSchemeSql
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string source,
            string configurationString,
            string databaseName,
            IEnumerable<string> includedPartitions,
            IEnumerable<string> excludedPartitions)
        {
            string filePath = DefaultConsoleHelper.GetSourceFiles(source, "*.tsd").FirstOrDefault();
            if (filePath is null)
            {
                await logger.ErrorAsync("Can't find database file *.tsd in \"{0}\"", source);
                return -2;
            }

            await logger.InfoAsync("Importing the scheme using sql connection");

            await logger.InfoAsync(
                string.IsNullOrEmpty(configurationString)
                    ? "Connection will be opened to default database"
                    : "Connection will be opened to database with connection \"{0}\"",
                configurationString);

            if (!string.IsNullOrEmpty(databaseName))
            {
                await logger.InfoAsync("Changes will be applied to database \"{0}\"", databaseName);
            }

            string fileFullPath = Path.GetFullPath(filePath);
            await logger.InfoAsync("Reading scheme from: \"{0}\"", fileFullPath);

            var fileSchemeService = new FileSchemeService(
                fileFullPath,
                DefaultConsoleHelper.GetSchemePartitions(fileFullPath, includedPartitions, excludedPartitions));

            foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
            {
                await logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
            }

            if (!await fileSchemeService.IsStorageUpToDateAsync())
            {
                await logger.InfoAsync("Scheme isn't up-to-date in the file folder, upgrading it...");
                await fileSchemeService.UpdateStorageAsync();
            }

            SchemeDatabase tessaDatabase = new SchemeDatabase(DatabaseNames.Original);
            await tessaDatabase.RefreshAsync(fileSchemeService);

            await logger.InfoAsync("Importing the scheme");

            var configurationProvider = (await ConfigurationManager.GetDefaultAsync()).Configuration
                .GetConfigurationDataProvider(configurationString);
            var factory = ConfigurationManager
                .GetConfigurationDataProviderFromType(configurationProvider.Item2.DataProvider)
                .GetDbProviderFactory();
            var connectionString = configurationProvider.Item2.ConnectionString;

            if (!string.IsNullOrEmpty(databaseName))
            {
                var builder = factory.CreateConnectionStringBuilder();

                builder.ConnectionString = connectionString;
                builder["Database"] = databaseName;
                connectionString = builder.ToString();
            }

            var container = await new UnityContainer().RegisterDatabaseForConsoleAsync();
            container
                .RegisterFactory<DbManager>(
                    (c) => new DbManager(configurationProvider.Item1.GetDataProvider(connectionString), connectionString),
                    new PerResolveLifetimeManager());

            var dbScope = container.Resolve<IDbScope>();

            var databaseSchemeService = new DatabaseSchemeService(
                dbScope,
                new ServerConfigurationVersionProvider(dbScope));

            if (!await databaseSchemeService.IsStorageExistsAsync())
            {
                await logger.InfoAsync("Scheme doesn't exists in the database, creating it...");
                await databaseSchemeService.CreateStorageAsync();
            }

            if (!await databaseSchemeService.IsStorageUpToDateAsync())
            {
                await logger.InfoAsync("Scheme isn't up-to-date in the database, upgrading it...");
                await databaseSchemeService.UpdateStorageAsync();
            }

            await tessaDatabase.SubmitChangesAsync(databaseSchemeService);

            await logger.InfoAsync("Scheme has been imported successfully");
            return 0;
        }
    }
}
