using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class CreateOrAddPartnerCardStoreExtension : CardStoreExtension
    {
        #region Constants

        private const string NewPartnerIDKey = StorageHelper.SystemKeyPrefix + "NewPartnerID";

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        public CreateOrAddPartnerCardStoreExtension(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            var card = context.Request.Card;
            if (card == null ||
                !card.Sections.ContainsKey("DocumentCommonInfo") ||
                !card.Sections["DocumentCommonInfo"].Fields.TryGetValue("PartnerID", out var partnerID))
            {
                return;
            }

            // Если нормально заданный контрагент или вообще не задан, то выходим
            if (partnerID == null ||
                (Guid)partnerID != Guid.Empty)
            {
                return;
            }

            var partnerName = card.Sections["DocumentCommonInfo"].Fields.Get<string>("PartnerName");

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var command =
                    db.SetCommand(context.DbScope.BuilderFactory
                        .Select().Top(1).C(null, "ID", "Name")
                        .From("Partners").NoLock()
                        .Where().LowerC("Name").Equals().LowerP("Name")
                        .Limit(1)
                        .Build(),
                        db.Parameter("Name", partnerName))
                    .LogCommand();

                await using var reader = await command.ExecuteReaderAsync(context.CancellationToken);
                if (await reader.ReadAsync(context.CancellationToken))
                {
                    // Если нашёлся такой же, то подставляем и выходим
                    card.Sections["DocumentCommonInfo"].Fields["PartnerID"] = reader.GetValue<Guid>(0);
                    card.Sections["DocumentCommonInfo"].Fields["PartnerName"] = reader.GetValue<string>(1);
                    return;
                }
            }

            var newPartnerID = Guid.NewGuid();

            card.Info.Add(NewPartnerIDKey, newPartnerID);

            card.Sections["DocumentCommonInfo"].Fields["PartnerID"] = newPartnerID;
            card.Sections["DocumentCommonInfo"].Fields["PartnerName"] = partnerName;
        }

        public override async Task AfterBeginTransaction(ICardStoreExtensionContext context)
        {
            var card = context.Request.Card;
            if (card == null ||
                !card.Info.ContainsKey(NewPartnerIDKey))
            {
                return;
            }

            var info = new Dictionary<string, object>();

            if (card.StoreMode == CardStoreMode.Update)
            {
                info.Add(CreatePartnerKeys.MainCardIDKey, card.ID);
            }

            info.Add(CreatePartnerKeys.MainCardTypeIDKey, context.CardType.ID);

            if (card.Sections.ContainsKey("DocumentCommonInfo") &&
                card.Sections["DocumentCommonInfo"].Fields.TryGetValue("DocType", out var docTypeID) &&
                docTypeID != null)
            {
                info.Add(CreatePartnerKeys.MainCardDocTypeIDKey, docTypeID);
            }

            var partnerCardNewResponse =
                await this.cardRepository.NewAsync(
                    new CardNewRequest
                    {
                        CardTypeID = DefaultCardTypes.PartnerTypeID,
                        CardTypeName = DefaultCardTypes.PartnerTypeName,
                        Info = info
                    },
                    context.CancellationToken);

            if (!partnerCardNewResponse.ValidationResult.IsSuccessful())
            {
                context.ValidationResult.Add(partnerCardNewResponse.ValidationResult);
                return;
            }

            var partnerCard = partnerCardNewResponse.Card;

            partnerCard.ID = (Guid)card.Info[NewPartnerIDKey];
            partnerCard.Sections["Partners"].Fields["Name"] = card.Sections["DocumentCommonInfo"].Fields["PartnerName"];

            var partnerCardStoreResponse = await this.cardRepository.StoreAsync(
                new CardStoreRequest
                {
                    Card = partnerCard
                },
                context.CancellationToken);

            if (!partnerCardStoreResponse.ValidationResult.IsSuccessful())
            {
                context.ValidationResult.Add(partnerCardNewResponse.ValidationResult);
            }
        }

        #endregion
    }
}
