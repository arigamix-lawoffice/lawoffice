using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ObjectLocking;
using Tessa.Platform.Redis;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionsLockStrategy" />
    public sealed class KrPermissionsLockStrategy :
        IKrPermissionsLockStrategy
    {
        #region Constants And Static Fields

        private static readonly Guid objectID = Guid.Empty;

        #endregion

        #region Fields

        private readonly IObjectTransactionLockingStrategy transactionLockingStrategy;

        private readonly IKrPermissionsObjectLockingStrategy krPermissionsObjectLockingStrategy;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrPermissionsLockStrategy"/>.
        /// </summary>
        /// <param name="transactionLockingStrategy"><inheritdoc cref="IObjectTransactionLockingStrategy" path="/summary"/></param>
        /// <param name="krPermissionsObjectLockingStrategy"><inheritdoc cref="IKrPermissionsObjectLockingStrategy" path="/summary"/></param>
        public KrPermissionsLockStrategy(
            [Dependency(nameof(KrPermissionsObjectLockingStrategy))] IObjectTransactionLockingStrategy transactionLockingStrategy,
            IKrPermissionsObjectLockingStrategy krPermissionsObjectLockingStrategy)
        {
            Check.ArgumentNotNull(transactionLockingStrategy, nameof(transactionLockingStrategy));
            Check.ArgumentNotNull(krPermissionsObjectLockingStrategy, nameof(krPermissionsObjectLockingStrategy));

            this.transactionLockingStrategy = transactionLockingStrategy;
            this.krPermissionsObjectLockingStrategy = krPermissionsObjectLockingStrategy;
        }

        #endregion

        #region IKrPermissionsLockStrategy Implementation

        /// <inheritdoc />
        public Task<ValidationResult> ObtainReaderLockAsync(
            CancellationToken cancellationToken = default) =>
            this.transactionLockingStrategy.ObtainReaderLockAsync(
                objectID,
                RedisLockKeys.KrPermissionsObjectKey,
                cancellationToken: cancellationToken);

        /// <inheritdoc />
        public Task<ValidationResult> ObtainWriterLockAsync(
            CancellationToken cancellationToken = default) =>
            this.transactionLockingStrategy.ObtainWriterLockAsync(
                objectID,
                RedisLockKeys.KrPermissionsObjectKey,
                cancellationToken: cancellationToken);

        /// <inheritdoc />
        public Task ClearLocksAsync(
            CancellationToken cancellationToken = default) =>
            this.krPermissionsObjectLockingStrategy.ClearLocksAsync(
                objectID,
                RedisLockKeys.KrPermissionsObjectKey,
                cancellationToken);

        #endregion
    }
}
