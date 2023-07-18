using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение заполняет виртуальную секцию результатов комментирования, чтобы была возможность
    /// отобржать результаты в таблице в кратком виде, а по двойному клику - в полном
    /// </summary>
    public sealed class FillCommentsVirtualSectionGetExtension :
        CardGetExtension
    {
        #region Private Methods

        private static string GetShortString(string s) => s.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(30);

        #endregion

        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            CardType cardType;
            ListStorage<CardTask> tasks;

            if (!context.RequestIsSuccessful
                || (cardType = context.CardType) == null
                || cardType.Flags.HasNot(CardTypeFlags.AllowTasks)
                || context.Request.RestrictionFlags.Has(CardGetRestrictionFlags.RestrictTaskSections)
                || (card = context.Response.TryGetCard()) == null
                || (tasks = card.TryGetTasks()) == null
                || tasks.Count == 0)
            {
                return Task.CompletedTask;
            }

            ListStorage<CardTaskHistoryItem> taskHistory = card.TaskHistory;

            foreach (var task in card.Tasks.Where(x =>
                x.TypeID == DefaultTaskTypes.KrApproveTypeID ||
                x.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID ||
                x.TypeID == DefaultTaskTypes.KrSigningTypeID))
            {
                if (task.IsLockedEffective)
                {
                    continue;
                }

                StringDictionaryStorage<CardSection> sections = task.Card.Sections;

                ListStorage<CardRow> commentsOriginalRows;
                if (!sections.TryGetValue("KrCommentsInfoVirtual", out CardSection commentsVirtual)
                    || !sections.TryGetValue("KrCommentsInfo", out CardSection commentsOriginal)
                    || (commentsOriginalRows = commentsOriginal.TryGetRows()) == null
                    || commentsOriginalRows.Count == 0)
                {
                    continue;
                }

                ListStorage<CardRow> commentsVirtualRows = commentsVirtual.Rows;
                foreach (CardRow originalRow in commentsOriginalRows)
                {
                    CardRow virtualRow = commentsVirtualRows.Add();

                    Guid originalRowID = originalRow.RowID;
                    virtualRow.RowID = originalRowID;

                    string commentator = originalRow.Get<string>("CommentatorName");
                    virtualRow["CommentatorNameFull"] = commentator;
                    virtualRow["CommentatorNameShort"] = GetShortString(commentator);

                    string question = originalRow.Get<string>("Question");
                    virtualRow["QuestionFull"] = question;
                    virtualRow["QuestionShort"] = GetShortString(question);

                    string answer = originalRow.Get<string>("Answer");
                    virtualRow["AnswerFull"] = answer;
                    virtualRow["AnswerShort"] = GetShortString(answer);

                    CardTaskHistoryItem taskHistoryItem = taskHistory.FirstOrDefault(x => x.RowID == originalRowID);
                    if (taskHistoryItem != null)
                    {
                        virtualRow["Completed"] = taskHistoryItem.Completed;
                    }

                    virtualRow.State = CardRowState.None;
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
