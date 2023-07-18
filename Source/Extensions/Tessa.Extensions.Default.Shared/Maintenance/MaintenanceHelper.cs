namespace Tessa.Extensions.Default.Shared.Maintenance
{
    /// <summary>
    /// Помощник работы для режима техобслуживания.
    /// </summary>
    public static class MaintenanceHelper
    {
        #region Fields

        /// <summary>
        /// Порядок использования сервиса получения данных из
        /// файла конфигурации.
        /// </summary>
        public const int ConfigFileLocalizationOrder = 10;
        
        /// <summary>
        /// Порядок использования сервиса получения данных из
        /// системного сервиса локализации.
        /// </summary>
        public const int LocalizationServiceLocalizationOrder = 100;

        /// <summary>
        /// Имя секции для настроек режима техобслуживания.
        /// </summary>
        public static readonly string MaintenanceConfigSection = "Maintenance";
        
        /// <summary>
        /// Ключ аргументов для языка по умолчанию.
        /// </summary>
        public static readonly string DefaultLanguage = nameof(DefaultLanguage);
        
        /// <summary>
        /// Ключ аргументов для префикса имени в библиотеке локализации.
        /// </summary>
        public static readonly string Prefix = nameof(Prefix);
        
        #endregion
    }
}
