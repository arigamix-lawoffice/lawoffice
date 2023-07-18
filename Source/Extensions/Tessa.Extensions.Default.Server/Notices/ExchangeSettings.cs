using System;
using Tessa.Exchange.WebServices.Data;

namespace Tessa.Extensions.Default.Server.Notices
{
    /// <summary>
    /// Настройки отправки и получения почты по протоколу Exchange.
    /// </summary>
    public class ExchangeSettings
    {
        public string OAuthToken { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public Uri ProxyAddress { get; set; }

        public string ProxyUser { get; set; }

        public string ProxyPassword { get; set; }

        public ExchangeVersion Version { get; set; }

        public string From { get; set; }

        public string FromDisplayName { get; set; }
    }
}