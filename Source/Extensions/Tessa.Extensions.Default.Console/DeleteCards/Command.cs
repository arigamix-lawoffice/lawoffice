using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.DeleteCards
{
    public static class Command
    {
        [Verb("DeleteCards")]
        [LocalizableDescription("Common_CLI_DeleteCards")]
        public static async Task DeleteCards(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_CardIdentifiers")] IEnumerable<string> identifiers = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("s")] [LocalizableDescription("Common_CLI_ColumnSeparator")] string separatorChar = null,
            [Argument("c"), LocalizableDescription("Common_CLI_IgnoreAlreadyDeleted")] bool ignoreAlreadyDeleted = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);

            var logger = container.Resolve<IConsoleLogger>();
            List<CardInfo> cardInfoList = await DefaultConsoleHelper.TryParseCardInfoListAsync(identifiers, logger, separatorChar);
            if (cardInfoList == null)
            {
                // ошибка парсинга
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    CardInfoList = cardInfoList,
                    IgnoreAlreadyDeleted = ignoreAlreadyDeleted,
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