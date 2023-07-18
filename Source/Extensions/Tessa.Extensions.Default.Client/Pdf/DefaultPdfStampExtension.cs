using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Pdf
{
    public sealed class DefaultPdfStampExtension :
        PdfStampExtension
    {
        #region Constructors

        public DefaultPdfStampExtension(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Base Overrides

        public override async Task GenerateForPage(IPdfStampExtensionContext context)
        {
            if (!context.StampWriter.IsEmpty)
            {
                return;
            }

            Card card = context.Card;

            DateTime? documentDate;
            if (!card.Sections.TryGetValue("DocumentCommonInfo", out CardSection section)
                || !(documentDate = section.RawFields.TryGet<DateTime?>("DocDate")).HasValue)
            {
                documentDate = card.Created;
            }

            ICardModel model = context.Model;
            string digest = model.Digest;

            if (string.IsNullOrEmpty(digest) && card.StoreMode == CardStoreMode.Insert)
            {
                digest = await this.cardRepository.GetDigestAsync(card, CardDigestEventNames.CardModelClient, context.CancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(digest))
                {
                    model.Digest = digest;
                }
            }

            context.StampWriter
                .AppendLine(digest)
                .AppendDate(documentDate)
                ;
        }

        #endregion
    }
}
