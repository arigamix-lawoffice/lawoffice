using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <summary>
    /// Конфигуратор расширения <see cref="OpenForumContextMenuViewExtension"/>.
    /// </summary>
    public sealed class OpenForumContextMenuViewExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$OpenForumContextMenuViewExtension_Description";
        private const string NameLocalization = null;

        public OpenForumContextMenuViewExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
