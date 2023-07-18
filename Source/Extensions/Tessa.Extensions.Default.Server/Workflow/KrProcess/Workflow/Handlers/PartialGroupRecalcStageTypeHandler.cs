using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Collections;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="Shared.Workflow.KrProcess.StageTypeDescriptors.NotificationDescriptor"/>.
    /// </summary>
    public class PartialGroupRecalcStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PartialGroupRecalcStageTypeHandler"/>.
        /// </summary>
        /// <param name="executorFunc"><inheritdoc cref="ExecutorFunc" path="/summary"/></param>
        public PartialGroupRecalcStageTypeHandler(
            [Dependency(KrExecutorNames.CacheExecutor)] Func<IKrExecutor> executorFunc) =>
            this.ExecutorFunc = NotNullOrThrow(executorFunc);

        #endregion

        #region Protected Properties

        /// <summary>
        /// Метод возвращающий объект, позволяющий выполнять сценарии шаблонов этапов.
        /// </summary>
        protected Func<IKrExecutor> ExecutorFunc { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Выполняет поиск порядковых индексов этапов для определения начала области обновления текущего процесса после пересчёта.
        /// </summary>
        /// <param name="modifiedProcess">Объектная модель процесса после пересчёта.</param>
        /// <param name="originalProcess">Объектная модель процесса до пересчёта.</param>
        /// <param name="currentStageID">Идентификатор текущего этапа процесса.</param>
        /// <param name="currentGroupID">Идентификатор группы этапов текущего этапа процесса.</param>
        /// <param name="originalIdx">Возвращаемое значение. Порядковый индекс этапа, из процесса до пересчёта, начиная с которого будет выполнен пересчёт.</param>
        /// <param name="modifiedIdx">Возвращаемое значение. Порядковый индекс этапа, из процесса после пересчёта, начиная с которого будет выполнен пересчёт.</param>
        /// <returns>Значение <see langword="true"/>, если этап с идентификатором <paramref name="currentStageID"/> содержится в объектной модели процесса до пересчёта не затронутой выполненным пересчётом, иначе - <see langword="false"/>.</returns>
        protected virtual bool FindHeads(
            WorkflowProcess modifiedProcess,
            WorkflowProcess originalProcess,
            Guid currentStageID,
            Guid currentGroupID,
            out int originalIdx,
            out int modifiedIdx)
        {
            originalIdx = 0;
            modifiedIdx = 0;
            while (originalIdx < originalProcess.Stages.Count
                && originalProcess.Stages[originalIdx].StageGroupID != currentGroupID)
            {
                originalIdx++;
            }

            while (originalIdx < originalProcess.Stages.Count
                && modifiedIdx < modifiedProcess.Stages.Count
                && originalProcess.Stages[originalIdx].EqualsWithAutomaticallyChangedValues(modifiedProcess.Stages[modifiedIdx]))
            {
                if (originalProcess.Stages[originalIdx].ID == currentStageID)
                {
                    return true;
                }

                originalIdx++;
                modifiedIdx++;
            }

            return false;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var baseResult = await base.HandleStageStartAsync(context);
            if (baseResult != StageHandlerResult.EmptyResult)
            {
                return baseResult;
            }

            if (!context.MainCardID.HasValue)
            {
                return StageHandlerResult.SkipResult;
            }

            var workflowProcessCopy = context.WorkflowProcess
                .CloneWithDedicatedStageGroup(context.Stage.StageGroupID, true);

            // Пересчёт текущей группы этапов.
            var executor = this.ExecutorFunc();
            var ctx = new KrExecutionContext(
                cardContext: context.CardExtensionContext,
                mainCardAccessStrategy: context.MainCardAccessStrategy,
                cardID: context.MainCardID,
                cardType: context.MainCardType,
                docTypeID: context.MainCardDocTypeID,
                krComponents: context.KrComponents,
                workflowProcess: workflowProcessCopy,
                executionUnits: new[] { context.Stage.StageGroupID },
                cancellationToken: context.CancellationToken);
            var result = await executor.ExecuteAsync(ctx);
            context.ValidationResult.Add(result.Result);

            var stageGroupID = context.Stage.StageGroupID;

            // Определение этапов с которых должно быть выполнено обновление маршрута.
            if (!this.FindHeads(
                workflowProcessCopy,
                context.WorkflowProcess,
                context.Stage.ID,
                stageGroupID,
                out var originalIdx,
                out var modifiedIdx))
            {
                // Маршрут был изменён до текущего этапа.
                // Никакие изменения не применяются.
                return StageHandlerResult.SkipResult;
            }

            originalIdx++;
            modifiedIdx++;

            // Перенос информации по изменённым этапам.
            var newStages = workflowProcessCopy.Stages;
            var stages = context.WorkflowProcess.Stages;
            while (modifiedIdx < newStages.Count
                && originalIdx < stages.Count
                && stages[originalIdx].StageGroupID == stageGroupID)
            {
                stages[originalIdx] = newStages[modifiedIdx];
                originalIdx++;
                modifiedIdx++;
            }

            // Если после пересчета этапов больше, чем было
            if (modifiedIdx < newStages.Count)
            {
                // В конце маршрута
                if (originalIdx == stages.Count)
                {
                    stages.AddRange(newStages.Skip(modifiedIdx));
                }
                // Еще есть группы после текущей
                else
                {
                    stages.InsertRange(originalIdx, newStages.Skip(modifiedIdx).ToList().AsReadOnly());
                }
            }
            else
            {
                // Если после пересчета в текущей группе оказалось меньше этапов, чем было
                // надо аккуратно лишнее удалить.
                while (originalIdx < stages.Count
                    && stages[originalIdx].StageGroupID == stageGroupID)
                {
                    context.WorkflowProcess.Stages.RemoveAt(originalIdx);
                    originalIdx++;
                }
            }

            return StageHandlerResult.CompleteResult;
        }

        #endregion
    }
}
