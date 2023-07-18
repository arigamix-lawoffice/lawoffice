using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ValidateLocalization
{
    public static class Command
    {
        [Verb("ValidateLocalization")]
        [LocalizableDescription("Common_CLI_ValidateLocalization")]
        public static async Task ValidateLocalization(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_ValidateLocalizationSource")] string source,
            [Argument("q")] [LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            Check.ArgumentNotNullOrEmpty(source, nameof(source));

            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IUnityContainer container =
                await new UnityContainer().ConfigureConsoleForClientAsync(stdOut, stdErr, quiet);

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    Source = source.NormalizePathOnCurrentPlatform(),
                };

                result = await operation.ExecuteAsync(context);
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
