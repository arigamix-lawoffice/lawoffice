#nullable enable
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ObjectLocking;
using Tessa.Platform.Redis;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    /// <inheritdoc cref="IDeskiMobileLockingStrategy" />
    public sealed class DeskiMobileLockingStrategy :
        IDeskiMobileLockingStrategy
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="lockingStrategy">Стратегия по управлению блокировками любых объектов.</param>
        public DeskiMobileLockingStrategy(IObjectLockingStrategy lockingStrategy) =>
            this.lockingStrategy = NotNullOrThrow(lockingStrategy);

        #endregion

        #region Fields

        private readonly IObjectLockingStrategy lockingStrategy;

        #endregion

        #region IDeskiMobileLockingStrategy Members

        /// <inheritdoc />
        public Task<ValidationResult> ObtainWriterLockAsync(Guid operationID, CancellationToken cancellationToken = default) =>
            this.lockingStrategy.ObtainWriterLockAsync(operationID, RedisLockKeys.DeskiMobileKey, cancellationToken: cancellationToken);

        /// <inheritdoc />
        public Task<ValidationResult> ReleaseWriterLockAsync(Guid operationID) =>
            this.lockingStrategy.ReleaseWriterLockAsync(operationID, RedisLockKeys.DeskiMobileKey);

        /// <inheritdoc />
        public Task<ValidationResult> ObtainReaderLockAsync(Guid operationID, CancellationToken cancellationToken = default) =>
            this.lockingStrategy.ObtainReaderLockAsync(operationID, RedisLockKeys.DeskiMobileKey, cancellationToken: cancellationToken);

        /// <inheritdoc />
        public async Task<(bool Success, ValidationResult Result)> TryObtainReaderLockNoWaitAsync(Guid operationID, CancellationToken cancellationToken = default)
        {
            var result = await this.lockingStrategy.ObtainReaderLockAsync(operationID, RedisLockKeys.DeskiMobileKey, attemptCount: 1, cancellationToken: cancellationToken);
            if (result.Items.Any(static x => x.Key == ValidationKeys.ObjectLockingStrategyWriterLock))
            {
                return (false, ValidationResult.Empty);
            }

            return (result.IsSuccessful, result);
        }

        /// <inheritdoc />
        public Task<ValidationResult> ReleaseReaderLockAsync(Guid operationID) =>
            this.lockingStrategy.ReleaseReaderLockAsync(operationID, RedisLockKeys.DeskiMobileKey);

        /// <inheritdoc />
        public Task<ValidationResult> EscalateReaderLockAsync(Guid operationID, CancellationToken cancellationToken = default) =>
            this.lockingStrategy.EscalateReaderLockAsync(operationID, RedisLockKeys.DeskiMobileKey, cancellationToken: cancellationToken);

        #endregion
    }
}
