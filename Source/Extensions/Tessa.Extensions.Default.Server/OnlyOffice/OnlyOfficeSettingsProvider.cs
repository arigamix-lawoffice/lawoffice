#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    public sealed class OnlyOfficeSettingsProvider :
        IOnlyOfficeSettingsProvider
    {
        #region Constructors

        public OnlyOfficeSettingsProvider(ICardCache cardCache) =>
            this.cardCache = cardCache ?? throw new ArgumentNullException(nameof(cardCache));

        #endregion

        #region Fields

        private readonly ICardCache cardCache;

        #endregion

        #region IOnlyOfficeSettingsProvider Members

        /// <inheritdoc />
        public async ValueTask<IOnlyOfficeSettings> GetSettingsAsync(CancellationToken cancellationToken = default)
        {
            var result = await this.cardCache.Cards.GetAsync("OnlyOfficeSettings", cancellationToken);
            if (!result.IsSuccess)
            {
                throw new ValidationException(result.ValidationResult);
            }

            Dictionary<string, object?> fields = result.GetValue().Sections["OnlyOfficeSettings"].RawFields;

            var converterUrl = fields.Get<string>("ConverterUrl");
            var documentBuilderPath = fields.Get<string>("DocumentBuilderPath");
            var webApiBasePath = fields.Get<string>("WebApiBasePath");
            var tokenLifetimePeriod = fields.Get<int>("TokenLifetimePeriod");
            var loadTimeout = TimeSpan.FromMinutes(fields.Get<int>("LoadTimeoutPeriod"));

            return new OnlyOfficeSettings(converterUrl, documentBuilderPath, webApiBasePath, tokenLifetimePeriod, loadTimeout);
        }

        #endregion


    }
}
