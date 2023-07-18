#nullable enable

using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.TextRecognition;
using Tessa.TextRecognition.Constants;
using OcrLanguages = Tessa.TextRecognition.Enums.OcrLanguages;
using OcrRequestStates = Tessa.TextRecognition.Enums.OcrRequestStates;

namespace Tessa.Extensions.Default.Server.TextRecognition
{
    /// <summary>
    /// Расширение на сохранение карточки операции OCR, в котором выполняется создание операции для распознавания текста в файле.
    /// </summary>
    public sealed class OcrOperationStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IOcrAsyncService ocrService;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр класса <see cref="OcrOperationStoreExtension"/>.
        /// </summary>
        /// <param name="ocrService"><inheritdoc cref="IOcrAsyncService" path="/summary"/></param>
        public OcrOperationStoreExtension(IOcrAsyncService ocrService) => this.ocrService = NotNullOrThrow(ocrService);

        #endregion

        #region Base overrides

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful() ||
                !context.Request.Card.Sections.TryGetValue(nameof(OcrRequests), out var requests))
            {
                return;
            }

            List<int>? languages = null;
            CardGetFileContentRequest? fileContentRequest = null;
            var cancellationToken = context.CancellationToken;
            var validationResult = context.ValidationResult;
            var card = context.Request.Card;

            var createdRequests = requests.Rows
                .Where(static row =>
                    row.State == CardRowState.Inserted &&
                    row.TryGet<int?>(OcrRequests.StateID) == (int) OcrRequestStates.Created)
                .ToArray();

            if (createdRequests.Any())
            {
                var parameter = DataParameter.Guid(nameof(card.ID), card.ID);

                if (await IsSameRequestAlreadyExistsAsync(context.DbScope!, parameter, cancellationToken))
                {
                    validationResult.AddError("Text recognition request already exists with the same parameters.");
                    return;
                }

                fileContentRequest = await TryGetFileContentRequestAsync(context.DbScope!, parameter, cancellationToken);
            }

            var interruptedRequests = requests.Rows
                .Where(static row =>
                    row.State == CardRowState.Modified &&
                    row.TryGet<int?>(OcrRequests.StateID) == (int) OcrRequestStates.Interrupted)
                .ToArray();

            foreach (var request in interruptedRequests)
            {
                await this.ocrService.CancelOperationAsync(request.RowID, validationResult, cancellationToken);
                if (!validationResult.IsSuccessful())
                {
                    return;
                }
            }


            foreach (var request in createdRequests)
            {
                var ocrRequest = new OcrAsyncRequest(fileContentRequest)
                {
                    OcrCardID = card.ID,
                    Confidence = (float) request.Get<decimal>(OcrRequests.Confidence),
                    Preprocess = request.Get<bool>(OcrRequests.Preprocess),
                    Overwrite = request.Get<bool>(OcrRequests.Overwrite),
                    DetectRotation = request.Get<bool>(OcrRequests.DetectRotation),
                    DetectTables = request.Get<bool>(OcrRequests.DetectTables),
                    SegmentationModeID = request.Get<int>(OcrRequests.SegmentationModeID),
                    Languages = request.Get<bool>(OcrRequests.DetectLanguages)
                        ? languages ??= Enum.GetValues<OcrLanguages>().Cast<int>().ToList()
                        : card.Sections.TryGet(nameof(OcrRequestsLanguages))?.Rows
                            .Where(row => row.State == CardRowState.Inserted && row.ParentRowID == request.RowID)
                            .Select(static row => row.Get<int>(OcrRequestsLanguages.LanguageID))
                            .ToList(),
                    Info = { ["OperationID"] = request.RowID }
                };

                var operationID = await this.ocrService.CreateOperationAsync(ocrRequest, validationResult, cancellationToken);
                if (!validationResult.IsSuccessful())
                {
                    return;
                }
                else if (!operationID.HasValue)
                {
                    validationResult.AddError($"An error occurred while creating text recognition operation with ID={request.RowID:B}.");
                    return;
                }
            }
        }

        #endregion

        #region Private

        private static async Task<CardGetFileContentRequest?> TryGetFileContentRequestAsync(IDbScope dbScope, DataParameter parameter, CancellationToken cancellationToken)
        {
            await using (dbScope.Create())
            {
                var query = dbScope.BuilderFactory
                    .Select()
                        .C(OcrOperations.CardID).As(nameof(CardGetFileContentRequest.CardID))
                        .C(OcrOperations.CardTypeID).As(nameof(CardGetFileContentRequest.CardTypeID))
                        .C(OcrOperations.CardTypeName).As(nameof(CardGetFileContentRequest.CardTypeName))
                        .C(OcrOperations.FileID).As(nameof(CardGetFileContentRequest.FileID))
                        .C(OcrOperations.FileName).As(nameof(CardGetFileContentRequest.FileName))
                        .C(OcrOperations.FileTypeID).As(nameof(CardGetFileContentRequest.FileTypeID))
                        .C(OcrOperations.FileTypeName).As(nameof(CardGetFileContentRequest.FileTypeName))
                        .C(OcrOperations.VersionRowID).As(nameof(CardGetFileContentRequest.VersionRowID))
                    .From(nameof(OcrOperations)).NoLock()
                    .Where().C(OcrOperations.ID).Equals().P(parameter.Name!)
                    .Build();

                return await dbScope.Db
                    .SetCommand(query, parameter)
                    .LogCommand()
                    .ExecuteAsync<CardGetFileContentRequest>(cancellationToken);
            }
        }

        private static async Task<bool> IsSameRequestAlreadyExistsAsync(IDbScope dbScope, DataParameter parameter, CancellationToken cancellationToken)
        {
            await using (dbScope.Create())
            {
                var query = dbScope.BuilderFactory
                    .SelectExists(e => e
                        .Select().V(null)
                        .From(nameof(OcrRequests), "r").NoLock()
                        .LeftJoinLateral(j => j
                            .Select().Sum(s => s
                                .Power(v => v.V(10),
                                    d => d.C("l", OcrRequestsLanguages.LanguageID))
                             ).As("LanguagesHash")
                            .From(nameof(OcrRequestsLanguages), "l").NoLock()
                            .Where().C("l", OcrRequestsLanguages.ParentRowID).Equals().C("r", OcrRequests.RowID)
                        , "l")
                        .Where().C("r", OcrRequests.ID).Equals().P(parameter.Name!)
                            .And().C("r", OcrRequests.StateID).NotEquals().V((int) OcrRequestStates.Interrupted)
                        .GroupBy()
                            .C("r", OcrRequests.SegmentationModeID)
                            .C("r", OcrRequests.Confidence)
                            .C("r", OcrRequests.Preprocess)
                            .C("r", OcrRequests.DetectLanguages)
                            .C("r", OcrRequests.DetectRotation)
                            .C("r", OcrRequests.DetectTables)
                            .C("r", OcrRequests.Overwrite)
                            .C("l", "LanguagesHash")
                        .Having(h => h.Count().Greater().V(1))
                    ).Build();

                return await dbScope.Db
                    .SetCommand(query, parameter)
                    .LogCommand()
                    .ExecuteAsync<bool>(cancellationToken);
            }
        }

        #endregion
    }
}
