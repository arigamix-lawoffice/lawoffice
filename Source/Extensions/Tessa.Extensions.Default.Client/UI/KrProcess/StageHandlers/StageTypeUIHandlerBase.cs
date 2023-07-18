#nullable enable

using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <inheritdoc cref="IStageTypeUIHandler"/>
    public abstract class StageTypeUIHandlerBase :
        IStageTypeUIHandler
    {
        #region IStageTypeUIHandler Members

        /// <inheritdoc />
        public virtual Task Initialize(IKrStageTypeUIHandlerContext context) => Task.CompletedTask;

        /// <inheritdoc />
        public virtual Task Validate(IKrStageTypeUIHandlerContext context) => Task.CompletedTask;

        /// <inheritdoc />
        public virtual Task Finalize(IKrStageTypeUIHandlerContext context) => Task.CompletedTask;

        #endregion
    }
}
