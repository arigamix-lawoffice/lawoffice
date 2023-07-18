using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Server.Cards.Satellites.Handlers;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Satellite
{
    public sealed class KrSecondarySatelliteHandler : SatelliteHandlerBase
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public KrSecondarySatelliteHandler(IKrTypesCache krTypesCache)
        {
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override ValueTask<bool> IsMainCardTypeAsync(CardType mainCardType, CancellationToken cancellationToken = default)
        {
            return KrComponentsHelper.HasBaseAsync(mainCardType.ID, this.krTypesCache);
        }

        /// <inheritdoc/>
        public override ValueTask PrepareSatelliteForCreateAsync(ICardGetExtensionContext context, Card satellite, Guid mainCardID, Guid? taskRowID)
        {
            var krApprovalSection = satellite.Sections.GetOrAdd(KrConstants.KrSecondaryProcessCommonInfo.Name);
            krApprovalSection.Fields[KrConstants.KrSecondaryProcessCommonInfo.MainCardID] = mainCardID;

            return new ValueTask();
        }

        #endregion
    }
}
