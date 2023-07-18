using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Server.LoginProvider
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .RegisterType<ISessionLoginProvider, LawSessionLoginProvider>(new ContainerControlledLifetimeManager());
        }
    }
}
