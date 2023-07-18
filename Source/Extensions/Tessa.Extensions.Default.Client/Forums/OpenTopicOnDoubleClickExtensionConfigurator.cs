using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <summary>
    /// Конфигуратор расширения <see cref="OpenTopicOnDoubleClickExtension"/>.
    /// </summary>
    public sealed class OpenTopicOnDoubleClickExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$OpenTopicOnDoubleClickExtension_Description";
        private const string NameLocalization = null;

        public OpenTopicOnDoubleClickExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
