using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Console.ImportSearchQueries
{
    /// <summary>
    /// Регистратор для команды импорта поисковых запросов.
    /// </summary>
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.ConsoleClient)]
    public sealed class OperationRegistrator :
        RegistratorBase
    {
        /// <inheritdoc />
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<Operation>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}