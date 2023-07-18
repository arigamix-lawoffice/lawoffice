using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Контекст области выполнения.
    /// </summary>
    /// <remarks>Чтобы два объекта были равны, они должны иметь одинаковое название области выполнения.</remarks>
    [DebuggerDisplay("{" + nameof(ToDebugString) + "(),nq}")]
    public sealed class ScopeContext :
        IAsyncDisposable,
        IEquatable<ScopeContext>
    {
        #region Fields

        private readonly AsyncLock innerAsyncLock = new();

        private readonly AsyncLock asyncLock = new();

        private bool isDisposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ScopeContext"/>.
        /// </summary>
        /// <param name="name">Название области выполнения.</param>
        /// <param name="instances">Число экземпляров области выполнения.</param>
        public ScopeContext(
            string name,
            int instances)
        {
            Check.ArgumentNotNullOrEmpty(name, nameof(name));

            if (instances < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(instances), instances, "The value is less than zero.");
            }

            this.Name = name;
            this.Instances = instances;

            this.Info = new ConcurrentDictionary<string, object>(StringComparer.Ordinal);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает название области выполнения.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает число экземпляров области выполнения.
        /// </summary>
        public int Instances { get; private set; }

        /// <summary>
        /// Возвращает значение, показывающее, что область выполнения инициализирована.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Возвращает дополнительную информацию. Значение не равно <see langword="null"/>.
        /// </summary>
        public ConcurrentDictionary<string, object> Info { get; }

        /// <summary>
        /// Класс, обеспечивающий блокировку вида <c>lock(resource) { ... }</c>.
        /// </summary>
        public AsyncLock AsyncLock => this.asyncLock;

        #endregion

        #region Public Methods

        /// <summary>
        /// Инициализирует область выполнения.
        /// </summary>
        /// <param name="initializationFuncAsync">Метод, выполняющий инициализацию области выполнения.</param>
        /// <param name="needInitializationFuncAsync">Метод, выполняемый, если инициализация области выполнения не требуется.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task InitializeScopeAsync(
            Func<CancellationToken, Task> initializationFuncAsync,
            Func<CancellationToken, Task> needInitializationFuncAsync,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(initializationFuncAsync, nameof(initializationFuncAsync));
            Check.ArgumentNotNull(needInitializationFuncAsync, nameof(needInitializationFuncAsync));
            this.CheckDisposed();

            if (this.IsInitialized)
            {
                await needInitializationFuncAsync(cancellationToken);
                return;
            }

            using (await this.innerAsyncLock.EnterAsync(cancellationToken))
            {
                if (this.IsInitialized)
                {
                    await needInitializationFuncAsync(cancellationToken);
                    return;
                }

                await initializationFuncAsync(cancellationToken);

                this.IsInitialized = true;
            }
        }

        /// <summary>
        /// Уменьшает число экземпляров области выполнения на 1.
        /// </summary>
        /// <param name="releaseFuncAsync">Метод, выполняющий финализацию области выполнения.</param>
        /// <param name="needReleaseFuncAsync">Метод, выполняемый, если финализация области выполнения не требуется. Выполняется только, если остались активные экземпляры области выполнения.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task ReleaseScopeAsync(
            Func<CancellationToken, Task> releaseFuncAsync,
            Func<CancellationToken, Task> needReleaseFuncAsync,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(releaseFuncAsync, nameof(releaseFuncAsync));
            this.CheckDisposed();

            if (!this.IsInitialized
                || this.Instances <= 0)
            {
                return;
            }

            using (await this.innerAsyncLock.EnterAsync(cancellationToken))
            {
                if (!this.IsInitialized
                    || this.Instances <= 0)
                {
                    return;
                }

                if (--this.Instances > 0)
                {
                    await needReleaseFuncAsync(cancellationToken);
                    return;
                }

                await releaseFuncAsync(cancellationToken);
            }

            return;
        }

        #endregion

        #region IAsyncDisposable Members

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Область имеет активные экземпляры.</exception>
        public async ValueTask DisposeAsync()
        {
            if (this.isDisposed)
            {
                return;
            }

            using (await this.innerAsyncLock.EnterAsync())
            {
                if (this.IsInitialized && this.Instances > 0)
                {
                    throw new InvalidOperationException($"The scope has active instances.");
                }
            }

            this.innerAsyncLock.Dispose();
            this.asyncLock.Dispose();

            this.isDisposed = true;
        }

        #endregion

        #region IEquatable<T> Members

        /// <inheritdoc/>
        public bool Equals(ScopeContext other)
        {
            if (other is null)
            {
                return false;
            }

            return string.Equals(this.Name, other.Name, StringComparison.Ordinal);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            this.Equals(obj as ScopeContext);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            this.Name.GetHashCode(StringComparison.Ordinal);

        #endregion

        #region Private Methods

        private string ToDebugString()
        {
            return $"{DebugHelper.GetTypeName(this)}: " +
                $"{nameof(this.Name)} = \"{this.Name}\", " +
                $"{nameof(this.IsInitialized)} = {this.IsInitialized}, " +
                $"{nameof(this.Instances)} = {this.Instances}";
        }

        private void CheckDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(ScopeContext));
            }
        }

        #endregion
    }
}
