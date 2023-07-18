#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.TextRecognition;
using Tessa.TextRecognition.Enums;
using Unity;

namespace Tessa.Extensions.Default.Console.TextRecognition.Base
{
    /// <summary>
    /// Базовая команда распознавания файла.
    /// </summary>
    public abstract class Command
    {
        protected static async Task<OcrParameters?> GetOcrParametersAsync(
            IConsoleLogger logger,
            IEnumerable<string>? languages,
            int segmentationMode,
            float confidence,
            bool preprocess,
            bool detectRotation,
            bool detectTables,
            bool overwrite)
        {
            try
            {
                var ocrLanguages = languages is null
                    ? Enum.GetValues<OcrLanguages>()
                    : languages
                        .Select(static l => Enum.Parse<OcrLanguages>(l, ignoreCase: true))
                        .Distinct()
                        .ToArray();

                return new OcrParameters
                {
                    Languages = ocrLanguages,
                    SegmentationMode = (OcrSegmentationModes)segmentationMode,
                    Confidence = confidence,
                    Preprocess = preprocess,
                    DetectRotation = detectRotation,
                    DetectTables = detectTables,
                    Overwrite = overwrite
                };
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync("An error occurred during validate recognition parameters.", ex);
                return null;
            }
        }

        protected static async Task<int> RunAsync<TOperation, TContext>(
            IConsoleLogger logger,
            TContext context,
            TextWriter stdOut,
            TextWriter stdErr,
            string? address,
            string? instanceName,
            string? userName,
            string? password,
            bool quiet)
            where TContext: OperationContext
            where TOperation: ConsoleOperation<TContext>
        {
            IUnityContainer? container = null;

            try
            {
                // Создание и инициализация контейнера с зависимостями
                container = await new UnityContainer().RegisterServerForConsoleAsync();
                await container.ConfigureConsoleForClientAsync(stdOut, stdErr, quiet, instanceName, address);

                await using var operation = container.Resolve<TOperation>();

                // Инициализация возможности отмены операции
                using var cts = new CancellationTokenSource();
                System.Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    cts.Cancel();
                    eventArgs.Cancel = true;
                };

                // Вход в систему под пользователем
                if (!await operation.LoginAsync(userName, password, cts.Token))
                {
                    ConsoleAppHelper.EnvironmentExit(ConsoleAppHelper.FailedLoginExitCode);
                    return -1;
                }

                // Выполнение команды
                return await operation.ExecuteAsync(context, cts.Token);
            }
            finally
            {
                if (container is not null)
                {
                    await container.DisposeAllRegistrationsAsync();
                }
            }
        }
    }
}
