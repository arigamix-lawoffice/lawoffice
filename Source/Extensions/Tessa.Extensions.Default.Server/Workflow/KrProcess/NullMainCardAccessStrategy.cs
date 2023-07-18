using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Представляет стратегию доступа к карточке. Методы возвращающие значения возвращают значение - null.
    /// </summary>
    public sealed class NullMainCardAccessStrategy: IMainCardAccessStrategy
    {
        #region Fields

        public static readonly NullMainCardAccessStrategy Instance = new NullMainCardAccessStrategy();

        #endregion

        #region Constructor

        private NullMainCardAccessStrategy()
        {

        }

        #endregion

        #region IMainCardAccessStrategy members

        /// <inheritdoc />
        public bool WasUsed => false;

        /// <inheritdoc />
        public bool WasFileContainerUsed => false;

        /// <inheritdoc />
        public ValueTask<Card> GetCardAsync(
            IValidationResultBuilder validationResult = null,
            bool withoutTransaction = false,
            CancellationToken cancellationToken = default)
        {
            return new ValueTask<Card>(default(Card));
        }

        /// <inheritdoc />
        public ValueTask<ICardFileContainer> GetFileContainerAsync(
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            return new ValueTask<ICardFileContainer>(default(ICardFileContainer));
        }

        /// <inheritdoc />
        public Task EnsureTaskHistoryLoadedAsync(
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IAsyncDisposable members

        /// <doc path='info[@type="IAsyncDisposable" and @item="DisposeAsync"]'/>
        public ValueTask DisposeAsync() => new ValueTask();

        #endregion
    }
}