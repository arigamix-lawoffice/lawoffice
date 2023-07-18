using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<SkipProcessSignalHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<CancelProcessSignalHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<TransitionGlobalSignalHandler>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .Resolve<IKrProcessContainer>()
                .RegisterGlobalSignal<SkipProcessSignalHandler>(KrConstants.KrSkipProcessGlobalSignal)
                .RegisterGlobalSignal<CancelProcessSignalHandler>(KrConstants.KrCancelProcessGlobalSignal)
                .RegisterGlobalSignal<TransitionGlobalSignalHandler>(KrConstants.KrTransitionGlobalSignal)
                ;

        }
    }
}