namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Типы файлов, используемые только в типовом решении.
    /// </summary>
    public static class DefaultFileTypes
    {
        #region Constants

        /// <summary>
        /// Имя виртуального типа файла для листа согласования, используемого в типовом процессе согласования.
        /// </summary>
        [System.Obsolete]
        public const string KrApprovalList = "KrApprovalList";

        /// <summary>
        /// Имя виртуального типа файла, который размещается в папке на сервере.
        /// </summary>
        public const string ServerPath = "ServerPath";

        /// <summary>
        /// Имя виртуального типа файла, данные которого размещаются в запросе на получение контента.
        /// Это используется для прикладывания файлов к письмам с заранее известным контентом.
        /// </summary>
        public const string EmbeddedData = "EmbeddedData";

        /// <summary>
        /// Имя виртуального типа файла, который используется для получения штрих-кода
        /// </summary>
        public const string Barcode = "Barcode";

        /// <summary>
        /// Имя виртуального типа файла, который используется для подсистемы виртуальных файлов
        /// </summary>
        public const string KrVirtualFile = "KrVirtualFileType";
        #endregion
    }
}
