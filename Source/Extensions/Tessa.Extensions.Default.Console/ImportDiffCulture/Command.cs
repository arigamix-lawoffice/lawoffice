using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ImportDiffCulture
{
    public static class Command
    {
        [Verb("ImportDiffCulture")]
        [LocalizableDescription("Common_CLI_ImportDiffCulture")]
        public static async Task ImportDiffCulture(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_ImportDiffCultureSource")] string source,
            [Argument("o")] [LocalizableDescription("Common_CLI_ImportDiffCultureOutput")] string output,
            [Argument("target")] [LocalizableDescription("Common_CLI_ImportDiffCultureTarget")] string target,
            [Argument("q")] [LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            Check.ArgumentNotNullOrEmpty(source, nameof(source));
            Check.ArgumentNotNullOrEmpty(output, nameof(output));
            Check.ArgumentNotNullOrEmpty(target, nameof(target));

            CultureInfo targetCulture = CultureInfo.GetCultureInfo(target.Trim());

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
                    Output = output.NormalizePathOnCurrentPlatform(),
                    TargetCulture = targetCulture
                };

                result = await operation.ExecuteAsync(context);
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
