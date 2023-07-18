using Tessa.UI.Files;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    /// <summary>
    /// Регистрация расширений для внешних файлов.
    /// </summary>
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ExternalFilesFileControlExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IFileControlExtension, ExternalFilesFileControlExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<IFileExtension, ExternalFileExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                ;
        }
    }
}
