using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SqlScript
{
    public static class Command
    {
        [Verb("Sql")]
        [LocalizableDescription("Common_CLI_Sql")]
        public static async Task SqlScript(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_SourceSql")] IEnumerable<string> source = null,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("p"), LocalizableDescription("Common_CLI_ParametersSql")] IEnumerable<string> parameters = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SqlScript)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(
                logger,
                source,
                configurationString,
                databaseName,
                parameters,
                selectMode: false);

            ConsoleAppHelper.EnvironmentExit(result);
        }

        [Verb("Select")]
        [LocalizableDescription("Common_CLI_Select")]
        public static async Task SelectScript(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_SourceSql")] IEnumerable<string> source = null,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("top"), LocalizableDescription("Common_CLI_TopRowCount")] int rowCount = 0,
            [Argument("s"), LocalizableDescription("Common_CLI_CsvSeparator")] string separatorChar = ";",
            [Argument("h"), LocalizableDescription("Common_CLI_ShowHeaders")] bool showHeaders = false,
            [Argument("text"), LocalizableDescription("Common_CLI_TextMode")] bool text = false,
            [Argument("p"), LocalizableDescription("Common_CLI_ParametersSql")] IEnumerable<string> parameters = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SelectScript)), stdOut, stdErr, quiet);

            string csvSeparator = DefaultConsoleHelper.EscapeStringFromConsole(separatorChar);
            if (string.IsNullOrEmpty(csvSeparator))
            {
                csvSeparator = ";";
            }

            int result = await Operation.ExecuteAsync(
                logger,
                source,
                configurationString,
                databaseName,
                parameters,
                selectMode: true,
                csvResult: !text,
                topRowCount: rowCount,
                csvSeparator: csvSeparator.First(),
                showHeaders: showHeaders);

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}