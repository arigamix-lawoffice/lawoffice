using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="CustomNavigationViewExtension"/>.
    /// </summary>
    public sealed class CustomNavigationViewExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$CustomNavigationViewExtension_Description";
        private const string NameLocalization = null;

        public CustomNavigationViewExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
