using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Предоставляет вспомогательные методы для выполнения переходов между этапами.
    /// </summary>
    public static class TransitionHelper
    {
        #region Constants

        /// <summary>
        /// Порядковый индекс этапа, если его не удалось найти.
        /// </summary>
        public const int NotFound = -1;

        #endregion

        #region Public Methods

        /// <summary>
        /// Определяет порядковый номер первого этапа удовлетворяющего заданное условие.
        /// </summary>
        /// <param name="stages">Рассматриваемая коллекция этапов.</param>
        /// <param name="predicate">Условие поиска.</param>
        /// <returns>Порядковый номер этапа или значение <see cref="NotFound"/>, если его не удалось определить.</returns>
        public static int TransitByPredicate(
            ICollection<Stage> stages,
            Func<Stage, bool> predicate)
        {
            Check.ArgumentNotNull(stages, nameof(stages));
            Check.ArgumentNotNull(predicate, nameof(predicate));

            var transitionIndex = 0;
            foreach (var stage in stages)
            {
                if (predicate(stage))
                {
                    break;
                }

                transitionIndex++;
            }

            return TransitionIndex(stages, transitionIndex);
        }

        /// <summary>
        /// Определяет порядковый номер этапа имеющего указанный идентификатор.
        /// </summary>
        /// <param name="stages">Рассматриваемая коллекция этапов.</param>
        /// <param name="stageID">Идентификатор этапа (<see cref="Stage.ID"/>).</param>
        /// <returns>Порядковый номер этапа или значение <see cref="NotFound"/>, если его не удалось определить.</returns>
        public static int TransitToStage(
            ICollection<Stage> stages,
            Guid stageID) =>
            // Параметры будут проверены в TransitByPredicate.
            TransitByPredicate(stages, s => s.ID == stageID);

        /// <summary>
        /// Определяет порядковый номер первого этапа, относящегося к группе с указанным идентификатором.
        /// </summary>
        /// <param name="stages">Рассматриваемая коллекция этапов.</param>
        /// <param name="stageGroupID">Идентификатор группы этапов.</param>
        /// <returns>Порядковый номер этапа или значение <see cref="NotFound"/>, если его не удалось определить.</returns>
        public static int TransitToStageGroup(
            ICollection<Stage> stages,
            Guid stageGroupID) =>
            // Параметры будут проверены в TransitByPredicate.
            TransitByPredicate(stages, s => s.StageGroupID == stageGroupID);

        /// <summary>
        /// Определяет порядковый номер первого этапа, относящегося к группе следующей за группой с указанным идентификатором.
        /// </summary>
        /// <param name="stages">Рассматриваемая коллекция этапов.</param>
        /// <param name="stageGroupID">Идентификатор группы этапов относительно этапа которой должен быть определён этап относящийся к следующей группе.</param>
        /// <returns>Порядковый номер этапа или значение <see cref="NotFound"/>, если его не удалось определить.</returns>
        public static int TransitToNextGroup(
            ICollection<Stage> stages,
            Guid stageGroupID)
        {
            var transitionIndex = 0;
            var currentGroup = false;
            foreach (var stage in stages)
            {
                if (!currentGroup
                    && stage.StageGroupID == stageGroupID)
                {
                    currentGroup = true;
                }

                if (currentGroup
                    && stage.StageGroupID != stageGroupID)
                {
                    break;
                }

                transitionIndex++;
            }
            return TransitionIndex(stages, transitionIndex);
        }

        /// <summary>
        /// Определяет порядковый номер первого этапа, относящегося к группе предшествующей группе с указанным идентификатором.
        /// </summary>
        /// <param name="stages">Рассматриваемая коллекция этапов.</param>
        /// <param name="stageGroupID">Идентификатор группы этапов относительно этапа которой должен быть определён этап относящийся к предыдущей группе.</param>
        /// <returns>Порядковый номер этапа или значение <see cref="NotFound"/>, если его не удалось определить.</returns>
        public static int TransitToPreviousGroup(
            ICollection<Stage> stages,
            Guid stageGroupID)
        {
            Check.ArgumentNotNull(stages, nameof(stages));

            var transitionIndex = 0;
            var firstStageInGroupIndex = transitionIndex;
            Guid? currentGroupFirstStageID = null;
            Guid? prevStageGroupID = null;
            foreach (var stage in stages)
            {
                if (stage.StageGroupID == stageGroupID)
                {
                    break;
                }

                if (prevStageGroupID != stage.StageGroupID)
                {
                    firstStageInGroupIndex = transitionIndex;
                    currentGroupFirstStageID = stage.RowID;
                    prevStageGroupID = stage.StageGroupID;
                }

                transitionIndex++;
            }

            transitionIndex = currentGroupFirstStageID.HasValue
                ? firstStageInGroupIndex
                : stages.Count;

            return TransitionIndex(stages, transitionIndex);
        }

        /// <summary>
        /// Обновляет состояния этапов в маршруте в связи с переходом.
        /// </summary>
        /// <param name="stages">Список этапов.</param>
        /// <param name="startStageRowID">Идентификатор этапа с которого выполняется переход.</param>
        /// <param name="finalStateRowID">Идентификатор этапа на который выполняется переход.</param>
        /// <param name="processHolderSatellite">Текущий сателлит процесса. Может быть не задан.</param>
        public static void ChangeStatesTransition(
            IReadOnlyList<Stage> stages,
            Guid startStageRowID,
            Guid finalStateRowID,
            Card processHolderSatellite)
        {
            Check.ArgumentNotNull(stages, nameof(stages));

            // Те, что до всего процесса не трогаем
            var transitionIntervalBeginIndex = 0;
            var skipInterval = false;
            var inactiveInterval = false;
            var roots = new Queue<Guid>();
            foreach (var stage in stages)
            {
                if (stage.RowID == finalStateRowID)
                {
                    // Вернулись назад
                    inactiveInterval = true;
                    break;
                }

                if (stage.RowID == startStageRowID)
                {
                    // Перескочили дальше
                    skipInterval = true;
                    break;
                }

                transitionIntervalBeginIndex++;
            }

            if (skipInterval)
            {
                for (var i = transitionIntervalBeginIndex;
                    i < stages.Count && stages[i].RowID != finalStateRowID;
                    i++)
                {
                    var stage = stages[i];
                    // Ставим, что этап пропущен, только если он уже не считается завершенным.
                    if (stage.State != KrStageState.Completed)
                    {
                        stage.State = KrStageState.Skipped;
                        roots.Enqueue(stage.RowID);
                    }

                    if (processHolderSatellite != null)
                    {
                        SetStateToNesteds(processHolderSatellite, roots, KrStageState.Skipped);
                    }
                }
            }
            else if (inactiveInterval)
            {
                var i = transitionIntervalBeginIndex;
                for (;
                    i < stages.Count && stages[i].RowID != startStageRowID;
                    i++)
                {
                    var stage = stages[i];
                    stage.State = KrStageState.Inactive;
                    roots.Enqueue(stage.RowID);
                }

                if (i < stages.Count)
                {
                    var stage = stages[i];
                    stage.State = KrStageState.Inactive;
                    roots.Enqueue(stage.RowID);
                }

                if (processHolderSatellite is not null)
                {
                    SetStateToNesteds(processHolderSatellite, roots, KrStageState.Inactive);
                }
            }
        }

        /// <summary>
        /// Установить состояние <see cref="KrStageState.Skipped"/> для всех этапов расположенных после указанного этапа.
        /// </summary>
        /// <param name="currentStage">Текущий этап.</param>
        /// <param name="stages">Коллекция этапов.</param>
        /// <param name="processHolderSatellite">Текущий сателлит процесса. Может быть не задан.</param>
        public static void SetSkipStateToSubsequentStages(
            Stage currentStage,
            SealableObjectList<Stage> stages,
            Card processHolderSatellite)
        {
            Check.ArgumentNotNull(currentStage, nameof(currentStage));
            Check.ArgumentNotNull(stages, nameof(stages));

            if (currentStage.State == KrStageState.Active)
            {
                currentStage.State = KrStageState.Skipped;
            }

            var currentStageIndex = stages.IndexOf(currentStage);
            var roots = new Queue<Guid>();
            for (var i = currentStageIndex + 1; i < stages.Count; i++)
            {
                var stage = stages[i];
                stage.State = KrStageState.Skipped;
                roots.Enqueue(stage.RowID);
            }

            if (processHolderSatellite is not null)
            {
                SetStateToNesteds(processHolderSatellite, roots, KrStageState.Skipped);
            }
        }

        /// <summary>
        /// Устанавливает состояние <see cref="KrStageState.Inactive"/> для всех этапов.
        /// </summary>
        /// <param name="stages">Коллекция этапов.</param>
        /// <param name="processHolderSatellite">Текущий сателлит процесса. Может быть не задан.</param>
        public static void SetInactiveStateToAllStages(
            SealableObjectList<Stage> stages,
            Card processHolderSatellite)
        {
            Check.ArgumentNotNull(stages, nameof(stages));

            var roots = new Queue<Guid>();
            foreach (var stage in stages)
            {
                stage.State = KrStageState.Inactive;
                roots.Enqueue(stage.RowID);
            }

            if (processHolderSatellite != null)
            {
                SetStateToNesteds(processHolderSatellite, roots, KrStageState.Inactive);
            }
        }

        #endregion

        #region Private Methods

        private static int TransitionIndex(
            ICollection<Stage> stages,
            int transitionIndex)
        {
            return stages.Count == transitionIndex
                ? NotFound
                : transitionIndex;
        }

        private static void SetStateToNesteds(
            Card processHolder,
            Queue<Guid> roots,
            KrStageState state)
        {
            if (roots.Count == 0)
            {
                return;
            }

            var stageRows = processHolder.GetStagesSection().Rows;
            var stateIDObj = Int32Boxes.Box(state.ID);
            var stateName = state.TryGetDefaultName();

            while (roots.Count != 0)
            {
                var parentStageRowID = roots.Dequeue();
                foreach (var row in stageRows
                    .Where(p => p.TryGet<Guid?>(KrConstants.KrStages.ParentStageRowID) == parentStageRowID))
                {
                    row.Fields[KrConstants.KrStages.StateID] = stateIDObj;
                    row.Fields[KrConstants.KrStages.StateName] = stateName;
                    roots.Enqueue(row.RowID);
                }
            }
        }

        #endregion
    }
}
