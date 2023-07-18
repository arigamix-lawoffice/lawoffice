using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class DocLoadBarcodeTemplateNewExtension : CardNewExtension
    {
        #region fields

        private readonly ICardCache cardCache;
        private readonly ILicenseManager licenseManager;

        #endregion

        #region ctor

        public DocLoadBarcodeTemplateNewExtension(ICardCache cardCache, ILicenseManager licenseManager)
        {
            this.cardCache = cardCache;
            this.licenseManager = licenseManager;
        }

        #endregion

        #region CardStoreExtension

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !(await this.licenseManager.GetLicenseAsync(context.CancellationToken))
                    .Modules.Contains(LicenseModules.DocLoadID))
            {
                return;
            }

            var docLoad = await this.cardCache.Cards.GetAsync("DocLoad", context.CancellationToken);
            if (!docLoad.IsSuccess)
            {
                return;
            }

            Card settingsCard = docLoad.GetValue();

            IDictionary<string, object> fields = settingsCard.Sections["DocLoadSettings"].Fields;
            var isEnabled = fields.TryGet<bool>("IsEnabled");
            if (!isEnabled)
            {
                return;
            }

            var tableName = fields.TryGet<string>("DefaultBarcodeTableName");
            var fieldName = fields.TryGet<string>("DefaultBarcodeFieldName");
            var sections = context.Response.Card.Sections;
            if (sections.TryGetValue(tableName, out var section)
                && section.Fields.TryGetValue(fieldName, out _))
            {
                context.Response.Card.Sections[tableName].Fields[fieldName] = null;
            }
        }

        #endregion
    }
}