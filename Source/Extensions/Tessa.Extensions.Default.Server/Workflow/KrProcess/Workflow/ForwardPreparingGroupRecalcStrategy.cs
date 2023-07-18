using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Стратегия подготавливающая данные для пересчёта маршрута. Реализует продвижение вперёд по маршруту.
    /// </summary>
    public sealed class ForwardPreparingGroupRecalcStrategy : IPreparingGroupRecalcStrategy
    {
        #region Fields

        private readonly IDbScope dbScope;

        private readonly ISession session;

        private int minOrder;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ForwardPreparingGroupRecalcStrategy"/>.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="session">Сессия пользователя.</param>
        public ForwardPreparingGroupRecalcStrategy(
            IDbScope dbScope,
            ISession session)
        {
            this.dbScope = dbScope;
            this.session = session;
        }

        #endregion

        #region IPreparingGroupRecalcStrategy Members

        /// <inheritdoc />
        public bool Used { get; private set; } = false;

        /// <inheritdoc />
        public IList<Guid> ExecutionUnits { get; private set; }

        /// <inheritdoc />
        public Stage GetSuitableStage(
            IList<Stage> stages)
        {
            foreach (var stage in stages)
            {
                if (stage.StageGroupOrder >= this.minOrder)
                {
                    return stage;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public async Task ApplyAsync(
            IKrProcessRunnerContext context,
            Stage stage,
            Stage prevStage)
        {
            if (this.Used)
            {
                throw new InvalidOperationException($"Current object {this.GetType().FullName} was used previously.");
            }

            this.Used = true;

            if (stage != null && prevStage != null)
            {
                if (prevStage.StageGroupOrder < stage.StageGroupOrder)
                {
                    // Переход между группами
                    this.ExecutionUnits = await this.GetNextStageGroupsAsync(prevStage, stage, context);
                    this.minOrder = prevStage.StageGroupOrder + 1;

                    if (this.ExecutionUnits.Count == 0)
                    {
                        // Такая ситуация возможна, когда следующая группа удалена, а между ними ничего нет.
                        // Нужно попытаться найти что-нибудь для старта, а также добавить удаленную группу в расчет,
                        // чтобы она вылетела из маршрута.
                        var nextGroups = await this.GetNextStageGroupsAsync(prevStage, null, context);
                        this.ExecutionUnits = new List<Guid>();
                        if (nextGroups.Count > 0)
                        {
                            // Они отсортированы по Order, поэтому в 0 будет с минимальным ордером
                            this.ExecutionUnits.Add(nextGroups[0]);
                        }

                        this.ExecutionUnits.Add(stage.StageGroupID);
                    }
                }
                else
                {
                    context.ValidationResult.AddError(this, "$KrProcess_ErrorMessage_StageStageGroupOrderLessPrevStageStageGroupOrder");
                    this.ExecutionUnits = Array.Empty<Guid>();
                    return;
                }
            }
            else if (stage is null && prevStage != null)
            {
                // Новой группы нет, процесс завершается.
                // Пытаемся найти еще что-нибудь
                this.ExecutionUnits = await this.GetNextStageGroupsAsync(prevStage, null, context);
                this.minOrder = prevStage.StageGroupOrder + 1;
            }
            else/* if (prevStage is null)*/
            {
                // Процесс только начался, старой группы нет, только новая
                // При старте процесса считаем маршрут посчитанным и пересчет отдельной группы не имеет смысла.
                this.ExecutionUnits = Array.Empty<Guid>();
                this.minOrder = stage.StageGroupOrder;
            }
        }

        #endregion

        #region Private Methods

        private Task<List<Guid>> GetNextStageGroupsAsync(
            Stage from,
            Stage to,
            IKrProcessRunnerContext context)
        {
            return KrCompilersSqlHelper
                .SelectFilteredStageGroupsAsync(
                    this.dbScope,
                    context.DocTypeID ?? context.CardType?.ID ?? Guid.Empty,
                    context.WorkflowProcess.ProcessOwnerCurrentProcess?.AuthorID ?? context.WorkflowProcess.AuthorCurrentProcess?.AuthorID ?? this.session.User.ID,
                    from?.StageGroupOrder + 1,
                    to?.StageGroupOrder,
                    context.SecondaryProcess?.ID,
                    cancellationToken: context.CancellationToken);
        }

        #endregion
    }
}