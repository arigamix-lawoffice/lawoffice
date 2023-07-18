namespace Tessa.Extensions.Default.Server.Notices
{
    public class NoticeMessage
    {
        #region Properties

        /// <summary>
        /// Отправитель письма (e-mail).
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма в виде простого текста.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// HTML тело письма или <c>null</c>, если доступно только тело в виде простого текста <see cref="Body"/>.
        /// </summary>
        public string HtmlBody { get; set; }

        /// <summary>
        /// Исходное сообщение в виде объекта. В платформе это <c>MimeKit.MimeMessage</c> для POP3/IMAP,
        /// или <c>Tessa.Exchange.WebServices.Data.EmailMessage</c> для Exchange.
        /// </summary>
        public object OriginalMessage { get; set; }

        /// <summary>
        /// Приложенные к письму файлы.
        /// </summary>
        public NoticeAttachment[] Attachments { get; set; }

        #endregion
    }
}
