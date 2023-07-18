using Tessa.Cards;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Фабрика для создания объектов <see cref="ICardTypeServerRepository"/>. Используется в юнит-тестах.
    /// </summary>
    public interface ICardTypeRepositoryFactory
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="ICardTypeServerRepository"/> для заданного <see cref="DbManager"/>.
        /// </summary>
        /// <param name="dbScope"> Область видимости объекта <see cref="DbManager"/>. </param>
        /// <returns>Созданный экземпляр класса <see cref="ICardTypeServerRepository"/>.</returns>
        ICardTypeServerRepository Create(IDbScope dbScope);
    }
}
