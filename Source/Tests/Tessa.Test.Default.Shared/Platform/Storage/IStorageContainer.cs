using System.Collections.Generic;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Контейнер для объекта <c>Dictionary&lt;string, object&gt;</c>.
    /// 
    /// Интерфейс должен быть реализован на TestFixture для успешного применения атрибута
    /// <see cref="SetupStorageAttribute"/>, который автоматически заполнит свойства для каждого теста
    /// с помощью фабрики <see cref="IStorageFactory"/>.
    /// </summary>
    public interface IStorageContainer
    {
        /// <summary>
        /// Фабрика <see cref="IStorageFactory"/>, с помощью которой можно получить
        /// новый объект <c>Dictionary&lt;string, object&gt;</c>.
        /// </summary>
        IStorageFactory StorageFactory { get; set; }

        /// <summary>
        /// Объект <c>Dictionary&lt;string, object&gt;</c>, заполненный с помощью фабрики <see cref="StorageFactory"/>.
        /// </summary>
        Dictionary<string, object> Storage { get; set; }
    }
}
