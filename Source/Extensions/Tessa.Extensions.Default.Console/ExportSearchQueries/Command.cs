using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Tessa.Views.SearchQueries;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.ExportSearchQueries
{
    public static class Command
    {
        [Verb("ExportSearchQueries")]
        [LocalizableDescription("Common_CLI_ExportSearchQueries")]
        public static async Task ExportSearchQueries(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_SearchQueriesNamesOrIdentifiers")] IEnumerable<string> nameOrIdentifier = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("o")] [LocalizableDescription("Common_CLI_OutputFolder")] string outputFolder = null,
            [Argument("c")] [LocalizableDescription("Common_CLI_ClearOutputFolder")] bool clearOutputFolder = false,
            [Argument("public")][LocalizableDescription("Common_CLI_ExportPublicSearchQueriesOnly")] bool publicOnly = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container = await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);
            container.RegisterType<ISearchQueryService, SearchQueryServiceClient>(
                new ContainerControlledLifetimeManager());

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    SearchQueryNamesOrIdentifiers = nameOrIdentifier?.ToList(),
                    OutputFolder = outputFolder,
                    ClearOutputFolder = clearOutputFolder,
                    PublicQueriesOnly = publicOnly
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