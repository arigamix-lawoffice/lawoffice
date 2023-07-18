using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="CustomFolderViewExtension"/>.
    /// </summary>
    public sealed class CustomFolderViewExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$CustomFolderViewExtension_Description";
        private const string NameLocalization = null;

        public CustomFolderViewExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
