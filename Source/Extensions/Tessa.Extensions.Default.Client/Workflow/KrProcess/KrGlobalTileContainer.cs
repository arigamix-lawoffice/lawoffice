using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <inheritdoc cref="IKrGlobalTileContainer"/>
    public sealed class KrGlobalTileContainer :
        IKrGlobalTileContainer
    {
        #region Fields

        private volatile IReadOnlyList<KrTileInfo> infos;

        #endregion

        #region IKrGlobalTileContainer Members

        /// <inheritdoc />
        public void Init(
            IReadOnlyList<KrTileInfo> globalTiles)
        {
            Check.ArgumentNotNull(globalTiles, nameof(globalTiles));

            this.infos = globalTiles;
        }

        /// <inheritdoc />
        public IReadOnlyList<KrTileInfo> GetTileInfos() => this.infos;

        #endregion
    }
}
