using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.TimeZone
{
    public static class Command
    {
        [Verb("TimeZone")]
        [LocalizableDescription("Common_CLI_TimeZone")]
        public static async Task TimeZone(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_TimeZoneOperation")] IEnumerable<OperationFunction> operation,
            [Argument("id")] [LocalizableDescription("Common_CLI_ID")] int? zoneID = null,
            [Argument("offset")] [LocalizableDescription("Common_CLI_Offset")] int? minutes = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);

            int result;
            await using (var operationObj = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    OperationFunction = operation,
                    ZoneID = zoneID,
                    ZoneOffset = minutes,
                };

                if (!await operationObj.LoginAsync(userName, password))
                {
                    ConsoleAppHelper.EnvironmentExit(ConsoleAppHelper.FailedLoginExitCode);
                    return;
                }

                result = await operationObj.ExecuteAsync(context);
                await operationObj.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}