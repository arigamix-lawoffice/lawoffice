using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ObjectLocking;
using Tessa.Platform.Redis;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Стратегия блокировки на чтение и запись правил доступа.
    /// </summary>
    public sealed class KrPermissionsObjectLockingStrategy :
        ObjectLockingStrategy,
        IKrPermissionsObjectLockingStrategy
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="serverSettings">Настройки TESSA на сервере, которые выносятся в конфигурационный файл.</param>
        /// <param name="redisConnectionProvider">Объект, предоставляющий доступ к соединению Redis.</param>
        /// <param name="options">Настройки стратегии блокировок.</param>
        public KrPermissionsObjectLockingStrategy(
            ITessaServerSettings serverSettings,
            IRedisConnectionProvider redisConnectionProvider,
            ObjectLockingStrategyOptions options)
            : base(
                  serverSettings,
                  redisConnectionProvider,
                  options,
                  typeof(KrPermissionsObjectLockingStrategy)
                    .Assembly
                    .GetStringFromEmbeddedResource(
                        "Tessa.Extensions.Default.Server.Resources.RedisKrPermissionsLocks.lua"))
        {
        }

        #endregion

        #region IKrPermissionsObjectLockingStrategy Members

        /// <inheritdoc/>
        public async Task ClearLocksAsync(
            Guid objectID,
            string objectPrefix,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNullOrWhiteSpace(objectPrefix, nameof(objectPrefix));

            var redisConnection =
                await this.RedisConnectionProvider.GetOpenedConnectionAsync(cancellationToken);
            var rdb = redisConnection.GetDatabase();
            var key = this.GetRedisKey(objectPrefix);

            await rdb.HashDeleteAsync(key, objectID.ToString());
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override Task<ValidationResult> EscalateReaderLockCoreAsync(
            Guid objectID,
            string objectPrefix,
            int attemptCount,
            int retryTimeout,
            CancellationToken cancellationToken = default)
        {
            // Эскалация блокировки не поддерживается.
            // Всегда возвращается ошибка ObjectLockingStrategyReaderLock для понятного сообщения при взятии блокировки на запись с помощью ObjectTransactionLockingStrategy.

            var validationResult = new ValidationResultBuilder(1);
            var key = this.GetRedisKey(objectPrefix);

            ValidationSequence
                .Begin(validationResult)
                .SetObjectName(this)
                .Error(ValidationKeys.ObjectLockingStrategyReaderLock,
                    Commands.ObtainWriteLock,
                    key,
                    objectID)
                .End();
            return Task.FromResult(validationResult.Build());
        }

        #endregion
    }
}
