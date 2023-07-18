using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Предоставляет методы для удаления вложенных этапов.
    /// </summary>
    public static class NestedStagesCleaner
    {
        #region Public Methods

        /// <summary>
        /// Изменяет состояние у всех строк секции, содержащей созданные по шаблону и имеющие к нему привязку этапы в указанной карточке, на <see cref="CardRowState.Deleted"/>.
        /// </summary>
        /// <param name="processHolder">Карточка, содержащая обрабатываемые этапы.</param>
        public static void ClearAll(
            Card processHolder)
        {
            Check.ArgumentNotNull(processHolder, nameof(processHolder));

            var stageRows = processHolder.GetStagesSection().Rows;
            foreach (var row in stageRows)
            {
                if (row.TryGet<Guid?>(KrConstants.KrStages.ParentStageRowID).HasValue)
                {
                    row.State = CardRowState.Deleted;
                }
            }
        }

        /// <summary>
        /// Изменяет состояние у всех строк, в том числе дочерних (определяется по полю <see cref="KrConstants.KrStages.ParentStageRowID"/>), секции, содержащей этапы в указанной карточке, на <see cref="CardRowState.Deleted"/> удовлетворяющих условию: этап принадлежит к группе из <paramref name="stageGroupIDs"/> и вложенному процессу с идентификатором <paramref name="nestedProcessID"/>.
        /// </summary>
        /// <param name="processHolder">Карточка, содержащая обрабатываемые этапы.</param>
        /// <param name="nestedProcessID">Идентификатор вложенного процесса.</param>
        /// <param name="stageGroupIDs">Коллекция идентификаторов групп этапов.</param>
        public static void ClearGroup(
            Card processHolder,
            Guid? nestedProcessID,
            ICollection<Guid> stageGroupIDs)
        {
            Check.ArgumentNotNull(processHolder, nameof(processHolder));
            Check.ArgumentNotNull(stageGroupIDs, nameof(stageGroupIDs));

            ClearWithRoots(processHolder,
                p => stageGroupIDs.Contains(p.TryGet<Guid>(KrConstants.KrStages.StageGroupID))
                    && p.TryGet<Guid?>(KrConstants.KrStages.NestedProcessID) == nestedProcessID);
        }

        /// <summary>
        /// Изменяет состояние у строки, и её дочерних строках, имеющей заданный идентификатор содержащей информацию о этапе маршрута, на <see cref="CardRowState.Deleted"/>.
        /// </summary>
        /// <param name="processHolder">Карточка, содержащая обрабатываемые этапы.</param>
        /// <param name="stageRowID">Идентификатор строки.</param>
        public static void ClearStage(
            Card processHolder,
            Guid stageRowID) =>
            ClearWithRoots(processHolder, p => p.RowID == stageRowID);

        #endregion

        #region Private Methods

        private static void ClearWithRoots(
            Card processHolder,
            Func<CardRow, bool> rootsSelector)
        {
            var stageRows = processHolder.GetStagesSection().Rows;
            var parentQueue = new Queue<Guid>(
                stageRows
                    .Where(rootsSelector)
                    .Select(p => p.RowID));

            while (parentQueue.Count != 0)
            {
                var parentStageRowID = parentQueue.Dequeue();
                foreach (var row in stageRows
                    .Where(p => p.TryGet<Guid?>(KrConstants.KrStages.ParentStageRowID) == parentStageRowID))
                {
                    row.State = CardRowState.Deleted;
                    parentQueue.Enqueue(row.RowID);
                }
            }
        }

        #endregion
    }
}
