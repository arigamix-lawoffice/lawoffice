using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Licensing;

namespace Tessa.Extensions.Default.Shared.Cards
{
    /// <summary>
    /// Расширение на метаданные карточки настроек типового решения.
    /// Скрывает настройку "Доступ ACL на чтение карточек" в ситуации, когда модуль лицензии ACL отсутствует.
    /// </summary>
    public sealed class KrSettingsCardMetadataExtension : CardTypeMetadataExtension
    {
        #region Fields

        private readonly ILicenseManager licenseManager;

        #endregion

        #region Constructors

        public KrSettingsCardMetadataExtension(
            ILicenseManager licenseManager)
        {
            //конструктор для сервера
            this.licenseManager = NotNullOrThrow(licenseManager);
        }

        public KrSettingsCardMetadataExtension(
            ICardMetadata clientCardMetadata,
            ILicenseManager licenseManager)
            : base(clientCardMetadata)
        {
            //конструктор для клиента
            this.licenseManager = NotNullOrThrow(licenseManager);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task ModifyTypes(ICardMetadataExtensionContext context)
        {
            CardType settingsType = await this.TryGetCardTypeAsync(context, DefaultCardTypes.KrSettingsTypeID, false).ConfigureAwait(false);
            if (settingsType is null)
            {
                return;
            }

            var license = await this.licenseManager.GetLicenseAsync(context.CancellationToken).ConfigureAwait(false);
            if (license.Modules.HasEnterpriseOrContains(LicenseModules.AclID))
            {
                return;
            }

            var control = settingsType.Forms[0].Blocks.FirstOrDefault(x => x.Name == "OtherSettings")?.Controls.FirstOrDefault(x => x.Name == "AclReadCardAccess");
            control?.SetVisible(false);
        }

        #endregion
    }
}
