using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Предоставляет методы для отзыва заданий этапа.
    /// </summary>
    public sealed class StageTasksRevoker : IStageTasksRevoker
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly IKrScope krScope;
        private readonly ICardGetStrategy cardGetStrategy;
        private readonly ISession session;
        private readonly ICardMetadata cardMetadata;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StageTasksRevoker"/>.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="cardGetStrategy">Стратегия загрузки карточки.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="cardMetadata">Метаинформация, необходимую для использования типов карточек совместно с пакетом карточек.</param>
        public StageTasksRevoker(
            IDbScope dbScope,
            IKrScope krScope,
            ICardGetStrategy cardGetStrategy,
            ISession session,
            ICardMetadata cardMetadata)
        {
            this.dbScope = dbScope;
            this.krScope = krScope;
            this.cardGetStrategy = cardGetStrategy;
            this.session = session;
            this.cardMetadata = cardMetadata;
        }

        #endregion

        #region IStageTasksRevoker Members

        /// <inheritdoc />
        public async Task<bool> RevokeAllStageTasksAsync(IStageTasksRevokerContext context)
        {
            if (context.Context.ProcessInfo is null
                || context.Context.MainCardID is null)
            {
                return true;
            }
            context.CardID = context.Context.MainCardID.Value;

            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                // Получение списка заданий из таблицы WorkflowTasks
                var currentTasks = await db.SetCommand(
                        this.dbScope.BuilderFactory
                            .Select()
                            .C("RowID")
                            .From("WorkflowTasks").NoLock()
                            .Where().C("ProcessRowID").Equals().P("pid")
                            .Build(),
                        db.Parameter("pid", context.Context.ProcessInfo.ProcessID))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(context.CancellationToken);

                switch (currentTasks.Count)
                {
                    case 0:
                        // Заданий нет, прерывание этапа завершено.
                        return true;
                    case 1:
                        // Задание есть, его нужно отозвать.
                        context.TaskID = currentTasks[0];
                        return await this.RevokeTaskAsync(context);
                    default:
                        context.TaskIDs = currentTasks;
                        return await this.RevokeTasksAsync(context) == 0;
                }
            }
        }

        /// <inheritdoc />
        public async Task<bool> RevokeTaskAsync(IStageTasksRevokerContext context)
        {
            await using (this.dbScope.Create())
            {
                var validationResult = context.Context.ValidationResult;
                var card = await this.krScope.GetMainCardAsync(
                    context.CardID,
                    cancellationToken: context.CancellationToken);

                if (card is null)
                {
                    return true;
                }

                var cardTasks = card.TryGetTasks();
                if (cardTasks is null
                    || cardTasks.All(p => p.RowID != context.TaskID))
                {
                    var db = this.dbScope.Db;
                    var taskContexts = await this.cardGetStrategy.TryLoadTaskInstancesAsync(
                        card.ID,
                        card,
                        db,
                        this.cardMetadata,
                        validationResult,
                        this.session,
                        getTaskMode: CardGetTaskMode.All,
                        loadCalendarInfo: false,
                        taskRowIDList: new[] { context.TaskID },
                        cancellationToken: context.CancellationToken);
                    foreach (var taskContext in taskContexts)
                    {
                        await this.cardGetStrategy.LoadSectionsAsync(taskContext, context.CancellationToken);
                    }
                }

                var task = cardTasks?.FirstOrDefault(p => p.RowID == context.TaskID);
                if (task is null)
                {
                    return true;
                }

                await RevokeTaskInternalAsync(context, task);

                return false;
            }
        }

        /// <inheritdoc />
        public async Task<int> RevokeTasksAsync(IStageTasksRevokerContext context)
        {
            var tasksToRevoke = context.TaskIDs;
            if (tasksToRevoke.Count == 0)
            {
                return 0;
            }

            var tasksToLoad = tasksToRevoke;
            var card = await this.krScope.GetMainCardAsync(
                context.CardID,
                cancellationToken: context.CancellationToken);

            if (card is null)
            {
                return 0;
            }

            var cardTasks = card.TryGetTasks();
            if (cardTasks is not null)
            {
                tasksToLoad = new List<Guid>(tasksToLoad);

                foreach (var cardTask in cardTasks)
                {
                    tasksToLoad.Remove(cardTask.RowID);
                }
            }

            if (tasksToLoad.Count > 0)
            {
                IList<CardGetContext> taskContexts;
                await using (this.dbScope.Create())
                {
                    taskContexts = await this.cardGetStrategy.TryLoadTaskInstancesAsync(
                        card.ID,
                        card,
                        this.dbScope.Db,
                        this.cardMetadata,
                        context.Context.ValidationResult,
                        this.session,
                        getTaskMode: CardGetTaskMode.All,
                        loadCalendarInfo: false,
                        taskRowIDList: tasksToLoad,
                        cancellationToken: context.CancellationToken);
                }
                foreach (var taskContext in taskContexts)
                {
                    await this.cardGetStrategy.LoadSectionsAsync(taskContext, context.CancellationToken);
                }
            }

            cardTasks = card.TryGetTasks();
            if (cardTasks == null)
            {
                return 0;
            }

            var tasksRevoked = 0;
            foreach (var taskToRevoke in cardTasks)
            {
                if (tasksToRevoke.Contains(taskToRevoke.RowID))
                {
                    await RevokeTaskInternalAsync(context, taskToRevoke);
                    tasksRevoked++;
                }
            }

            return tasksRevoked;
        }

        #endregion

        #region Provate Methods

        private static async Task RevokeTaskInternalAsync(
            IStageTasksRevokerContext context,
            CardTask task)
        {
            task.Action = CardTaskAction.Complete;
            task.State = CardRowState.Deleted;
            task.Flags = task.Flags & ~CardTaskFlags.Locked | CardTaskFlags.UnlockedForPerformer | CardTaskFlags.HistoryItemCreated;
            task.OptionID = context.OptionID ?? DefaultCompletionOptions.Cancel;

            if (context.RemoveFromActive)
            {
                await context.Context.WorkflowAPI.TryRemoveActiveTaskAsync(task.RowID, context.CancellationToken);
            }

            context.TaskModificationAction?.Invoke(task);
        }

        #endregion
    }
}
