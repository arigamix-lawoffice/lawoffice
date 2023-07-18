using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Контейнер для объекта <see cref="Tessa.Cards.CardType"/>.
    /// 
    /// Интерфейс должен быть реализован на TestFixture для успешного применения атрибута
    /// <see cref="SetupCardTypeAttribute"/>, который автоматически заполнит свойства для каждого теста
    /// с помощью фабрики <see cref="ICardTypeFactory"/>.
    /// </summary>
    public interface ICardTypeContainer
    {
        /// <summary>
        /// Фабрика <see cref="ICardTypeFactory"/>, с помощью которой можно получить
        /// новый объект <see cref="Tessa.Cards.CardType"/>.
        /// </summary>
        ICardTypeFactory CardTypeFactory { get; set; }

        /// <summary>
        /// Сериализуемый объект, заполненный с помощью фабрики <see cref="CardTypeFactory"/>.
        /// </summary>
        CardType CardType { get; set; }
    }
}
