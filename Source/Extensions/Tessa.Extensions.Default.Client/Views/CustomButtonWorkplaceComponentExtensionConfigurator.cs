using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="CustomButtonWorkplaceComponentExtension"/>.
    /// </summary>
    public sealed class CustomButtonWorkplaceComponentExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$CustomButtonWorkplaceComponentExtension_Description";
        private const string NameLocalization = null;

        public CustomButtonWorkplaceComponentExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
