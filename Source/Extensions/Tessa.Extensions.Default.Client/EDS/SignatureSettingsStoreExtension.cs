using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.EDS;

namespace Tessa.Extensions.Default.Client.EDS
{
    public sealed class SignatureSettingsStoreExtension : CardStoreExtension
    {
        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            Card card;

            if (context.Response != null
                || !context.ValidationResult.IsSuccessful()
                || (card = context.Request.TryGetCard()) == null
            )
            {
                return Task.CompletedTask;
            }

            card.Sections.TryGetValue(SignatureHelper.SignatureSettingsCertificateSettingsSectionName, out var certsSection);
            var certsRows = certsSection?.TryGetRows();

            if (certsRows?.Any() ?? false)
            {
                foreach (var row in certsRows)
                {
                    var isValidDate = row.Get<bool?>(SignatureHelper.CertificateSettingsIsValidDateFieldName);
                    if (isValidDate != true
                        && row[SignatureHelper.CertificateSettingsStartDateFieldName] == null
                        && row[SignatureHelper.CertificateSettingsEndDateFieldName] == null
                        && string.IsNullOrEmpty(row.Get<string>(SignatureHelper.CertificateSettingsCompanyFieldName))
                        && string.IsNullOrEmpty(row.Get<string>(SignatureHelper.CertificateSettingsSubjectFieldName))
                        && string.IsNullOrEmpty(row.Get<string>(SignatureHelper.CertificateSettingsIssuerFieldName))
                    )
                    {
                        row.State = CardRowState.Deleted;
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}