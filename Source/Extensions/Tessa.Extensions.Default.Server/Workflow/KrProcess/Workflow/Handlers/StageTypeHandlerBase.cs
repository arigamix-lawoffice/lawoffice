using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Представляет абстрактный обработчик этапа.
    /// </summary>
    public abstract class StageTypeHandlerBase : IStageTypeHandler
    {
        #region implementation

        /// <inheritdoc />
        public virtual Task BeforeInitializationAsync(
            IStageTypeHandlerContext context)
        {
            context.Stage.ClearAutomaticallyChangedValues();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task AfterPostprocessingAsync(
            IStageTypeHandlerContext context)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            return Task.FromResult(StageHandlerResult.EmptyResult);
        }

        /// <inheritdoc />
        public virtual Task<StageHandlerResult> HandleTaskCompletionAsync(IStageTypeHandlerContext context)
        {
            return Task.FromResult(StageHandlerResult.EmptyResult);
        }

        /// <inheritdoc />
        public virtual Task<StageHandlerResult> HandleTaskReinstateAsync(IStageTypeHandlerContext context)
        {
            return Task.FromResult(StageHandlerResult.EmptyResult);
        }

        /// <inheritdoc />
        public virtual Task<StageHandlerResult> HandleSignalAsync(IStageTypeHandlerContext context)
        {
            return Task.FromResult(StageHandlerResult.EmptyResult);
        }

        /// <inheritdoc />
        public virtual Task<StageHandlerResult> HandleResurrectionAsync(IStageTypeHandlerContext context)
        {
            return Task.FromResult(StageHandlerResult.EmptyResult);
        }

        /// <inheritdoc />
        public virtual Task<bool> HandleStageInterruptAsync(
            IStageTypeHandlerContext context) => TaskBoxes.True;

        #endregion
    }
}
