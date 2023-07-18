#nullable enable

using Tessa.UI.Views.Extensions;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Конфигуратор расширения <see cref="TagCardsViewExtension"/>.
    /// </summary>
    public sealed class TagCardsViewExtensionConfigurator :
        ExtensionSettingsConfiguratorBase
    {
        #region Constants And Static Fields

        private const string DescriptionLocalization = "$TagCardsViewExtension_Decription";

        private const string NameLocalization = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TagCardsViewExtensionConfigurator"/>.
        /// </summary>
        public TagCardsViewExtensionConfigurator()
            : base(ViewExtensionConfiguratorType.None, NameLocalization, DescriptionLocalization)
        {
        }

        #endregion
    }
}
