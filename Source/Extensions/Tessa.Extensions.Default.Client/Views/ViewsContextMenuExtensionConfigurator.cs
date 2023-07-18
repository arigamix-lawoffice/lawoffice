using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="ViewsContextMenuExtension"/>.
    /// </summary>
    public sealed class ViewsContextMenuExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$ViewsContextMenuExtension_Description";
        private const string NameLocalization = null;

        public ViewsContextMenuExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
