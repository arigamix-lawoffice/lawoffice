#nullable enable

using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.Maintenance
{
    public sealed class OperationRegistrator:
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<Operation>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
