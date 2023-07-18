using System;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Представляет данные используемые при проверке файла.
    /// </summary>
    public readonly struct TestFileInfo
    {
        /// <summary>
        /// Возвращает идентификатор файла.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Возвращает имя файла.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает контент файла.
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestFileInfo"/>.
        /// </summary>
        /// <param name="rowID">Идентификатор файла.</param>
        /// <param name="name">Имя файла.</param>
        /// <param name="content">Контент файла.</param>
        public TestFileInfo(
            Guid id,
            string name,
            byte[] content)
        {
            this.ID = id;
            this.Name = name;
            this.Content = content;
        }
    }
}
