using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Console.CheckService
{
    public static class Command
    {
        [Verb("CheckService")]
        [LocalizableDescription("Common_CLI_CheckService")]
        public static async Task CheckService(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("timeout")] [LocalizableDescription("Common_CLI_ConnectTimeout")] int seconds = 0,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);

            // отрицательное значение seconds использует таймаут по умолчанию в соответствии с конфигурационным файлом
            if (seconds >= 0)
            {
                TimeSpan timeoutSpan = seconds == 0 ? Timeout.InfiniteTimeSpan : TimeSpan.FromSeconds(seconds);

                var connectionSettings = container.Resolve<IConnectionSettings>();
                connectionSettings.OpenTimeout = timeoutSpan;
                connectionSettings.SendTimeout = timeoutSpan;
                connectionSettings.CloseTimeout = timeoutSpan;
            }

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                if (!await operation.LoginAsync(userName, password))
                {
                    ConsoleAppHelper.EnvironmentExit(ConsoleAppHelper.FailedLoginExitCode);
                    return;
                }

                result = await operation.ExecuteAsync();
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}