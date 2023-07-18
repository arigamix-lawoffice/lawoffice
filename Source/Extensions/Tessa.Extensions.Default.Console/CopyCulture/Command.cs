using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.CopyCulture
{
    public static class Command
    {
        [Verb("CopyCulture")]
        [LocalizableDescription("Common_CLI_CopyCulture")]
        public static async Task CopyCulture(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_CopyCultureSource")] string source,
            [Argument("from")] [LocalizableDescription("Common_CLI_CopyCultureFrom")] string from,
            [Argument("to")] [LocalizableDescription("Common_CLI_CopyCultureTo")] string to,
            [Argument("o")] [LocalizableDescription("Common_CLI_CopyCultureOutputPath")] string target = null,
            [Argument("detached"), LocalizableDescription("Common_CLI_CopyCultureDetached")] bool detached = false,
            [Argument("empty"), LocalizableDescription("Common_CLI_CopyCultureEmpty")] bool empty = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            Check.ArgumentNotNullOrEmpty(source, nameof(source));
            Check.ArgumentNotNullOrEmpty(from, nameof(from));
            Check.ArgumentNotNullOrEmpty(to, nameof(to));

            source = source.NormalizePathOnCurrentPlatform();
            target = target.NormalizePathOnCurrentPlatform();

            CultureInfo fromCulture = CultureInfo.GetCultureInfo(from.Trim());
            CultureInfo toCulture = CultureInfo.GetCultureInfo(to.Trim());
            
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
                    Source = source,
                    Target = target,
                    FromCulture = fromCulture,
                    ToCulture = toCulture,
                    ForceDetached = detached,
                    EmptyOnly = empty,
                };

                result = await operation.ExecuteAsync(context);
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
