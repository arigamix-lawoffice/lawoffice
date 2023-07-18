using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    [Registrator(Order = 2)]
    public sealed class SecondRegistrator :
        RegistratorBase
    {
        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrProcessLauncher, KrProcessUILauncher>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
