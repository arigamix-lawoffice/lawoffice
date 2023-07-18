namespace Tessa.Extensions.Default.Shared.Views
{
    /// <summary>
    /// Режим открытия созданной карточки.
    /// </summary>
    public enum CardOpeningKind
    {
        /// <summary>
        /// Созданная карточка открывается во вкладке основного окна приложения. Это режим по умолчанию. 
        /// </summary>
        ApplicationTab = 0,
        
        /// <summary>
        /// Созданная карточка открывается в модальном диалоге.
        /// </summary>
        ModalDialog = 1,
    }
}