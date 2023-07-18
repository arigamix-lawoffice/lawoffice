using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Files;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение добавляет выбранный пользователем шаблон файла в карточку
    /// </summary>
    public sealed class SaveFileTemplateOnCardStoreExtension : CardStoreExtension
    {
        #region internal

        private class FileTemplateData
        {
            public Guid ID;
            public string FileName;
            public Guid ReplacedFileID;
        }

        #endregion

        #region fields

        private const string SaveWithFileTemplateKey = CardHelper.SystemKeyPrefix + "SaveWithFileTemplate";
        private const string SaveWithFileTemplateResponseKey = CardHelper.SystemKeyPrefix + "SaveWithFileTemplateResponse";

        private readonly ICardFileManager fileManager;
        private readonly ICardStreamServerRepository cardStreamRepository;
        private readonly ICardRepository cardRepository;

        #endregion

        #region ctor

        public SaveFileTemplateOnCardStoreExtension(
            ICardFileManager fileManager,
            ICardStreamServerRepository cardStreamRepository,
            ICardRepository cardRepository)
        {
            this.fileManager = fileManager;
            this.cardStreamRepository = cardStreamRepository;
            this.cardRepository = cardRepository;
        }

        #endregion

        #region private methods

        private static FileTemplateData GetFileTemplateDataFromInfo(Dictionary<string, object> info)
        {
            if (info == null)
            {
                return null;
            }

            var fileTemplateData = info.TryGet<Dictionary<string, object>>(SaveWithFileTemplateKey);
            if (fileTemplateData == null)
            {
                return null;
            }

            Guid id = fileTemplateData.TryGet("id", Guid.Empty);
            string fileName = fileTemplateData.TryGet("fileName", string.Empty);
            Guid fileId = fileTemplateData.TryGet("fileId", Guid.Empty);

            if (id == Guid.Empty || string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            return new FileTemplateData
            {
                ID = id,
                FileName = fileName,
                ReplacedFileID = fileId
            };
        }

        #endregion

        #region CardStoreExtension

        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (!context.Session.IsWebClient() ||
                GetFileTemplateDataFromInfo(context.Request.Info) == null)
            {
                return Task.CompletedTask;
            }

            context.Request.ForceTransaction = true;
            return Task.CompletedTask;
        }


        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            Card card;
            FileTemplateData fileTemplateData;
            if (!context.Session.IsWebClient()
                || !context.ValidationResult.IsSuccessful()
                || (card = context.Request.TryGetCard()) == null
                || (fileTemplateData = GetFileTemplateDataFromInfo(context.Request.Info)) == null)
            {
                return;
            }

            Guid cardID = card.ID;
            card = card.Clone();
            card.Sections.Clear();
            card.Files.Clear();
            card.Tasks.Clear();
            card.TaskHistory.Clear();

            await using var container = await this.fileManager.CreateContainerAsync(card, cancellationToken: context.CancellationToken);
            var contentRequest =
                new CardGetFileContentRequest
                {
                    ServiceType = CardServiceType.Client,
                    CardID = fileTemplateData.ID,
                    VersionRowID = CardHelper.ReplacePlaceholdersVersionRowID
                };

            Dictionary<string, object> requestInfo = contentRequest.Info;
            requestInfo[CardHelper.PlaceholderCurrentCardIDInfo] = cardID;

            ICardFileContentResult contentResult = await this.cardStreamRepository.GetFileContentAsync(
                contentRequest, context.CancellationToken);

            CardGetFileContentResponse contentResponse = contentResult.Response;

            ValidationResult result = contentResponse.ValidationResult.Build();
            context.ValidationResult.Add(result);

            if (!result.IsSuccessful || !contentResult.HasContent)
            {
                return;
            }

            string suggestedFileName = contentResponse.TryGetSuggestedFileName();
            string fileName = string.IsNullOrWhiteSpace(suggestedFileName)
                ? fileTemplateData.FileName
                : suggestedFileName;

            CardStoreResponse storeResponse = null;
            if (fileTemplateData.ReplacedFileID == Guid.Empty)
            {
                // Вставляется новый файл
                await container.FileContainer
                    .BuildFile(fileName)
                    .SetContent(contentResult.GetContentOrThrowAsync, contentResponse.Size)
                    .AddWithNotificationAsync(cancellationToken: context.CancellationToken);

                storeResponse = await container.StoreAsync(
                    (c, request, ct) =>
                    {
                        request.ServiceType = CardServiceType.Client;
                        return new ValueTask();
                    },
                    cancellationToken: context.CancellationToken);
            }
            else
            {
                // Заменяется версия файла на файл по шаблону
                var resp = await this.cardRepository.GetAsync(
                    new CardGetRequest
                    {
                        CardID = cardID
                    },
                    context.CancellationToken);

                if (resp.IsValid())
                {
                    Card cardWithFiles = resp.TryGetCard();

                    KrToken.TryGet(context.Request.Card.Info)?.Set(cardWithFiles.Info);

                    if (cardWithFiles != null)
                    {
                        await using var cardContainer = await this.fileManager.CreateContainerAsync(cardWithFiles, cancellationToken: context.CancellationToken);
                        var file = cardContainer.FileContainer.Files.TryGet(fileTemplateData.ReplacedFileID);
                        if (file != null)
                        {
                            await file.RenameAsync(fileName, context.CancellationToken);

                            using (var tempFile = TempFile.Acquire(fileName))
                            {
                                await using (var content = await contentResult.GetContentOrThrowAsync(context.CancellationToken))
                                {
                                    await using var fileStream = FileHelper.Create(tempFile.Path);
                                    await content.CopyToAsync(fileStream, context.CancellationToken);
                                }

                                await file.ReplaceAsync(tempFile.Path, cancellationToken: context.CancellationToken);
                            }

                            storeResponse = await cardContainer.StoreAsync(
                                (c, request, ct) =>
                                {
                                    request.ServiceType = CardServiceType.Client;
                                    return new ValueTask();
                                },
                                cancellationToken: context.CancellationToken);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            context.Info[SaveWithFileTemplateResponseKey] = storeResponse;
        }


        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            CardStoreResponse storeResponse;

            if (!context.Session.IsWebClient()
                || (storeResponse = context.Info.TryGet<CardStoreResponse>(SaveWithFileTemplateResponseKey)) == null
                || !context.ValidationResult.IsSuccessful())
            {
                return Task.CompletedTask;
            }

            storeResponse.ValidationResult.Add(context.ValidationResult);
            context.Response = storeResponse;

            return Task.CompletedTask;
        }

        #endregion
    }
}