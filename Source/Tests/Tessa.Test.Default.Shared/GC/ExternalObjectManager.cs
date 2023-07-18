using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using Tessa.Platform;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.GC.Handlers;
using Unity;

namespace Tessa.Test.Default.Shared.GC
{
    /// <inheritdoc cref="IExternalObjectManager"/>
    public sealed class ExternalObjectManager :
        IExternalObjectManager,
        IDisposable
    {
        #region Fields

        private readonly object initializeLock = new object();

        private bool isInitialized;

        private bool isDisposed;

        private readonly ILiteDatabase liteDatabase;

        private readonly IExternalObjectHandlerRegistry externalObjectHandlerRegistry;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExternalObjectManager"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения к локальной базе данных.</param>
        /// <param name="externalObjectHandlerRegistry"> Реестр обработчиков внешних объектов <see cref="IExternalObjectHandler"/>.</param>
        /// <param name="unityDisposableContainer">Контейнер, содержащий объекты <see cref="IDisposable"/>, которые будут освобождены при закрытии контейнеров <see cref="IUnityContainer"/>.</param>
        public ExternalObjectManager(
            ConnectionString connectionString,
            IExternalObjectHandlerRegistry externalObjectHandlerRegistry,
            [OptionalDependency] IUnityDisposableContainer unityDisposableContainer = null)
        {
            // Параметр connectionString будет проверен в конструкторе.
            this.liteDatabase = new LiteDatabase(connectionString);
            this.externalObjectHandlerRegistry = externalObjectHandlerRegistry ?? throw new ArgumentNullException(nameof(externalObjectHandlerRegistry));

            unityDisposableContainer?.Register(this);
        }

        #endregion

        #region IExternalObjectManager Members

        /// <inheritdoc/>
        public async ValueTask CollectAsync(
            TimeSpan keepAliveInterval,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            if (keepAliveInterval < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(keepAliveInterval),
                    keepAliveInterval,
                    "The value must be non-negative.");
            }

            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            this.Initialize();

            var query = this.GetCollection().Query();

            if (keepAliveInterval > TimeSpan.Zero)
            {
                query.Where(
                    $"$.{nameof(ExternalObjectInfo.Created)} < @0",
                    DateTime.UtcNow - keepAliveInterval);
            }

            foreach (var externalObjectInfo in query.ToArray())
            {
                var context = new ExternalObjectHandlerContext(
                    externalObjectInfo,
                    new ValidationResultBuilder())
                {
                    CancellationToken = cancellationToken,
                };

                await this.CollectCoreAsync(context);
                validationResult.Add(context.ValidationResult);
            }
        }

        /// <inheritdoc/>
        public async ValueTask CollectAsync(
            int fixtureID,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            this.Initialize();

            var query = this.GetCollection()
                .Query()
                .Where(i => i.FixtureID == fixtureID);

            foreach (var externalObjectInfo in query.ToArray())
            {
                var context = new ExternalObjectHandlerContext(
                    externalObjectInfo,
                    new ValidationResultBuilder())
                {
                    CancellationToken = cancellationToken,
                };

                await this.CollectCoreAsync(context);
                validationResult.Add(context.ValidationResult);
            }
        }

        /// <inheritdoc/>
        public async ValueTask CollectAsync(
            Guid id,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            this.Initialize();

            var externalObjectInfo = this.GetCollection()
                .FindById(id);

            if (externalObjectInfo is null)
            {
                // Не найдена информация об объекте.
                return;
            }

            var context = new ExternalObjectHandlerContext(
                externalObjectInfo,
                new ValidationResultBuilder())
            {
                CancellationToken = cancellationToken,
            };

            await this.CollectCoreAsync(context);
            validationResult.Add(context.ValidationResult);
        }

        /// <inheritdoc/>
        public async ValueTask CollectAsync(
            ExternalObjectInfo externalObjectInfo,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(externalObjectInfo, nameof(externalObjectInfo));
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            this.Initialize();

            var context = new ExternalObjectHandlerContext(
                externalObjectInfo,
                new ValidationResultBuilder())
            {
                CancellationToken = cancellationToken,
            };

            await this.CollectCoreAsync(context);
            validationResult.Add(context.ValidationResult);
        }

        /// <inheritdoc/>
        public void KeepAlive(Guid id)
        {
            this.Initialize();

            this.GetCollection()
                .Delete(id);
        }

        /// <inheritdoc/>
        public void RegisterForFinalize(ExternalObjectInfo obj)
        {
            Check.ArgumentNotNull(obj, nameof(obj));

            this.Initialize();

            this.GetCollection()
                .Insert(obj);
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc/>
        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            this.liteDatabase.Dispose();

            this.isDisposed = true;
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            if (this.isInitialized)
            {
                return;
            }

            lock (this.initializeLock)
            {
                if (this.isInitialized)
                {
                    return;
                }

                this.liteDatabase.UtcDate = true;

                var collection = this.GetCollection();
                collection.EnsureIndex(static x => x.ID, true);
                collection.EnsureIndex(static x => x.Created);
                collection.EnsureIndex(static x => x.FixtureID);

                this.isInitialized = true;
            }
        }

        private ILiteCollection<ExternalObjectInfo> GetCollection() =>
            this.liteDatabase.GetCollection<ExternalObjectInfo>();

        private async ValueTask CollectCoreAsync(
            IExternalObjectHandlerContext context)
        {
            if (!this.externalObjectHandlerRegistry.TryGet(context.ObjectInfo.TypeID, out var externalObjectHandler))
            {
                // Не найден обработчик.

                context.ValidationResult.AddWarning(
                    this,
                    $"Not found handler for processing item: \"{context.ObjectInfo}\".");

                return;
            }

            try
            {
                await externalObjectHandler.HandleAsync(context);

                if (!context.Cancel
                    && context.ValidationResult.IsSuccessful())
                {
                    // Информацию об объекте можно удалять только, если он успешно обработан.
                    this.GetCollection()
                        .Delete(context.ObjectInfo.ID);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                context.ValidationResult.AddException(
                    this,
                    e,
                    false,
                    $"An unhandled exception was thrown while processing item: \"{context.ObjectInfo}\".");
            }
        }

        #endregion
    }
}
