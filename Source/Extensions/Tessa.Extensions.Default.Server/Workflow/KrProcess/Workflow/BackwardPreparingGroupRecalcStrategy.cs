using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Стратегия подготавливающая данные для пересчёта маршрута. Реализует продвижение назад по маршруту.
    /// </summary>
    public sealed class BackwardPreparingGroupRecalcStrategy : IPreparingGroupRecalcStrategy
    {
        #region Fields

        private readonly IDbScope dbScope;

        private readonly ISession session;

        private Guid stageGroupID;

        private bool transitToCurrent;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BackwardPreparingGroupRecalcStrategy"/>.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="session">Сессия пользователя.</param>
        public BackwardPreparingGroupRecalcStrategy(
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
            var idx = this.transitToCurrent
                ? TransitionHelper.TransitByPredicate(stages, p => p.StageGroupID == this.stageGroupID)
                : TransitionHelper.TransitToPreviousGroup(stages, this.stageGroupID);

            return idx == TransitionHelper.NotFound
                ? null
                : stages[idx];
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

            if (stage != null
                && prevStage != null
                && stage.StageGroupOrder < prevStage.StageGroupOrder)
            {
                // Расчет разницы между текущим и новым, который выше
                this.ExecutionUnits = await this.GetStageGroupsAsync(stage, prevStage, context);
                this.transitToCurrent = false;

                if (this.ExecutionUnits.Count == 0)
                {
                    // Между ними нет этапов, а также сам этап выше удален.
                    // Делается пересчет текущего и производится переход на текущий.
                    this.ExecutionUnits.Add(stage.StageGroupID);
                    this.transitToCurrent = true;
                }
                else
                {
                    this.ExecutionUnits = this.ExecutionUnits.Take(1).ToList();
                }

                this.stageGroupID = prevStage.StageGroupID;

                return;
            }

            throw new InvalidOperationException();
        }

        #endregion

        #region Private Methods

        private Task<List<Guid>> GetStageGroupsAsync(
            Stage from,
            Stage to,
            IKrProcessRunnerContext context)
        {
            return KrCompilersSqlHelper
                .SelectFilteredStageGroupsAsync(
                    this.dbScope,
                    context.DocTypeID ?? context.CardType?.ID ?? Guid.Empty,
                    context.WorkflowProcess.ProcessOwnerCurrentProcess?.AuthorID ?? context.WorkflowProcess.AuthorCurrentProcess?.AuthorID ?? this.session.User.ID,
                    from?.StageGroupOrder,
                    to?.StageGroupOrder - 1,
                    context.SecondaryProcess?.ID,
                    cancellationToken: context.CancellationToken);
        }

        #endregion
    }
}