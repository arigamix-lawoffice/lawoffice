using System.Threading.Tasks;
using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Фабрика для создания объектов <see cref="CardType"/>. Используется в юнит-тестах.
    /// </summary>
    public interface ICardTypeFactory
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="CardType"/>.
        /// </summary>
        /// <returns>Созданный экземпляр класса <see cref="CardType"/>.</returns>
        ValueTask<CardType> CreateAsync();
    }
}
