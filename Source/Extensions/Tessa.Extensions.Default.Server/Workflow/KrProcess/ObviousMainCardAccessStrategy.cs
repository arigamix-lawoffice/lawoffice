using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Представляет стратегию доступа к карточке. Используется явно заданная карточка или карточка возвращаемая функцией.
    /// </summary>
    public sealed class ObviousMainCardAccessStrategy : IMainCardAccessStrategy
    {
        #region Fields

        private Card card;

        private readonly Func<IValidationResultBuilder, CancellationToken, ValueTask<Card>> cardGetterAsync;

        private readonly ICardFileManager fileManager;

        private readonly IValidationResultBuilder validationResult;

        private ICardFileContainer cardFileContainer;

        private bool disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ObviousMainCardAccessStrategy"/>.
        /// </summary>
        /// <param name="card">Карточка, которой выполняется инициализация.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="validationResult">Результат валидации.</param>
        public ObviousMainCardAccessStrategy(
            Card card,
            ICardFileManager fileManager,
            IValidationResultBuilder validationResult)
        {
            Check.ArgumentNotNull(fileManager, nameof(fileManager));
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            this.card = card;
            this.fileManager = fileManager;
            this.validationResult = validationResult;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ObviousMainCardAccessStrategy"/>.
        /// </summary>
        /// <param name="cardGetterAsync">Функция возвращающая карточку.</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="validationResult">Результат валидации.</param>
        public ObviousMainCardAccessStrategy(
            Func<IValidationResultBuilder, CancellationToken, ValueTask<Card>> cardGetterAsync,
            ICardFileManager fileManager,
            IValidationResultBuilder validationResult)
        {
            Check.ArgumentNotNull(cardGetterAsync, nameof(cardGetterAsync));
            Check.ArgumentNotNull(fileManager, nameof(fileManager));
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            this.cardGetterAsync = cardGetterAsync;
            this.fileManager = fileManager;
            this.validationResult = validationResult;
        }

        #endregion

        #region IMainCardAccessStrategy members

        /// <inheritdoc />
        public bool WasUsed { get; private set; }

        /// <inheritdoc />
        public bool WasFileContainerUsed => this.cardFileContainer != null;

        /// <inheritdoc />
        public async ValueTask<Card> GetCardAsync(
            IValidationResultBuilder validationResult = null,
            bool withoutTransaction = false,
            CancellationToken cancellationToken = default)
        {
            if (!this.WasUsed)
            {
                this.WasUsed = true;
            }

            if (this.card is null
                && this.cardGetterAsync is not null)
            {
                this.card = await this.cardGetterAsync(validationResult ?? this.validationResult, cancellationToken);
            }

            return this.card;
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
                var card = await this.GetCardAsync(validationResult, cancellationToken: cancellationToken);
                if (card != null)
                {
                    this.cardFileContainer = await this.fileManager.CreateContainerAsync(card, cancellationToken: cancellationToken);
                    (validationResult ?? this.validationResult).Add(this.cardFileContainer.CreationResult);
                    if (this.cardFileContainer.CreationResult.HasErrors)
                    {
                        await this.cardFileContainer.DisposeAsync();
                        this.cardFileContainer = default;
                    }
                }
            }

            return this.cardFileContainer;
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

            return Task.CompletedTask;
        }

        #endregion

        #region IDisposable members

        /// <doc path='info[@type="IAsyncDisposable" and @item="DisposeAsync"]'/>
        public async ValueTask DisposeAsync()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;

            if (this.WasFileContainerUsed)
            {
                await this.cardFileContainer.DisposeAsync();
            }
        }

        #endregion
    }
}
