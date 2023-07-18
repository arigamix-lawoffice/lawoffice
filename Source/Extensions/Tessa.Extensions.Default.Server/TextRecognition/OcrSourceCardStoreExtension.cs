#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.TextRecognition.Constants;
using Tessa.TextRecognition.Enums;
using Unity;

namespace Tessa.Extensions.Default.Server.TextRecognition
{
    /// <summary>
    /// Расширение на сохранение исходной карточки, содержащей файл, распознавание которого необходимо выполнить.
    /// </summary>
    public sealed class OcrSourceCardStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр класса <see cref="OcrSourceCardStoreExtension"/>.
        /// </summary>
        /// <param name="cardRepository"><inheritdoc cref="ICardRepository" path="/summary"/></param>
        public OcrSourceCardStoreExtension([Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardRepository cardRepository) =>
            this.cardRepository = NotNullOrThrow(cardRepository);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (context.ValidationResult.IsSuccessful()
                && !context.Request.DoesNotAffectVersion
                && context.Request.TryGetInfo()?.ContainsKey(OcrCommon.OcrKey) is true)
            {
                // Если создается OCR запрос, то в любом случае увеличиваем версию карточки с исходным файлом.
                context.Request.AffectVersion = true;
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var deleteResult = await this.DeleteOperationCardsAsync(context.Request.Card.Files, context.CancellationToken);
            context.ValidationResult.Add(deleteResult);
            if (!deleteResult.IsSuccessful)
            {
                return;
            }

            Dictionary<string, object?>? request = null;
            List<object>? requestLanguages = null;

            if (!context.Request.AffectVersion
                || context.Request.DoesNotAffectVersion
                || context.Request.TryGetInfo()?.TryGetValue(OcrCommon.OcrKey, out var ocrInfo) is not true
                || ocrInfo is not Dictionary<string, object?> ocrStorage
                || (request = ocrStorage.TryGet<Dictionary<string, object?>>(nameof(OcrRequests))) is null
                || (requestLanguages = ocrStorage.TryGet<List<object>>(nameof(OcrRequestsLanguages))) is null)
            {
                return;
            }

            // Ищем исходный файл, распознавание которого требуется выполнить
            var file = context.Request.Card.Files.First(static file => file.Info.ContainsKey(OcrCommon.OcrKey));

            // Получаем опции OCR, которые были установлены в опциях файла
            var ocrOptions = file.DeserializeOptions().Get<Dictionary<string, object?>>(OcrCommon.OcrKey);
            var ocrCardId = ocrOptions?.Get<Guid>("CardID");
            if (!ocrCardId.HasValue)
            {
                context.ValidationResult.AddError(this, "OCR operation card identifier is empty at file options.");
                return;
            }

            // Пытаемся создать карточку OCR
            var newRequest = new CardNewRequest
            {
                CardTypeID = OcrCardTypes.OcrOperationTypeID,
                CardTypeName = OcrCardTypes.OcrOperationTypeName
            };
            var newResponse = await this.cardRepository.NewAsync(newRequest, context.CancellationToken);
            context.ValidationResult.Add(newResponse.ValidationResult);
            if (newResponse.ValidationResult.IsSuccessful())
            {
                var ocrCard = newResponse.Card;
                ocrCard.ID = ocrCardId.Value;

                // Заполняем основную информацию по карточке OCR
                CopyOperationInfo(context.Request.Card, file, ocrCard.Sections[nameof(OcrOperations)].Fields);

                // Заполняем информацию по запросу на распознавание
                var requestRow = CreateRequestRow(request, context.Session.User);
                var requestLanguagesRows = CreateRequestLanguagesRows(requestLanguages, requestRow.RowID);
                ocrCard.Sections[nameof(OcrRequests)].Rows.Add(requestRow);
                ocrCard.Sections[nameof(OcrRequestsLanguages)].Rows.AddRange(requestLanguagesRows);

                // Пытаемся сохранить карточку OCR. В расширении на сохранение будет создана операция распознавания.
                var storeRequest = new CardStoreRequest { Card = ocrCard };
                var storeResponse = await this.cardRepository.StoreAsync(storeRequest, context.CancellationToken);
                context.ValidationResult.Add(storeResponse.ValidationResult);
            }
        }

        #endregion

        #region Private Methods

        private async Task<ValidationResult> DeleteOperationCardsAsync(
            ListStorage<CardFile> files,
            CancellationToken cancellationToken = default)
        {
            foreach (var file in files.Where(static f => f.State != CardFileState.None && f.State != CardFileState.Deleted))
            {
                var deleteOperationID = file.Info.TryGet<Guid?>(OcrCommon.OcrDeleteOperationIdKey);
                if (deleteOperationID.HasValue)
                {
                    var deleteRequest = new CardDeleteRequest
                    {
                        CardID = deleteOperationID.Value,
                        CardTypeID = OcrCardTypes.OcrOperationTypeID,
                        CardTypeName = OcrCardTypes.OcrOperationTypeName
                    };
                    var deleteResponse = await this.cardRepository.DeleteAsync(deleteRequest, cancellationToken);
                    return deleteResponse.ValidationResult.Build();
                }
            }

            return ValidationResult.Empty;
        }

        private static void CopyOperationInfo(Card card, CardFile file, IDictionary<string, object?> target)
        {
            target[OcrOperations.CardID] = card.ID;
            target[OcrOperations.CardTypeID] = card.TypeID;
            target[OcrOperations.CardTypeName] = card.TypeName;
            target[OcrOperations.FileID] = file.Card.ID;
            target[OcrOperations.FileName] = file.Name;
            target[OcrOperations.FileTypeID] = file.TypeID;
            target[OcrOperations.FileTypeName] = file.TypeName;
            target[OcrOperations.VersionRowID] = file.VersionRowID;
        }

        private static CardRow CreateRequestRow(IDictionary<string, object?> request, IUser user) => new()
        {
            State = CardRowState.Inserted,
            RowID = request.TryGet(nameof(OcrRequests.RowID), Guid.NewGuid()),
            [OcrRequests.Created] = DateTime.UtcNow,
            [OcrRequests.CreatedByID] = user.ID,
            [OcrRequests.CreatedByName] = user.Name,
            [OcrRequests.StateID] = Int32Boxes.Box((int) OcrRequestStates.Created),
            [OcrRequests.Confidence] = request[OcrRequests.Confidence],
            [OcrRequests.Preprocess] = request[OcrRequests.Preprocess],
            [OcrRequests.SegmentationModeID] = request[OcrRequests.SegmentationModeID],
            [OcrRequests.SegmentationModeName] = request[OcrRequests.SegmentationModeName],
            [OcrRequests.DetectLanguages] = request[OcrRequests.DetectLanguages],
            [OcrRequests.Overwrite] = request[OcrRequests.Overwrite],
            [OcrRequests.DetectRotation] = request[OcrRequests.DetectRotation],
            [OcrRequests.DetectTables] = request[OcrRequests.DetectTables]
        };

        private static CardRow[] CreateRequestLanguagesRows(List<object> requestLanguages, Guid requestID) => requestLanguages
            .Cast<IDictionary<string, object?>>()
            .DistinctBy(static language => language.Get<int>(OcrRequestsLanguages.LanguageID))
            .Select(language => new CardRow
            {
                State = CardRowState.Inserted,
                RowID = Guid.NewGuid(),
                ParentRowID = requestID,
                [OcrRequestsLanguages.LanguageID] = language[OcrRequestsLanguages.LanguageID],
                [OcrRequestsLanguages.LanguageISO] = language[OcrRequestsLanguages.LanguageISO],
                [OcrRequestsLanguages.LanguageCaption] = language[OcrRequestsLanguages.LanguageCaption]
            })
            .ToArray();

        #endregion
    }
}
