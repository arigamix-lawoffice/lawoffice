// ReSharper disable InconsistentNaming

using Tessa.Localization;

namespace Tessa.Extensions.Default.Console.ConvertConfiguration
{
    /// <summary>
    /// Режим преобразования конфигурации.
    /// </summary>
    public enum ConversionMode
    {
        /// <summary>
        /// Преобразовать объекты конфигурации до форматов в текущей версии платформы.
        /// </summary>
        [LocalizableDescription("Common_CLI_ConfigurationConversionMode_Upgrade")]
        Upgrade,
        
        /// <summary>
        /// Преобразовать файлы конфигурации в старые форматы, совместимые с предыдущими версиями платформы.
        /// </summary>
        [LocalizableDescription("Common_CLI_ConfigurationConversionMode_Downgrade")]
        Downgrade,
        
        /// <summary>
        /// Переводы строк преобразуются в символ LF, которые совместимы с Windows и Linux. Это рекомендуемый формат.
        /// </summary>
        [LocalizableDescription("Common_CLI_ConfigurationConversionMode_LF")]
        LF,
        
        /// <summary>
        /// Переводы строк преобразуются в символы CR LF, которые совместимы с Windows. Используйте для сравнения с объектами,
        /// выгруженными в предыдущих версиях платформы из приложений TessaAdmin или посредством команды tadmin на Windows.
        /// </summary>
        [LocalizableDescription("Common_CLI_ConfigurationConversionMode_CRLF")]
        CRLF,
    }
}