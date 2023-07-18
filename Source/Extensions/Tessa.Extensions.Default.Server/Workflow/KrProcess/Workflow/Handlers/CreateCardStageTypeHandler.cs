using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Numbers;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants.KrCreateCardStageSettingsVirtual;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Предоставляет обработчик этапа "Создание карточки" (<see cref="StageTypeDescriptors.CreateCardDescriptor"/>).
    /// </summary>
    public class CreateCardStageTypeHandler : StageTypeHandlerBase
    {
        #region Nested Types

        /// <summary>
        /// Перечисление режимов работы этапа "Создание карточки".
        /// </summary>
        public enum CreateCardMode
        {
            /// <summary>
            /// Открыть новую карточку.
            /// </summary>
            Open = 0,

            /// <summary>
            /// Сохранить и открыть карточку.
            /// </summary>
            StoreAndOpen = 1,

            /// <summary>
            /// Запустить основной процесс по карточке.
            /// </summary>
            StartProcess = 2,

            /// <summary>
            /// Запустить основной процесс по карточке и открыть её.
            /// </summary>
            StartProcessAndOpen = 3,

            /// <summary>
            /// Сохранить карточку.
            /// </summary>
            Store = 4
        }

        /// <summary>
        /// Предоставляет контекст этапа "Создание карточки".
        /// </summary>
        protected struct CreateCardStageLocalContext
        {
            /// <summary>
            /// Идентификатор шаблона по которому создаётся карточка.
            /// </summary>
            public Guid? TemplateID;

            /// <summary>
            /// Идентификатор типа создаваемой карточки.
            /// </summary>
            public Guid? TypeID;

            /// <summary>
            /// Отображаемое имя типа создаваемой карточки.
            /// </summary>
            public string TypeCaption;

            /// <summary>
            /// Режим работы этапа "Создание карточки".
            /// </summary>
            public CreateCardMode Mode;

            /// <summary>
            /// Значение <see langword="true"/>, если контекст этапа содержит корректные данные, иначе - <see langword="false"/>.
            /// </summary>
            public bool Valid;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CreateCardStageTypeHandler"/>.
        /// </summary>
        /// <param name="cardRepositoryDef">Репозиторий для управления карточками (<see cref="CardRepositoryNames.Default"/>).</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="tokenProvider">Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="signatureProvider">Объект, предоставляющий криптографические средства для подписания и проверки подписи.</param>
        public CreateCardStageTypeHandler(
            [Dependency(CardRepositoryNames.Default)] ICardRepository cardRepositoryDef,
            ICardRepository cardRepository,
            IKrTokenProvider tokenProvider,
            ICardFileManager fileManager,
            IDbScope dbScope,
            IKrScope krScope,
            IKrTypesCache typesCache,
            ISignatureProvider signatureProvider)
        {
            ThrowIfNull(cardRepositoryDef);
            ThrowIfNull(cardRepository);
            ThrowIfNull(tokenProvider);
            ThrowIfNull(fileManager);
            ThrowIfNull(dbScope);
            ThrowIfNull(krScope);
            ThrowIfNull(typesCache);
            ThrowIfNull(signatureProvider);

            this.CardRepository = cardRepository;
            this.CardRepositoryDef = cardRepositoryDef;
            this.TokenProvider = tokenProvider;
            this.FileManager = fileManager;
            this.DbScope = dbScope;
            this.KrScope = krScope;
            this.TypesCache = typesCache;
            this.SignatureProvider = signatureProvider;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задаёт репозиторий для управления карточками.
        /// </summary>
        protected ICardRepository CardRepository { get; set; }

        /// <summary>
        /// Возвращает или задаёт репозиторий для управления карточками (<see cref="CardRepositoryNames.Default"/>).
        /// </summary>
        protected ICardRepository CardRepositoryDef { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект, обеспечивающий создание и валидацию токена безопасности для типового решения.
        /// </summary>
        protected IKrTokenProvider TokenProvider { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.
        /// </summary>
        protected ICardFileManager FileManager { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект для взаимодействия с базой данных.
        /// </summary>
        protected IDbScope DbScope { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; set; }

        /// <summary>
        /// Возвращает или задаёт кэш по типам карточек и документов, содержащих информацию по типовому решению.
        /// </summary>
        protected IKrTypesCache TypesCache { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект, предоставляющий криптографические средства для подписания и проверки подписи.
        /// </summary>
        protected ISignatureProvider SignatureProvider { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var localCtx = this.GetLocalContext(context);
            if (!localCtx.Valid)
            {
                return StageHandlerResult.CompleteResult;
            }

            switch (localCtx.Mode)
            {
                case CreateCardMode.Open:
                {
                    Guid typeID;
                    if (localCtx.TemplateID.HasValue)
                    {
                        var nullableTypeID = await KrProcessHelper.GetTemplateCardTypeAsync(
                            localCtx.TemplateID.Value,
                            this.DbScope,
                            context.CancellationToken);

                        if (!nullableTypeID.HasValue)
                        {
                            context.ValidationResult.AddError(
                                this,
                                LocalizationManager.Format(
                                    "$KrProcess_ErrorMessage_ErrorFormat2",
                                    KrErrorHelper.GetTraceTextFromStage(context.Stage),
                                    "$KrStages_CreateCard_TemplateNotFound"));
                            return StageHandlerResult.EmptyResult;
                        }

                        typeID = nullableTypeID.Value;
                    }
                    else if (localCtx.TypeID.HasValue)
                    {
                        typeID = await this.GetCardTypeAsync(localCtx.TypeID.Value, context.CancellationToken);
                    }
                    else
                    {
                        context.ValidationResult.AddError(
                            this,
                            LocalizationManager.Format(
                                "$KrProcess_ErrorMessage_ErrorFormat2",
                                KrErrorHelper.GetTraceTextFromStage(context.Stage),
                                "$KrStages_CreateCard_TemplateAndTypeNotSpecified"));
                        return StageHandlerResult.EmptyResult;
                    }

                    var newRequest = new CardNewRequest() { CardTypeID = typeID };

                    await this.CreateEmptyCardAsync(
                        context,
                        this.CardRepositoryDef,
                        newRequest,
                        Guid.Empty);
                    break;
                }
                case CreateCardMode.Store:
                case CreateCardMode.StoreAndOpen:
                case CreateCardMode.StartProcess:
                case CreateCardMode.StartProcessAndOpen:
                {
                    var newRequest = new CardNewRequest();
                    if (localCtx.TemplateID.HasValue)
                    {
                        var docTypeID = await KrProcessHelper.GetTemplateDocTypeAsync(
                            localCtx.TemplateID.Value,
                            this.DbScope,
                            context.CancellationToken);

                        if (!docTypeID.HasValue)
                        {
                            context.ValidationResult.AddError(
                                this,
                                LocalizationManager.Format(
                                    "$KrProcess_ErrorMessage_ErrorFormat2",
                                    KrErrorHelper.GetTraceTextFromStage(context.Stage),
                                    "$KrStages_CreateCard_TemplateNotFound"));
                            return StageHandlerResult.EmptyResult;
                        }

                        newRequest.CardTypeID = CardHelper.TemplateTypeID;
                        newRequest.SetTemplateCardID(localCtx.TemplateID);
                    }
                    else
                    {
                        var docType = localCtx.TypeID.HasValue
                            ? (await this.TypesCache.GetDocTypesAsync(context.CancellationToken))
                                .FirstOrDefault(x => x.ID == localCtx.TypeID)
                            : null;

                        if (docType is not null)
                        {
                            newRequest.CardTypeID = docType.CardTypeID;
                            newRequest.Info[KrConstants.Keys.DocTypeID] = localCtx.TypeID;
                            newRequest.Info[KrConstants.Keys.DocTypeTitle] = localCtx.TypeCaption;
                        }
                        else
                        {
                            newRequest.CardTypeID = localCtx.TypeID;
                        }
                    }

                    this.TokenProvider.CreateFullToken(Guid.Empty).Set(newRequest.Info);

                    await this.CreateEmptyCardAsync(
                        context,
                        this.CardRepository,
                        newRequest,
                        Guid.NewGuid());

                    break;
                }
                default:
                    return StageHandlerResult.CompleteResult;
            }

            return StageHandlerResult.CompleteResult;
        }

        /// <inheritdoc />
        public override async Task AfterPostprocessingAsync(
            IStageTypeHandlerContext context)
        {
            await base.AfterPostprocessingAsync(context);

            var localCtx = this.GetLocalContext(context);
            if (!localCtx.Valid)
            {
                return;
            }

            await using var newCardAccessStrategy = context.Stage.InfoStorage.TryGet<IMainCardAccessStrategy>(KrConstants.Keys.NewCard);
            var newCard = await newCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

            context.Stage.InfoStorage.Remove(KrConstants.Keys.NewCard);

            if (newCard is null)
            {
                return;
            }

            if (localCtx.Mode == CreateCardMode.Open)
            {
                newCard.RemoveAllButChanged();
                var serialized = newCard.ToSerializable().Serialize();
                var sign = this.SignatureProvider.Sign(serialized);

                var info = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    [KrConstants.Keys.NewCard] = serialized,
                    [KrConstants.Keys.NewCardSignature] = sign,
                };

                if (localCtx.TemplateID.HasValue)
                {
                    info[KrConstants.Keys.TemplateID] = localCtx.TemplateID;
                    this.KrScope.TryAddClientCommand(
                        new KrProcessClientCommand(DefaultCommandTypes.CreateCardViaTemplate, info));
                }
                else
                {
                    var docType = localCtx.TypeID.HasValue
                        ? (await this.TypesCache.GetDocTypesAsync(context.CancellationToken))
                            .FirstOrDefault(x => x.ID == localCtx.TypeID.Value)
                        : null;

                    if (docType is not null)
                    {
                        info.Add(KrConstants.Keys.TypeID, docType.CardTypeID);
                        info.Add(KrConstants.Keys.DocTypeID, localCtx.TypeID);
                        info.Add(KrConstants.Keys.DocTypeTitle, localCtx.TypeCaption);
                    }
                    else
                    {
                        info.Add(KrConstants.Keys.TypeID, localCtx.TypeID);
                        info.Add(KrConstants.Keys.TypeCaption, localCtx.TypeCaption);
                    }
                    this.KrScope.TryAddClientCommand(new KrProcessClientCommand(
                        DefaultCommandTypes.CreateCardViaDocType,
                        info));
                }

                return;
            }

            var card = await this.StoreCardAsync(
                context,
                this.CardRepository,
                newCard,
                (request, ct) =>
                {
                    var card = request.Card;
                    this.TokenProvider.CreateFullToken(card).Set(card.Info);
                    return new ValueTask();
                });

            if (card is null)
            {
                return;
            }

            if (localCtx.Mode is CreateCardMode.StoreAndOpen
                or CreateCardMode.StartProcessAndOpen)
            {
                this.KrScope.TryAddClientCommand(
                    new KrProcessClientCommand(
                        DefaultCommandTypes.OpenCard,
                        new Dictionary<string, object>(StringComparer.Ordinal)
                        {
                            [KrConstants.Keys.NewCardID] = card.ID,
                            [KrConstants.Keys.TypeID] = card.TypeID,
                            [KrConstants.Keys.TypeName] = card.TypeName,
                        }));
            }

            if (localCtx.Mode is CreateCardMode.StartProcess
                or CreateCardMode.StartProcessAndOpen)
            {
                await this.StartProcessAsync(context, card);
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Возвращает объект <see cref="CreateCardStageLocalContext"/> содержащий контекст этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Объект <see cref="CreateCardStageLocalContext"/> содержащий контекст этапа.</returns>
        protected CreateCardStageLocalContext GetLocalContext(
            IStageTypeHandlerContext context)
        {
            var localCtx = new CreateCardStageLocalContext
            {
                TemplateID = context.Stage.SettingsStorage.TryGet<Guid?>(TemplateID),
                TypeID = context.Stage.SettingsStorage.TryGet<Guid?>(TypeID),
                TypeCaption = context.Stage.SettingsStorage.TryGet<string>(TypeCaption),
            };

            if (!localCtx.TemplateID.HasValue && !localCtx.TypeID.HasValue)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(context.Stage),
                        "$KrStages_CreateCard_TemplateAndTypeNotSpecified"));
                return localCtx;
            }

            if (localCtx.TemplateID.HasValue && localCtx.TypeID.HasValue)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(context.Stage),
                        "$KrStages_CreateCard_TemplateAndTypeSelected"));
                return localCtx;
            }

            var modeID = context.Stage.SettingsStorage.TryGet<int?>(ModeID);

            if (!modeID.HasValue)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(context.Stage),
                        "$KrStages_CreateCard_ModeRequired"));
                return localCtx;
            }

            localCtx.Mode = (CreateCardMode) modeID;
            localCtx.Valid = true;
            return localCtx;
        }

        /// <summary>
        /// Инициализирует стратегию доступа к новой карточке доступной по ключу <see cref="KrConstants.Keys.NewCard"/> в <see cref="Stage.InfoStorage"/>.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками используемый при создании карточки-заготовки.</param>
        /// <param name="cardNewRequest">Запрос на создание карточки определённого типа посредством сервиса карточек.</param>
        /// <param name="newCardID">Идентификатор новой карточки.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task CreateEmptyCardAsync(
            IStageTypeHandlerContext context,
            ICardRepository cardRepository,
            CardNewRequest cardNewRequest,
            Guid newCardID)
        {
            context.Stage.InfoStorage[KrConstants.Keys.NewCardID] = newCardID;

            var newCardAccessStrategy = new ObviousMainCardAccessStrategy(
                async (validationResult, ct) =>
                {
                    var newResponse = await cardRepository.NewAsync(cardNewRequest, ct);

                    if (validationResult is not null)
                    {
                        var validationResultResponse = newResponse.TryGetValidationResult();
                        if (validationResultResponse is not null)
                        {
                            validationResult.Add(validationResultResponse);
                        }
                    }

                    var newCard = newResponse.TryGetCard();

                    if (newCard is not null)
                    {
                        newCard.ID = newCardID;
                    }

                    return newCard;
                },
                this.FileManager,
                context.ValidationResult);

            this.KrScope.AddDisposableObject(newCardAccessStrategy);
            context.Stage.InfoStorage[KrConstants.Keys.NewCard] = newCardAccessStrategy;
        }

        /// <summary>
        /// Асинхронно сохраняет указанную карточку с учётом приложенных файлов.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками используемый при сохранении карточки.</param>
        /// <param name="card">Сохраняемая карточка.</param>
        /// <param name="modifyRequestActionAsync">Функция используемая для изменения запроса на сохранение карточки.</param>
        /// <returns>Сохранённая карточка.</returns>
        protected async Task<Card> StoreCardAsync(
            IStageTypeHandlerContext context,
            ICardRepository cardRepository,
            Card card,
            Func<CardStoreRequest, CancellationToken, ValueTask> modifyRequestActionAsync = default)
        {
            if (card.ID == Guid.Empty)
            {
                card.ID = Guid.NewGuid();
            }

            int version;
            await using (var container = await this.FileManager.CreateContainerAsync(card, cancellationToken: context.CancellationToken))
            {
                var storeResponse = await container.StoreAsync(async (c, request, ct) =>
                {
                    var digest = await cardRepository.GetDigestAsync(request.Card, CardDigestEventNames.ActionHistoryStoreRouteCreateCard, ct);
                    if (!string.IsNullOrEmpty(digest))
                    {
                        request.SetDigest(digest);
                    }

                    if (modifyRequestActionAsync is not null)
                    {
                        await modifyRequestActionAsync(request, ct);
                    }
                },
                cancellationToken: context.CancellationToken);
                version = storeResponse.CardVersion;
                context.ValidationResult.Add(storeResponse.ValidationResult);
                if (!storeResponse.ValidationResult.IsSuccessful())
                {
                    return default;
                }
            }

            card.Version = version;
            return card;
        }

        /// <summary>
        /// Асинхронно запускает основной процесс по указанной карточке.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="card">Карточка по которой должен быть запущен процесс.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task StartProcessAsync(
            IStageTypeHandlerContext context,
            Card card)
        {
            card.RemoveChanges(CardRemoveChangesDeletedHandling.Remove);
            card.RemoveNumberQueue();
            card.RemoveWorkflowQueue();
            card.RemoveAllButChanged(card.StoreMode);

            this.TokenProvider.CreateFullToken(card).Set(card.Info);

            var startProcessRequest = new CardStoreRequest { Card = card };

            var storeInfo = startProcessRequest.Info;
            storeInfo.SetStartingProcessName(KrConstants.KrProcessName);

            if (context.NotMessageHasNoActiveStages)
            {
                var processParameters = new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    { KrConstants.Keys.NotMessageHasNoActiveStages, BooleanBoxes.True }
                };

                storeInfo.SetStartingKrProcessParameters(processParameters);
            }

            startProcessRequest.SetIgnorePermissionsWarning();
            var startProcessResponse = await this.CardRepository.StoreAsync(startProcessRequest, context.CancellationToken);
            context.ValidationResult.Add(startProcessResponse.ValidationResult);
        }

        /// <summary>
        /// Асинхронно возвращает идентификатор типа карточки соответствующий указанному типу документа, если указанному значению не соответствует типу документа, то оно возвращается без изменений.
        /// </summary>
        /// <param name="docOrCardTypeID">Идентификатор типа документа или тип карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор типа карточки соответствующий указанному типу документа, если указанному значению не соответствует типу документа, то оно возвращается без изменений.</returns>
        protected async ValueTask<Guid> GetCardTypeAsync(Guid docOrCardTypeID, CancellationToken cancellationToken = default)
        {
            var docType = (await this.TypesCache.GetDocTypesAsync(cancellationToken))
                .FirstOrDefault(x => x.ID == docOrCardTypeID);

            return docType?.CardTypeID ?? docOrCardTypeID;
        }

        #endregion

    }
}
