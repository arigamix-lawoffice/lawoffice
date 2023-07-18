using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Plugins;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    /// <summary>
    /// Плагин, возвращающий из отложенного задания, для которых срок откладывания завершился.
    /// </summary>
    public sealed class ReturnTasksFromPostponedPlugin :
        PluginExtension
    {
        #region TaskRecord Private Class

        public sealed class TaskRecord
        {
            #region Constructors

            public TaskRecord(Guid cardID, Guid taskID)
            {
                this.cardID = cardID;
                this.taskID = taskID;
            }

            #endregion

            #region Properties

            private readonly Guid cardID;

            public Guid CardID
            {
                get { return this.cardID; }
            }


            private readonly Guid taskID;

            public Guid TaskID
            {
                get { return this.taskID; }
            }

            #endregion
        }

        #endregion

        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Private Methods

        private static async Task<List<TaskRecord>> GetTasksToReturnFromPostponedAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            db
                .SetCommand(
                    builderFactory
                        .Select()
                        .C("t", "ID", "RowID")
                        .From("Tasks", "t").NoLock()
                        .Where().C("t", "PostponedTo").LessOrEquals().P("UtcNow")
                        .Build(),
                    db.Parameter("UtcNow", DateTime.UtcNow))
                .LogCommand();

            var result = new List<TaskRecord>();

            await using DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(
                    new TaskRecord(
                        cardID: reader.GetGuid(0),
                        taskID: reader.GetGuid(1)));
            }

            return result;
        }

        #endregion

        #region IPlugin Members

        public override async Task EntryPoint(IPluginExtensionContext context)
        {
            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var builderFactory = context.DbScope.BuilderFactory;

                List<TaskRecord> records = await GetTasksToReturnFromPostponedAsync(db, builderFactory, context.CancellationToken);
                if (records.Count > 0)
                {
                    // инициализируем все зависимости из UnityContainer только после того, как мы нашли задания, возвращаемые из отложенных
                    var cardRepository = context.Resolve<ICardRepository>();
                    var cardMetadata = context.Resolve<ICardMetadata>();
                    var cardGetStrategy = context.Resolve<ICardGetStrategy>();
                    var permissionsProvider = context.Resolve<ICardServerPermissionsProvider>();
                    var notificationManager = context.Resolve<INotificationManager>();
                    var serverSettings = context.Resolve<ITessaServerSettings>();

                    // загрузка выполняется без расширений через cardGetStrategy,
                    // т.к. они могут неадекватно реагировать на загрузку только заданий без секций
                    // на момент написания плагина неадекватная реакция была у расширений на KrProcess

                    // для каждой карточки, для которой найдены отложенные задания
                    foreach (IGrouping<Guid, TaskRecord> recordsByCardID in records.GroupBy(x => x.CardID))
                    {
                        Guid cardID = recordsByCardID.Key;
                        logger.Trace("Loading tasks from card with identifier '{0}'.", cardID);

                        // загружаем системную информацию по заданиям карточки
                        var validationResult = new ValidationResultBuilder();

                        // ... загрузка выполняется без расширений через cardGetStrategy,
                        // ... т.к. расширения могут неадекватно реагировать на загрузку только заданий без секций;
                        // ... серверные расширения невозможно отключить с клиента, поэтому используем серверное API;
                        // ... на момент написания плагина неадекватная реакция была у расширений на KrProcess
                        CardGetContext getContext = await cardGetStrategy.TryLoadCardInstanceAsync(
                            cardID, db, cardMetadata, validationResult, cancellationToken: context.CancellationToken);

                        if (getContext == null)
                        {
                            logger.Warn("Card with identifier '{0}' isn't found.", cardID);
                            continue;
                        }

                        Card card = getContext.Card;
                        IList<CardGetContext> tasks = await cardGetStrategy.TryLoadTaskInstancesAsync(
                            cardID,
                            card,
                            db,
                            cardMetadata,
                            validationResult,
                            Session.CreateSystemSession(SessionType.Server, serverSettings),
                            getTaskMode: CardGetTaskMode.All,
                            loadCalendarInfo: false,
                            taskRowIDList: recordsByCardID.Select(x => x.TaskID),
                            cancellationToken: context.CancellationToken);

                        if (tasks is null)
                        {
                            logger.Warn("Can't find tasks for card with identifier '{0}'.", cardID);
                            continue;
                        }

                        // если есть ошибки или нет заданий - переходим к следующей карточке
                        ValidationResult getResult = validationResult.Build();
                        logger.LogResult(getResult);

                        if (!getResult.IsSuccessful)
                        {
                            continue;
                        }

                        ListStorage<CardTask> cardTasks = card.TryGetTasks();
                        // если не было помеченных заданий - переходим к следующей карточке
                        if (cardTasks == null || cardTasks.Count == 0)
                        {
                            continue;
                        }

                        foreach (CardTask task in cardTasks)
                        {
                            task.State = CardRowState.Modified;
                            task.Action = CardTaskAction.Progress;
                        }

                        // иначе выполняем сохранение с возвратом заданий
                        logger.Trace(
                            "Returning {0} task(s) from postponed state for CardID = '{1}'.",
                            cardTasks.Count,
                            cardID);

                        var cardClone = card.Clone();
                        var storeRequest = new CardStoreRequest { Card = cardClone };
                        storeRequest.SetPluginType(CardPluginTypes.ReturnTasksFromPostponed);

                        if (await cardGetStrategy.LoadSectionsAsync(getContext, context.CancellationToken))
                        {
                            string digest = await cardRepository.GetDigestAsync(cardClone, CardDigestEventNames.ActionHistoryReturnTasksFromPostponed, context.CancellationToken);
                            if (digest != null)
                            {
                                storeRequest.SetDigest(digest);
                            }
                        }

                        cardClone.RemoveAllButChanged();

                        permissionsProvider.SetFullPermissions(storeRequest);

                        CardStoreResponse storeResponse = await cardRepository.StoreAsync(storeRequest, context.CancellationToken);

                        // пишем, завершилась ли операция успехом или нет
                        // в случае успеха отсылаем сообщения на почту пользователям
                        ValidationResult storeResult = storeResponse.ValidationResult.Build();
                        logger.LogResult(storeResult);

                        if (storeResult.IsSuccessful)
                        {
                            logger.Trace(
                                "{0} task(s) were successfully returned from postponed state for CardID = '{1}'.",
                                cardTasks.Count,
                                cardID);

                            var postMessageResult = new ValidationResultBuilder();
                            foreach (CardTask task in cardTasks)
                            {
                                if (task.UserID.HasValue)
                                {
                                    postMessageResult.Add(
                                        await notificationManager.SendAsync(
                                            DefaultNotifications.ReturnFromPostponeNotification,
                                            new[] { task.UserID.Value },
                                            new NotificationSendContext
                                            {
                                                MainCardID = card.ID,
                                                GetCardFuncAsync = (_, ct) => ValueTask.FromResult(card),
                                                ExcludeDeputies = true,
                                                Info = Shared.Notices.NotificationHelper.GetInfoWithTask(task),
                                                ModifyEmailActionAsync = async (email, ct) => { Shared.Notices.NotificationHelper.ModifyTaskCaption(email, task); }
                                            }));
                                }
                            }

                            ValidationResult result = postMessageResult.Build();
                            logger.LogResult(result);
                        }
                        else
                        {
                            logger.Warn("Can't return task(s) from postponed state for CardID = '{0}'.", cardID);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
