using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Server.Cards.Satellites.Handlers;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Satellite
{
    public sealed class KrSatelliteHandler : SatelliteHandlerBase
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public KrSatelliteHandler(IKrTypesCache krTypesCache)
        {
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region Base Overrides

        public override async ValueTask<bool> IsMainCardTypeAsync(CardType mainCardType, CancellationToken cancellationToken = default)
        {
            return (await KrComponentsHelper.GetKrComponentsAsync(mainCardType.ID, this.krTypesCache, cancellationToken)).Has(KrComponents.Base);
        }

        public override ValueTask PrepareSatelliteForCreateAsync(ICardGetExtensionContext context, Card satellite, Guid mainCardID, Guid? taskRowID)
        {
            var krApprovalSection = satellite.Sections.GetOrAdd(KrConstants.KrApprovalCommonInfo.Name);
            krApprovalSection.Fields[KrConstants.KrApprovalCommonInfo.MainCardID] = mainCardID;

            return new ValueTask();
        }

        #endregion
    }
}
