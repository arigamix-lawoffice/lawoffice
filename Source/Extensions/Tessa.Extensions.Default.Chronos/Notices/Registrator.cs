using Tessa.Platform.Plugins;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    [Registrator(Tag = RegistratorTag.ServerPlugin)]
    public sealed class Registrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ExchangeSender>(new PerResolveLifetimeManager())
                .RegisterType<SmtpSender>(new PerResolveLifetimeManager())
                .RegisterType<IOutboxManager, OutboxManager>(new PerResolveLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IPluginExtension, NoticeMailerPlugin>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WithScheduling(PluginSchedulingMode.Often))
                ;
        }
    }
}
