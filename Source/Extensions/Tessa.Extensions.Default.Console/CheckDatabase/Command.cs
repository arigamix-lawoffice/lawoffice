using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.CheckDatabase
{
    public static class Command
    {
        [Verb("CheckDatabase")]
        [LocalizableDescription("Common_CLI_CheckDatabase")]
        public static async Task CheckDatabase(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_CheckDatabaseName")] string databaseName = null,
            [Argument("timeout")] [LocalizableDescription("Common_CLI_ConnectTimeout")] int seconds = -1,
            [Argument("dbms")] [LocalizableDescription("Common_CLI_OutputDbms")] bool outputDbms = false,
            [Argument("c")] [LocalizableDescription("Common_CLI_OutputConnectionString")] bool outputConnectionString = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (outputDbms)
            {
                quiet = true;
                nologo = true;
            }
            else if (outputConnectionString)
            {
                quiet = true;
            }

            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(CheckDatabase)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, configurationString, databaseName, seconds, outputDbms, outputConnectionString);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}