using Tessa.Web.Client.Services;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Web.Services
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ISamlService, SamlService>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
