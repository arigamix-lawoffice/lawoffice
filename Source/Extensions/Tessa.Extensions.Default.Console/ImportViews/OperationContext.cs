namespace Tessa.Extensions.Default.Console.ImportViews
{
    /// <summary>
    ///     Контекст операции импорта представлений
    /// </summary>
    public class OperationContext
    {
        /// <summary>
        ///     Gets or sets a value indicating whether Признак очистки списка представлений
        /// </summary>
        public bool ClearViews { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether Признак импорта ролей
        /// </summary>
        public bool ImportRoles { get; set; }

        /// <summary>
        ///     Gets or sets Источник файлов представлений
        /// </summary>
        public string Source { get; set; }
    }
}