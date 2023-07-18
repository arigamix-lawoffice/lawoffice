using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Специальные методы-расширения,
    /// использовние которых обусловлено только в обработчиках этапов.
    /// </summary>
    public static class HandlerExtensions
    {
        /// <summary>
        /// Добавить в историю процесса запись.
        /// </summary>
        /// <param name="satellite">Контекстуальный сателлит</param>
        /// <param name="taskID">ID задания.</param>
        /// <param name="cycle">Текущий цикл.</param>
        /// <param name="advisory">Добавляется запись о рекомендательном согласовании</param>
        public static void AddToHistory(
            this Card satellite,
            Guid? taskID, 
            int cycle = 0,
            bool advisory = false)
        {
            KrErrorHelper.AssertKrSatellte(satellite);

            var historySection = satellite.GetKrApprovalHistorySection();
            var row = historySection.Rows.Add();
            row.State = CardRowState.Inserted;
            row.RowID = Guid.NewGuid();
            row.Fields[KrConstants.Keys.Cycle] = Int32Boxes.Box(cycle);
            row.Fields[KrConstants.KrApprovalHistory.HistoryRecord] = taskID;
            row.Fields[KrConstants.KrApprovalHistory.Advisory] = BooleanBoxes.Box(advisory);
        }

        /// <summary>
        /// Выполняет указанное действие над всеми этапами группы с указанным идентификатором.
        /// </summary>
        /// <param name="stages">Список всех этапов маршрута.</param>
        /// <param name="groupID">Идентификатор группы этапов, к этапам которой требуется применить указанное действие.</param>
        /// <param name="action">Действие выполняемое над этапом.</param>
        public static void ForEachStageInGroup(
            this IList<Stage> stages,
            Guid groupID,
            Action<Stage> action)
        {
            var i = stages.IndexOf(p => p.StageGroupID == groupID);
            var cnt = stages.Count;
            Stage currStage;
            while (i < cnt
                && (currStage = stages[i++]).StageGroupID == groupID)
            {
                action(currStage);
            }
        }

        /// <summary>
        /// Выполняет указанное действие над всеми этапами группы с указанным идентификатором.
        /// </summary>
        /// <param name="stages">Список всех этапов маршрута.</param>
        /// <param name="groupID">Идентификатор группы этапов, к этапам которой требуется применить указанное действие.</param>
        /// <param name="action">Действие выполняемое над этапом. Метод возвращает значение, показывающее, следует ли продолжать обработку (<see langword="true"/>) или нет (<see langword="false"/>).</param>
        /// <returns>Значение <see langword="true"/>, если обработка этапов не прерывалась, иначе - <see langword="false"/>.</returns>
        public static bool ForEachStageInGroup(
            this IList<Stage> stages,
            Guid groupID,
            Func<Stage, bool> action)
        {
            var i = stages.IndexOf(p => p.StageGroupID == groupID);
            var cnt = stages.Count;
            Stage currStage;
            while (i < cnt
                && (currStage = stages[i++]).StageGroupID == groupID)
            {
                if (!action(currStage))
                {
                    return false;
                }
            }

            return true;
        }
    }
}