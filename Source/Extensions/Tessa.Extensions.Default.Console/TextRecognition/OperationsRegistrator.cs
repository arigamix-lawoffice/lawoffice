#nullable enable

using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.TextRecognition
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.ConsoleClient)]
    public sealed class ClientOperationRegistrator : RegistratorBase
    {
        public override void RegisterUnity() => this.UnityContainer
            .RegisterType<Synchronous.Operation>(new ContainerControlledLifetimeManager())
            .RegisterType<Asynchronous.Operation>(new ContainerControlledLifetimeManager());
    }
}
