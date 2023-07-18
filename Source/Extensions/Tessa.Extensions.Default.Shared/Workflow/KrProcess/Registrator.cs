using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrCreateBasedOnHandler, KrCreateBasedOnHandler>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}