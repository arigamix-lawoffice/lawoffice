#nullable enable

using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="TagsWorkplaceViewDemoActionExtension"/>.
    /// </summary>
    public sealed class TagsWorkplaceViewDemoActionExtensionConfigurator :
        ExtensionSettingsConfiguratorBase
    {
        #region Constants And Static Fields

        private const string DescriptionLocalization = "$TagsWorkplaceViewDemoActionExtension_Description";

        private const string NameLocalization = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TagsWorkplaceViewDemoActionExtensionConfigurator"/>.
        /// </summary>
        public TagsWorkplaceViewDemoActionExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }

        #endregion
    }
}
