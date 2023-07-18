using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.ExportDiffCulture
{
    public static class Command
    {
        [Verb("ExportDiffCulture")]
        [LocalizableDescription("Common_CLI_ExportDiffCulture")]
        public static async Task ExportDiffCulture(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_ExportDiffCultureSources")] IEnumerable<string> sources,
            [Argument("base")] [LocalizableDescription("Common_CLI_ExportDiffCultureBase")] string @base,
            [Argument("target")] [LocalizableDescription("Common_CLI_ExportDiffCultureTarget")] string target,
            [Argument("o")] [LocalizableDescription("Common_CLI_ExportDiffCultureOutput")] string output,
            [Argument("q")] [LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            Check.ArgumentNotNullOrEmpty(@base, nameof(@base));
            Check.ArgumentNotNullOrEmpty(target, nameof(target));
            Check.ArgumentNotNullOrEmpty(output, nameof(output));

            CultureInfo baseCulture = CultureInfo.GetCultureInfo(@base.Trim());
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
                    Sources = sources?.Select(x => x.NormalizePathOnCurrentPlatform()).ToList() ?? new(),
                    Output = output.NormalizePathOnCurrentPlatform(),
                    TargetCulture = targetCulture,
                    BaseCulture = baseCulture
                };

                result = await operation.ExecuteAsync(context);
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
