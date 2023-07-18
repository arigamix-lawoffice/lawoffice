#nullable enable

namespace Tessa.Extensions.Default.Console.Maintenance
{
    /// <summary>
    /// Поддерживаемые команды.
    /// </summary>
    public enum SupportedCommand
    {
        /// <summary>
        /// Перевести в режим техобслуживания.
        /// </summary>
        SwitchOn,
        
        /// <summary>
        /// Перевести в нормальный режим работы.
        /// </summary>
        SwitchOff,
        
        /// <summary>
        /// Проверить возможность перевода в режим техобслуживания.
        /// </summary>
        Check,
        
        /// <summary>
        /// Получить текущий режим технического обслуживания.
        /// </summary>
        Status,
        
        /// <summary>
        /// Проверить доступность сервиса.
        /// </summary>
        HealthCheck,
    }
}
