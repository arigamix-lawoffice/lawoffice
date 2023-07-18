using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Представляет обработчик этапа "Управление процессом".
    /// </summary>
    public class ProcessManagementStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProcessManagementStageTypeHandler"/>.
        /// </summary>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        public ProcessManagementStageTypeHandler(IKrScope krScope)
        {
            this.KrScope = krScope;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Выполняет отправку сигнала.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual async Task<StageHandlerResult> SendSignalAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess,
            string customSignal = null)
        {
            var signal = customSignal
                ?? context.Stage.SettingsStorage.TryGet<string>(KrConstants.KrProcessManagementStageSettingsVirtual.Signal);
            if (!managePrimaryProcess || !context.MainCardID.HasValue || string.IsNullOrWhiteSpace(signal))
            {
                return StageHandlerResult.SkipResult;
            }

            var mainCard = await this.KrScope
                .GetMainCardAsync(context.MainCardID.Value, cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            mainCard.GetWorkflowQueue()
                .AddSignal(KrConstants.KrProcessName, signal);
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет переход на начало текущей группы.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual async Task<StageHandlerResult> CurGroupTransitionAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            if (!managePrimaryProcess || !context.MainCardID.HasValue)
            {
                return StageHandlerResult.CurrentGroupTransition();
            }

            var mainCard = await this.KrScope
                .GetMainCardAsync(context.MainCardID.Value, cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            mainCard.GetWorkflowQueue()
                .AddSignal(
                    KrConstants.KrProcessName,
                    KrConstants.KrTransitionGlobalSignal,
                    parameters: new Dictionary<string, object>
                    {
                        [KrConstants.KrTransitionCurrentGroup] = BooleanBoxes.True,
                    });
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет переход на предыдущую группу.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual async Task<StageHandlerResult> PrevGroupTransitionAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            if (!managePrimaryProcess || !context.MainCardID.HasValue)
            {
                return StageHandlerResult.PreviousGroupTransition();
            }

            var mainCard = await this.KrScope
                .GetMainCardAsync(context.MainCardID.Value, cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            mainCard.GetWorkflowQueue()
                .AddSignal(
                    KrConstants.KrProcessName,
                    KrConstants.KrTransitionGlobalSignal,
                    parameters: new Dictionary<string, object>
                    {
                        [KrConstants.KrTransitionPrevGroup] = BooleanBoxes.True,
                    });
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет переход на следующую группу.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual async Task<StageHandlerResult> NextGroupTransitionAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            if (!managePrimaryProcess || !context.MainCardID.HasValue)
            {
                return StageHandlerResult.NextGroupTransition();
            }

            var mainCard = await this.KrScope
                .GetMainCardAsync(context.MainCardID.Value, cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            mainCard.GetWorkflowQueue()
                .AddSignal(
                    KrConstants.KrProcessName,
                    KrConstants.KrTransitionGlobalSignal,
                    parameters: new Dictionary<string, object>
                    {
                        [KrConstants.KrTransitionNextGroup] = BooleanBoxes.True,
                    });
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет переход на этап.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual async Task<StageHandlerResult> StageTransitionAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            var transitToStage = context.Stage.SettingsStorage
                .TryGet<Guid?>(KrConstants.KrProcessManagementStageSettingsVirtual.StageRowID);

            if (!transitToStage.HasValue)
            {
                return StageHandlerResult.SkipResult;
            }

            if (!managePrimaryProcess || !context.MainCardID.HasValue)
            {
                return StageHandlerResult.Transition(transitToStage.Value);
            }

            var mainCard = await this.KrScope
                .GetMainCardAsync(context.MainCardID.Value, cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            mainCard.GetWorkflowQueue()
                .AddSignal(
                    KrConstants.KrProcessName,
                    KrConstants.KrTransitionGlobalSignal,
                    parameters: new Dictionary<string, object>
                    {
                        [KrConstants.StageRowID] = transitToStage.Value,
                    });
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет переход на группу.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual async Task<StageHandlerResult> GroupTransitionAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            var transitToGroup = context.Stage.SettingsStorage
                .TryGet<Guid?>(KrConstants.KrProcessManagementStageSettingsVirtual.StageGroupID);

            if (!transitToGroup.HasValue)
            {
                return StageHandlerResult.SkipResult;
            }

            if (!managePrimaryProcess || !context.MainCardID.HasValue)
            {
                return StageHandlerResult.GroupTransition(transitToGroup.Value);
            }

            var mainCard = await this.KrScope
                .GetMainCardAsync(context.MainCardID.Value, cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            mainCard.GetWorkflowQueue()
                .AddSignal(
                    KrConstants.KrProcessName,
                    KrConstants.KrTransitionGlobalSignal,
                    parameters: new Dictionary<string, object>
                    {
                        [KrConstants.StageGroupID] = transitToGroup.Value,
                    });
            return StageHandlerResult.CompleteResult;
        }

        /// <summary>
        /// Выполняет отмену процесса.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual Task<StageHandlerResult> CancelProcessAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            if (!managePrimaryProcess)
            {
                return Task.FromResult(StageHandlerResult.CancelProcessResult);
            }

            return this.SendSignalAsync(context, true, KrConstants.KrCancelProcessGlobalSignal);
        }

        /// <summary>
        /// Выполняет пропуск процесса.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="managePrimaryProcess">Признак управления основным процессом.</param>
        /// <returns>Результат выполнения.</returns>
        protected virtual Task<StageHandlerResult> SkipProcessAsync(
            IStageTypeHandlerContext context,
            bool managePrimaryProcess)
        {
            if (!managePrimaryProcess)
            {
                return Task.FromResult(StageHandlerResult.SkipProcessResult);
            }

            return this.SendSignalAsync(context, true, KrConstants.KrSkipProcessGlobalSignal);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var managePrimaryProcessFlag = context.Stage.SettingsStorage
                .TryGet<bool?>(KrConstants.KrProcessManagementStageSettingsVirtual.ManagePrimaryProcess) ?? false;
            var managePrimaryProcess = managePrimaryProcessFlag
                && context.ProcessInfo?.ProcessTypeName != KrConstants.KrProcessName;

            var modeInt = context.Stage.SettingsStorage
                .TryGet<int?>(KrConstants.KrProcessManagementStageSettingsVirtual.ModeID);

            if (!modeInt.HasValue
                || !(0 <= modeInt && modeInt <= (int) ProcessManagementStageTypeMode.SkipProcessMode))
            {
                context.ValidationResult.AddError(this, "$KrStages_ProcessManagement_ModeNotSpecified");
                return Task.FromResult(StageHandlerResult.SkipResult);
            }

            // Этап синхронный и в любом случае будет завершен.
            // В дальнейшем переходы могут либо пропустить следующие этапы, либо отменять предыдущие
            // В случае пропуска следующих, текущий должен быть Completed, а не Skipped.
            // Это обрабатывается в KrProcessHelper.SetSkipStateToSubsequentStages
            context.Stage.State = KrStageState.Completed;

            var mode = (ProcessManagementStageTypeMode) modeInt;

            switch (mode)
            {
                case ProcessManagementStageTypeMode.StageMode:
                    return this.StageTransitionAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.GroupMode:
                    return this.GroupTransitionAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.NextGroupMode:
                    return this.NextGroupTransitionAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.PrevGroupMode:
                    return this.PrevGroupTransitionAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.CurrentGroupMode:
                    return this.CurGroupTransitionAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.SendSignalMode:
                    return this.SendSignalAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.CancelProcessMode:
                    return this.CancelProcessAsync(context, managePrimaryProcess);
                case ProcessManagementStageTypeMode.SkipProcessMode:
                    return this.SkipProcessAsync(context, managePrimaryProcess);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
