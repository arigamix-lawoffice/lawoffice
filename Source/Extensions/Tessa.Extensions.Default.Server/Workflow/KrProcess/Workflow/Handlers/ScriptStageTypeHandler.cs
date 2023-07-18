using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="Shared.Workflow.KrProcess.StageTypeDescriptors.ScriptDescriptor"/>.
    /// </summary>
    public class ScriptStageTypeHandler : StageTypeHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext ctx) =>
            Task.FromResult(StageHandlerResult.CompleteResult);

        #endregion
    }
}