using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public static class Command
    {
        [Verb("ImportUsers")]
        [LocalizableDescription("Common_CLI_ImportUsers")]
        public static async Task ImportUsers(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_SourceUserFile")] string pathToUserFile,
            [Argument("sd")] [LocalizableDescription("Common_CLI_SourceDepartmentFile")] string pathToDepartmentFile = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (pathToUserFile == null)
            {
                throw new ArgumentNullException("pathToUserFile is null");
            }

            var fileExt = Path.GetExtension(pathToUserFile);
            ImportType importType;
            switch (fileExt.ToLowerInvariant())
            {
                case ".xlsx":
                    importType = ImportType.Excel;
                    break;
                case ".csv":
                    if (pathToDepartmentFile == null)
                    {
                        throw new ArgumentNullException("Missing department file for loading from csv");
                    }
                    importType = ImportType.Csv;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Only xlsx or csv files supported");
            }

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
                    PathToUserFile = pathToUserFile,
                    PathToDepartmentFile = pathToDepartmentFile,
                    ImportType = importType,
                    ConfigurationString = configurationString,
                    DatabaseName = databaseName,
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