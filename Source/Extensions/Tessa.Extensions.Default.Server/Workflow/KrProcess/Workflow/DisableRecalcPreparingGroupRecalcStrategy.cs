using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Стратегия подготавливающая данные для пересчёта маршрута. Возвращает этап соответствующий текущему этапу, на момент применения стратегии, иначе значение по умолчанию для типа.
    /// </summary>
    public sealed class DisableRecalcPreparingGroupRecalcStrategy : IPreparingGroupRecalcStrategy
    {
        #region Fields

        private bool hasStage;

        private Guid stageID;

        #endregion

        #region IPreparingGroupRecalcStrategy Members

        /// <inheritdoc />
        public bool Used { get; private set; } = false;

        /// <inheritdoc />
        public IList<Guid> ExecutionUnits { get; } = EmptyHolder<Guid>.Collection;

        /// <inheritdoc />
        public Stage GetSuitableStage(
            IList<Stage> stages)
        {
            return this.hasStage
                ? stages.FirstOrDefault(p => p.ID == this.stageID)
                : null;
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
            this.hasStage = stage != null;

            if (this.hasStage)
            {
                this.stageID = stage.ID;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}