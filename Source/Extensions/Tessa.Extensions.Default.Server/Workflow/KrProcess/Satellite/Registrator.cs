using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Platform.Server.Cards.Satellites;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Satellite
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrSatelliteHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSecondarySatelliteHandler>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            var registry = this.UnityContainer.Resolve<ISatelliteTypeRegistry>();
            registry.Register(new SatelliteTypeDescriptor(DefaultCardTypes.KrSatelliteTypeID)
            {
                HandlerType = typeof(KrSatelliteHandler),
                IsSingleton = true,
                IgnoreGetPrepare = true,
                IgnoreStoreExtensions = true,
                CopySatelliteOnCardCopy = true,
            });

            registry.Register(new SatelliteTypeDescriptor(DefaultCardTypes.KrSecondarySatelliteTypeID)
            {
                HandlerType = typeof(KrSecondarySatelliteHandler),
                IgnoreGetPrepare = true,
                IgnoreStoreExtensions = true,
                CopySatelliteOnCardCopy = true,
            });
        }
    }
}