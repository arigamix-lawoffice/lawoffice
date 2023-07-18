using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Editors;
using Tessa.UI.Cards.Extensions;

namespace Tessa.Extensions.Default.Client.Extensions
{
    /// <summary>
    /// Настройки типа расширения для добавления возможности
    /// открытия карточки из представления.
    /// </summary>
    public sealed class OpenCardInViewExtensionType
        : TypeExtensionTypeBase
    {
        #region Constants

        private const string CaptionText = "$UI_Cards_TypesEditor_OpenCardInView";

        #endregion

        #region ITypeExtensionType Members

        /// <inheritdoc/>
        public override string Caption => CaptionText;

        /// <inheritdoc/>
        public override async ValueTask<IEditorViewModel> CreateEditorCoreAsync(
            CardTypeExtension extension,
            CardType type,
            ICardUIResolver cardUIResolver, 
            ICardSchemeInfoProvider cardSchemeInfoProvider, 
            CancellationToken cancellationToken = default)
        {
            // получаем блок настроек расширения
            ISerializableObject settings = extension.ExtensionSettings;

            // создаём значения по умолчанию, если их нет
            // алиас контрола представления (обязательный)
            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.ViewControlAlias))
            {
                settings.Add(DefaultCardTypeExtensionSettings.ViewControlAlias, null);
            }

            // префикс референса
            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.ViewReferencePrefix))
            {
                settings.Add(DefaultCardTypeExtensionSettings.ViewReferencePrefix, null);
            }

            // открывать в диалоге, по умолчанию - нет
            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.IsOpenCardInDialog))
            {
                settings.Add(DefaultCardTypeExtensionSettings.IsOpenCardInDialog, BooleanBoxes.False);
            }

            // заголовок диалога, по умолчанию - отсутствует.
            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.CardDialogName))
            {
                settings.Add(DefaultCardTypeExtensionSettings.CardDialogName, null);
            }

            // создаём редактор, и возвращаем его
            return await PropertyGrid.CreateEditorAsync(
                () => GetCaption(settings),
                cancellationToken,
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_ViewControlAlias",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.ViewControlAlias),
                    "$UI_Cards_TypesEditor_ViewControlAlias_ExtensionToolTip"),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_ReferencePrefix",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.ViewReferencePrefix),
                    "$UI_Cards_TypesEditor_ReferencePrefix_Tooltip"),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_DialogName",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.CardDialogName),
                    "$UI_Cards_TypesEditor_DialogName_ExtensionToolTip"),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_OpenInDialog",
                    PropertyGridTypes.CreateBool(settings, DefaultCardTypeExtensionSettings.IsOpenCardInDialog),
                    "$UI_Cards_TypesEditor_OpenInDialog_ExtensionToolTip"));
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Создает название расширение, добавляя к стандартному названию алиас представления.
        /// </summary>
        /// <param name="settings">Настройки расширения.</param>
        /// <returns>Заголовок расширения.</returns>
        private static string GetCaption(ISerializableObject settings)
        {
            string viewControlAlias = string.Empty;
            if (settings.ContainsKey(CardTypeExtensionSettings.ViewControlAlias))
            {
                viewControlAlias = $" \"{settings.TryGet<string>(CardTypeExtensionSettings.ViewControlAlias)}\"";
            }

            return LocalizationManager.Localize(CaptionText) + viewControlAlias;
        }

        #endregion
    }
}
