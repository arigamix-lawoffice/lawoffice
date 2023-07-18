using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Scheme;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.SchemeUpdateSql
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string configurationString,
            string databaseName)
        {
            await logger.InfoAsync("Updating the scheme to current version from database");

            await logger.InfoAsync(
                string.IsNullOrEmpty(configurationString)
                    ? "Connection will be opened to default database"
                    : "Connection will be opened to database with connection \"{0}\"",
                configurationString);

            if (!string.IsNullOrEmpty(databaseName))
            {
                await logger.InfoAsync("Changes will be applied to database \"{0}\"", databaseName);
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
                await logger.ErrorAsync("Scheme doesn't exists in the database");
                return -1;
            }

            if (await databaseSchemeService.IsStorageUpToDateAsync())
            {
                await logger.InfoAsync("Scheme is up-to-date, skipping update");
            }
            else
            {
                await logger.InfoAsync("Scheme isn't up-to-date in the database, updating it...");
                await databaseSchemeService.UpdateStorageAsync();
                await logger.InfoAsync("Scheme has been successfully updated");
            }

            return 0;
        }
    }
}
