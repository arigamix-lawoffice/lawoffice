#nullable enable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.TextRecognition;
using Tessa.TextRecognition.Constants;

namespace Tessa.Extensions.Default.Console.TextRecognition.Synchronous
{
    /// <summary>
    /// Синхронная операция распознавания файла.
    /// </summary>
    public sealed class Operation : Base.Operation<OperationContext, OcrSyncRequest, OcrSyncResponse>
    {
        #region Constructors

        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            IOcrSyncService ocrService)
            : base(logger, sessionManager, ocrService, extendedInitialization: false)
        {

        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var operationID = await CreateOperationAsync(context, cancellationToken);
                if (!operationID.HasValue)
                {
                    return -1;
                }

                cancellationToken.ThrowIfCancellationRequested();
                var isSuccessful = await WaitOperationAsync(operationID.Value, context, cancellationToken);
                if (!isSuccessful)
                {
                    return -1;
                }

                cancellationToken.ThrowIfCancellationRequested();
                await using var result = await GetOperationResultAsync(operationID.Value, context, cancellationToken);
                if (result is null)
                {
                    return -1;
                }
                else if (result.ContentStream is null)
                {
                    await this.Logger.ErrorAsync("Result stream with recognized file content is empty for OCR operation");
                    return -1;
                }
                else if (result.MetadataStream is null)
                {
                    await this.Logger.ErrorAsync("Result stream with metadata file content is empty for OCR operation");
                    return -1;
                }

                // Копируем результат операции в целевую директорию
                var fileName = FileHelper.GetFileName(context.SourceFilePath);
                var fileNameWithoutExtension = FileHelper.GetFileNameWithoutExtension(fileName);
                var contentPath = Path.Combine(context.TargetFolderPath, $"{fileNameWithoutExtension}{OcrCommon.ContentFileExtension}");
                var metadataPath = Path.Combine(context.TargetFolderPath, $"{fileNameWithoutExtension}{OcrCommon.MetadataFileExtension}");

                await Task.WhenAll(
                    SaveFileAsync(result.ContentStream, contentPath, cancellationToken),
                    SaveFileAsync(result.MetadataStream, metadataPath, cancellationToken));

                await this.Logger.InfoAsync(
                    StringBuilderHelper.Acquire(256)
                        .AppendLine($"  - Output file path with recognized content: \"{contentPath}\",")
                        .AppendLine($"  - Output file path with recognized metadata: \"{metadataPath}\",")
                        .AppendLine(result.Info?.HasTextLayer ?? false
                            ? "  - Text layer has been detected at recognized files."
                            : "  - Text layer has not been detected at recognized files.")
                        .ToStringAndRelease());

                return 0;
            }
            catch (OperationCanceledException)
            {
                await this.Logger.InfoAsync("OCR operation was cancelled.");
                return 0;
            }
            catch (Exception ex)
            {
                await this.Logger.LogExceptionAsync("An error occurred during the file recognition process.", ex);
                return -1;
            }
        }

        /// <inheritdoc/>
        protected override async Task<Guid?> CreateOperationAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            await this.Logger.WriteLineAsync();
            await this.Logger.InfoAsync($"Try create OCR operation with parameters:{Environment.NewLine}{context.GetDescription()}");
            await this.Logger.WriteLineAsync();

            var fileName = FileHelper.GetFileName(context.SourceFilePath);
            await using var fileStream = FileHelper.OpenRead(context.SourceFilePath);
            var request = new OcrSyncRequest(fileName, fileStream, context.Parameters);
            var operationID = await this.ocrService.CreateOperationAsync(request, context.ValidationResult, cancellationToken);
            var result = context.ValidationResult.Build();

            if (!operationID.HasValue)
            {
                await this.Logger.ErrorAsync("An error occurred while creating OCR operation. OCR operation identifier is empty.");
                return null;
            }
            else if (!result.IsSuccessful)
            {
                await this.Logger.LogResultAsync(result);
                return null;
            }

            return operationID;
        }

        #endregion

        #region Private

        private static async Task SaveFileAsync(Stream source, string filePath, CancellationToken cancellationToken = default)
        {
            await using var contentStream = FileHelper.Create(filePath);
            await source.CopyToAsync(contentStream, cancellationToken);
        }

        #endregion
    }
}
