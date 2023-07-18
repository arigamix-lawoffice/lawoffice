using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="RecordViewExtension"/>.
    /// </summary>
    public sealed class RecordViewExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$RecordViewExtension_Description";
        private const string NameLocalization = null;

        public RecordViewExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
