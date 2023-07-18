using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Extensions.Platform.Server.Cards.Satellites.Handlers;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    public sealed class WfSatelliteHandler : SatelliteHandlerBase
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public WfSatelliteHandler(IKrTypesCache krTypesCache)
        {
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region Base Overrides

        public override ValueTask<bool> IsMainCardTypeAsync(CardType mainCardType, CancellationToken cancellationToken = default)
        {
            return WfHelper.TypeSupportsWorkflowAsync(this.krTypesCache, mainCardType, cancellationToken);
        }

        #endregion
    }
}
