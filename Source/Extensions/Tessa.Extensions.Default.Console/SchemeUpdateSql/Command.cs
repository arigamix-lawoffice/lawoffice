using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SchemeUpdateSql
{
    public static class Command
    {
        [Verb("SchemeUpdateSql")]
        [LocalizableDescription("Common_CLI_SchemeUpdateSql")]
        public static async Task SchemeUpdateSql(
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

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SchemeUpdateSql)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, configurationString, databaseName);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}