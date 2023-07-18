using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.DropDatabase
{
    public static class Command
    {
        [Verb("DropDatabase")]
        [LocalizableDescription("Common_CLI_DropDatabase")]
        public static async Task DropDatabase(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(DropDatabase)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, configurationString, databaseName);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}