namespace Tessa.Extensions.Default.Server.Notices
{
    /// <summary>
    /// Объект, содержащий настройки отправки и получения почты по протоколу POP3 или IMAP.
    /// </summary>
    public interface IPop3ImapSettingsContainer
    {
        /// <summary>
        /// Настройки отправки и получения почты по протоколу POP3 или IMAP.
        /// </summary>
        Pop3ImapSettings Pop3ImapSettings { get; set; }
    }
}
