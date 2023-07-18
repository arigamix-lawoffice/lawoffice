namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Вспомогательные методы и константы для генерации PDF.
    /// </summary>
    public static class PdfHelper
    {
        #region Constants

        /// <summary>
        /// Поле "Author" для документов PDF, которые были сгенерированы в Tessa.
        /// По этому полю можно отличить такие документы от прочих документов, загруженных в систему из внешнего источника.
        /// </summary>
        public const string TessaGeneratedPdfAuthor = "ARIGAMIX";

        #endregion
    }
}
