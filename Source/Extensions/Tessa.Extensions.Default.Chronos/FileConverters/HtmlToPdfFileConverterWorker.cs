#nullable enable
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.FileConverters;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    public class HtmlToPdfFileConverterWorker :
        IFileConverterWorker,
        IAsyncDisposable
    {
        #region Fields

        private readonly IConfigurationManager configurationManager;

        private IProcessManager? processManager;

        private string? wkHtmlToPdfPath;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public HtmlToPdfFileConverterWorker(
            IConfigurationManager configurationManager,
            Func<IProcessManager> createProcessManagerFunc)
        {
            this.configurationManager = NotNullOrThrow(configurationManager);
            this.processManager = new LazyProcessManager(NotNullOrThrow(createProcessManagerFunc));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Путь к внешнему исполняемому файлу "wkhtmltopdf".
        /// </summary>
        private string GetWkHtmlToPdfPath() =>
            this.wkHtmlToPdfPath ??= Environment.ExpandEnvironmentVariables(
                    this.configurationManager.Configuration.Settings.TryGet<string>("WkHtmlToPdf") ??
                    string.Empty)
                .Trim();

        /// <summary>
        /// Получает текст с сообщениями об ошибках в выходном файле.
        /// </summary>
        /// <param name="errorFile">Выходной файл с ошибками.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Текст с сообщениями об ошибках.</returns>
        private static async Task<string> GetErrorTextAsync(ITempFile errorFile, CancellationToken cancellationToken = default)
        {
            string? errorText = null;

            string path = errorFile.Path;
            if (File.Exists(path))
            {
                try
                {
                    errorText = await File.ReadAllTextAsync(path, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            if (errorText != null)
            {
                errorText = errorText.Trim();
            }

            if (string.IsNullOrEmpty(errorText))
            {
                errorText = "Unknown error";
            }

            return errorText;
        }

        #endregion

        #region IFileConverterWorker Members

        /// <summary>
        /// Преобразует файл в заданный формат.
        /// </summary>
        /// <param name="context">Контекст, содержащий информацию по выполняемому преобразованию.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public virtual async Task ConvertFileAsync(IFileConverterContext context, CancellationToken cancellationToken = default)
        {
            var processManager = this.processManager ??
                throw new ObjectDisposedException(this.GetType().FullName);

            using ITempFile errorFile = TempFile.Acquire("error.txt");

            // wkhtmltopdf производит конвертацию файла на основе его расширения.
            // Передать его в качестве аргумента нельзя, так что приходится временно переименовывать файл.
            var previousPath = context.InputFilePath;
            var newFilePath = $"{context.InputFilePath}.{context.InputExtension}";
            File.Move(previousPath, newFilePath);

            try
            {
                var startInfo = new ProcessStartInfo()
                    .SetSilentExecution()
                    .SetCommandLine(
                        this.GetWkHtmlToPdfPath(),
                        $"\"{newFilePath}\" \"{context.OutputFilePath}\"",
                        outputFile: errorFile.Path,
                        errorFile: errorFile.Path);

                using Process process = processManager.StartProcess(startInfo);
                await process.WaitForExitAsync(cancellationToken);

                if (process.ExitCode != 0)
                {
                    context.ValidationResult.AddError(this,
                        "Conversion failed. Exit code: {0}. Output:{1}{2}",
                        process.ExitCode, Environment.NewLine, await GetErrorTextAsync(errorFile, cancellationToken));

                    return;
                }

                logger.Trace("PDF file is generated from {0}.", context.InputExtension);

                // пишем ключ, через который вызывающая сторона поймёт, что конвертация была выполнена через наш конвертер
                context.ResponseInfo[FileConverterWorkerNames.HtmlToPdf] = BooleanBoxes.True;
            }
            finally
            {
                File.Move(newFilePath, previousPath);
            }
        }

        /// <summary>
        /// Выполняет обработку перед запуском цикла обслуживания для очереди на конвертацию файлов.
        /// Метод запускается единственный раз при старте сервиса конвертации.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <remarks>
        /// Реализация по умолчанию не выполняет действий.
        /// </remarks>
        public virtual Task PreprocessAsync(CancellationToken cancellationToken = default)
        {
            // does nothing by default
            return Task.CompletedTask;
        }

        /// <summary>
        /// Выполняет обработку в процессе выполнения цикла обслуживания для очереди на конвертацию файлов.
        /// Метод запускается множество раз внутри цикла с периодичностью, заданной в конфигурационном файле.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <remarks>
        /// Реализация по умолчанию не выполняет действий.
        /// </remarks>
        public virtual Task PerformMaintenanceAsync(CancellationToken cancellationToken = default)
        {
            // does nothing by default
            return Task.CompletedTask;
        }

        #endregion

        #region IAsyncDisposable Members

        /// <summary>
        /// Освобождение занятых ресурсов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        public virtual async ValueTask DisposeAsync()
        {
            this.processManager?.Dispose();
            this.processManager = null;
        }

        #endregion
    }
}
