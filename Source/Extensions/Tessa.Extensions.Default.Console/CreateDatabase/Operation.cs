using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.CreateDatabase
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string configurationString,
            string databaseName,
            bool dropIfExists,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(configurationString))
            {
                await logger.InfoAsync("Creating database from default configuration string");
            }
            else
            {
                await logger.InfoAsync("Creating database from configuration string \"{0}\"", configurationString);
            }

            await DefaultConsoleHelper.DropAndCreateDatabaseAsync(
                logger,
                configurationString,
                databaseName,
                dropOld: dropIfExists,
                createNew: true,
                cancellationToken: cancellationToken);

            await logger.InfoAsync("Database has been created");
            return 0;
        }
    }
}
