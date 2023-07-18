#nullable enable

using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.TextRecognition;
using Tessa.TextRecognition.Constants;
using Tessa.TextRecognition.Enums;
using Unity;
using Enums = Tessa.TextRecognition.Enums;

namespace Tessa.Extensions.Default.Console.TextRecognition.Asynchronous
{
    /// <summary>
    /// Асинхронная операция распознавания файла.
    /// </summary>
    public sealed class Operation : Base.Operation<OperationContext, OcrAsyncRequest, OcrAsyncResponse>
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly ISession session;
        private readonly ICardRepository cardRepository;
        private readonly ICardFileManager cardFileManager;

        #endregion

        #region Constructors

        public Operation(
            IDbScope dbScope,
            ISession session,
            [Dependency(CardRepositoryNames.Extended)]
            ICardRepository cardRepository,
            ICardFileManager cardFileManager,
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            IOcrAsyncService ocrService)
            : base(logger, sessionManager, ocrService, extendedInitialization: true)
        {
            this.dbScope = NotNullOrThrow(dbScope);
            this.session = NotNullOrThrow(session);
            this.cardRepository = NotNullOrThrow(cardRepository);
            this.cardFileManager = NotNullOrThrow(cardFileManager);
        }

        #endregion

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var operationID = await this.CreateOperationAsync(context, cancellationToken);
                if (!operationID.HasValue)
                {
                    return -1;
                }

                cancellationToken.ThrowIfCancellationRequested();
                var isSuccessful = await this.WaitOperationAsync(operationID.Value, context, cancellationToken);
                if (!isSuccessful)
                {
                    return -1;
                }

                cancellationToken.ThrowIfCancellationRequested();
                var result = await this.GetOperationResultAsync(operationID.Value, context, cancellationToken);
                if (result is null)
                {
                    return -1;
                }

                await this.Logger.InfoAsync(
                    StringBuilderHelper.Acquire(256)
                        .AppendLine($"  - Card file identifier with recognized content: \"{result.ContentFileID}\",")
                        .AppendLine($"  - Card file identifier with recognized metadata: \"{result.MetadataFileID}\",")
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

        /// <inheritdoc />
        protected override async Task<Guid?> CreateOperationAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            await this.Logger.WriteLineAsync();
            await this.Logger.InfoAsync($"Try create OCR operation with parameters:{Environment.NewLine}{context.GetDescription()}");
            await this.Logger.WriteLineAsync();

            var requestID = Guid.NewGuid();

            // Сначала попытаемся найти карточку операции OCR, проассоциированную с идентификатором версии файла
            var ocrCardID = await GetOcrCardIdAsync(context.FileID, cancellationToken);

            // Если карточка существует, то работаем с карточкой OCR (создаем запросы через нее)
            if (ocrCardID.HasValue)
            {
                var ocrCard = await this.GetCardAsync(ocrCardID.Value, context.ValidationResult, cancellationToken);
                if (ocrCard is not null)
                {
                    ocrCard.Sections[nameof(OcrRequests)].Rows.Add(CreateRequestRow(requestID, context.Parameters, this.session.User));
                    ocrCard.Sections[nameof(OcrRequestsLanguages)].Rows.AddRange(CreateRequestLanguagesRows(requestID, context.Parameters.Languages));
                    ocrCard.RemoveAllButChanged();

                    if (await this.StoreCardAsync(ocrCard, context.ValidationResult, cancellationToken))
                    {
                        return requestID;
                    }
                }

                await this.Logger.LogResultAsync(context.ValidationResult.Build());
                return null;
            }
            // В противном случае, запросы будем создавать через сохранение основной карточки
            else
            {
                var cardID = await GetCardIdAsync(context.FileID, cancellationToken);
                if (!cardID.HasValue)
                {
                    await this.Logger.ErrorAsync("An error occurred while creating OCR operation. Not found card identifier for file version identifier.");
                    return null;
                }

                var card = await this.GetCardAsync(cardID.Value, context.ValidationResult, cancellationToken);
                if (card is null)
                {
                    await this.Logger.LogResultAsync(context.ValidationResult.Build());
                    return null;
                }

                // Создание файлового контейнера для карточки
                await using var container = await this.cardFileManager.CreateContainerAsync(card, cancellationToken: cancellationToken);
                if (!container.CreationResult.IsSuccessful)
                {
                    await this.Logger.LogResultAsync(container.CreationResult);
                    return null;
                }

                // Установка параметров OCR в опциях файла, если их не было
                var cardFile = card.Files.First(f => f.RowID == context.FileID);
                cardFile.Info[OcrCommon.OcrKey] = BooleanBoxes.True;
                cardFile.State = CardFileState.Modified;

                var file = container.FileContainer.Files.First(f => f.ID == cardFile.RowID);
                if (!file.Options.TryGetValue(OcrCommon.OcrKey, out var options) || options is null)
                {
                    file.Options[OcrCommon.OcrKey] = new Dictionary<string, object> { ["CardID"] = Guid.NewGuid() };
                }
                await file.NotifyAsync(FileNotificationType.OptionsModified, cancellationToken);

                card.RemoveAllButChanged();

                // Сохранение карточки с измененным файлом
                var storeResponse = await container.StoreAsync((_, storeRequest, _) =>
                {
                    storeRequest.Info[OcrCommon.OcrKey] = new Dictionary<string, object>
                    {
                        [nameof(OcrRequests)] = CreateRequestRow(requestID, context.Parameters, this.session.User).GetStorage(),
                        [nameof(OcrRequestsLanguages)] = CreateRequestLanguagesRows(requestID, context.Parameters.Languages)
                            .Select(r => (object) r.GetStorage())
                            .ToList()
                    };

                    return new ValueTask();
                }, cancellationToken: cancellationToken);

                var storeResult = storeResponse.ValidationResult.Build();
                context.ValidationResult.Add(storeResult);
                if (!storeResult.IsSuccessful)
                {
                    await this.Logger.LogResultAsync(storeResult);
                    return null;
                }
            }

            return requestID;
        }


        #region Private

        private async Task<Card?> GetCardAsync(Guid cardID, IValidationResultBuilder validationResult, CancellationToken cancellationToken)
        {
            var request = new CardGetRequest { CardID = cardID };
            var response = await this.cardRepository.GetAsync(request, cancellationToken);
            validationResult.Add(response.ValidationResult);
            return response.TryGetCard();
        }

        private async Task<bool> StoreCardAsync(Card card, IValidationResultBuilder validationResult, CancellationToken cancellationToken)
        {
            var request = new CardStoreRequest { Card = card };
            var response = await this.cardRepository.StoreAsync(request, cancellationToken);
            var result = response.ValidationResult.Build();
            validationResult.Add(result);
            return result.IsSuccessful;
        }

        private async Task<Guid?> GetOcrCardIdAsync(Guid fileID, CancellationToken cancellationToken)
        {
            await using (this.dbScope.Create())
            {
                var parameter = DataParameter.Guid(nameof(fileID), fileID);

                var query = this.dbScope.BuilderFactory
                    .Select().C(OcrOperations.ID)
                    .From(nameof(OcrOperations)).NoLock()
                    .Where().C(OcrOperations.FileID).Equals().P(parameter.Name!)
                    .Build();

                return await this.dbScope.Db
                    .SetCommand(query, parameter)
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }
        }

        private async Task<Guid?> GetCardIdAsync(Guid fileID, CancellationToken cancellationToken)
        {
            await using (this.dbScope.Create())
            {
                var parameter = DataParameter.Guid(nameof(fileID), fileID);

                var query = this.dbScope.BuilderFactory
                    .Select().C("ID")
                    .From("Files").NoLock()
                    .Where().C("RowID").Equals().P(parameter.Name!)
                    .Build();

                return await this.dbScope.Db
                    .SetCommand(query, parameter)
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }
        }

        private static CardRow CreateRequestRow(Guid requestID, OcrParameters parameters, IUser user) => new CardRow
        {
            State = CardRowState.Inserted,
            RowID = requestID,
            [OcrRequests.Created] = DateTime.UtcNow,
            [OcrRequests.CreatedByID] = user.ID,
            [OcrRequests.CreatedByName] = user.Name,
            [OcrRequests.StateID] = Int32Boxes.Box((int) OcrRequestStates.Created),
            [OcrRequests.Confidence] = (decimal) parameters.Confidence,
            [OcrRequests.Preprocess] = parameters.Preprocess,
            [OcrRequests.SegmentationModeID] = Int32Boxes.Box((int) parameters.SegmentationMode),
            [OcrRequests.SegmentationModeName] = parameters.SegmentationMode.GetDescription(),
            [OcrRequests.DetectLanguages] = !parameters.Languages.Any(),
            [OcrRequests.Overwrite] = parameters.Overwrite,
            [OcrRequests.DetectRotation] = parameters.DetectRotation,
            [OcrRequests.DetectTables] = parameters.DetectTables
        };

        private static List<CardRow> CreateRequestLanguagesRows(Guid requestID, Enums.OcrLanguages[] languages) => languages
            .Select(language => new CardRow
            {
                State = CardRowState.Inserted,
                RowID = Guid.NewGuid(),
                ParentRowID = requestID,
                [OcrRequestsLanguages.LanguageID] = Int32Boxes.Box((int) language),
                [OcrRequestsLanguages.LanguageISO] = language.ToString(),
                [OcrRequestsLanguages.LanguageCaption] = language.GetDescription()
            }).ToList();

        #endregion
    }
}
