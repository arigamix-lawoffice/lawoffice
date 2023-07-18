using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using Tessa.Exchange.WebServices.Data;
using Tessa.Extensions.Default.Chronos.Notices;
using Tessa.Extensions.Default.Server.Notices;
using Tessa.Extensions.Default.Server.Workflow;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Task = System.Threading.Tasks.Task;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public sealed class ExchangeMailReceiver :
        IMailReceiver,
        IExchangeSettingsContainer
    {
        #region Fields

        private readonly Func<IMessageProcessor> getProcessorFunc;

        private ExchangeService service;

        private ExchangeSettings settings;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public ExchangeMailReceiver(Func<IMessageProcessor> getProcessorFunc)
        {
            // получаем функцию Func<IMessageProcessor>, чтобы зависимости Unity (всякие кэши метаинфы)
            // не инициализировались раньше времени
            this.getProcessorFunc = getProcessorFunc;
        }

        #endregion

        #region Properties

        public bool StopRequested => this.StopRequestedFunc?.Invoke() == true;

        #endregion

        #region Private Methods

        private static string ExtractPlainText(MessageBody body) =>
            body.BodyType switch
            {
                BodyType.HTML => FormattingHelper.ExtractPlainTextFromHtml(body.Text),
                BodyType.Text => body.Text,
                _ => throw new ArgumentOutOfRangeException()
            };

        #endregion

        #region IMailReceiver Methods

        /// <inheritdoc />
        public Func<bool> StopRequestedFunc { get; set; }


        /// <inheritdoc />
        public async Task ReceiveMessagesAsync(CancellationToken cancellationToken = default)
        {
            if (this.service is null)
            {
                logger.Trace("Initializing exchange service.");
                this.service = await ExchangeServiceHelper.CreateExchangeServiceAsync(this.settings, cancellationToken);
            }

            Folder incomingFolder = await Folder.Bind(this.service, WellKnownFolderName.Inbox, cancellationToken);

            if (incomingFolder is null || incomingFolder.TotalCount == 0)
            {
                logger.Trace("There are no messages to process.");
                return;
            }

            if (this.StopRequested)
            {
                return;
            }

            ItemView itemView = new ItemView(incomingFolder.TotalCount);
            itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);

            FindItemsResults<Item> items = await incomingFolder.FindItems(itemView, cancellationToken);

            if (this.StopRequested)
            {
                return;
            }

            PropertySet itemPropertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.Attachments);

            bool askTextBody = this.service.RequestedServerVersion >= ExchangeVersion.Exchange2013;
            if (askTextBody)
            {
                itemPropertySet.Add(ItemSchema.TextBody);
            }

            // requesting HTML by default, if we want plain text:
            // itemPropertySet.RequestedBodyType = BodyType.Text;

            itemView.PropertySet = itemPropertySet;

            IMessageProcessor processor = null;
            foreach (Item item in items)
            {
                if (this.StopRequested)
                {
                    return;
                }

                logger.Trace("Message is loading. Subject: \"{0}\".", item.Subject);
                if (item is not EmailMessage message)
                {
                    continue;
                }

                try
                {
                    await message.Load(itemPropertySet, cancellationToken);
                    logger.Trace("Message is loaded. From: \"{0}\", Subject: \"{1}\".", message.From, message.Subject);

                    logger.Trace("Finding user and setting user session.");

                    string body;
                    string htmlBody = message.Body.BodyType is BodyType.HTML ? message.Body.Text : null;

                    if (askTextBody)
                    {
                        try
                        {
                            body = message.TextBody.Text.Trim();
                        }
                        catch (Exception ex)
                        {
                            logger.LogException(ex, LogLevel.Warn);
                            body = ExtractPlainText(message.Body);
                        }
                    }
                    else
                    {
                        body = ExtractPlainText(message.Body);
                    }

                    var attachments = new List<NoticeAttachment>();

                    foreach (var attachment in message.Attachments)
                    {
                        if (attachment is FileAttachment fileAttachment)
                        {
                            await fileAttachment.Load(cancellationToken);

                            attachments.Add(
                                new NoticeAttachment
                                {
                                    Name = string.IsNullOrEmpty(fileAttachment.FileName)
                                        ? fileAttachment.Name
                                        : fileAttachment.FileName,
                                    Data = fileAttachment.Content,
                                });
                        }
                    }

                    processor ??= this.getProcessorFunc();

                    await processor.ProcessMessageAsync(
                        new NoticeMessage
                        {
                            From = message.From.Address,
                            Subject = message.Subject,
                            Body = body,
                            HtmlBody = htmlBody,
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
                    logger.LogException($"Failed to process message. From: \"{message.From.Address}\", Subject: \"{message.Subject}\".", ex);
                }
                finally
                {
                    try
                    {
                        await message.Delete(DeleteMode.HardDelete);
                        logger.Trace("Message is deleted. From: \"{0}\", Subject: \"{1}\".", message.From.Address, message.Subject);
                    }
                    catch (Exception ex)
                    {
                        logger.LogException($"Failed to delete message. From: \"{message.From.Address}\", Subject: \"{message.Subject}\".", ex);
                    }
                }
            }
        }

        #endregion

        #region IExchangeSettingsContainer Members

        /// <inheritdoc />
        public ExchangeSettings ExchangeSettings
        {
            get => this.settings;
            set
            {
                this.settings = value;
                this.service = null;
            }
        }

        #endregion
    }
}
