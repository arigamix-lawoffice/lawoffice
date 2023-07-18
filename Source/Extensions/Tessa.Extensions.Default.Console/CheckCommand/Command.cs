using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.CheckCommand
{
    // неймспейс называн CheckCommand, чтобы не конфликтовать с классом Tessa.Platform.Check, который используется в других командах
    public static class Command
    {
        [Verb("Check")]
        [LocalizableDescription("Common_CLI_Check")]
        public static async Task Check(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("h")] [LocalizableDescription("Common_CLI_Check_Health")] bool checkHealth = false,
            [Argument("timeout")] [LocalizableDescription("Common_CLI_ConnectTimeout")] int seconds = 0,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(Check)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, checkHealth, address, instanceName, seconds, quiet);
            
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}