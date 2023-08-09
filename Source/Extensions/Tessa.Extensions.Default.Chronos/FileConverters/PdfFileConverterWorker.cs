#nullable enable
using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards.Caching;
using Tessa.FileConverters;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Объект, ответственный за преобразование файла в формат <see cref="FileConverterFormat.Pdf"/>
    /// посредством внешних программ, таких как OpenOffice или LibreOffice.
    /// </summary>
    /// <remarks>
    /// Наследники класса могут переопределять методы интерфейса, например, добавив к ним обработку файлов других форматов.
    /// Класс может также реализовывать <see cref="IAsyncDisposable"/> для очистки ресурсов,
    /// для этого в наследнике переопределяется метод <see cref="DisposeAsync"/> и вызывается сначала его базовая реализация.
    /// </remarks>
    public class PdfFileConverterWorker :
        IFileConverterWorker,
        IAsyncDisposable
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="createProcessManagerFunc">Объект для запуска дочерних процессов.</param>
        /// <param name="cardCache"><inheritdoc cref="ICardCache" path="/summary"/></param>
        /// <param name="onlyOfficeServicePdfWorker"><inheritdoc cref="FileConverterWorkerNames.OnlyOfficeServiceToPdf" path="/summary"/></param>
        /// <param name="onlyOfficeDocumentBuilderPdfWorker"><inheritdoc cref="FileConverterWorkerNames.OnlyOfficeDocumentBuilderToPdf" path="/summary"/></param>
        /// <param name="tiffToPdfWorker"><inheritdoc cref="FileConverterWorkerNames.TiffToPdf" path="/summary"/></param>
        /// <param name="htmlToPdfWorker"><inheritdoc cref="FileConverterWorkerNames.HtmlToPdf" path="/summary"/></param>
        public PdfFileConverterWorker(
            Func<IProcessManager> createProcessManagerFunc,
            ICardCache cardCache,
            [OptionalDependency(FileConverterWorkerNames.OnlyOfficeServiceToPdf)]
            IFileConverterWorker? onlyOfficeServicePdfWorker = null,
            [OptionalDependency(FileConverterWorkerNames.OnlyOfficeDocumentBuilderToPdf)]
            IFileConverterWorker? onlyOfficeDocumentBuilderPdfWorker = null,
            [OptionalDependency(FileConverterWorkerNames.TiffToPdf)]
            IFileConverterWorker? tiffToPdfWorker = null,
            [OptionalDependency(FileConverterWorkerNames.HtmlToPdf)]
            IFileConverterWorker? htmlToPdfWorker = null)
        {
            this.processManager = new LazyProcessManager(NotNullOrThrow(createProcessManagerFunc));
            this.cardCache = NotNullOrThrow(cardCache);
            this.onlyOfficeServicePdfWorker = onlyOfficeServicePdfWorker;
            this.onlyOfficeDocumentBuilderPdfWorker = onlyOfficeDocumentBuilderPdfWorker;
            this.tiffToPdfWorker = tiffToPdfWorker;
            this.htmlToPdfWorker = htmlToPdfWorker;
        }

        #endregion

        #region Fields

        private IProcessManager? processManager;

        private readonly ICardCache cardCache;

        private readonly IFileConverterWorker? onlyOfficeServicePdfWorker;

        private readonly IFileConverterWorker? onlyOfficeDocumentBuilderPdfWorker;

        private readonly IFileConverterWorker? tiffToPdfWorker;

        private readonly IFileConverterWorker? htmlToPdfWorker;

        /// <summary>
        /// Текст ошибки, которая должна выбрасываться при попытке конвертировать файл PDF через OpenOffice/LibreOffice,
        /// или <c>null</c>, если ошибок нет и конвертацию можно выполнять.
        /// </summary>
        private string? openOfficeErrorText; // = null

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Environment variables

        private const string UnoconvExternalCommandSettingName = "UnoconvExternalCommand";


        private static string? unoconvExternalCommand;

        /// <summary>
        /// Путь к внешнему исполняемому файлу "unoconv", который будет использоваться вместо OpenOffice.
        /// </summary>
        private static string UnoconvExternalCommand =>
            unoconvExternalCommand ??= Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.Settings.TryGet<string>(UnoconvExternalCommandSettingName) ??
                    string.Empty)
                .Trim();


        private const string OpenOfficePythonSettingName = "OpenOfficePython";


        private static string? openOfficePython;

        /// <summary>
        /// Путь к исполняемому файлу "OpenOffice".
        /// </summary>
        private static string OpenOfficePython
        {
            get
            {
                InitializeOpenOfficePythonPath();
                return openOfficePython;
            }
        }


        private static string? openOfficePythonParamsPrefix;

        /// <summary>
        /// Дополнительные параметры, передаваемые в команду "OpenOffice" перед параметрами unoconv.
        /// Равны либо пустой строке (нет параметров), либо параметрам плюс завершающий пробел.
        /// </summary>
        private static string OpenOfficePythonParamsPrefix
        {
            get
            {
                InitializeOpenOfficePythonPath();
                return openOfficePythonParamsPrefix;
            }
        }


        private static bool openOfficePythonPathInitialized; // = false

        [MemberNotNull(nameof(openOfficePython))]
        [MemberNotNull(nameof(openOfficePythonParamsPrefix))]
        private static void InitializeOpenOfficePythonPath()
        {
            if (openOfficePythonPathInitialized)
            {
                Debug.Assert(openOfficePython is not null);
                Debug.Assert(openOfficePythonParamsPrefix is not null);
                return;
            }

            openOfficePythonPathInitialized = true;

            string command = Environment.ExpandEnvironmentVariables(
                    ConfigurationManager.Settings.TryGet<string>(OpenOfficePythonSettingName) ?? string.Empty)
                .Trim();

            int separatorIndex = command.IndexOf('|', StringComparison.Ordinal);
            if (separatorIndex <= 0 || separatorIndex == command.Length - 1)
            {
                openOfficePython = command;
                openOfficePythonParamsPrefix = string.Empty;
            }
            else
            {
                openOfficePython = command[..separatorIndex].Trim();
                openOfficePythonParamsPrefix = command[(separatorIndex + 1)..].Trim() + " ";
            }
        }

        #endregion

        #region UnoconvPath Private Property

        private static string? unoconvPath;

        private static string UnoconvPath => unoconvPath ??= GetUnoconvPath();

        private static string GetUnoconvPath()
        {
            // location - путь до сборки с плагином, в этой же папке должен лежать unoconv
            string location = typeof(PdfFileConverterWorker).Assembly.GetActualLocationFolder();
            return string.IsNullOrEmpty(location) ? string.Empty : Path.Combine(location, "unoconv");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Завершаем экземпляр OpenOffice или LibreOffice в этой сессии.
        /// </summary>
        private static void KillSessionOfficeInstance()
        {
            Process? process = GetOfficeProcess();
            if (process is not null && process.SessionId == Process.GetCurrentProcess().SessionId)
            {
                logger.Trace(
                    "There is an Office process found PID={0} in current session {1}. It would be terminated to avoid problems.",
                    process.Id,
                    process.SessionId);

                try
                {
                    process.Kill();
                    logger.Trace("Process is terminated PID={0}.", process.Id);
                }
                catch
                {
                    // ignored
                }
            }
        }


        /// <summary>
        /// Возвращает рабочий процесс OpenOffice или LibreOffice.
        /// </summary>
        /// <returns>Рабочий процесс OpenOffice или LibreOffice.</returns>
        private static Process? GetOfficeProcess()
        {
            Process[] processesList = Process.GetProcessesByName("soffice.bin");
            return processesList.Length != 0 ? processesList[0] : null;
        }


        /// <summary>
        /// Возвращает запускающий процесс OpenOffice или LibreOffice, который относится к текущей сессии
        /// и будет закрыт при завершении работы.
        ///
        /// Если процессы запускаются на .NET Framework в пределах того же WinAPI Job,
        /// то такой процесс будет закрыт автоматически, но для .NET Core необходимо закрыть его вручную.
        /// </summary>
        /// <returns>Запускающий процесс OpenOffice или LibreOffice.</returns>
        private static Process? GetCurrentSessionOfficeDisposableProcess()
        {
            int sessionId = Process.GetCurrentProcess().SessionId;
            return Process.GetProcessesByName("soffice.exe").FirstOrDefault(x => x.SessionId == sessionId)
                ?? Process.GetProcessesByName("soffice.bin").FirstOrDefault(x => x.SessionId == sessionId);
        }


        /// <summary>
        /// Завершает запускающий процесс OpenOffice или LibreOffice,
        /// поиск которого выполняется методом <see cref="GetCurrentSessionOfficeDisposableProcess"/>.
        /// </summary>
        private static void KillOfficeDisposableProcess()
        {
            try
            {
                Process? officeDisposableProcess = GetCurrentSessionOfficeDisposableProcess();
                if (officeDisposableProcess is not null)
                {
                    logger.Trace(
                        "Terminating Office process PID={0} in current session {1}.",
                        officeDisposableProcess.Id,
                        officeDisposableProcess.SessionId);

                    try
                    {
                        officeDisposableProcess.Kill();
                        logger.Trace("Process is terminated PID={0}.", officeDisposableProcess.Id);
                    }
                    catch (Exception ex)
                    {
                        logger.Warn("Error terminating Office process.");
                        logger.LogException(ex, LogLevel.Warn);
                    }
                }
            }
            catch
            {
                // ignore errors
            }
        }


        /// <summary>
        /// Выполняет чтение стандартного вывода System.Diagnostics.Process.StandardOutput
        /// и ошибок System.Diagnostics.Process.StandardError процесса process до их завершения
        /// (закрытия дескрипторов, что обычно происходит перед завершением процесса). Возвращает
        /// прочитанные значения.
        /// Используйте, чтобы не происходило переполнение буфера при выполнении процесса,
        /// вывод которого перенаправлен. Чтение гарантированно выполняется в другом потоке
        /// на пуле.
        /// </summary>
        /// <param name="process">Процесс, для которого выполняется чтение.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Стандартный вывод и ошибки процесса, прочитанные до их завершения.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static async Task<(string Output, string Error)> ReadOutputAndErrorToEndAsync(Process process, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            cancellationToken.ThrowIfCancellationRequested();
            string[] closure = ArrayPool<string>.Shared.Rent(2);
            closure[0] = string.Empty;
            closure[1] = string.Empty;
            try
            {
                await Task.WhenAll<string>(Task.Run(async delegate
                {
                    string[] array2 = closure;
                    return array2[0] = await process.StandardOutput.ReadToEndAsync(cancellationToken);
                }, cancellationToken), Task.Run(async delegate
                {
                    string[] array = closure;
                    return array[1] = await process.StandardError.ReadToEndAsync(cancellationToken);
                }, cancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (AggregateException)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw;
            }

            (string, string) result = (closure[0], closure[1]);
            ArrayPool<string>.Shared.Return(closure);
            return result;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Выполняется в методах <see cref="PreprocessAsync"/> или <see cref="PerformMaintenanceAsync"/>.
        /// </summary>
        protected virtual async Task PreprocessOrPerformMaintenanceCoreAsync(CancellationToken cancellationToken = default)
        {
            // завершаем предыдущий инстанс. это нестрашно ибо в одной сессии может быть только один инстанс
            KillSessionOfficeInstance();

            var startInfo = new ProcessStartInfo()
                .SetSilentExecution();

            string externalCommand = UnoconvExternalCommand;
            if (externalCommand.Length == 0)
            {
                startInfo
                    .SetApplication(
                        OpenOfficePython,
                        OpenOfficePythonParamsPrefix + $"\"{UnoconvPath}\" -l");
            }
            else
            {
                startInfo
                    .SetApplication(externalCommand, "-l");
            }

            logger.Trace(
                "Starting Office process via command line:{0}\"{1}\" {2}",
                Environment.NewLine,
                startInfo.FileName,
                startInfo.Arguments);

            this.processManager?.StartProcess(startInfo);

            // запускается небыстро, даем возможность
            int count = 10;

            Process? process;
            while ((process = GetOfficeProcess()) is null && count-- != 0)
            {
                await Task.Delay(500, cancellationToken);
            }

            if (process is not null)
            {
                logger.Trace("Office process has been started, PID={0}", process.Id);
            }
            else
            {
                logger.Trace("Office process can't be found, may be there is a problem with its configuration. Continuing anyway.");
            }
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
            var result = await this.cardCache.Cards.GetAsync("ServerInstance", cancellationToken);
            var fields = result.GetValue().Sections["ServerInstances"].RawFields;

            var converterType = (FileConverterType)fields.TryGet("FileConverterTypeID", (int)FileConverterType.LibreOffice);

            switch (converterType)
            {
                case FileConverterType.None:
                    throw new ArgumentOutOfRangeException(nameof(converterType), converterType, "File converter is not set.");

                case FileConverterType.LibreOffice:
                    {
                        switch (context.InputExtension)
                        {
                            case "pdf":
                                // файл PDF можно "конвертировать" в pdf, просто скопировав
                                await FileHelper.CopyAsync(context.InputFilePath, context.OutputFilePath, cancellationToken);
                                return;

                            case "tif":
                            case "tiff":
                                // файлы TIFF конвертируются отдельным worker-ом, если он задан
                                if (this.tiffToPdfWorker is not null)
                                {
                                    await this.tiffToPdfWorker.ConvertFileAsync(context, cancellationToken);
                                }
                                else
                                {
                                    context.ValidationResult.AddError(this, "Can't convert from TIFF without registered worker.");
                                }

                                return;

                            case "htm":
                            case "html":
                                // файлы HTML конвертируются отдельным worker-ом, если он задан
                                if (this.htmlToPdfWorker is not null)
                                {
                                    await this.htmlToPdfWorker.ConvertFileAsync(context, cancellationToken);
                                }
                                else
                                {
                                    context.ValidationResult.AddError(this, "Can't convert from HTML without registered worker.");
                                }

                                return;
                        }

                        // все остальные файлы конвертируются средствами OpenOffice/LibreOffice

                        if (this.openOfficeErrorText is not null)
                        {
                            // какая-то ошибка возникла при инициализации PDF
                            context.ValidationResult.AddError(this, this.openOfficeErrorText);
                            return;
                        }

                        IProcessManager processManager = this.processManager
                            ?? throw new ObjectDisposedException(this.GetType().FullName);

                        string externalCommand = UnoconvExternalCommand;
                        var startInfo = new ProcessStartInfo
                        {
                            CreateNoWindow = true,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };

                        if (externalCommand.Length == 0)
                        {
                            startInfo.FileName = OpenOfficePython;
                            startInfo.Arguments = $"{OpenOfficePythonParamsPrefix}\"{UnoconvPath}\" -f {context.OutputExtension} -o \"{context.OutputFilePath}\" \"{context.InputFilePath}\"";
                        }
                        else
                        {
                            startInfo.FileName = externalCommand;
                            startInfo.Arguments = $"-f {context.OutputExtension} -o \"{context.OutputFilePath}\" \"{context.InputFilePath}\"";
                        }

                        logger.Trace("Converting file via command line:{0}\"{1}\" {2}", Environment.NewLine, startInfo.FileName, startInfo.Arguments);

                        string output, error;
                        int exitCode;
                        using (Process process = processManager.StartProcess(startInfo))
                        {
                            (output, error) = await ReadOutputAndErrorToEndAsync(process, cancellationToken);
                            await process.WaitForExitAsync(cancellationToken);

                            exitCode = process.ExitCode;
                        }

                        if (exitCode != 0)
                        {
                            output = (output.Trim() + Environment.NewLine + error.Trim()).Trim();
                            if (output.Length == 0)
                            {
                                output = "Unknown error";
                            }

                            context.ValidationResult.AddError(this,
                                "File conversion failed. Exit code: {0}. Output:{1}{2}",
                                exitCode, Environment.NewLine, output);

                            return;
                        }

                        if (!string.IsNullOrEmpty(context.OutputExtension))
                        {
                            if (externalCommand.Length > 0)
                            {
                                string filePath = context.OutputFilePath;
                                if (!File.Exists(filePath))
                                {
                                    context.OutputFilePath += "." + context.OutputExtension;
                                }
                            }
                            else
                            {
                                // unoconv добавляет от себя расширения в выходной папке
                                context.OutputFilePath += "." + context.OutputExtension;
                            }
                        }

                        logger.Trace("File has been converted successfully via command line.");

                        // пишем ключ, через который вызывающая сторона поймёт, что конвертация была выполнена посредством unoconv
                        context.ResponseInfo["unoconv"] = BooleanBoxes.True;

                        break;
                    }

                case FileConverterType.OnlyOfficeService:
                    if (this.onlyOfficeServicePdfWorker is null)
                    {
                        throw new NotSupportedException($"Converter {nameof(FileConverterType.OnlyOfficeService)} isn't registered");
                    }

                    await this.onlyOfficeServicePdfWorker.ConvertFileAsync(context, cancellationToken);
                    break;

                case FileConverterType.OnlyOfficeDocumentBuilder:
                    if (this.onlyOfficeDocumentBuilderPdfWorker is null)
                    {
                        throw new NotSupportedException($"Converter {nameof(FileConverterType.OnlyOfficeDocumentBuilder)} isn't registered");
                    }

                    await this.onlyOfficeDocumentBuilderPdfWorker.ConvertFileAsync(context, cancellationToken);
                    break;
            }
        }


        /// <summary>
        /// Выполняет обработку перед запуском цикла обслуживания для очереди на конвертацию файлов.
        /// Метод запускается единственный раз при старте сервиса конвертации.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public virtual async Task PreprocessAsync(CancellationToken cancellationToken = default)
        {
            string openOfficePython = OpenOfficePython;
            string unoconvPath = UnoconvPath;

            // вместо пути к python может быть указана глобальная команда из %path%, например, "python3", которая сама подтягивается из окружения;
            // в этом случае нельзя проверять наличие файла, просто "доверимся" настройкам
            if (string.IsNullOrEmpty(openOfficePython)
                || Path.GetFileName(openOfficePython) != openOfficePython && !File.Exists(openOfficePython))
            {
                this.openOfficeErrorText = $"OpenOffice or LibreOffice \"{Path.GetFileName(openOfficePython)}\" isn't found: "
                    + (string.IsNullOrEmpty(openOfficePython) ? "<value is empty>" : openOfficePython);
            }
            else if (string.IsNullOrEmpty(unoconvPath) || !File.Exists(unoconvPath))
            {
                this.openOfficeErrorText = "Conversion script \"unoconv\" isn't found: "
                    + (string.IsNullOrEmpty(unoconvPath) ? "<value is empty>" : unoconvPath);
            }

            if (this.openOfficeErrorText is null)
            {
                await this.PreprocessOrPerformMaintenanceCoreAsync(cancellationToken);
            }
            else
            {
                logger.Warn(this.openOfficeErrorText);
            }

            if (this.tiffToPdfWorker is not null)
            {
                await this.tiffToPdfWorker.PreprocessAsync(cancellationToken);
            }

            if (this.htmlToPdfWorker is not null)
            {
                await this.htmlToPdfWorker.PreprocessAsync(cancellationToken);
            }

            if (this.onlyOfficeServicePdfWorker is not null)
            {
                await this.onlyOfficeServicePdfWorker.PreprocessAsync(cancellationToken);
            }

            if (this.onlyOfficeDocumentBuilderPdfWorker is not null)
            {
                await this.onlyOfficeDocumentBuilderPdfWorker.PreprocessAsync(cancellationToken);
            }
        }


        /// <summary>
        /// Выполняет обработку в процессе выполнения цикла обслуживания для очереди на конвертацию файлов.
        /// Метод запускается множество раз внутри цикла с переидичностью, заданной в конфигурационном файле.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public virtual async Task PerformMaintenanceAsync(CancellationToken cancellationToken = default)
        {
            if (this.openOfficeErrorText is null)
            {
                await this.PreprocessOrPerformMaintenanceCoreAsync(cancellationToken);
            }

            if (this.tiffToPdfWorker is not null)
            {
                await this.tiffToPdfWorker.PerformMaintenanceAsync(cancellationToken);
            }

            if (this.htmlToPdfWorker is not null)
            {
                await this.htmlToPdfWorker.PerformMaintenanceAsync(cancellationToken);
            }
        }

        #endregion

        #region IAsyncDisposable Members

        /// <summary>
        /// Освобождение занятых ресурсов.
        /// Метод принудительно закрывает все запущенные процессы.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        public virtual async ValueTask DisposeAsync()
        {
            logger.Trace("Shutting down file converter. All child processes will be terminated.");

            this.processManager?.Dispose();
            this.processManager = null;

            // процесс Office уже может быть остановлен здесь, но для .NET Core убиваем его явно
            KillOfficeDisposableProcess();

            switch (this.tiffToPdfWorker)
            {
                case IAsyncDisposable asyncDisposable:
                    await asyncDisposable.DisposeAsync();
                    break;

                // ReSharper disable once SuspiciousTypeConversion.Global
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }

            switch (this.htmlToPdfWorker)
            {
                case IAsyncDisposable asyncDisposable:
                    await asyncDisposable.DisposeAsync();
                    break;

                // ReSharper disable once SuspiciousTypeConversion.Global
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }
        }

        #endregion
    }
}
