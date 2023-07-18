using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    /// <inheritdoc cref="IKrScope"/>
    public sealed class KrScope :
        IKrScope
    {
        #region Fields

        private const int MaxDepth = 20;

        private readonly ICardRepository cardRepository;

        private readonly ICardRepository cardRepositoryEwt;

        private readonly ICardTransactionStrategy cardTransactionStrategy;

        private readonly ICardGetStrategy cardGetStrategy;

        private readonly IDbScope dbScope;

        private readonly ICardMetadata cardMetadata;

        private readonly IKrTokenProvider tokenProvider;

        private readonly IKrStageSerializer serializer;

        private readonly IKrTypesCache typesCache;

        private readonly ICardFileManager cardFileManager;

        private readonly ICardStreamServerRepository cardStreamServerRepository;

        private readonly ICardStreamServerRepository cardStreamServerRepositoryEwt;

        #endregion

        #region Constructors

        public KrScope(
            ICardRepository cardRepository,
            [Unity.Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardRepository cardRepositoryEwt,
            ICardTransactionStrategy cardTransactionStrategy,
            ICardGetStrategy cardGetStrategy,
            IDbScope dbScope,
            ICardMetadata cardMetadata,
            IKrTokenProvider tokenProvider,
            IKrStageSerializer serializer,
            IKrTypesCache typesCache,
            ICardFileManager cardFileManager,
            ICardStreamServerRepository cardStreamServerRepository,
            [Unity.Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardStreamServerRepository cardStreamServerRepositoryEwt)
        {
            this.cardRepository = NotNullOrThrow(cardRepository);
            this.cardRepositoryEwt = NotNullOrThrow(cardRepositoryEwt);
            this.cardTransactionStrategy = NotNullOrThrow(cardTransactionStrategy);
            this.cardGetStrategy = NotNullOrThrow(cardGetStrategy);
            this.dbScope = NotNullOrThrow(dbScope);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.tokenProvider = NotNullOrThrow(tokenProvider);
            this.serializer = NotNullOrThrow(serializer);
            this.typesCache = NotNullOrThrow(typesCache);
            this.cardFileManager = NotNullOrThrow(cardFileManager);
            this.cardStreamServerRepository = NotNullOrThrow(cardStreamServerRepository);
            this.cardStreamServerRepositoryEwt = NotNullOrThrow(cardStreamServerRepositoryEwt);
        }

        #endregion

        #region IKrScope Members

        /// <inheritdoc />
        public bool Exists => KrScopeContext.HasCurrent;

        /// <inheritdoc />
        public Dictionary<string, object> Info
        {
            get
            {
                var scopeContext = KrScopeContext.Current;
                AssertNonEmptyKrScopeContext(scopeContext);
                return scopeContext.Info;
            }
        }

        /// <inheritdoc />
        public int Depth => KrScopeContext.Current?.LevelStack.Count ?? 0;

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult
        {
            get
            {
                var scopeContext = KrScopeContext.Current;
                AssertNonEmptyKrScopeContext(scopeContext);
                return scopeContext.ValidationResult;
            }
        }

        /// <inheritdoc />
        public KrScopeLevel CurrentLevel => KrScopeContext.Current?.LevelStack.Peek();

        /// <inheritdoc />
        public KrScopeLevel EnterNewLevel()
        {
            var level = new KrScopeLevel(
                this.cardRepository,
                this.cardRepositoryEwt,
                this.tokenProvider,
                this.typesCache,
                this.serializer,
                this.cardGetStrategy,
                this.cardTransactionStrategy,
                this.dbScope,
                this.cardMetadata,
                this.cardStreamServerRepository,
                this.cardStreamServerRepositoryEwt);

            var scopeContext = KrScopeContext.Current;
            if (MaxDepth < scopeContext.LevelStack.Count)
            {
                scopeContext.LevelStack.Pop();
                var text = LocalizationManager.Format("$KrProcess_MaximumKrScopeDepth", "$CardTypes_Controls_RunOnce");
                throw new InvalidOperationException(text);
            }
            return level;
        }

        /// <inheritdoc />
        public async ValueTask<Card> GetMainCardAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            bool withoutTransaction = false,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            Card card;

            if (scopeContext is null)
            {
                // в случае KrScope загружается основная карточка
                var isCreatedValidationResult = false;

                if (validationResult is null)
                {
                    validationResult = new ValidationResultBuilder();
                    isCreatedValidationResult = true;
                }

                card = await this.GetMainCardInternalAsync(
                    mainCardID,
                    validationResult,
                    withoutTransaction ? this.cardRepositoryEwt : this.cardRepository,
                    cancellationToken);

                if (isCreatedValidationResult
                    && !validationResult.IsSuccessful())
                {
                    throw new ValidationException(validationResult.Build());
                }

                return card;
            }

            if (scopeContext.Cards.TryGetValue(mainCardID, out card))
            {
                return card;
            }

            card = await this.GetMainCardInternalAsync(
                mainCardID,
                validationResult ?? scopeContext.ValidationResult,
                this.cardRepository,
                cancellationToken);
            if (card is not null)
            {
                scopeContext.Cards[mainCardID] = card;
            }
            return card;
        }

        /// <inheritdoc />
        public async Task<ICardFileContainer> GetMainCardFileContainerAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            if (scopeContext.CardFileContainers.TryGetValue(mainCardID, out var container))
            {
                return container;
            }

            var card = await this.GetMainCardAsync(mainCardID, validationResult ?? scopeContext.ValidationResult, cancellationToken: cancellationToken);
            if (card is not null)
            {
                container = await this.GetFileContainerInternalAsync(card, cancellationToken);
                if (container is not null)
                {
                    scopeContext.CardFileContainers[mainCardID] = container;
                }
            }

            return container;
        }

        /// <inheritdoc />
        public void ForceIncrementMainCardVersion(
            Guid mainCardID)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);
            scopeContext.ForceIncrementCardVersion.Add(mainCardID);
        }

        /// <inheritdoc />
        public async Task EnsureMainCardHasTaskHistoryAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            if (scopeContext.CardsWithTaskHistory.Contains(mainCardID))
            {
                return;
            }

            if (!scopeContext.Cards.TryGetValue(mainCardID, out var card))
            {
                throw new InvalidOperationException($"Card {mainCardID:B} not found in {nameof(KrScope)}.");
            }

            await this.LoadTaskHistoryAsync(card, validationResult ?? scopeContext.ValidationResult, cancellationToken);
            scopeContext.CardsWithTaskHistory.Add(mainCardID);
        }

        /// <inheritdoc />
        public async ValueTask<Card> GetKrSatelliteAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            bool noLockingMainCard = false,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            if (scopeContext is null)
            {
                return await GetKrSatelliteAsync(
                    mainCardID,
                    this.cardRepository,
                    validationResult,
                    noLockingMainCard,
                    cancellationToken);
            }

            if (scopeContext.MainKrSatellites.TryGetItem(mainCardID, out var satellite))
            {
                return satellite;
            }

            satellite = await GetKrSatelliteAsync(
                mainCardID,
                this.cardRepository,
                validationResult ?? scopeContext.ValidationResult,
                noLockingMainCard,
                cancellationToken);

            if (satellite is not null)
            {
                scopeContext.MainKrSatellites.Add(satellite);
                scopeContext.Cards.Add(satellite.ID, satellite);
            }

            return satellite;
        }

        /// <inheritdoc />
        public async ValueTask<Card> TryGetKrSatelliteAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;

            if (scopeContext is null)
            {
                return await this.TryGetSatelliteWithCheckAsync(
                    mainCardID,
                    this.cardRepository,
                    validationResult,
                    cancellationToken);
            }

            if (scopeContext.MainKrSatellites.TryGetItem(mainCardID, out var satellite))
            {
                return satellite;
            }

            satellite = await this.TryGetSatelliteWithCheckAsync(
                mainCardID,
                this.cardRepository,
                validationResult,
                cancellationToken);

            if (satellite is not null)
            {
                scopeContext.MainKrSatellites.Add(satellite);
                scopeContext.Cards.Add(satellite.ID, satellite);
            }

            return satellite;
        }

        /// <inheritdoc />
        public async ValueTask<Guid?> GetCurrentHistoryGroupAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            var satellite = await this.GetKrSatelliteAsync(mainCardID, validationResult, cancellationToken: cancellationToken);
            if (satellite is null)
            {
                return null;
            }

            return satellite.TryGetKrApprovalCommonInfoSection(out var aci)
                && aci.RawFields.TryGetValue(KrConstants.KrApprovalCommonInfo.CurrentHistoryGroup, out var chgObj)
                    ? chgObj as Guid?
                    : null;
        }

        /// <inheritdoc />
        public async Task SetCurrentHistoryGroupAsync(
            Guid mainCardID,
            Guid? newGroupHistoryID,
            IValidationResultBuilder validationResult = null,
             CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            var satellite = await this.GetKrSatelliteAsync(mainCardID, validationResult, cancellationToken: cancellationToken);
            if (satellite is not null
                && satellite.TryGetKrApprovalCommonInfoSection(out var aci))
            {
                aci.Fields[KrConstants.KrApprovalCommonInfo.CurrentHistoryGroup] = newGroupHistoryID;
            }
        }

        /// <inheritdoc />
        public async Task<Card> CreateSecondaryKrSatelliteAsync(
            Guid mainCardID,
            Guid processID,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            const string errorFormat = $"{DefaultCardTypes.KrSecondarySatelliteTypeName} already exists. Process ID = {{0}}.";

            if (scopeContext.SecondaryKrSatellites.ContainsKey(processID))
            {
                scopeContext.ValidationResult.AddError(
                    this,
                    errorFormat,
                    processID);
            }

            Card resSatellite = null;

            await this.cardTransactionStrategy.ExecuteInWriterLockAsync(
                mainCardID,
                CardComponentHelper.DoNotCheckVersion,
                scopeContext.ValidationResult,
                async p =>
                {
                    var satelliteID = await this.GetKrSecondarySatelliteIDByProcessIDAsync(
                        processID,
                        p.CancellationToken);

                    if (satelliteID.HasValue)
                    {
                        p.ValidationResult.AddError(
                            this,
                            errorFormat,
                            processID);

                        p.ReportError = true;
                        return;
                    }

                    var newResponse = await this.cardRepositoryEwt.NewAsync(
                        new CardNewRequest
                        {
                            CardTypeID = DefaultCardTypes.KrSecondarySatelliteTypeID,
                            NewMode = CardNewMode.Valid,
                        },
                        p.CancellationToken);

                    p.ValidationResult.Add(newResponse.ValidationResult);
                    if (!p.ValidationResult.IsSuccessful())
                    {
                        p.ReportError = true;
                        return;
                    }

                    var internalSatellite = newResponse.Card;
                    internalSatellite.ID = processID;

                    SetMainCardID(internalSatellite, p.CardID.Value);

                    var storedSatellite = internalSatellite.Clone();
                    storedSatellite.RemoveAllButChanged(CardStoreMode.Insert);

                    var storeResponse = await this.cardRepositoryEwt.StoreAsync(
                        new CardStoreRequest
                        {
                            Card = storedSatellite,
                        },
                        p.CancellationToken);

                    p.ValidationResult.Add(storeResponse.ValidationResult);
                    if (!storeResponse.ValidationResult.IsSuccessful())
                    {
                        p.ReportError = true;
                        return;
                    }

                    internalSatellite.RemoveChanges(CardRemoveChangesDeletedHandling.Remove);
                    internalSatellite.Version = storeResponse.CardVersion;

                    resSatellite = internalSatellite;
                },
                cancellationToken: cancellationToken);

            if (resSatellite is not null)
            {
                scopeContext.SecondaryKrSatellites[processID] = resSatellite;
                scopeContext.Cards.Add(resSatellite.ID, resSatellite);
            }

            return resSatellite;
        }

        /// <inheritdoc />
        public async ValueTask<Card> GetSecondaryKrSatelliteAsync(
            Guid processID,
            CancellationToken cancellationToken = default)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            if (scopeContext.SecondaryKrSatellites.TryGetValue(processID, out var satellite))
            {
                return satellite;
            }

            var satelliteID = await this.GetKrSecondarySatelliteIDByProcessIDAsync(
                processID,
                cancellationToken);

            if (!satelliteID.HasValue)
            {
                return null;
            }

            satellite = await GetSatelliteAsync(
                satelliteID.Value,
                null,
                this.cardRepository,
                scopeContext.ValidationResult,
                cancellationToken: cancellationToken);

            if (satellite is not null)
            {
                scopeContext.SecondaryKrSatellites[processID] = satellite;
                scopeContext.Cards.Add(satellite.ID, satellite);
            }

            return satellite;
        }

        /// <inheritdoc />
        public Guid? LockCard(
            Guid cardID)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            if (scopeContext.Locks.Contains(cardID))
            {
                return null;
            }

            var key = Guid.NewGuid();
            scopeContext.Locks.Add(cardID);
            scopeContext.LockKeys[cardID] = key;
            return key;
        }

        /// <inheritdoc />
        public bool IsCardLocked(
            Guid cardID)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            return scopeContext.Locks.Contains(cardID);
        }

        /// <inheritdoc />
        public bool ReleaseCard(
            Guid cardID,
            Guid? key)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            if (!scopeContext.Locks.Contains(cardID)
                || scopeContext.LockKeys.TryGetValue(cardID, out var cardKey) && cardKey != key)
            {
                return false;
            }

            scopeContext.Locks.Remove(cardID);
            scopeContext.LockKeys.Remove(cardID);
            return true;
        }

        /// <inheritdoc />
        public void AddProcessHolder(
            ProcessHolder processHolder)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            scopeContext.ProcessHolders.Add(processHolder);
        }

        /// <inheritdoc />
        public ProcessHolder GetProcessHolder(
            Guid processHolderID)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            return scopeContext.ProcessHolders.TryGetItem(processHolderID, out var holder)
                ? holder
                : null;
        }

        /// <inheritdoc />
        public void RemoveProcessHolder(
            Guid processHolderID)
        {
            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            scopeContext.ProcessHolders.RemoveByKey(processHolderID);
        }

        /// <inheritdoc/>
        public void AddDisposableObject(
            IDisposable obj)
        {
            ThrowIfNull(obj);

            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            scopeContext.DisposableObjects.Add(obj);
        }

        /// <inheritdoc/>
        public void AddDisposableObject(
            IAsyncDisposable obj)
        {
            ThrowIfNull(obj);

            var scopeContext = KrScopeContext.Current;
            AssertNonEmptyKrScopeContext(scopeContext);

            scopeContext.AsyncDisposableObjects.Add(obj);
        }

        #endregion

        #region Private Methods

        private static Task<Card> GetKrSatelliteAsync(
            Guid cardID,
            ICardRepository cardRepository,
            IValidationResultBuilder validationResult,
            bool noLockingMainCard,
            CancellationToken cancellationToken = default)
        {
            return GetSatelliteAsync(
                cardID,
                DefaultCardTypes.KrSatelliteTypeID,
                cardRepository,
                validationResult,
                noLockingMainCard,
                cancellationToken);
        }

        /// <summary>
        /// Возвращает основной сателлит (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>).
        /// </summary>
        /// <param name="cardID">Идентификатор основной карточки.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="validationResult">Объект для построения результатов валидации. Может иметь значение <see langword="null"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Основной сателлит или значение <see langword="null"/>, если он не существует или произошла ошибка.</returns>
        private async Task<Card> TryGetSatelliteWithCheckAsync(
            Guid cardID,
            ICardRepository cardRepository,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var satelliteID = await KrProcessHelper.GetKrSatelliteIDAsync(
                cardID,
                this.dbScope,
                cancellationToken);

            return satelliteID.HasValue
                ? await GetSatelliteAsync(
                    satelliteID.Value,
                    null,
                    cardRepository,
                    validationResult,
                    cancellationToken: cancellationToken)
                : null;
        }

        /// <summary>
        /// Возвращает сателлит.
        /// </summary>
        /// <param name="cardID">Идентификатор основной карточки.</param>
        /// <param name="satelliteTypeID">Идентификатор типа сателлита. Если указано значение <see langword="null"/>, то автоматическое создание не выполняется.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="noLockingMainCard">Значение <see langword="true"/>, если не следует выполнять блокировку основной карточки при создании сателлита, иначе - <see langword="false"/>.</param>
        /// <param name="validationResult">Объект для построения результатов валидации. Может иметь значение <see langword="null"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Основной сателлит или значение <see langword="null"/>, если произошла ошибка.</returns>
        private static async Task<Card> GetSatelliteAsync(
            Guid cardID,
            Guid? satelliteTypeID,
            ICardRepository cardRepository,
            IValidationResultBuilder validationResult,
            bool noLockingMainCard = false,
            CancellationToken cancellationToken = default)
        {
            // На сателлит права не используются, поэтому токен ему не нужен.

            var request = new CardGetRequest
            {
                CardID = cardID,
                CardTypeID = satelliteTypeID,
                GetMode = CardGetMode.ReadOnly,
                RestrictionFlags = CardGetRestrictionValues.Satellite,
            };

            if (noLockingMainCard)
            {
                request.SetNoLockingMainCard(true);
            }

            var response = await cardRepository.GetAsync(request, cancellationToken);
            validationResult?.Add(response.ValidationResult);

            return response.ValidationResult.IsSuccessful()
                ? response.Card
                : null;
        }

        private static void SetMainCardID(Card satelliteCard, Guid mainCardID)
        {
            CardSatelliteHelper.SetupUniversalSatellite(satelliteCard, mainCardID);
            if (!satelliteCard.TryGetKrApprovalCommonInfoSection(out var satelliteInfoSection))
            {
                return;
            }
            satelliteInfoSection.Fields[KrConstants.KrProcessCommonInfo.MainCardID] = mainCardID;
        }

        private async Task<Card> GetMainCardInternalAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult,
            ICardRepository cardRepository,
            CancellationToken cancellationToken = default)
        {
            var request = new CardGetRequest
            {
                CardID = mainCardID,
                GetMode = CardGetMode.ReadOnly,
                RestrictionFlags = CardGetRestrictionFlags.RestrictTasks | CardGetRestrictionFlags.RestrictTaskHistory,
            };
            request.IgnoreButtons();
            request.IgnoreKrSatellite();
            request.SetForbidStoringHistory(true);
            // Основной карточке создаем токен, чтобы процесс мог к ней обращаться
            // вне зависимости от прав пользователя.
            var token = this.tokenProvider.CreateFullToken(mainCardID);
            token.Set(request.Info);

            var response = await cardRepository.GetAsync(request, cancellationToken);
            validationResult.Add(response.ValidationResult);

            return response.ValidationResult.IsSuccessful()
                ? response.Card
                : null;
        }

        /// <summary>
        /// Возвращает контейнер, содержащий информацию по карточке и её файлам созданный для указанной карточки.
        /// </summary>
        /// <param name="card">Карточка для которой должны быть создан файловый контейнер.</param>
        /// <param name="cancellationToken">Объект, посредством которого может быть отменена асинхронная задача.</param>
        /// <returns>Контейнер, содержащий информацию по карточке и её файлам.</returns>
        private async Task<ICardFileContainer> GetFileContainerInternalAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            ICardFileContainer container = null;

            try
            {
                container = await this.cardFileManager.CreateContainerAsync(card, cancellationToken: cancellationToken);
                if (!container.CreationResult.IsSuccessful)
                {
                    return null;
                }

                var result = container;
                container = null;

                return result;
            }
            finally
            {
                if (container is not null)
                {
                    await container.DisposeAsync();
                }
            }
        }

        private async Task LoadTaskHistoryAsync(Card card, IValidationResultBuilder validationResult, CancellationToken cancellationToken = default)
        {
            var insertedHistoryItems = card
                .TaskHistory
                .Where(p => p.State == CardTaskHistoryState.Inserted)
                .ToList();
            var insertedGroupItems = card
                .TaskHistoryGroups
                .Where(p => p.State == CardTaskHistoryState.Inserted)
                .ToList();

            card.TaskHistory.Clear();
            card.TaskHistoryGroups.Clear();

            await using (this.dbScope.Create())
            {
                await this.cardGetStrategy.LoadTaskHistoryAsync(
                    card.ID,
                    card,
                    this.dbScope.Db,
                    this.cardMetadata,
                    validationResult,
                    new Dictionary<Guid, CardTask>(),
                    cancellationToken: cancellationToken);
            }

            card.TaskHistory.AddRange(insertedHistoryItems);
            card.TaskHistoryGroups.AddRange(insertedGroupItems);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void AssertNonEmptyKrScopeContext(KrScopeContext scopeContext)
        {
            if (scopeContext is null)
            {
                throw new InvalidOperationException($"Code called outside of {nameof(KrScopeContext)}.");
            }
        }

        private Task<Guid?> GetKrSecondarySatelliteIDByProcessIDAsync(
            Guid processID,
            CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            return db
                .SetCommand(
                    this.dbScope.BuilderFactory
                        .Select()
                        .C("ID")
                        .From("WorkflowProcesses").NoLock()
                        .Where().C("RowID").Equals().P(nameof(processID))
                        .Build(),
                    db.Parameter(nameof(processID), processID))
                .LogCommand()
                .ExecuteAsync<Guid?>(cancellationToken);
        }

        #endregion
    }
}
