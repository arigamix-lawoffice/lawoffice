using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class FixTaskHistoryCardStoreExtension : CardStoreExtension
    {
        #region Base Overrides

        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            var card = context.Request.TryGetCard();
            var tasks = card?.TryGetTasks();
            if (tasks is null || tasks.Count == 0)
            {
                return;
            }

            var anyComplettionApprovalTask = tasks.FirstOrDefault(p =>
                (p.Action == CardTaskAction.Complete) &&
                (p.TypeID == DefaultTaskTypes.KrApproveTypeID || p.TypeID == DefaultTaskTypes.KrSigningTypeID));

            var anyComplettionAdditionalApprovalTask = tasks.FirstOrDefault(p =>
                p.Action == CardTaskAction.Complete &&
                p.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID &&
                p.OptionID == DefaultCompletionOptions.Revoke);

            // Если просто отозвали задание доп. согласования
            if (anyComplettionAdditionalApprovalTask is not null &&
                anyComplettionApprovalTask is null)
            {
                var parentCommentsTasks = tasks.Where(p =>
                    p.ParentRowID == anyComplettionAdditionalApprovalTask.RowID &&
                    p.TypeID == DefaultTaskTypes.KrRequestCommentTypeID);
                // Обновляем дочерние задания комментирования
                foreach (var parrentCommentsTask in parentCommentsTasks)
                {
                    await UpdateHistoryRecordAsync(context.DbScope, parrentCommentsTask.RowID, context.CancellationToken);
                }
            }

            // Если есть завершаемй таск согласования
            if (anyComplettionApprovalTask is not null)
            {
                // Находим дочерние задания комментирования и доп. согласования и обновляем им дату на более позднюю
                var additionalApprovalOrCommentsTasks = tasks.Where(p =>
                    (p.ParentRowID == anyComplettionApprovalTask.RowID) &&
                    (p.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID || p.TypeID == DefaultTaskTypes.KrRequestCommentTypeID));

                foreach (var parentTasks in additionalApprovalOrCommentsTasks)
                {
                    // Обновляем дату завершения доп. согласования
                    await UpdateHistoryRecordAsync(context.DbScope, parentTasks.RowID, context.CancellationToken);
                    // Ищим дочерние таски комментирования
                    var parrentCommentsTasks = tasks.Where(p =>
                        p.ParentRowID == parentTasks.RowID &&
                        p.TypeID == DefaultTaskTypes.KrRequestCommentTypeID);
                    // Обновляем дочерние задания комментирования
                    foreach (var parrentCommentsTask in parrentCommentsTasks)
                    {
                        await UpdateHistoryRecordAsync(context.DbScope, parrentCommentsTask.RowID, context.CancellationToken);
                    }
                }

                // Если есть задание редактирование
                if (tasks.Any(p => p.TypeID == DefaultTaskTypes.KrEditTypeID) && card.Info.ContainsKey("satellite"))
                {
                    // Не менять "satellite" на KrWorkflowHelper.SatelliteKey, приведёт к сложно отслежтваемым багам с дубликатами записей в истории
                    var satellite = new Card(card.Info.Get<Dictionary<string, object>>("satellite"));
                    CardRow currentStage = null;
                    // Ищем текущий этап согласования
                    var currentStageRowID =
                        satellite.Sections[KrConstants.KrApprovalCommonInfo.Name].RawFields.Get<Guid?>(
                            KrConstants.KrProcessCommonInfo.CurrentApprovalStageRowID);
                    foreach (var stage in satellite.Sections[KrConstants.KrStages.Virtual].Rows)
                    {
                        if (currentStageRowID.HasValue && stage.RowID == currentStageRowID.Value ||
                            stage.IsChanged(KrConstants.KrStages.StateID))
                        {
                            currentStage = stage;
                            break;
                        }
                    }

                    if (currentStage is null)
                    {
                        context.ValidationResult.AddError(this, "$KrMessages_CantFindCurrentApprovalStage");
                        return;
                    }

                    // Если нужно - находим запись об возврате на доработку и обновляем ей дату.
                    if (currentStage.Fields.Get<bool>("IsParallel") ||
                        anyComplettionApprovalTask.OptionID == DefaultCompletionOptions.Disapprove ||
                        anyComplettionApprovalTask.OptionID == DefaultCompletionOptions.Decline)
                    {
                        await UpdateKrRebuildHistoryRecordAsync(context.DbScope, card.ID, context.CancellationToken);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Обновляет дату создания и завершения для записи о возврате на доработку редактирования
        /// </summary>
        /// <param name="dbScope">IDbScope</param>
        /// <param name="cardID">ID карточки</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private static async Task UpdateKrRebuildHistoryRecordAsync(IDbScope dbScope, Guid cardID, CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;

                await db.SetCommand(
                        builderFactory
                            .Update("TaskHistory")
                                .C("Completed").Assign().P("Time")
                                .C("Created").Assign().P("Time")
                            .Where().C("RowID").Equals().E(b => b
                                    .Select().Top(1).C("RowID")
                                    .From("TaskHistory").NoLock()
                                    .Where()
                                        .C("ID").Equals().P("CardID")
                                        .And().C("TypeID").Equals().P("TaskTypeID")
                                    .OrderBy("Completed", SortOrder.Descending).By("Created", SortOrder.Descending)
                                    .Limit(1),
                                multiLine: true)
                            .Build(),
                        dbScope.Db.Parameter("CardID", cardID),
                        dbScope.Db.Parameter("TaskTypeID", DefaultTaskTypes.KrRebuildTypeID),
                        dbScope.Db.Parameter("Time", DateTime.UtcNow))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Обновляет дату завершения для заданий
        /// </summary>
        /// <param name="dbScope">IDbScope</param>
        /// <param name="taskRowID">RowID задания</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private static async Task UpdateHistoryRecordAsync(IDbScope dbScope, Guid taskRowID, CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;

                await db.SetCommand(
                        builderFactory
                            .Update("TaskHistory")
                                .C("Completed").Assign().P("Completed")
                            .Where().C("RowID").Equals().P("RowID")
                            .Build(),
                        dbScope.Db.Parameter("RowID", taskRowID),
                        dbScope.Db.Parameter("Completed", DateTime.UtcNow))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }

        #endregion
    }
}
