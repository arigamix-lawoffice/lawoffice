using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Platform;
using Tessa.Platform.Operations;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Platform.Operations
{
    /// <summary>
    /// Базовый класс для тестов <see cref="IOperationRepository"/>.
    /// </summary>
    /// <typeparam name="T">Тип для которого этот класс является обёрткой.</typeparam>
    public abstract class OperationRepositoryTestBase<T> :
        TestBaseWrapper<T>
        where T : class, ITestBase
    {
        #region Create Tests

        /// <summary>
        /// Проверяет создание операции в состоянии по умолчанию.
        /// </summary>
        [Test]
        public async Task CreateDefaultState()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.State, Is.EqualTo(OperationState.Created));
            Assert.That(operation.InProgress, Is.Null);
        }

        /// <summary>
        /// Проверяет создание операции в состоянии <see cref="OperationState.InProgress"/>.
        /// </summary>
        [Test]
        public async Task CreateInProgressState()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operation.InProgress, Is.EqualTo(operation.Created));
        }

        /// <summary>
        /// Проверяет создание операции, сообщающей о ходе своего выполнения.
        /// </summary>
        [Test]
        public async Task CreateReportsProgress()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.ReportsProgress);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.ReportsProgress, Is.True);
            Assert.That(operation.Progress, Is.EqualTo(0.0));
        }

        /// <summary>
        /// Проверяет создание операции, не сообщающей о ходе своего выполнения.
        /// </summary>
        [Test]
        public async Task CreateWithoutProgress()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.ReportsProgress, Is.False);
            Assert.That(operation.Progress, Is.Null);
        }

        /// <summary>
        /// Проверяет создание операции вместе с кратким описанием.
        /// </summary>
        [Test]
        public async Task CreateWithDigest()
        {
            const string digest = "operation #42";

            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.None, digest);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Digest, Is.EqualTo(digest));
        }

        /// <summary>
        /// Проверяет создание операции вместе с запросом.
        /// </summary>
        [Test]
        public async Task CreateWithRequest()
        {
            var request = new OperationRequest();
            request.DynamicInfo.MagicNumber = 42;

            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.None, null, request);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Request, Is.Not.Null);
            Assert.That(operation.Request.DynamicInfo.MagicNumber == 42);
        }

        /// <summary>
        /// Проверяет сложное создание операции.
        /// </summary>
        [Test]
        public async Task CreateComplex()
        {
            // русские буквы для теста сериализации строк в REST-контроллерах при вызове с клиента
            const string digest = "operation #42 русские буквы";

            var request = new OperationRequest();
            request.DynamicInfo.MagicNumber = 42;

            Guid id = await this.OperationRepository.CreateAsync(
                OperationTypes.FileConvert,
                OperationCreationFlags.CreateInProgress
                | OperationCreationFlags.ReportsProgress,
                digest,
                request);

            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operation.ReportsProgress, Is.True);
            Assert.That(operation.Progress, Is.EqualTo(0.0));
            Assert.That(operation.Digest, Is.EqualTo(digest));
            Assert.That(operation.Request, Is.Not.Null);
            Assert.That(operation.Request.DynamicInfo.MagicNumber == 42);
        }

        /// <summary>
        /// Проверяет, что дата создания операции отличается от текущей даты менее, чем на минуту.
        /// </summary>
        [Test]
        public async Task Created()
        {
            DateTime created = DateTime.UtcNow;
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(ComparisonHelper.FuzzyEquals(operation.Created, created, new TimeSpan(0, 1, 0)), Is.True);
        }

        /// <summary>
        /// Проверяет создание отложеной операции.
        /// Проверяет, что состояние операции "Отложено".
        /// Проверяет, что дата откладывания отличается меньше чем на секунду (погрешность округления в DATETIME ~3 мс).
        /// </summary>
        [Test]
        public async Task CreatePostponed()
        {
            DateTime postponed = DateTime.UtcNow.AddMinutes(1);
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, postponedTo: postponed);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Postponed, Is.Not.Null);
            Assert.That(operation.State, Is.EqualTo(OperationState.Postponed));
            Assert.That(ComparisonHelper.FuzzyEquals(operation.Postponed, postponed), Is.True);
        }

        /// <summary>
        /// Проверяет, что при создании операции устаналивается идентификатор сессии, полученный из самой сессии.
        /// </summary>
        [Test]
        public async Task SessionID()
        {
            var operationID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);
            var operation = await this.OperationRepository.TryGetAsync(operationID);
            await this.OperationRepository.DeleteAsync(operationID, OperationTypes.CardStore);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.SessionID, Is.EqualTo(this.Session.ID));
        }

        #endregion

        #region Get Tests

        /// <summary>
        /// Проверяет свойства операции, возвращённые в TryGet и не проверенные тестами на создание карточки.
        /// </summary>
        [Test]
        public async Task TryGet()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.ID, Is.EqualTo(id));
            Assert.That(operation.TypeID, Is.EqualTo(OperationTypes.FileConvert));
            Assert.That(operation.Completed, Is.Null);
            Assert.That(operation.Digest, Is.Null);
            Assert.That(operation.Request, Is.Null);
            Assert.That(operation.Response, Is.Null);
        }

        /// <summary>
        /// Проверяет наличие свойств Digest, Request и Response для загруженной операции с loadEverything: true.
        /// </summary>
        [Test]
        public async Task TryGetWithEverything()
        {
            const string digest = "operation #42";

            var request = new OperationRequest();
            request.DynamicInfo.MagicNumber = 42;

            var response = new OperationResponse();
            response.DynamicInfo.MagicString = "42";

            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress, digest, request);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert, response);

            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Digest, Is.EqualTo(digest));
            Assert.That(operation.Request, Is.Not.Null);
            Assert.That(operation.Request.DynamicInfo.MagicNumber == 42);
            Assert.That(operation.Response, Is.Not.Null);
            Assert.That(operation.Response.DynamicInfo.MagicString == "42");
        }

        /// <summary>
        /// Проверяет отсутствие свойств Request и Response, но наличие Digest для загруженной операции с loadEverything: false.
        /// </summary>
        [Test]
        public async Task TryGetWithoutEverything()
        {
            const string digest = "operation #42";

            var request = new OperationRequest();
            request.DynamicInfo.MagicNumber = 42;

            var response = new OperationResponse();
            response.DynamicInfo.MagicString = "42";

            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress, digest, request);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert, response);

            IOperation operation = await this.OperationRepository.TryGetAsync(id, loadEverything: false);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Digest, Is.EqualTo(digest));
            Assert.That(operation.Request, Is.Null);
            Assert.That(operation.Response, Is.Null);
        }

        /// <summary>
        /// Проверяет состояние операции на различных этапах.
        /// </summary>
        [Test]
        public async Task GetState()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            OperationState? createdState = await this.OperationRepository.GetStateAsync(id);
            OperationStateAndProgress? createdStateAndProgress = await this.OperationRepository.GetStateAndProgressAsync(id);

            await this.OperationRepository.StartAsync(id, OperationTypes.FileConvert);
            OperationState? inProgressState = await this.OperationRepository.GetStateAsync(id);
            OperationStateAndProgress? inProgressStateAndProgress = await this.OperationRepository.GetStateAndProgressAsync(id);

            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert);
            OperationState? completedState = await this.OperationRepository.GetStateAsync(id);
            OperationStateAndProgress? completedStateAndProgress = await this.OperationRepository.GetStateAndProgressAsync(id);

            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            OperationState? deletedState = await this.OperationRepository.GetStateAsync(id);
            OperationStateAndProgress? deletedStateAndProgress = await this.OperationRepository.GetStateAndProgressAsync(id);

            Assert.That(createdState, Is.EqualTo(OperationState.Created));
            Assert.That(createdStateAndProgress?.State, Is.EqualTo(OperationState.Created));
            Assert.That(inProgressState, Is.EqualTo(OperationState.InProgress));
            Assert.That(inProgressStateAndProgress?.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(completedState, Is.EqualTo(OperationState.Completed));
            Assert.That(completedStateAndProgress?.State, Is.EqualTo(OperationState.Completed));
            Assert.That(deletedState, Is.Null);
            Assert.That(deletedStateAndProgress, Is.Null);
        }

        /// <summary>
        /// Проверяет наличие операции.
        /// </summary>
        [Test]
        public async Task IsAlive()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            bool isAliveWhenAlive = await this.OperationRepository.IsAliveAsync(id);

            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            bool isAliveWhenDeleted = await this.OperationRepository.IsAliveAsync(id);

            Assert.That(isAliveWhenAlive, Is.True);
            Assert.That(isAliveWhenDeleted, Is.False);
        }

        /// <summary>
        /// Проверяет загрузку всех операций по типу <see cref="OperationTypes.FileConvert"/> со свойствами Digest, Request и Response.
        /// </summary>
        [Test]
        public async Task GetAllByTypeWithEverything()
        {
            const string digest = "operation #42";

            var request = new OperationRequest();
            request.DynamicInfo.MagicNumber = 42;

            var response = new OperationResponse();
            response.DynamicInfo.MagicString = "42";

            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.None, digest, request);
            await this.OperationRepository.CompleteAsync(firstID, OperationTypes.FileConvert, response);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert,
                OperationCreationFlags.CreateInProgress);
            Guid thirdID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore,
                OperationCreationFlags.CreateInProgress);

            List<IOperation> allOperations = await this.OperationRepository.GetAllAsync(OperationTypes.FileConvert, loadEverything: true);
            await this.OperationRepository.DeleteAsync(firstID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(secondID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(thirdID, OperationTypes.CardStore);
            List<IOperation> noOperations = await this.OperationRepository.GetAllAsync(OperationTypes.FileConvert, loadEverything: true);

            Assert.That(allOperations, Is.Not.Null);
            Assert.That(allOperations.Count, Is.GreaterThanOrEqualTo(2));

            IOperation firstOperation = allOperations.FirstOrDefault(x => x.ID == firstID);
            Assert.That(firstOperation, Is.Not.Null);
            Assert.That(firstOperation.Digest, Is.EqualTo(digest));
            Assert.That(firstOperation.Request, Is.Not.Null);
            Assert.That(firstOperation.Request.DynamicInfo.MagicNumber == 42);
            Assert.That(firstOperation.Response, Is.Not.Null);
            Assert.That(firstOperation.Response.DynamicInfo.MagicString == "42");

            IOperation secondOperation = allOperations.FirstOrDefault(x => x.ID == secondID);
            Assert.That(secondOperation, Is.Not.Null);
            Assert.That(secondOperation.Digest, Is.Null);
            Assert.That(secondOperation.Request, Is.Null);
            Assert.That(secondOperation.Response, Is.Null);

            IOperation thirdOperation = allOperations.FirstOrDefault(x => x.ID == thirdID);
            Assert.That(thirdOperation, Is.Null);

            Assert.That(noOperations, Is.Not.Null);
            Assert.That(noOperations.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Проверяет загрузку всех операций по типу <see cref="OperationTypes.FileConvert"/> без свойств Request и Response,
        /// но со свойством Digest.
        /// </summary>
        [Test]
        public async Task GetAllByTypeWithoutEverything()
        {
            const string digest = "operation #42";

            var request = new OperationRequest();
            request.DynamicInfo.MagicNumber = 42;

            var response = new OperationResponse();
            response.DynamicInfo.MagicString = "42";

            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.None, digest, request);
            await this.OperationRepository.CompleteAsync(firstID, OperationTypes.FileConvert, response);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert,
                OperationCreationFlags.CreateInProgress);
            Guid thirdID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore,
                OperationCreationFlags.CreateInProgress);

            List<IOperation> allOperations = await this.OperationRepository.GetAllAsync(OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(firstID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(secondID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(thirdID, OperationTypes.CardStore);
            List<IOperation> noOperations = await this.OperationRepository.GetAllAsync(OperationTypes.FileConvert);

            Assert.That(allOperations, Is.Not.Null);
            Assert.That(allOperations.Count, Is.GreaterThanOrEqualTo(2));

            IOperation firstOperation = allOperations.FirstOrDefault(x => x.ID == firstID);
            Assert.That(firstOperation, Is.Not.Null);
            Assert.That(firstOperation.Digest, Is.EqualTo(digest));
            Assert.That(firstOperation.Request, Is.Null);
            Assert.That(firstOperation.Response, Is.Null);

            IOperation secondOperation = allOperations.FirstOrDefault(x => x.ID == secondID);
            Assert.That(secondOperation, Is.Not.Null);
            Assert.That(secondOperation.Digest, Is.Null);
            Assert.That(secondOperation.Request, Is.Null);
            Assert.That(secondOperation.Response, Is.Null);

            IOperation thirdOperation = allOperations.FirstOrDefault(x => x.ID == thirdID);
            Assert.That(thirdOperation, Is.Null);

            Assert.That(noOperations, Is.Not.Null);
            Assert.That(noOperations.Count, Is.EqualTo(0));
        }

        #endregion

        #region Start Tests

        /// <summary>
        /// Начинает выполнение операции и проверяет, что дата начала выполнения
        /// отличается от текущей даты менее, чем на минуту.
        /// </summary>
        [Test]
        public async Task Start()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);

            DateTime inProgress = DateTime.UtcNow;
            await this.OperationRepository.StartAsync(id, OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, operation.TypeID);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operation.InProgress, Is.Not.Null);
            Assert.That(ComparisonHelper.FuzzyEquals(operation.InProgress.Value, inProgress, new TimeSpan(0, 1, 0)), Is.True);
        }

        /// <summary>
        /// Проверяет, что повторное начало операции не влияет на дату её начала.
        /// </summary>
        [Test]
        public async Task StartWhenCreatedInProgress()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            IOperation beforeStartOperation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.StartAsync(id, OperationTypes.FileConvert);
            IOperation afterStartOperation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(beforeStartOperation, Is.Not.Null);
            Assert.That(afterStartOperation, Is.Not.Null);
            Assert.That(afterStartOperation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(afterStartOperation.InProgress, Is.EqualTo(beforeStartOperation.InProgress));
        }

        /// <summary>
        /// Проверяет, что два начала операции подряд не влияют на дату её первого начала.
        /// </summary>
        [Test]
        public async Task StartTwice()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            await this.OperationRepository.StartAsync(id, OperationTypes.FileConvert);
            IOperation beforeStartOperation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.StartAsync(id, OperationTypes.FileConvert);
            IOperation afterStartOperation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(beforeStartOperation, Is.Not.Null);
            Assert.That(afterStartOperation, Is.Not.Null);
            Assert.That(afterStartOperation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(afterStartOperation.InProgress, Is.EqualTo(beforeStartOperation.InProgress));
        }

        /// <summary>
        /// Проверяет начало операции для типа, когда любые операции отсутствуют.
        /// </summary>
        [Test]
        public async Task StartFirstNothing()
        {
            Guid? started = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);

            Assert.That(started, Is.Null);
        }

        /// <summary>
        /// Проверяет начало операции для типа, когда подходящие операции заданного типа отсутствуют.
        /// </summary>
        [Test]
        public async Task StartFirstWrongType()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid? started = await this.OperationRepository.StartFirstAsync(OperationTypes.CardStore);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(started, Is.Null);
        }

        /// <summary>
        /// Проверяет начало операции для типа, когда подходящие операции в состоянии
        /// <see cref="OperationState.Created"/> отсутствуют.
        /// </summary>
        [Test]
        public async Task StartFirstWrongState()
        {
            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert,
                OperationCreationFlags.CreateInProgress);
            await this.OperationRepository.CompleteAsync(secondID, OperationTypes.FileConvert);

            Guid? started = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(firstID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(secondID, OperationTypes.FileConvert);

            Assert.That(started, Is.Null);
        }

        /// <summary>
        /// Проверяет начало операции для типа, когда имеется единственная подходящая операция этого типа.
        /// Проверяет, что дата начала операции отличается от текущей меньше, чем на минуту.
        /// Проверяет, что повторный вызов метода не влияет на дату начала первой операции.
        /// </summary>
        [Test]
        public async Task StartFirstSingle()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid firstOtherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);
            Guid secondOtherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);

            DateTime inProgress = DateTime.UtcNow;
            Guid? firstStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            Guid? secondStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            IOperation operationAfterSecondStarted = await this.OperationRepository.TryGetAsync(id);

            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(firstOtherID, OperationTypes.CardStore);
            await this.OperationRepository.DeleteAsync(secondOtherID, OperationTypes.CardStore);

            Assert.That(firstStarted, Is.EqualTo(id));
            Assert.That(secondStarted, Is.Null);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.ID, Is.EqualTo(id));
            Assert.That(operation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operation.InProgress, Is.Not.Null);
            Assert.That(ComparisonHelper.FuzzyEquals(operation.InProgress.Value, inProgress, new TimeSpan(0, 1, 0)), Is.True);

            Assert.That(operationAfterSecondStarted, Is.Not.Null);
            Assert.That(operationAfterSecondStarted.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operationAfterSecondStarted.InProgress, Is.EqualTo(operationAfterSecondStarted.InProgress));
        }

        /// <summary>
        /// Проверяет начало двух операций для типа.
        /// </summary>
        [Test]
        public async Task StartFirstTwice()
        {
            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            await Task.Delay(50);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            await Task.Delay(50);
            Guid thirdID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid otherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);

            Guid? firstStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            Guid? secondStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            IOperation firstOperation = await this.OperationRepository.TryGetAsync(firstID);
            IOperation secondOperation = await this.OperationRepository.TryGetAsync(secondID);
            IOperation thirdOperation = await this.OperationRepository.TryGetAsync(thirdID);
            IOperation otherOperation = await this.OperationRepository.TryGetAsync(otherID);

            await this.OperationRepository.DeleteAsync(firstID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(secondID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(thirdID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(otherID, OperationTypes.CardStore);

            Assert.That(firstStarted, Is.Not.Null);
            Assert.That(secondStarted, Is.Not.Null);
            Assert.That(secondStarted, Is.Not.EqualTo(firstStarted));

            var createdOperationIDs = new Guid[]
            {
                firstID,
                secondID,
                thirdID,
            };

            var startedOperationsIDs = new Guid[]
            {
                firstStarted.Value,
                secondStarted.Value
            };

            Assert.That(startedOperationsIDs.All(so => createdOperationIDs.Contains(so)), Is.EqualTo(true));

            Assert.That(firstOperation, Is.Not.Null);
            Assert.That(secondOperation, Is.Not.Null);
            Assert.That(thirdOperation, Is.Not.Null);

            var operations = new IOperation[]
            {
                firstOperation,
                secondOperation,
                thirdOperation
            };

            Assert.That(operations.Count(o => o.State == OperationState.InProgress), Is.EqualTo(2));
            Assert.That(operations.Count(o => o.State == OperationState.Created), Is.EqualTo(1));
            
            Assert.That(otherOperation, Is.Not.Null);
            Assert.That(otherOperation.State, Is.EqualTo(OperationState.Created));
        }

        /// <summary>
        /// Проверяет начало операции для типа, когда имеется единственная подходящая операция этого типа, и время, до которого она отложена, ещё не настало.
        /// Проверяет, что операция не может быть запущена, пока она отложена.
        /// </summary>
        [Test]
        public async Task StartFirstPostponed()
        {
            DateTime? postponed = DateTime.UtcNow.AddMinutes(1);
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, postponedTo: postponed);
            Guid firstOtherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);
            Guid secondOtherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);

            Guid? firstStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);

            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(firstOtherID, OperationTypes.CardStore);
            await this.OperationRepository.DeleteAsync(secondOtherID, OperationTypes.CardStore);

            Assert.That(firstStarted, Is.Null);
        }

        /// <summary>
        /// Проверяет начало операции для типа, когда имеется единственная подходящая операция этого типа и время, до которого она отложена, уже настало.
        /// Проверяет, что дата начала операции отличается от текущей меньше, чем на минуту.
        /// Проверяет, что повторный вызов метода не влияет на дату начала первой операции.
        /// </summary>
        [Test]
        public async Task StartFirstReadyPostponed()
        {
            DateTime? postponed = DateTime.UtcNow.AddMinutes(-1);
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, postponedTo: postponed);
            Guid firstOtherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);
            Guid secondOtherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);

            DateTime inProgress = DateTime.UtcNow;
            Guid? firstStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            Guid? secondStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            IOperation operationAfterSecondStarted = await this.OperationRepository.TryGetAsync(id);

            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(firstOtherID, OperationTypes.CardStore);
            await this.OperationRepository.DeleteAsync(secondOtherID, OperationTypes.CardStore);

            Assert.That(firstStarted, Is.EqualTo(id));
            Assert.That(secondStarted, Is.Null);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.ID, Is.EqualTo(id));
            Assert.That(operation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operation.InProgress, Is.Not.Null);
            Assert.That(ComparisonHelper.FuzzyEquals(operation.InProgress.Value, inProgress, new TimeSpan(0, 1, 0)), Is.True);

            Assert.That(operationAfterSecondStarted, Is.Not.Null);
            Assert.That(operationAfterSecondStarted.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(operationAfterSecondStarted.InProgress, Is.EqualTo(operationAfterSecondStarted.InProgress));
        }

        /// <summary>
        /// Проверяет начало двух операций для типа при наличии одной отложенной, одной отложенной, но уже готовой к запуску, и одной новой операции.
        /// </summary>
        [Test]
        public async Task StartFirstPostponedAndCreated()
        {
            DateTime? postponed1 = DateTime.UtcNow.AddMinutes(1);
            DateTime? postponed2 = DateTime.UtcNow.AddMinutes(-1);
            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, postponedTo: postponed1);
            await Task.Delay(50);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, postponedTo: postponed2);
            await Task.Delay(50);
            Guid thirdID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid otherID = await this.OperationRepository.CreateAsync(OperationTypes.CardStore);

            Guid? firstStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            Guid? secondStarted = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            IOperation firstOperation = await this.OperationRepository.TryGetAsync(firstID);
            IOperation secondOperation = await this.OperationRepository.TryGetAsync(secondID);
            IOperation thirdOperation = await this.OperationRepository.TryGetAsync(thirdID);
            IOperation otherOperation = await this.OperationRepository.TryGetAsync(otherID);

            await this.OperationRepository.DeleteAsync(firstID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(secondID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(thirdID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(otherID, OperationTypes.CardStore);

            Assert.That(firstStarted, Is.EqualTo(secondID));
            Assert.That(secondStarted, Is.EqualTo(thirdID));

            Assert.That(firstOperation, Is.Not.Null);
            Assert.That(firstOperation.State, Is.EqualTo(OperationState.Postponed));
            Assert.That(secondOperation, Is.Not.Null);
            Assert.That(secondOperation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(thirdOperation, Is.Not.Null);
            Assert.That(thirdOperation.State, Is.EqualTo(OperationState.InProgress));
            Assert.That(otherOperation, Is.Not.Null);
            Assert.That(otherOperation.State, Is.EqualTo(OperationState.Created));
        }

        [Test]
        public async Task StartPostponedWithOffset()
        {
            var postponed = DateTime.UtcNow.AddHours(1);
            var id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, postponedTo: postponed);

            var started = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            Assert.That(started, Is.Null);

            this.SetServerUtcNow(postponed.AddMinutes(-30));

            started = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            Assert.That(started, Is.Null);

            this.SetServerUtcNow(postponed.AddMinutes(30));

            started = await this.OperationRepository.StartFirstAsync(OperationTypes.FileConvert);
            Assert.That(started, Is.EqualTo(id));

            await this.OperationRepository.DeleteAsync(started.Value, OperationTypes.FileConvert);
        }

        #endregion

        #region ReportProgress Tests

        /// <summary>
        /// Проверяет корректные сообщения о прогрессе операции.
        /// </summary>
        [Test]
        public async Task ReportProgress()
        {
            Guid id = await this.OperationRepository.CreateAsync(
                OperationTypes.FileConvert,
                OperationCreationFlags.ReportsProgress | OperationCreationFlags.ReportsProgress);

            await this.OperationRepository.StartAsync(id, OperationTypes.FileConvert);

            bool minReportResult = await this.OperationRepository.ReportProgressAsync(id, 0);
            IOperation minOperation = await this.OperationRepository.TryGetAsync(id);

            bool mediumReportResult = await this.OperationRepository.ReportProgressAsync(id, 50);
            IOperation mediumOperation = await this.OperationRepository.TryGetAsync(id);

            bool maxReportResult = await this.OperationRepository.ReportProgressAsync(id, 100);
            IOperation maxOperation = await this.OperationRepository.TryGetAsync(id);

            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(minReportResult, Is.True);
            Assert.That(minOperation.Progress, Is.EqualTo(0.0));
            Assert.That(mediumReportResult, Is.True);
            Assert.That(mediumOperation.Progress, Is.EqualTo(50.0));
            Assert.That(maxReportResult, Is.True);
            Assert.That(maxOperation.Progress, Is.EqualTo(100.0));
        }

        /// <summary>
        /// Проверяет сообщения о прогрессе для удалённой операции.
        /// </summary>
        [Test]
        public async Task ReportProgressToUnknown()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.ReportsProgress);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            bool reportResult = await this.OperationRepository.ReportProgressAsync(id, 10);

            Assert.That(reportResult, Is.False);
        }

        /// <summary>
        /// Проверяет, что сообщение о прогрессе операции не выполняется,
        /// если операция создавалась как не сообщающая о прогрессе.
        /// </summary>
        [Test]
        public async Task ReportProgressWhenForbidden()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            bool reportResult = await this.OperationRepository.ReportProgressAsync(id, 10);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(reportResult, Is.False);
            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Progress, Is.Null);
            Assert.That(operation.ReportsProgress, Is.False);
        }

        /// <summary>
        /// Проверяет некорректные значение в сообщениях о прогрессе.
        /// </summary>
        [Test]
        public async Task ReportInvalidProgress()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.ReportsProgress);

            try
            {
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await this.OperationRepository.ReportProgressAsync(id, -1));
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await this.OperationRepository.ReportProgressAsync(id, 101));
            }
            finally
            {
                await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            }
        }

        #endregion

        #region Complete Tests

        /// <summary>
        /// Завершает выполнение операции со свойством Response и проверяет его содержимое.
        /// </summary>
        [Test]
        public async Task CompleteWithResponse()
        {
            var response = new OperationResponse();
            response.DynamicInfo.MagicNumber = 42;
            response.ValidationResult.AddError(null, "Error text");

            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert, response);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Response, Is.Not.Null);
            Assert.That(operation.Response.DynamicInfo.MagicNumber == 42);

            ValidationResult result = operation.Response.ValidationResult.Build();
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items[0].Message, Is.EqualTo("Error text"));
        }

        /// <summary>
        /// Завершает выполнение операции без свойства Response и проверяет его содержимое.
        /// </summary>
        [Test]
        public async Task CompleteWithoutResponse()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Response, Is.Null);
        }

        /// <summary>
        /// Проверяет, что два завершения операции подряд не влияют на дату её первого завершения.
        /// </summary>
        [Test]
        public async Task CompleteTwice()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert, OperationCreationFlags.CreateInProgress);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert);
            IOperation beforeStartOperation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert);
            IOperation afterStartOperation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(beforeStartOperation, Is.Not.Null);
            Assert.That(afterStartOperation, Is.Not.Null);
            Assert.That(afterStartOperation.State, Is.EqualTo(OperationState.Completed));
            Assert.That(afterStartOperation.Completed, Is.EqualTo(beforeStartOperation.Completed));
        }

        /// <summary>
        /// Проверяет, что корректно выполняется завершение операции из состояния <see cref="OperationState.Created"/>.
        /// </summary>
        [Test]
        public async Task CompleteFromCreated()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            await this.OperationRepository.CompleteAsync(id, OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);

            Assert.That(operation, Is.Not.Null);
            Assert.That(operation.Response, Is.Null);
        }

        #endregion

        #region Delete Tests

        /// <summary>
        /// Проверяет, что операция была удалена.
        /// </summary>
        [Test]
        public async Task Delete()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(id, OperationTypes.FileConvert);
            IOperation operation = await this.OperationRepository.TryGetAsync(id);

            Assert.That(operation, Is.Null);
        }

        /// <summary>
        /// Удаляет записи старше текущей даты, если записей нет.
        /// </summary>
        [Test]
        public async Task DeleteOlderThanNothing()
        {
            int deletedCount = await this.OperationRepository.DeleteOlderThanAsync(DateTime.UtcNow);

            Assert.That(deletedCount, Is.EqualTo(0));
        }

        /// <summary>
        /// Удаляет записи старше текущей даты, если записи есть, но все они не подходят под условие.
        /// </summary>
        [Test]
        public async Task DeleteOlderThanZero()
        {
            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);

            int deletedCount = await this.OperationRepository.DeleteOlderThanAsync(DateTime.UtcNow.AddDays(-1));
            IOperation firstOperation = await this.OperationRepository.TryGetAsync(firstID);
            IOperation secondOperation = await this.OperationRepository.TryGetAsync(secondID);
            await this.OperationRepository.DeleteAsync(firstID, OperationTypes.FileConvert);
            await this.OperationRepository.DeleteAsync(secondID, OperationTypes.FileConvert);

            Assert.That(deletedCount, Is.EqualTo(0));
            Assert.That(firstOperation, Is.Not.Null);
            Assert.That(firstOperation.ID, Is.EqualTo(firstID));
            Assert.That(secondOperation, Is.Not.Null);
            Assert.That(secondOperation.ID, Is.EqualTo(secondID));
        }

        /// <summary>
        /// Удаляет записи старше текущей даты, если запись ровно одна.
        /// </summary>
        [Test]
        public async Task DeleteOlderThanSingle()
        {
            Guid id = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);

            int deletedCount = await this.OperationRepository.DeleteOlderThanAsync(DateTime.UtcNow.AddMinutes(1));
            IOperation operation = await this.OperationRepository.TryGetAsync(id);

            Assert.That(deletedCount, Is.EqualTo(1));
            Assert.That(operation, Is.Null);
        }

        /// <summary>
        /// Удаляет записи старше текущей даты, если запись ровно одна.
        /// </summary>
        [Test]
        public async Task DeleteOlderThanTwice()
        {
            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);

            int deletedCount = await this.OperationRepository.DeleteOlderThanAsync(DateTime.UtcNow.AddMinutes(1));
            IOperation firstOperation = await this.OperationRepository.TryGetAsync(firstID);
            IOperation secondOperation = await this.OperationRepository.TryGetAsync(secondID);

            Assert.That(deletedCount, Is.EqualTo(2));
            Assert.That(firstOperation, Is.Null);
            Assert.That(secondOperation, Is.Null);
        }

        /// <summary>
        /// Удаляет все операции, кроме одной, которая не подходит по дате создания.
        /// </summary>
        [Test]
        public async Task DeleteOlderThanButOne()
        {
            Guid firstID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            Guid secondID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);
            await Task.Delay(50);
            DateTime removalDate = DateTime.UtcNow;
            await Task.Delay(50);
            Guid thirdID = await this.OperationRepository.CreateAsync(OperationTypes.FileConvert);

            int deletedCount = await this.OperationRepository.DeleteOlderThanAsync(removalDate);
            IOperation firstOperation = await this.OperationRepository.TryGetAsync(firstID);
            IOperation secondOperation = await this.OperationRepository.TryGetAsync(secondID);
            IOperation thirdOperation = await this.OperationRepository.TryGetAsync(thirdID);
            await this.OperationRepository.DeleteAsync(thirdID, OperationTypes.FileConvert);

            Assert.That(deletedCount, Is.EqualTo(2));
            Assert.That(firstOperation, Is.Null);
            Assert.That(secondOperation, Is.Null);
            Assert.That(thirdOperation, Is.Not.Null);
            Assert.That(thirdOperation.ID, Is.EqualTo(thirdID));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создаёт репозиторий <see cref="IOperationRepository"/>, который используется для тестирования.
        /// </summary>
        /// <returns>Репозиторий, используемый для тестирования.</returns>
        protected abstract IOperationRepository CreateOperationRepository();

        #endregion

        #region Properties

        private IOperationRepository operationRepository;

        /// <summary>
        /// Репозиторий, используемый для тестирования.
        /// </summary>
        public IOperationRepository OperationRepository =>
            this.operationRepository ??= this.CreateOperationRepository();

        #endregion

        #region Constructors

        protected OperationRepositoryTestBase(T testBase)
            : base(testBase)
        {
        }

        #endregion
    }
}
