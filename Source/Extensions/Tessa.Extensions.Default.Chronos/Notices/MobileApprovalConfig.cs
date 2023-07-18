using System;
using Tessa.Exchange.WebServices.Data;
using NLog;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public static class MobileApprovalConfig
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constants

        // ReSharper disable InconsistentNaming
        public const string MobileApproval_Mode_PropertyName = "MobileApproval.Mode";
        public const string MobileApproval_Pop3ImapHost_PropertyName = "MobileApproval.Pop3ImapHost";
        public const string MobileApproval_Pop3ImapPort_PropertyName = "MobileApproval.Pop3ImapPort";
        public const string MobileApproval_Pop3ImapUser_PropertyName = "MobileApproval.Pop3ImapUser";
        public const string MobileApproval_Pop3ImapPassword_PropertyName = "MobileApproval.Pop3ImapPassword";
        public const string MobileApproval_Pop3ImapUseSsl_PropertyName = "MobileApproval.Pop3ImapUseSsl";

        public const string MobileApproval_ExchangeOAuthToken_PropertyName = "MobileApproval.ExchangeOAuthToken";
        public const string MobileApproval_ExchangeUser_PropertyName = "MobileApproval.ExchangeUser";
        public const string MobileApproval_ExchangeServer_PropertyName = "MobileApproval.ExchangeServer";
        public const string MobileApproval_ExchangePassword_PropertyName = "MobileApproval.ExchangePassword";
        public const string MobileApproval_ExchangeProxyAddress_PropertyName = "MobileApproval.ExchangeProxyAddress";
        public const string MobileApproval_ExchangeProxyUser_PropertyName = "MobileApproval.ExchangeProxyUser";
        public const string MobileApproval_ExchangeProxyPassword_PropertyName = "MobileApproval.ExchangeProxyPassword";

        public const string MobileApproval_ExchangeVersion_PropertyName = "MobileApproval.ExchangeVersion";
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
                logger.Warn("Attribute '" + settingName + "' is not found.");
            }

            return attribute;
        }

        #endregion

        #region Properties

        public static string Mode => GetSetting<string>(MobileApproval_Mode_PropertyName);

        public static string Pop3ImapHost => GetSetting<string>(MobileApproval_Pop3ImapHost_PropertyName);

        public static int? Pop3ImapPort => (int?) GetSetting<long?>(MobileApproval_Pop3ImapPort_PropertyName);

        public static string Pop3ImapUser => GetSetting<string>(MobileApproval_Pop3ImapUser_PropertyName);

        public static string Pop3ImapPassword => GetSetting<string>(MobileApproval_Pop3ImapPassword_PropertyName);

        public static bool? Pop3ImapUseSsl => GetSetting<bool?>(MobileApproval_Pop3ImapUseSsl_PropertyName);

        public static string ExchangeOAuthToken => GetSetting<string>(MobileApproval_ExchangeOAuthToken_PropertyName);

        public static string ExchangeUser => GetSetting<string>(MobileApproval_ExchangeUser_PropertyName);

        public static string ExchangePassword => GetSetting<string>(MobileApproval_ExchangePassword_PropertyName);

        public static string ExchangeServer => GetSetting<string>(MobileApproval_ExchangeServer_PropertyName);

        public static string ExchangeProxyAddress => GetSetting<string>(MobileApproval_ExchangeProxyAddress_PropertyName, nullable: true);

        public static string ExchangeProxyUser => GetSetting<string>(MobileApproval_ExchangeProxyUser_PropertyName, nullable: true);

        public static string ExchangeProxyPassword => GetSetting<string>(MobileApproval_ExchangeProxyPassword_PropertyName, nullable: true);

        public static ExchangeVersion? ExchangeVersion
        {
            get
            {
                string exchangeVersion = GetSetting<string>(MobileApproval_ExchangeVersion_PropertyName);
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

        #endregion
    }
}