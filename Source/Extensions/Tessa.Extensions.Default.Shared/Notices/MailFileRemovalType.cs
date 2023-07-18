namespace Tessa.Extensions.Default.Shared.Notices
{
    /// <summary>
    /// Тип удаления файла после успешной отправки письма.
    /// </summary>
    public enum MailFileRemovalType
    {
        /// <summary>
        /// Файл не удаляется.
        /// </summary>
        KeepFile,

        /// <summary>
        /// Удаляется файл, но не папка, в которой расположен файл.
        /// </summary>
        RemoveFileOnly,

        /// <summary>
        /// Удаляется файл и папка, в которой он расположен, если папка будет пуста после удаления файла.
        /// </summary>
        RemoveFileAndFolder,
    }
}
