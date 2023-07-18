using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Numbers;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет базовую реализацию <see cref="ICardLifecycleCompanion{T}"/>.
    /// </summary>
    /// <typeparam name="T">Тип объекта, запланированные действия которого выполняются методом <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.</typeparam>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "()}")]
    public class CardLifecycleCompanion<T> :
        PendingActionsProvider<IPendingAction, T>,
        ICardLifecycleCompanion<T>
        where T : CardLifecycleCompanion<T>
    {
        #region Fields

        private ICardFileContainer cardFileContainer;

        private readonly ParentStageRowIDVisitor visitor;

        private readonly T thisObj;

        private readonly CardLifecycleCompanionData lastData = new CardLifecycleCompanionData();

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion{T}"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        private CardLifecycleCompanion(
            ICardLifecycleCompanionDependencies deps)
        {
            Check.ArgumentNotNull(deps, nameof(deps));

            this.Dependencies = deps;
            this.visitor = new ParentStageRowIDVisitor(deps.CardMetadata);
            this.thisObj = (T) this;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion{T}"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="cardTypeName">Имя типа карточки.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public CardLifecycleCompanion(
            Guid cardID,
            Guid? cardTypeID,
            string cardTypeName,
            ICardLifecycleCompanionDependencies deps)
            : this(deps)
        {
            this.CardID = cardID;
            this.CardTypeID = cardTypeID;
            this.CardTypeName = cardTypeName;
            this.Card = default;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion{T}"/>.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="cardTypeName">Имя типа карточки.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public CardLifecycleCompanion(
            Guid? cardTypeID,
            string cardTypeName,
            ICardLifecycleCompanionDependencies deps)
            : this(Guid.NewGuid(), cardTypeID, cardTypeName, deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion{T}"/>.
        /// </summary>
        /// <param name="card">Карточка, жизненным циклом которой необходимо управлять.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public CardLifecycleCompanion(
            Card card,
            ICardLifecycleCompanionDependencies deps)
            : this(deps)
        {
            Check.ArgumentNotNull(card, nameof(card));

            this.CardID = card.ID;
            this.CardTypeID = card.TypeID;
            this.CardTypeName = card.TypeName;
            this.Card = card;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public Guid? CardTypeID { get; protected set; }

        /// <inheritdoc/>
        public string CardTypeName { get; protected set; }

        /// <inheritdoc/>
        public Guid CardID { get; protected set; }

        /// <inheritdoc/>
        public Card Card { get; protected set; }

        /// <inheritdoc/>
        public ICardLifecycleCompanionDependencies Dependencies { get; }

        /// <inheritdoc/>
        public ICardLifecycleCompanionData LastData => this.lastData;

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public virtual async ValueTask<ICardFileContainer> GetCardFileContainerAsync(
            IFileRequest request = default,
            IList<IFileTag> additionalTags = default,
            CancellationToken cancellationToken = default)
        {
            return this.cardFileContainer ??= await this.Dependencies.CardFileManager.CreateContainerAsync(
                this.GetCardOrThrow(),
                request: request,
                additionalTags: additionalTags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public virtual T Create(Action<CardNewRequest> modifyRequestAction = null)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanion<T>) + "." + nameof(this.Create),
                    (action, ct) =>
                        this.CreateActionAsync(action, modifyRequestAction, ct)));
            return this.thisObj;
        }

        /// <inheritdoc/>
        public virtual T Save(Action<CardStoreRequest> modifyRequestAction = null)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanion<T>) + "." + nameof(this.Save),
                    (action, ct) =>
                        this.SaveActionAsync(action, modifyRequestAction, ct)));
            return this.thisObj;
        }

        /// <inheritdoc/>
        public virtual T Load(Action<CardGetRequest> modifyRequestAction = null)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanion<T>) + "." + nameof(this.Load),
                    (action, ct) =>
                        this.LoadActionAsync(action, modifyRequestAction, ct)));
            return this.thisObj;
        }

        /// <inheritdoc/>
        public virtual T Delete(Action<CardDeleteRequest> modifyRequestAction = null)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanion<T>) + "." + nameof(this.Delete),
                    (action, ct) =>
                        this.DeleteActionAsync(action, modifyRequestAction, ct)));
            return this.thisObj;
        }

        /// <inheritdoc/>
        public T WithInfoPair(
            string key,
            object val)
        {
            var pendingAction = this.GetLastPendingAction();
            pendingAction.Info[key] = val;
            return this.thisObj;
        }

        /// <inheritdoc/>
        public T WithInfo(
            Dictionary<string, object> info)
        {
            this.GetLastPendingAction().SetInfo(info);
            return this.thisObj;
        }

        /// <inheritdoc/>
        public Card GetCardOrThrow()
        {
            var card = this.Card;
            if (card is null)
            {
                throw new InvalidOperationException("Card isn't specified.");
            }

            return card;
        }

        /// <inheritdoc/>
        public virtual T CreateOrLoadSingleton()
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanion) + "." + nameof(CreateOrLoadSingleton),
                    this.CreateOrLoadSingletonAsync));

            return this.thisObj;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask<T> GoCoreAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            await using (this.Dependencies.DbScope.Create())
            {
                return await base.GoCoreAsync(validationFunc: validationFunc, cancellationToken: cancellationToken);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Проверяет, что <see cref="Card"/> имеет значение <see langword="null"/>, если это не так, то создаёт исключение <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Card already specified.</exception>
        protected void CheckCardNotExists()
        {
            if (this.Card is not null)
            {
                throw new InvalidOperationException("Card already specified.");
            }
        }

        /// <summary>
        /// Проверяет данные указанные в запросе на соответствие ожидаемым значениям.
        /// </summary>
        /// <param name="actualCardID">Идентификатор карточки в запросе.</param>
        /// <param name="expectedCardID">Ожидаемый идентификатор карточки.</param>
        /// <param name="additionalMessage">Сообщение, добавляемое к описанию ошибки.</param>
        protected static void CheckRequest(
            Guid? actualCardID,
            Guid? expectedCardID,
            string additionalMessage = null)
        {
            if (actualCardID != expectedCardID)
            {
                if (!string.IsNullOrEmpty(additionalMessage))
                {
                    additionalMessage = " " + additionalMessage;
                }

                throw new InvalidOperationException(
                    $"The request parameters do not match the expected card properties.{additionalMessage}{Environment.NewLine}" +
                    $"Actual card ID = {FormattingHelper.FormatNullable(actualCardID, "N")}, expected card ID = {FormattingHelper.FormatNullable(expectedCardID, "N")}.");
            }
        }

        #endregion

        #region Private methods

        private async ValueTask<ValidationResult> CreateActionAsync(
            IPendingAction action,
            Action<CardNewRequest> modifyRequestAction = null,
            CancellationToken cancellationToken = default)
        {
            this.CheckCardNotExists();

            var newRequest = new CardNewRequest
            {
                CardTypeID = this.CardTypeID,
                CardTypeName = this.CardTypeName,
            };

            if (action is not null)
            {
                newRequest.Info = action.Info;
            }

            this.Dependencies.RequestExtender?.ExtendNewRequest(newRequest);

            modifyRequestAction?.Invoke(newRequest);

            this.lastData.NewRequest = newRequest;
            this.lastData.NewResponse = default;

            var newResponse = await this.Dependencies.CardRepository.NewAsync(
                newRequest,
                cancellationToken);
            this.lastData.NewResponse = newResponse;

            if (newResponse.ValidationResult.IsSuccessful())
            {
                this.Card = newResponse.Card;

                if (newResponse.Card.ID == Guid.Empty)
                {
                    if (this.CardID == Guid.Empty)
                    {
                        this.CardID = Guid.NewGuid();
                    }

                    newResponse.Card.ID = this.CardID;
                }
                else
                {
                    this.CardID = this.Card.ID;
                }

                this.CardTypeID = this.Card.TypeID;
                this.CardTypeName = this.Card.TypeName;
            }
            else
            {
                this.Card = default;
            }

            return newResponse.ValidationResult.Build();
        }

        private async ValueTask<ValidationResult> SaveActionAsync(
            IPendingAction action,
            Action<CardStoreRequest> modifyRequestAction = null,
            CancellationToken cancellationToken = default)
        {
            var card = this.GetCardOrThrow();

            if (this.Dependencies.ServerSide)
            {
                if (card.Sections.ContainsKey(KrConstants.KrStages.Virtual))
                {
                    await this.visitor.VisitAsync(
                        card.Sections,
                        DefaultCardTypes.KrCardTypeID,
                        KrConstants.KrStages.Virtual,
                        cancellationToken: cancellationToken);
                }
                card.RemoveAllButChanged(card.StoreMode);
            }

            var expectedCardID = card.ID;
            var storeRequest = new CardStoreRequest
            {
                Card = card,
                Info = action.Info,
            };

            this.Dependencies.RequestExtender?.ExtendStoreRequest(storeRequest);

            modifyRequestAction?.Invoke(storeRequest);

            var storeRequestCard = storeRequest.TryGetCard();

            CheckRequest(
                storeRequestCard?.ID,
                expectedCardID);

            this.lastData.StoreRequest = storeRequest;
            this.lastData.StoreResponse = default;

            CardStoreResponse storeResponse;
            if (this.cardFileContainer is null)
            {
                storeResponse = await this.Dependencies.CardRepository.StoreAsync(
                    storeRequest,
                    cancellationToken);
            }
            else
            {
                if (this.Dependencies.ServerSide)
                {
                    storeResponse = await CardHelper.StoreAsync(
                        storeRequest,
                        this.cardFileContainer?.FileContainer,
                        this.Dependencies.CardRepository,
                        this.Dependencies.CardStreamServerRepository,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    storeResponse = await CardHelper.StoreAsync(
                        storeRequest,
                        this.cardFileContainer?.FileContainer,
                        this.Dependencies.CardRepository,
                        this.Dependencies.CardStreamClientRepository,
                        cancellationToken: cancellationToken);
                }

                if (this.cardFileContainer is not null)
                {
                    await this.cardFileContainer.DisposeAsync();
                    this.cardFileContainer = default;
                }
            }

            this.lastData.StoreResponse = storeResponse;

            card.RemoveWorkflowQueue();
            card.RemoveNumberQueue();

            return storeResponse.ValidationResult.IsSuccessful()
                ? storeResponse.ValidationResult.Add(
                    await this.LoadActionAsync(
                        null,
                        null,
                        cancellationToken)).Build()
                : storeResponse.ValidationResult.Build();
        }

        private async ValueTask<ValidationResult> DeleteActionAsync(
            IPendingAction action,
            Action<CardDeleteRequest> modifyRequestAction = null,
            CancellationToken cancellationToken = default)
        {
            var deleteRequest = new CardDeleteRequest
            {
                CardID = this.CardID,
                CardTypeID = this.CardTypeID,
                CardTypeName = this.CardTypeName,
                Info = action.Info,
                DeletionMode = CardDeletionMode.WithoutBackup,
            };

            this.Dependencies.RequestExtender?.ExtendDeleteRequest(deleteRequest);

            modifyRequestAction?.Invoke(deleteRequest);

            CheckRequest(
                deleteRequest.CardID,
                this.CardID);

            this.lastData.DeleteRequest = deleteRequest;
            this.lastData.DeleteResponse = default;

            var deleteResponse = await this.Dependencies.CardRepository.DeleteAsync(
                deleteRequest,
                cancellationToken);
            this.lastData.DeleteResponse = deleteResponse;

            var validationResult = deleteResponse.ValidationResult.Build();
            if (validationResult.IsSuccessful)
            {
                this.Card = default;
                this.cardFileContainer = default;
            }

            return validationResult;
        }

        private async ValueTask<ValidationResult> LoadActionAsync(
            IPendingAction action,
            Action<CardGetRequest> modifyRequestAction = null,
            CancellationToken cancellationToken = default)
        {
            var getRequest = new CardGetRequest
            {
                CardID = this.CardID,
                CardTypeID = this.CardTypeID,
                CardTypeName = this.CardTypeName,
                CompressionMode = CardCompressionMode.None,
            };

            if (action is not null)
            {
                getRequest.Info = action.Info;
            }

            this.Dependencies.RequestExtender?.ExtendGetRequest(getRequest);

            modifyRequestAction?.Invoke(getRequest);

            this.lastData.GetRequest = getRequest;
            this.lastData.GetResponse = default;

            var getResponse = await this.Dependencies.CardRepository.GetAsync(
                getRequest,
                cancellationToken);
            this.lastData.GetResponse = getResponse;

            if (getResponse.ValidationResult.IsSuccessful())
            {
                this.Card = getResponse.Card;
                this.CardID = this.Card.ID;
                this.CardTypeID = this.Card.TypeID;
                this.CardTypeName = this.Card.TypeName;
            }
            else
            {
                this.Card = default;
            }

            this.cardFileContainer = default;

            return getResponse.ValidationResult.Build();
        }

        private async ValueTask<ValidationResult> CreateOrLoadSingletonAsync(
            IPendingAction action,
            CancellationToken cancellationToken = default)
        {
            this.CheckCardNotExists();

            if (string.IsNullOrEmpty(this.CardTypeName))
            {
                throw new InvalidOperationException(nameof(this.CardTypeName) + " can't be null or empty.");
            }

            var value = await this.Dependencies.CardCache.Cards
                .GetAsync(this.CardTypeName, cancellationToken: cancellationToken);

            if (value.Result == CardCacheResult.SingletonNotFound)
            {
                if (value.ValidationResult.Items.Any(i =>
                    i.Type == ValidationResultType.Error
                    && i.Key != CardValidationKeys.UnknownSingleton))
                {
                    this.Card = default;
                    value.GetValue(); // throw
                }

                return await this.CreateActionAsync(
                    null,
                    null,
                    cancellationToken);
            }

            this.Card = value.GetValue().Clone();
            this.CardID = this.Card.ID;
            this.CardTypeID = this.Card.TypeID;
            this.CardTypeName = this.Card.TypeName;

            return ValidationResult.Empty;
        }

        /// <summary>
        /// Возвращает строковое представление объекта, отображаемое в окне отладчика.
        /// </summary>
        /// <returns>Строковое представление объекта, отображаемое в окне отладчика.</returns>
        private string GetDebuggerDisplay()
        {
            return $"{nameof(this.CardID)} = {this.CardID:B}, " +
                $"{nameof(this.CardTypeID)} = {FormattingHelper.FormatNullable(this.CardTypeID, "B")}, " +
                $"{nameof(this.CardTypeName)} = {FormattingHelper.FormatNullable(this.CardTypeName)}, " +
                $"CardIsSet = {this.Card is not null}";
        }

        #endregion

    }
}
