using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Контейнер для объекта <see cref="DbManager"/>.
    /// 
    /// Интерфейс должен быть реализован на TestFixture для успешного применения атрибута
    /// <see cref="SetupDbScopeAttribute"/>, который автоматически заполнит свойства для каждого теста
    /// с помощью фабрики <see cref="IDbFactory"/>.
    /// </summary>
    public interface IDbScopeContainer
    {
        /// <summary>
        /// Фабрика <see cref="IDbFactory"/>, с помощью которой можно получить
        /// новый объект <see cref="DbManager"/>.<para/>
        /// Может иметь значение <see langword="null"/>.
        /// </summary>
        IDbFactory DbFactory { get; set; }

        /// <summary>
        /// Область видимости объекта <see cref="DbManager"/>,
        /// который заполняется с помощью фабрики <see cref="DbFactory"/>.<para/>
        /// Может иметь значение <see langword="null"/>.
        /// </summary>
        IDbScope DbScope { get; set; }
    }
}
