namespace Tessa.Extensions.Default.Server.Notices
{
    /// <summary>
    /// Объект, содержащий настройки отправки и получения почты по протоколу Exchange.
    /// </summary>
    public interface IExchangeSettingsContainer
    {
        /// <summary>
        /// Настройки отправки и получения почты по протоколу Exchange.
        /// </summary>
        ExchangeSettings ExchangeSettings { get; set; }
    }
}
