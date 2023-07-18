using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.ImportScheme
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.ConsoleClient)]
    public sealed class OperationRegistrator :
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