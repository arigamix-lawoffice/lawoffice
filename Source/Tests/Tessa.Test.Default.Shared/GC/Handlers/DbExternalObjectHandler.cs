using System;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.GC.Handlers
{
    /// <summary>
    /// Обработчик внешнего ресурса типа "база данных" <see cref="ExternalObjectTypes.Database"/>.
    /// </summary>
    public sealed class DbExternalObjectHandler :
        ExternalObjectHandlerBase
    {
        #region Constants And Static Fields

        /// <summary>
        /// Ключ, по которому в <see cref="ExternalObjectInfo.Info"/> содержится строка подключения к базе данных. Тип значения: <see cref="string"/>.
        /// </summary>
        public const string ConnectionStringKey = "ConnectionString";

        /// <summary>
        /// Ключ, по которому в <see cref="ExternalObjectInfo.Info"/> содержится провайдер для подключения к базе данных. Тип значения: <see cref="string"/>.
        /// </summary>
        public const string DataProviderKey = "DataProvider";

        private static readonly Guid ObjectTypeID = ExternalObjectTypes.Database;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DbExternalObjectHandler"/>.
        /// </summary>
        public DbExternalObjectHandler()
            : base(ObjectTypeID)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask HandleAsync(IExternalObjectHandlerContext context)
        {
            var objInfo = context.ObjectInfo.Info;
            var connectionString = objInfo.Get<string>(ConnectionStringKey);
            var dataProvider = objInfo.TryGet<string>(DataProviderKey);

            await TestHelper.DropDatabaseAsync(
                new ConfigurationConnection(connectionString, dataProvider),
                context.CancellationToken);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Создаёт объект обрабатываемого типа.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        /// <param name="dataProvider">Провайдер для подключения к базе данных.
        /// Обычно это имя класса для подключения или имя объекта <see cref="ConfigurationDataProvider"/>.
        /// Значение <see langword="null"/> или <see cref="string.Empty"/> означает, что будет использоваться стандартный провайдер SQL Server.
        /// </param>
        /// <param name="fixtureID">Идентификатор владельца объекта.
        /// Обычно это значение, возвращаемое методом <see cref="object.GetHashCode()"/>,
        /// где <see cref="object"/> - класс, содержащий текущий набор тестов,
        /// в котором был создан внешний ресурс (test fixture).</param>
        /// <returns>Созданный объект.</returns>
        public static ExternalObjectInfo CreateObjectInfo(
            string connectionString,
            string dataProvider,
            int fixtureID)
        {
            Check.ArgumentNotNull(connectionString, nameof(connectionString));

            var obj = new ExternalObjectInfo()
            {
                ID = Guid.NewGuid(),
                TypeID = ObjectTypeID,
                Created = DateTime.UtcNow,
                FixtureID = fixtureID,
            };

            obj.Info[ConnectionStringKey] = connectionString;

            if (!string.IsNullOrEmpty(dataProvider))
            {
                obj.Info[DataProviderKey] = dataProvider;
            }

            return obj;
        }

        #endregion
    }
}
