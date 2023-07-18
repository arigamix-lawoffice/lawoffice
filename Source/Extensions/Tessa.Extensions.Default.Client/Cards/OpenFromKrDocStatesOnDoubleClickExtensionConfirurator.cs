using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Cards
{
    /// <summary>
    /// Конфигуратор расширения <see cref="OpenFromKrDocStatesOnDoubleClickExtension"/>.
    /// </summary>
    public sealed class OpenFromKrDocStatesOnDoubleClickExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$OpenFromKrDocStatesOnDoubleClickExtension_Description";
        private const string NameLocalization = null;

        public OpenFromKrDocStatesOnDoubleClickExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
