using System;
using System.Threading;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Контекст используемый при определении видимости тайла вторичного процесса работающего в режиме "Кнопка".
    /// </summary>
    public sealed class KrProcessButtonVisibilityEvaluatorContext : IKrProcessButtonVisibilityEvaluatorContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessButtonVisibilityEvaluatorContext"/>.
        /// </summary>
        /// <param name="validationResult">Результат валидации.</param>
        /// <param name="mainCardAccessStrategy">Стратегия загрузки основной карточки.</param>
        /// <param name="card">Карточка.</param>
        /// <param name="cardType">Тип карточки.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="krComponents">Включенные компоненты типового решения для текущей карточки.</param>
        /// <param name="state">Состояние карточки.</param>
        /// <param name="cardContext">Конекст расширения карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public KrProcessButtonVisibilityEvaluatorContext(
            IValidationResultBuilder validationResult,
            IMainCardAccessStrategy mainCardAccessStrategy,
            Card card,
            CardType cardType,
            Guid? docTypeID,
            KrComponents? krComponents,
            KrState? state,
            ICardExtensionContext cardContext,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));
            Check.ArgumentNotNull(mainCardAccessStrategy, nameof(mainCardAccessStrategy));

            this.ValidationResult = validationResult;
            this.MainCardAccessStrategy = mainCardAccessStrategy;
            this.Card = card;
            this.CardType = cardType;
            this.DocTypeID = docTypeID;
            this.KrComponents = krComponents;
            this.State = state;
            this.CardContext = cardContext;
            this.CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessButtonVisibilityEvaluatorContext"/>. Конструктор используется для инициализации контекста глобальных тайлов.
        /// </summary>
        /// <param name="validationResult">Результат валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public KrProcessButtonVisibilityEvaluatorContext(
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
            : this(
                  validationResult: validationResult,
                  mainCardAccessStrategy: NullMainCardAccessStrategy.Instance,
                  card: null,
                  cardType: null,
                  docTypeID: null,
                  krComponents: null,
                  state: null,
                  cardContext: null,
                  cancellationToken: cancellationToken)
        {
        }

        #endregion

        #region IKrProcessButtonVisibilityEvaluatorContext Members

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <inheritdoc />
        public Card Card { get; }

        /// <inheritdoc />
        public CardType CardType { get; }

        /// <inheritdoc />
        public Guid? DocTypeID { get; }

        /// <inheritdoc />
        public KrComponents? KrComponents { get; }

        /// <inheritdoc />
        public KrState? State { get; }

        /// <inheritdoc />
        public ICardExtensionContext CardContext { get; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}
