using System;
using System.Threading;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Предоставляет контекст содержащий информацию о запускаемом вторичном процессе.
    /// </summary>
    public sealed class KrSecondaryProcessEvaluatorContext : IKrSecondaryProcessEvaluatorContext
    {
        #region Constructors

        public KrSecondaryProcessEvaluatorContext(
            IKrSecondaryProcess secondaryProcess,
            IValidationResultBuilder validationResult,
            IMainCardAccessStrategy mainCardAccessStrategy,
            Guid? cardID,
            CardType cardType,
            Guid? docTypeID,
            KrComponents? krComponents,
            KrState? state,
            Card contextualSatellite,
            ICardExtensionContext cardContext,
            CancellationToken cancellationToken)
        {
            this.SecondaryProcess = secondaryProcess;
            this.ValidationResult = validationResult;
            this.MainCardAccessStrategy = mainCardAccessStrategy;
            this.CardID = cardID;
            this.CardType = cardType;
            this.DocTypeID = docTypeID;
            this.KrComponents = krComponents;
            this.State = state;
            this.ContextualSatellite = contextualSatellite;
            this.CardContext = cardContext;
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IKrSecondaryProcessEvaluatorContext Members

        /// <inheritdoc />
        public IKrSecondaryProcess SecondaryProcess { get; }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <inheritdoc />
        public Guid? CardID { get; }

        /// <inheritdoc />
        public CardType CardType { get; }

        /// <inheritdoc />
        public Guid? DocTypeID { get; }

        /// <inheritdoc />
        public KrComponents? KrComponents { get; }

        /// <inheritdoc />
        public KrState? State { get; }

        /// <inheritdoc />
        public Card ContextualSatellite { get; }

        /// <inheritdoc />
        public ICardExtensionContext CardContext { get; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}