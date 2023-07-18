using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Actions
{
    /// <summary>
    /// Расширение на создание карточки. Создаёт событие типа "Создание новой карточки" для обработки его в подсистеме мершрутов.
    /// </summary>
    public sealed class CardNewEventEmitter : CardNewExtension
    {
        #region Fields

        private readonly IKrEventManager eventManager;

        private readonly IKrTypesCache typesCache;

        private readonly ICardFileManager fileManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardNewEventEmitter"/>.
        /// </summary>
        /// <param name="eventManager">Объект, предоставляющий методы для отправки событий маршрутов документов.</param>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        public CardNewEventEmitter(
            IKrEventManager eventManager,
            IKrTypesCache typesCache,
            ICardFileManager fileManager)
        {
            this.eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardNewExtensionContext context)
        {
            var card = context.Response.TryGetCard();

            if (!context.ValidationResult.IsSuccessful()
                || card is null
                || !await KrComponentsHelper.HasBaseAsync(card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            await using var cardAccessStrategy = new ObviousMainCardAccessStrategy(
                card,
                this.fileManager,
                context.ValidationResult);
            var cardType = context.CardType ?? await CardComponentHelper.TryGetCardTypeAsync(
                card.TypeID,
                context.CardMetadata,
                context.CancellationToken);
            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                context.DbScope,
                context.CancellationToken);

            await this.eventManager.RaiseAsync(
                DefaultEventTypes.NewCard,
                card.ID,
                cardType,
                docTypeID,
                cardAccessStrategy,
                context,
                context.ValidationResult,
                cancellationToken: context.CancellationToken);
        }

        #endregion
    }
}