namespace Tessa.Extensions.Default.Console.ImportWorkplaces
{
    /// <summary>
    /// Контекст операции импорта рабочих мест.
    /// </summary>
    public class OperationContext
    {
        /// <summary>
        /// Признак импорта ролей.
        /// </summary>
        public bool ImportRoles { get; set; }

        /// <summary>
        /// Признак импорта внедренных поисковых запросов.
        /// </summary>
        public bool ImportSearchQueries { get; set; }

        /// <summary>
        /// Признак импорта внедренных представлений.
        /// </summary>
        public bool ImportViews { get; set; }

        /// <summary>
        /// Признак предварительной очистки справочника рабочих мест.
        /// </summary>
        public bool ClearWorkplaces { get; set; }

        /// <summary>
        /// Источник файлов.
        /// </summary>
        public string Source { get; set; }

    }
}