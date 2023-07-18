using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Представляет стратегию доступа к карточке. Является обёрткой над <see cref="IKrScope"/>.
    /// </summary>
    public sealed class KrScopeMainCardAccessStrategy: IMainCardAccessStrategy
    {
        #region Fields

        private readonly Guid cardID;

        private readonly IKrScope scope;

        private readonly IValidationResultBuilder result;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrScopeMainCardAccessStrategy"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="result">Результат валидации.</param>
        public KrScopeMainCardAccessStrategy(
            Guid cardID,
            IKrScope scope,
            IValidationResultBuilder result = null)
        {
            this.cardID = cardID;
            this.scope = scope;
            this.result = result;
        }

        #endregion

        #region IMainCardAccessStrategy members

        /// <inheritdoc />
        public bool WasUsed { get; private set; }

        /// <inheritdoc />
        public bool WasFileContainerUsed { get; private set; }

        /// <inheritdoc />
        public ValueTask<Card> GetCardAsync(
            IValidationResultBuilder validationResult = null,
            bool withoutTransaction = false,
            CancellationToken cancellationToken = default)
        {
            if (!this.WasUsed)
            {
                this.WasUsed = true;
            }
            return this.scope.GetMainCardAsync(this.cardID, validationResult ?? this.result, withoutTransaction: true, cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask<ICardFileContainer> GetFileContainerAsync(
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            if (!this.WasUsed)
            {
                this.WasUsed = true;
            }

            if (!this.WasFileContainerUsed)
            {
                this.WasFileContainerUsed = true;
            }

            return await this.scope.GetMainCardFileContainerAsync(this.cardID, validationResult ?? this.result, cancellationToken);
        }

        /// <inheritdoc />
        public Task EnsureTaskHistoryLoadedAsync(
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            if (!this.WasUsed)
            {
                this.WasUsed = true;
            }
            return this.scope.EnsureMainCardHasTaskHistoryAsync(this.cardID, validationResult ?? this.result, cancellationToken);
        }

        #endregion

        #region IAsyncDisposable members

        /// <doc path='info[@type="IAsyncDisposable" and @item="DisposeAsync"]'/>
        public ValueTask DisposeAsync() => new ValueTask();

        #endregion
    }
}