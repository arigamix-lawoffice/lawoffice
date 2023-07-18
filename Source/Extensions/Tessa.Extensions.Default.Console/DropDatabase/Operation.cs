using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.DropDatabase
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string configurationString,
            string databaseName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(configurationString))
            {
                await logger.InfoAsync("Dropping database from default configuration string");
            }
            else
            {
                await logger.InfoAsync("Dropping database from configuration string \"{0}\"", configurationString);
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                await logger.InfoAsync("Database will be dropped with name from connection string");
            }
            else
            {
                await logger.InfoAsync("Database will be dropped with name \"{0}\"", databaseName);
            }

            await DefaultConsoleHelper.DropAndCreateDatabaseAsync(
                logger,
                configurationString,
                databaseName,
                dropOld: true,
                createNew: false,
                cancellationToken: cancellationToken);

            await logger.InfoAsync("Database has been dropped");
            return 0;
        }
    }
}
