using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Стратегия подготавливающая данные для пересчёта маршрута. Возвращает первый этап у которого порядковый номер группы этапов не превышает порядковый номер группы текущего этапа.
    /// </summary>
    public sealed class ExplicitlySelectedPreparingGroupRecalcStrategy : IPreparingGroupRecalcStrategy
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

            if (stage != null)
            {
                // Переход в конкретное место
                this.ExecutionUnits = new[] { stage.StageGroupID };
                this.minOrder = stage.StageGroupOrder;
            }
            else
            {
                this.ExecutionUnits = EmptyHolder<Guid>.Collection;
                this.minOrder = int.MaxValue;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}