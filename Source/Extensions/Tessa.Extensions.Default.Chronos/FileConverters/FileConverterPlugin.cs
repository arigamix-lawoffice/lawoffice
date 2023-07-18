using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Chronos.Plugins;
using NLog;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Server.OnlyOffice;
using Tessa.FileConverters;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Operations;
using Tessa.Platform.Runtime;
using Tessa.Platform.Scopes;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Делает преобразование файлов, сохраняет их в карточку кэша
    /// </summary>
    [Plugin(
        Name = "File converter plugin",
        Description = "Convert files to specific formats and stores them to the cache card",
        Version = 1,
        ConfigFile = ConfigFilePath)]
    public sealed class FileConverterPlugin :
        Plugin
    {
        #region Nested Types

        private record Dependencies(
            IDbScope DbScope,
            IOperationRepository OperationRepository,
            IErrorManager ErrorManager,
            IExtensionContainer ExtensionContainer,
            IFileConverterComposer FileConverterComposer,
            IFileConverterCache FileConverterCache,
            ICardStreamServerRepository CardStreamServerRepository,
            ICardServerPermissionsProvider PermissionsProvider,
            ICardCache CardCache);

        #endregion

        #region Fields

        private IUnityContainer container;

        private IFileConverterWorker fileConverterWorker;

        private Dependencies dependencies;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly string tempPathBase = Path.Combine(FileHelper.GetPath(FileSpecialFolder.TempBase), "FileConverter");

        private static readonly string tempPathInput = Path.Combine(tempPathBase, "input");

        private static readonly string tempPathOutput = Path.Combine(tempPathBase, "output");

        #endregion

        #region Constants

        /// <summary>
        /// Относительный путь к конфигурационному файлу плагина.
        /// </summary>
        private const string ConfigFilePath = "configuration/FileConverter.xml";

        #endregion

        #region Private Members

        private async Task PerformOperationCycleAsync(CancellationToken cancellationToken = default)
        {
            // очистка кэша будет запущена сразу же после старта плагина
            // обслуживание (например, перезапуск процесса Office) будет выполняться периодически, но не сразу же

            DateTime utcNow = DateTime.UtcNow;
            DateTime lastCacheCleanDate = DateTime.MinValue;
            DateTime lastMaintenanceDate = utcNow;

            lastCacheCleanDate = await this.CheckCleanCacheAsync(utcNow, lastCacheCleanDate, cancellationToken);

            IDbScopeInstance dbScopeInstance = null;
            int dbCounter = 0;
            const int maxDbCounter = 10;

            try
            {
                dbScopeInstance = this.dependencies.DbScope.Create();

                while (true)
                {
                    if (this.StopRequested)
                    {
                        return;
                    }

                    utcNow = DateTime.UtcNow;
                    bool wait = false;
                    bool recoveryAfterError = false;

                    if (dbCounter++ >= maxDbCounter)
                    {
                        dbCounter = 0;
                        await dbScopeInstance.DisposeAsync();

                        dbScopeInstance = this.dependencies.DbScope.Create();
                    }

                    bool processed;

                    try
                    {
                        processed = await this.ProcessOperationAsync(cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        logger.LogException(ex);

                        processed = false;
                        wait = true;
                        recoveryAfterError = true;
                    }

                    if (processed)
                    {
                        lastCacheCleanDate = await this.CheckCleanCacheAsync(utcNow, lastCacheCleanDate, cancellationToken);
                    }
                    else
                    {
                        bool performed = false;

                        try
                        {
                            (performed, lastMaintenanceDate) = await this.CheckPerformMaintenanceAsync(utcNow, lastMaintenanceDate, cancellationToken);

                            if (performed)
                            {
                                // пересоздаём контейнер и все зависимости
                                logger.Trace("Performing maintenance: recreating services");
                                IUnityContainer container = await CreateUnityContainerAsync(cancellationToken);

                                // пишем Resolve, а не TryResolve: IFileConverterWorker гарантированно зарегистрирован в предыдущем контейнере, т.к. мы дошли до этого метода;
                                // новый контейнер не будет отличаться от предыдущего, поэтому в нём также должен быть инстанс
                                var fileConverterWorker = container.Resolve<IFileConverterWorker>();
                                var dependencies = ResolveUnityDependencies(container);

                                // ошибки в Dispose пишем, но игнорируем, т.к. новые зависимости уже подготовлены,
                                // а на полуразрушенных старых не продолжишь работу плагина
                                logger.Trace("Performing maintenance: finalizing old services");

                                try
                                {
                                    // коннекшен к БД сразу закрываем до финализации контейнера
                                    await dbScopeInstance.DisposeAsync();
                                }
                                catch (Exception ex)
                                {
                                    logger.LogException("Performing maintenance: error while finalizing database connection", ex, LogLevel.Warn);
                                }

                                try
                                {
                                    await DisposeFileConverterWorkerAsync(this.fileConverterWorker);
                                }
                                catch (Exception ex)
                                {
                                    logger.LogException(
                                        $"Performing maintenance: error while finalizing \"{this.fileConverterWorker.GetType().FullName}\"",
                                        ex,
                                        LogLevel.Warn);
                                }

                                try
                                {
                                    await this.container.DisposeAllRegistrationsAsync();
                                }
                                catch (Exception ex)
                                {
                                    logger.LogException("Performing maintenance: error while finalizing services", ex, LogLevel.Warn);
                                }

                                // все действия успешные, сохраняем в this зависимости
                                this.container = container;
                                this.dependencies = dependencies;
                                this.fileConverterWorker = fileConverterWorker;

                                // самое время провести полную сборку мусора, удалив старый контейнер из памяти
                                logger.Trace("Performing maintenance: garbage collection");
                                GCHelper.CollectAll(compactLargeObjectHeap: true);

                                // на следующей итерации пересоздаём dbScopeInstance, он будет вызван из нового контейнера,
                                // и ссылок на старый контейнер не должно остаться;

                                // это обеспечивается за счёт того, что ниже мы попадём в else-ветку if (wait)
                                
                                await fileConverterWorker.PreprocessAsync(cancellationToken);
                            }
                            else
                            {
                                wait = true;
                            }
                        }
                        finally
                        {
                            if (performed)
                            {
                                logger.Info("Performing maintenance: completed");
                            }
                        }
                    }

                    if (wait)
                    {
                        if (recoveryAfterError)
                        {
                            // возвращаем коннекшен в пул, поскольку ждать будем долго
                            dbCounter = 0;
                            await dbScopeInstance.DisposeAsync();

                            dbScopeInstance = this.dependencies.DbScope.Create();
                        }

                        int waitCount = recoveryAfterError ? 60 : 1;
                        for (int i = 0; i < waitCount; i++)
                        {
                            if (this.StopRequested)
                            {
                                // в случае ожидания после ошибки - сюда будем часто попадать, и при вежливой остановке плагина надо скорее завершаться
                                return;
                            }

                            await Task.Delay(1000, cancellationToken);

                            if (this.StopRequested)
                            {
                                // в случае ожидания после ошибки - сюда будем часто попадать, и при вежливой остановке плагина надо скорее завершаться
                                return;
                            }
                        }
                    }
                    else
                    {
                        // на следующей итерации пересоздаём dbScope
                        dbCounter = maxDbCounter;
                    }
                }
            }
            finally
            {
                if (dbScopeInstance != null)
                {
                    await dbScopeInstance.DisposeAsync();
                }
            }
        }


        private async Task<(bool performed, DateTime lastMaintenanceDate)> CheckPerformMaintenanceAsync(
            DateTime utcNow,
            DateTime lastMaintenanceDate,
            CancellationToken cancellationToken = default)
        {
            if (utcNow.Subtract(lastMaintenanceDate) <= FileConverterSettings.MaintenancePeriod)
            {
                return (false, lastMaintenanceDate);
            }

            DateTime resultingDateTime;

            try
            {
                logger.Info("Performing maintenance: started");
                await this.fileConverterWorker.PerformMaintenanceAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException(ex, LogLevel.Warn);
            }
            finally
            {
                resultingDateTime = utcNow;
            }

            return (true, resultingDateTime);
        }


        private async Task<DateTime> CheckCleanCacheAsync(DateTime utcNow, DateTime lastCacheCleanDate, CancellationToken cancellationToken = default)
        {
            DateTime resultingDate = lastCacheCleanDate;

            if (utcNow.Subtract(lastCacheCleanDate) > FileConverterSettings.CacheCleanPeriod)
            {
                try
                {
                    logger.Info("Cache cleaning: started");

                    ValidationResult cleanResult = await this.dependencies.FileConverterCache
                        .CleanCacheAsync(utcNow.Subtract(FileConverterSettings.OldestPreviewFilePeriod), cancellationToken);

                    logger.LogResult(cleanResult);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    logger.LogException(ex, LogLevel.Warn);
                }
                finally
                {
                    resultingDate = utcNow;
                    logger.Info("Cache cleaning: completed");
                }
            }

            return resultingDate;
        }

        private async Task<FileConverterType> GetConverterTypeAsync(CancellationToken cancellationToken = default)
        {
            var result = await this.dependencies.CardCache.Cards.GetAsync("ServerInstance", cancellationToken);
            var fields = result.GetValue().Sections["ServerInstances"].RawFields;

            return (FileConverterType) fields.TryGet("FileConverterTypeID", 1);
        }


        private async Task<bool> ProcessOperationAsync(CancellationToken cancellationToken = default)
        {
            Guid? operationID = await this.dependencies.OperationRepository.StartFirstAsync(OperationTypes.FileConvert, cancellationToken);
            if (!operationID.HasValue)
            {
                return false;
            }

            string inputFilePath = null;
            string outputFilePath = null;
            IFileConverterRequest request = null;

            try
            {
                // получаем параметры операции
                IOperation operation = await this.dependencies.OperationRepository.TryGetAsync(operationID.Value, cancellationToken: cancellationToken);
                if (operation == null)
                {
                    return false;
                }

                var validationResult = new ValidationResultBuilder();

                var converterType = await this.GetConverterTypeAsync(cancellationToken);
                request = new FileConverterRequest();

                if (converterType == FileConverterType.None)
                {
                    validationResult.AddError(this,
                        $"File Converter is not set");

                    await this.CompleteWithErrorAsync(operationID.Value, request, validationResult, cancellationToken);
                    return false;
                }

                request.Deserialize(operation.Request.Info);

                if (this.fileConverterWorker is IFileConverterAggregateWorker aggregateWorker
                    && !aggregateWorker.IsRegistered(request.OutputFormat))
                {
                    validationResult.AddError(this,
                        "Unsupported output format \"{0}\" while trying to convert file \"{1}\".",
                        request.OutputFormat, request.FileName);

                    await this.CompleteWithErrorAsync(operationID.Value, request, validationResult, cancellationToken);
                    return false;
                }

                if (!FileConverterFormat.IsSupportedConversion(request.OutputFormat, request.InputFormat))
                {
                    validationResult.AddError(this,
                        "Unsupported input format \"{0}\" while trying to convert file \"{1}\".",
                        request.InputFormat, request.FileName);

                    await this.CompleteWithErrorAsync(operationID.Value, request, validationResult, cancellationToken);
                    return false;
                }

                logger.Info("Converting file '{0}': cardID='{1}', fileID='{2}'", request.FileName, request.CardID, request.FileID);

                inputFilePath = Path.Combine(tempPathInput, request.VersionID.ToString());
                FileHelper.CreateDirectoryIfNotExists(tempPathInput);

                string suggestedName = null;

                if (converterType != FileConverterType.OnlyOfficeService)
                {
                    // получаем файл из карточки
                    var fileRequest = new CardGetFileContentRequest
                    {
                        CardID = request.CardID,
                        FileID = request.FileID,
                        VersionRowID = request.VersionID,
                        FileName = request.FileName,
                        FileTypeID = request.FileTypeID,
                        FileTypeName = request.FileTypeName,
                        CardTypeID = request.CardTypeID,
                        CardTypeName = request.CardTypeName,
                    };

                    if (!request.FileRequestInfo.IsEmpty())
                    {
                        fileRequest.Info = StorageHelper.Clone(request.FileRequestInfo.GetStorage());
                    }

                    // в момент открытия файла нам мог прийти или не прийти токен, здесь мы его выбрасываем и устанавливаем пермишены "всегда можно"
                    this.dependencies.PermissionsProvider.SetFullPermissions(fileRequest);

                    ICardFileContentResult fileResult = await this.dependencies.CardStreamServerRepository.GetFileContentAsync(fileRequest, cancellationToken);
                    CardGetFileContentResponse fileResponse = fileResult.Response;

                    ValidationResult result = fileResponse.ValidationResult.Build();
                    validationResult.Add(result);

                    if (!result.IsSuccessful || !fileResult.HasContent)
                    {
                        await this.CompleteWithErrorAsync(operationID.Value, request, validationResult, cancellationToken);
                        return false;
                    }

                    // копируем файл во временную папку

                    await using (Stream inputFileStream = FileHelper.Create(inputFilePath))
                    {
                        await using Stream contentStream = await fileResult.GetContentOrThrowAsync(cancellationToken);
                        await contentStream.CopyToAsync(inputFileStream, cancellationToken);
                    }

                    suggestedName = fileResponse.TryGetSuggestedFileName();
                }

                // преобразуем файл во временную папку
                Guid convertedFileID = Guid.NewGuid();
                outputFilePath = Path.Combine(tempPathOutput, convertedFileID.ToString());
                FileHelper.CreateDirectoryIfNotExists(tempPathOutput);

                // преобразуем файл с расширениями
                var context = new FileConverterContext(inputFilePath, outputFilePath, suggestedName, request, operation, cancellationToken);

                IDbScopeInstance dbScopeInstance = null;
                IExtensionExecutor executor = null;
                IInheritableScopeInstance<IScopeHolderContext> scopeInstance = null;

                try
                {
                    // расширения на конвертацию и fileConverterWorker будут выполняться в своём соединении, которое не зависит от внешнего цикла операций
                    dbScopeInstance = this.dependencies.DbScope.CreateNew();
                    executor = await this.dependencies.ExtensionContainer.ResolveExecutorAsync<IFileConverterExtension>(cancellationToken);
                    scopeInstance = ScopeHolderContext.Create();
                    RuntimeHelper.ServerRequestID = Guid.NewGuid();

                    try
                    {
                        await executor.ExecuteAsync(nameof(IFileConverterExtension.BeforeRequest), context);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        context.ValidationResult.AddException(this, ex);
                    }

                    context.RequestIsSuccessful = context.ValidationResult.IsSuccessful();

                    if (context.RequestIsSuccessful)
                    {
                        try
                        {
                            await this.fileConverterWorker.ConvertFileAsync(context, cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            context.ValidationResult.AddException(this, ex);
                        }
                    }

                    try
                    {
                        await executor.ExecuteAsync(nameof(IFileConverterExtension.AfterRequest), context);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        context.ValidationResult.AddException(this, ex);
                    }
                }
                finally
                {
                    RuntimeHelper.ServerRequestID = null;

                    scopeInstance?.Dispose();

                    if (executor != null)
                    {
                        await executor.DisposeAsync();
                    }

                    if (dbScopeInstance != null)
                    {
                        await dbScopeInstance.DisposeAsync();
                    }
                }

                validationResult.Add(context.ValidationResult);

                if (!context.ValidationResult.IsSuccessful())
                {
                    await this.CompleteWithErrorAsync(operationID.Value, request, validationResult, cancellationToken);
                    return false;
                }

                outputFilePath = context.OutputFilePath;

                // сохраняем файл в кеш с заданным идентификатором
                if (request.Flags.HasNot(FileConverterRequestFlags.DoNotCacheResult))
                {
                    byte[] requestHash = this.dependencies.FileConverterComposer.CalculateHash(request);

                    ValidationResult conversionResult = await this.dependencies.FileConverterCache
                        .StoreFileAsync(
                            versionID: request.VersionID,
                            requestHash: requestHash,
                            fileID: convertedFileID,
                            fileName: FileHelper.GetFileNameWithoutExtension(request.FileName, ignoreFolder: true)
                            + (string.IsNullOrEmpty(context.OutputExtension) ? null : "." + context.OutputExtension),
                            contentFilePath: outputFilePath,
                            responseInfo: context.ResponseInfo.GetStorage(),
                            cancellationToken: cancellationToken);

                    validationResult.Add(conversionResult);
                    if (!validationResult.IsSuccessful())
                    {
                        await this.CompleteWithErrorAsync(operationID.Value, request, validationResult, cancellationToken);
                        return false;
                    }
                }

                // завершаем операцию, указывая идентификатор файла в кэше
                await this.CompleteSuccessfulAsync(operationID.Value, request, convertedFileID, context, validationResult, cancellationToken);

                logger.Info("File has been converted '{0}': cardID='{1}', fileID='{2}'", request.FileName, request.CardID, request.FileID);
                return true;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await this.CompleteWithUnhandledExceptionAsync(operationID.Value, request, ex, cancellationToken);
                return false;
            }
            finally
            {
                DeleteFileSafe(inputFilePath);
                DeleteFileSafe(outputFilePath);
            }
        }


        private Task CompleteSuccessfulAsync(
            Guid operationID,
            IFileConverterRequest request,
            Guid convertedFileID,
            IFileConverterContext context,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            if (request != null && request.Flags.Has(FileConverterRequestFlags.WithoutResponse))
            {
                return this.DeleteOperationSafeAsync(operationID, cancellationToken);
            }

            var suggestedName = context.SuggestedName;
            if (!string.IsNullOrEmpty(suggestedName))
            {
                suggestedName = FileHelper.RemoveInvalidFileNameChars(
                    FileHelper.GetFileNameWithoutExtension(suggestedName, true),
                    FileHelper.InvalidCharReplacement) + "." + context.OutputExtension;
            }

            var response = new OperationResponse();
            response.ValidationResult.Add(validationResult);

            Dictionary<string, object> responseInfo = response.Info;
            responseInfo["FileID"] = convertedFileID;
            responseInfo["SuggestedName"] = suggestedName;
            responseInfo["ResponseInfo"] = StorageHelper.Clone(context.ResponseInfo.GetStorage());

            return this.dependencies.OperationRepository.CompleteAsync(operationID, OperationTypes.FileConvert, response, cancellationToken);
        }


        private async Task CompleteWithErrorAsync(
            Guid operationID,
            IFileConverterRequest request,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            logger.Error(
                "Error converting file '{0}'. cardID='{1}', fileID='{2}'.{3}{4}",
                request.FileName,
                request.CardID,
                request.FileID,
                Environment.NewLine,
                validationResult.Build().ToString(ValidationLevel.Detailed));

            await this.ReportErrorSafeAsync(validationResult.Build(), request);

            if (request.Flags.Has(FileConverterRequestFlags.WithoutResponse))
            {
                await this.DeleteOperationSafeAsync(operationID, cancellationToken);
            }

            var response = new OperationResponse();
            response.ValidationResult.Add(validationResult);

            await this.dependencies.OperationRepository.CompleteAsync(operationID, OperationTypes.FileConvert, response, cancellationToken);
        }


        private async Task CompleteWithUnhandledExceptionAsync(
            Guid operationID,
            IFileConverterRequest request,
            Exception ex,
            CancellationToken cancellationToken = default)
        {
            // ErrorException уже записывает ошибку в лог, не будем её дублировать

            var response = new OperationResponse();
            response.ValidationResult.AddException(this, ex);

            await this.ReportErrorSafeAsync(response.ValidationResult.Build(), request);

            if (request != null && request.Flags.Has(FileConverterRequestFlags.WithoutResponse))
            {
                await this.DeleteOperationSafeAsync(operationID, cancellationToken);
            }

            await this.dependencies.OperationRepository.CompleteAsync(operationID, OperationTypes.FileConvert, response, cancellationToken);
        }


        private Task ReportErrorSafeAsync(
            ValidationResult result,
            IFileConverterRequest request)
        {
            Guid cardID;
            string cardName;
            if (request != null)
            {
                cardID = request.CardID;
                cardName = request.EventName;
            }
            else
            {
                cardID = FileConverterHelper.CacheCardID;
                cardName = "Unhandled exception";
            }

            return this.dependencies.ErrorManager.ReportErrorSafeAsync(
                CardHelper.FileConverterCacheTypeID,
                cardID,
                cardName,
                new ErrorDescription(result, ErrorCategories.FileConverterFailed));
        }


        private async Task DeleteOperationSafeAsync(Guid operationID, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.dependencies.OperationRepository.DeleteAsync(operationID, OperationTypes.FileConvert, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException(
                    $"Error during removing file conversion operation ID={operationID:B}:",
                    ex, LogLevel.Warn);
            }
        }

        private static void DeleteFileSafe(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException(ex, LogLevel.Warn);
            }
        }

        private static async Task<IUnityContainer> CreateUnityContainerAsync(CancellationToken cancellationToken = default)
        {
            // поскольку в расширениях может быть переопределён FileConverterFormat.Pdf, то здесь корректно сначала вызвать
            // RegisterServerForPluginAsync, и потом уже RegisterWorker
            IUnityContainer container = (await new UnityContainer().RegisterServerForPluginAsync(cancellationToken: cancellationToken))

                    // регистрацию всех известных в типовом решении Worker-ов и их зависимостей выполняем здесь;

                    // если необходимо создать Worker в рамках проекта, то его можно указать в библиотеке расширений,
                    // например, в Tessa.Extensions.Server.dll, и в методе Registrator.FinalizeRegistration
                    // выполнить такую же регистрацию;

                    // если в одном из расширений уже зарегистрированы Worker-ы для этих форматов,
                    // то регистрация ниже их не перезапишет, т.к. мы не указали overwrite
                    .RegisterWorker<PdfFileConverterWorker>(FileConverterFormat.Pdf)
                ;

            if (!container.IsRegistered<IFileConverterWorker>(FileConverterWorkerNames.TiffToPdf))
            {
                container
                    .RegisterType<IFileConverterWorker, TiffToPdfFileConverterWorker>(
                        FileConverterWorkerNames.TiffToPdf,
                        new ContainerControlledLifetimeManager())
                    ;
            }

            if (!container.IsRegistered<IFileConverterWorker>(FileConverterWorkerNames.HtmlToPdf))
            {
                container
                    .RegisterType<IFileConverterWorker, HtmlToPdfFileConverterWorker>(
                        FileConverterWorkerNames.HtmlToPdf,
                        new ContainerControlledLifetimeManager())
                    ;
            }

            if (!container.IsRegistered<IFileConverterWorker>(FileConverterWorkerNames.OnlyOfficeServiceToPdf))
            {
                container
                    .RegisterFactory<IFileConverterWorker>(
                        FileConverterWorkerNames.OnlyOfficeServiceToPdf,
                        c => new OnlyOfficeServiceConverter(
                            c.Resolve<IOnlyOfficeSettingsProvider>(),
                            c.Resolve<IOnlyOfficeService>(),
                            c.Resolve<ICardStreamServerRepository>(CardRepositoryNames.Extended)),
                        new ContainerControlledLifetimeManager())
                    ;
            }

            if (!container.IsRegistered<IFileConverterWorker>(FileConverterWorkerNames.OnlyOfficeDocumentBuilderToPdf))
            {
                container
                    .RegisterFactory<IFileConverterWorker>(
                        FileConverterWorkerNames.OnlyOfficeDocumentBuilderToPdf,
                        c => c.Resolve<OnlyOfficeDocumentBuilderConverter>(),
                        new ContainerControlledLifetimeManager())
                    ;
            }

            return container;
        }

        private static Dependencies ResolveUnityDependencies(IUnityContainer container) =>
            new Dependencies(
                container.Resolve<IDbScope>(),
                container.Resolve<IOperationRepository>(),
                container.Resolve<IErrorManager>(),
                container.Resolve<IExtensionContainer>(),
                container.Resolve<IFileConverterComposer>(),
                container.Resolve<IFileConverterCache>(),
                container.Resolve<ICardStreamServerRepository>(),
                container.Resolve<ICardServerPermissionsProvider>(),
                container.Resolve<ICardCache>())
            ;

        private static ValueTask DisposeFileConverterWorkerAsync(IFileConverterWorker fileConverterWorker)
        {
            switch (fileConverterWorker)
            {
                case IAsyncDisposable asyncDisposable:
                    return asyncDisposable.DisposeAsync();

                // ReSharper disable once SuspiciousTypeConversion.Global
                case IDisposable disposable:
                    disposable.Dispose();
                    return ValueTask.CompletedTask;

                default:
                    return ValueTask.CompletedTask;
            }
        }

        #endregion

        #region Base Overrides

        public override async Task EntryPointAsync(CancellationToken cancellationToken = default)
        {
            logger.Info("Starting plugin");

            try
            {
                this.container = await CreateUnityContainerAsync(cancellationToken);

                this.fileConverterWorker = this.container.TryResolve<IFileConverterWorker>();
                if (this.fileConverterWorker != null)
                {
                    this.dependencies = ResolveUnityDependencies(this.container);

                    await this.fileConverterWorker.PreprocessAsync(cancellationToken);
                    await this.PerformOperationCycleAsync(cancellationToken);
                }
                else
                {
                    logger.Error(
                        "Can't find registration for {0}. Check if default extensions are registered.",
                        typeof(IFileConverterWorker).FullName);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            finally
            {
                await DisposeFileConverterWorkerAsync(this.fileConverterWorker);
            }

            logger.Info("File converter: shutdown completed");
        }


        public override Task StopAsync(IPluginStopToken token)
        {
            logger.Info("File converter: shutting down");
            return base.StopAsync(token);
        }

        #endregion
    }
}
