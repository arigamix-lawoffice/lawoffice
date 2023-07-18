using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.References
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, AddIncomingReferencesGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithSingleton()
                    .WhenMethod(CardGetMethod.Default))
                ;
        }
    }
}
