using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Фабрика для создания объектов <see cref="DbManager"/>. Используется в юнит-тестах.
    /// </summary>
    public interface IDbFactory
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="DbManager"/>.
        /// </summary>
        /// <returns>Созданный экземпляр класса <see cref="DbManager"/>.</returns>
        DbManager Create();
    }
}
