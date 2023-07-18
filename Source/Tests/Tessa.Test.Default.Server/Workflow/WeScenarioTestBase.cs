using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Operations;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Test.Default.Shared.Workflow;
using Tessa.Workflow;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Signals;
using Unity;

namespace Tessa.Test.Default.Server.Workflow
{
    /// <summary>
    /// Предоставляет базовую функциональность для тестирования процессов WorkflowEngine.
    /// </summary>
    public abstract class WeScenarioTestBase : WeTestBase
    {
        #region Constants

        /// <summary>
        /// Имя ключа, по которому в <see cref="CardInfoStorageObject.Info"/> карточки, в которой запущен бизнес-процесс, содержится значение флага, показывающего, запущен процесс из тестов или нет. Значение типа: <see cref="bool"/>.
        /// </summary>
        protected const string IsLaunchedInTestKey = "IsLaunchedInTest";

        /// <summary>
        /// Имя ключа, по которому в <see cref="CardInfoStorageObject.Info"/> карточки, в которой запущен бизнес-процесс, содержится метод инициализации бизнес-процесса при выполнении из тестов. Значение типа: <see cref="Func{T, TResult}"/>, где T - <see cref="WorkflowEngineCompiledBase"/>, TResult - <see cref="ValueTask"/>.
        /// </summary>
        protected const string TestInitializerActionKey = "TestInitializerAction";

        #endregion

        #region Fields

        /// <summary>
        /// Описание объекта-обработчика процессов WorkflowEngine на сервере.
        /// </summary>
        private IWorkflowEngineProcessor processor;

        /// <summary>
        /// Стратегия обеспечения блокировок reader/writer при выполнении операций с карточкой.
        /// </summary>
        private ICardTransactionStrategy transactionStrategy;

        /// <summary>
        /// Репозиторий, управляющий операциями.
        /// </summary>
        private IOperationRepository operationRepository;

        /// <summary>
        /// Объект для получения шаблонов процессов с кешированием их.
        /// </summary>
        private IWorkflowEngineCache workflowEngineCache;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор карточки шаблона процесса, используемого при тестировании.
        /// </summary>
        protected abstract Guid ProcessTemplateID { get; }

        /// <summary>
        /// Возвращает словарь содержащий: ключ - имя теста, значение - функция выполняющая проверку правильности выполнения теста.
        /// </summary>
        protected Dictionary<string, Func<WeProcessInstanceLifecycleCompanion, ValidationResult, ValueTask>> Scenarios { get; }
            = new Dictionary<string, Func<WeProcessInstanceLifecycleCompanion, ValidationResult, ValueTask>>(StringComparer.Ordinal);

        /// <summary>
        /// Возвращает словарь содержащий: ключ - имя теста, значение - функция выполняющая инициализацию теста.
        /// </summary>
        protected Dictionary<string, Func<WorkflowEngineCompiledBase, ValueTask>> Inits { get; }
            = new Dictionary<string, Func<WorkflowEngineCompiledBase, ValueTask>>(StringComparer.Ordinal);

        #endregion

        #region Test

        /// <summary>
        /// Выполняет тестирования сценария с указанным именем.
        /// </summary>
        /// <param name="scenarioName">Имя сценария.</param>
        /// <param name="isProcessAlive">Значение <see langword="false"/>, если процесс должен завершиться, иначе - <see langword="true"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual Task ScenarioTestAsync(
            string scenarioName,
            bool isProcessAlive = default,
            CancellationToken cancellationToken = default)
        {
            return this.ScenarioTestAsync(
                scenarioName,
                this.GetScenarioMethod(scenarioName),
                this.GetInitMethod(scenarioName),
                isProcessAlive: isProcessAlive,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Выполняет тестирования сценария запускаемого по указанному сигналу.
        /// </summary>
        /// <param name="signalType">Сигнал, запускающий тест.</param>
        /// <param name="scenarioMethodAsync">Метод выполняющий тестирование процесса.</param>
        /// <param name="initProcessMethodAsync">Метод выполняющий инициализацию запускаемого процесса.</param>
        /// <param name="initCardMethodAsync">Метод выполняющий инициализацию карточки в которой запускается процесс. Если метод не задан - планируется изменение темы карточки (поле <see cref="KrConstants.DocumentCommonInfo.Subject"/>). После выполнения метода выполняется сохранение карточки и запуск процесса.</param>
        /// <param name="isProcessAlive">Значение <see langword="false"/>, если процесс должен завершиться, иначе - <see langword="true"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public Task ScenarioTestAsync(
            string signalType,
            Func<WeProcessInstanceLifecycleCompanion, ValidationResult, ValueTask> scenarioMethodAsync,
            Func<WorkflowEngineCompiledBase, ValueTask> initProcessMethodAsync = default,
            Func<CardLifecycleCompanion, CancellationToken, ValueTask> initCardMethodAsync = default,
            bool isProcessAlive = default,
            CancellationToken cancellationToken = default)
        {
            return this.ScenarioTestAsync(
                new WorkflowEngineSignal(signalType),
                scenarioMethodAsync,
                initProcessMethodAsync,
                initCardMethodAsync,
                isProcessAlive,
                cancellationToken);
        }

        /// <summary>
        /// Выполняет тестирование процесса запускаемого заданным сигналом.
        /// </summary>
        /// <param name="signal">Сигнал, запускающий тестируемый процесс.</param>
        /// <param name="scenarioMethodAsync">Метод выполняющий тестирование процесса.</param>
        /// <param name="initProcessMethodAsync">Метод выполняющий инициализацию запускаемого процесса.</param>
        /// <param name="initCardMethodAsync">Метод выполняющий инициализацию карточки в которой запускается процесс. Если метод не задан - планируется изменение темы карточки (поле <see cref="KrConstants.DocumentCommonInfo.Subject"/>). После выполнения метода выполняется сохранение карточки и запуск процесса.</param>
        /// <param name="isProcessAlive">Значение <see langword="false"/>, если процесс должен завершиться, иначе - <see langword="true"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task ScenarioTestAsync(
            WorkflowEngineSignal signal,
            Func<WeProcessInstanceLifecycleCompanion, ValidationResult, ValueTask> scenarioMethodAsync,
            Func<WorkflowEngineCompiledBase, ValueTask> initProcessMethodAsync = default,
            Func<CardLifecycleCompanion, CancellationToken, ValueTask> initCardMethodAsync = default,
            bool isProcessAlive = default,
            CancellationToken cancellationToken = default)
        {
            var cardCompanion = this.CreateCardLifecycleCompanion()
                .Create()
                .WithDocType(this.TestDocTypeID, this.TestDocTypeName);

            if (initCardMethodAsync is null)
            {
                cardCompanion
                    .ModifyDocument();
            }
            else
            {
                await initCardMethodAsync(cardCompanion, cancellationToken);
            }

            await cardCompanion
                .Save()
                .GoAsync(cancellationToken: cancellationToken);

            var cardInfo = cardCompanion.Card.Info;
            cardInfo[IsLaunchedInTestKey] = BooleanBoxes.True;

            if (initProcessMethodAsync is not null)
            {
                cardInfo[TestInitializerActionKey] = initProcessMethodAsync;
            }

            var processInstanceCompanion =
                cardCompanion
                .CreateWorkflowEngineProcess(
                    this.transactionStrategy,
                    this.processor,
                    this.WorkflowService,
                    this.DbScope,
                    this.operationRepository,
                    this.workflowEngineCache,
                    this.ProcessTemplateID,
                    signal);

            var validationResults = new ValidationResultBuilder();

            await cardCompanion
                .GoAsync(
                    validationFunc: (validationResult) => validationResults.Add(validationResult),
                    cancellationToken: cancellationToken);

            await scenarioMethodAsync(processInstanceCompanion, validationResults.Build());

            Assert.AreEqual(
                isProcessAlive,
                await processInstanceCompanion.IsAliveAsync(cancellationToken),
                "Process completed.");
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task InitializeCoreAsync()
        {
            await base.InitializeCoreAsync();

            this.processor = this.UnityContainer.Resolve<IWorkflowEngineProcessor>();
            this.transactionStrategy = this.UnityContainer.Resolve<ICardTransactionStrategy>();
            this.operationRepository = this.UnityContainer.Resolve<IOperationRepository>();
            this.workflowEngineCache = this.UnityContainer.Resolve<IWorkflowEngineCache>();
        }

        /// <inheritdoc/>
        protected override async Task InitializeScopeCoreAsync()
        {
            await base.InitializeScopeCoreAsync();

            await this.ImportWorkflowCardsAsync();
        }

        #endregion

        #region Private Methods

        private Func<WeProcessInstanceLifecycleCompanion, ValidationResult, ValueTask> GetScenarioMethod(string scenarioName)
        {
            if (this.Scenarios.TryGetValue(scenarioName, out var scenarioMethodAsync))
            {
                return scenarioMethodAsync;
            }

            throw new InvalidOperationException($"Scenario '{scenarioName}' not initialized.");
        }

        private Func<WorkflowEngineCompiledBase, ValueTask> GetInitMethod(string scenarioName)
        {
            if (this.Inits.TryGetValue(scenarioName, out var initMethod))
            {
                return initMethod;
            }

            return null;
        }

        private Task ImportWorkflowCardsAsync(CancellationToken cancellationToken = default)
        {
            return WorkflowTestHelper.ImportWorkflowCardsAsync(
                this.ResourceAssembly,
                this.CardManager,
                this.CardRepository,
                cancellationToken: cancellationToken);
        }

        #endregion
    }
}
