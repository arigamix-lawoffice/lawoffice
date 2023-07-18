using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using NLog;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using MailKitSmtpClient = MailKit.Net.Smtp.SmtpClient;
using NetSmtpClient = System.Net.Mail.SmtpClient;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public sealed class SmtpSender : MailSender, IAsyncDisposable
    {
        #region Constructors

        public SmtpSender(
            ICardServerPermissionsProvider permissionsProvider,
            ICardContentStrategy contentStrategy,
            ICardStreamClientRepository clientRepository,
            ICardRepository extendedRepository,
            IDbScope dbScope,
            IOutboxManager outboxManager,
            IFormattingSettingsCache formattingSettingsCache)
            : base(permissionsProvider, contentStrategy, clientRepository, extendedRepository, dbScope, outboxManager, formattingSettingsCache)
        {
        }

        #endregion

        #region Fields

        private NetSmtpClient netSmtpClient;

        private ISmtpClient mailKitSmtpClient;

        private readonly AsyncLock asyncLock = new AsyncLock();

        #endregion

        #region Private Methods

        private async ValueTask<object> EnsureSmtpClientInitializedAsync(CancellationToken cancellationToken = default)
        {
            object client = (object) this.mailKitSmtpClient ?? this.netSmtpClient;
            if (client is null)
            {
                using (await this.asyncLock.EnterAsync(cancellationToken))
                {
                    string pickupDirectoryLocation = NoticeMailerConfig.SmtpPickupDirectoryLocation;
                    if (!string.IsNullOrEmpty(pickupDirectoryLocation))
                    {
                        NetSmtpClient netSmtpClient = CreateNetSmtpClient(pickupDirectoryLocation);

                        this.netSmtpClient = netSmtpClient;
                        client = netSmtpClient;
                    }
                    else
                    {
                        ISmtpClient mailKitSmtpClient = await CreateMailKitSmtpClientAsync(cancellationToken);

                        this.mailKitSmtpClient = mailKitSmtpClient;
                        client = mailKitSmtpClient;
                    }
                }
            }

            return client;
        }

        #endregion

        #region Properties

        public string SmtpFrom { get; set; }

        public string SmtpFromDisplayName { get; set; }

        #endregion

        #region Base Overrides

        protected override async Task<bool> SendMessageAsync(MailSenderMessage message, CancellationToken cancellationToken = default)
        {
            object client = await this.EnsureSmtpClientInitializedAsync(cancellationToken);

            try
            {
                string body = message.Message.Body;

                this.AddMissedFilesInfo(ref body, message.MissedFiles, MissedFilesRegex);
                this.AddMissedFilesInfo(ref body, message.OversizedFiles, OversizedFilesRegex);

                string mainRecipientDisplayName = message.Info.MainRecipientDisplayName;

                switch (client)
                {
                    case NetSmtpClient netSmtpClient:
                        var fromAddress = new MailAddress(
                            this.SmtpFrom,
                            string.IsNullOrWhiteSpace(this.SmtpFromDisplayName) ? null : this.SmtpFromDisplayName,
                            Encoding.UTF8);

                        var toAddress = new MailAddress(
                            message.Message.Email,
                            string.IsNullOrWhiteSpace(mainRecipientDisplayName) ? null : mainRecipientDisplayName,
                            Encoding.UTF8);

                        using (var smtpMessage = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = message.Message.Subject,
                            Body = body,
                            IsBodyHtml = message.Info.Format != MailFormat.PlainText,
                            SubjectEncoding = Encoding.UTF8,
                            BodyEncoding = Encoding.UTF8,
                            BodyTransferEncoding = TransferEncoding.EightBit,
                        })
                        {
                            ListStorage<MailRecipient> recipients = message.Info.TryGetRecipients();
                            if (recipients is { Count: > 0 })
                            {
                                foreach (MailRecipient recipient in recipients)
                                {
                                    string displayName = recipient.DisplayName;

                                    var recipientAddress = new MailAddress(
                                        recipient.Email,
                                        string.IsNullOrWhiteSpace(displayName) ? null : displayName,
                                        Encoding.UTF8);

                                    switch (recipient.Type)
                                    {
                                        case MailRecipientType.To:
                                            smtpMessage.To.Add(recipientAddress);
                                            break;

                                        case MailRecipientType.Cc:
                                            smtpMessage.CC.Add(recipientAddress);
                                            break;

                                        case MailRecipientType.Bcc:
                                            smtpMessage.Bcc.Add(recipientAddress);
                                            break;

                                        default:
                                            throw new ArgumentOutOfRangeException(nameof(MailRecipientType), recipient.Type, null);
                                    }
                                }
                            }

                            foreach (ITempFile file in message.MessageFiles)
                            {
                                try
                                {
                                    var data = new Attachment(file.Path);
                                    smtpMessage.Attachments.Add(data);
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogException(ex, LogLevel.Error);
                                }
                            }

                            Logger.Trace("Sending message ID='{0}'", message.Message.ID);
                            await netSmtpClient.SendMailAsync(smtpMessage, cancellationToken);
                            Logger.Trace("Message was sent, ID='{0}'", message.Message.ID);
                        }

                        break;

                    case ISmtpClient mailKitSmtpClient:
                        var mimeMessage = new MimeMessage
                        {
                            Subject = message.Message.Subject,
                        };

                        var bodyBuilder = new BodyBuilder();
                        if (message.Info.Format != MailFormat.PlainText)
                        {
                            bodyBuilder.HtmlBody = body;
                        }
                        else
                        {
                            bodyBuilder.TextBody = body;
                        }

                        foreach (ITempFile file in message.MessageFiles)
                        {
                            try
                            {
                                MimeEntity attachment = await bodyBuilder.Attachments.AddAsync(file.Path, cancellationToken);

                                // For greater compatibility with Outlook/Exchange clients, we'll have
                                // to specify the filename in Rfc2047 encoding as well. So what we'll
                                // do is this: for Content-Disposition we'll follow Rfc2231, and for
                                // Content-Type we'll (incorrectly) follow Rfc2047.
                                // https://github.com/jstedfast/MimeKit/issues/585

                                if (!string.IsNullOrEmpty(attachment.ContentType.Name))
                                {
                                    attachment.ContentDisposition.FileName = attachment.ContentType.Name;
                                    if (attachment.ContentType.Parameters.TryGetValue("name", out Parameter name))
                                    {
                                        name.EncodingMethod = ParameterEncodingMethod.Rfc2047;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.LogException(ex, LogLevel.Error);
                            }
                        }

                        mimeMessage.Body = bodyBuilder.ToMessageBody();

                        mimeMessage.From.Add(
                            new MailboxAddress(
                                string.IsNullOrWhiteSpace(this.SmtpFromDisplayName) ? null : this.SmtpFromDisplayName,
                                this.SmtpFrom));

                        mimeMessage.To.Add(
                            new MailboxAddress(
                                string.IsNullOrWhiteSpace(mainRecipientDisplayName) ? null : mainRecipientDisplayName,
                                message.Message.Email));

                        ListStorage<MailRecipient> mimeRecipients = message.Info.TryGetRecipients();
                        if (mimeRecipients is { Count: > 0 })
                        {
                            foreach (MailRecipient recipient in mimeRecipients)
                            {
                                string displayName = recipient.DisplayName;

                                var recipientAddress = new MailboxAddress(
                                    string.IsNullOrWhiteSpace(displayName) ? null : displayName,
                                    recipient.Email);

                                switch (recipient.Type)
                                {
                                    case MailRecipientType.To:
                                        mimeMessage.To.Add(recipientAddress);
                                        break;

                                    case MailRecipientType.Cc:
                                        mimeMessage.Cc.Add(recipientAddress);
                                        break;

                                    case MailRecipientType.Bcc:
                                        mimeMessage.Bcc.Add(recipientAddress);
                                        break;

                                    default:
                                        throw new ArgumentOutOfRangeException(nameof(MailRecipientType), recipient.Type, null);
                                }
                            }
                        }

                        Logger.Trace("Sending message ID='{0}'", message.Message.ID);
                        await mailKitSmtpClient.SendAsync(mimeMessage, cancellationToken);
                        Logger.Trace("Message was sent, ID='{0}'", message.Message.ID);

                        break;

                    default:
                        throw new InvalidOperationException("Unsupported smtp client of type " + client?.GetType().AssemblyQualifiedName);
                }

                return true;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await this.LogProcessingErrorAsync(message.Message, ex, cancellationToken);
                return false;
            }
        }

        #endregion

        #region Methods

        private static NetSmtpClient CreateNetSmtpClient(string pickupDirectoryLocation)
        {
            // складываем письма в папку, которая соответствует либо абсолютному пути, либо пути относительно папки с текущей сборкой
            if (!Path.IsPathRooted(pickupDirectoryLocation))
            {
                string currentFolder = Assembly.GetExecutingAssembly().GetActualLocationFolder();
                pickupDirectoryLocation = Path.Combine(currentFolder, pickupDirectoryLocation);
            }

            // если папка уже есть или не удалось её создать - игнорируем ошибки, их будет выбрасывать сам SmtpClient
            FileHelper.CreateDirectoryIfNotExists(pickupDirectoryLocation);

            // указываем хост "localhost", чтобы не было обращений к конфигурации
            return new NetSmtpClient("localhost")
            {
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = pickupDirectoryLocation,
            };
        }


        private static async Task<ISmtpClient> CreateMailKitSmtpClientAsync(CancellationToken cancellationToken = default)
        {
            // отправляем письма по SMTP, читаем все настройки из конфига
            string host = NoticeMailerConfig.SmtpHost;
            int port = NoticeMailerConfig.SmtpPort;
            bool enableSsl = NoticeMailerConfig.SmtpEnableSsl;
            bool defaultCredentials = NoticeMailerConfig.SmtpDefaultCredentials;
            string userName = NoticeMailerConfig.SmtpUserName;
            string password = NoticeMailerConfig.SmtpPassword;
            string clientDomain = NoticeMailerConfig.SmtpClientDomain;
            int timeout = NoticeMailerConfig.SmtpTimeout;

            MailKitSmtpClient client = null;

            try
            {
                client = new MailKitSmtpClient { ServerCertificateValidationCallback = (s, c, h, e) => true };

                if (timeout > 0)
                {
                    client.Timeout = timeout;
                }

                await client.ConnectAsync(host, port, enableSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None, cancellationToken);

                if ((client.Capabilities & SmtpCapabilities.Authentication) != 0
                    && (defaultCredentials || !string.IsNullOrEmpty(userName)))
                {
                    ICredentials credentials = defaultCredentials
                        ? CredentialCache.DefaultCredentials
                        : string.IsNullOrEmpty(clientDomain)
                            ? new NetworkCredential(userName, password)
                            : new NetworkCredential(userName, password, clientDomain);

                    await client.AuthenticateAsync(credentials, cancellationToken);
                }

                MailKitSmtpClient result = client;
                client = null;

                return result;
            }
            finally
            {
                if (client != null)
                {
                    if (client.IsConnected)
                    {
                        await client.DisconnectAsync(true, cancellationToken);
                    }

                    client.Dispose();
                }
            }
        }

        #endregion

        #region IAsyncDisposable Members

        public async ValueTask DisposeAsync()
        {
            if (this.netSmtpClient != null)
            {
                this.netSmtpClient.Dispose();
                this.netSmtpClient = null;
            }

            if (this.mailKitSmtpClient != null)
            {
                try
                {
                    await this.mailKitSmtpClient.DisconnectAsync(true);
                }
                catch (OperationCanceledException)
                {
                    // ignored
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex, LogLevel.Warn);
                }

                this.mailKitSmtpClient.Dispose();
                this.mailKitSmtpClient = null;
            }

            this.asyncLock.Dispose();
        }

        #endregion
    }
}