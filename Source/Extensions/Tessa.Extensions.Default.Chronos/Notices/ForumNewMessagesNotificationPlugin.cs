using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Chronos.Plugins;
using NLog;
using Tessa.Extensions.Default.Shared;
using Tessa.Forums.Notifications;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Operations;
using Tessa.Platform.Redis;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    /// <summary>
    /// Плагин, выполняющий добавление уведомлений о текущих заданиях пользователя.
    /// </summary>
    [Plugin(
        Name = "Forum new messages notifications plugin",
        Description = "Plugin starts at configured time and send emails to users with information about new unread messages in forums.",
        Version = 1,
        ConfigFile = ConfigFilePath)]
    public class ForumNewMessagesNotificationPlugin : Plugin
    {
        #region Constants

        /// <summary>
        /// Относительный путь к конфигурационному файлу плагина.
        /// </summary>
        public const string ConfigFilePath = "configuration/ForumNewMessagesNotification.xml";

        public const string FmMessagesPluginTable = nameof(FmMessagesPluginTable);

        private static OperationLockOptions LockOptions { get; } =
           new()
           {
                LockName = RedisLockKeys.ForumsKey,
                OperationTypeID = OperationTypes.ForumNewMessagesProcessingNotificationsOperationID,
                OperationDescription = "$Forums_Operation_NewMessagesNotificationsSending",
                Timeout = TimeSpan.FromSeconds(300),
                TimeoutMessageTemplate =
                    "Forums messages lock: failed to acquire, timeout in {0} seconds. Please, check whether forums messages lock is stuck" +
                    " in active operations table \"Operations\". Remove the row if that`s the case."
           };

        #endregion

        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IDbScope dbScope;
        private INotificationManager notificationManager;
        private IOperationLockingStrategy operationLockingStrategy;
        private ITransactionStrategy transactionStrategy;
        private ITopicNotificationService topicNotificationService;

        #endregion

        #region Private Methods

        /// <summary>
        /// Получает дату последнего запуска плагина.
        /// До этой даты "включительно" рассылались сообщения при прошлом запуске.
        /// </summary>
        /// <param name="db">DbManager</param>
        /// <param name="builderFactory">IQueryBuilderFactory</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Дату последнего запуска плагина</returns>
        private static async Task<DateTime> GetLastPluginRunDateAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            var lastPluginRunDate = await db
                .SetCommand(
                    builderFactory
                        .Select().Top(1).C("LastPluginRunDate")
                        .From(FmMessagesPluginTable).NoLock()
                        .Limit(1)
                        .Build())
                .LogCommand()
                .ExecuteAsync<DateTime>(cancellationToken);

            return DateTime.SpecifyKind(lastPluginRunDate, DateTimeKind.Utc);
        }

        /// <summary>
        /// Обновляет дату последнего запуска плагина.
        /// До этой даты "включительно" рассылались сообщения при текущем запуске.
        /// </summary>
        /// <param name="db">DbManager</param>
        /// <param name="builderFactory">IQueryBuilderFactory</param>
        /// <param name="newLastPluginRunDate">Новая дата последнего запуска плагина.</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Асинхронная задача.</returns>
        private static async Task UpdateLastPluginRunDateAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            DateTime newLastPluginRunDate,
            CancellationToken cancellationToken = default)
        {
            int updated = await db
                .SetCommand(
                    builderFactory
                        .Update(FmMessagesPluginTable)
                        .C("LastPluginRunDate").Equals().P("NewLastPluginRunDate")
                        .Build(),
                    db.Parameter("NewLastPluginRunDate", newLastPluginRunDate))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);

            if (updated == 0)
            {
                await db
                    .SetCommand(
                        builderFactory
                            .InsertInto(FmMessagesPluginTable, "LastPluginRunDate")
                            .Values(v => v.P("NewLastPluginRunDate"))
                            .Build(),
                        db.Parameter("NewLastPluginRunDate", newLastPluginRunDate))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }

        private static void ProceedNotificationRow(
            ITopicNotificationInfo info,
            ForumMessagesNotification currentNotification)
        {
            if (info != null)
            {
                info.HtmlText = Regex.Replace(info.HtmlText, "<img ", "<img alt=\"[image]\" ");
                currentNotification.TopicsNotifications.Add(info);
            }
        }

        private async Task ProcessOperationAsync(CancellationToken cancellationToken = default)
        {
            if (this.StopRequested)
            {
                return;
            }

            logger.Trace("Forums new messages notifications processing: started");

            await using (this.dbScope.Create())
            {
                await this.operationLockingStrategy.ExecuteInLockAsync(
                    options: LockOptions,
                    logger: logger,
                    cancellationToken: cancellationToken,
                    actionAsync: async (_, ct) =>
                    {
                        var db = this.dbScope.Db;
                        var builderFactory = this.dbScope.BuilderFactory;

                        try
                        {
                            if (this.StopRequested)
                            {
                                return;
                            }

                            // Записываем текущую дату/время запуска в переменную
                            // и считываем из базы дату/время предыдущего запуска
                            DateTime currentPluginRunDateTime = DateTime.UtcNow;
                            DateTime lastPluginRunDateTime = await GetLastPluginRunDateAsync(db, builderFactory, cancellationToken);

                            logger.Trace("Getting notifications.");

                            // Инфо упорядочена по пользователям
                            IList<ITopicNotificationInfo> notificationList = await this.topicNotificationService.GetNotificationsInfoAsync(
                                lastPluginRunDateTime,
                                currentPluginRunDateTime,
                                cancellationToken);

                            if (this.StopRequested || notificationList.Count == 0)
                            {
                                return;
                            }

                            logger.Trace("Processing notifications.");

                            // Открываем транзакцию, чтобы, если что,
                            // всегда можно было откатиться на момент до отправки первого уведомления
                            var validationResult = new ValidationResultBuilder();
                            bool success = await this.transactionStrategy.ExecuteInTransactionAsync(
                                validationResult,
                                async p =>
                                {
                                    ForumMessagesNotification currentNotification =
                                        new(
                                            notificationList[0].UserID,
                                            notificationList[0].WebLink);

                                    // Парсим уведомления
                                    foreach (ITopicNotificationInfo notification in notificationList)
                                    {
                                        if (currentNotification.UserID != notification.UserID)
                                        {
                                            var sendResult = await this.notificationManager.SendAsync(
                                                DefaultNotifications.ForumNewMessagesNotification,
                                                new[] { currentNotification.UserID },
                                                new NotificationSendContext
                                                {
                                                    ExcludeDeputies = true,
                                                    MainCardID = currentNotification.UserID,
                                                    Info = currentNotification.CreateInfo(),
                                                },
                                                p.CancellationToken);

                                            p.ValidationResult.Add(sendResult);
                                            if (!sendResult.IsSuccessful)
                                            {
                                                return;
                                            }

                                            // Начинаем обработку следующего пользователя
                                            currentNotification =
                                                new ForumMessagesNotification(
                                                    notification.UserID,
                                                    notification.WebLink);

                                            ProceedNotificationRow(notification, currentNotification);
                                        }
                                        else
                                        {
                                            ProceedNotificationRow(notification, currentNotification);
                                        }
                                    }

                                    var lastSendResult = await this.notificationManager.SendAsync(
                                        DefaultNotifications.ForumNewMessagesNotification,
                                        new[] { currentNotification.UserID },
                                        new NotificationSendContext
                                        {
                                            ExcludeDeputies = true,
                                            MainCardID = currentNotification.UserID,
                                            Info = currentNotification.CreateInfo(),
                                        },
                                        p.CancellationToken);

                                    p.ValidationResult.Add(lastSendResult);
                                    if (!lastSendResult.IsSuccessful)
                                    {
                                        return;
                                    }

                                    // Обновляем дату последнего запуска плагина (LastPluginRunDate) из даты СurrentPluginRunDateTime
                                    logger.Trace("Updating LastPluginRunDate.");
                                    await UpdateLastPluginRunDateAsync(db, builderFactory, currentPluginRunDateTime, p.CancellationToken);
                                },
                                cancellationToken);

                            logger.LogResultItems(validationResult);

                            if (!success)
                            {
                                throw new ValidationException(validationResult.Build());
                            }

                            logger.Trace("Notifications were successfully processed.");
                        }
                        catch (Exception ex)
                        {
                            // исключения игнорируются
                            if (ex is not OperationCanceledException)
                            {
                                logger.LogException(ex);
                            }

                            if (db.DataConnection.Transaction != null)
                            {
                                try
                                {
                                    await db.RollbackTransactionAsync(CancellationToken.None);
                                }
                                catch (Exception ex2)
                                {
                                    // исключения игнорируются
                                    logger.LogException(ex2);
                                }
                            }
                        }
                    });
            }

            logger.Trace("Forums new messages notifications processing: completed");
        }

        #endregion

        #region Base Overrides

        public override async Task EntryPointAsync(CancellationToken cancellationToken = default)
        {
            logger.Trace("Starting forum new messages notifications plugin.");

            IUnityContainer container = await new UnityContainer().RegisterServerForPluginAsync(cancellationToken: cancellationToken);

            this.dbScope = container.Resolve<IDbScope>();
            this.operationLockingStrategy = container.Resolve<IOperationLockingStrategy>();
            this.transactionStrategy = container.Resolve<ITransactionStrategy>();
            this.notificationManager = container.Resolve<INotificationManager>();
            this.topicNotificationService = container.Resolve<ITopicNotificationService>();

            await this.ProcessOperationAsync(cancellationToken);
        }

        #endregion
    }
}
