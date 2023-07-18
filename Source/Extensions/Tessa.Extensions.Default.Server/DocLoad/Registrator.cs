using Tessa.Extensions.Platform.Server.DocLoad;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.DocLoad
{
    [Registrator]
    public class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IBarcodeConverter, BarcodeConverter>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}