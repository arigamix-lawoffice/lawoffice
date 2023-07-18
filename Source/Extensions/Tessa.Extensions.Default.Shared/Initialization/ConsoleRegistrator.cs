using Tessa.Platform.Initialization;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Initialization
{
    /// <summary>
    /// Выполняет регистрацию только для консольного приложения.
    /// </summary>
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.ConsoleClient)]
    public sealed class ConsoleRegistrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrClientAndConsoleInitializationExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IClientInitializationExtension, KrClientAndConsoleInitializationExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenApplications(ApplicationIdentifiers.TessaAdminConsole))
                ;
        }
    }
}
