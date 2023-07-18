using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение на сохранение задания дополнительного согласования <see cref="DefaultTaskTypes.KrAdditionalApprovalTypeID"/>.
    /// </summary>
    public sealed class KrAdditionalApprovalCardStoreExtension :
        CardStoreExtension
    {
        #region Private Methods

        private static async Task InsertChildRecordAsync(
            CardTask task,
            Guid parentID,
            DateTime storeDateTime,
            IDbScope dbScope,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            // при вставке задания у него присутствуют все секции,
            // а вся его информация актуальна в объекте task

            // метод следует вызывать только в том случае, если задание parentID ещё не завершено

            Card taskCard = task.Card;
            Dictionary<string, object> taskFields = taskCard.Sections[TaskCommonInfo.Name].RawFields;
            Dictionary<string, object> additionalApprovalFields = taskCard.Sections[KrAdditionalApprovalTaskInfo.Name].RawFields;

            DateTime created = taskCard.Created ?? storeDateTime;
            DateTime planned = task.Planned ?? storeDateTime;
            string parentComment = taskFields.TryGet<string>(TaskCommonInfo.Info);
            bool isResponsible = additionalApprovalFields.TryGet<bool>(KrAdditionalApprovalTaskInfo.IsResponsible);

            // У заданий доп. согласования всегда есть исполнитель. Если его вдруг нет, значит где-то была изменена логика этих задач и она работает некорректно.
            // В этом случае возьмём за исполнителя запись с признаком "Master".
            await CardComponentHelper.FillTaskAssignedRolesAsync(task, dbScope, cancellationToken: cancellationToken);
            
            CardTaskAssignedRole role =
                CardComponentHelper.TryGetMasterTaskAssignedRole(task, validationResult, typeof(KrAdditionalApprovalCardStoreExtension));

            if (role is null)
            {
                return;
            }

            var executor = dbScope.Executor;
            await executor
                .ExecuteNonQueryAsync(
                    dbScope.BuilderFactory
                        .InsertInto(
                            KrAdditionalApprovalInfo.Name,
                            KrAdditionalApprovalInfo.ID,
                            KrAdditionalApprovalInfo.RowID,
                            KrAdditionalApprovalInfo.PerformerID,
                            KrAdditionalApprovalInfo.PerformerName,
                            KrAdditionalApprovalInfo.UserID,
                            KrAdditionalApprovalInfo.UserName,
                            KrAdditionalApprovalInfo.OptionID,
                            KrAdditionalApprovalInfo.OptionCaption,
                            KrAdditionalApprovalInfo.Comment,
                            KrAdditionalApprovalInfo.Answer,
                            KrAdditionalApprovalInfo.Created,
                            KrAdditionalApprovalInfo.Planned,
                            KrAdditionalApprovalInfo.InProgress,
                            KrAdditionalApprovalInfo.Completed,
                            KrAdditionalApprovalInfo.IsResponsible)
                        .Values(v => v
                            .P("ParentID", "TaskID", "RoleID", "RoleName").V(null)
                            .V(null).V(null).V(null).P("Comment").V(null)
                            .P("Created", "Planned").V(null).V(null).P("IsResponsible"))
                        .Build(),
                    cancellationToken,
                    executor.Parameter("ParentID", parentID),
                    executor.Parameter("TaskID", task.RowID),
                    executor.Parameter("RoleID", role.RoleID),
                    executor.Parameter("RoleName", SqlHelper.LimitString(role.RoleName, RoleHelper.RoleNameMaxLength)),
                    executor.Parameter("Comment", parentComment),
                    executor.Parameter("Created", created),
                    executor.Parameter("Planned", planned),
                    executor.Parameter("IsResponsible", BooleanBoxes.Box(isResponsible)));
        }

        private static Task ClearMainTaskInfoAsync(
            Guid parentID,
            IQueryExecutor executor,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            return executor
                .ExecuteNonQueryAsync(
                    builderFactory
                        .DeleteFrom(KrAdditionalApprovalUsers.Name)
                        .Where().C(KrAdditionalApprovalUsers.ID).Equals().P("ParentID").Z()

                        .Update(KrAdditionalApproval.Name)
                            .C(KrAdditionalApproval.FirstIsResponsible).Assign().V(false)
                            .C(KrAdditionalApproval.Comment).Assign().V(null)
                        .Where().C("ID").Equals().P("ParentID")
                        .Build(),
                    cancellationToken,
                    executor.Parameter("ParentID", parentID));
        }


        private static Task<bool> HasTaskAsync(
            Guid taskRowID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            return db
                .SetCommand(
                    builderFactory
                        .Select().V(true)
                        .From("Tasks").NoLock()
                        .Where().C("RowID").Equals().P("RowID")
                        .Build(),
                    db.Parameter("RowID", taskRowID))
                .LogCommand()
                .ExecuteAsync<bool>(cancellationToken);
        }


        private static async Task UpdateChildRecordAsync(
            CardTask task,
            IDbScope dbScope,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var executor = dbScope.Executor;
            var parameters = new List<DataParameter>
            {
                executor.Parameter("TaskID", task.RowID)
            };

            await CardComponentHelper.FillTaskAssignedRolesAsync(task, dbScope, cancellationToken: cancellationToken);

            // Найдём запись в TaskAssignedRoles с пометкой "Master".
            var masterTaskAssignedRole = CardComponentHelper.TryGetMasterTaskAssignedRole(task, validationResult, typeof(KrAdditionalApprovalCardStoreExtension));

            // если даже родительское задание уже завершено, то update не упадёт, а просто не обновит строки
            var builder = dbScope.BuilderFactory
                .Update(KrAdditionalApprovalInfo.Name)
                .C(KrAdditionalApprovalInfo.InProgress).Assign().C("th", "InProgress")
                .C(KrAdditionalApprovalInfo.UserID).Assign().C("th", "UserID")
                .C(KrAdditionalApprovalInfo.UserName).Assign().C("th", "UserName");

            if (task.State == CardRowState.Deleted)
            {
                builder
                    .C(KrAdditionalApprovalInfo.Completed).Assign().C("th", "Completed")
                    .C(KrAdditionalApprovalInfo.OptionID).Assign().C("th", "OptionID")
                    .C(KrAdditionalApprovalInfo.OptionCaption).Assign().C("th", "OptionCaption");
            }

            if (masterTaskAssignedRole is not null)
            {
                builder
                    .C(KrAdditionalApprovalInfo.PerformerID).Assign().P("PerformerRoleID")
                    .C(KrAdditionalApprovalInfo.PerformerName).Assign().P("PerformerRoleName");
                
                parameters.Add(executor.Parameter("PerformerRoleID", masterTaskAssignedRole.RoleID));
                parameters.Add(executor.Parameter("PerformerRoleName", masterTaskAssignedRole.RoleName));
            }

            if (task.Action == CardTaskAction.Complete && task.OptionID.HasValue)
            {
                string answer = null;

                if (task.OptionID != DefaultCompletionOptions.Revoke)
                {
                    Card taskCard = task.TryGetCard();
                    StringDictionaryStorage<CardSection> taskSections;
                    if (taskCard is not null
                        && (taskSections = taskCard.TryGetSections()) is not null
                        && taskSections.TryGetValue(KrAdditionalApprovalTaskInfo.Name, out var additionalApprovalSection))
                    {
                        answer = additionalApprovalSection.RawFields.TryGet<string>(KrAdditionalApprovalTaskInfo.Comment);
                    }
                }
                else
                {
                    // удаляем комментарий автора, если он есть, отделённый переводом строки от прочей информации
                    answer = task.Result;
                    if (!string.IsNullOrEmpty(answer))
                    {
                        int firstLineEnding = answer.IndexOf(Environment.NewLine, StringComparison.Ordinal);
                        if (firstLineEnding > 0)
                        {
                            answer = answer.Substring(firstLineEnding + Environment.NewLine.Length);
                        }
                    }
                }

                if (answer is not null)
                {
                    builder
                        .C(KrAdditionalApprovalInfo.Answer).Assign().P("Answer");
                    parameters.Add(
                        executor.Parameter("Answer", answer));
                }
            }

            builder
                .From("TaskHistory", "th").NoLock()
                .Where()
                    .C(KrAdditionalApprovalInfo.Name, KrAdditionalApprovalInfo.RowID).Equals().C("th", "RowID")
                    .And().C(KrAdditionalApprovalInfo.Name, KrAdditionalApprovalInfo.RowID).Equals().P("TaskID");

            await executor
                .ExecuteNonQueryAsync(
                    builder.Build(),
                    cancellationToken,
                    parameters.ToArray());
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
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

            await using (context.DbScope.Create())
            {
                var executor = context.DbScope.Executor;
                var builderFactory = context.DbScope.BuilderFactory;
                var db = context.DbScope.Db;

                foreach (var task in context.Request.Card.Tasks)
                {
                    var parentRowID = task.ParentRowID;
                    if (parentRowID is null || task.TypeID != DefaultTaskTypes.KrAdditionalApprovalTypeID)
                    {
                        continue;
                    }

                    if (task.State == CardRowState.Inserted)
                    {
                        // родительское задание расположено в той же карточке, что и дочернее;
                        // поскольку мы находимся в транзакции на сохранение этой карточки,
                        // то никто посторонний гарантированно не может завершить родительское задание,
                        // пока не закончится этот метод

                        // однако задание уже может быть завершено на момент выполнения метода,
                        // поэтому надо проверить его существование перед вставкой строки

                        if (await HasTaskAsync(parentRowID.Value, db, builderFactory, context.CancellationToken))
                        {
                            await InsertChildRecordAsync(
                                task,
                                parentRowID.Value,
                                context.StoreDateTime ?? DateTime.UtcNow,
                                context.DbScope,
                                context.ValidationResult,
                                context.CancellationToken);

                            await ClearMainTaskInfoAsync(
                                parentRowID.Value,
                                executor,
                                builderFactory,
                                context.CancellationToken);
                        }
                    }
                    else
                    {
                        await UpdateChildRecordAsync(
                            task,
                            context.DbScope,
                            context.ValidationResult,
                            context.CancellationToken);
                    }
                }
            }
        }

        #endregion
    }
}
