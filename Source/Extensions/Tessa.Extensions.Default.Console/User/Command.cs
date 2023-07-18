using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.User
{
    public static class Command
    {
        [Verb("User")]
        [LocalizableDescription("Common_CLI_UpdateUser")]
        public static async Task User(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_UpdateUserAlias")] string user,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("account")] [LocalizableDescription("Common_CLI_UpdateUserAccount")] string newAccount = null,
            [Argument("login")] [LocalizableDescription("Common_CLI_UpdateUserLogin")] string newLogin = null,
            [Argument("password")] [LocalizableDescription("Common_CLI_UpdateUserPassword")] string newPassword = null,
            [Argument("ldap")] [LocalizableDescription("Common_CLI_UpdateUserLdap")] bool ldap = false,
            [Argument("nologin")] [LocalizableDescription("Common_CLI_UpdateUserForbidden")] bool nologin = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            int paramCounter = 0;
            if (!string.IsNullOrEmpty(newAccount))
            {
                paramCounter++;
            }
            if (!string.IsNullOrEmpty(newLogin) && !string.IsNullOrEmpty(newPassword))
            {
                paramCounter++;
            }
            if (nologin)
            {
                paramCounter++;
            }

            if (paramCounter == 0)
            {
                throw new ArgumentException("No action specified to perform while updating user.");
            }
            if (paramCounter > 1)
            {
                throw new ArgumentException("More than one action specified to perform while updating user.");
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    User = user,
                    Account = newAccount,
                    Login = newLogin,
                    Password = newPassword,
                    Ldap = ldap,
                    NoLogin = nologin
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