#nullable enable

using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Tessa.TextRecognition.Enums;

namespace Tessa.Extensions.Default.Console.TextRecognition.Asynchronous
{
    /// <summary>
    /// Асинхронная команда распознавания файла.
    /// </summary>
    public sealed class Command : Base.Command
    {
        [Verb(nameof(TextRecognitionAsynchronous))]
        [LocalizableDescription("Common_CLI_TextRecognitionAsynchronous")]
        public static async Task TextRecognitionAsynchronous(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_FileIdentifier")] string fileIdentifier,
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

            var logger = new ConsoleLogger(LogManager.GetLogger(nameof(TextRecognitionAsynchronous)), stdOut, stdErr, quiet);

            // Проверка параметров
            if (string.IsNullOrEmpty(fileIdentifier))
            {
                await logger.ErrorAsync($"Pass the file identifier in the \"{nameof(fileIdentifier)}\" parameter.");
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            var isParsed = Guid.TryParse(fileIdentifier, out var fileID);
            if (!isParsed)
            {
                await logger.ErrorAsync($"Can not parse file identifier to GUID. Please, check value and format in the \"{nameof(fileIdentifier)}\" parameter.");
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            var ocrParameters = await GetOcrParametersAsync(logger, languages, segmentationMode, confidence, preprocess, detectRotation, detectTables, overwrite);
            if (ocrParameters is null)
            {
                ConsoleAppHelper.EnvironmentExit(-1);
                return;
            }

            // Запуск операции
            var context = new OperationContext(fileID, ocrParameters);
            var result = await RunAsync<Operation, OperationContext>(logger, context, stdOut, stdErr, address, instanceName, userName, password, quiet);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}
