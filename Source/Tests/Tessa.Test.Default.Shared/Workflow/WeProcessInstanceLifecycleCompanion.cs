using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Operations;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Workflow;
using Tessa.Workflow.Signals;
using Tessa.Workflow.Storage;

namespace Tessa.Test.Default.Shared.Workflow
{
    /// <summary>
    /// Предоставляет методы для управления жизненным циклом карточки, в которой запущен экземпляр бизнес-процесса.
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class WeProcessInstanceLifecycleCompanion :
        IPendingActionsProvider<IPendingAction, WeProcessInstanceLifecycleCompanion>,
        ICardLifecycleCompanion<WeProcessInstanceLifecycleCompanion>
    {
        #region Constants

        private const string SendSignalInfoKey = CardHelper.SystemKeyPrefix + nameof(WeProcessInstanceLifecycleCompanion.SendSignal) + "." + "Signal";

        #endregion

        #region Fields

        private readonly CardLifecycleCompanion cardLifecycle;
        private readonly IWorkflowService workflowService;
        private readonly IWorkflowEngineProcessor workflowEngineProcessor;
        private readonly ICardTransactionStrategy cardTransactionStrategy;
        private readonly IDbScope dbScope;
        private readonly IOperationRepository operationRepository;
        private readonly IWorkflowEngineCache workflowEngineCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WeProcessInstanceLifecycleCompanion"/>.
        /// </summary>
        /// <param name="cardLifecycle">Объект, управляющий жизненным циклом карточки, в которой запущен бизнес-процесс.</param>
        /// <param name="processInstanceID">Идентификатор экземпляра бизнес-процесса запущенного в карточке управляемой <paramref name="cardLifecycle"/>.</param>
        /// <param name="workflowService">Сервис для управления шаблонами, экземплярами и подписками Бизнес-процесса.</param>
        /// <param name="workflowEngineProcessor">Объект-обработчик процессов WorkflowEngine на сервере.</param>
        /// <param name="cardTransactionStrategy">Стратегия обеспечения блокировок reader/writer при выполнении операций с карточкой.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="operationRepository">Репозиторий, управляющий операциями.</param>
        /// <param name="workflowEngineCache">Объект для получения шаблонов процессов с кешированием их.</param>
        public WeProcessInstanceLifecycleCompanion(
            CardLifecycleCompanion cardLifecycle,
            Guid processInstanceID,
            IWorkflowService workflowService,
            IWorkflowEngineProcessor workflowEngineProcessor,
            ICardTransactionStrategy cardTransactionStrategy,
            IDbScope dbScope,
            IOperationRepository operationRepository,
            IWorkflowEngineCache workflowEngineCache)
        {
            this.cardLifecycle = NotNullOrThrow(cardLifecycle);
            this.ProcessInstanceID = processInstanceID;
            this.workflowService = NotNullOrThrow(workflowService);
            this.workflowEngineProcessor = NotNullOrThrow(workflowEngineProcessor);
            this.cardTransactionStrategy = NotNullOrThrow(cardTransactionStrategy);
            this.dbScope = NotNullOrThrow(dbScope);
            this.operationRepository = NotNullOrThrow(operationRepository);
            this.workflowEngineCache = NotNullOrThrow(workflowEngineCache);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор экземпляра бизнес-процесса.
        /// </summary>
        public Guid ProcessInstanceID { get; }

        /// <inheritdoc/>
        public Guid CardID => this.cardLifecycle.CardID;

        /// <inheritdoc/>
        public Guid? CardTypeID => this.cardLifecycle.CardTypeID;

        /// <inheritdoc/>
        public string CardTypeName => this.cardLifecycle.CardTypeName;

        /// <inheritdoc/>
        public Card Card => this.cardLifecycle.Card;

        /// <inheritdoc/>
        public ICardLifecycleCompanionDependencies Dependencies => this.cardLifecycle.Dependencies;

        /// <inheritdoc/>
        public ICardLifecycleCompanionData LastData => this.cardLifecycle.LastData;

        #endregion

        #region Public Methods

        /// <summary>
        /// Возвращает значение, показывающее, что процесс, с идентификатором <see cref="ProcessInstanceID"/>, является активным.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если процесс является активным, иначе - <see langword="false"/>.</returns>
        public async Task<bool> IsAliveAsync(CancellationToken cancellationToken = default) =>
            (await this.GetProcessInstanceAsync(cancellationToken)).Item1 is not null;

        /// <summary>
        /// Возвращает экземпляр процесса с идентификатором <see cref="ProcessInstanceID"/>.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж: &lt;Экземпляр процесса WorkflowEngine или значение <see langword="null"/>, если процесс не активен; Результат выполнения&gt;.</returns>
        public Task<(WorkflowProcessStateStorage, ValidationResult)> GetProcessInstanceAsync(CancellationToken cancellationToken = default) =>
            this.workflowService.GetProcessStateAsync(this.ProcessInstanceID, cancellationToken);

        /// <summary>
        /// Отправляет указанный сигнал на все подписанные на него узлы процесса.
        /// </summary>
        /// <param name="signalType">Тип сигнала.</param>
        /// <param name="signalHash">Дополнительная информация.</param>
        /// <returns>Объект <see cref="WeProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// После отправки сигнала выполняется загрузка карточки.<para/>
        /// Обратите внимание: после выполнения метода, последним запланированным действием становится загрузка карточки.<para/>
        /// Последний запрос на обработку сигнала в <see cref="IWorkflowEngineProcessor"/> и ответ на него можно получить в <see cref="WeProcessInstanceLifecycleCompanion.LastData"/> в свойствах <see cref="ICardLifecycleCompanionData.OtherRequests"/> и <see cref="ICardLifecycleCompanionData.OtherResponses"/> по ключам <see cref="WorkflowTestHelper.WorkflowEngineProcessRequestKey"/> и <see cref="WorkflowTestHelper.WorkflowEngineProcessResultKey"/>, соответственно.
        /// </remarks>
        public WeProcessInstanceLifecycleCompanion SendSignal(
            string signalType,
            Dictionary<string, object> signalHash = null)
        {
            var action = new PendingAction(
                nameof(WeProcessInstanceLifecycleCompanion) + "." + nameof(this.SendSignal),
                this.SendSignalInternalAsync);
            action.Info[SendSignalInfoKey] = new WorkflowEngineSignal(signalType, signalHash);

            this.Load()
                .GetLastPendingAction().AddPreparationAction(action);

            return this;
        }

        /// <summary>
        /// Выполняет асинхронные операции созданные бизнес-процессами.
        /// </summary>
        /// <param name="executeNewOperations">Значение <see langword="true"/>, если необходимо выполнить операции созданные после выполнения других операций, иначе - <see langword="false"/>, если необходимо выполнить только текущие операции. Значение по умолчанию: <see langword="true"/>.</param>
        /// <param name="executeAllProcessOperations">Значение <see langword="true"/>, если должны быть выполнены все операции, иначе - <see langword="false"/>, если должны быть выполнены только операции созданные процессом с идентификатором <see cref="ProcessInstanceID"/> и его дочерними процессами. Значение по умолчанию: <see langword="false"/>.</param>
        /// <returns>Объект <see cref="WeProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// После обработки операций выполняется загрузка карточки.<para/>
        /// Обратите внимание: после выполнения метода, последним запланированным действием становится загрузка карточки.
        /// </remarks>
        public WeProcessInstanceLifecycleCompanion ProcessAsyncOperations(
            bool executeNewOperations = true,
            bool executeAllProcessOperations = false)
        {
            var action = new PendingAction(
                nameof(WeProcessInstanceLifecycleCompanion) + "." + nameof(this.ProcessAsyncOperations),
                async (_, ct) => executeAllProcessOperations
                    ? await this.ProcessAsyncOperationsAsync(executeNewOperations, ct)
                    : await this.ProcessAsyncOperationsAsync(this.ProcessInstanceID, executeNewOperations, ct));

            this.Load()
                .GetLastPendingAction()
                .AddPreparationAction(action);

            return this;
        }

        /// <summary>
        /// Обрабатывает все активные таймеры расположенные в процессе с идентификатором <see cref="ProcessInstanceID"/> и его дочерних процессах.
        /// </summary>
        /// <returns>Объект <see cref="WeProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// После обработки таймеров выполняется загрузка карточки.<para/>
        /// Обратите внимание: после выполнения метода, последним запланированным действием становится загрузка карточки.<para/>
        /// </remarks>
        public WeProcessInstanceLifecycleCompanion ProcessTimerOperations()
        {
            this.Load()
                .GetLastPendingAction()
                .AddPreparationAction(
                    new PendingAction(
                    nameof(WeProcessInstanceLifecycleCompanion) + "." + nameof(this.ProcessTimerOperations),
                    this.ProcessTimerOperationsAsync));

            return this;
        }

        #endregion

        #region ICardLifecycleCompanion<T> Members

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion Create(Action<CardNewRequest> modifyRequestAction = null)
        {
            this.cardLifecycle.Create(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion Save(Action<CardStoreRequest> modifyRequestAction = null)
        {
            this.cardLifecycle.Save(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion Load(Action<CardGetRequest> modifyRequestAction = null)
        {
            this.cardLifecycle.Load(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion Delete(Action<CardDeleteRequest> modifyRequestAction = null)
        {
            this.cardLifecycle.Delete(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion WithInfoPair(string key, object val)
        {
            this.cardLifecycle.WithInfoPair(key, val);
            return this;
        }

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion WithInfo(Dictionary<string, object> info)
        {
            this.cardLifecycle.WithInfo(info);
            return this;
        }

        /// <inheritdoc/>
        public WeProcessInstanceLifecycleCompanion CreateOrLoadSingleton()
        {
            _ = this.cardLifecycle.CreateOrLoadSingleton();
            return this;
        }

        #endregion

        #region ICardLifecycleCompanion Members

        /// <inheritdoc/>
        public ValueTask<ICardFileContainer> GetCardFileContainerAsync(
            IFileRequest request = null,
            IList<IFileTag> additionalTags = null,
            CancellationToken cancellationToken = default)
        {
            return this.cardLifecycle.GetCardFileContainerAsync(
                request: request,
                additionalTags: additionalTags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Card GetCardOrThrow() => this.cardLifecycle.GetCardOrThrow();

        #endregion

        #region IExecutePendingActions Members

        /// <inheritdoc/>
        public async ValueTask<WeProcessInstanceLifecycleCompanion> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            await this.cardLifecycle.GoAsync(
                validationFunc: validationFunc,
                cancellationToken: cancellationToken);
            return this;
        }

        #endregion

        #region ISealable Members

        /// <inheritdoc/>
        public bool IsSealed => this.cardLifecycle.IsSealed;

        /// <inheritdoc/>
        public void Seal() => this.cardLifecycle.Seal();

        #endregion

        #region IProvidePendingActions Members

        /// <inheritdoc/>
        public bool HasPendingActions => this.cardLifecycle.HasPendingActions;

        /// <inheritdoc/>
        public int Count => this.cardLifecycle.Count;

        /// <inheritdoc/>
        public IPendingAction this[int index] => this.cardLifecycle[index];

        /// <inheritdoc/>
        public void AddPendingAction(IPendingAction pendingAction) => this.cardLifecycle.AddPendingAction(pendingAction);

        /// <inheritdoc/>
        public IPendingAction GetLastPendingAction() => this.cardLifecycle.GetLastPendingAction();

        /// <inheritdoc/>
        public IEnumerator<IPendingAction> GetEnumerator() => this.cardLifecycle.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.cardLifecycle).GetEnumerator();

        #endregion

        #region Private Methods

        private async ValueTask<ValidationResult> SendSignalInternalAsync(
            IPendingAction action,
            CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResultBuilder();
            await this.cardTransactionStrategy.ExecuteInWriterLockAsync(
                this.cardLifecycle.GetCardOrThrow().ID,
                CardComponentHelper.DoNotCheckVersion,
                validationResult,
                async (p) =>
                {
                    var processRequest = new WorkflowEngineProcessRequest
                    {
                        ProcessFlag = WorkflowEngineProcessFlags.DefaultRuntime | WorkflowEngineProcessFlags.SendToSubscribers,
                        StoreCard = this.cardLifecycle.GetCardOrThrow(),
                        Signal = action.Info.Get<WorkflowEngineSignal>(SendSignalInfoKey),
                        ProcessInstanceID = this.ProcessInstanceID,
                    };

                    this.LastData.OtherRequests[WorkflowTestHelper.WorkflowEngineProcessRequestKey] = processRequest;
                    this.LastData.OtherResponses[WorkflowTestHelper.WorkflowEngineProcessResultKey] = default;

                    var processResult = await this.workflowEngineProcessor.ProcessSignalAsync(processRequest, p.CancellationToken);
                    this.LastData.OtherResponses[WorkflowTestHelper.WorkflowEngineProcessResultKey] = processResult;
                    p.ValidationResult.Add(processResult.ValidationResult);

                    if (!p.ValidationResult.IsSuccessful())
                    {
                        p.ReportError = true;
                    }
                },
                cancellationToken: cancellationToken);

            return validationResult.Build();
        }

        private async Task<ValidationResult> ProcessAsyncOperationsAsync(
            Guid processInstanceID,
            bool executeNewOperations = false,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.CreateNew())
            {
                var db = this.dbScope.Db;
                var validationResult = new ValidationResultBuilder();
                bool hasProcessed; // Признак обработки хотя бы одного асинхронного действия.
                do
                {
                    hasProcessed = false;

                    // 1. Проверяем наличие операций имеющих тип OperationTypes.WorkflowAsync.
                    var operations = await this.operationRepository.GetAllAsync(
                            OperationTypes.WorkflowAsync,
                            true,
                            cancellationToken);

                    // 2. Операций нет. Выходим.
                    if (operations.Count == 0)
                    {
                        return validationResult.Build();
                    }

                    // 3. Получаем список идентификаторов процессов управляемых этим объектом.
                    var processes = await this.GetTreeProcessesAsync(processInstanceID, cancellationToken);

                    // 4. Обрабатываем операции.
                    foreach (var operation in operations)
                    {
                        var currentProcessInstanceID = operation.Request.Info.TryGet<Guid?>("ProcessInstanceID");

                        if (!currentProcessInstanceID.HasValue)
                        {
                            validationResult.AddError(
                                this,
                                $"Process ID is not specified in operation with ID = {operation.ID:B}.");
                            return validationResult.Build();
                        }

                        // 5. Текущая операция относится к процессу не входящему в группу процессов управляемых этим объектом.
                        if (!processes.Contains(currentProcessInstanceID.Value))
                        {
                            continue;
                        }

                        // 6. Обрабатываем операцию.
                        await this.operationRepository.StartAsync(
                            operation.ID,
                            operation.TypeID,
                            cancellationToken);

                        await this.ProcessAsyncOperationAsync(
                            operation,
                            currentProcessInstanceID.Value,
                            validationResult,
                            cancellationToken);

                        if (!validationResult.IsSuccessful())
                        {
                            return validationResult.Build();
                        }

                        await this.operationRepository.DeleteAsync(operation.ID, operation.TypeID, cancellationToken);

                        hasProcessed = true;
                    }

                    // 7. После выполнения операций могли быть созданы новые. Повторяем выполнение.
                } while (executeNewOperations && hasProcessed);

                return validationResult.Build();
            }
        }

        private async Task<ValidationResult> ProcessAsyncOperationsAsync(
            bool executeNewOperations = false,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.CreateNew())
            {
                var db = this.dbScope.Db;
                var validationResult = new ValidationResultBuilder();

                do
                {
                    // 1. Пытаемся запустить операцию. Если операции нет, то выходим.
                    var operationID = await this.operationRepository.StartFirstAsync(
                        OperationTypes.WorkflowAsync,
                        cancellationToken);

                    if (!operationID.HasValue)
                    {
                        return validationResult.Build();
                    }

                    var operation = await this.operationRepository.TryGetAsync(
                        operationID.Value,
                        cancellationToken: cancellationToken);
                    if (operation is null)
                    {
                        return validationResult.Build();
                    }

                    var processInstanceID = operation.Request.Info.TryGet<Guid?>("ProcessInstanceID");

                    if (!processInstanceID.HasValue)
                    {
                        validationResult.AddError(
                            this,
                            $"Process ID is not specified in operation with ID = {operation.ID:B}.");
                        return validationResult.Build();
                    }

                    // 2. Обрабатываем операцию.
                    await this.operationRepository.StartAsync(
                        operation.ID,
                        operation.TypeID,
                        cancellationToken);

                    await this.ProcessAsyncOperationAsync(
                        operation,
                        processInstanceID.Value,
                        validationResult,
                        cancellationToken);

                    if (!validationResult.IsSuccessful())
                    {
                        return validationResult.Build();
                    }

                    await this.operationRepository.DeleteAsync(operation.ID, operation.TypeID, cancellationToken);
                }
                // 3. После выполнения операций могли быть созданы новые. Повторяем выполнение.
                while (executeNewOperations);

                return validationResult.Build();
            }
        }

        private async Task ProcessAsyncOperationAsync(
            IOperation operation,
            Guid processInstanceID,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var (processInstance, valResult) = await this.workflowService.GetProcessStateAsync(
                processInstanceID,
                cancellationToken);

            if (!valResult.IsSuccessful)
            {
                validationResult.Add(valResult);
                return;
            }

            var processTemplate = await this.workflowEngineCache.GetAsync(
                processInstance.TemplateID,
                cancellationToken);

            if (processTemplate is null)
            {
                validationResult.AddError(
                    this,
                    $"The version of the process template with the identifier \"{processInstance.TemplateID:B}\" was not found.");
                return;
            }

            var workflowCardID = await this.workflowService.GetProcessCardIDAsync(
                processInstanceID,
                cancellationToken);

            if (!workflowCardID.HasValue)
            {
                validationResult.AddError(
                    this,
                    $"A process card for the process instance with ID = {processInstanceID:B} was not found.");
                return;
            }

            var opRequestInfo = operation.Request.Info;
            var signalType = opRequestInfo.TryGet<string>("SignalType");
            var signalHash = opRequestInfo.TryGet<Dictionary<string, object>>("SignalHash");

            var signal = signalType is null
                ? WorkflowEngineSignal.CreateDefaultSignal()
                : new WorkflowEngineSignal(signalType, signalHash);

            var nodeInstanceID = opRequestInfo.TryGet<Guid?>("NodeInstanceID");
            var lockProcess = opRequestInfo.TryGet<bool>("LockProcess");

            await this.cardTransactionStrategy.ExecuteInTransactionAsync(
                validationResult,
                async p =>
                {
                    var result = await this.workflowEngineProcessor.SendSignalAsync(
                        processInstanceID,
                        signal,
                        null,
                        nodeInstanceID,
                        lockProcess
                            ? WorkflowEngineProcessFlags.DefaultAsync | WorkflowEngineProcessFlags.LockProcess
                            : WorkflowEngineProcessFlags.DefaultAsync,
                        cancellationToken: p.CancellationToken);

                    p.ValidationResult.Add(result.ValidationResult);

                    if (!p.ValidationResult.IsSuccessful())
                    {
                        p.ReportError = true;
                    }
                },
                cancellationToken);
        }

        /// <summary>
        /// Возвращает список идентификаторов процессов, которыми управляет процесс с заданным идентификатором.
        /// </summary>
        /// <param name="processID">Идентификатор родительского процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Список идентификаторов процессов, которыми управляет процесс с заданным идентификатором.</returns>
        private async Task<IList<Guid>> GetTreeProcessesAsync(
            Guid processID,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;

                return await db
                    .SetCommand(
                        this.dbScope.BuilderFactory
                            .With("ChildProcess", e => e
                                    .Select()
                                        .P("RootRowID")
                                    .UnionAll()
                                    .Select()
                                        .C("p", "RowID")
                                    .From("WorkflowEngineProcesses", "p").NoLock()
                                    .InnerJoin("ChildProcess", "cp")
                                    .On().C("cp", "RowID").Equals().C("p", "ParentRowID"),
                                columnNames: new[] { "RowID" },
                                recursive: true)
                            .Select()
                                .C("p", "RowID")
                            .From("WorkflowEngineProcesses", "p").NoLock()
                            .InnerJoin("ChildProcess", "cp")
                                .On().C("cp", "RowID").Equals().C("p", "RowID")
                            .Build(),
                        db.Parameter("RootRowID", processID))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken);
            }
        }

        private async ValueTask<ValidationResult> ProcessTimerOperationsAsync(
            IPendingAction action,
            CancellationToken cancellationToken = default)
        {
            // 1. Получение всех подписок таймеров.
            var allTimerSubscriptions = await this.workflowService.GetAllModifiedTimerSubscriptionsAsync(
                null,
                cancellationToken);

            if (allTimerSubscriptions.Count == 0)
            {
                return ValidationResult.Empty;
            }

            // 2. Получение идентификаторов процессов, которыми выполняется управление.
            var processes = await this.GetTreeProcessesAsync(this.ProcessInstanceID, cancellationToken);

            // 3. Обработка подписок таймеров.
            var validationResult = new ValidationResultBuilder();

            foreach (var workflowTimerSubscriptionStorage in allTimerSubscriptions)
            {
                if (!processes.Contains(workflowTimerSubscriptionStorage.ProcessID.Value))
                {
                    continue;
                }

                await this.ProcessTimerAsync(
                    workflowTimerSubscriptionStorage,
                    validationResult,
                    cancellationToken);

                if (!validationResult.IsSuccessful())
                {
                    break;
                }
            }

            return validationResult.Build();
        }

        private async Task ProcessTimerAsync(
            WorkflowTimerSubscriptionStorage workflowTimerSubscriptionStorage,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var timerID = workflowTimerSubscriptionStorage.ID;
            var nodeID = workflowTimerSubscriptionStorage.NodeID.Value;
            var processID = workflowTimerSubscriptionStorage.ProcessID.Value;
            var runOnce = workflowTimerSubscriptionStorage.RunOnce;

            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                // Проверяем наличие подписки.
                var timerExists = (
                    await db
                        .SetCommand(
                            builderFactory
                                .Select().Top(1).V(1).From("WorkflowEngineTimerSubscriptions", "ts").NoLock()
                                .Where().C("RowID").Equals().P("TimerID")
                                .Limit(1)
                                .Build(),
                            db.Parameter("TimerID", timerID))
                        .LogCommand()
                        .ExecuteAsync<int?>(cancellationToken)
                    )
                    .HasValue;

                if (!timerExists)
                {
                    return;
                }

                await this.cardTransactionStrategy.ExecuteInTransactionAsync(
                    validationResult,
                    async p =>
                    {
                        var result = await this.workflowEngineProcessor.SendSignalAsync(
                            processID,
                            new WorkflowEngineTimerSignal(new Dictionary<string, object>(StringComparer.Ordinal))
                            {
                                TimerIDs = new List<object> { timerID },
                                Type = WorkflowSignalTypes.TimerTick,
                            },
                            null,
                            nodeID,
                            WorkflowEngineProcessFlags.DefaultAsync,
                            cancellationToken: p.CancellationToken);

                        p.ValidationResult.Add(result.ValidationResult);

                        if (!p.ValidationResult.IsSuccessful())
                        {
                            p.ReportError = true;
                        }

                        if (runOnce)
                        {
                            await this.workflowService.DeleteTimerSubscriptionAsync(
                                timerID,
                                p.CancellationToken);
                        }
                    },
                    cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает строковое представление объекта, отображаемое в окне отладчика.
        /// </summary>
        /// <returns>Строковое представление объекта, отображаемое в окне отладчика.</returns>
        private string GetDebuggerDisplay()
        {
            return $"{nameof(this.CardID)} = {this.CardID:B}, " +
                $"{nameof(this.CardTypeID)} = {FormattingHelper.FormatNullable(this.CardTypeID, "B")}, " +
                $"{nameof(this.CardTypeName)} = {FormattingHelper.FormatNullable(this.CardTypeName)}, " +
                $"CardIsSet = {this.Card is not null}";
        }

        #endregion
    }
}
