using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    /// <summary>
    /// Загрузка карточки с заполнением виртуальных секций в её заданиях.
    /// Метод выполняется при загрузке для всех карточек во всех режимах CardGetMethod, кроме Storage,
    /// но действительную работу выполняет только для заданий Workflow, у которых загружены секции.
    /// </summary>
    public sealed class WfTasksServerGetExtension :
        CardGetExtension
    {
        #region Constructors

        public WfTasksServerGetExtension(KrSettingsLazy settingsLazy)
        {
            this.settingsLazy = settingsLazy;
        }

        #endregion

        #region ChildResolutionInfo Private Class

        private sealed class ChildResolutionInfo
        {
            #region Constructors

            public ChildResolutionInfo(
                Guid rowID,
                string comment,
                string answer,
                DateTime created,
                DateTime planned,
                Guid performerID,
                string performerName,
                DateTime? inProgress,
                Guid? userID,
                string userName,
                DateTime? completed,
                Guid? optionID,
                string optionCaption)
            {
                this.RowID = rowID;
                this.Comment = comment;
                this.Answer = answer;
                this.Created = created;
                this.Planned = planned;
                this.PerformerID = performerID;
                this.PerformerName = performerName;
                this.InProgress = inProgress;
                this.UserID = userID;
                this.UserName = userName;
                this.Completed = completed;
                this.OptionID = optionID;
                this.OptionCaption = optionCaption;
            }

            #endregion

            #region Properties

            public Guid RowID { get; }

            public string Comment { get; }

            public string Answer { get; }

            public DateTime Created { get; }

            public DateTime Planned { get; }

            public Guid PerformerID { get; }

            public string PerformerName { get; }

            public DateTime? InProgress { get; }

            public Guid? UserID { get; }

            public string UserName { get; }

            public DateTime? Completed { get; }

            public Guid? OptionID { get; }

            public string OptionCaption { get; }

            #endregion
        }

        #endregion

        #region Fields

        private readonly KrSettingsLazy settingsLazy;

        #endregion

        #region Private Methods

        private static string TryGetTitleFromSection(CardTask task)
        {
            Card taskCard = task.TryGetCard();
            StringDictionaryStorage<CardSection> taskSections;
            return taskCard != null
                   && (taskSections = taskCard.TryGetSections()) != null
                   && taskSections.TryGetValue(WfHelper.CommonInfoSection, out CardSection commonInfoSection)
                ? commonInfoSection.RawFields.TryGet<string>(WfHelper.CommonInfoKindCaptionField)
                : null;
        }


        private static Task<string> TryGetTitleFromDbAsync(
            CardTask task,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            return db
                .SetCommand(
                    builderFactory
                        .Select().C("KindCaption")
                        .From("TaskCommonInfo").NoLock()
                        .Where().C("ID").Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", task.RowID))
                .LogCommand()
                .ExecuteAsync<string>(cancellationToken);
        }


        private static async Task<Dictionary<Guid, int>> GetFileCountByTaskRowIDFromDbAsync(
            Guid cardID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            // возвращает список RowID задания -> кол-во файлов в карточке задания
            var result = new Dictionary<Guid, int>();

            db
                .SetCommand(builderFactory
                    .Select().C("t", CardSatelliteHelper.TaskRowIDColumn).Count()
                    .From(CardSatelliteHelper.SatellitesSectionName, "t").NoLock()
                    .InnerJoin("Files", "f").NoLock()
                        .On().C("f", "ID").Equals().C("t", "ID")
                    .Where().C("t", CardSatelliteHelper.MainCardIDColumn).Equals().P("CardID")
                        .And().C("t", CardSatelliteHelper.SatelliteTypeIDColumn).Equals().V(DefaultCardTypes.WfTaskCardTypeID)
                    .GroupBy("t", CardSatelliteHelper.TaskRowIDColumn)
                    .Build(),
                    db.Parameter("CardID", cardID))
                .LogCommand();

            await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    Guid taskRowID = reader.GetGuid(0);
                    int count = reader.GetInt32(1);

                    result[taskRowID] = count;
                }
            }

            return result;
        }


        private static List<ChildResolutionInfo> TryGetChildrenInfo(CardTask task)
        {
            Card taskCard = task.TryGetCard();
            StringDictionaryStorage<CardSection> taskSections;
            ListStorage<CardRow> childrenRows;
            if (taskCard == null
                || (taskSections = taskCard.TryGetSections()) == null
                || !taskSections.TryGetValue(WfHelper.ResolutionChildrenSection, out CardSection childrenSection)
                || (childrenRows = childrenSection.TryGetRows()) == null
                || childrenRows.Count == 0)
            {
                return null;
            }

            var result = new List<ChildResolutionInfo>(childrenRows.Count);
            foreach (CardRow childrenRow in childrenRows)
            {
                result.Add(
                    new ChildResolutionInfo(
                        childrenRow.RowID,
                        childrenRow.Get<string>("Comment"),
                        childrenRow.Get<string>("Answer"),
                        childrenRow.Get<DateTime>("Created"),
                        childrenRow.Get<DateTime>("Planned"),
                        childrenRow.Get<Guid>("PerformerID"),
                        childrenRow.Get<string>("PerformerName"),
                        childrenRow.Get<DateTime?>("InProgress"),
                        childrenRow.Get<Guid?>("UserID"),
                        childrenRow.Get<string>("UserName"),
                        childrenRow.Get<DateTime?>("Completed"),
                        childrenRow.Get<Guid?>("OptionID"),
                        childrenRow.Get<string>("OptionCaption")));
            }

            return result;
        }


        private async ValueTask SetChildrenInfoToVirtualAsync(
            CardTask task,
            List<ChildResolutionInfo> infoList,
            CancellationToken cancellationToken = default)
        {
            if (infoList == null || infoList.Count == 0)
            {
                return;
            }

            CardSection virtualSection = task.Card.Sections.GetOrAdd(WfHelper.ResolutionChildrenVirtualSection);
            virtualSection.Type = CardSectionType.Table;
            virtualSection.TableType = CardTableType.Collection;

            ListStorage<CardRow> rows = virtualSection.Rows;
            rows.Clear();

            foreach (ChildResolutionInfo info in infoList.OrderBy(x => x.Created))
            {
                CardRow row = rows.Add();
                await this.StoreChildResolutionInfoAsync(info, row, cancellationToken);
                // по умолчанию уже установлено: row.State = CardRowState.None;
            }
        }


        private async ValueTask StoreChildResolutionInfoAsync(
            ChildResolutionInfo info,
            CardRow row,
            CancellationToken cancellationToken = default)
        {
            row.RowID = info.RowID;
            row["Comment"] = info.Comment;
            row["Answer"] = LocalizationManager.Format(info.Answer);
            row["Created"] = info.Created;
            row["Planned"] = info.Planned;
            row["PerformerID"] = info.PerformerID;
            row["PerformerName"] = info.PerformerName;
            row["InProgress"] = info.InProgress;
            row["UserID"] = info.UserID;
            row["UserName"] = info.UserName;
            row["Completed"] = info.Completed;
            row["OptionID"] = info.OptionID;
            row["OptionCaption"] = info.OptionCaption;
            row["ColumnComment"] = await this.GetColumnCommentAsync(info, cancellationToken);
            row["ColumnState"] = await this.GetColumnStateAsync(info, cancellationToken);
        }


        private async ValueTask<string> GetColumnCommentAsync(
            ChildResolutionInfo info,
            CancellationToken cancellationToken = default)
        {
            return info.Comment
                .NormalizeComment()
                .Limit((await this.settingsLazy.GetValueAsync(cancellationToken))
                    .ChildResolutionColumnCommentMaxLength);
        }


        private async ValueTask<string> GetColumnStateAsync(
            ChildResolutionInfo info,
            CancellationToken cancellationToken = default)
        {
            return WfHelper.GetResolutionState(
                await this.settingsLazy.GetValueAsync(cancellationToken),
                info.PerformerName,
                info.UserID.HasValue ? info.UserName : null,
                info.OptionID);
        }


        private static Guid? TryGetMainCardIDFromTaskCard(Card card)
        {
            StringDictionaryStorage<CardSection> sections = card.TryGetSections();

            return sections != null
                && sections.TryGetValue(CardSatelliteHelper.SatellitesSectionName, out var section)
                    ? section.RawFields.TryGet<Guid?>(CardSatelliteHelper.MainCardIDColumn)
                    : null;
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || context.CardType == null
                || context.CardType.Flags.HasNot(CardTypeFlags.AllowTasks)
                || context.Request.RestrictionFlags.Has(CardGetRestrictionFlags.RestrictTaskSections)
                || (card = context.Response.TryGetCard()) == null)
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                ListStorage<CardTask> tasks = card.TryGetTasks();
                bool hasTasks = false;

                if (tasks != null && tasks.Count > 0)
                {
                    foreach (CardTask task in tasks)
                    {
                        if (WfHelper.TaskTypeIsResolution(task.TypeID))
                        {
                            hasTasks = true;

                            if (task.IsLocked)
                            {
                                string title = await TryGetTitleFromDbAsync(task, context.DbScope.Db, context.DbScope.BuilderFactory, context.CancellationToken);
                                if (!string.IsNullOrEmpty(title))
                                {
                                    // если в строке NULL, то ExecuteScalar<string> вернёт пустую строку
                                    task.SetTitle(title);
                                }
                            }
                            else
                            {
                                string title = TryGetTitleFromSection(task);
                                if (title != null)
                                {
                                    task.SetTitle(title);
                                }

                                List<ChildResolutionInfo> infoList = TryGetChildrenInfo(task);
                                await this.SetChildrenInfoToVirtualAsync(task, infoList, context.CancellationToken);

                                Dictionary<string, object> fields = task.Card.Sections[WfHelper.ResolutionVirtualSection].RawFields;
                                fields[WfHelper.ResolutionVirtualPlannedField] = task.Planned;
                                fields[WfHelper.ResolutionVirtualDigestField] = await LocalizationManager.FormatAsync(task.Digest);
                            }
                        }
                    }
                }

                ListStorage<CardTaskHistoryItem> taskHistory = card.TryGetTaskHistory();
                bool isTaskCard = context.CardTypeIs(DefaultCardTypes.WfTaskCardTypeID);
                bool hasTaskHistory = false;

                if (taskHistory != null && taskHistory.Count > 0)
                {
                    Dictionary<Guid, CardTaskHistoryItem> historyItemsByRowID = null;

                    foreach (CardTaskHistoryItem item in taskHistory)
                    {
                        if (WfHelper.TaskTypeIsResolution(item.TypeID))
                        {
                            hasTaskHistory = true;

                            if (historyItemsByRowID == null)
                            {
                                historyItemsByRowID = new Dictionary<Guid, CardTaskHistoryItem>();
                            }

                            // если вдруг в TaskHistory расширения дописали записи с такими же RowID,
                            // то мы будем учитывать последнюю найденную запись
                            historyItemsByRowID[item.RowID] = item;
                        }
                    }

                    if (historyItemsByRowID != null)
                    {
                        Guid cardID = isTaskCard
                            ? TryGetMainCardIDFromTaskCard(card) ?? card.ID
                            : card.ID;

                        await WfHelper.LoadHistoryWorkflowInfoAsync(
                            cardID,
                            historyItemsByRowID,
                            context.DbScope.Db,
                            context.DbScope.BuilderFactory,
                            cancellationToken: context.CancellationToken);
                    }
                }

                // получаем информацию по файлам задач, если есть хотя бы одна задача
                if (!isTaskCard && (hasTasks || hasTaskHistory))
                {
                    Dictionary<Guid, int> fileCountByTaskRowID = await GetFileCountByTaskRowIDFromDbAsync(
                        card.ID, context.DbScope.Db, context.DbScope.BuilderFactory, context.CancellationToken);

                    if (fileCountByTaskRowID.Count > 0)
                    {
                        if (hasTasks)
                        {
                            foreach (CardTask task in tasks)
                            {
                                if (fileCountByTaskRowID.TryGetValue(task.RowID, out int fileCount))
                                {
                                    task.Info[WfHelper.FileCountTaskKey] = fileCount;
                                }
                            }
                        }

                        if (hasTaskHistory)
                        {
                            foreach (CardTaskHistoryItem historyItem in taskHistory)
                            {
                                if (fileCountByTaskRowID.TryGetValue(historyItem.RowID, out int fileCount))
                                {
                                    historyItem.Info[WfHelper.FileCountTaskKey] = fileCount;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
