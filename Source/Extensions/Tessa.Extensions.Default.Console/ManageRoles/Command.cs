using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ManageRoles
{
    public static class Command
    {
        [Verb("ManageRoles")]
        [LocalizableDescription("Common_CLI_ManageRoles")]
        public static async Task ManageRoles(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_ManageRoleCommands")] IEnumerable<CommandType> command = null,
            [Argument("id")] [LocalizableDescription("Common_CLI_RoleIdentifiers")] string identifiers = null,
            [Argument("bulk"), LocalizableDescription("Common_CLI_SyncDeputiesBulkSize")] int bulkSize = 500000,
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
            var logger = container.Resolve<IConsoleLogger>();

            CommandType[] commands = command?.ToArray() ?? Array.Empty<CommandType>();
            if (commands.Length == 0)
            {
                await logger.ErrorAsync("No commands are specified");
                ConsoleAppHelper.EnvironmentExit(-2);
                return;
            }

            Guid[] identifierArray = commands.Any(x => x == CommandType.RecalcDynamicRoles || x == CommandType.RecalcRoleGenerators)
                ? (await DefaultConsoleHelper.TryParseIdentifiersListAsync(new[] { identifiers }, logger)).ToArray()
                : Array.Empty<Guid>();

            foreach (CommandType commandType in commands)
            {
                switch (commandType)
                {
                    case CommandType.SyncAllDeputies:
                    case CommandType.RecalcAllDynamicRoles:
                    case CommandType.RecalcAllRoleGenerators:
                        // дополнительные проверки отсутствуют
                        break;

                    case CommandType.RecalcDynamicRoles:
                    case CommandType.RecalcRoleGenerators:
                        if (identifierArray.Length == 0)
                        {
                            await logger.ErrorAsync("No identifiers are specified for command {0}", commandType);
                            ConsoleAppHelper.EnvironmentExit(-3);
                            return;
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(commandType), commandType, null);
                }
            }

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    Commands = commands,
                    Identifiers = identifierArray,
                    BulkSize = bulkSize,
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