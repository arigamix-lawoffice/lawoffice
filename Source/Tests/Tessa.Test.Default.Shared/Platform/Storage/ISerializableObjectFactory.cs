using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Фабрика для создания объектов <see cref="SerializableObject"/>. Используется в юнит-тестах.
    /// </summary>
    public interface ISerializableObjectFactory
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="SerializableObject"/>.
        /// </summary>
        /// <returns>Созданный экземпляр класса <see cref="SerializableObject"/>.</returns>
        SerializableObject CreateSerializableObject();
    }
}
