using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Properties.Resharper;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.ForkDescriptor"/>.
    /// </summary>
    public class ForkStageTypeHandler : ForkStageTypeHandlerBase
    {
        #region Nested Types

        /// <summary>
        /// Предоставляет информацию о ветке процесса.
        /// </summary>
        // ReSharper disable once ClassCanBeSealed.Global
        [StorageObjectGenerator(GenerateDefaultConstructor = false)]
        public partial class BranchInfo : StorageObject
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="BranchInfo"/>.
            /// </summary>
            /// <param name="rowID">Идентификатор строки вложенного процесса в <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.</param>
            /// <param name="nestedWorkflowProcessID">Идентификатор экземпляра вложенного процесса или значение <see cref="Guid.Empty"/>, если при запуске процесса произошла ошибка.</param>
            /// <param name="secondaryProcessID">Идентификатор вторичного процесса из <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID"/>.</param>
            /// <param name="completed">Значение <see langword="true"/>, если вложенный процесс завершён, инаеч - <see langword="false"/>.</param>
            public BranchInfo(
                Guid rowID,
                Guid nestedWorkflowProcessID,
                Guid secondaryProcessID,
                bool completed)
                : base(new Dictionary<string, object>())
            {
                this.Init(nameof(this.RowID), rowID);
                this.Init(nameof(this.ProcessID), nestedWorkflowProcessID);
                this.Init(nameof(this.SecondaryProcessID), secondaryProcessID);
                this.Init(nameof(this.Completed), BooleanBoxes.Box(completed));
            }

            /// <inheritdoc />
            public BranchInfo(
                Dictionary<string, object> storage)
                : base(storage)
            {
            }

            /// <summary>
            /// Возвращает идентификатор строки вложенного процесса в <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.
            /// </summary>
            public Guid RowID => this.Get<Guid>(nameof(this.RowID));

            /// <summary>
            /// Возвращает идентификатор экземпляра вложенного процесса или значение <see cref="Guid.Empty"/>, если при запуске процесса произошла ошибка.
            /// </summary>
            public Guid ProcessID => this.Get<Guid>(nameof(this.ProcessID));

            /// <summary>
            /// Возвращает идентификатор вторичного процесса из <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID"/>.
            /// </summary>
            public Guid SecondaryProcessID => this.Get<Guid>(nameof(this.SecondaryProcessID));

            /// <summary>
            /// Возвращает значение, показывающее, что вложенный процесс завершён.
            /// </summary>
            public bool Completed => this.Get<bool>(nameof(this.Completed));
        }

        /// <summary>
        /// Предоставляет контекст сценария <see cref="AfterNestedMethodDescriptor"/>.
        /// </summary>
        // ReSharper disable once ClassCanBeSealed.Global
        public class ScriptContext
        {
            /// <summary>
            /// Возвращает или задаёт значение, показывающее, необходимо ли завершить выполнение ветвления и перейти к следующему этапу или нет.
            /// </summary>
            [UsedImplicitly]
            public bool SkipForkAndContinueRoute { get; set; }

            /// <summary>
            /// Возвращает или задаёт значение, показывающее, необходимо ли отозвать оставшиеся ветки (<see langword="false"/>) или оставить - <see langword="true"/>. Используется только с флагом <see cref="SkipForkAndContinueRoute"/>.
            /// </summary>
            [UsedImplicitly]
            public bool KeepBranchesAlive { get; set; }

            /// <summary>
            /// Возвращает или задаёт дополнительную информацию завершённого вложенного процесса.
            /// </summary>
            [UsedImplicitly]
            public IDictionary<string, object> ProcessInfo { get; set; }

            /// <summary>
            /// Возвращает или задаёт информацию о вторичном процессе.
            /// </summary>
            [UsedImplicitly]
            public IKrSecondaryProcess SecondaryProcess { get; set; }

            /// <summary>
            /// Возвращает или задаёт коллекцию содержащую информацию о ветках этапа.
            /// </summary>
            [UsedImplicitly]
            public ListStorage<BranchInfo> BranchInfos { get; set; }
        }

        #endregion

        #region Constants And Static Fields

        /// <summary>
        /// Дескриптор метода "После завершения ветки".
        /// </summary>
        public static readonly KrExtraSourceDescriptor AfterNestedMethodDescriptor = new KrExtraSourceDescriptor("AfterNested")
        {
            DisplayName = "$KrStages_Fork_AfterNested",
            ParameterName = "NestedProcessInfo",
            ParameterType = $"global::{typeof(ForkStageTypeHandler).FullName}.{nameof(ScriptContext)}",
            ScriptField = KrConstants.KrForkSettingsVirtual.AfterEachNestedProcess
        };

        /// <summary>
        /// Ключ, по которому в <see cref="Stage.InfoStorage"/> содержится коллекция объектов, содержащих информацию по веткам этапа "Ветвление".
        /// </summary>
        public const string PendingProcesses = nameof(PendingProcesses);

        #endregion

        #region Fields

        /// <summary>
        /// Фабрика объектов <see cref="BranchInfo"/>.
        /// </summary>
        public static readonly IStorageValueFactory<int, BranchInfo> BranchInfoFactory =
            new DictionaryStorageValueFactory<int, BranchInfo>(
                (key, storage) => new BranchInfo(storage));

        #endregion

        #region Properties

        /// <inheritdoc cref="IKrProcessLauncher"/>
        protected IKrProcessLauncher ProcessLauncher { get; }

        /// <inheritdoc cref="IKrProcessCache"/>
        protected IKrProcessCache ProcessCache { get; }

        /// <inheritdoc cref="IKrStageTemplateCompilationCache"/>
        protected IKrStageTemplateCompilationCache CompilationCache { get; }

        /// <summary>
        /// Unity-контейнер.
        /// </summary>
        protected IUnityContainer UnityContainer { get; }

        /// <inheritdoc cref="IDbScope"/>
        protected IDbScope DbScope { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ForkStageTypeHandler"/>.
        /// </summary>
        /// <param name="processLauncher">Загрузчик процессов.</param>
        /// <param name="processCache">Кэш с данными из карточек шаблонов этапов.</param>
        /// <param name="compilationCache"><inheritdoc cref="CompilationCache" path="/summary"/></param>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        public ForkStageTypeHandler(
            IKrProcessLauncher processLauncher,
            IKrProcessCache processCache,
            IKrStageTemplateCompilationCache compilationCache,
            IUnityContainer unityContainer,
            IDbScope dbScope)
        {
            this.ProcessLauncher = NotNullOrThrow(processLauncher);
            this.ProcessCache = NotNullOrThrow(processCache);
            this.CompilationCache = NotNullOrThrow(compilationCache);
            this.UnityContainer = NotNullOrThrow(unityContainer);
            this.DbScope = NotNullOrThrow(dbScope);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleStageStartAsync(
            IStageTypeHandlerContext context)
        {
            if (context.ProcessHolderSatellite is not null)
            {
                NestedStagesCleaner.ClearStage(
                    context.ProcessHolderSatellite,
                    context.Stage.RowID);
            }

            var branchInfos = new ListStorage<BranchInfo>(new List<object>(), BranchInfoFactory);
            var processInfos = GetProcessInfos(context.Stage);
            var result = await this.StartNestedProcessesAsync(
                context,
                branchInfos,
                processInfos,
                EnumerateSecondaryProcessRows(context));

            if (result != StageHandlerResult.EmptyResult)
            {
                return result;
            }

            context.Stage.InfoStorage[PendingProcesses] = branchInfos.GetStorage();

            return branchInfos.All(p => p.Completed)
                ? StageHandlerResult.CompleteResult
                : StageHandlerResult.InProgressResult;
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleSignalAsync(
            IStageTypeHandlerContext context)
        {
            var signal = context.SignalInfo.Signal;

            if (signal.Name == KrConstants.AsyncForkedProcessCompletedSingal
                && signal.Parameters.TryGetValue(KrConstants.Keys.ProcessID, out var pidObj)
                && pidObj is Guid pid)
            {
                return await this.HandleBranchCompletionAsync(context, pid);
            }

            if (signal.Name == KrConstants.ForkAddBranchSignal
                && signal.Parameters.TryGetValue(nameof(BranchAdditionInfo), out var bai)
                && bai is IList baiStorage)
            {
                return await this.HandleBranchAdditionAsync(context, new ListStorage<BranchAdditionInfo>(baiStorage, BranchAdditionInfoFactory));
            }

            if (signal.Name == KrConstants.ForkRemoveBranchSignal
                && signal.Parameters.TryGetValue(nameof(BranchRemovalInfo), out var bri)
                && bri is Dictionary<string, object> briStorage)
            {
                return await this.HandleBranchRemovalAsync(context, new BranchRemovalInfo(briStorage));
            }

            return await base.HandleSignalAsync(context);
        }

        /// <inheritdoc />
        public override async Task<bool> HandleStageInterruptAsync(
            IStageTypeHandlerContext context)
        {
            var card = await context.MainCardAccessStrategy.GetCardAsync(
                cancellationToken: context.CancellationToken);

            if (card is null)
            {
                return false;
            }

            var branchInfos = new ListStorage<BranchInfo>(
                context.Stage.InfoStorage.Get<List<object>>(PendingProcesses),
                BranchInfoFactory);

            if (branchInfos.Count == 0)
            {
                return true;
            }

            var workflowQueue = card.GetWorkflowQueue();

            var signal = context.DirectionAfterInterrupt switch
            {
                DirectionAfterInterrupt.Forward => KrConstants.KrSkipProcessGlobalSignal,
                DirectionAfterInterrupt.Backward => KrConstants.KrCancelProcessGlobalSignal,
                _ => throw new InvalidOperationException(
                    $"Unknown value \"{nameof(context)}.{nameof(context.DirectionAfterInterrupt)}\": \"{context.DirectionAfterInterrupt}\"."),
            };

            // Вложенные процессы должны завершаться до продолжения обработки процесса.
            // Это, например, необходимо для поддержки возможности перехода из вложенного процесса на текущую группу с пересчётом.
            var isComplete = true;

            foreach (var binfo in branchInfos)
            {
                if (binfo.Completed)
                {
                    continue;
                }

                // Вложенный процесс мог быть уже завершён при обработке глобального сигнала.
                if (!await this.IsAliveProcessAsync(
                        binfo.ProcessID,
                        context.CancellationToken))
                {
                    SetCompleted(binfo);

                    continue;
                }

                workflowQueue
                    .AddSignal(
                        KrConstants.KrNestedProcessName,
                        signal,
                        processID: binfo.ProcessID);

                isComplete = false;
            }

            return isComplete;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Обрабатывает завершение ветки.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="pid">Идентификатор завершающегося процесса.</param>
        /// <returns>Результат обработки.</returns>
        protected async Task<StageHandlerResult> HandleBranchCompletionAsync(
            IStageTypeHandlerContext context,
            Guid pid)
        {
            var branchInfos = new ListStorage<BranchInfo>(context.Stage.InfoStorage.Get<List<object>>(PendingProcesses), BranchInfoFactory);

            // Если процесс окажется неизвестным или голос подает уже завершенный процесс,
            // то просто пропускаем, ничего не делая.
            var binfo = branchInfos.FirstOrDefault(p => p.ProcessID.Equals(pid));
            if (binfo is null
                || binfo.Completed)
            {
                return await base.HandleSignalAsync(context);
            }

            SetCompleted(binfo);

            var nestedProcessInfo = context
                .SignalInfo
                .Signal
                .Parameters
                .TryGet<IDictionary<string, object>>(KrConstants.Keys.ProcessInfoAtEnd);
            SetProcessInfo(GetProcessInfos(context.Stage), binfo.RowID, nestedProcessInfo);

            var afterNestedMethodScriptContext = await this.RunScriptAsync(
                context,
                binfo,
                context.SignalInfo.Signal.Parameters.TryGet<IDictionary<string, object>>(
                    KrConstants.Keys.ProcessInfoAtEnd),
                branchInfos);

            if (afterNestedMethodScriptContext is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            if (afterNestedMethodScriptContext.SkipForkAndContinueRoute)
            {
                return StageHandlerResult.CompleteResult;
            }

            return branchInfos.All(p => p.Completed)
                ? StageHandlerResult.CompleteResult
                : StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Обрабатывает создание ветки.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="additionInfos"></param>
        /// <returns>Результат обработки.</returns>
        protected async Task<StageHandlerResult> HandleBranchAdditionAsync(
            IStageTypeHandlerContext context,
            ListStorage<BranchAdditionInfo> additionInfos)
        {
            var stageRowID = context.Stage.RowID;
            var newRows = new List<object>(additionInfos.Count);
            var processInfos = new Dictionary<string, object>(additionInfos.Count, StringComparer.Ordinal);

            foreach (var ai in additionInfos)
            {
                var rowID = Guid.NewGuid();
                newRows.Add(new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    [KrConstants.KrForkSecondaryProcessesSettingsVirtual.RowID] = rowID,
                    [KrConstants.StageRowIDReferenceToOwner] = stageRowID,
                    [KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID] = ai.SecondaryProcessID,
                    [KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessName] = ai.SecondaryProcessName,
                });

                SetProcessInfo(processInfos, rowID, ai.StartingProcessInfo ?? new Dictionary<string, object>(StringComparer.Ordinal));
            }

            var branchInfos = new ListStorage<BranchInfo>(context.Stage.InfoStorage.Get<List<object>>(PendingProcesses), BranchInfoFactory);
            var result = await this.StartNestedProcessesAsync(context, branchInfos, processInfos, newRows.Cast<Dictionary<string, object>>());
            if (result != StageHandlerResult.EmptyResult)
            {
                return result;
            }

            var list = context
                .Stage
                .SettingsStorage
                .TryGet<IList>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic);

            if (list is not null)
            {
                foreach (var newRow in newRows)
                {
                    list.Add(newRow);
                }
            }

            return StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Обрабатывает удаление ветки.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="branchRemovalInfo">Информация необходимая для удаления ветки.</param>
        /// <returns>Результат обработки.</returns>
        protected async Task<StageHandlerResult> HandleBranchRemovalAsync(
            IStageTypeHandlerContext context,
            BranchRemovalInfo branchRemovalInfo)
        {
            var signal = branchRemovalInfo.DirectionAfterInterrupt switch
            {
                DirectionAfterInterrupt.Forward => KrConstants.KrSkipProcessGlobalSignal,
                DirectionAfterInterrupt.Backward => KrConstants.KrCancelProcessGlobalSignal,
                _ => throw new InvalidOperationException(
                    $"Unknown value \"{nameof(context)}.{nameof(context.DirectionAfterInterrupt)}\": \"{context.DirectionAfterInterrupt}\"."),
            };

            var sp = branchRemovalInfo.SecondaryProcesses;

            // Сателлит есть всегда, т.к. синхронный процесс не может получать сигналы.
            var branchInfos = new ListStorage<BranchInfo>((List<object>) context.Stage.InfoStorage[PendingProcesses], BranchInfoFactory);
            var np = await this.GetProcessIDsOfNestedsAsync(
                context.ProcessHolderSatellite.ID,
                branchInfos,
                branchRemovalInfo.NestedProcesses);

            var card = await context.MainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

            if (card is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            var workflowQueue = card.GetWorkflowQueue();

            foreach (var binfo in branchInfos
                .Where(p => !p.Completed && (sp.Contains(p.SecondaryProcessID) || np.Contains(p.ProcessID))))
            {
                workflowQueue
                    .AddSignal(
                        KrConstants.KrNestedProcessName,
                        signal,
                        processID: binfo.ProcessID);

                // При удалении ветки, если вложенный процесс является асинхронным, не завершаем этап и процесс сразу.
                // Процесс будет завершён при обработке события завершения ветки KrConstants.AsyncForkedProcessCompletedSingal.
                // Этап будет завершён после обработки всех событий завершения веток.
                if (!(await this.ProcessCache.GetSecondaryProcessAsync(binfo.SecondaryProcessID, context.CancellationToken)).Async)
                {
                    SetCompleted(binfo);
                }
            }

            return branchInfos.All(p => p.Completed)
                ? StageHandlerResult.CompleteResult
                : StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Выполняет сценарий после завершения ветки.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="binfo">Информация о ветке процесса.</param>
        /// <param name="processInfo">Дополнительная информация завершённого вложенного процесса.</param>
        /// <param name="branchInfos">Коллекция, содержащая информацию о ветках этапа.</param>
        /// <returns>Контекст сценария <see cref="AfterNestedMethodDescriptor"/> или значение <see langword="null"/>, если произошла ошибка компиляции сценария <see cref="AfterNestedMethodDescriptor"/>.</returns>
        protected async Task<ScriptContext?> RunScriptAsync(
            IStageTypeHandlerContext context,
            BranchInfo binfo,
            IDictionary<string, object> processInfo,
            ListStorage<BranchInfo> branchInfos)
        {
            var ctx = new ScriptContext
            {
                SecondaryProcess = await this.ProcessCache.GetSecondaryProcessAsync(
                    binfo.SecondaryProcessID,
                    context.CancellationToken),
                ProcessInfo = processInfo,
                BranchInfos = branchInfos,
            };

            if (context.Stage.TemplateID.HasValue)
            {
                var compilationObject = await this.CompilationCache.GetAsync(
                    context.Stage.TemplateID.Value,
                    cancellationToken: context.CancellationToken);

                var inst = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrRuntimeClass,
                        SourceIdentifiers.StageAlias,
                        context.Stage.ID),
                    context.ValidationResult,
                    true);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return null;
                }

                if (inst is not null)
                {
                    await HandlerHelper.InitScriptContextAsync(this.UnityContainer, inst, context);
                    await inst.InvokeExtraAsync(AfterNestedMethodDescriptor.MethodName, ctx);
                }
            }

            if (ctx.SkipForkAndContinueRoute)
            {
                await ProcessKeepBranchesAliveAsync(context, ctx, branchInfos);
            }

            return ctx;
        }

        /// <summary>
        /// Обрабатывает завершение веток этапа при завершении одной из них.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="ctx">Контекст сценария <see cref="AfterNestedMethodDescriptor"/>.</param>
        /// <param name="branchInfos">Коллекция содержащая информацию о ветках этапа.</param>
        /// <returns>Асинхронная задача.</returns>
        protected static async Task ProcessKeepBranchesAliveAsync(
            IStageTypeHandlerContext context,
            ScriptContext ctx,
            ListStorage<BranchInfo> branchInfos)
        {
            if (ctx.KeepBranchesAlive)
            {
                return;
            }

            var card = await context.MainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

            if (card is null)
            {
                return;
            }

            var workflowQueue = card.GetWorkflowQueue();

            foreach (var binfo in branchInfos.Where(p => !p.Completed))
            {
                workflowQueue
                    .AddSignal(
                        KrConstants.KrNestedProcessName,
                        KrConstants.KrSkipProcessGlobalSignal,
                        processID: binfo.ProcessID);
            }
        }

        /// <summary>
        /// Проверяет, что вторичный процесс, имеющий указанный идентификатор, существует и имеет правильный тип, т.е. не является асинхронным, если текущий основной процесс является синхронным.
        /// </summary>
        /// <param name="processID">Идентификатор вторичного процесса.</param>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Значение <see langword="true"/>, если проверка успешно пройдена, иначе - <see langword="false"/>.</returns>
        protected async Task<bool> CheckSecondaryProcessAsync(
            Guid processID,
            IStageTypeHandlerContext context)
        {
            try
            {
                var process = await this.ProcessCache.GetSecondaryProcessAsync(processID, context.CancellationToken);
                if (process.Async
                    && context.RunnerMode == KrProcessRunnerMode.Sync)
                {
                    context.ValidationResult.AddError(this, "$KrStages_Fork_CannotRunAsyncIntoSync");
                    return false;
                }

                return true;
            }
            catch (InvalidOperationException)
            {
                context.ValidationResult.AddError(this, "$KrStages_Fork_SecondaryProcessMissed");
                return false;
            }
        }

        /// <summary>
        /// Запускает новый вложенный процесс.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="branchInfos">Коллекция содержащая информацию о ветках этапа.</param>
        /// <param name="processInfos">Коллекция ключ-значение: ключ - идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/> представленный в строковом представлении по формату <see cref="ForkStageTypeHandlerBase.ForkSecondaryProcessesRowIDFormat"/>; значение - коллекцию ключ-значение содержащая информацию по вложенному запускаемому процессу, тип значения <see cref="IDictionary{TKey, TValue}"/>, где TKey - <see cref="string"/>, TValue - <see cref="object"/>.</param>
        /// <param name="secProcsRows">Перечисление коллекций ключ-значение содержащих значения элементов из <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.</param>
        /// <returns>Результат обработки.</returns>
        protected async Task<StageHandlerResult> StartNestedProcessesAsync(
            IStageTypeHandlerContext context,
            ListStorage<BranchInfo> branchInfos,
            IDictionary<string, object> processInfos,
            IEnumerable<IDictionary<string, object>> secProcsRows)
        {
            var parentProcessTypeName = context.ProcessInfo?.ProcessTypeName;
            var parentProcessID = context.ProcessInfo?.ProcessID;
            var order = branchInfos.Count;

            foreach (var secondaryProcessRow in secProcsRows)
            {
                var rowID = secondaryProcessRow.TryGet<Guid>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.RowID);
                var processID =
                    secondaryProcessRow.TryGet<Guid>(
                        KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessID);
                if (!await this.CheckSecondaryProcessAsync(processID, context))
                {
                    return StageHandlerResult.CompleteResult;
                }

                var processInfo = GetProcessInfo(processInfos, rowID);
                var forkProcessBuilder = KrProcessBuilder
                    .CreateProcess()
                    .SetProcess(processID)
                    .SetProcessInfo(processInfo);

                if (context.MainCardID.HasValue)
                {
                    forkProcessBuilder.SetCard(context.MainCardID.Value);
                }

                forkProcessBuilder.SetNestedProcess(
                    context.ProcessHolder.ProcessHolderID,
                    parentProcessTypeName,
                    parentProcessID,
                    context.Stage.RowID,
                    order++);
                var forkProcess = forkProcessBuilder.Build();

                var result = await this.ProcessLauncher.LaunchAsync(forkProcess);
                context.ValidationResult.Add(result.ValidationResult);

                var complete = result.LaunchStatus == KrProcessLaunchStatus.Complete;

                var branchInfo = new BranchInfo(
                    rowID,
                    result.ProcessID ?? Guid.Empty,
                    processID,
                    complete);
                branchInfos.Add(branchInfo);

                if (complete)
                {
                    SetProcessInfo(processInfos, rowID, result.ProcessInfo);

                    var afterNestedMethodScriptContext = await this.RunScriptAsync(
                        context,
                        branchInfo,
                        result.ProcessInfo,
                        branchInfos);

                    if (afterNestedMethodScriptContext is null)
                    {
                        return StageHandlerResult.EmptyResult;
                    }

                    if (afterNestedMethodScriptContext.SkipForkAndContinueRoute)
                    {
                        return StageHandlerResult.CompleteResult;
                    }
                }

                if (result.LaunchStatus == KrProcessLaunchStatus.Error)
                {
                    break;
                }
            }

            return StageHandlerResult.EmptyResult;
        }

        /// <summary>
        /// Возвращает коллекцию идентификаторов завершаемых вложенных процессов.
        /// </summary>
        /// <param name="id">Идентификатор текущего сателлита процесса.</param>
        /// <param name="branchInfos">Коллекция содержащая информацию о ветках этапа.</param>
        /// <param name="nestedProcessIDList">Коллекция идентификаторов завершаемых вложенных процессов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция идентификаторов завершаемых вложенных процессов.</returns>
        protected async Task<IList<Guid>> GetProcessIDsOfNestedsAsync(
            Guid id,
            ListStorage<BranchInfo> branchInfos,
            IList<Guid> nestedProcessIDList,
            CancellationToken cancellationToken = default)
        {
            if (nestedProcessIDList.Count == 0)
            {
                return EmptyHolder<Guid>.Collection;
            }

            var resultProcesses = new List<Guid>();
            await using (this.DbScope.Create())
            {
                var db = this.DbScope.Db;
                var query = this.DbScope.BuilderFactory
                    .Select().C(null, "RowID", "Params")
                    .From("WorkflowProcesses").NoLock()
                    .Where().C("ID").Equals().P("ID")
                    .And().C("RowID").InArray(branchInfos.Where(p => !p.Completed).Select(p => p.ProcessID), "ProcessIDs", out var processIDs)
                    .Build();
                db
                    .SetCommand(
                        query,
                        DataParameters.Get(
                            db.Parameter("ID", id, DataType.Guid),
                            processIDs))
                    .LogCommand();

                await using var reader = await db.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    var processID = reader.GetGuid(0);

                    var paramsJson = await reader.GetSequentialNullableStringAsync(1, cancellationToken);
                    if (paramsJson is null)
                    {
                        continue;
                    }

                    var paramsStorage = StorageHelper.DeserializeFromTypedJson(paramsJson);

                    var nestedProcessID = paramsStorage.TryGet<Guid>(KrConstants.Keys.NestedProcessID);
                    if (nestedProcessIDList.Contains(nestedProcessID))
                    {
                        resultProcesses.Add(processID);
                    }
                }
            }

            return resultProcesses;
        }

        /// <summary>
        /// Устанавливает признак показывающий, что вложенный процесс завершён.
        /// </summary>
        /// <param name="binfo">Информация о ветке процесса.</param>
        protected static void SetCompleted(BranchInfo binfo) =>
            // У BranchInfo.Completed отсутствует сеттер, чтобы нельзя его было сменить в скриптах
            // Но тут нам можно. Распространять такую практику не следует.
            binfo.GetStorage()[nameof(BranchInfo.Completed)] = BooleanBoxes.True;

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает значение, показывающее, активен ли процесс или нет.
        /// </summary>
        /// <param name="processID">Идентификатор процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если процесс существует, иначе - <see langword="false"/>.</returns>
        private async Task<bool> IsAliveProcessAsync(
            Guid processID,
            CancellationToken cancellationToken = default)
        {
            await using var _ = this.DbScope.Create();

            var db = this.DbScope.Db;

            return await db
                .SetCommand(
                    this.DbScope.BuilderFactory
                        .Select()
                            .V(true)
                        .From("WorkflowProcesses").NoLock()
                        .Where().C("RowID").Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", processID, DataType.Guid))
                .LogCommand()
                .ExecuteAsync<bool>(cancellationToken);
        }

        #endregion
    }
}
