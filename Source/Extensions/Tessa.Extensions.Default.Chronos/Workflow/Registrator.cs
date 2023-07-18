using Tessa.Extensions.Default.Server.Notices;
using Tessa.Extensions.Default.Server.Workflow;
using Tessa.Platform.Plugins;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    [Registrator(Tag = RegistratorTag.ServerPlugin)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterNoticesMessageProcessor()

                .RegisterType<IMailReceiver, ExchangeMailReceiver>(MailReceiverNames.ExchangeMailReceiver, new PerResolveLifetimeManager())
                .RegisterType<IMailReceiver, Pop3MailReceiver>(MailReceiverNames.Pop3MailReceiver, new PerResolveLifetimeManager())
                .RegisterType<IMailReceiver, ImapMailReceiver>(MailReceiverNames.ImapMailReceiver, new PerResolveLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IPluginExtension, KrAutoApprovePlugin>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WithScheduling(PluginSchedulingMode.Normal))

                .RegisterExtension<IPluginExtension, ReturnTasksFromPostponedPlugin>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WithScheduling(PluginSchedulingMode.Normal))

                // "Often, AfterPlatform, 1" - это Tessa.Extensions.Default.Chronos.Notices.NoticeMailerPlugin

                .RegisterExtension<IPluginExtension, MobileApprovalPlugin>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WithScheduling(PluginSchedulingMode.Often))
                ;
        }
    }
}
