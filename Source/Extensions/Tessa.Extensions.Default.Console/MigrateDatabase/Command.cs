using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.MigrateDatabase
{
    public static class Command
    {
        [Verb("MigrateDatabase")]
        [LocalizableDescription("Common_CLI_MigrateDatabase")]
        public static async Task MigrateDatabase(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            // ReSharper disable once InconsistentNaming
            [Argument] [LocalizableDescription("Common_CLI_MigrationTargetConfigurationString")] string targetCS,
            [Argument("tdb"), LocalizableDescription("Common_CLI_MigrationTargetDatabaseName")] string targetDatabaseName = null,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("p"), LocalizableDescription("Common_CLI_ConcurrentConnectionCount")] int threadCount = 0,
            [Argument("bulk"), LocalizableDescription("Common_CLI_BulkInsertSize")] int bulkSize = 10000,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            if (threadCount <= 0)
            {
                threadCount = Environment.ProcessorCount;
            }

            if (bulkSize <= 0)
            {
                bulkSize = 1;
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(MigrateDatabase)), stdOut, stdErr, quiet, synchronized: true);

            int result = await Operation.ExecuteAsync(logger, configurationString, databaseName, targetCS, targetDatabaseName, threadCount, bulkSize, quiet);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}