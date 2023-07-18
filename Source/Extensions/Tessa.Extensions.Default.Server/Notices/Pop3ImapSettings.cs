namespace Tessa.Extensions.Default.Server.Notices
{
    /// <summary>
    /// Настройки отправки и получения почты по протоколу POP3 или IMAP.
    /// </summary>
    public class Pop3ImapSettings
    {
        /// <summary>
        /// Адрес почтового сервера.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт, используемый для взаимодействия с почтовым сервером,
        /// или <c>null</c>, если используется порт по умолчанию.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Имя пользователя, от имени которого выполняется взаимодействие с почтовым сервером.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Пароль пользователя, от имени которого выполняется взаимодействие с почтовым сервером.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Признак того, должен ли использоваться почтовый протокол с поддержкой шифрования SSL.
        /// Значение <c>null</c> позволяет выполнить автоматическое определение
        /// в зависимости от возможностей почтового сервера.
        /// </summary>
        public bool? UseSsl { get; set; }
    }
}