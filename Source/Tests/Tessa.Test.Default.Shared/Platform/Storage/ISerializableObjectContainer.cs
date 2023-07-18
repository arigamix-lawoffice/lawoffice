using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Контейнер для объекта <see cref="Tessa.Platform.Storage.SerializableObject"/>.
    /// 
    /// Интерфейс должен быть реализован на TestFixture для успешного применения атрибута
    /// <see cref="SetupSerializableObjectAttribute"/>, который автоматически заполнит свойства для каждого теста
    /// с помощью фабрики <see cref="ISerializableObjectFactory"/>.
    /// </summary>
    public interface ISerializableObjectContainer
    {
        /// <summary>
        /// Фабрика <see cref="ISerializableObjectFactory"/>, с помощью которой можно получить
        /// новый объект <see cref="Tessa.Platform.Storage.SerializableObject"/>.
        /// </summary>
        ISerializableObjectFactory SerializableObjectFactory { get; set; }

        /// <summary>
        /// Сериализуемый объект, заполненный с помощью фабрики <see cref="SerializableObjectFactory"/>.
        /// </summary>
        /// <remarks>
        /// При изменении свойства <see cref="SerializableObject"/> следует также изменить
        /// свойство <see cref="DynamicObject"/>.
        /// </remarks>
        SerializableObject SerializableObject { get; set; }

        /// <summary>
        /// Экземпляр динамической оболочки для объекта <see cref="SerializableObject"/>.
        /// </summary>
        /// <remarks>
        /// При изменении свойства <see cref="SerializableObject"/> следует также изменить
        /// свойство <see cref="DynamicObject"/>.
        /// </remarks>
        dynamic DynamicObject { get; set; }
    }
}
