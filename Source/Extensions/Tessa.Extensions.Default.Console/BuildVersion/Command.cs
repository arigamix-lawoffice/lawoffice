using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.BuildVersion
{
    public static class Command
    {
        [Verb("BuildVersion")]
        [LocalizableDescription("Common_CLI_BuildVersion")]
        public static async Task BuildVersion(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("full")] [LocalizableDescription("Common_CLI_BuildFullName")] bool fullName = false)
        {
            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(BuildVersion)), stdOut, stdErr, quiet: true);

            int result = await Operation.ExecuteAsync(logger, fullName);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}