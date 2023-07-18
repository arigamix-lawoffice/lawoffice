using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using Tessa.Platform;
using Tessa.Platform.Redis;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Test.Default.Shared.GC.Handlers
{
    /// <summary>
    /// Обработчик внешнего ресурса типа "Redis" <see cref="ExternalObjectTypes.Redis"/>.
    /// </summary>
    public sealed class RedisExternalObjectHandler :
        ExternalObjectHandlerBase
    {
        #region Constants And Static Fields

        /// <summary>
        /// Ключ, по которому в <see cref="ExternalObjectInfo.Info"/> содержится шаблон поиска удаляемых ключей. Подробнее см. в <see href="https://redis.io/commands/keys">Redis KEYS</see>. Тип значения: <see cref="string"/>.
        /// </summary>
        public const string PatternKey = "Pattern";

        private static readonly Guid ObjectTypeID = ExternalObjectTypes.Redis;

        #endregion

        #region Fields

        private readonly IRedisConnectionProvider redisConnectionProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RedisExternalObjectHandler"/>.
        /// </summary>
        /// <param name="redisConnectionProvider">Объект, предоставляющий доступ к соединению Redis.</param>
        public RedisExternalObjectHandler(
            [OptionalDependency] IRedisConnectionProvider redisConnectionProvider = null)
            : base(ObjectTypeID)
        {
            this.redisConnectionProvider = redisConnectionProvider;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask HandleAsync(IExternalObjectHandlerContext context)
        {
            // Механизм сбора мусора может работать при частично инициализированном контейнере.
            if (this.redisConnectionProvider is null)
            {
                context.ValidationResult.AddWarning(
                    this,
                    $"Type \"{typeof(IRedisConnectionProvider).FullName}\" not registered. The handler {nameof(RedisExternalObjectHandler)} skipped.");
                context.Cancel = true;

                return;
            }

            var objInfo = context.ObjectInfo.Info;
            var pattern = objInfo.Get<string>(PatternKey);

            var connection = await this.redisConnectionProvider.GetOpenedConnectionAsync(context.CancellationToken);

            await this.DeleteKeysAsync(
                connection,
                pattern,
                context.ValidationResult);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Создаёт объект обрабатываемого типа.
        /// </summary>
        /// <param name="pattern">Шаблон поиска удаляемых ключей.</param>
        /// <param name="fixtureID">Идентификатор владельца объекта.
        /// Обычно это значение, возвращаемое методом <see cref="object.GetHashCode()"/>,
        /// где <see cref="object"/> - класс, содержащий текущий набор тестов,
        /// в котором был создан внешний ресурс (test fixture).</param>
        /// <returns>Созданный объект.</returns>
        public static ExternalObjectInfo CreateObjectInfo(
            string pattern,
            int fixtureID)
        {
            Check.ArgumentNotNull(pattern, nameof(pattern));

            var obj = new ExternalObjectInfo()
            {
                ID = Guid.NewGuid(),
                TypeID = ObjectTypeID,
                Created = DateTime.UtcNow,
                FixtureID = fixtureID,
            };

            obj.Info[PatternKey] = pattern;

            return obj;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Удаляет ключи соответствующие указанном шаблону.
        /// </summary>
        /// <param name="connectionMultiplexer">Объект, предоставляющий доступ соединению Redis.</param>
        /// <param name="pattern">Шаблон поиска удаляемых ключей. Подробнее см. в <see href="https://redis.io/commands/keys">Redis KEYS</see>.</param>
        /// <param name="validationResult">Объект, выполняющий построение результатов валидации.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task DeleteKeysAsync(
            IConnectionMultiplexer connectionMultiplexer,
            RedisValue pattern,
            IValidationResultBuilder validationResult)
        {
            var endPoints = connectionMultiplexer.GetEndPoints();
            var db = connectionMultiplexer.GetDatabase();

            foreach (var endPoint in endPoints)
            {
                try
                {
                    var server = connectionMultiplexer.GetServer(endPoint);

                    await foreach (var key in server.KeysAsync(pattern: pattern))
                    {
                        await db.KeyDeleteAsync(key);
                    }
                }
                catch (Exception ex)
                {
                    validationResult.AddException(this, ex);
                }
            }
        }

        #endregion
    }
}
