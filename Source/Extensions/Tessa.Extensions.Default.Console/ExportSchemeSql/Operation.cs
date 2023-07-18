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

namespace Tessa.Extensions.Default.Console.ExportSchemeSql
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string outputFolder,
            bool updateSchemeInDatabase,
            string configurationString,
            string databaseName)
        {
            await logger.InfoAsync("Exporting the scheme using sql connection");

            await logger.InfoAsync(
                string.IsNullOrEmpty(configurationString)
                    ? "Connection will be opened to default database"
                    : "Connection will be opened to database with connection \"{0}\"",
                configurationString);

            if (!string.IsNullOrEmpty(databaseName))
            {
                await logger.InfoAsync("Changes will be applied from database \"{0}\"", databaseName);
            }

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
                await logger.ErrorAsync("Scheme doesn't exists in the database, can't continue");
                return -1;
            }

            if (!await databaseSchemeService.IsStorageUpToDateAsync())
            {
                if (!updateSchemeInDatabase)
                {
                    await logger.ErrorAsync("Scheme isn't up-to-date in the database, can't continue");
                    return -1;
                }

                await logger.InfoAsync("Scheme isn't up-to-date in the database, upgrading it...");
                await databaseSchemeService.UpdateStorageAsync();
            }

            SchemeDatabase tessaDatabase = new SchemeDatabase(DatabaseNames.Original);
            await tessaDatabase.RefreshAsync(databaseSchemeService);

            string exportPath = DefaultConsoleHelper.NormalizeFolderAndCreateIfNotExists(outputFolder);
            if (string.IsNullOrEmpty(exportPath))
            {
                exportPath = Directory.GetCurrentDirectory();
            }

            string tsdFilePath = Directory.EnumerateFiles(exportPath, "*.tsd").OrderBy(x => x).FirstOrDefault()
                ?? Path.Combine(exportPath, "Platform.tsd");

            await logger.InfoAsync("Reading scheme from file \"{0}\"", tsdFilePath);

            string[] partitions = FileSchemeService.GetPartitionPaths(tsdFilePath);
            var fileSchemeService = new FileSchemeService(tsdFilePath, partitions);

            if (!await fileSchemeService.IsStorageExistsAsync())
            {
                await logger.InfoAsync("Scheme doesn't exist in the file folder, creating it...");
                await fileSchemeService.CreateStorageAsync();
            }

            if (!await fileSchemeService.IsStorageUpToDateAsync())
            {
                await logger.InfoAsync("Scheme isn't up-to-date in the file folder, upgrading it...");
                await fileSchemeService.UpdateStorageAsync();
            }

            await logger.InfoAsync("Exporting the scheme using database to folder \"{0}\"", exportPath);
            await tessaDatabase.SubmitChangesAsync(fileSchemeService);

            await logger.InfoAsync("Scheme has been exported successfully");
            return 0;
        }
    }
}
