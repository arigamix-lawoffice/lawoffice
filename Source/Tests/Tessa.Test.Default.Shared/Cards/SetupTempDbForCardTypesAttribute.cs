using System;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Cards;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// <para>Создаёт временную базу данных, выполняет на ней заданный скрипт и устанавливает
    /// свойства на объекте <see cref="ICardTypeServerRepository"/>, являющемся TestFixture,
    /// с помощью фабрик указанных типов <see cref="IDbFactory"/> и <see cref="ICardTypeServerRepository"/>
    /// таким образом, чтобы объекты <see cref="ICardTypeRepositoryContainer"/> и <see cref="IDbScope"/>
    /// ссылались на временную базу данных.</para>
    /// <para>Затем выполняются все тесты в TestFixture, которые могут считывать и изменять
    /// временную базу данных. После завершения тестов временная база данных удаляется.</para>
    /// </summary>
    /// <remarks>
    /// <para>Атрибут должен быть применён только на класс с тестом при условии,
    /// что соответствующий TestFixture (класс) реализует интерфейсы: <see cref="IDbScopeContainer"/>, <see cref="ITestActionsContainer"/>, <see cref="IFixtureNameProvider"/> и <see cref="ICardTypeRepositoryContainer"/>.
    /// В противном случае перед выполнением теста будет сгенерировано исключение <see cref="InvalidOperationException"/>.</para>
    /// <para>Фабрика, доступная через свойство <see cref="IDbScopeContainer.DbFactory"/> на объекте
    /// <see cref="DbManager"/>, создаёт объекты <see cref="ICardTypeRepositoryFactory"/>, указывающие
    /// на временную базу данных.</para>
    /// </remarks>
    public class SetupTempDbForCardTypesAttribute :
        SetupTempDbAttribute
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием имени скрипта, выполняемого на временной базе данных.
        /// </summary>
        /// <param name="testScriptFileNames">
        /// Имена SQL-скриптов, добавленных в ресурсы сборки (Build Action = Embedded Resource) и выполняемых в заданном порядке.
        /// Скрипты должны помещаться в папку <c>Sql</c> проекта содержащего класс к которому применён данный атрибут.
        /// Пример: <c>"Default.sql"</c>, <c>"SubFolder/Cards.sql"</c>.
        /// </param>
        public SetupTempDbForCardTypesAttribute(params string[] testScriptFileNames)
            : base(testScriptFileNames)
        {
            this.cardTypeRepositoryFactoryType = typeof(DefaultCardTypeRepositoryFactory);
        }

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием типа базы данных, строки подключения и имени скрипта, выполняемого на временной базе данных.
        /// </summary>
        /// <param name="dbms">Тип базы данных.</param>
        /// <param name="configurationString">Строка подключения.</param>
        /// <param name="testScriptFileNames">
        /// Имена SQL-скриптов, добавленных в ресурсы сборки (Build Action = Embedded Resource) и выполняемых в заданном порядке.
        /// Скрипты должны помещаться в папку <c>Sql</c> проекта содержащего класс к которому применён данный атрибут.
        /// Пример: <c>"Default.sql"</c>, <c>"SubFolder/Cards.sql"</c>.
        /// </param>
        public SetupTempDbForCardTypesAttribute(
            Dbms dbms,
            string configurationString,
            params string[] testScriptFileNames) : base(dbms, configurationString, testScriptFileNames)
        {
            this.cardTypeRepositoryFactoryType = typeof(DefaultCardTypeRepositoryFactory);
        }

        #endregion

        #region Properties

        private Type cardTypeRepositoryFactoryType;

        /// <summary>
        /// Тип фабрики для создания объектов <see cref="ICardTypeServerRepository"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Задаваемое значение равно <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// Задаваемое значение является типом, не реализующим интерфейс <see cref="ICardTypeRepositoryFactory"/>.
        /// </exception>
        public Type CardTypeRepositoryFactoryType
        {
            get => this.cardTypeRepositoryFactoryType;
            set
            {
                if (this.cardTypeRepositoryFactoryType != value)
                {
                    TestHelper.CheckTypeOf<ICardTypeRepositoryFactory>(value, nameof(this.CardTypeRepositoryFactoryType));
                    this.cardTypeRepositoryFactoryType = value;
                }
            }
        }

        #endregion

        #region Protected Declarations

        private ICardTypeRepositoryFactory cardTypeRepositoryFactory;

        /// <summary>
        /// Экземпляр фабрики, используемой для заполнения свойств объекта <see cref="ICardTypeRepositoryFactory"/>.
        /// </summary>
        /// <remarks>
        /// Значение свойства может быть получено через отложенную инициализацию.
        /// </remarks>
        protected ICardTypeRepositoryFactory CardTypeRepositoryFactory
        {
            get
            {
                return TestHelper.InitValue(
                    this.cardTypeRepositoryFactoryType,
                    ref this.cardTypeRepositoryFactory);
            }
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Метод, выполняемый перед запуском теста.
        /// </summary>
        /// <param name="test">Информация о тесте.</param>
        public override void BeforeTest(ITest test)
        {
            base.BeforeTest(test);

            var testActions = test.Get<ITestActionsContainer>();
            testActions.GetTestActions(ActionStage.BeforeInitialize).Add(new TestAction(this, BeforeInitializeAsync));
            testActions.GetTestActions(ActionStage.AfterOneTimeTearDown).Add(new TestAction(this, AfterOneTimeTearDownAsync));
        }

        #endregion

        #region Private Methods

        private static ValueTask BeforeInitializeAsync(object sender)
        {
            var instance = (SetupTempDbForCardTypesAttribute) sender;
            var test = TestHelper.TestExecutionContext.CurrentTest;

            var container = test.Get<ICardTypeRepositoryContainer>();
            if (container.CardTypeRepository is null)
            {
                var repository = instance.CardTypeRepositoryFactory.Create(container.DbScope);
                container.CardTypeRepository = repository;
            }

            return new ValueTask();
        }

        private static ValueTask AfterOneTimeTearDownAsync(object sender)
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<ICardTypeRepositoryContainer>();
            container.CardTypeRepository = null;

            return new ValueTask();
        }

        #endregion
    }
}
