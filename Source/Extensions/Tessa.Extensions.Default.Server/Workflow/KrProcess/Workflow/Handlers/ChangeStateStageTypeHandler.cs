using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.ChangesStateDescriptor"/>.
    /// </summary>
    public class ChangeStateStageTypeHandler : StageTypeHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var state = context.Stage.SettingsStorage.TryGet<int?>(KrConstants.KrChangeStateSettingsVirtual.StateID);
            if (state.HasValue)
            {
                context.WorkflowProcess.State = (KrState) state;
            }

            return Task.FromResult(StageHandlerResult.CompleteResult);
        }

        #endregion
    }
}