using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Documents
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardUIExtension, HideEmptyIncomingReferencesControl>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                    ;
        }
    }
}
