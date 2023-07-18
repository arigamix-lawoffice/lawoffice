using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ExportViews
{
    public static class Command
    {
        [Verb("ExportViews")]
        [LocalizableDescription("Common_CLI_ExportViews")]
        public static async Task ExportViews(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_ViewAliasesOrIdentifiers")] IEnumerable<string> aliasOrIdentifier = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("o")] [LocalizableDescription("Common_CLI_OutputFolder")] string outputFolder = null,
            [Argument("c")] [LocalizableDescription("Common_CLI_ClearOutputFolder")] bool clearOutputFolder = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    ViewAliasesOrIdentifiers = aliasOrIdentifier?.ToList(),
                    OutputFolder = outputFolder,
                    ClearOutputFolder = clearOutputFolder,
                };

                if (!await operation.LoginAsync(userName, password))
                {
                    ConsoleAppHelper.EnvironmentExit(ConsoleAppHelper.FailedLoginExitCode);
                    return;
                }

                result = await operation.ExecuteAsync(context);
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}