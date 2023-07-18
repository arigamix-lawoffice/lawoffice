using System.Collections.Generic;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Фабрика для создания объектов <c>Dictionary&lt;string, object&gt;</c>. Используется в юнит-тестах.
    /// </summary>
    public interface IStorageFactory
    {
        /// <summary>
        /// Создаёт экземпляр класса <c>Dictionary&lt;string, object&gt;</c> и заполняет его данными.
        /// </summary>
        /// <returns>Созданный экземпляр класса <c>Dictionary&lt;string, object&gt;</c>.</returns>
        Dictionary<string, object> Create();
    }
}
