using System;
using Tessa.Exchange.WebServices.Data;
using NLog;
using Tessa.Extensions.Default.Chronos.Notices;
using Tessa.Extensions.Default.Server.Notices;
using Tessa.Extensions.Default.Server.Workflow;
using Tessa.Platform.Licensing;
using Tessa.Platform.Plugins;
using Unity;
using Unity.Lifetime;
using Task = System.Threading.Tasks.Task;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public sealed class MobileApprovalPlugin : PluginExtension
    {
        #region Fields

        private MailingMode mode;
        private ExchangeSettings exchangeSettings;
        private Pop3ImapSettings pop3ImapSettings;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region MailingMode Enum

        private enum MailingMode
        {
            Exchange,
            Pop3,
            Imap,
            Disabled,
            Unknown,
        }

        private static MailingMode ParseMailingMode(string mode) =>
            string.IsNullOrEmpty(mode)
                ? MailingMode.Disabled
                : mode.ToLowerInvariant() switch
                {
                    "exchange" => MailingMode.Exchange,
                    "pop3" => MailingMode.Pop3,
                    "imap" => MailingMode.Imap,
                    "disabled" => MailingMode.Disabled,
                    _ => MailingMode.Unknown
                };

        #endregion

        #region Private methods

        private bool ParseSettings()
        {
            this.mode = ParseMailingMode(MobileApprovalConfig.Mode);
            bool invalidSettings = false;

            if (this.mode == MailingMode.Disabled)
            {
                return false;
            }

            if (this.mode == MailingMode.Unknown)
            {
                logger.Error(
                    "Invalid mode setting specified in setting {0}.",
                    MobileApprovalConfig.MobileApproval_Mode_PropertyName);

                return false;
            }

            switch (this.mode)
            {
                case MailingMode.Exchange:
                    string exchangeOAuthToken = MobileApprovalConfig.ExchangeOAuthToken;
                    string exchangeUser = MobileApprovalConfig.ExchangeUser;
                    string exchangePassword = MobileApprovalConfig.ExchangePassword;
                    string exchangeServer = MobileApprovalConfig.ExchangeServer;
                    string exchangeProxyAddress = MobileApprovalConfig.ExchangeProxyAddress;
                    string exchangeProxyUser = MobileApprovalConfig.ExchangeProxyUser;
                    string exchangeProxyPassword = MobileApprovalConfig.ExchangeProxyPassword;
                    ExchangeVersion? exchangeVersion = MobileApprovalConfig.ExchangeVersion;

                    if (string.IsNullOrEmpty(exchangeUser))
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid settings specified for connection to Exchange server: {0}.",
                            MobileApprovalConfig.MobileApproval_ExchangeUser_PropertyName);
                    }

                    if (!exchangeVersion.HasValue)
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid settings specified for connection to Exchange server: {0}.",
                            MobileApprovalConfig.MobileApproval_ExchangeVersion_PropertyName);
                    }

                    if (invalidSettings)
                    {
                        return false;
                    }

                    this.exchangeSettings =
                        new ExchangeSettings
                        {
                            OAuthToken = exchangeOAuthToken,
                            User = exchangeUser,
                            Password = exchangePassword,
                            Server = exchangeServer,
                            ProxyAddress = string.IsNullOrEmpty(exchangeProxyAddress) ? null : new Uri(exchangeProxyAddress),
                            ProxyUser = exchangeProxyUser,
                            ProxyPassword = exchangeProxyPassword,
                            Version = exchangeVersion.Value
                        };
                    break;

                case MailingMode.Pop3:
                case MailingMode.Imap:
                    var pop3ImapHost = MobileApprovalConfig.Pop3ImapHost;
                    var pop3ImapPort = MobileApprovalConfig.Pop3ImapPort;
                    var pop3ImapUser = MobileApprovalConfig.Pop3ImapUser;
                    var pop3ImapPassword = MobileApprovalConfig.Pop3ImapPassword;
                    var pop3ImapUseSsl = MobileApprovalConfig.Pop3ImapUseSsl;

                    if (string.IsNullOrEmpty(pop3ImapHost))
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid POP3/IMAP settings: {0}.", MobileApprovalConfig.MobileApproval_Pop3ImapHost_PropertyName);
                    }

                    if (!pop3ImapPort.HasValue)
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid POP3/IMAP settings: {0}.", MobileApprovalConfig.MobileApproval_Pop3ImapPort_PropertyName);
                    }

                    if (string.IsNullOrEmpty(pop3ImapUser))
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid POP3/IMAP settings: {0}.", MobileApprovalConfig.MobileApproval_Pop3ImapUser_PropertyName);
                    }

                    if (string.IsNullOrEmpty(pop3ImapPassword))
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid POP3/IMAP settings: {0}.", MobileApprovalConfig.MobileApproval_Pop3ImapPassword_PropertyName);
                    }

                    if (!pop3ImapUseSsl.HasValue)
                    {
                        invalidSettings = true;
                        logger.Error(
                            "Invalid POP3/IMAP settings: {0}.", MobileApprovalConfig.MobileApproval_Pop3ImapUseSsl_PropertyName);
                    }

                    if (invalidSettings)
                    {
                        return false;
                    }

                    this.pop3ImapSettings =
                        new Pop3ImapSettings
                        {
                            Host = pop3ImapHost,
                            Port = pop3ImapPort,
                            User = pop3ImapUser,
                            Password = pop3ImapPassword,
                            UseSsl = pop3ImapUseSsl
                        };
                    break;

                case MailingMode.Unknown:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.mode), this.mode, null);
            }

            return true;
        }

        // здесь нельзя делать RestrictTaskSections, т.к. секции заданий ожидают расширения на загрузку карточки, такие как WfTasksServerGetExtension

        #endregion

        #region Base Overrides

        public override async Task EntryPoint(IPluginExtensionContext context)
        {
            // парсим настройки получения почты
            if (!this.ParseSettings())
            {
                logger.Trace("Plugin disabled");
                return;
            }

            if (context.StopRequested)
            {
                return;
            }

            if (!context.UnityContainer.IsRegistered<SetProcessorTokenAction>())
            {
                context.UnityContainer
                    .RegisterFactory<SetProcessorTokenAction>(
                        c => new SetProcessorTokenAction(token => context.SessionToken = token),
                        new ContainerControlledLifetimeManager())
                    ;
            }

            await using (context.DbScope.Create())
            {
                var licenseValidator = context.Resolve<ILicenseValidator>();

                ILicense license = await context.Resolve<ILicenseManager>().GetLicenseAsync(context.CancellationToken);

                if (!license.Modules.Contains(LicenseModules.MobileApprovalID))
                {
                    logger.Error("Mobile approval license is not found");
                    return;
                }

                int userCount = await licenseValidator.GetMobileLicenseCountAsync(context.CancellationToken);
                int count = license.GetMobileCount();

                if (userCount > count)
                {
                    logger.Error("Mobile approval license limit exceeded. Selected employees: {0}. " +
                        "The total number of licenses: {1}", userCount, count);
                    return;
                }

                if (context.StopRequested)
                {
                    return;
                }

                logger.Trace("Mail processing.");

                IMailReceiver receiver = this.mode switch
                {
                    MailingMode.Exchange => context.Resolve<IMailReceiver>(MailReceiverNames.ExchangeMailReceiver),
                    MailingMode.Pop3 => context.Resolve<IMailReceiver>(MailReceiverNames.Pop3MailReceiver),
                    MailingMode.Imap => context.Resolve<IMailReceiver>(MailReceiverNames.ImapMailReceiver),
                    _ => null
                };

                if (receiver is not null)
                {
                    receiver.StopRequestedFunc = () => context.StopRequested;

                    // может имплементить один из интерфейсов, оба или ни одного
                    if (receiver is IPop3ImapSettingsContainer pop3ImapReceiver)
                    {
                        pop3ImapReceiver.Pop3ImapSettings = this.pop3ImapSettings;
                    }

                    if (receiver is IExchangeSettingsContainer exchangeReceiver)
                    {
                        exchangeReceiver.ExchangeSettings = this.exchangeSettings;
                    }

                    await receiver.ReceiveMessagesAsync(context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
