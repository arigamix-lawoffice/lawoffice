namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Имена стандартных конвертеров <see cref="Tessa.FileConverters.IFileConverterWorker"/>,
    /// которые используются в других конвертерах.
    /// </summary>
    public static class FileConverterWorkerNames
    {
        #region Constants

        /// <summary>
        /// Конвертер из TIFF в PDF, который используется в <see cref="PdfFileConverterWorker"/> или его наследниках.
        /// </summary>
        public const string TiffToPdf = "TiffToPdf";

        /// <summary>
        /// Конвертер из HTML в PDF, который используется в <see cref="PdfFileConverterWorker"/> или его наследниках.
        /// </summary>
        public const string HtmlToPdf = "HtmlToPdf";

        /// <summary>
        /// Конвертер из HTML в PDF, который используется в <see cref="OnlyOfficeServiceConverter"/> или его наследниках.
        /// </summary>
        public const string OnlyOfficeServiceToPdf = "OnlyOfficeServiceToPdf";

        /// <summary>
        /// Конвертер из HTML в PDF, который используется в <see cref="OnlyOfficeDocumentBuilderConverter"/> или его наследниках.
        /// </summary>
        public const string OnlyOfficeDocumentBuilderToPdf = "OnlyOfficeDocumentBuilderToPdf";

        #endregion
    }
}
