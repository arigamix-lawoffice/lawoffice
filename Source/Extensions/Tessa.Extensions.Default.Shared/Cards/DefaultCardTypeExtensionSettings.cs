namespace Tessa.Extensions.Default.Shared.Cards
{
    /// <summary>
    /// Настройки типовых расширений для типов карточек.
    /// </summary>
    public static class DefaultCardTypeExtensionSettings
    {
        #region Common fields

        /// <summary>
        /// Псевдоним (алиас) контрола представления.
        /// </summary>
        public static readonly string ViewControlAlias = nameof(ViewControlAlias);

        /// <summary>
        /// Префикс референса. Используется для определения референса.
        /// </summary>
        public static readonly string ViewReferencePrefix = nameof(ViewReferencePrefix);

        #endregion

        #region InitializeFilesView type extension

        public static readonly string FilesViewAlias = nameof(FilesViewAlias);

        public static readonly string CategoriesViewAlias = nameof(CategoriesViewAlias);

        public static readonly string PreviewControlName = nameof(PreviewControlName);

        public static readonly string IsCategoriesEnabled = nameof(IsCategoriesEnabled);

        public static readonly string IsManualCategoriesCreationDisabled = nameof(IsManualCategoriesCreationDisabled);

        public static readonly string IsNullCategoryCreationDisabled = nameof(IsNullCategoryCreationDisabled);

        public static readonly string IsIgnoreExistingCategories = nameof(IsIgnoreExistingCategories);

        public static readonly string DefaultGroup = nameof(DefaultGroup);

        public static readonly string TableControlSettings = nameof(TableControlSettings);

        public static readonly string CategoriesViewMapping = nameof(CategoriesViewMapping);

        #endregion

        #region OpenCardInVew type extension

        /// <summary>
        /// Нужно ли открывать карточку в режиме диалога.
        /// </summary>
        public static readonly string IsOpenCardInDialog = nameof(IsOpenCardInDialog);

        /// <summary>
        /// Имя заголовка диалогового окна, в случае открытия карточки в диалоге.
        /// </summary>
        public static readonly string CardDialogName = nameof(CardDialogName);

        #endregion
    }
}
