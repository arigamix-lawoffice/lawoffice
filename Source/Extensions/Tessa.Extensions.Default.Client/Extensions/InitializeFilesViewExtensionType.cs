using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Scheme;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Editors;
using Tessa.UI.Cards.Extensions;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Extensions
{
    public sealed class InitializeFilesViewExtensionType :
        TypeExtensionTypeBase
    {
        #region Constants

        private const string CaptionText = "$UI_Cards_TypesEditor_InitializeFilesView";

        #endregion

        #region Private Fields

        private readonly ICardMetadata cardMetadata;
        private readonly ICardDialogManager cardDialogManager;
        private readonly ICardSchemeInfoProvider cardSchemeInfoProvider;

        #endregion

        #region Constructor

        public InitializeFilesViewExtensionType(
            ICardMetadata cardMetadata,
            ICardDialogManager cardDialogManager,
            ICardSchemeInfoProvider cardSchemeInfoProvider)
        {
            this.cardMetadata = cardMetadata;
            this.cardDialogManager = cardDialogManager;
            this.cardSchemeInfoProvider = cardSchemeInfoProvider;
        }

        #endregion

        #region ITypeExtensionType Members

        /// <doc path='info[@type="ITypeExtensionType" and @item="Caption"]'/>
        public override string Caption => CaptionText;

        /// <doc path='info[@type="ITypeExtensionType" and @item="CreateEditorAsync"]'/>
        public override async ValueTask<IEditorViewModel> CreateEditorCoreAsync(
            CardTypeExtension extension,
            CardType type,
            ICardUIResolver cardUIResolver,
            ICardSchemeInfoProvider cardSchemeInfoProvider,
            CancellationToken cancellationToken = default)
        {
            ISerializableObject settings = extension.ExtensionSettings;
            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.FilesViewAlias))
            {
                settings.Add(DefaultCardTypeExtensionSettings.FilesViewAlias, null);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.CategoriesViewAlias))
            {
                settings.Add(DefaultCardTypeExtensionSettings.CategoriesViewAlias, "FileCategoriesFiltered");
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.PreviewControlName))
            {
                settings.Add(DefaultCardTypeExtensionSettings.PreviewControlName, null);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.DefaultGroup))
            {
                settings.Add(DefaultCardTypeExtensionSettings.DefaultGroup, null);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.IsCategoriesEnabled))
            {
                settings.Add(DefaultCardTypeExtensionSettings.IsCategoriesEnabled, BooleanBoxes.False);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.IsManualCategoriesCreationDisabled))
            {
                settings.Add(DefaultCardTypeExtensionSettings.IsManualCategoriesCreationDisabled, BooleanBoxes.False);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.IsNullCategoryCreationDisabled))
            {
                settings.Add(DefaultCardTypeExtensionSettings.IsNullCategoryCreationDisabled, BooleanBoxes.False);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.IsIgnoreExistingCategories))
            {
                settings.Add(DefaultCardTypeExtensionSettings.IsIgnoreExistingCategories, BooleanBoxes.False);
            }

            if (!settings.ContainsKey(DefaultCardTypeExtensionSettings.CategoriesViewMapping))
            {
                settings.Add(DefaultCardTypeExtensionSettings.CategoriesViewMapping, null);
            }

            static string GetCaptionAsync(ISerializableObject settings)
            {
                string viewControlAlias = string.Empty;
                if (settings.ContainsKey(DefaultCardTypeExtensionSettings.FilesViewAlias))
                {
                    viewControlAlias = $" \"{settings.TryGet<string>(DefaultCardTypeExtensionSettings.FilesViewAlias)}\"";
                }

                return LocalizationManager.Localize(CaptionText) + viewControlAlias;
            }

            return await PropertyGrid.CreateEditorAsync(
                () => GetCaptionAsync(settings),
                cancellationToken,
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_ViewControlAlias",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.FilesViewAlias)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_CategoriesView",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.CategoriesViewAlias)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_FilePreviewControlAlias",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.PreviewControlName)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_Grouping",
                    PropertyGridTypes.CreateString(settings, DefaultCardTypeExtensionSettings.DefaultGroup),
                    string.Format(
                            "{1} - " + LocalizationManager.GetString("UI_Cards_TypesEditor_CategoryGrouping_Tooltip") + "{0}" +
                            "{2} - " + LocalizationManager.GetString("UI_Cards_TypesEditor_CopyGrouping_Tooltip"),
                            Environment.NewLine, FileGroupingNames.Category, FileGroupingNames.Copy)),
                new PropertyGridItem(
                        "$UI_Cards_TypesEditor_CategoriesViewMapping",
                        PropertyGridTypes.CreateViewMap(
                            settings,
                            DefaultCardTypeExtensionSettings.CategoriesViewMapping,
                            type,
                            null,
                            this.cardMetadata,
                            this.cardDialogManager,
                            this.cardSchemeInfoProvider)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_UseCategories",
                    PropertyGridTypes.CreateBool(settings, DefaultCardTypeExtensionSettings.IsCategoriesEnabled)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_CreateCategoriesDisabled",
                    PropertyGridTypes.CreateBool(settings, DefaultCardTypeExtensionSettings.IsManualCategoriesCreationDisabled)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_CreateNullCategoryDisabled",
                    PropertyGridTypes.CreateBool(settings, DefaultCardTypeExtensionSettings.IsNullCategoryCreationDisabled)),
                new PropertyGridItem(
                    "$UI_Cards_TypesEditor_IsIgnoreExistingCategories",
                    PropertyGridTypes.CreateBool(settings, DefaultCardTypeExtensionSettings.IsIgnoreExistingCategories))
                );
        }

        #endregion
    }
}
