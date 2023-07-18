using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public sealed class KrProcessRunnerProvider : IKrProcessRunnerProvider
    {
        #region fields

        private readonly IUnityContainer unityContainer;

        #endregion

        #region constructor

        public KrProcessRunnerProvider(
            IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        #endregion

        #region implementation

        /// <inheritdoc />
        public IKrProcessRunner GetRunner(
            string runnerName) => this.unityContainer.Resolve<IKrProcessRunner>(runnerName);

        #endregion
    }
}