namespace Tessa.Extensions.Default.Console.ConvertConfiguration
{
    /// <summary>
    /// Вспомогательный класс, который предоставляет информацию о конвертируемом оъекте.
    /// </summary>
    internal sealed class ConversionItem
    {
        /// <summary>
        /// Создает экземпляр класса и записывает в него информацию по конвертируемому объекту.
        /// </summary>
        /// <param name="oldPath">Путь до файла, который необходимо сконвертировать.</param>
        /// <param name="newPath">Путь до файла, куда будет сконвертирован объект.</param>
        /// <param name="obj">
        /// Объект, который будет сконвертирован. В зависимости от типа объекта,
        /// будет выполнена необходимая логика конвертации.
        /// </param>
        public ConversionItem(string oldPath, string newPath, object obj)
        {
            this.OldPath = oldPath;
            this.NewPath = newPath;
            this.Object = obj;
        }

        /// <summary>
        /// Путь до файла, который необходимо сконвертировать.
        /// </summary>
        public string OldPath { get; }

        /// <summary>
        /// Путь до файла, куда будет сконвертирован объект.
        /// </summary>
        public string NewPath { get; }

        /// <summary>
        /// Объект, который будет сконвертирован. В зависимости от типа объекта,
        /// будет выполнена необходимая логика конвертации.
        /// </summary>
        public object Object { get; }
    }
}