#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <inheritdoc cref="IWorkflowAPIBridge"/>
    public sealed class WorkflowAPIBridge : IWorkflowAPIBridge
    {
        #region Fields

        private readonly KrProcessWorkflowManager manager;
        private readonly IWorkflowProcessInfo processInfo;

        #endregion

        #region Constructor

        public WorkflowAPIBridge(
            KrProcessWorkflowManager manager,
            IWorkflowProcessInfo processInfo)
        {
            this.manager = NotNullOrThrow(manager);
            this.processInfo = NotNullOrThrow(processInfo);
        }

        #endregion

        #region IWorkflowAPIBridge Members

        /// <inheritdoc />
        public async Task<IWorkflowTaskInfo?> SendTaskAsync(
            Guid taskTypeID,
            string? digest,
            Guid? roleID,
            string? roleName = null,
            Dictionary<string, object?>? taskParameters = null,
            Guid? taskRowID = null,
            Func<CardTask, CancellationToken, ValueTask>? modifyTaskAction = null,
            CancellationToken cancellationToken = default)
        {
            var task = await this.AddNewTaskAsync(taskTypeID, roleID, roleName, taskRowID: taskRowID, cancellationToken: cancellationToken);

            if (task is null)
            {
                return null;
            }

            if (digest is not null)
            {
                task.Digest = digest;
            }

            if (modifyTaskAction is not null)
            {
                await modifyTaskAction(task, cancellationToken);
            }

            return await this.manager.AddTaskAsync(task, this.processInfo, taskParameters, cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddActiveTaskAsync(Guid taskID, CancellationToken cancellationToken = default)
        {
            var krSatellite = await this.manager
                .WorkflowContext
                .KrScope
                .GetKrSatelliteAsync(
                    this.manager.WorkflowContext.CardID,
                    cancellationToken: cancellationToken);

            if (krSatellite is null)
            {
                return;
            }

            var activeTasksSection = krSatellite.GetActiveTasksSection();

            if (activeTasksSection.Rows.Any(p => taskID.Equals(p.Fields[KrConstants.KrActiveTasks.TaskID])))
            {
                throw new InvalidOperationException($"Task with id {taskID} is already active.");
            }

            var row = activeTasksSection.Rows.Add();
            row.State = CardRowState.Inserted;
            row.RowID = Guid.NewGuid();
            row.Fields[KrConstants.KrActiveTasks.TaskID] = taskID;
        }

        /// <inheritdoc />
        public ValueTask<bool> TryRemoveActiveTaskAsync(
            Guid taskID,
            CancellationToken cancellationToken = default) =>
            this.RemoveActiveTaskInternalAsync(taskID, false, cancellationToken);

        /// <inheritdoc />
        public async ValueTask RemoveActiveTaskAsync(Guid taskID, CancellationToken cancellationToken = default) =>
            await this.RemoveActiveTaskInternalAsync(taskID, true, cancellationToken);

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<Guid>> GetActiveTasksAsync(CancellationToken cancellationToken = default)
        {
            var krSatellite = await this.manager.WorkflowContext.KrScope.GetKrSatelliteAsync(
                this.manager.WorkflowContext.CardID,
                cancellationToken: cancellationToken);

            if (krSatellite is null)
            {
                return EmptyHolder<Guid>.Collection;
            }

            return krSatellite
                .GetActiveTasksSection()
                .Rows
                .Select(p => (Guid) p.Fields[KrConstants.KrActiveTasks.TaskID]!)
                .ToList()
                .AsReadOnly();
        }

        /// <inheritdoc />
        public ValueTask InitCounterAsync(int counterNumber, int initialValue, CancellationToken cancellationToken = default) =>
            this.manager.InitCounterAsync(counterNumber, this.processInfo, initialValue, cancellationToken);

        /// <inheritdoc />
        public ValueTask<WorkflowCounterState> DecrementCounterAsync(int counterNumber, int decrementValue = 1, CancellationToken cancellationToken = default) =>
            this.manager.DecrementCounterAsync(counterNumber, this.processInfo, decrementValue, cancellationToken);

        /// <inheritdoc />
        public ValueTask<bool> RemoveCounterAsync(int counterNumber, CancellationToken cancellationToken = default) =>
            this.manager.RemoveCounterAsync(counterNumber, this.processInfo, cancellationToken);

        /// <inheritdoc />
        public IList<IWorkflowProcessInfo> ProcessesAwaitingRemoval => this.manager.ProcessesAwaitingRemoval;

        /// <inheritdoc />
        public CardStoreRequest NextRequest => this.manager.NextRequest;

        /// <inheritdoc />
        public bool NextRequestPending => this.manager.NextRequestPending;

        /// <inheritdoc />
        public void NotifyNextRequestPending() => this.manager.NotifyNextRequestPending();

        #endregion

        #region Private Methods

        /// <summary>
        /// Создаёт и добавляет задание в основную карточку.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа создаваемого задания.</param>
        /// <param name="roleID">Идентификатор роли, на которую назначается задание или значение <see langword="null"/>, если он будет задан позже.</param>
        /// <param name="roleName">
        /// Имя роли, на которую назначается задание,
        /// или <see langword="null"/>, если имя роли определяется автоматически в момент сохранения.
        /// </param>
        /// <param name="planned">
        /// Запланированная дата завершения задания
        /// или <see langword="null"/>, если планируется дата на 3 дня позже момента создания задания.
        /// </param>
        /// <param name="taskRowID">
        /// Идентификатор отправляемого задания или <see langword="null"/>, если для задания создаётся новый идентификатор.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Добавленное задание или <see langword="null"/>, если задание не удалось добавить.
        /// </returns>
        private async Task<CardTask?> AddNewTaskAsync(
            Guid taskTypeID,
            Guid? roleID,
            string? roleName = null,
            DateTime? planned = null,
            Guid? taskRowID = null,
            CancellationToken cancellationToken = default)
        {
            var newRequest = new CardNewRequest { CardTypeID = taskTypeID };
            var newResponse = await this.manager.WorkflowContext.CardRepositoryToCreateTasks.NewAsync(newRequest, cancellationToken);

            this.manager.ValidationResult.Add(newResponse.ValidationResult);

            if (!newResponse.ValidationResult.IsSuccessful())
            {
                return null;
            }

            var card = await this.manager.WorkflowContext.KrScope.GetMainCardAsync(
                this.manager.WorkflowContext.CardID,
                cancellationToken: cancellationToken);

            if (card is null)
            {
                return null;
            }

            var task = card.Tasks.Add();
            task.State = CardRowState.Inserted;

            var taskCard = newResponse.Card;
            taskCard.ID = taskRowID ?? Guid.NewGuid();
            task.SetCard(taskCard);
            task.SectionRows = newResponse.SectionRows;

            if (roleID.HasValue)
            {
                task.AddPerformer(roleID.Value, roleName);
            }

            task.Planned = planned ?? this.manager.StoreDateTime.AddDays(3);
            task.Settings = new Dictionary<string, object?>(StringComparer.Ordinal);

            return task;
        }

        private async ValueTask<bool> RemoveActiveTaskInternalAsync(
            Guid taskID,
            bool throwOnFailure,
            CancellationToken cancellationToken = default)
        {
            var krSatellite = await this.manager
                .WorkflowContext
                .KrScope
                .GetKrSatelliteAsync(
                this.manager.WorkflowContext.CardID,
                cancellationToken: cancellationToken);

            if (krSatellite is null)
            {
                return false;
            }

            var activeTasksSection = krSatellite
                .GetActiveTasksSection();

            var activeTaskRow = activeTasksSection.Rows.FirstOrDefault(p =>
                taskID.Equals(p.Fields[KrConstants.KrActiveTasks.TaskID]));

            if (activeTaskRow is null)
            {
                return RemoveActiveTaskFailure(taskID, throwOnFailure);
            }

            switch (activeTaskRow.State)
            {
                case CardRowState.Deleted:
                    return RemoveActiveTaskFailure(taskID, throwOnFailure);
                case CardRowState.Inserted:
                    activeTasksSection.Rows.Remove(activeTaskRow);
                    break;
                case CardRowState.Modified:
                case CardRowState.None:
                    activeTaskRow.State = CardRowState.Deleted;
                    break;
            }

            return true;
        }

        private static bool RemoveActiveTaskFailure(
            Guid taskID,
            bool throwOnFailure)
        {
            return throwOnFailure
                ? throw new InvalidOperationException($"Task with id \"{taskID:B}\" is inactive.")
                : false;
        }

        #endregion
    }
}
