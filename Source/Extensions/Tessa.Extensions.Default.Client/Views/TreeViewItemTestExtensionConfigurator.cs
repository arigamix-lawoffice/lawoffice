using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="TreeViewItemTestExtension"/>.
    /// </summary>
    public sealed class TreeViewItemTestExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$TreeViewItemTestExtension_Description";
        private const string NameLocalization = null;

        public TreeViewItemTestExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
