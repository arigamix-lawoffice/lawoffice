using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Tessa.Exchange.WebServices.Data;
using NLog;
using Tessa.Extensions.Default.Server.Notices;
using Tessa.Platform.Plugins;
using Task = System.Threading.Tasks.Task;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    /// <summary>
    /// Плагин, выполняющий рассылку уведомлений.
    /// </summary>
    public sealed class NoticeMailerPlugin :
        PluginExtension
    {
        #region Fields

        private MailingMode mode;
        private string exchangeOAuthToken;
        private string exchangeUser;
        private string exchangePassword;
        private string exchangeServer;
        private string exchangeProxyAddress;
        private string exchangeProxyUser;
        private string exchangeProxyPassword;
        private string exchangeFrom;
        private string exchangeFromDisplayName;
        private ExchangeVersion exchangeVersion;
        private string smtpFrom;
        private string smtpFromDisplayName;
        private string smtpUserName;
        private int numberOfMessagesToProcessAtOnce;
        private int maxAttemptsBeforeDelete;
        private int retryIntervalMinutes;
        private long maxFilesSizeEmail;
        private int maxNumberWorkingProcesses;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region MailingMode Enum

        private enum MailingMode
        {
            Exchange,
            Smtp,
            Disabled,
            Unknown,
        }

        private static MailingMode ParseMailingMode(string mode)
        {
            if (string.IsNullOrEmpty(mode))
            {
                return MailingMode.Disabled;
            }

            switch (mode.ToLowerInvariant())
            {
                case "exchange":
                    return MailingMode.Exchange;

                case "smtp":
                    return MailingMode.Smtp;

                case "disabled":
                    return MailingMode.Disabled;

                default:
                    return MailingMode.Unknown;
            }
        }

        #endregion

        #region Private methods

        private bool ParseSettings()
        {
            this.mode = ParseMailingMode(NoticeMailerConfig.Mode);

            if (this.mode == MailingMode.Disabled)
            {
                return false;
            }

            if (this.mode == MailingMode.Unknown)
            {
                logger.Error(
                    "Invalid mail sending mode specified in setting {0}",
                    NoticeMailerConfig.NoticeMailer_Mode_PropertyName);

                return false;
            }

            long? maxFilesSizeEmailTemp = NoticeMailerConfig.MaxFilesSizeEmail;
            if (maxFilesSizeEmailTemp == null)
            {
                logger.Error(
                    "Invalid max files size specified in setting {0}",
                    NoticeMailerConfig.NoticeMailer_MaxFilesSizeEmail_PropertyName);

                return false;
            }

            this.maxFilesSizeEmail = maxFilesSizeEmailTemp.Value;

            int? maxNumberWorkingProcessesTemp = NoticeMailerConfig.MaxNumberWorkingProcesses;
            if (maxNumberWorkingProcessesTemp == null)
            {
                logger.Error(
                    "Invalid max number working processes specified in setting {0}",
                    NoticeMailerConfig.NoticeMailer_MaxNumberWorkingProcesses_PropertyName);

                return false;
            }

            this.maxNumberWorkingProcesses = maxNumberWorkingProcessesTemp.Value;

            bool isInvalidSettings = false;
            switch (this.mode)
            {
                case MailingMode.Exchange:
                    this.exchangeOAuthToken = NoticeMailerConfig.ExchangeOAuthToken;
                    this.exchangeUser = NoticeMailerConfig.ExchangeUser;
                    this.exchangePassword = NoticeMailerConfig.ExchangePassword;
                    this.exchangeServer = NoticeMailerConfig.ExchangeServer;
                    this.exchangeProxyAddress = NoticeMailerConfig.ExchangeProxyAddress;
                    this.exchangeProxyUser = NoticeMailerConfig.ExchangeProxyUser;
                    this.exchangeProxyPassword = NoticeMailerConfig.ExchangeProxyPassword;
                    this.exchangeFrom = NoticeMailerConfig.ExchangeFrom;
                    this.exchangeFromDisplayName = NoticeMailerConfig.ExchangeFromDisplayName;
                    ExchangeVersion? exchangeVersionTemp = NoticeMailerConfig.ExchangeVersion;

                    if (string.IsNullOrEmpty(this.exchangeUser))
                    {
                        isInvalidSettings = true;
                        logger.Error(
                            "Invalid settings specified for connection to Exchange server: {0}",
                            NoticeMailerConfig.NoticeMailer_ExchangeUser_PropertyName);
                    }

                    if (exchangeVersionTemp == null)
                    {
                        isInvalidSettings = true;
                        logger.Error(
                            "Invalid settings specified for connection to Exchange server: {0}",
                            NoticeMailerConfig.NoticeMailer_ExchangeVersion_PropertyName);
                    }

                    if (isInvalidSettings)
                        return false;

                    this.exchangeVersion = exchangeVersionTemp.Value;
                    break;

                case MailingMode.Smtp:
                    this.smtpFrom = NoticeMailerConfig.SmtpFrom;
                    this.smtpUserName = NoticeMailerConfig.SmtpUserName;
                    if (string.IsNullOrEmpty(this.smtpFrom) && string.IsNullOrEmpty(this.smtpUserName))
                    {
                        logger.Error(
                            "Invalid address used to send mail using SMTP in settings {0} and {1}",
                            NoticeMailerConfig.NoticeMailer_SmtpFrom_PropertyName,
                            NoticeMailerConfig.NoticeMailer_SmtpUserName_PropertyName);

                        return false;
                    }
                    this.smtpFromDisplayName = NoticeMailerConfig.SmtpFromDisplayName;  // может быть null или пустой строкой
                    break;

                case MailingMode.Unknown:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.mode), this.mode, null);
            }

            int? numberOfMessagesToProcessAtOnceTmp = NoticeMailerConfig.NumberOfMessagesToProcessAtOnce;
            if (numberOfMessagesToProcessAtOnceTmp == null)
            {
                logger.Error(
                    "Invalid number of messages to process at once in setting {0}",
                    NoticeMailerConfig.NoticeMailer_NumberOfMessagesToProcessAtOnce_PropertyName);

                return false;
            }

            this.numberOfMessagesToProcessAtOnce = numberOfMessagesToProcessAtOnceTmp.Value;

            var maxAttemptsBeforeDeleteTmp = NoticeMailerConfig.MaxAttemptsBeforeDelete;
            if (maxAttemptsBeforeDeleteTmp == null)
            {
                logger.Error(
                    "Invalid attempt count before message is deleted in setting {0}",
                    NoticeMailerConfig.NoticeMailer_MaxAttemptsBeforeDelete_PropertyName);

                return false;
            }

            this.maxAttemptsBeforeDelete = maxAttemptsBeforeDeleteTmp.Value;

            var retryIntervalMinutesTmp = NoticeMailerConfig.RetryIntervalMinutes;
            if (retryIntervalMinutesTmp == null)
            {
                logger.Error(
                    "Invalid interval before failed message is sent again in setting {0}",
                    NoticeMailerConfig.NoticeMailer_RetryIntervalMinutes_PropertyName);

                return false;
            }

            this.retryIntervalMinutes = retryIntervalMinutesTmp.Value;
            return true;
        }

        #endregion

        #region IPlugin Members

        public override async Task EntryPoint(IPluginExtensionContext context)
        {
            // парсим настройки отправки почты
            if (!this.ParseSettings())
            {
                logger.Trace("Plugin disabled");
                return;
            }

            // за раз выполняется обработка только части сообщений - думаю нет смысла напрягаться
            logger.Trace("Retrieving list of messages to send.");

            ConcurrentQueue<OutboxMessage> messagesQueue =
                await context.Resolve<IOutboxManager>()
                    .GetTopMessagesAsync(this.numberOfMessagesToProcessAtOnce, this.retryIntervalMinutes, context.CancellationToken);

            if (context.StopRequested)
            {
                return;
            }

            var disposables = new List<IAsyncDisposable>(this.maxNumberWorkingProcesses);
            Task[] tasks = new Task[this.maxNumberWorkingProcesses];

            try
            {
                switch (this.mode)
                {
                    case MailingMode.Exchange:
                        var exchangeSettings = new ExchangeSettings
                        {
                            OAuthToken = this.exchangeOAuthToken,
                            User = this.exchangeUser,
                            Password = this.exchangePassword,
                            Server = this.exchangeServer,
                            ProxyAddress =
                                string.IsNullOrWhiteSpace(this.exchangeProxyAddress)
                                    ? null
                                    : new Uri(this.exchangeProxyAddress),
                            ProxyUser = this.exchangeProxyUser,
                            ProxyPassword = this.exchangeProxyPassword,
                            Version = this.exchangeVersion,
                            From = string.IsNullOrWhiteSpace(this.exchangeFrom) ? this.exchangeUser : this.exchangeFrom,
                            FromDisplayName = this.exchangeFromDisplayName,
                        };

                        for (int i = 0; i < tasks.Length; i++)
                        {
                            var exchangeSender = context.Resolve<ExchangeSender>();
                            exchangeSender.Context = context;
                            exchangeSender.Settings = exchangeSettings;
                            exchangeSender.MaxFilesSizeEmail = this.maxFilesSizeEmail;
                            exchangeSender.MaxAttemptsBeforeDelete = this.maxAttemptsBeforeDelete;

                            disposables.Add(exchangeSender);

                            tasks[i] = exchangeSender.StartAsync(messagesQueue, context.CancellationToken);
                        }
                        break;

                    case MailingMode.Smtp:
                        for (int i = 0; i < tasks.Length; i++)
                        {
                            var smtpSender = context.Resolve<SmtpSender>();
                            smtpSender.Context = context;
                            smtpSender.SmtpFrom = string.IsNullOrWhiteSpace(this.smtpFrom) ? this.smtpUserName : this.smtpFrom;
                            smtpSender.SmtpFromDisplayName = this.smtpFromDisplayName;
                            smtpSender.MaxFilesSizeEmail = this.maxFilesSizeEmail;
                            smtpSender.MaxAttemptsBeforeDelete = this.maxAttemptsBeforeDelete;

                            disposables.Add(smtpSender);

                            tasks[i] = smtpSender.StartAsync(messagesQueue, context.CancellationToken);
                        }
                        break;
                }

                await Task.WhenAll(tasks);
            }
            finally
            {
                foreach (IAsyncDisposable disposable in disposables)
                {
                    await disposable.DisposeAsync();
                }
            }
        }

        #endregion
    }
}
