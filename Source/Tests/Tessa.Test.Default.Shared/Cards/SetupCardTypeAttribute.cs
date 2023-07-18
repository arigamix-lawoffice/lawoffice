using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Устанавливает свойства на объекте <see cref="ICardTypeFactory"/>, являющемся TestFixture,
    /// с помощью фабрики указанного типа <see cref="ICardTypeContainer"/>.
    /// </summary>
    /// <remarks>
    /// Атрибут должен быть применён только на класс с тестом или метод тестов NUnit при условии,
    /// что соответствующий TestFixture (класс) реализует интерфейсы: <see cref="ITestActionsContainer"/> и <see cref="ICardTypeContainer"/>.
    /// В противном случае перед выполнением теста будет сгенерировано исключение <see cref="InvalidOperationException"/>.
    /// </remarks>
    public class SetupCardTypeAttribute :
        TestActionAttribute
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием типа фабрики, используемой для заполнения
        /// свойств объекта <see cref="ICardTypeContainer"/>.
        /// </summary>
        /// <param name="cardTypeFactoryType">
        /// Тип фабрики, реализующей интерфейс <see cref="ICardTypeFactory"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cardTypeFactoryType"/> равен <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="cardTypeFactoryType"/> является типом, не реализующем интерфейс
        /// <see cref="ICardTypeFactory"/>.
        /// </exception>
        public SetupCardTypeAttribute(Type cardTypeFactoryType)
        {
            TestHelper.CheckTypeOf<ICardTypeFactory>(
                cardTypeFactoryType,
                nameof(cardTypeFactoryType));

            this.cardTypeFactoryType = cardTypeFactoryType;
        }

        #endregion

        #region Fields

        private readonly Type cardTypeFactoryType;

        private ICardTypeFactory cardTypeFactory;

        #endregion

        #region Protected Declarations

        /// <summary>
        /// Экземпляр фабрики, используемой для заполнения свойств объекта <see cref="ICardTypeContainer"/>.
        /// </summary>
        /// <remarks>
        /// Значение свойства может быть получено через отложенную инициализацию.
        /// </remarks>
        protected ICardTypeFactory CardTypeFactory
        {
            get
            {
                return TestHelper.InitValue(
                    this.cardTypeFactoryType,
                    ref this.cardTypeFactory);
            }
        }

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

        private static async ValueTask BeforeSetUpAsync(object sender)
        {
            var instance = (SetupCardTypeAttribute) sender;
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<ICardTypeContainer>();

            if (container.CardTypeFactory is null
                || container.CardType is null)
            {
                var factory = instance.CardTypeFactory;
                container.CardTypeFactory = factory;

                var cardType = await factory.CreateAsync();
                container.CardType = cardType;
            }
        }

        private static ValueTask AfterTearDownAsync(object sender)
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<ICardTypeContainer>();

            container.CardTypeFactory = null;
            container.CardType = null;

            return new ValueTask();
        }

        #endregion
    }
}
