using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <inheritdoc cref="IKrDocumentStateManager" />
    public sealed class KrDocumentStateManager :
        IKrDocumentStateManager
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrDocumentStateManager"/>.
        /// </summary>
        /// <param name="cardMetadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        public KrDocumentStateManager(ICardMetadata cardMetadata)
        {
            this.cardMetadata = cardMetadata ?? throw new ArgumentNullException(nameof(cardMetadata));
        }

        #endregion

        #region IDocStateManager Members

        /// <inheritdoc/>
        public async ValueTask<(bool HasCardChanges, bool HasMainSatelliteChanges, KrState? OldState)> SetStateAsync(
            Card card,
            Card mainSatelliteCard,
            KrState state,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(mainSatelliteCard, nameof(mainSatelliteCard));

            var stateName = await this.cardMetadata.GetDocumentStateNameAsync(state, cancellationToken);

            (bool HasCardChanges, bool HasMainSatelliteChanges, KrState? OldStateID) result = default;

            if (card.Sections.TryGetValue(KrConstants.DocumentCommonInfo.Name, out var documentCommonInfoSection))
            {
                var cardMetadata = await this.cardMetadata.GetMetadataForTypeAsync(card.TypeID, cancellationToken);
                var cardMetadataSections = await cardMetadata.GetSectionsAsync(cancellationToken);
                var documentCommonInfoMetadataColums = cardMetadataSections[KrConstants.DocumentCommonInfo.Name].Columns;

                if (documentCommonInfoMetadataColums.Contains(KrConstants.DocumentCommonInfo.StateID))
                {
                    var fields = documentCommonInfoSection.Fields;
                    var oldStateID = fields.TryGet<int?>(KrConstants.KrApprovalCommonInfo.StateID);

                    if (oldStateID != state.ID)
                    {
                        fields[KrConstants.DocumentCommonInfo.StateID] = Int32Boxes.Box(state.ID);

                        if (documentCommonInfoMetadataColums.Contains(KrConstants.DocumentCommonInfo.StateName))
                        {
                            fields[KrConstants.DocumentCommonInfo.StateName] = stateName;
                        }

                        result.HasCardChanges = true;
                    }
                }
            }

            if (mainSatelliteCard.TryGetKrApprovalCommonInfoSection(out var krApprovalCommonInfoSection))
            {
                var fields = krApprovalCommonInfoSection.Fields;
                var oldStateID = fields.TryGet<int?>(KrConstants.KrApprovalCommonInfo.StateID);
                result.OldStateID = (KrState?) oldStateID;

                if (oldStateID != state.ID)
                {
                    fields[KrConstants.KrApprovalCommonInfo.StateID] = Int32Boxes.Box(state.ID);
                    fields[KrConstants.KrApprovalCommonInfo.StateName] = stateName;
                    result.HasMainSatelliteChanges = true;
                }
            }

            return result;
        }

        #endregion
    }
}
