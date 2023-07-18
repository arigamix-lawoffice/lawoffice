using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Actions
{
    /// <summary>
    /// Расширение на сохранение карточки. Создаёт события типов "Перед сохранением карточки" и "Сохранение карточки" для обработки их в подсистеме мершрутов.
    /// </summary>
    public sealed class CardStoreEventEmitter : CardStoreExtension
    {
        #region Fields

        private readonly IKrEventManager eventManager;

        private readonly IKrTypesCache typesCache;

        private readonly IKrScope scope;

        private readonly ICardFileManager fileManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardStoreEventEmitter"/>.
        /// </summary>
        /// <param name="eventManager">Объект, предоставляющий методы для отправки событий маршрутов документов.</param>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        public CardStoreEventEmitter(
            IKrEventManager eventManager,
            IKrTypesCache typesCache,
            IKrScope scope,
            ICardFileManager fileManager)
        {
            this.eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.scope = scope ?? throw new ArgumentNullException(nameof(scope));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(
            ICardStoreExtensionContext context)
        {
            var card = context.Request.TryGetCard();

            if (!context.ValidationResult.IsSuccessful()
                || card is null
                || !await KrComponentsHelper.HasBaseAsync(card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                context.DbScope,
                context.CancellationToken);
            await using var cardAccessStrategy = new ObviousMainCardAccessStrategy(
                card,
                this.fileManager,
                context.ValidationResult);

            await this.eventManager.RaiseAsync(
                DefaultEventTypes.BeforeStoreCard,
                card.ID,
                context.CardType,
                docTypeID,
                cardAccessStrategy,
                context,
                context.ValidationResult,
                cancellationToken: context.CancellationToken);
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(
            ICardStoreExtensionContext context)
        {
            var card = context.Request.TryGetCard();

            if (!context.ValidationResult.IsSuccessful()
                || card is null
                || !await KrComponentsHelper.HasBaseAsync(card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                context.DbScope,
                context.CancellationToken);
            var cardID = card.ID;
            await using var cardAccessStrategy = new KrScopeMainCardAccessStrategy(
                cardID,
                this.scope);

            await this.eventManager.RaiseAsync(
                DefaultEventTypes.StoreCard,
                cardID,
                context.CardType,
                docTypeID,
                cardAccessStrategy,
                context,
                context.ValidationResult,
                cancellationToken: context.CancellationToken);
        }

        #endregion
    }
}