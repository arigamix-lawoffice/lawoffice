using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.PackageApp
{
    public static class Command
    {
        [Verb("PackageApp")]
        [LocalizableDescription("Common_CLI_PackageApp")]
        public static async Task PackageApp(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_AppSourceExecutable")] string executable,
            [Argument("out")] [LocalizableDescription("Common_CLI_AppOutputPackage")] string jcardFile = null,
            [Argument("ico")] [LocalizableDescription("Common_CLI_AppIconFile")] string icon = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_AppAlias")] string alias = null,
            [Argument("n")] [LocalizableDescription("Common_CLI_AppName")] string name = null,
            [Argument("g")] [LocalizableDescription("Common_CLI_AppGroup")] string group = null,
            [Argument("v")] [LocalizableDescription("Common_CLI_AppVersion")] string version = null,
            [Argument("admin"), LocalizableDescription("Common_CLI_AppAdmin")] bool admin = false,
            [Argument("64bit"), LocalizableDescription("Common_CLI_Client64Bit")] bool client64Bit = false,
            [Argument("api2"), LocalizableDescription("Common_CLI_AppManagerApiV2")] bool appManagerApiV2 = false,
            [Argument("hidden"), LocalizableDescription("Common_CLI_HiddenApplication")] bool hidden = false,
            [Argument("b"), LocalizableDescription("Common_CLI_AppBinaryMode")] bool binaryMode = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(PackageApp)), stdOut, stdErr, quiet);
            int result = await Operation.ExecuteAsync(logger, executable, jcardFile, icon, alias, name, group, version, admin, client64Bit, appManagerApiV2, hidden, binaryMode);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}