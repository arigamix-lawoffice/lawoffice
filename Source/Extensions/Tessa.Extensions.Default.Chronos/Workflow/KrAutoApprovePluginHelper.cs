using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Notices;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public static class KrAutoApprovePluginHelper
    {
        #region Public Methods

        /// <summary>
        /// Получение заданий для авто-завершения
        /// </summary>
        /// <param name="db">DbManager</param>
        /// <param name="builderFactory">IQueryBuilderFactory</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Список записей для авто-завершения</returns>
        public static async Task<List<KrAutoApproveTaskRecord>> GetTasksToAutoApproveAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            db
                .SetCommand(
                    builderFactory
                        .Select()
                            .C("ins", "ID", "TypeCaption")
                            .C("innerTable", "RowID")
                            .C("kdt", "AutoApproveComment")
                            .C("innerTable", "StageSettings")
                            .C("innerTable", "TaskSettings")
                        .From()
                            .E(e => e
                                .Select()
                                    .C("tsk", "ID")
                                    .C("tsk", "RowID")
                                    .C("tsk", "TimeZoneUtcOffsetMinutes")
                                    .C("tsk", "CalendarID")
                                    .C("tsk", "Planned")
                                    .C("krs", "Settings").As("StageSettings")
                                    .V(default(object)).As("TaskSettings")
                                .From("Tasks", "tsk").NoLock()
                                .InnerJoin("KrApprovalCommonInfo", "kraci").NoLock()
                                    .On().C("kraci", "MainCardID").Equals().C("tsk", "ID")
                                .InnerJoin("KrStages", "krs").NoLock()
                                    .On().C("krs", "RowID").Equals().C("kraci", "CurrentApprovalStageRowID")
                                .Where()
                                    .C("tsk", "TypeID").Equals().P("TaskTypeID")
                                    .And()
                                    .C("krs", "StageTypeID").Equals().V(StageTypeDescriptors.ApprovalDescriptor.ID)
                                .UnionAll()
                                .Select()
                                    .C("tsk", "ID")
                                    .C("tsk", "RowID")
                                    .C("tsk", "TimeZoneUtcOffsetMinutes")
                                    .C("tsk", "CalendarID")
                                    .C("tsk", "Planned")
                                    .V(default(object)).As("StageSettings")
                                    .C("tsk", "Settings").As("TaskSettings")
                                .From("Tasks", "tsk").NoLock()
                                .InnerJoin("WorkflowEngineTaskSubscriptions", "wets").NoLock()
                                    .On().C("wets", "TaskID").Equals().C("tsk", "RowID")
                                .Where()
                                    .C("tsk", "TypeID").Equals().P("TaskTypeID"))
                            .As("innerTable")
                        .InnerJoin("Instances", "ins").NoLock()
                            .On().C("ins", "ID").Equals().C("innerTable", "ID")
                        .InnerJoin("KrSettingsCardTypes", "sct").NoLock()
                            .On().C("sct", "CardTypeID").Equals().C("ins", "TypeID")
                        .LeftJoin("DocumentCommonInfo", "dci").NoLock()
                            .On().C("dci", "ID").Equals().C("innerTable", "ID")
                        .LeftJoin("KrDocType", "kdt").NoLock()
                            .On().C("kdt", "ID").Equals().C("dci", "DocTypeID")
                        .LeftJoinLateral(cs=>cs
                            .Select()
                                .C("cs", "CalendarID")
                            .From("CalendarSettings", "cs").NoLock()
                            .Where()
                                .C("cs", "ID").Equals().C("innerTable", "CalendarID"), "csa")
                        .Where()
                            .E(e => e
                                .C("sct", "UseDocTypes").Equals().V(false).And()
                                .C("sct", "UseApproving").Equals().V(true).And()
                                .C("sct", "UseAutoApprove").Equals().V(true).And()
                                .E(expr => expr
                                    .P("CurrentDate").GreaterOrEquals()
                                    .If(Dbms.SqlServer,
                                        v => v.Q(" DATEADD(minute, -").C("innerTable", "TimeZoneUtcOffsetMinutes").Q(", ")
                                            .Function("CalendarAddWorkingDaysToDate",
                                                p => InsertPlannedWithOffset(p)
                                                    .C("sct", "ExceededDays")
                                                    .C("csa", "CalendarID")).Q(") "))
                                    .ElseIf(Dbms.PostgreSql,
                                        v => v.Function("CalendarAddWorkingDaysToDate",
                                                p => InsertPlannedWithOffset(p)
                                                    .C("sct", "ExceededDays")
                                                    .C("csa", "CalendarID"))
                                            .Substract().Q("(").C("innerTable", "TimeZoneUtcOffsetMinutes").Q(" * interval '1 minute') "))
                                .ElseThrow()))
                            .Or()
                            .E(e => e
                                .C("kdt", "UseApproving").Equals().V(true).And()
                                .C("kdt", "UseAutoApprove").Equals().V(true).And()
                                .E(expr => expr
                                    .P("CurrentDate").GreaterOrEquals()
                                    .If(Dbms.SqlServer,
                                        v => v.Q(" DATEADD(minute, -").C("innerTable", "TimeZoneUtcOffsetMinutes").Q(", ")
                                            .Function("CalendarAddWorkingDaysToDate",
                                                p => InsertPlannedWithOffset(p)
                                                    .C("kdt", "ExceededDays")
                                                    .C("csa", "CalendarID")).Q(") "))
                                    .ElseIf(Dbms.PostgreSql,
                                        v => v.Function("CalendarAddWorkingDaysToDate",
                                                p => InsertPlannedWithOffset(p)
                                                    .C("kdt", "ExceededDays")
                                                    .C("csa", "CalendarID"))
                                            .Substract().Q("(").C("innerTable", "TimeZoneUtcOffsetMinutes").Q(" * interval '1 minute') "))
                                    .ElseThrow()))
                        .Build(),
                    db.Parameter("TaskTypeID", DefaultTaskTypes.KrApproveTypeID),
                    db.Parameter("CurrentDate", DateTime.UtcNow))
                .LogCommand();

            var result = new List<KrAutoApproveTaskRecord>();
            await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    var settings = reader.GetNullableString(4);
                    var taskSettings = reader.GetNullableString(5);

                    if (taskSettings != null
                        && Regex.IsMatch(taskSettings, "\"\\" + WorkflowConstants.NamesKeys.IsDisableAutoApproval + "\":\\s*true", RegexOptions.Compiled)
                        || settings != null
                        && Regex.IsMatch(settings, "\"KrApprovalSettingsVirtual__DisableAutoApproval\":\\s*true", RegexOptions.Compiled))
                    {
                        continue;
                    }

                    result.Add(
                        new KrAutoApproveTaskRecord(
                            reader.GetGuid(0),
                            reader.GetString(1),
                            reader.GetGuid(2),
                            reader.GetValue<string>(3)));
                }
            }

            return result;
        }

        /// <summary>
        /// Завершаем задания согласования
        /// </summary>
        /// <param name="tasksToApprove">Список заданий для завершения</param>
        /// <param name="db">DbManager</param>
        /// <param name="builderFactory">IQueryBuilderFactory</param>
        /// <param name="cardMetadata">ICardMetadata</param>
        /// <param name="cardRepository">ICardRepository</param>
        /// <param name="session">ISession</param>
        /// <param name="cardGetStrategy">ICardGetStrategy</param>
        /// <param name="permissionsProvider">ICardServerPermissionsProvider</param>
        /// <param name="transactionStrategy"></param>
        /// <param name="logger">Логгер</param>
        /// <param name="checkStopFunc">Функция проверки, что пора прекратить выполнение</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task CompleteApproveTasksAsync(
            List<KrAutoApproveTaskRecord> tasksToApprove,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            ISession session,
            ICardGetStrategy cardGetStrategy,
            ICardServerPermissionsProvider permissionsProvider,
            ICardTransactionStrategy transactionStrategy,
            ILogger logger,
            Func<bool> checkStopFunc,
            CancellationToken cancellationToken = default)
        {
            foreach (KrAutoApproveTaskRecord task in tasksToApprove)
            {
                if (checkStopFunc.Invoke())
                {
                    break;
                }

                var validationResult = new ValidationResultBuilder();

                KrAutoApproveTaskRecord taskClosure = task;

                await transactionStrategy.ExecuteInWriterLockAsync(
                    task.CardID,
                    CardComponentHelper.DoNotCheckVersion,
                    validationResult,
                    async p =>
                    {
                        Guid cardID = taskClosure.CardID;
                        Guid taskID = taskClosure.TaskID;

                        // Получаем карточку
                        var cardGetContext =
                            await cardGetStrategy.TryLoadCardInstanceAsync(
                                cardID,
                                db,
                                cardMetadata,
                                p.ValidationResult,
                                cancellationToken: p.CancellationToken);
                        if (!p.ValidationResult.IsSuccessful())
                        {
                            p.ReportError = true;
                            return;
                        }

                        // Грузим секции
                        if (!await cardGetStrategy.LoadSectionsAsync(cardGetContext, p.CancellationToken))
                        {
                            p.ReportError = true;
                            return;
                        }

                        Card card = cardGetContext.Card;

                        // Получаем описание карточки для ссылки в отчёте
                        string cardNumber = null;
                        string cardSubject = null;
                        if (card.Sections.TryGetValue("DocumentCommonInfo", out var dciSection))
                        {
                            cardNumber = dciSection.Fields.TryGet<string>("FullNumber");
                            cardSubject = dciSection.Fields.TryGet<string>("Subject");
                        }

                        var digest =
                            NotificationHelper.GetNameForLink(
                                !string.IsNullOrEmpty(cardNumber) &&
                                !string.IsNullOrEmpty(cardSubject)
                                    ? cardNumber + ", " + cardSubject
                                    : await cardRepository.GetDigestAsync(card, CardDigestEventNames.AutoApproveNotification, p.CancellationToken),
                                cardNumber,
                                await cardRepository.GetDigestAsync(card, CardDigestEventNames.AutoApproveNotification, p.CancellationToken));

                        // Грузим задания
                        var taskContextList =
                            await cardGetStrategy.TryLoadTaskInstancesAsync(
                                cardID,
                                cardGetContext.Card,
                                db,
                                cardMetadata,
                                p.ValidationResult,
                                session,
                                getTaskMode: CardGetTaskMode.All,
                                taskRowIDList: new[] { taskID },
                                cancellationToken: p.CancellationToken);

                        if (taskContextList is null)
                        {
                            p.ReportError = true;
                            return;
                        }

                        // Получаем секции задания
                        var taskContext = taskContextList.FirstOrDefault(x => x.Card.ID == taskID);
                        if (taskContext == null)
                        {
                            logger.Error("Failed to get task context, TaskID = {0}", taskID);
                            p.ReportError = true;
                            return;
                        }

                        if (!await cardGetStrategy.LoadSectionsAsync(taskContext, p.CancellationToken))
                        {
                            p.ReportError = true;
                            return;
                        }

                        CardTask taskToComplete = card.Tasks[0];

                        // Получаем данные о доп. согласовании
                        List<KrAutoApproveAdditionalTaskRecord> additionalApproveTasks =
                            await GetAdditionalApprovalInfoAsync(db, builderFactory, taskID, p.CancellationToken);

                        KrAutoApproveAdditionalTaskRecord responsibleTaskOrNull = additionalApproveTasks.FirstOrDefault(x => x.IsResponsible);

                        // Завершаем задание
                        taskToComplete.Action = CardTaskAction.Complete;
                        taskToComplete.State = CardRowState.Deleted;
                        taskToComplete.OptionID = GetCompletionOption(responsibleTaskOrNull);
                        taskToComplete.Card.Sections["KrTask"].Fields["Comment"] = GetAutoApproveComment(responsibleTaskOrNull, taskClosure);
                        taskToComplete.Flags |= CardTaskFlags.HistoryItemCreated;

                        // Пишем в AutoApproveHistory
                        await AddRowToAutoApproveHistoryAsync(db, builderFactory, cardID, taskID, card.TypeID, card.TypeCaption, digest,
                            taskClosure.ApprovalComment, p.CancellationToken);

                        // Сохраняем карточку

                        // Вычисляем дайджест, чтобы он попал в историю действий.
                        string cardDigest = await cardRepository.GetDigestAsync(card, CardDigestEventNames.ActionHistoryStoreAutoApprove, p.CancellationToken);
                        card.RemoveAllButChanged();

                        var request = new CardStoreRequest { Card = card };
                        request.SetDigest(cardDigest);

                        permissionsProvider.SetFullPermissions(request);

                        var response = await cardRepository.StoreAsync(request, p.CancellationToken);
                        if (!response.ValidationResult.IsSuccessful())
                        {
                            p.ReportError = true;
                        }
                    },
                    cancellationToken: cancellationToken);

                logger.LogResult(validationResult.Build());
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Повторяющийся кусок билдера для смещения плановой даты во временную зону задания, перед вызовом хоранимки из календаря.
        /// </summary>
        /// <param name="builder">IQueryBuilder</param>
        /// <returns>IQueryBuilder</returns>
        private static IQueryBuilder InsertPlannedWithOffset(IQueryBuilder builder)
        {
            return
                builder
                    .E(p => p
                        .If(Dbms.SqlServer,
                            iv => iv.Q(" DATEADD(minute, ").C("innerTable", "TimeZoneUtcOffsetMinutes").Q(", ")
                                .C("innerTable", "Planned").Q(") "))
                        .ElseIf(Dbms.PostgreSql,
                            iv => iv.C("innerTable", "Planned").Add().Q("(")
                                .C("innerTable", "TimeZoneUtcOffsetMinutes").Q(" * interval '1 minute') "))
                        .ElseThrow());
        }

        /// <summary>
        /// Получение информации по заданиям доп. согласования
        /// </summary>
        /// <param name="db">DbManager</param>
        /// <param name="builderFactory">IQueryBuilderFactory</param>
        /// <param name="mainTaskID">ID основного задания</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Список записей для авто-завершения</returns>
        private static async Task<List<KrAutoApproveAdditionalTaskRecord>> GetAdditionalApprovalInfoAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            Guid mainTaskID,
            CancellationToken cancellationToken = default)
        {
            db
                .SetCommand(
                    builderFactory
                        .Select()
                            .C("aai", "RowID", "UserID", "UserName")
                            .C("pr", "Position")
                            .C("aai", "Answer", "Completed", "IsResponsible", "OptionID")
                        .From("KrAdditionalApprovalInfo", "aai").NoLock()
                        .LeftJoin("PersonalRoles", "pr").NoLock()
                            .On().C("aai", "UserID").Equals().C("pr", "ID")
                        .Where().C("aai", "ID").Equals().P("MainTaskID")
                        .Build(),
                    db.Parameter("MainTaskID", mainTaskID))
                .LogCommand();

            var result = new List<KrAutoApproveAdditionalTaskRecord>();
            await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    DateTime? completionDate = reader.GetNullableDateTimeUtc(5);

                    result.Add(
                        new KrAutoApproveAdditionalTaskRecord(
                            reader.GetValue<Guid>(0),
                            reader.GetValue<string>(2),
                            reader.GetValue<string>(3),
                            reader.GetValue<bool>(6),
                            reader.GetValue<string>(4),
                            completionDate != null && completionDate != DateTime.MinValue,
                            reader.GetValue<Guid?>(7)));
                }
            }

            return result;
        }

        /// <summary>
        /// Добавление записи в историю авто-согласования
        /// </summary>
        /// <param name="db">DbManager</param>
        /// <param name="builderFactory">IQueryBuilderFactory</param>
        /// <param name="cardID">ID карточки</param>
        /// <param name="taskID">ID задания</param>
        /// <param name="cardTypeID">ID типа карточки</param>
        /// <param name="cardTypeCaption">Имя типа карточки</param>
        /// <param name="cardDigest">Дайджест карточки</param>
        /// <param name="comment">Комментарий</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private static Task AddRowToAutoApproveHistoryAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            Guid cardID,
            Guid taskID,
            Guid cardTypeID,
            string cardTypeCaption,
            string cardDigest,
            string comment,
            CancellationToken cancellationToken = default) =>
            db
                .SetCommand(
                    builderFactory
                        .InsertInto("KrAutoApproveHistory",
                            "ID", "CardID", "CardTypeID", "CardTypeCaption", "CardDigest", "Date", "UserID", "Comment")
                        .Select()
                            .P("TaskID").P("CardID", "CardTypeID", "CardTypeCaption", "CardDigest", "Date").C("ru", "UserID").P("Comment")
                        .From("Tasks", "tsk").NoLock()
                        .InnerJoin("TaskAssignedRoles", "tar").NoLock()
                            .On().C("tsk", "RowID").Equals().C("tar", "ID")
                        .InnerJoin("RoleUsers", "ru").NoLock()
                            .On().C("tar", "RoleID").Equals().C("ru", "ID")
                        .Where()
                            .C("tsk", "RowID").Equals().P("TaskID")
                        .Build(),
                    db.Parameter("CardID", cardID),
                    db.Parameter("TaskID", taskID),
                    db.Parameter("CardTypeID", cardTypeID),
                    db.Parameter("CardTypeCaption", cardTypeCaption),
                    db.Parameter("CardDigest", SqlHelper.LimitString(cardDigest, 255)),
                    db.Parameter("Date", DateTime.UtcNow),
                    db.Parameter("Comment", comment))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);

        /// <summary>
        /// Получаем комментарий для автосогласования.
        /// </summary>
        /// <param name="responsibleTask">Задание ответственного доп. согласующего.</param>
        /// <param name="approveTask">Завершаемое задание.</param>
        /// <returns>Комментарий.</returns>
        private static string GetAutoApproveComment(KrAutoApproveAdditionalTaskRecord responsibleTask, KrAutoApproveTaskRecord approveTask)
        {
            if (responsibleTask == null || !responsibleTask.IsCompleted)
            {
                return approveTask.ApprovalComment;
            }

            // Если есть завершенное задание ответственного доп. согласующего – то результатом
            // задания основного согласующего становится результат задания доп.согласующего.
            // При этом система в комментарии пишет что - то вроде "Автоматически завершено системой
            // согласно мнению ответственного доп.согласующего, ФИО: тут ФИО доп.согласующего (в скобках его должность),
            // Комментарий: тут комментарий доп.согласующего, если таковой был".

            StringBuilder text =
                StringBuilderHelper.Acquire()
                    .Append("{$UI_Tasks_AutoApprovedMessageWithAdditionalApprove}, ")
                    .Append(responsibleTask.UserName)
                ;

            if (!string.IsNullOrWhiteSpace(responsibleTask.UserPosition))
            {
                text
                    .Append(" (")
                    .Append(responsibleTask.UserPosition)
                    .Append(')')
                    ;
            }

            if (!string.IsNullOrWhiteSpace(responsibleTask.Сomment))
            {
                text
                    .Append(": ")
                    .Append(responsibleTask.Сomment)
                    ;
            }

            return text.ToStringAndRelease();
        }

        /// <summary>
        /// Получаем вариант завершения
        /// </summary>
        /// <param name="responsibleTaskOrNull">Задание ответственного доп. согласующего.</param>
        /// <returns>Вариант завершения</returns>
        private static Guid GetCompletionOption(KrAutoApproveAdditionalTaskRecord responsibleTaskOrNull) =>
            responsibleTaskOrNull != null &&
            responsibleTaskOrNull.IsCompleted &&
            responsibleTaskOrNull.OptionID.HasValue
                ? responsibleTaskOrNull.OptionID.Value
                : DefaultCompletionOptions.Approve;

        #endregion
    }
}
