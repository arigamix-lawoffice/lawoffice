using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Tiles;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    public interface IKrTileInflater
    {
        List<ITile> Inflate(
            ITileContextSource contextSource,
            IReadOnlyCollection<KrTileInfo> tileInfos);
    }
}