using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.UI.WorkflowViewer.Actions;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    public sealed class KrChangeStateActionUIHandler : WorkflowActionUIHandlerBase
    {
        public KrChangeStateActionUIHandler()
            :base(KrDescriptors.KrChangeStateDescriptor)
        {
        }
    }
}
