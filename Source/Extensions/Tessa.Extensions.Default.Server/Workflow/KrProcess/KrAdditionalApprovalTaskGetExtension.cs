using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение на получение карточки. Заполняет виртуальные секции отображающие информацию о дополнительных согласованиях.
    /// </summary>
    public sealed class KrAdditionalApprovalCardGetExtension :
        CardGetExtension
    {
        #region ChildResolutionInfo Private Class

        private sealed class AdditionalApprovalInfo
        {
            #region Constructors

            public AdditionalApprovalInfo(
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
                string optionCaption,
                bool isResponsible)
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
                this.IsResponsible = isResponsible;
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

            public bool IsResponsible { get; }

            #endregion
        }

        #endregion

        #region Private Methods

        private static async Task<List<AdditionalApprovalInfo>> TryGetParentInfoAsync(
            CardTask task,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            db
                .SetCommand(
                    builderFactory
                        .Select()
                        .C(
                            null,
                            KrAdditionalApprovalInfo.RowID,
                            KrAdditionalApprovalInfo.Comment,
                            KrAdditionalApprovalInfo.Answer,
                            KrAdditionalApprovalInfo.Created,
                            KrAdditionalApprovalInfo.Planned,
                            KrAdditionalApprovalInfo.PerformerID,
                            KrAdditionalApprovalInfo.PerformerName,
                            KrAdditionalApprovalInfo.InProgress,
                            KrAdditionalApprovalInfo.UserID,
                            KrAdditionalApprovalInfo.UserName,
                            KrAdditionalApprovalInfo.Completed,
                            KrAdditionalApprovalInfo.OptionID,
                            KrAdditionalApprovalInfo.OptionCaption,
                            KrAdditionalApprovalInfo.IsResponsible)
                        .From(KrAdditionalApprovalInfo.Name).NoLock()
                        .Where().C("ID").Equals().P("TaskRowID")
                        .Build(),
                    db.Parameter("TaskRowID", task.ParentRowID))
                .LogCommand();

            var result = new List<AdditionalApprovalInfo>();
            await using (var reader = await db.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    result.Add(new AdditionalApprovalInfo(
                        reader.GetGuid(0),
                        reader.GetValue<string>(1),
                        reader.GetValue<string>(2),
                        reader.GetDateTimeUtc(3),
                        reader.GetDateTimeUtc(4),
                        reader.GetGuid(5),
                        reader.GetValue<string>(6),
                        reader.GetNullableDateTimeUtc(7),
                        reader.GetNullableGuid(8),
                        reader.GetValue<string>(9),
                        reader.GetNullableDateTimeUtc(10),
                        reader.GetNullableGuid(11),
                        reader.GetValue<string>(12),
                        reader.GetBoolean(13)));
                }
            }

            return result;
        }

        private static List<AdditionalApprovalInfo> TryGetChildrenInfo(CardTask task)
        {
            Card taskCard = task.TryGetCard();
            StringDictionaryStorage<CardSection> taskSections;
            ListStorage<CardRow> childrenRows;
            if (taskCard == null
                || (taskSections = taskCard.TryGetSections()) == null
                || !taskSections.TryGetValue(KrAdditionalApprovalInfo.Name, out CardSection childrenSection)
                || (childrenRows = childrenSection.TryGetRows()) == null
                || childrenRows.Count == 0)
            {
                return null;
            }

            var result = new List<AdditionalApprovalInfo>(childrenRows.Count);
            foreach (CardRow childrenRow in childrenRows)
            {
                result.Add(
                    new AdditionalApprovalInfo(
                        childrenRow.RowID,
                        childrenRow.Get<string>(KrAdditionalApprovalInfo.Comment),
                        childrenRow.Get<string>(KrAdditionalApprovalInfo.Answer),
                        childrenRow.Get<DateTime>(KrAdditionalApprovalInfo.Created),
                        childrenRow.Get<DateTime>(KrAdditionalApprovalInfo.Planned),
                        childrenRow.Get<Guid>(KrAdditionalApprovalInfo.PerformerID),
                        childrenRow.Get<string>(KrAdditionalApprovalInfo.PerformerName),
                        childrenRow.Get<DateTime?>(KrAdditionalApprovalInfo.InProgress),
                        childrenRow.Get<Guid?>(KrAdditionalApprovalInfo.UserID),
                        childrenRow.Get<string>(KrAdditionalApprovalInfo.UserName),
                        childrenRow.Get<DateTime?>(KrAdditionalApprovalInfo.Completed),
                        childrenRow.Get<Guid?>(KrAdditionalApprovalInfo.OptionID),
                        childrenRow.Get<string>(KrAdditionalApprovalInfo.OptionCaption),
                        childrenRow.Get<bool>(KrAdditionalApprovalInfo.IsResponsible)));
            }

            return result;
        }

        private static void SetAdditionalApprovalInfoToVirtual(
            CardTask task,
            IReadOnlyCollection<AdditionalApprovalInfo> infoList,
            string sectionName,
            Action<AdditionalApprovalInfo, CardRow> storeHandler)
        {
            if (infoList == null || infoList.Count == 0)
            {
                return;
            }

            CardSection virtualSection = task.Card.Sections.GetOrAdd(sectionName);
            virtualSection.Type = CardSectionType.Table;
            virtualSection.TableType = CardTableType.Collection;

            ListStorage<CardRow> rows = virtualSection.Rows;
            rows.Clear();

            foreach (AdditionalApprovalInfo info in infoList.OrderBy(x => x.Created))
            {
                CardRow row = rows.Add();
                storeHandler(info, row);
                // по умолчанию уже установлено: row.State = CardRowState.None;
            }
        }

        private static void StoreAdditionalApprovalInfo(
            AdditionalApprovalInfo info,
            CardRow row)
        {
            row.RowID = info.RowID;
            row[KrAdditionalApprovalBase.Comment] = info.Comment;
            row[KrAdditionalApprovalBase.Answer] = LocalizationManager.Format(info.Answer);
            row[KrAdditionalApprovalBase.Created] = info.Created;
            row[KrAdditionalApprovalBase.Planned] = info.Planned;
            row[KrAdditionalApprovalBase.PerformerID] = info.PerformerID;
            row[KrAdditionalApprovalBase.PerformerName] = info.PerformerName;
            row[KrAdditionalApprovalBase.InProgress] = info.InProgress;
            row[KrAdditionalApprovalBase.UserID] = info.UserID;
            row[KrAdditionalApprovalBase.UserName] = info.UserName;
            row[KrAdditionalApprovalBase.Completed] = info.Completed;
            row[KrAdditionalApprovalBase.OptionID] = info.OptionID;
            row[KrAdditionalApprovalBase.OptionCaption] = info.OptionCaption;
            row[KrAdditionalApprovalsRequestedInfo.ColumnComment] = GetColumnComment(info);
            row[KrAdditionalApprovalsRequestedInfo.ColumnState] = GetColumnState(info);
            row[KrAdditionalApprovalInfo.IsResponsible] = BooleanBoxes.Box(info.IsResponsible);
        }

        private static string GetColumnComment(AdditionalApprovalInfo info)
        {
            return info.Comment
                .ReplaceLineEndingsAndTrim().NormalizeSpaces();
        }

        private static string GetColumnState(AdditionalApprovalInfo info)
        {
            return GetState(
                info.PerformerName,
                info.UserID.HasValue
                    ? info.UserName
                    : null,
                info.OptionID);
        }

        private static string GetState(
            string performerRoleName,
            string userName,
            Guid? completionOptionID)
        {
            string state;
            if (completionOptionID.HasValue)
            {
                state = completionOptionID.Value == DefaultCompletionOptions.Revoke
                    ? string.Format(LocalizationManager.GetString("WfResolution_State_Revoked"), LocalizationManager.Localize(userName))
                    : string.Format(LocalizationManager.GetString("Cards_TaskState_Completed"), LocalizationManager.Localize(userName));
            }
            else if (userName != null)
            {
                state = string.Format(LocalizationManager.GetString("Cards_TaskState_InProgress"), LocalizationManager.Localize(userName));
            }
            else
            {
                state = string.Format(LocalizationManager.GetString("Cards_TaskState_Created"), LocalizationManager.Localize(performerRoleName));
            }

            return state;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || context.CardType is null
                || context.CardType.Flags.HasNot(CardTypeFlags.AllowTasks)
                || context.Request.RestrictionFlags.Has(CardGetRestrictionFlags.RestrictTaskSections)
                || (card = context.Response.TryGetCard()) is null)
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                ListStorage<CardTask> tasks = card.TryGetTasks();

                var db = context.DbScope.Db;
                var builderFactory = context.DbScope.BuilderFactory;

                if (tasks != null && tasks.Count > 0)
                {
                    foreach (CardTask task in tasks)
                    {
                        var taskTypeID = task.TypeID;

                        if (taskTypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID)
                        {
                            List<AdditionalApprovalInfo> infoList = await TryGetParentInfoAsync(task, db, builderFactory, context.CancellationToken);
                            SetAdditionalApprovalInfoToVirtual(task, infoList, KrAdditionalApprovalInfo.Virtual, StoreAdditionalApprovalInfo);

                            infoList = TryGetChildrenInfo(task);
                            SetAdditionalApprovalInfoToVirtual(task, infoList, KrAdditionalApprovalsRequestedInfo.Virtual, StoreAdditionalApprovalInfo);
                        }
                        else if (taskTypeID == DefaultTaskTypes.KrApproveTypeID
                                 || taskTypeID == DefaultTaskTypes.KrSigningTypeID)
                        {
                            List<AdditionalApprovalInfo> infoList = TryGetChildrenInfo(task);
                            SetAdditionalApprovalInfoToVirtual(task, infoList, KrAdditionalApprovalInfo.Virtual, StoreAdditionalApprovalInfo);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
