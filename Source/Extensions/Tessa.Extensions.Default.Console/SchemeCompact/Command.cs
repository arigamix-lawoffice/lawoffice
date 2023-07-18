using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SchemeCompact
{
    public static class Command
    {
        [Verb("SchemeCompact")]
        [LocalizableDescription("Common_CLI_Scheme_Compact")]
        public static async Task SchemeCompact(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_Scheme_X_Source")] string source,
            [Argument("out"), LocalizableDescription("Common_CLI_Scheme_Compact_Out")] string target = null,
            [Argument("include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitions = null,
            [Argument("exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitions = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SchemeCompact)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, stdOut, source, target, includedPartitions, excludedPartitions);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}