using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.UpdateActionHistory
{
    public static class Command
    {
        [Verb("UpdateActionHistory")]
        [LocalizableDescription("Common_CLI_UpdateActionHistory")]
        public static async Task UpdateActionHistory(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument][LocalizableDescription("Common_CLI_SourceSchemePath")] string source = null,
            [Argument("from"), LocalizableDescription("Common_CLI_SourceConfigurationString")] string sourceConfigurationString = null,
            [Argument("to"), LocalizableDescription("Common_CLI_TargetConfigurationString")] string targetConfigurationString = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")][LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(UpdateActionHistory)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, source, sourceConfigurationString, targetConfigurationString);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
