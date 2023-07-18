using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="GetDataWithDelayExtension"/>.
    /// </summary>
    public sealed class GetDataWithDelayExtensionConfigurator
        : ExtensionSettingsConfiguratorBase
    {
        private const string DescriptionLocalization = "$GetDataWithDelayExtension_Description";
        private const string NameLocalization = null;

        public GetDataWithDelayExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }
    }
}
