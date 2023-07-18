using Tessa.Platform.Plugins;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Chronos.OnlyOffice
{
    [Registrator(Tag = RegistratorTag.ServerPlugin)]
    public sealed class Registrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<OnlyOfficeRemoveFileCacheInfoPlugin>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IPluginExtension, OnlyOfficeRemoveFileCacheInfoPlugin>(x => x
                    .WithOrder(ExtensionStage.Platform, 1)
                    .WithUnity(this.UnityContainer)
                    .WithScheduling(PluginSchedulingMode.Daily))
                ;
        }
    }
}