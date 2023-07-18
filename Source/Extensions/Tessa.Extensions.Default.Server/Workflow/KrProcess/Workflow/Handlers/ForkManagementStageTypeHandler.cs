using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.ForkManagementDescriptor"/>.
    /// </summary>
    public class ForkManagementStageTypeHandler : ForkStageTypeHandlerBase
    {
        #region Nested Types

        /// <summary>
        /// Перечисление режимов работы этапа.
        /// </summary>
        public enum ForkManagementMode
        {
            /// <summary>
            /// Добавить ветку.
            /// </summary>
            Add = 0,

            /// <summary>
            /// Завершение ветки.
            /// </summary>
            Remove = 1,
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleStageStartAsync(
            IStageTypeHandlerContext context)
        {
            var stageSettings = context.Stage.SettingsStorage;
            var managePrimaryProcessFlag = stageSettings.TryGet<bool?>(KrConstants.KrForkManagementSettingsVirtual.ManagePrimaryProcess) ?? false;
            var managePrimaryProcess = managePrimaryProcessFlag
                && context.ProcessInfo?.ProcessTypeName != KrConstants.KrProcessName;
            Guid? processID;
            string processType;
            if (managePrimaryProcess)
            {
                processType = KrConstants.KrProcessName;
                processID = null;
            }
            else
            {
                processType = context.ParentProcessTypeName;
                processID = context.ParentProcessID;
            }

            var modeInt = stageSettings.TryGet<int?>(KrConstants.KrForkManagementSettingsVirtual.ModeID);

            if (!modeInt.HasValue
                || !(0 <= modeInt && modeInt <= (int) ForkManagementMode.Remove))
            {
                context.ValidationResult.AddError(this, "$KrStages_ForkManagement_ModeNotSpecified");
                return StageHandlerResult.SkipResult;
            }

            switch ((ForkManagementMode) modeInt)
            {
                case ForkManagementMode.Add:
                    await this.AddAsync(processType, processID, context);
                    break;
                case ForkManagementMode.Remove:
                    await this.RemoveAsync(processType, processID, context);
                    break;
            }

            return StageHandlerResult.CompleteResult;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Добавляет новую ветку.
        /// </summary>
        /// <param name="processType">Имя типа процесса которому отправляется сигнал.</param>
        /// <param name="processID">Идентификатор процесса. Не указывается для основного процесса.</param>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async ValueTask AddAsync(
            string processType,
            Guid? processID,
            IStageTypeHandlerContext context)
        {
            var branchAdditionInfos = new ListStorage<BranchAdditionInfo>(new List<object>(), BranchAdditionInfoFactory);
            var processInfos = GetProcessInfos(context.Stage);

            foreach (var storage in EnumerateSecondaryProcessRows(context))
            {
                var rowID = storage.TryGet<Guid>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.RowID);
                var spID = storage.TryGet<Guid>(
                        KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID);
                var spName = storage.TryGet<string>(
                    KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessName);
                var processInfo = GetProcessInfo(processInfos, rowID);

                branchAdditionInfos.Add(new BranchAdditionInfo(spID, spName, processInfo.ToDictionaryStorage()));
            }

            var card = await context.MainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

            if (card is null)
            {
                return;
            }

            card
                .GetWorkflowQueue()
                .AddSignal(
                    processType,
                    KrConstants.ForkAddBranchSignal,
                    processID: processID,
                    parameters: new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        [nameof(BranchAdditionInfo)] = branchAdditionInfos.GetStorage(),
                    });
        }

        /// <summary>
        /// Завершает ветку.
        /// </summary>
        /// <param name="processType">Имя типа процесса которому отправляется сигнал.</param>
        /// <param name="processID">Идентификатор процесса. Не указывается для основного процесса.</param>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task RemoveAsync(
            string processType,
            Guid? processID,
            IStageTypeHandlerContext context)
        {
            var secondaryProcesses = EnumerateSecondaryProcessRows(context)
                .Select(p => p.TryGet<Guid>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID))
                .ToList();
            var nestedProcesses = context
                .Stage
                .SettingsStorage
                .TryGet<IList>(KrConstants.KrForkNestedProcessesSettingsVirtual.Synthetic)
                .Cast<IDictionary<string, object>>()
                .Select(p => p.TryGet<Guid>(KrConstants.KrForkNestedProcessesSettingsVirtual.NestedProcessID))
                .ToList();

            var directionAfterInterrupt = (DirectionAfterInterrupt) context.Stage.SettingsStorage
                .TryGet(KrConstants.KrForkManagementSettingsVirtual.DirectionAfterInterrupt, (int) DirectionAfterInterrupt.Forward);
            var branchRemovalInfo = new BranchRemovalInfo(secondaryProcesses, nestedProcesses, directionAfterInterrupt);

            var card = await context.MainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

            if (card is null)
            {
                return;
            }

            card
                .GetWorkflowQueue()
                .AddSignal(
                    processType,
                    KrConstants.ForkRemoveBranchSignal,
                    processID: processID,
                    parameters: new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        [nameof(BranchRemovalInfo)] = branchRemovalInfo.GetStorage(),
                    });
        }

        #endregion

    }
}