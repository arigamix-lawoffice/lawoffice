using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrReassignAdditionalApprovalStoreExtension : CardStoreExtension
    {
        public const string ReassignTo = nameof(ReassignTo);

        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            var card = context.Request.TryGetCard();
            if (card is null)
            {
                return;
            }

            var tasks = card.TryGetTasks();
            if (tasks is null || tasks.Count == 0)
            {
                return;
            }

            foreach (var task in tasks)
            {
                var skip = task.State == CardRowState.None;
                if (skip && task.UpdateState())
                {
                    skip = task.State == CardRowState.None;
                }

                if (skip || task.OptionID != DefaultCompletionOptions.Delegate)
                {
                    continue;
                }

                object reassingToObj = default;
                var reassingTo = task.TryGetInfo()?.TryGetValue(ReassignTo, out reassingToObj) ?? default
                    ? (Guid?)reassingToObj
                    : context.Request.Info.TryGet<Guid?>(ReassignTo);

                if (reassingTo is null)
                {
                    continue;
                }

                await using (context.DbScope.Create())
                {
                    DbManager db = context.DbScope.Db;

                    await db
                        .SetCommand(
                            context.DbScope.BuilderFactory
                                .Update("Tasks")
                                    .C("ParentID").Assign().P("NewParentID")
                                .Where()
                                    .C("ParentID").Equals().P("OldParentID")
                                    .And().C("TypeID").Equals().P("TaskTypeID").Z()
                                .Update("TaskHistory")
                                    .C("ParentRowID").Assign().P("NewParentID")
                                .Where()
                                    .C("ParentRowID").Equals().P("OldParentID")
                                    .And().C("TypeID").Equals().P("HistoryTypeID")
                                .Build(),
                            db.Parameter("NewParentID", reassingTo),
                            db.Parameter("OldParentID", task.RowID),
                            db.Parameter("TaskTypeID", DefaultTaskTypes.KrAdditionalApprovalTypeID),
                            db.Parameter("HistoryTypeID", DefaultTaskTypes.KrInfoAdditionalApprovalTypeID))
                        .LogCommand()
                        .ExecuteNonQueryAsync(context.CancellationToken);
                }
            }
        }
    }
}
