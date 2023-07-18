using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MimeKit.Text;
using NLog;
using Tessa.Extensions.Default.Server.Notices;
using Tessa.Extensions.Default.Server.Workflow;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Platform.IO;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public sealed class ImapMailReceiver :
        IMailReceiver,
        IPop3ImapSettingsContainer
    {
        #region Fields

        private readonly Func<IMessageProcessor> getProcessorFunc;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public ImapMailReceiver(Func<IMessageProcessor> getProcessorFunc)
        {
            // получаем функцию Func<IMessageProcessor>, чтобы зависимости Unity (всякие кэши метаинфы)
            // не инициализировались раньше времени
            this.getProcessorFunc = getProcessorFunc;
        }

        #endregion

        #region Properties

        public bool StopRequested => this.StopRequestedFunc?.Invoke() == true;

        #endregion

        #region IMailReceiver Members

        /// <inheritdoc />
        public Func<bool> StopRequestedFunc { get; set; }


        /// <inheritdoc />
        public async Task ReceiveMessagesAsync(CancellationToken cancellationToken = default)
        {
            logger.Trace("Loading messages.");

            using var client = new ImapClient();

            // ReSharper disable PossibleInvalidOperationException
            await client.ConnectAsync(this.Pop3ImapSettings.Host, (int) this.Pop3ImapSettings.Port, this.Pop3ImapSettings.UseSsl ?? false, cancellationToken);
            // ReSharper restore PossibleInvalidOperationException

            try
            {
                // Authenticate ourselves towards the server
                await client.AuthenticateAsync(this.Pop3ImapSettings.User, this.Pop3ImapSettings.Password, cancellationToken);

                if (this.StopRequested)
                {
                    return;
                }

                // Get the number of messages in the inbox
                IMailFolder inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);

                IList<UniqueId> uids = await inbox.SearchAsync(SearchQuery.NotDeleted, cancellationToken);
                int messageCount = uids.Count;

                if (messageCount == 0)
                {
                    logger.Trace("There are no messages to process.");
                    return;
                }

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                IMessageProcessor processor = null;
                for (int messageIndex = 0; messageIndex < messageCount; messageIndex++)
                {
                    if (this.StopRequested)
                    {
                        return;
                    }

                    UniqueId uid = uids[messageIndex];
                    MimeMessage message = await inbox.GetMessageAsync(uid, cancellationToken);

                    string from = message.From.Mailboxes.FirstOrDefault()?.Address;
                    string subject = message.Subject ?? string.Empty;
                    logger.Trace("Message loaded. From: \"{0}\", Subject: \"{1}\".", from, subject);

                    if (this.StopRequested)
                    {
                        return;
                    }

                    try
                    {
                        var htmlText = message.GetTextBody(TextFormat.Html);
                        var plainText = message.GetTextBody(TextFormat.Plain);

                        var messageText = plainText
                            ?? (htmlText is null ? null : FormattingHelper.ExtractPlainTextFromHtml(htmlText))
                            ?? string.Empty;

                        logger.Trace("Finding user and setting user session.");

                        processor ??= this.getProcessorFunc();

                        var attachments = new List<NoticeAttachment>();
                        foreach (MimeEntity entity in message.Attachments)
                        {
                            if (entity is MimePart part)
                            {
                                await using MemoryStream stream = StreamHelper.AcquireMemoryStream(capacity: StreamHelper.MaxCachedSize);
                                await part.Content.DecodeToAsync(stream, cancellationToken);
                                attachments.Add(new NoticeAttachment { Name = part.FileName, Data = stream.ToArray() });
                            }
                        }

                        await processor.ProcessMessageAsync(
                            new NoticeMessage
                            {
                                From = from,
                                Subject = subject,
                                Body = messageText,
                                HtmlBody = htmlText,
                                OriginalMessage = message,
                                Attachments = attachments.ToArray(),
                            },
                            cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        logger.LogException($"Failed to process message. From: \"{from}\", Subject: \"{subject}\".", ex);
                    }
                    finally
                    {
                        try
                        {
                            await inbox.AddFlagsAsync(uid, MessageFlags.Deleted, true, cancellationToken);
                            logger.Trace("Message is deleted. From: \"{0}\", Subject: \"{1}\".", from, subject);
                        }
                        catch (Exception ex)
                        {
                            logger.LogException($"Failed to delete message. From: \"{from}\", Subject: \"{subject}\".", ex);
                        }
                    }
                }
            }
            finally
            {
                await client.DisconnectAsync(true, cancellationToken);
            }
        }

        #endregion

        #region IPop3ImapSettingsContainer Members

        /// <inheritdoc />
        public Pop3ImapSettings Pop3ImapSettings { get; set; }

        #endregion
    }
}
