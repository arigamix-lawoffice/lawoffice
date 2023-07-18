using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ImportScheme
{
    public static class Command
    {
        [Verb("ImportScheme")]
        [LocalizableDescription("Common_CLI_ImportScheme")]
        public static async Task ImportScheme(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_SourceSchemePath")] string source,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitions = null,
            [Argument("exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitions = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source is null");
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
                    Source = source,
                    IncludedPartitions = includedPartitions,
                    ExcludedPartitions = excludedPartitions,
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