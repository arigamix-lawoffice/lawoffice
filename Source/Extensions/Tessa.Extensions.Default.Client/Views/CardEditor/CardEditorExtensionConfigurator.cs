using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views.CardEditor
{
    /// <summary>
    /// Конфигуратор расширения <see cref="CardEditorExtension"/>.
    /// </summary>

    public sealed class CardEditorExtensionConfigurator :
        ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$CardEditorExtension_Description";

        private const string NameLocalization = null;

        public CardEditorExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
