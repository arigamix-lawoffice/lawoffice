using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Chronos.Plugins;
using LinqToDB;
using NLog;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Runtime;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    /// <summary>
    /// Плагин, выполняющий добавление уведомлений о текущих заданиях пользователя.
    /// </summary>
    [Plugin(
        Name = "Password notifications plugin",
        Description = "Plugin starts at configured time and send emails to users for whom theirs passwords will expire soon.",
        Version = 1,
        ConfigFile = ConfigFilePath)]
    public sealed class PasswordNotificationsPlugin :
        Plugin
    {
        #region Constants

        /// <summary>
        /// Относительный путь к конфигурационному файлу плагина.
        /// </summary>
        public const string ConfigFilePath = "configuration/PasswordNotifications.xml";

        #endregion

        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        public override async Task EntryPointAsync(CancellationToken cancellationToken = default)
        {
            logger.Trace("Starting password notifications plugin.");

            IUnityContainer container = await new UnityContainer().RegisterServerForPluginAsync(cancellationToken: cancellationToken);

            var session = container.Resolve<ISession>();
            var serverSecurityProvider = container.Resolve<IServerSecurityProvider>();

            IServerSecurityOptions options = await serverSecurityProvider
                .GetSecurityOptionsAsync(session.InstanceName, cancellationToken);

            if (!options.PasswordExpirationNotificationTime.HasValue || !options.PasswordExpirationTime.HasValue)
            {
                logger.Trace("Password expiration notifications are disabled in security settings.");
                return;
            }

            // дата/время начала отправки уведомлений об истечении паролей;
            // если пароль истекает раньше этой даты, то отправляем уведомление
            DateTime notificationStartDateTime = DateTime.UtcNow + options.PasswordExpirationNotificationTime.Value - options.PasswordExpirationTime.Value;

            var dbScope = container.Resolve<IDbScope>();
            var notificationManager = container.Resolve<INotificationManager>();

            if (this.StopRequested)
            {
                return;
            }

            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;

                var recipientList = new List<NotificationRecipient>();

                db
                    .SetCommand(
                        builderFactory
                            .Select()
                            .C("pr", "Email", "ID", "Name")
                            .Coalesce(b => b.C("prs", "LanguageCode").C("krs", "NotificationsDefaultLanguageCode"))
                            .Coalesce(b => b.C("prs", "FormatName").C("krs", "NotificationsDefaultFormatName"))
                            .C("m", "Value")
                            .From(RoleStrings.PersonalRoles, "pr").NoLock()
                            .LeftJoinLateral(b => b
                                    .Select().C("prs", "LanguageCode", "FormatName")
                                    .From(CardSatelliteHelper.SatellitesSectionName, "sat").NoLock()
                                    .InnerJoin(RoleStrings.PersonalRoleSatellite, "prs").NoLock()
                                    .On().C("prs", "ID").Equals().C("sat", "ID")
                                    .Where().C("sat", CardSatelliteHelper.MainCardIDColumn).Equals().C("pr", "ID")
                                    .And().C("sat", CardSatelliteHelper.SatelliteTypeIDColumn).Equals().V(RoleHelper.PersonalRoleSatelliteTypeID),
                                "prs")
                            .LeftJoinLateral(b => b
                                    .Select().V(true).As("Value")
                                    .From("MobileLicenses", "ml").NoLock()
                                    .Where().C("ml", "UserID").Equals().C("pr", "ID"),
                                "m")
                            .CrossJoin("KrSettings", "krs").NoLock()
                            .Where()
                            .C("pr", "PasswordChanged").LessOrEquals().P("StartDateTime")
                            .AppendLine()
                            .And().C("pr", "LoginTypeID").Equals().V((short) UserLoginType.Tessa)
                            .And().C("pr", "Email").IsNotNull()
                            .And().C("pr", "Email").NotEquals().V(string.Empty)
                            .Build(),
                        db.Parameter("StartDateTime", notificationStartDateTime, DataType.DateTime))
                    .LogCommand();

                await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        string email = reader.GetValue<string>(0);

                        if (string.IsNullOrWhiteSpace(email)
                            || !FormattingHelper.EmailRegex.IsMatch(email))
                        {
                            continue;
                        }

                        Guid userID = reader.GetGuid(1);
                        string userName = reader.GetString(2);

                        string languageCode = reader.GetValue<string>(3);
                        if (string.IsNullOrEmpty(languageCode))
                        {
                            languageCode = LocalizationManager.EnglishLanguageCode;
                        }

                        string formatName = reader.GetValue<string>(4);
                        if (string.IsNullOrEmpty(formatName))
                        {
                            formatName = LocalizationManager.EnglishLanguageCode;
                        }

                        bool hasMobileApproval = !await reader.IsDBNullAsync(5, cancellationToken);

                        recipientList.Add(new NotificationRecipient
                        {
                            Email = email,
                            LanguageCode = languageCode,
                            FormatName = formatName,
                            UserID = userID,
                            UserName = userName,
                            HasMobileApproval = hasMobileApproval,
                        });
                    }
                }

                if (recipientList.Count == 0)
                {
                    logger.Trace("There are no users with specified emails to notify about theirs passwords expiration.");
                    return;
                }

                foreach (NotificationRecipient recipient in recipientList)
                {
                    if (this.StopRequested)
                    {
                        return;
                    }

                    logger.Trace(
                        "Sending notification to user \"{0}\", ID={1:B}, Email={2}, Language={3}",
                        recipient.UserName, recipient.UserID, recipient.Email, recipient.LanguageCode);

                    await notificationManager.SendUsersAsync(
                        DefaultNotifications.PasswordExpiresID,
                        new[] { recipient },
                        new NotificationSendContext
                        {
                            MainCardID = recipient.UserID,
                        },
                        cancellationToken);
                }
            }

            logger.Trace("Notifications were successfully processed.");
        }

        #endregion
    }
}
