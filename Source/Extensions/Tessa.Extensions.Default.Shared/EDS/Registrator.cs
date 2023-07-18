using Tessa.Platform.EDS;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.EDS
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ICAdESManager, DefaultEDSManager>(nameof(DefaultEDSManager), new ContainerControlledLifetimeManager())
                .RegisterFactory<ICAdESManager>(
                    c => c.Resolve<ICAdESManager>(nameof(DefaultEDSManager)),
                    new ContainerControlledLifetimeManager())
                .RegisterFactory<IEDSManager>(
                    c => c.Resolve<ICAdESManager>(nameof(DefaultEDSManager)),
                    new ContainerControlledLifetimeManager())
                ;
        }
    }
}