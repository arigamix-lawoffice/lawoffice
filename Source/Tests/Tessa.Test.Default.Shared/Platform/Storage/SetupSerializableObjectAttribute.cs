using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Устанавливает свойства на объекте <see cref="ISerializableObjectContainer"/>, являющемся TestFixture,
    /// с помощью фабрики указанного типа <see cref="ISerializableObjectFactory"/>.
    /// </summary>
    /// <remarks>
    /// Атрибут должен быть применён только на класс с тестом или метод тестов NUnit при условии,
    /// что соответствующий TestFixture (класс) реализует интерфейсы: <see cref="ITestActionsContainer"/> и <see cref="ISerializableObjectContainer"/>.
    /// В противном случае перед выполнением теста будет сгенерировано исключение <see cref="InvalidOperationException"/>.
    /// </remarks>
    public class SetupSerializableObjectAttribute :
        TestActionAttribute
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием типа фабрики, используемой для заполнения
        /// свойств объекта <see cref="ISerializableObjectContainer"/>.
        /// </summary>
        /// <param name="serializableObjectFactoryType">
        /// Тип фабрики, реализующей интерфейс <see cref="ISerializableObjectFactory"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serializableObjectFactoryType"/> равен <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="serializableObjectFactoryType"/> является типом, не реализующем интерфейс
        /// <see cref="ISerializableObjectFactory"/>.
        /// </exception>
        public SetupSerializableObjectAttribute(Type serializableObjectFactoryType)
        {
            TestHelper.CheckTypeOf<ISerializableObjectFactory>(
                serializableObjectFactoryType,
                nameof(serializableObjectFactoryType));

            this.serializableObjectFactoryType = serializableObjectFactoryType;
        }

        #endregion

        #region Fields

        private readonly Type serializableObjectFactoryType;

        private ISerializableObjectFactory serializableObjectFactory;

        #endregion

        #region Protected Declarations

        /// <summary>
        /// Экземпляр фабрики, используемой для заполнения свойств объекта <see cref="ISerializableObjectContainer"/>.
        /// </summary>
        /// <remarks>
        /// Значение свойства может быть получено через отложенную инициализацию.
        /// </remarks>
        protected ISerializableObjectFactory SerializableObjectFactory =>
            TestHelper.InitValue(
                this.serializableObjectFactoryType,
                ref this.serializableObjectFactory);

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
            var instance = (SetupSerializableObjectAttribute) sender;
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<ISerializableObjectContainer>();

            if (container.SerializableObjectFactory is null
                || container.SerializableObject is null)
            {
                ISerializableObjectFactory factory = instance.SerializableObjectFactory;
                container.SerializableObjectFactory = factory;

                SerializableObject serializableObject = factory.CreateSerializableObject();
                container.SerializableObject = serializableObject;
                container.DynamicObject = serializableObject.ToDynamic();
            }

            return new ValueTask();
        }

        private static ValueTask AfterTearDownAsync(object sender)
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<ISerializableObjectContainer>();

            container.SerializableObjectFactory = null;
            container.SerializableObject = null;

            return new ValueTask();
        }

        #endregion
    }
}
