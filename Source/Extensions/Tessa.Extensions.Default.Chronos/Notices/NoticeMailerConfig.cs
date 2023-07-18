using System;
using Tessa.Exchange.WebServices.Data;
using NLog;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public static class NoticeMailerConfig
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constants

        // ReSharper disable InconsistentNaming
        public const string NoticeMailer_Mode_PropertyName = "NoticeMailer.Mode";
        public const string NoticeMailer_ExchangeOAuthToken_PropertyName = "NoticeMailer.ExchangeOAuthToken";
        public const string NoticeMailer_ExchangeUser_PropertyName = "NoticeMailer.ExchangeUser";
        public const string NoticeMailer_ExchangeServer_PropertyName = "NoticeMailer.ExchangeServer";
        public const string NoticeMailer_ExchangePassword_PropertyName = "NoticeMailer.ExchangePassword";
        public const string NoticeMailer_ExchangeVersion_PropertyName = "NoticeMailer.ExchangeVersion";
        public const string NoticeMailer_ExchangeProxyAddress_PropertyName = "NoticeMailer.ExchangeProxyAddress";
        public const string NoticeMailer_ExchangeProxyUser_PropertyName = "NoticeMailer.ExchangeProxyUser";
        public const string NoticeMailer_ExchangeProxyPassword_PropertyName = "NoticeMailer.ExchangeProxyPassword";
        public const string NoticeMailer_ExchangeFrom_PropertyName = "NoticeMailer.ExchangeFrom";
        public const string NoticeMailer_ExchangeFromDisplayName_PropertyName = "NoticeMailer.ExchangeFromDisplayName";
        public const string NoticeMailer_SmtpPickupDirectoryLocation_PropertyName = "NoticeMailer.SmtpPickupDirectoryLocation";
        public const string NoticeMailer_SmtpHost_PropertyName = "NoticeMailer.SmtpHost";
        public const string NoticeMailer_SmtpPort_PropertyName = "NoticeMailer.SmtpPort";
        public const string NoticeMailer_SmtpEnableSsl_PropertyName = "NoticeMailer.SmtpEnableSsl";
        public const string NoticeMailer_SmtpDefaultCredentials_PropertyName = "NoticeMailer.SmtpDefaultCredentials";
        public const string NoticeMailer_SmtpUserName_PropertyName = "NoticeMailer.SmtpUserName";
        public const string NoticeMailer_SmtpPassword_PropertyName = "NoticeMailer.SmtpPassword";
        public const string NoticeMailer_SmtpClientDomain_PropertyName = "NoticeMailer.SmtpClientDomain";
        public const string NoticeMailer_SmtpFrom_PropertyName = "NoticeMailer.SmtpFrom";
        public const string NoticeMailer_SmtpFromDisplayName_PropertyName = "NoticeMailer.SmtpFromDisplayName";
        public const string NoticeMailer_SmtpTimeout_PropertyName = "NoticeMailer.SmtpTimeout";
        public const string NoticeMailer_NumberOfMessagesToProcessAtOnce_PropertyName = "NoticeMailer.NumberOfMessagesToProcessAtOnce";
        public const string NoticeMailer_MaxAttemptsBeforeDelete_PropertyName = "NoticeMailer.MaxAttemptsBeforeDelete";
        public const string NoticeMailer_RetryIntervalMinutes_PropertyName = "NoticeMailer.RetryIntervalMinutes";
        public const string NoticeMailer_MaxFilesSizeEmail_PropertyName = "NoticeMailer.MaxFilesSizeEmail";
        public const string NoticeMailer_MaxNumberWorkingProcesses_PropertyName = "NoticeMailer.MaxNumberWorkingProcesses";
        // ReSharper restore InconsistentNaming

        private const ExchangeVersion DefaultExchangeVersion = Tessa.Exchange.WebServices.Data.ExchangeVersion.Exchange2010;

        #endregion

        #region Private methods

        private static T GetSetting<T>(string settingName, bool nullable = false)
        {
            T attribute = ConfigurationManager.Settings.TryGet<T>(settingName);

            if (attribute == null && !nullable)
            {
                // для типов, допускающих null
                logger.Warn("Attribute '" + settingName + "' isn't found or null.");
            }

            return attribute;
        }

        #endregion

        #region Properties

        public static string Mode => GetSetting<string>(NoticeMailer_Mode_PropertyName);

        public static string ExchangeOAuthToken => GetSetting<string>(NoticeMailer_ExchangeOAuthToken_PropertyName);

        public static string ExchangeUser => GetSetting<string>(NoticeMailer_ExchangeUser_PropertyName);

        public static string ExchangePassword => GetSetting<string>(NoticeMailer_ExchangePassword_PropertyName);

        public static string ExchangeServer => GetSetting<string>(NoticeMailer_ExchangeServer_PropertyName);

        public static string ExchangeProxyAddress => GetSetting<string>(NoticeMailer_ExchangeProxyAddress_PropertyName, nullable: true);

        public static string ExchangeProxyUser => GetSetting<string>(NoticeMailer_ExchangeProxyUser_PropertyName, nullable: true);

        public static string ExchangeProxyPassword => GetSetting<string>(NoticeMailer_ExchangeProxyPassword_PropertyName, nullable: true);

        public static string ExchangeFrom => GetSetting<string>(NoticeMailer_ExchangeFrom_PropertyName, nullable: true);

        public static string ExchangeFromDisplayName => GetSetting<string>(NoticeMailer_ExchangeFromDisplayName_PropertyName, nullable: true);

        public static ExchangeVersion? ExchangeVersion
        {
            get
            {
                string exchangeVersion = GetSetting<string>(NoticeMailer_ExchangeVersion_PropertyName);
                if (string.IsNullOrWhiteSpace(exchangeVersion))
                {
                    return DefaultExchangeVersion;
                }

                if (!Enum.TryParse(exchangeVersion, out ExchangeVersion version))
                {
                    return null;
                }

                return version;
            }
        }


        public static string SmtpPickupDirectoryLocation => GetSetting<string>(NoticeMailer_SmtpPickupDirectoryLocation_PropertyName, nullable: true);

        public static string SmtpHost => GetSetting<string>(NoticeMailer_SmtpHost_PropertyName);

        public static int SmtpPort => (int)(GetSetting<long?>(NoticeMailer_SmtpPort_PropertyName) ?? 0L);

        public static bool SmtpEnableSsl => GetSetting<bool>(NoticeMailer_SmtpEnableSsl_PropertyName);

        public static bool SmtpDefaultCredentials => GetSetting<bool>(NoticeMailer_SmtpDefaultCredentials_PropertyName);

        public static string SmtpUserName => GetSetting<string>(NoticeMailer_SmtpUserName_PropertyName);

        public static string SmtpPassword => GetSetting<string>(NoticeMailer_SmtpPassword_PropertyName);

        public static string SmtpClientDomain => GetSetting<string>(NoticeMailer_SmtpClientDomain_PropertyName);

        public static string SmtpFrom => GetSetting<string>(NoticeMailer_SmtpFrom_PropertyName);

        public static string SmtpFromDisplayName => GetSetting<string>(NoticeMailer_SmtpFromDisplayName_PropertyName);

        public static int SmtpTimeout => (int)(GetSetting<long?>(NoticeMailer_SmtpTimeout_PropertyName) ?? 0L);


        public static int? NumberOfMessagesToProcessAtOnce => (int?)GetSetting<long?>(NoticeMailer_NumberOfMessagesToProcessAtOnce_PropertyName);

        public static int? MaxAttemptsBeforeDelete => (int?)GetSetting<long?>(NoticeMailer_MaxAttemptsBeforeDelete_PropertyName);

        public static int? RetryIntervalMinutes => (int?)GetSetting<long?>(NoticeMailer_RetryIntervalMinutes_PropertyName);

        public static long? MaxFilesSizeEmail => GetSetting<long?>(NoticeMailer_MaxFilesSizeEmail_PropertyName);

        public static int? MaxNumberWorkingProcesses => (int?)GetSetting<long?>(NoticeMailer_MaxNumberWorkingProcesses_PropertyName);

        #endregion
    }
}
