using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrProcessStateMachine, KrProcessStateMachine>(new ContainerControlledLifetimeManager())
                .RegisterType<DefaultStateHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<InterruptionStateHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<CancelProcessStateHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<SkipProcessStateHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<TransitionStateHandler>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .Resolve<IKrProcessStateMachine>()
                .RegisterHandler<DefaultStateHandler>(KrConstants.DefaultProcessState)
                .RegisterHandler<InterruptionStateHandler>(KrConstants.InterruptionProcessState)
                .RegisterHandler<CancelProcessStateHandler>(KrConstants.CancelellationProcessState)
                .RegisterHandler<SkipProcessStateHandler>(KrConstants.SkipProcessState)
                .RegisterHandler<TransitionStateHandler>(KrConstants.TransitionProcessState)
                ;
        }
    }
}