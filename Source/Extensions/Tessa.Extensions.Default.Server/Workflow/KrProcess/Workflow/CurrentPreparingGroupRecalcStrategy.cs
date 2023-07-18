using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Стратегия подготавливающая данные для пересчёта маршрута. Реализует продвижение по маршруту в начало текущей группы текущего этапа.
    /// </summary>
    public sealed class CurrentPreparingGroupRecalcStrategy : IPreparingGroupRecalcStrategy
    {
        #region Fields

        private int minOrder;

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
        public Task ApplyAsync(
            IKrProcessRunnerContext context,
            Stage stage,
            Stage prevStage)
        {
            if (this.Used)
            {
                throw new InvalidOperationException($"Current object {this.GetType().FullName} was used previously.");
            }

            this.Used = true;

            if (stage.StageGroupID == prevStage.StageGroupID)
            {
                // Переход в начало группы
                this.ExecutionUnits = new[] { stage.StageGroupID };
                this.minOrder = stage.StageGroupOrder;
                return Task.CompletedTask;
            }

            throw new InvalidOperationException($"The group of stages of the current stage (stage RowID = {stage.RowID:B}, group ID = {stage.StageGroupID:B}) is not equal to the group of stages of the previous stage (stage RowID = {prevStage.RowID:B}, group ID = {prevStage.StageGroupID:B}).");
        }

        #endregion
    }
}