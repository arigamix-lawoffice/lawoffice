using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public sealed class KrProcessWorkflowWorker : WorkflowWorker<KrProcessWorkflowManager>
    {
        #region Constructors

        public KrProcessWorkflowWorker(KrProcessWorkflowManager manager)
            : base(manager)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// WorkflowContext
        /// </summary>
        private KrProcessWorkflowContext WCtx => this.Manager.WorkflowContext;

        #endregion

        #region Private Methods

        private async Task StartRunnerAsync(IWorkflowProcessInfo info, CancellationToken cancellationToken = default)
        {
            // ValidationResult здесь и во всех вложенных выводах используется из context.ValidationResult
            // расширения на сохранение карточки. Таким образом все, что могло возникнуть здесь и глубже, будет корректно
            // выведено.

            var scope = this.WCtx.KrScope;
            if (!scope.MultirunEnabled(info.ProcessID)
                && !scope.FirstLaunchPerRequest(info.ProcessID))
            {
                return;
            }

            scope.AddToLaunchedLevels(info.ProcessID);

            var startingSecondaryProcess = this.WCtx.CardStoreContext.Request.GetStartingSecondaryProcess();
            StoreParentProcess(info, startingSecondaryProcess);

            var mainCardID = this.Manager.Request.Card.ID;
            var contextualSatellite = await this.WCtx.KrScope.GetKrSatelliteAsync(
                mainCardID,
                cancellationToken: cancellationToken);

            if (contextualSatellite is null)
            {
                return;
            }

            // Получаем холдер процесса в зависимости от типа процесса и его вложенности
            (var processHolderSatellite, var processHolderID, var processHolder) = await this.GetProcessHolderAsync(
                info,
                startingSecondaryProcess,
                contextualSatellite,
                cancellationToken);

            var processHolderCreated = false;
            var isNested = string.Equals(info.ProcessTypeName, KrNestedProcessName, StringComparison.Ordinal);

            if (processHolder is null)
            {
                // Холдер отсутствует, т.е. для текущего сателлита-холдер запускается самый верхний процесс.
                processHolder = new ProcessHolder();
                processHolderCreated = true;

                // Если запускается нестед, значит по нестеду совершено какое-то действие (уже был запущен ранее)
                // Иначе запускается основной процесс
                processHolder.MainProcessType = isNested
                    ? GetMainProcessType(info, null)
                    : info.ProcessTypeName;

                processHolder.Persistent = true;
                processHolder.ProcessHolderID = processHolderID;

                this.WCtx.KrScope.AddProcessHolder(processHolder);
            }

            // Строим модель процесса на основе доступных сателлитов и холдеров.
            (var workflowProcess, var pci) = await this.GetWorkflowProcessAsync(
                info,
                startingSecondaryProcess,
                contextualSatellite,
                processHolderSatellite,
                isNested,
                processHolder,
                cancellationToken);

            if (!processHolder.Persistent)
            {
                this.Manager.ValidationResult.AddError(
                    this,
                    "$KrProcess_Error_AsyncProcessWithoutPersistentHolder");
                return;
            }

            var mainCardKey = scope.LockCard(mainCardID);
            var contextualKey = scope.LockCard(contextualSatellite.ID);
            var processHolderKey = scope.LockCard(processHolderSatellite.ID);

            if (startingSecondaryProcess is not null)
            {
                StorageHelper.Merge(startingSecondaryProcess.ProcessInfo, workflowProcess.InfoStorage);
            }

            var secondaryProcess = await this.GetSecondaryProcessAsync(
                info,
                startingSecondaryProcess?.SecondaryProcessID,
                pci,
                cancellationToken);

            await using var cardLoadingStrategy = new KrScopeMainCardAccessStrategy(this.WCtx.CardID, this.WCtx.KrScope);
            var taskHistoryResolver = new KrTaskHistoryResolver(
                cardLoadingStrategy,
                this.WCtx,
                this.WCtx.ValidationResult,
                this.WCtx.TaskHistoryManager);

            var notMessageHasNoActiveStages = info.ProcessParameters.TryGet<bool>(Keys.NotMessageHasNoActiveStages)
                || secondaryProcess?.NotMessageHasNoActiveStages == true;

            var runnerContext = new KrProcessRunnerContext(
                workflowAPI: new WorkflowAPIBridge(this.Manager, info),
                taskHistoryResolver: taskHistoryResolver,
                mainCardAccessStrategy: cardLoadingStrategy,
                cardID: this.WCtx.CardID,
                cardType: this.WCtx.CardType,
                docTypeID: this.WCtx.DocTypeID,
                krComponents: this.WCtx.KrComponents,
                contextualSatellite: contextualSatellite,
                processHolderSatellite: processHolderSatellite,
                workflowProcess: workflowProcess,
                processHolder: processHolder,
                processInfo: info,
                validationResult: this.Manager.ValidationResult,
                cardContext: this.WCtx.CardStoreContext,
                defaultPreparingGroupStrategyFunc: this.DefaultPreparingStrategy,
                parentProcessTypeName: info.ProcessParameters.TryGet<string>(Keys.ParentProcessType),
                parentProcessID: info.ProcessParameters.TryGet<Guid?>(Keys.ParentProcessID),
                isProcessHolderCreated: processHolderCreated,
                updateCardFuncAsync: this.UpdateCardAsync,
                notMessageHasNoActiveStages: notMessageHasNoActiveStages,
                secondaryProcess: secondaryProcess,
                cancellationToken: cancellationToken);

            await this.WCtx.AsyncProcessRunner.RunAsync(runnerContext);

            if (runnerContext.WorkflowProcess.CurrentApprovalStageRowID is null)
            {
                await this.WCtx.EventManager.RaiseAsync(
                    DefaultEventTypes.AsyncProcessCompleted,
                    currentStage: null,
                    runnerMode: KrProcessRunnerMode.Async,
                    runnerContext: runnerContext,
                    cancellationToken: cancellationToken);

                this.Manager.ProcessesAwaitingRemoval.Add(info);

                this.WCtx.CardStoreContext.Info.SetProcessInfoAtEnd(workflowProcess.InfoStorage);

                if (runnerContext.InitiationCause == KrProcessRunnerInitiationCause.StartProcess)
                {
                    this.WCtx.CardStoreContext.Info.SetAsyncProcessCompletedSimultaniosly();
                }
            }

            await runnerContext.UpdateCardAsync();

            if (runnerContext.IsProcessHolderCreated)
            {
                this.WCtx.KrScope.RemoveProcessHolder(processHolder.ProcessHolderID);
            }

            scope.ReleaseCard(mainCardID, mainCardKey);
            scope.ReleaseCard(contextualSatellite.ID, contextualKey);
            scope.ReleaseCard(processHolderSatellite.ID, processHolderKey);
        }

        /// <summary>
        /// Возвращает объектную модель и основную информацию по процессу.
        /// </summary>
        /// <param name="info">Информация по процессу.</param>
        /// <param name="startingSecondaryProcess">Информация для запуска процесса посредством <see cref="WorkflowStoreExtension"/>.</param>
        /// <param name="contextualSatellite">Контекстуальный сателлит - сателлит основного процесса.</param>
        /// <param name="processHolderSatellite">Карточка процессного сателлита. Содержит информацию о текущем процессе (для вложенного процесса - это сателлит родительского процесса). Если текущий процесс является основным, то он равен <paramref name="contextualSatellite"/>.</param>
        /// <param name="isNested">Значение <see langword="true"/>, если текущий процесс является вложенным, иначе - <see langword="false"/>.</param>
        /// <param name="processHolder">Объект, предоставляющий информацию по текущему и основному процессу.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж &lt;Объектная модель процесса; Информация по процессу&gt;.</returns>
        private async ValueTask<(WorkflowProcess workflowProcess, ProcessCommonInfo pci)> GetWorkflowProcessAsync(
            IWorkflowProcessInfo info,
            StartingSecondaryProcessInfo startingSecondaryProcess,
            Card contextualSatellite,
            Card processHolderSatellite,
            bool isNested,
            ProcessHolder processHolder,
            CancellationToken cancellationToken = default)
        {
            processHolder.MainProcessCommonInfo ??= this.WCtx.ObjectModelMapper.GetMainProcessCommonInfo(processHolderSatellite);

            processHolder.PrimaryProcessCommonInfo ??= processHolder.MainProcessType == KrProcessName
                ? processHolder.MainProcessCommonInfo
                : this.WCtx.ObjectModelMapper.GetMainProcessCommonInfo(contextualSatellite, false);

            if (isNested
                && processHolder.NestedProcessCommonInfos is null)
            {
                processHolder.SetNestedProcessCommonInfosList(this.WCtx.ObjectModelMapper.GetNestedProcessCommonInfos(processHolderSatellite));
            }

            ProcessCommonInfo pci = null;
            WorkflowProcess workflowProcess = null;
            var nestedProcessID = GetNestedProcessID(info, isNested);

            if (isNested
                && nestedProcessID.HasValue)
            {
                if (!processHolder.NestedProcessCommonInfos.TryGetItem(nestedProcessID.Value, out var npci))
                {
                    npci = new NestedProcessCommonInfo(
                        null,
                        new Dictionary<string, object>(StringComparer.Ordinal),
                        startingSecondaryProcess.SecondaryProcessID ?? Guid.Empty,
                        nestedProcessID.Value,
                        startingSecondaryProcess.ParentStageRowID ?? Guid.Empty,
                        startingSecondaryProcess.NestedOrder ?? 0);
                    processHolder.NestedProcessCommonInfos.Add(npci);
                }

                if (!processHolder.NestedWorkflowProcesses.TryGetValue(nestedProcessID.Value, out workflowProcess))
                {
                    var (templates, stages) = await this.WCtx.ProcessCache.GetRelatedTemplatesAsync(
                        processHolderSatellite,
                        nestedProcessID,
                        cancellationToken);

                    workflowProcess = await this.WCtx.ObjectModelMapper.CardRowsToObjectModelAsync(
                        processHolderSatellite,
                        npci,
                        processHolder.MainProcessCommonInfo,
                        templates,
                        stages,
                        info.ProcessTypeName,
                        cancellationToken: cancellationToken);

                    this.WCtx.ObjectModelMapper.FillWorkflowProcessFromPci(
                        workflowProcess,
                        npci,
                        processHolder.PrimaryProcessCommonInfo);

                    processHolder.NestedWorkflowProcesses[nestedProcessID.Value] = workflowProcess;
                }

                pci = npci;
            }
            else if (!isNested)
            {
                workflowProcess = processHolder.MainWorkflowProcess;

                if (workflowProcess is null)
                {
                    var (templates, stages) = await this.WCtx.ProcessCache.GetRelatedTemplatesAsync(
                        processHolderSatellite,
                        nestedProcessID,
                        cancellationToken);

                    workflowProcess = await this.WCtx.ObjectModelMapper.CardRowsToObjectModelAsync(
                        processHolderSatellite,
                        processHolder.MainProcessCommonInfo,
                        processHolder.MainProcessCommonInfo,
                        templates,
                        stages,
                        info.ProcessTypeName,
                        cancellationToken: cancellationToken);

                    this.WCtx.ObjectModelMapper.FillWorkflowProcessFromPci(
                        workflowProcess,
                        processHolder.MainProcessCommonInfo,
                        processHolder.PrimaryProcessCommonInfo);

                    processHolder.MainWorkflowProcess = workflowProcess;
                }

                pci = processHolder.MainProcessCommonInfo;
            }

            return (workflowProcess, pci);
        }

        private static Guid? GetNestedProcessID(IWorkflowProcessInfo info, bool nested)
        {
            var nestedProcessID = info.ProcessParameters.TryGet<Guid?>(Keys.NestedProcessID);

            if (nestedProcessID is null && nested)
            {
                nestedProcessID = Guid.NewGuid();
                info.ProcessParameters[Keys.NestedProcessID] = nestedProcessID;
                info.PendingProcessParametersUpdate = true;
                return nestedProcessID;
            }

            return nestedProcessID;
        }

        /// <summary>
        /// Возвращает объект содержащий информацию по текущему и основному процессу (<see cref="ProcessHolder"/>).
        /// </summary>
        /// <param name="info">Информация по процессу.</param>
        /// <param name="startingSecondaryProcess">Информация для запуска процесса посредством <see cref="WorkflowStoreExtension"/>.</param>
        /// <param name="contextualSatellite">Контекстуальный сателлит - сателлит основного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж &lt;Карточка процессного сателлита. Содержит информацию о текущем процессе (для вложенного процесса - это сателлит родительского процесса); Идентификатор по которому можно получить процессный сателлит; Объект, предоставляющий информацию по текущему и основному процессу&gt;.</returns>
        private async ValueTask<(Card ProcessHolderSatellite, Guid ProcessHolderID, ProcessHolder ProcessHolder)> GetProcessHolderAsync(
            IWorkflowProcessInfo info,
            StartingSecondaryProcessInfo startingSecondaryProcess,
            Card contextualSatellite,
            CancellationToken cancellationToken = default)
        {
            var mainCardID = this.Manager.Request.Card.ID;
            Card processHolderSatellite; // Процессный сателлит. Содержит информацию о текущем процессе (для вложенного процесса - это сателлит родительского процесса).

            // Идентификатор по которому можно получить процессный сателлит.
            // Основной процесс - идентификатор карточки в которой запущен процесс.
            // Вторичный процесс - идентификатор вторичного процесса.
            // Вложенный процесс - если родительский процесс основной, то идентификатор основной карточки, если вторичный - идентификатор вторичного процесса.
            Guid processHolderID;
            ProcessHolder processHolder;

            switch (info.ProcessTypeName)
            {
                case KrProcessName:
                    processHolderSatellite = contextualSatellite;
                    processHolderID = mainCardID;
                    processHolder = this.TryGetProcessHolder(processHolderID);
                    break;
                case KrSecondaryProcessName:
                    processHolderSatellite = await this.WCtx.KrScope.GetSecondaryKrSatelliteAsync(info.ProcessID, cancellationToken);
                    processHolderID = info.ProcessID;
                    processHolder = this.TryGetProcessHolder(processHolderID);
                    break;
                case KrNestedProcessName:
                    var processHolderIDNullable = GetProcessHolderID(info, startingSecondaryProcess);
                    if (processHolderIDNullable is null)
                    {
                        throw new InvalidOperationException(
                            $"Process holder ID is null for process type {KrNestedProcessName}");
                    }

                    processHolder = this.TryGetProcessHolder(processHolderIDNullable.Value);
                    var mainProcessType = GetMainProcessType(info, processHolder);

                    switch (mainProcessType)
                    {
                        case KrProcessName:
                            processHolderID = mainCardID;
                            processHolderSatellite = contextualSatellite;
                            break;
                        case KrSecondaryProcessName:
                            processHolderID = processHolderIDNullable.Value;
                            processHolderSatellite = await this.WCtx.KrScope.GetSecondaryKrSatelliteAsync(processHolderID, cancellationToken);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(mainProcessType), mainProcessType, "Unsupported process type.");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(info) + "." + nameof(info.ProcessTypeName), info.ProcessTypeName, "Unsupported process type.");
            }

            return (processHolderSatellite, processHolderID, processHolder);
        }

        private static void StoreParentProcess(
            IWorkflowProcessInfo info,
            StartingSecondaryProcessInfo startingSecondaryProcess)
        {
            if (startingSecondaryProcess is not null)
            {
                info.ProcessParameters[Keys.ParentProcessType] = startingSecondaryProcess.ParentProcessTypeName;
                info.ProcessParameters[Keys.ParentProcessID] = startingSecondaryProcess.ParentProcessID;
                info.PendingProcessParametersUpdate = true;
            }
        }

        private static Guid? GetProcessHolderID(
            IWorkflowProcessInfo info,
            StartingSecondaryProcessInfo startingSecondaryProcess)
        {
            var processHolderID = startingSecondaryProcess?.ProcessHolderID;
            if (processHolderID is not null)
            {
                info.ProcessParameters[Keys.ProcessHolderID] = processHolderID;
                info.PendingProcessParametersUpdate = true;
                return processHolderID;
            }

            return info.ProcessParameters.TryGet<Guid?>(Keys.ProcessHolderID);
        }

        private static string GetMainProcessType(
            IWorkflowProcessInfo info,
            ProcessHolder holder)
        {
            if (holder is not null)
            {
                info.ProcessParameters[Keys.MainProcessType] = holder.MainProcessType;
                info.PendingProcessParametersUpdate = true;
                return holder.MainProcessType;
            }

            return info.ProcessParameters.TryGet<string>(Keys.MainProcessType);
        }

        private async Task ObjectModelToCardRowsAsync(
            ProcessHolder processHolder,
            Card processHolderSatellite,
            Card contextualSatellite,
            Card mainCard,
            CancellationToken cancellationToken = default)
        {
            foreach (var nested in processHolder.NestedWorkflowProcesses)
            {
                var process = nested.Value;
                var npci = processHolder.NestedProcessCommonInfos[nested.Key];

                await this.WCtx.ObjectModelMapper.ObjectModelToCardRowsAsync(
                    process,
                    processHolderSatellite,
                    npci,
                    cancellationToken);

                this.WCtx.ObjectModelMapper.ObjectModelToPci(
                    process,
                    npci,
                    processHolder.MainProcessCommonInfo,
                    processHolder.PrimaryProcessCommonInfo);
            }

            if (processHolder.MainWorkflowProcess is not null)
            {
                await this.WCtx.ObjectModelMapper.ObjectModelToCardRowsAsync(
                    processHolder.MainWorkflowProcess,
                    processHolderSatellite,
                    null,
                    cancellationToken);

                this.WCtx.ObjectModelMapper.ObjectModelToPci(
                    processHolder.MainWorkflowProcess,
                    processHolder.MainProcessCommonInfo,
                    processHolder.MainProcessCommonInfo,
                    processHolder.PrimaryProcessCommonInfo);
            }

            // Перенос информации по основному процессу в контекстуальный сателлит.
            await this.WCtx.ObjectModelMapper.SetMainProcessCommonInfoAsync(
                mainCard,
                contextualSatellite,
                processHolder.PrimaryProcessCommonInfo,
                cancellationToken);

            // Если это не основной процесс, то перенести информацию по текущему процессу в его карточку-холдер.
            if (!ReferenceEquals(contextualSatellite, processHolderSatellite))
            {
                await this.WCtx.ObjectModelMapper.SetMainProcessCommonInfoAsync(
                    mainCard,
                    processHolderSatellite,
                    processHolder.MainProcessCommonInfo,
                    cancellationToken);
            }

            // Переносим оставшиеся процессы-нестеды
            this.WCtx.ObjectModelMapper.SetNestedProcessCommonInfos(
                processHolderSatellite,
                processHolder.NestedProcessCommonInfos);
        }

        private IPreparingGroupRecalcStrategy DefaultPreparingStrategy() =>
            new ForwardPreparingGroupRecalcStrategy(this.WCtx.DbScope, this.WCtx.Session);

        private async ValueTask<IKrSecondaryProcess> GetSecondaryProcessAsync(
            IWorkflowProcessInfo info,
            Guid? secondaryProcessID,
            ProcessCommonInfo pci,
            CancellationToken cancellationToken = default)
        {
            if (info.ProcessTypeName != KrSecondaryProcessName
                && info.ProcessTypeName != KrNestedProcessName)
            {
                return null;
            }

            var process = secondaryProcessID.HasValue
                ? await this.WCtx.ProcessCache.GetSecondaryProcessAsync(secondaryProcessID.Value, cancellationToken)
                : null;

            if (process is not null)
            {
                pci.SecondaryProcessID = process.ID;
                return process;
            }

            if (pci.SecondaryProcessID.HasValue)
            {
                return await this.WCtx.ProcessCache.GetSecondaryProcessAsync(pci.SecondaryProcessID.Value, cancellationToken);
            }

            return null;
        }

        private ProcessHolder TryGetProcessHolder(Guid? processHolderID)
        {
            return processHolderID is null
                ? null
                : this.WCtx.KrScope.GetProcessHolder(processHolderID.Value);
        }

        private async ValueTask UpdateCardAsync(
            IKrProcessRunnerContext context)
        {
            // Только создающий процесс холдер может переводить его обратно.
            if (context.IsProcessHolderCreated)
            {
                var mainCard = await context.MainCardAccessStrategy.GetCardAsync(
                    cancellationToken: context.CancellationToken);

                if (mainCard is null)
                {
                    return;
                }

                await this.ObjectModelToCardRowsAsync(
                    context.ProcessHolder,
                    context.ProcessHolderSatellite,
                    context.ContextualSatellite,
                    mainCard,
                    context.CancellationToken);
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override Task StartProcessCoreAsync(IWorkflowProcessInfo processInfo, CancellationToken cancellationToken = default)
        {
            return this.StartRunnerAsync(processInfo, cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task CompleteTaskCoreAsync(IWorkflowTaskInfo taskInfo, CancellationToken cancellationToken = default)
        {
            if (taskInfo.ProcessTypeName == KrSecondaryProcessName)
            {
                var card = await this.WCtx.KrScope.GetSecondaryKrSatelliteAsync(taskInfo.ProcessID, cancellationToken);

                if (card is null)
                {
                    return;
                }

                this.Manager.SpecifySatelliteID(card.ID, false);
            }

            await this.StartRunnerAsync(taskInfo, cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task ReinstateTaskCoreAsync(IWorkflowTaskInfo taskInfo, CancellationToken cancellationToken = default)
        {
            if (taskInfo.ProcessTypeName == KrSecondaryProcessName)
            {
                var card = await this.WCtx.KrScope.GetSecondaryKrSatelliteAsync(taskInfo.ProcessID, cancellationToken);

                if (card is null)
                {
                    return;
                }

                this.Manager.SpecifySatelliteID(card.ID);
            }

            await this.StartRunnerAsync(taskInfo, cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task<bool> ProcessSignalCoreAsync(IWorkflowSignalInfo signalInfo, CancellationToken cancellationToken = default)
        {
            if (signalInfo.ProcessTypeName == KrSecondaryProcessName)
            {
                var card = await this.WCtx.KrScope.GetSecondaryKrSatelliteAsync(signalInfo.ProcessID, cancellationToken);

                if (card is null)
                {
                    return false;
                }

                this.Manager.SpecifySatelliteID(card.ID, false);
            }

            await this.StartRunnerAsync(signalInfo, cancellationToken);
            return true;
        }

        #endregion

    }
}
