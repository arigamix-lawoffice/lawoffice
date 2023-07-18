#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.Maintenance
{
    public static class Command
    {
        private const SupportedCommand Unsupported = (SupportedCommand) (-1);
        
        [Verb("Maintenance")]
        [LocalizableDescription("Common_CLI_Maintenance")]
        public static async Task Maintenance(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_MaintenanceCommand")] string? command = null,
            [Argument("wa")] [LocalizableDescription("Common_CLI_MaintenanceAddress")] string? webbiAddress = null,
            [Argument("wtimeout")] [LocalizableDescription("Common_CLI_MaintenanceTimeout")] int commandTimeoutSeconds = 0,
            [Argument("m")] [LocalizableDescription("Common_CLI_MaintenanceMessages")] IEnumerable<string>? messages = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string? address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string? instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string? userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string? password = null,
            [Argument("isolated")] [LocalizableDescription("Common_CLI_MaintenanceIsolatedMode")] bool isolated = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);
            var commandCode = GetCommand(command);
            if (commandCode == Unsupported)
            {
                var logger = container.Resolve<IConsoleLogger>();
                await logger.ErrorAsync("Invalid command name.");
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            await using var operation = container.Resolve<Operation>();
            var context = new OperationContext
            {
                Command = commandCode,
                Address = webbiAddress,
                Timeout = commandTimeoutSeconds,
                RawMessages = messages?.ToList(),
                Isolated = isolated,
            };

            if (commandCode == SupportedCommand.SwitchOn && !isolated)
            {
                if (!await operation.LoginAsync(userName, password))
                {
                    ConsoleAppHelper.EnvironmentExit(ConsoleAppHelper.FailedLoginExitCode);
                    return;
                }
            }
            
            var result = await operation.ExecuteAsync(context);
            ConsoleAppHelper.EnvironmentExit(result);
        }

        private static SupportedCommand GetCommand(string? command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return Unsupported;
            }

            command = command.ToLower();
            return command switch
            {
                "switch-on" => SupportedCommand.SwitchOn,
                "switch-off" => SupportedCommand.SwitchOff,
                "check" => SupportedCommand.Check,
                "hcheck" => SupportedCommand.HealthCheck,
                "status" => SupportedCommand.Status,
                _ => Unsupported
            };
        }
    }
}
