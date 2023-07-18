using Tessa.Cards;
using Tessa.TextRecognition.Constants;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.TextRecognition
{
    /// <summary>
    /// Регистрация расширений для UI карточки.
    /// </summary>
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer) => extensionContainer
            .RegisterExtension<ICardUIExtension, OcrSettingsUIExtension>(x => x
                .WithOrder(ExtensionStage.AfterPlatform)
                .WithSingleton()
                .WhenCardTypes(OcrCardTypes.OcrSettingsTypeID));
    }
}
