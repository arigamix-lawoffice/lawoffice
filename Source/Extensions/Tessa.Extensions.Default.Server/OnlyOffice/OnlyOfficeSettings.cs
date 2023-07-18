#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// Представляет собой класс, содержащий настройки редактора OnlyOffice.
    /// </summary>
    public sealed class OnlyOfficeSettings :
        IOnlyOfficeSettings
    {
        #region Constructors

        public OnlyOfficeSettings(
            string? converterUrl,
            string? documentBuilderPath,
            string? webApiBasepath,
            int tokenLifetimePeriod,
            TimeSpan loadTimeout)
        {
            this.ConverterUrl = converterUrl;
            this.DocumentBuilderPath = documentBuilderPath;
            this.WebApiBasePath = webApiBasepath;
            this.TokenLifetimePeriod = tokenLifetimePeriod;
            this.LoadTimeout = loadTimeout;
        }

        #endregion

        #region IOnlyOfficeSettings Members

        /// <inheritdoc />
        public string? ConverterUrl { get; }

        /// <inheritdoc />
        public string? DocumentBuilderPath { get; }

        /// <inheritdoc />
        public string? WebApiBasePath { get; }

        /// <inheritdoc />
        public int TokenLifetimePeriod { get; }

        /// <inheritdoc />
        public TimeSpan LoadTimeout { get; }

        #endregion
    }
}
