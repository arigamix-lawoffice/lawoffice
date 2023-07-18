using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Settings
{
    public class PartnersCardMetadataExtension : CardTypeMetadataExtension
    {
        #region Constructors

        public PartnersCardMetadataExtension(ICardMetadata clientCardMetadata, ICardCache clientCache)
            : base(clientCardMetadata)
        {
            Check.ArgumentNotNull(clientCache, "clientCache");

            this.clientCache = clientCache;
        }

        public PartnersCardMetadataExtension(IDbScope serverDbScope)
            : base()
        {
            Check.ArgumentNotNull(serverDbScope, "serverDbScope");

            this.serverDbScope = serverDbScope;
        }

        #endregion

        #region Fields

        private readonly ICardCache clientCache;

        private readonly IDbScope serverDbScope;

        #endregion

        #region Private Methods

        private async ValueTask<bool> GetPartnersSettingInClientModeAsync(CancellationToken cancellationToken = default)
        {
            var krSettings = await this.clientCache.Cards.GetAsync(DefaultCardTypes.KrSettingsTypeName, cancellationToken).ConfigureAwait(false);

            return krSettings.IsSuccess
                && krSettings.GetValue().Sections["KrSettings"].RawFields.Get<bool>("AllowManualInputAndAutoCreatePartners");
        }

        private async Task<bool> GetPartnersSettingInServerModeAsync(CancellationToken cancellationToken = default)
        {
            await using (this.serverDbScope.Create())
            {
                var db = this.serverDbScope.Db;
                return await
                    db.SetCommand(this.serverDbScope.BuilderFactory
                        .Select().Top(1).C("AllowManualInputAndAutoCreatePartners")
                        .From("KrSettings").NoLock()
                        .Limit(1)
                        .Build())
                    .LogCommand()
                    .ExecuteAsync<bool>(cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region Base Overrides

        public override async Task ModifyTypes(ICardMetadataExtensionContext context)
        {
            // Если включено, то всё нормально и ничего убирать из настроек не надо
            if (this.ClientMode)
            {
                if (await this.GetPartnersSettingInClientModeAsync(context.CancellationToken).ConfigureAwait(false))
                {
                    return;
                }
            }
            else if (await this.GetPartnersSettingInServerModeAsync(context.CancellationToken).ConfigureAwait(false))
            {
                return;
            }

            // Убираем из настроек ручной ввод
            List<Guid> allowedCardTypeIDs = new List<Guid> { DefaultCardTypes.ContractTypeID, DefaultCardTypes.IncomingTypeID, DefaultCardTypes.OutgoingTypeID };
            foreach (CardType cardType
                in context
                    .CardTypes
                    .Where(x => allowedCardTypeIDs.Contains(x.ID)))
            {
                RemoveManualInput(cardType);
            }
        }

        private static void RemoveManualInput(CardType cardType)
        {
            var mainFormBlocks = cardType.Forms[0].Blocks;

            foreach (var block in mainFormBlocks)
            {
                var partnerControl = block.Controls.FirstOrDefault(p => p.Name == "PartnerControl");

                if (partnerControl != null)
                {
                    partnerControl.ControlSettings[CardControlSettings.ManualInputSetting] = false;
                    partnerControl.ControlSettings[CardControlSettings.ManualInputColumnIDSetting] = null;

                    return;
                }
            }
        }

        #endregion
    }
}