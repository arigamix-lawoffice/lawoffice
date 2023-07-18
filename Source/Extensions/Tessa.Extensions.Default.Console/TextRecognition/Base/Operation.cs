#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.TextRecognition;

namespace Tessa.Extensions.Default.Console.TextRecognition.Base
{
    /// <summary>
    /// Базовая операция распознавания файла.
    /// </summary>
    /// <typeparam name="TContext">Тип контекста операции распознавания файла.</typeparam>
    /// <typeparam name="TRequest">Тип запроса на создание операции распознавания файла.</typeparam>
    /// <typeparam name="TResponse">Тип ответа на результат операции распознавания файла.</typeparam>
    public abstract class Operation<TContext, TRequest, TResponse>
        : ConsoleOperation<TContext>
        where TContext: OperationContext
    {
        #region Fields

        protected readonly IOcrService<TRequest, TResponse> ocrService;

        #endregion

        #region Constructors

        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            IOcrService<TRequest, TResponse> ocrService,
            bool extendedInitialization = false)
            : base(logger, sessionManager, extendedInitialization)
        {
            this.ocrService = NotNullOrThrow(ocrService);
        }

        #endregion

        #region Protected Methods

        protected abstract Task<Guid?> CreateOperationAsync(TContext context, CancellationToken cancellationToken = default);

        protected async Task CancelOperationAsync(Guid operationID, TContext context, CancellationToken cancellationToken = default)
        {
            await this.Logger.InfoAsync("Try cancel OCR operation...");

            var cancelledOperationID = await this.ocrService.CancelOperationAsync(operationID, context.ValidationResult, cancellationToken);
            var result = context.ValidationResult.Build();

            if (!result.IsSuccessful)
            {
                await this.Logger.WriteLineAsync();
                await this.Logger.LogResultAsync(result);
            }
            else if (!cancelledOperationID.HasValue)
            {
                await this.Logger.WriteLineAsync();
                await this.Logger.ErrorAsync("An error occurred while cancelling OCR operation. Cancelled OCR operation identifier is empty.");
            }
        }

        protected async Task<bool> WaitOperationAsync(Guid operationID, TContext context, CancellationToken cancellationToken = default)
        {
            await this.Logger.WriteLineAsync();
            await this.Logger.InfoAsync("Start monitoring OCR operation progress...");
            await this.Logger.InfoAsync("Press Ctrl + C or Ctrl + Break to stop and cancel operation.");
            await this.Logger.WriteLineAsync();

            try
            {
                int progress = 0, delay = 500;
                while (progress < 100)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var ocrOperationProgress = await this.ocrService.GetOperationProgressAsync(operationID, context.ValidationResult, cancellationToken);
                    var result = context.ValidationResult.Build();

                    if (!result.IsSuccessful)
                    {
                        await this.Logger.WriteLineAsync();
                        await this.Logger.LogResultAsync(result);
                        return false;
                    }
                    else if (!ocrOperationProgress.HasValue)
                    {
                        await this.Logger.WriteLineAsync();
                        await this.Logger.ErrorAsync("An error occurred while getting progress of OCR operation. OCR operation progress is empty.");
                        return false;
                    }
                    else if (ocrOperationProgress > progress)
                    {
                        progress = ocrOperationProgress.Value;
                        await this.Logger.WriteAsync($"\rCurrent progress: {progress}%");
                    }
                    else
                    {
                        delay = (int) Math.Min(delay * 1.3f, 10_000);
                        await Task.Delay(delay, cancellationToken);
                    }
                }

                await this.Logger.WriteLineAsync();
                return true;
            }
            catch (OperationCanceledException)
            {
                await this.Logger.WriteLineAsync();
                await this.Logger.InfoAsync("Requested OCR operation cancellation.");
                await CancelOperationAsync(operationID, context, CancellationToken.None);

                throw;
            }
        }

        protected async Task<TResponse?> GetOperationResultAsync(Guid operationID, TContext context, CancellationToken cancellationToken = default)
        {
            await this.Logger.WriteLineAsync();
            await this.Logger.InfoAsync("Try get result for OCR operation...");

            var response = await this.ocrService.GetOperationResultAsync(operationID, context.ValidationResult, cancellationToken);
            var result = context.ValidationResult.Build();

            if (!result.IsSuccessful)
            {
                await this.Logger.WriteLineAsync();
                await this.Logger.LogResultAsync(result);
                return default(TResponse);
            }
            else if (response is null)
            {
                await this.Logger.WriteLineAsync();
                await this.Logger.ErrorAsync("An error occurred while getting result of OCR operation. OCR operation result is empty.");
                return default(TResponse);
            }

            return response;
        }

        #endregion
    }
}
