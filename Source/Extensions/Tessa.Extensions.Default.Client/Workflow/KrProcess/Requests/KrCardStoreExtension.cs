using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на сохранение карточки, содержащей маршрут.
    /// </summary>
    public sealed class KrCardStoreExtension :
        CardStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        private readonly ParentStageRowIDVisitor visitor;

        #endregion

        #region Constructors

        public KrCardStoreExtension(
            IKrTypesCache krTypesCache,
            ICardMetadata cardMetadata)
        {
            this.krTypesCache = NotNullOrThrow(krTypesCache);
            this.visitor = new ParentStageRowIDVisitor(cardMetadata);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(
            ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || context.Request.TryGetCard() is not { } card
                || KrProcessSharedHelper.RuntimeCard(card.TypeID)
                && (await KrComponentsHelper.GetKrComponentsAsync(card, this.krTypesCache, context.CancellationToken)).HasNot(KrComponents.Routes))
            {
                return;
            }

            card.RemoveLocalTiles();

            if (!card.TryGetStagesSection(out var stagesSection, true)
                || stagesSection.TryGetRows() is not { Count: > 0 } stagesRows)
            {
                return;
            }

            await this.visitor.VisitAsync(
                card.Sections,
                DefaultCardTypes.KrCardTypeID,
                KrConstants.KrStages.Virtual);

            foreach (var row in stagesRows)
            {
                row.SetChanged(KrConstants.KrStages.DisplayTimeLimit, false);
                row.SetChanged(KrConstants.KrStages.DisplayParticipants, false);
                row.SetChanged(KrConstants.KrStages.DisplaySettings, false);
            }
        }

        #endregion
    }
}
