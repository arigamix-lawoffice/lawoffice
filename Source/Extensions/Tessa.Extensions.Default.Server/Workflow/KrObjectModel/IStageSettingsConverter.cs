using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    public interface IStageSettingsConverter
    {
        IDictionary<string, object> ToPlain(
            IDictionary<string, object> treeSettings);

        ValueTask<IDictionary<string, object>> ToTreeAsync(
            Guid topLevelRowID,
            IDictionary<string, object> plainSettings,
            CancellationToken cancellationToken = default);
    }
}