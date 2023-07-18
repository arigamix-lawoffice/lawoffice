#nullable enable

using NLog;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Tessa.TextRecognition.Enums;

namespace Tessa.Extensions.Default.Console.TextRecognition.Synchronous
{
    /// <summary>
    /// Синхронная команда распознавания файла.
    /// </summary>
    public sealed class Command : Base.Command
    {
        [Verb(nameof(TextRecognitionSynchronous))]
        [LocalizableDescription("Common_CLI_TextRecognitionSynchronous")]
        public static async Task TextRecognitionSynchronous(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_SourceFile")] string sourceFile,
            [Argument("target"), LocalizableDescription("Common_CLI_Target")] string? target = null,
            [Argument("lan"), LocalizableDescription("Common_CLI_Languages")] IEnumerable<string>? languages = null,
            [Argument("psm"), LocalizableDescription("Common_CLI_SegmentationMode")] int segmentationMode = (int) OcrSegmentationModes.AutoOsd,
            [Argument("cf"), LocalizableDescription("Common_CLI_Confidence")] float confidence = 50.0f,
            [Argument("pp"), LocalizableDescription("Common_CLI_Preprocess")] bool preprocess = false,
            [Argument("dr"), LocalizableDescription("Common_CLI_DetectRotation")] bool detectRotation = true,
            [Argument("dt"), LocalizableDescription("Common_CLI_DetectTables")] bool detectTables = false,
            [Argument("ow"), LocalizableDescription("Common_CLI_Overwrite")] bool overwrite = false,
            [Argument("a"), LocalizableDescription("Common_CLI_Address")] string? address = null,
            [Argument("i"), LocalizableDescription("Common_CLI_Instance")] string? instanceName = null,
            [Argument("u"), LocalizableDescription("Common_CLI_UserName")] string? userName = null,
            [Argument("p"), LocalizableDescription("Common_CLI_Password")] string? password = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo"), LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            // Отображение лого
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            var logger = new ConsoleLogger(LogManager.GetLogger(nameof(TextRecognitionSynchronous)), stdOut, stdErr, quiet);

            // Проверка параметров
            if (string.IsNullOrEmpty(sourceFile))
            {
                await logger.ErrorAsync($"Pass the path to the file to be recognized in the \"{nameof(sourceFile)}\" parameter.");
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }
            else if (!File.Exists(sourceFile))
            {
                await logger.ErrorAsync($"Can not find source file by path \"{sourceFile}\". Please, check if file exists and application has access to it.");
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            var ocrParameters = await GetOcrParametersAsync(logger, languages, segmentationMode, confidence, preprocess, detectRotation, detectTables, overwrite);
            if (ocrParameters is null)
            {
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            target = string.IsNullOrEmpty(target)
                ? Directory.GetCurrentDirectory()
                : DefaultConsoleHelper.NormalizeFolderAndCreateIfNotExists(target);

            // Запуск операции
            var context = new OperationContext(sourceFile, target, ocrParameters);
            var result = await RunAsync<Operation, OperationContext>(logger, context, stdOut, stdErr, address, instanceName, userName, password, quiet);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
