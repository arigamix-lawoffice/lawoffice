namespace Tessa.Extensions.Default.Server.Notices
{
    public class NoticeAttachment
    {
        /// <summary>
        /// Имя файла, приложенного к письму
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Данные файла
        /// </summary>
        public byte[] Data { get; set; }
    }
}
