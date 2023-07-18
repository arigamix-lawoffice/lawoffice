using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Console.ConvertConfiguration;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ConvertCards
{
    public static class Command
    {
        [Verb("ConvertCards")]
        [LocalizableDescription("Common_CLI_ConvertCards")]
        public static async Task ConvertCards(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_SourceConfiguration")] string source,
            [Argument("o")] [LocalizableDescription("Common_CLI_ConvertedConfigurationOutputFolder")] string target = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("mode"), LocalizableDescription("Common_CLI_ConfigurationConversionMode")]
            ConversionMode conversionMode = ConversionMode.Upgrade,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("nd"), LocalizableDescription("Common_CLI_DoNotDeleteOldConfigurationFilesAfterConvert")]
            bool doNotDelete = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source is null");
            }

            if (string.IsNullOrEmpty(target))
            {
                if (source == ".")
                {
                    target = Directory.GetCurrentDirectory();
                }
                else
                {
                    FileAttributes attr = File.GetAttributes(source);
                    bool sourcePathIsDirectory = (attr & FileAttributes.Directory) == FileAttributes.Directory;
                    target = sourcePathIsDirectory ? source : Path.GetDirectoryName(source);
                }
            }

            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container =
                await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, null, address);

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    Source = source,
                    Target = target,
                    DoNotDelete = doNotDelete,
                    ConversionMode = conversionMode
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