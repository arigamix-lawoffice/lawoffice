using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.PackageWebApp
{
    public static class Command
    {
        [Verb("PackageWebApp")]
        [LocalizableDescription("Common_CLI_PackageWebApp")]
        public static async Task PackageWebApp(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_WebAppSourceExecutable")] string executable,
            [Argument("out")] [LocalizableDescription("Common_CLI_WebAppOutputPackage")] string jcardFile = null,
            [Argument("n")] [LocalizableDescription("Common_CLI_WebAppName")] string name = null,
            [Argument("v")] [LocalizableDescription("Common_CLI_WebAppVersion")] string version = null,
            [Argument("d")] [LocalizableDescription("Common_CLI_WebAppDescription")] string description = null,
            [Argument("lang"), LocalizableDescription("Common_CLI_WebAppLanguageCode")] string languageCode = null,
            [Argument("os"), LocalizableDescription("Common_CLI_WebAppOSName")] string operatingSystem = null,
            [Argument("64bit"), LocalizableDescription("Common_CLI_Client64Bit")] bool client64Bit = false,
            [Argument("b"), LocalizableDescription("Common_CLI_WebAppBinaryMode")] bool binaryMode = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(PackageWebApp)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, executable, jcardFile, name, version, description, languageCode, operatingSystem, client64Bit, binaryMode);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}