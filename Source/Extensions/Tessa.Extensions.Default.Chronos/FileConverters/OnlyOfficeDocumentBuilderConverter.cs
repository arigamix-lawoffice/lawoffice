#nullable enable
using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.OnlyOffice;
using Tessa.FileConverters;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Объект, ответственный за преобразование файла в формат <see cref="FileConverterFormat.Pdf"/>
    /// посредством сервиса конвертации OnlyOffice
    /// </summary>
    /// <remarks>
    /// Наследники класса могут переопределять методы интерфейса, например, добавив к ним обработку файлов других форматов.
    /// Класс может также реализовывать <see cref="IAsyncDisposable"/> для очистки ресурсов,
    /// для этого в наследнике переопределяется метод <see cref="DisposeAsync"/> и вызывается сначала его базовая реализация.
    /// </remarks>
    public class OnlyOfficeDocumentBuilderConverter :
        IFileConverterWorker,
        IAsyncDisposable
    {
        #region Fields

        private readonly IOnlyOfficeSettingsProvider settingsProvider;

        private IProcessManager? processManager;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public OnlyOfficeDocumentBuilderConverter(
            IOnlyOfficeSettingsProvider settingsProvider,
            Func<IProcessManager> createProcessManagerFunc)
        {
            this.settingsProvider = NotNullOrThrow(settingsProvider);
            this.processManager = new LazyProcessManager(NotNullOrThrow(createProcessManagerFunc));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Получает текст с сообщениями об ошибках в выходном файле.
        /// </summary>
        /// <param name="errorFile">Выходной файл с ошибками.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Текст с сообщениями об ошибках.</returns>
        private static async Task<string?> GetErrorTextAsync(ITempFile errorFile, CancellationToken cancellationToken = default)
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

            if (errorText is not null)
            {
                errorText = errorText.Trim();
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
            logger.Trace("Start converting");

            var processManager = this.processManager ??
                throw new ObjectDisposedException(this.GetType().FullName);

            var settings = await this.settingsProvider.GetSettingsAsync(cancellationToken);
            var documentBuilderPath = NotNullOrThrow(settings.DocumentBuilderPath);

            using ITempFile errorFile = TempFile.Acquire("error.txt");

            // wkhtmltopdf производит конвертацию файла на основе его расширения.
            // Передать его в качестве аргумента нельзя, так что приходится временно переименовывать файл.
            var previousPath = context.InputFilePath;
            var newFilePath = $"{context.InputFilePath}.{context.InputExtension}";
            File.Move(previousPath, newFilePath);

            var script = @$"
builder.OpenFile(""{newFilePath}"");
builder.SaveFile(""pdf"", ""{context.OutputFilePath}"");
builder.CloseFile();
";

            using ITempFile scriptFile = TempFile.Acquire("script");
            await File.WriteAllTextAsync(scriptFile.Path, script, Encoding.UTF8, cancellationToken);

            try
            {
                var startInfo = new ProcessStartInfo()
                    .SetSilentExecution()
                    .SetCommandLine(
                        documentBuilderPath,
                        scriptFile.Path,
                        outputFile: errorFile.Path,
                        errorFile: errorFile.Path);

                using Process process = processManager.StartProcess(startInfo);
                await process.WaitForExitAsync(cancellationToken);

                var errorText = await GetErrorTextAsync(errorFile, cancellationToken);
                if (process.ExitCode != 0 || errorText?.Contains("error", StringComparison.OrdinalIgnoreCase) == true)
                {
                    context.ValidationResult.AddError(this,
                        "Conversion failed. Exit code: {0}. Output:{1}{2}",
                        process.ExitCode, Environment.NewLine, errorText);

                    return;
                }

                logger.Trace("PDF file is generated from {0}.", context.InputExtension);

                // пишем ключ, через который вызывающая сторона поймёт, что конвертация была выполнена через наш конвертер
                context.ResponseInfo[FileConverterWorkerNames.OnlyOfficeDocumentBuilderToPdf] = BooleanBoxes.True;
            }
            finally
            {
                File.Move(newFilePath, previousPath);
            }

            logger.Trace("End converting");
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
