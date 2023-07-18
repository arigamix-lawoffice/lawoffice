using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SchemeUpdate
{
    public static class Command
    {
        [Verb("SchemeUpdate")]
        [LocalizableDescription("Common_CLI_SchemeUpdate")]
        public static async Task SchemeUpdate(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_Scheme_X_Source")] string source,
            [Argument("include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitions = null,
            [Argument("exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitions = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SchemeUpdate)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, source, includedPartitions, excludedPartitions);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}