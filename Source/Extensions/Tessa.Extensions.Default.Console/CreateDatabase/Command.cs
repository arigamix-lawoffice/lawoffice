using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.CreateDatabase
{
    public static class Command
    {
        [Verb("CreateDatabase")]
        [LocalizableDescription("Common_CLI_CreateDatabase")]
        public static async Task CreateDatabase(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("c")] [LocalizableDescription("Common_CLI_DropDatabaseIfExists")] bool dropIfExists = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(CreateDatabase)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, configurationString, databaseName, dropIfExists);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}