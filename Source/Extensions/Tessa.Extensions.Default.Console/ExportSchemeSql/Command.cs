using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.ExportSchemeSql
{
    public static class Command
    {
        [Verb("ExportSchemeSql")]
        [LocalizableDescription("Common_CLI_ExportSchemeSql")]
        public static async Task ExportSchemeSql(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("o")] [LocalizableDescription("Common_CLI_OutputFolder")] string outputFolder = null,
            [Argument("u"), LocalizableDescription("Common_CLI_UpdateSchemeInDatabase")] bool updateDbScheme = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(ImportSchemeSql)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, outputFolder, updateDbScheme, configurationString, databaseName);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}