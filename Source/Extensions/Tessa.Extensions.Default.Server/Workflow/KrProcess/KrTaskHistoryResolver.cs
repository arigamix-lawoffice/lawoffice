using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Объект, позволяющий определить группу в истории заданий.
    /// </summary>
    public sealed class KrTaskHistoryResolver : IKrTaskHistoryResolver
    {
        #region Fields

        private readonly IMainCardAccessStrategy cardAccessStrategy;

        private readonly object placeholderContext;

        private readonly IValidationResultBuilder validationResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrTaskHistoryResolver"/>.
        /// </summary>
        /// <param name="cardAccessStrategy">Стратегия доступа к карточке.</param>
        /// <param name="placeholderContext">Объект внешнего контекста, передаваемый в плейсхолдеры при формировании названия экземпляра группы. Может иметь значение <see langword="null"/>.</param>
        /// <param name="validationResult">Объект, в который записывается результат выполнения по умолчанию.</param>
        /// <param name="taskHistoryManager">Объект, управляющий созданием групп в истории заданий на основании типов групп, определяемых по справочнику.</param>
        public KrTaskHistoryResolver(
            IMainCardAccessStrategy cardAccessStrategy,
            object placeholderContext,
            IValidationResultBuilder validationResult,
            ICardTaskHistoryManager taskHistoryManager)
        {
            this.cardAccessStrategy = cardAccessStrategy ?? throw new ArgumentNullException(nameof(cardAccessStrategy));
            this.TaskHistoryManager = taskHistoryManager ?? throw new ArgumentNullException(nameof(taskHistoryManager));
            this.validationResult = validationResult ?? throw new ArgumentNullException(nameof(validationResult));
            this.placeholderContext = placeholderContext;
        }

        #endregion

        #region IKrTaskHistoryResolver Members

        /// <inheritdoc />
        public ICardTaskHistoryManager TaskHistoryManager { get; }

        /// <inheritdoc />
        public async ValueTask<CardTaskHistoryGroup> ResolveTaskHistoryGroupAsync(
            Guid groupTypeID,
            Guid? parentGroupTypeID = null,
            bool newIteration = false,
            IValidationResultBuilder overrideValidationResult = null,
            CancellationToken cancellationToken = default)
        {
            var card = await this.cardAccessStrategy.GetCardAsync(overrideValidationResult, cancellationToken: cancellationToken);

            if (card is null)
            {
                return null;
            }

            await this.cardAccessStrategy.EnsureTaskHistoryLoadedAsync(overrideValidationResult, cancellationToken);

            return await this.TaskHistoryManager.ResolveGroupAsync(
                card,
                card.TaskHistoryGroups,
                card.TaskHistoryGroups,
                overrideValidationResult ?? this.validationResult,
                groupTypeID,
                parentGroupTypeID,
                newIteration,
                this.placeholderContext,
                cardHasNoSections: true,
                cancellationToken: cancellationToken);
        }

        #endregion
    }
}