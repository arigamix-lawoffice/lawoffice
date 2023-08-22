using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Client.Views
{
    /// <summary>
    /// <see cref="LawCreateCaseViewExtension"/> extension configurator.
    /// </summary>
    public sealed class LawCreateCaseViewExtensionConfigurator : ExtensionSettingsConfiguratorBase
    {
        /// <summary>
        /// Локализованное описание.
        /// </summary>
        private const string DescriptionLocalization = "Case view extension";

        /// <summary>
        /// Локализованное наименование.
        /// </summary>
        private const string NameLocalization = null;

        public LawCreateCaseViewExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization) { }
    }
}
