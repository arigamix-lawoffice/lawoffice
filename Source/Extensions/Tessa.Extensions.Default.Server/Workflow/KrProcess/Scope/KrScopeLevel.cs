using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Numbers;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.ObjectLocking;
using Tessa.Platform.Scopes;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    /// <summary>
    /// Объект, предоставляющий методы для управления текущим контекстом <see cref="KrScopeContext"/>.
    /// </summary>
    /// <remarks>После завершения работы с объектом, для выполнения задач связанных с освобождением ресурсов, вызовите метод <see cref="ExitAsync(IValidationResultBuilder)"/>.</remarks>
    public sealed class KrScopeLevel
    {
        #region Fields

        private readonly ICardRepository cardRepository;
        private readonly ICardRepository cardRepositoryEwt;
        private readonly IKrTokenProvider tokenProvider;
        private readonly IKrTypesCache krTypesCache;
        private readonly IKrStageSerializer serializer;
        private readonly ICardGetStrategy getStrategy;
        private readonly IDbScope dbScope;
        private readonly ICardMetadata cardMetadata;
        private readonly ICardStreamServerRepository cardStreamServerRepository;
        private readonly ICardStreamServerRepository cardStreamServerRepositoryEwt;

        private readonly IInheritableScopeInstance<KrScopeContext> scope;

        #endregion

        #region Constructors

        public KrScopeLevel(
            ICardRepository cardRepository,
            ICardRepository cardRepositoryEwt,
            IKrTokenProvider tokenProvider,
            IKrTypesCache krTypesCache,
            IKrStageSerializer serializer,
            ICardGetStrategy getStrategy,
            ICardTransactionStrategy cardCardTransactionStrategy,
            IDbScope dbScope,
            ICardMetadata cardMetadata,
            ICardStreamServerRepository cardStreamServerRepository,
            ICardStreamServerRepository cardStreamServerRepositoryEwt)
        {
            this.cardRepository = NotNullOrThrow(cardRepository);
            this.cardRepositoryEwt = NotNullOrThrow(cardRepositoryEwt);
            this.tokenProvider = NotNullOrThrow(tokenProvider);
            this.krTypesCache = NotNullOrThrow(krTypesCache);
            this.serializer = NotNullOrThrow(serializer);
            this.getStrategy = NotNullOrThrow(getStrategy);
            this.CardTransactionStrategy = NotNullOrThrow(cardCardTransactionStrategy);
            this.dbScope = NotNullOrThrow(dbScope);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.cardStreamServerRepository = NotNullOrThrow(cardStreamServerRepository);
            this.cardStreamServerRepositoryEwt = NotNullOrThrow(cardStreamServerRepositoryEwt);

            this.scope = KrScopeContext.Create();
            this.scope.Value.LevelStack.Push(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public Guid LevelID { get; } = Guid.NewGuid();

        /// <summary>
        /// Значение, показывающее, что для этого объекта были выполнены действия по освобождению ресурсов.
        /// </summary>
        public bool Exited { get; private set; } // = false;

        /// <inheritdoc cref="ICardTransactionStrategy" path="/summary"/>
        public ICardTransactionStrategy CardTransactionStrategy { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Обрабатывает изменения в основной карточке и её сателлитах, содержащих информацию о процессах.
        /// </summary>
        /// <param name="mainCardID">Идентификатор карточки документа, в которой запущен процесс.</param>
        /// <param name="isExistsMainCard">Значение <see langword="true"/>, если основная карточка существует, иначе - <see langword="false"/>.</param>
        /// <param name="validationResult"><inheritdoc cref="IValidationResultBuilder" path="/summary"/></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task ApplyChangesAsync(
            Guid mainCardID,
            bool isExistsMainCard,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(validationResult);

            var scopeContext = KrScopeContext.Current;
            if (scopeContext is null)
            {
                return;
            }

            var locks = scopeContext.Locks;

            if (scopeContext.MainKrSatellites.TryGetItem(mainCardID, out var satellite)
                && !locks.Contains(satellite.ID))
            {
                ProcessInfoCacheHelper.Update(this.serializer, satellite);
                if (satellite.StoreMode == CardStoreMode.Insert || satellite.HasChanges())
                {
                    if (isExistsMainCard)
                    {
                        await this.CardTransactionStrategy.ExecuteInWriterLockAsync(
                            mainCardID,
                            CardComponentHelper.DoNotCheckVersion,
                            validationResult,
                            async p =>
                            {
                                await this.StoreCardAsync(
                                    this.cardRepository,
                                    this.cardStreamServerRepository,
                                    satellite,
                                    null,
                                    p.ValidationResult,
                                    cancellationToken: p.CancellationToken);

                                p.ReportError = !p.ValidationResult.IsSuccessful();
                            },
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        await this.StoreCardAsync(
                            this.cardRepository,
                            this.cardStreamServerRepository,
                            satellite,
                            null,
                            validationResult,
                            cancellationToken: cancellationToken);
                    }
                }
            }

            if (!validationResult.IsSuccessful())
            {
                return;
            }

            var forceAffectVersion = scopeContext.ForceIncrementCardVersion.Remove(mainCardID);
            if (scopeContext.Cards.TryGetValue(mainCardID, out var mainCard)
                && !locks.Contains(mainCard.ID))
            {
                if (mainCard.HasChanges()
                    || mainCard.TryGetWorkflowQueue()?.Items?.Count > 0
                    || mainCard.HasNumberQueueToProcess()
                    || forceAffectVersion)
                {
                    ICardRepository suitableCardRepository;
                    ICardStreamServerRepository suitableCardStreamServerRepository;

                    if (TransactionScopeContext.HasCurrent
                        && TransactionScopeContext.Current.Locks.TryGetValue(mainCard.ID, out var objectLockInfo)
                        && objectLockInfo.LockType == ObjectLockTypes.WriteLock)
                    {
                        suitableCardRepository = this.cardRepositoryEwt;
                        suitableCardStreamServerRepository = this.cardStreamServerRepositoryEwt;
                    }
                    else
                    {
                        suitableCardRepository = this.cardRepository;
                        suitableCardStreamServerRepository = this.cardStreamServerRepository;
                    }

                    scopeContext.CardFileContainers.TryGetValue(mainCardID, out var container);
                    await this.StoreCardAsync(
                        suitableCardRepository,
                        suitableCardStreamServerRepository,
                        mainCard,
                        container,
                        validationResult,
                        forceAffectVersion: forceAffectVersion,
                        cancellationToken: cancellationToken);
                    // Освобождение ресурсов файлового контейнера будет выполнено в методе KrScopeLevel.Exit() после завершения сохранения карточки или при вызове KrScopeLevel.Dispose().
                }
            }
            else if (forceAffectVersion)
            {
                // Карточка не загружена, но нужно обязательно увеличить ее версию
                await this.ForceIncrementVersionAsync(
                    mainCardID,
                    validationResult,
                    cancellationToken);
            }

            if (!validationResult.IsSuccessful())
            {
                return;
            }

            var deletedSecondaryKrSatellites = new List<KeyValuePair<Guid, Card>>(scopeContext.SecondaryKrSatellites.Count);
            foreach (var secondarySatellitePair in scopeContext.SecondaryKrSatellites)
            {
                var secondarySatellite = secondarySatellitePair.Value;
                var secondaryProcessMainCardID = secondarySatellite
                    .GetApprovalInfoSection()
                    .Fields[KrConstants.KrProcessCommonInfo.MainCardID];

                if (locks.Contains(secondarySatellite.ID)
                    || !secondaryProcessMainCardID.Equals(mainCardID))
                {
                    continue;
                }

                var currentRowID = secondarySatellite
                    .GetApprovalInfoSection()
                    .Fields[KrConstants.KrProcessCommonInfo.CurrentApprovalStageRowID];

                // Процесс по сателлиту закончился, сателлит больше не нужен.
                if (currentRowID is null)
                {
                    // Если карточка уже создана, ее нужно удалить
                    if (secondarySatellite.StoreMode == CardStoreMode.Update)
                    {
                        await this.DeleteCardAsync(
                            secondarySatellite,
                            validationResult,
                            cancellationToken);
                    }
                    // Если карточка только создана, но не сохранялась, можно просто забыть про нее.

                    deletedSecondaryKrSatellites.Add(secondarySatellitePair);
                }
                else
                {
                    ProcessInfoCacheHelper.Update(this.serializer, secondarySatellite);
                    if (secondarySatellite.StoreMode == CardStoreMode.Insert
                        || secondarySatellite.HasChanges())
                    {
                        await this.CardTransactionStrategy.ExecuteInWriterLockAsync(
                            mainCardID,
                            CardComponentHelper.DoNotCheckVersion,
                            validationResult,
                            async p =>
                            {
                                await this.StoreCardAsync(
                                    this.cardRepository,
                                    this.cardStreamServerRepository,
                                    secondarySatellite,
                                    null,
                                    p.ValidationResult,
                                    cancellationToken: p.CancellationToken);

                                p.ReportError = !p.ValidationResult.IsSuccessful();
                            },
                            cancellationToken: cancellationToken);
                    }
                }
            }

            foreach (var deletedSecondarySatellitePair in deletedSecondaryKrSatellites)
            {
                scopeContext.SecondaryKrSatellites.Remove(deletedSecondarySatellitePair.Key);
                scopeContext.Cards.Remove(deletedSecondarySatellitePair.Value.ID);
            }
        }

        /// <summary>
        /// Выполняет задачи, связанные с высвобождением ресурсов этого объекта.
        /// </summary>
        /// <param name="validationResult"><inheritdoc cref="IValidationResultBuilder" path="/summary"/></param>
        public async ValueTask ExitAsync(
            IValidationResultBuilder validationResult)
        {
            ThrowIfNull(validationResult);

            if (this.Exited)
            {
                return;
            }

            this.Exited = true;

            if (this.scope.Value.LevelStack.Pop() != this)
            {
                validationResult.AddError(this, "Trying to exit from non-top level.");
                return;
            }

            var ctx = KrScopeContext.Current;
            this.scope.Dispose();

            if (!ctx.IsDisposed)
            {
                return;
            }

            foreach ((_, var container) in ctx.CardFileContainers)
            {
                if (container is not null)
                {
                    try
                    {
                        await container.DisposeAsync();
                    }
                    catch (Exception ex)
                    {
                        validationResult.AddException(this, ex, true);
                    }
                }
            }

            foreach (var disposableObject in ctx.DisposableObjects)
            {
                try
                {
                    disposableObject?.Dispose();
                }
                catch (Exception ex)
                {
                    validationResult.AddException(this, ex, true);
                }
            }

            foreach (var disposableObject in ctx.AsyncDisposableObjects)
            {
                if (disposableObject is not null)
                {
                    try
                    {
                        await disposableObject.DisposeAsync();
                    }
                    catch (Exception ex)
                    {
                        validationResult.AddException(this, ex, true);
                    }
                }
            }

            if (ctx.Locks.Count != 0)
            {
                validationResult.AddError(
                    this,
                    $"Disposed KrScope contains locks ({string.Join(", ", ctx.Locks)}). " +
                    "All cards inside scope must be saved at the end (no card should be locked).");
            }
        }

        #endregion

        #region Private Methods

        private async Task StoreCardAsync(
            ICardRepository cardRepository,
            ICardStreamServerRepository cardStreamServerRepository,
            Card card,
            ICardFileContainer fileContainer,
            IValidationResultBuilder validationResult,
            bool forceAffectVersion = false,
            CancellationToken cancellationToken = default)
        {
            var storedCard = card.Clone();
            card.RemoveChanges(CardRemoveChangesDeletedHandling.Remove);
            card.RemoveWorkflowQueue();
            card.RemoveNumberQueue();

            await this.StoreCardAsync(
                cardRepository,
                cardStreamServerRepository,
                card,
                storedCard,
                fileContainer,
                validationResult,
                forceAffectVersion,
                cancellationToken);
        }

        private async Task StoreCardAsync(
            ICardRepository cardRepository,
            ICardStreamServerRepository cardStreamServerRepository,
            Card originalCard,
            Card storedCard,
            ICardFileContainer fileContainer,
            IValidationResultBuilder validationResult,
            bool forceAffectVersion = false,
            CancellationToken cancellationToken = default)
        {
            var storeMode = storedCard.StoreMode;
            if (storeMode == CardStoreMode.Update)
            {
                storedCard.UpdateStates();
            }

            storedCard.RemoveAllButChanged(storeMode);
            var request = new CardStoreRequest
            {
                Card = storedCard,
                AffectVersion = forceAffectVersion,
            };

            if (await KrComponentsHelper.HasBaseAsync(originalCard.TypeID, this.krTypesCache, cancellationToken))
            {
                this.tokenProvider.CreateFullToken(storedCard).Set(storedCard.Info);
            }

            var digest = await cardRepository.GetDigestAsync(
                originalCard,
                CardDigestEventNames.ActionHistoryStoreRouteProcess,
                cancellationToken);

            if (digest is not null)
            {
                request.SetDigest(digest);
            }

            var response = await CardHelper.StoreAsync(
                request,
                fileContainer?.FileContainer,
                cardRepository,
                cardStreamServerRepository,
                cancellationToken);

            validationResult.Add(response.ValidationResult);
            originalCard.Version = response.CardVersion;
        }

        private async Task DeleteCardAsync(
            Card card,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var request = new CardDeleteRequest
            {
                CardID = card.ID,
                CardTypeID = card.TypeID,
                DeletionMode = CardDeletionMode.WithoutBackup,
            };
            var resp = await this.cardRepository.DeleteAsync(request, cancellationToken);
            validationResult.Add(resp.ValidationResult);
        }

        private Task ForceIncrementVersionAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            return this.CardTransactionStrategy.ExecuteInReaderLockAsync(
                mainCardID,
                validationResult,
                this.ForceIncrementVersionInternalAsync,
                cancellationToken
            );
        }

        private async Task ForceIncrementVersionInternalAsync(
            ICardTransactionParameter p)
        {
            var getContext = await this.getStrategy.TryLoadCardInstanceAsync(
                p.CardID.Value,
                this.dbScope.Db,
                this.cardMetadata,
                p.ValidationResult,
                cancellationToken: p.CancellationToken);

            if (!p.ValidationResult.IsSuccessful())
            {
                p.ReportError = true;
                return;
            }

            var card = getContext.Card;
            var token = this.tokenProvider.CreateFullToken(card);
            token.Set(card.Info);

            string digest;
            var workflowRequest = WorkflowScopeContext.Current.StoreContext?.Request;
            if (workflowRequest is not null)
            {
                digest = workflowRequest.TryGetDigest();
            }
            else if (await this.getStrategy.LoadSectionsAsync(getContext, p.CancellationToken))
            {
                digest = await this.cardRepository.GetDigestAsync(
                    card,
                    CardDigestEventNames.ActionHistoryStoreRouteProcess,
                    p.CancellationToken);

                card.RemoveAllButChanged();
            }
            else
            {
                digest = null;
            }

            var storeRequest = new CardStoreRequest
            {
                Card = card,
                AffectVersion = true,
            };
            storeRequest.SetDigest(digest);

            var storeResponse = await this.cardRepository.StoreAsync(
                storeRequest,
                p.CancellationToken);

            p.ValidationResult.Add(storeResponse.ValidationResult);

            if (!p.ValidationResult.IsSuccessful())
            {
                p.ReportError = true;
            }
        }

        #endregion
    }
}
