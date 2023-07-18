using Tessa.Cards;
using Tessa.Extensions.PostgreSql.Server;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Фабрика для создания объектов <see cref="ICardTypeServerRepository"/> для управления
    /// типами карточек с помощью базы данных SQL Server. Используется в юнит-тестах.
    /// </summary>
    /// <remarks>
    /// Используется по умолчанию при задании типа <see cref="ICardTypeRepositoryFactory"/> в атрибутах.
    /// </remarks>
    public class DefaultCardTypeRepositoryFactory :
        ICardTypeRepositoryFactory
    {
        #region ICardTypeRepositoryFactory Members

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ICardTypeServerRepository"/> для управления
        /// типами карточек с помощью базы данных SQL Server для заданного <see cref="DbManager"/>.
        /// </summary>
        /// <param name="dbScope"> Область видимости объекта <see cref="DbManager"/>. </param>
        /// <returns>Созданный экземпляр класса <see cref="ICardTypeServerRepository"/>.</returns>
        public ICardTypeServerRepository Create(IDbScope dbScope)
        {
            return new CardTypeServerRepository(dbScope, new PostgreSqlBulkInsertExecutor());
        }

        #endregion
    }
}
