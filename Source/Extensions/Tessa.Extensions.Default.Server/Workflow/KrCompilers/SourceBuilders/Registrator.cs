using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrSourceBuilderFactory, KrSourceBuilderFactory>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}