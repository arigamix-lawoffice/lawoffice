using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SchemeDiff
{
    public static class Command
    {
        [Verb("SchemeDiff")]
        [LocalizableDescription("Common_CLI_Scheme_Difference")]
        public static async Task SchemeDiff(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("a"), LocalizableDescription("Common_CLI_Scheme_X_Source")] string sourceA,
            [Argument("b"), LocalizableDescription("Common_CLI_Scheme_X_Source")] string sourceB,
            [Argument("a-include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitionsA = null,
            [Argument("a-exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitionsA = null,
            [Argument("b-include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitionsB = null,
            [Argument("b-exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitionsB = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SchemeDiff)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, stdOut, sourceA, sourceB, includedPartitionsA, excludedPartitionsA, includedPartitionsB, excludedPartitionsB);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}