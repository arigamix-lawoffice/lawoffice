#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Roles;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow
{
    /// <summary>
    /// Дописывает в историю заданий информацию об исполнителе маршрутов при создании записи в истории заданий по заданию или при изменении исполнителей задания.
    /// </summary>
    public class AddPerformerToHistoryCardStoreTaskExtension : CardStoreTaskExtension
    {
        #region Fields

        private readonly IKrProcessContainer processContainer;
        private readonly ICardGetStrategy cardGetStrategy;

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="processContainer"><inheritdoc cref="IKrProcessContainer" path="/summary"/></param>
        /// <param name="cardGetStrategy"><inheritdoc cref="ICardGetStrategy" path="/summary"/></param>
        public AddPerformerToHistoryCardStoreTaskExtension(
            IKrProcessContainer processContainer,
            ICardGetStrategy cardGetStrategy)
        {
            this.processContainer = NotNullOrThrow(processContainer);
            this.cardGetStrategy = NotNullOrThrow(cardGetStrategy);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task StoreTaskBeforeRequest(ICardStoreTaskExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return Task.CompletedTask;
            }

            var task = context.Task;
            if (task.Flags.Has(CardTaskFlags.CreateHistoryItem))
            {
                return this.CreateNewHistorySettingsForTaskAsync(context);
            }
            else if (context.Task.State == CardRowState.Modified
                && task.Flags.Has(CardTaskFlags.UpdateTaskAssignedRoles | CardTaskFlags.HistoryItemCreated))
            {
                return this.UpdateHistorySettingsForTaskAsync(context);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private async Task UpdateHistorySettingsForTaskAsync(ICardStoreTaskExtensionContext context)
        {
            var task = context.Task;
            // Если нет изменения ФРЗ с типом "Исполнитель", то не запускаем обновление исполнителя ФРЗ в настройках истории заданий.
            if (!task.TaskAssignedRoles.Any(x => x.State == CardTaskAssignedRoleState.Deleted || x.TaskRoleID == CardFunctionRoles.PerformerID)
                && !await this.IsStorePerformerInSettingsAsync(task.TypeID, context.CancellationToken))
            {
                return;
            }

            var card = context.Request.Card;

            await using var _ = context.DbScope!.Create();
            var db = context.DbScope.Db;

            await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope, cancellationToken: context.CancellationToken);

            var performerTaskAssignedRoleRow = TryGetFirstPerformer(task);
            if (performerTaskAssignedRoleRow is null)
            {
                return;
            }

            CardTaskHistoryItem? historyItem = null;
            if (card.TryGetTaskHistory()?.TryFirst(x => x!.RowID == task.RowID, out historyItem) != true
                || historyItem is not { State: CardTaskHistoryState.Modified or CardTaskHistoryState.Inserted })
            {
                // Удаляем запись, не надо ничего обновлять
                if (historyItem?.State == CardTaskHistoryState.Deleted)
                {
                    return;
                }

                // Если мы не имеем изменяемой истории заданий в текущей обновляемой карточки, то загружаем запись из базы
                await this.cardGetStrategy.LoadTaskHistoryAsync(
                    card.ID,
                    card,
                    db,
                    context.CardMetadata,
                    context.ValidationResult,
                    null,
                    itemRowIDList: new[] { task.RowID },
                    cancellationToken: context.CancellationToken);

                if (card.TryGetTaskHistory()?.TryFirst(x => x!.RowID == task.RowID, out historyItem) != true)
                {
                    // Записи нет в базе, нечего обновлять.
                    return;
                }

                historyItem!.State = CardTaskHistoryState.Modified;
            }

            var historySettings = historyItem.Settings ??= new Dictionary<string, object?>(StringComparer.Ordinal);
            await this.UpdateHistorySettingsAsync(
                context.DbScope,
                historySettings,
                performerTaskAssignedRoleRow,
                context.CancellationToken);

        }

        private async Task CreateNewHistorySettingsForTaskAsync(ICardStoreTaskExtensionContext context)
        {
            var task = context.Task;
            if (!await this.IsStorePerformerInSettingsAsync(task.TypeID, context.CancellationToken))
            {
                return;
            }

            if (task.State != CardRowState.Inserted)
            {
                await CardComponentHelper.FillTaskAssignedRolesAsync(task, context.DbScope!, cancellationToken: context.CancellationToken);
            }

            var performerTaskAssignedRoleRow = TryGetFirstPerformer(task);
            if (performerTaskAssignedRoleRow is null)
            {
                return;
            }

            var historySettings = task.HistorySettings ??= new Dictionary<string, object?>(StringComparer.Ordinal);
            await this.UpdateHistorySettingsAsync(
                context.DbScope!,
                historySettings,
                performerTaskAssignedRoleRow,
                context.CancellationToken);
        }

        private async ValueTask<bool> IsStorePerformerInSettingsAsync(Guid taskTypeID, CancellationToken cancellationToken = default)
        {
            // Сохраняем исполнителя задания в истории заданий для заданий типовой отправки задачи и для заданий маршрутов.
            return WfHelper.ResolutionTaskTypeIDList.Contains(taskTypeID)
                || await this.processContainer.IsTaskTypeRegisteredAsync(taskTypeID, cancellationToken);
        }

        private static CardTaskAssignedRole? TryGetFirstPerformer(
            CardTask task)
        {
            return
                task.TaskAssignedRoles
                    .Where(p => p.State != CardTaskAssignedRoleState.Deleted
                        && p.ParentRowID is null
                        && p.TaskRoleID == CardFunctionRoles.PerformerID)
                    .MinBy(p => p.RowID);
        }

        private async ValueTask UpdateHistorySettingsAsync(
            IDbScope dbScope,
            Dictionary<string, object?> historySettings,
            CardTaskAssignedRole performerTaskAssignedRoleRow,
            CancellationToken cancellationToken)
        {
            // Если роль - временная или временная, созданная по контекстной, то пытаемся рассчитать родительскую роль (идентификатор контекстной роли) и использовать его
            var roleTypeID = performerTaskAssignedRoleRow.RoleTypeID == Guid.Empty
                ? await this.cardGetStrategy.GetTypeIDAsync(performerTaskAssignedRoleRow.RoleID, cancellationToken: cancellationToken)
                : performerTaskAssignedRoleRow.RoleTypeID;
            var roleID = performerTaskAssignedRoleRow.RoleID;

            if (roleTypeID is not null
                && CardComponentHelper.TemporaryTaskRoleTypeIDList.Contains(roleTypeID.Value))
            {
                await using var _ = dbScope.Create();

                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;

                var contextRoleID = await db.SetCommand(
                    builderFactory
                        .Select().C("ParentID").From(RoleStrings.Roles).NoLock()
                        .Where().C("ID").Equals().P("CardID")
                        .Build(),
                    db.Parameter("CardID", performerTaskAssignedRoleRow.RoleID, LinqToDB.DataType.Guid))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);

                if (contextRoleID is not null)
                {
                    roleID = contextRoleID.Value;
                }
            }

            historySettings[TaskHistorySettingsKeys.PerformerID] = roleID;
            historySettings[TaskHistorySettingsKeys.PerformerName] = performerTaskAssignedRoleRow.RoleName;
            historySettings[TaskHistorySettingsKeys.PerformerRoleTypeID] = roleTypeID;
        }

        #endregion
    }
}
