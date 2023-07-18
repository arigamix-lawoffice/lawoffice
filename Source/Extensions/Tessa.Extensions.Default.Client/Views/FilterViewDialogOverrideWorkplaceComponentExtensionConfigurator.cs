using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="FilterViewDialogOverrideWorkplaceComponentExtension"/>.
    /// </summary>
    public sealed class FilterViewDialogOverrideWorkplaceComponentExtensionConfigurator :
        ExtensionSettingsConfiguratorBase
    {
        #region Constants And Static Fields

        private const string DescriptionLocalization = "$FilterViewDialogOverrideWorkplaceComponentExtension_Description";

        private const string NameLocalization = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FilterViewDialogOverrideWorkplaceComponentExtensionConfigurator"/>
        /// </summary>
        public FilterViewDialogOverrideWorkplaceComponentExtensionConfigurator()
            : base(
                  ViewExtensionConfiguratorType.None,
                  NameLocalization,
                  DescriptionLocalization)
        {
        }

        #endregion
    }
}
