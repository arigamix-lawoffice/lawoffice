using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Extensions.Default.Console.GetKey;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SetKey
{
    public static class Command
    {
        [Verb("SetKey")]
        [LocalizableDescription("Common_CLI_SetKey")]
        public static async Task SetKey(
            [Input] TextReader input,
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_KeyType")] KeyType key,
            [Argument("path"), LocalizableDescription("Common_CLI_KeyFolderOrFile")] string folderOrFile,
            [Argument("value"), LocalizableDescription("Common_CLI_KeyValue")] string value = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (string.IsNullOrWhiteSpace(folderOrFile))
            {
                throw new ArgumentException("Please, specify path to a folder or a file.");
            }

            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SetKey)), stdOut, stdErr, quiet);
            value ??= await input.ReadLineAsync();

            int result = await Operation.ExecuteAsync(logger, key, folderOrFile, value);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}