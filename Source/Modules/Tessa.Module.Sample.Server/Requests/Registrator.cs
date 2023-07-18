using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions;
using Tessa.Module.Sample.Shared;

namespace Tessa.Module.Sample.Server.Requests
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardRequestExtension, SampleActionRequestExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenRequestTypes(RequestTypes.SampleAction))
                ;
        }
    }
}
