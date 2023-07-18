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
    /// Расширение для заданий в процессе сохранения карточки. Создаёт события типов "Перед созданием задания", "Перед завершением задания", "Создание задания", "Завершение задания" для обработки их в подсистеме мершрутов.
    /// </summary>
    public sealed class TaskStoreEventEmitter : CardStoreTaskExtension
    {
        #region Fields

        private readonly IKrEventManager eventManager;

        private readonly IKrTypesCache typesCache;

        private readonly IKrScope scope;

        private readonly ICardFileManager fileManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TaskStoreEventEmitter"/>.
        /// </summary>
        /// <param name="eventManager">Объект, предоставляющий методы для отправки событий маршрутов документов.</param>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        public TaskStoreEventEmitter(
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
        public override async Task StoreTaskBeforeRequest(
            ICardStoreTaskExtensionContext context)
        {
            var card = context.Request.TryGetCard();

            if (!context.ValidationResult.IsSuccessful()
                || card is null
                || !await KrComponentsHelper.HasBaseAsync(card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            var task = context.Task;

            if (task.State == CardRowState.Inserted)
            {
                await using var cardAccessStrategy = new ObviousMainCardAccessStrategy(
                   card,
                   this.fileManager,
                   context.ValidationResult);
                await this.RaiseEventAsync(
                    DefaultEventTypes.BeforeNewTask,
                    card,
                    cardAccessStrategy,
                    context);
                return;
            }

            if (task.OptionID.HasValue
                && task.Action == CardTaskAction.Complete)
            {
                await using var cardAccessStrategy = new ObviousMainCardAccessStrategy(
                   card,
                   this.fileManager,
                   context.ValidationResult);
                await this.RaiseEventAsync(
                    DefaultEventTypes.BeforeCompleteTask,
                    card,
                    cardAccessStrategy,
                    context);
            }
        }

        /// <inheritdoc/>
        public override async Task StoreTaskBeforeCommitTransaction(
            ICardStoreTaskExtensionContext context)
        {
            var card = context.Request.TryGetCard();

            if (!context.ValidationResult.IsSuccessful()
                || card is null
                || !await KrComponentsHelper.HasBaseAsync(card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            var task = context.Task;

            if (task.State == CardRowState.Inserted)
            {
                await using var cardAccessStrategy = new KrScopeMainCardAccessStrategy(
                    card.ID,
                    this.scope);
                await this.RaiseEventAsync(
                    DefaultEventTypes.NewTask,
                    card,
                    cardAccessStrategy,
                    context);

                return;
            }

            if (task.OptionID.HasValue
                && task.Action == CardTaskAction.Complete)
            {
                await using var cardAccessStrategy = new KrScopeMainCardAccessStrategy(
                    card.ID,
                    this.scope);
                await this.RaiseEventAsync(
                    DefaultEventTypes.CompleteTask,
                    card,
                    cardAccessStrategy,
                    context);
            }
        }

        #endregion

        #region Private Methods

        private async Task RaiseEventAsync(
            string eventType,
            Card card,
            IMainCardAccessStrategy cardAccessStrategy,
            ICardExtensionContext context)
        {
            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                context.DbScope,
                context.CancellationToken);

            await this.eventManager.RaiseAsync(
                eventType,
                card.ID,
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