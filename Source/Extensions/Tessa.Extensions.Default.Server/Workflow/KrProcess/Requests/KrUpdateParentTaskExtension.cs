using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    public class KrUpdateParentTaskExtension : CardStoreTaskExtension
    {
        #region private

        private static async Task InsertNewCommentAsync(ICardStoreTaskExtensionContext context)
        {
            // Берём первую роль-исполнителя. Если по каким-то причинам исполнителя нет (что не корректно для данного типа задания), берём основную роль задания.
            var commentator = context.Task.TaskAssignedRoles.FirstOrDefault(x => x.TaskRoleID == CardFunctionRoles.PerformerID && x.ParentRowID is null)
                ?? CardComponentHelper.TryGetMasterTaskAssignedRole(
                    context.Task,
                    context.ValidationResult,
                    typeof(KrUpdateParentTaskExtension));

            if (commentator is not null)
            {
                var db = context.DbScope.Db;
                await db.SetCommand(
                        context.DbScope.BuilderFactory
                            .InsertInto(KrCommentsInfo.Name,
                                KrCommentsInfo.ID,
                                KrCommentsInfo.RowID,
                                KrCommentsInfo.Question,
                                KrCommentsInfo.CommentatorID,
                                KrCommentsInfo.CommentatorName)
                            .Values(b => b.P("ID", "RowID", "Question", "CommentatorID", "CommentatorName").N())
                            .Build(),
                        db.Parameter("ID", context.Task.ParentRowID),
                        db.Parameter("RowID", context.Task.RowID),
                        db.Parameter("Question", context.Task.Digest ?? string.Empty),
                        db.Parameter("CommentatorID", commentator.RoleID),
                        db.Parameter("CommentatorName", commentator.RoleName ?? string.Empty))
                    .LogCommand()
                    .ExecuteNonQueryAsync(context.CancellationToken);
            }
        }

        private static Task UpdateCommentWithAnswerAsync(CardTask commentTask, IDbScope dbScope, CancellationToken cancellationToken = default)
        {
            var db = dbScope.Db;
            return db.SetCommand(
                    dbScope.BuilderFactory
                        .Update(KrCommentsInfo.Name)
                            .C(KrCommentsInfo.Answer).Assign().P("Answer")
                            .C(KrCommentsInfo.CommentatorID).Assign().P("CommentatorID")
                            .C(KrCommentsInfo.CommentatorName).Assign().P("CommentatorName")
                        .Where().C("RowID").Equals().P("RowID")
                        .Build(),
                    db.Parameter("Answer", commentTask.Card.Sections[KrRequestComment.Name].Fields.TryGet<string>(KrRequestComment.Comment)),
                    db.Parameter("CommentatorID", commentTask.UserID),
                    db.Parameter("CommentatorName", commentTask.UserName),
                    db.Parameter("RowID", commentTask.RowID))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);
        }

        private static Task CancelCommentAsync(CardTask commentTask, IDbScope dbScope, CancellationToken cancellationToken = default)
        {
            var db = dbScope.Db;
            return db.SetCommand(
                    dbScope.BuilderFactory
                        .DeleteFrom(KrCommentsInfo.Name)
                        .Where().C(KrCommentsInfo.RowID).Equals().P("RowID")
                        .Build(),
                    db.Parameter("RowID", commentTask.RowID))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);
        }

        #endregion

        #region base overrides

        public override async Task StoreTaskBeforeCommitTransaction(ICardStoreTaskExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            if (context.Task.TypeID == DefaultTaskTypes.KrRequestCommentTypeID)
            {
                if (!context.Task.ParentRowID.HasValue)
                {
                    throw new InvalidOperationException("Comment task doesn't contain ParentRowID.");
                }

                if (context.State == CardRowState.Inserted)
                {
                    await InsertNewCommentAsync(context);
                }
                else if (context.Action == CardTaskAction.Complete
                    && context.CompletionOption.ID == DefaultCompletionOptions.AddComment)
                {
                    await UpdateCommentWithAnswerAsync(context.Task, context.DbScope, context.CancellationToken);
                }
                else if (context.Action == CardTaskAction.Complete
                    && context.CompletionOption.ID == DefaultCompletionOptions.Cancel)
                {
                    await CancelCommentAsync(context.Task, context.DbScope, context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
