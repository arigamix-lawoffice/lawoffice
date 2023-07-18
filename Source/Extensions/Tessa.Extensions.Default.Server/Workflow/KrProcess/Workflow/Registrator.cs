using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrProcessContainer, KrProcessContainer>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrProcessRunnerProvider, KrProcessRunnerProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrProcessRunner, KrSyncProcessRunner>(KrProcessRunnerNames.Sync, new ContainerControlledLifetimeManager())
                .RegisterType<IKrProcessRunner, KrAsyncProcessRunner>(KrProcessRunnerNames.Async, new ContainerControlledLifetimeManager())
                .RegisterType<IKrStageInterrupter, KrStageInterrupter>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrProcessButtonVisibilityEvaluator, KrProcessButtonVisibilityEvaluator>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrSecondaryProcessExecutionEvaluator, KrSecondaryProcessExecutionEvaluator>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrProcessLauncher, KrProcessServerLauncher>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}