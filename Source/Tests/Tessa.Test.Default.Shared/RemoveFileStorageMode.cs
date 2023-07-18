namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Перечисление режимов удаления файлового хранилища.
    /// </summary>
    public enum RemoveFileStorageMode
    {
        /// <summary>
        /// Файловое хранилище не удаляется.
        /// </summary>
        None,

        /// <summary>
        /// Файловое хранилище удаляется всегда.
        /// </summary>
        Always,

        /// <summary>
        /// Файловое хранилище удаляется в зависимости от того применён ли к классу атрибут <see cref="SetupTempDbAttribute"/> и выставлен ли флаг <see cref="SetupTempDbAttribute.RemoveDatabase"/>.
        /// </summary>
        /// <remarks>
        /// Файловое хранилище удаляется, если отсутствует атрибут <see cref="SetupTempDbAttribute"/> или выставлен флаг <see cref="SetupTempDbAttribute.RemoveDatabase"/>.
        /// </remarks>
        Auto
    }
} 
