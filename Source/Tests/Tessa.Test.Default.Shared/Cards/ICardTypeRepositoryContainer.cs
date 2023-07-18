using Tessa.Cards;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Контейнер для объектов <see cref="ICardTypeServerRepository"/> и <see cref="DbManager"/>.
    /// 
    /// Интерфейс должен быть реализован на TestFixture для успешного применения атрибута
    /// <see cref="SetupTempDbForCardTypesAttribute"/>, который автоматически заполнит свойства для каждого теста
    /// с помощью фабрик <see cref="ICardTypeRepositoryFactory"/> и <see cref="IDbFactory"/>.
    /// </summary>
    public interface ICardTypeRepositoryContainer :
        IDbScopeContainer
    {
        /// <summary>
        /// Репозиторий для управления типами карточек,
        /// заполненный с помощью фабрики <see cref="ICardTypeRepositoryFactory"/>.
        /// </summary>
        ICardTypeServerRepository CardTypeRepository { get; set; }
    }
}
