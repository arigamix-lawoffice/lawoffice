using LinqToDB.DataProvider;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <inheritdoc cref="IDbFactory"/>
    public sealed class TestDbFactory :
        IDbFactory
    {
        #region Fields

        private readonly IDataProvider dataProvider;
        private readonly string connectionString;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestDbFactory"/>.
        /// </summary>
        /// <param name="dataProvider">Провайдер соединения с базой данных.</param>
        /// <param name="connectionString">Текст строки подключения.</param>
        public TestDbFactory(
            IDataProvider dataProvider,
            string connectionString)
        {
            this.dataProvider = NotNullOrThrow(dataProvider);
            this.connectionString = connectionString;
        }

        #endregion

        #region IDbFactory Members

        /// <inheritdoc/>
        public DbManager Create() =>
            new DbManager(this.dataProvider, this.connectionString);

        #endregion
    }
}
