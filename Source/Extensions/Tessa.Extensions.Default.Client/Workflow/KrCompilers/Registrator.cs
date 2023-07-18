using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Client.Workflow.KrCompilers
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardStoreExtension, KrReplaceRecalcMessageStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform)
                    .WithUnity(this.UnityContainer))
                ;
        }


    }
}