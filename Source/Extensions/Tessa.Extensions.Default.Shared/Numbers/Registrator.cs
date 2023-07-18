using Tessa.Cards.Numbers;
using Tessa.Platform;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Numbers
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<DocumentNumberDirector>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<INumberDirectorContainer>()
                ?
                .Register(getDirectorFunc: c => c.Resolve<DocumentNumberDirector>())
                ;
        }
    }
}
