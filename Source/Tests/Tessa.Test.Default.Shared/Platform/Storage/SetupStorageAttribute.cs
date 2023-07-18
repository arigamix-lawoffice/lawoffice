using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Устанавливает свойства на объекте <see cref="IStorageContainer"/>, являющемся TestFixture,
    /// с помощью фабрики указанного типа <see cref="IStorageFactory"/>.
    /// </summary>
    /// <remarks>
    /// Атрибут должен быть применён только на класс с тестом или метод тестов NUnit при условии,
    /// что соответствующий TestFixture (класс) реализует интерфейсы: <see cref="ITestActionsContainer"/> и <see cref="IStorageContainer"/>.
    /// В противном случае перед выполнением теста будет сгенерировано исключение <see cref="InvalidOperationException"/>.
    /// </remarks>
    public class SetupStorageAttribute :
        TestActionAttribute
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием типа фабрики, используемой для заполнения
        /// свойств объекта <see cref="IStorageContainer"/>.
        /// </summary>
        /// <param name="storageFactoryType">
        /// Тип фабрики, реализующей интерфейс <see cref="IStorageFactory"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="storageFactoryType"/> равен <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="storageFactoryType"/> является типом, не реализующем интерфейс
        /// <see cref="IStorageFactory"/>.
        /// </exception>
        public SetupStorageAttribute(Type storageFactoryType)
        {
            TestHelper.CheckTypeOf<IStorageFactory>(
                storageFactoryType,
                nameof(storageFactoryType));

            this.storageFactoryType = storageFactoryType;
        }

        #endregion

        #region Fields

        private readonly Type storageFactoryType;

        private IStorageFactory storageFactory;

        #endregion

        #region Protected Declarations

        /// <summary>
        /// Экземпляр фабрики, используемой для заполнения свойств объекта <see cref="IStorageContainer"/>.
        /// </summary>
        /// <remarks>
        /// Значение свойства может быть получено через отложенную инициализацию.
        /// </remarks>
        protected IStorageFactory StorageFactory =>
            TestHelper.InitValue(
                this.storageFactoryType,
                ref this.storageFactory);

        #endregion

        #region Base Overrides

        /// <summary>
        /// Создаёт экземпляр класса с параметрами по умолчанию.
        /// </summary>
        /// <remarks>Атрибут всегда применяется на индивидуальный тест.</remarks>
        public override ActionTargets Targets => ActionTargets.Test;

        /// <summary>
        /// Метод, выполняемый перед запуском теста.
        /// </summary>
        /// <param name="test">Информация о тесте.</param>
        public override void BeforeTest(ITest test)
        {
            base.BeforeTest(test);

            var testActions = test.Get<ITestActionsContainer>();
            testActions.GetTestActions(ActionStage.BeforeSetUp).Add(new TestAction(this, BeforeSetUpAsync));
            testActions.GetTestActions(ActionStage.AfterTearDown).Add(new TestAction(this, AfterTearDownAsync));
        }

        #endregion

        #region Private Methods

        private static ValueTask BeforeSetUpAsync(object sender)
        {
            var instance = (SetupStorageAttribute) sender;
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<IStorageContainer>();

            if (container.StorageFactory is null
                || container.Storage is null)
            {
                IStorageFactory factory = instance.StorageFactory;
                container.StorageFactory = factory;

                Dictionary<string, object> storage = factory.Create();
                container.Storage = storage;
            }

            return new ValueTask();
        }

        private static ValueTask AfterTearDownAsync(object sender)
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<IStorageContainer>();

            container.StorageFactory = null;
            container.Storage = null;

            return new ValueTask();
        }

        #endregion
    }
}
