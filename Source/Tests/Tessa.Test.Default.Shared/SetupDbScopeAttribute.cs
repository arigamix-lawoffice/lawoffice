using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Extensions.PostgreSql.Server;
using Tessa.Platform;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Устанавливает свойства на объекте <see cref="IDbScopeContainer"/>, являющемся TestFixture,
    /// с помощью фабрики указанного типа <see cref="IDbFactory"/>.
    /// </summary>
    /// <remarks>
    /// <para>Атрибут должен быть применён только на класс с тестом при условии,
    /// что соответствующий TestFixture (класс) реализует интерфейсы: <see cref="IDbScopeContainer"/> и <see cref="ITestActionsContainer"/>.
    /// В противном случае перед выполнением теста будет сгенерировано исключение <see cref="InvalidOperationException"/>.</para>
    /// <para>По умолчанию строка подключения к базе данных находится в конфигурационном файле по имени,
    /// указанном в константе <see cref="TestHelper.DefaultConfigurationString"/>.</para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SetupDbScopeAttribute :
        TestActionAttribute,
        IApplyToTest
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием типа фабрики, используемой для заполнения
        /// свойств объекта <see cref="IDbScopeContainer"/>.
        /// </summary>
        /// <param name="dbFactoryType">
        /// Тип фабрики, реализующей интерфейс <see cref="IDbFactory"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dbFactoryType"/> равен <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dbFactoryType"/> является типом, не реализующем интерфейс
        /// <see cref="IDbFactory"/>.
        /// </exception>
        public SetupDbScopeAttribute(Type dbFactoryType)
             => this.DbFactoryType = dbFactoryType ?? throw new ArgumentNullException(nameof(dbFactoryType));

        /// <summary>
        /// Создаёт экземпляр атрибута с использованием фабрики <see cref="DefaultDbFactory"/>
        /// для заполнения свойств объекта <see cref="IDbScopeContainer"/>.
        /// </summary>
        public SetupDbScopeAttribute()
            : this(typeof(DefaultDbFactory))
        {
        }

        #endregion

        #region DbFactoryDecorator Private Class

        private sealed class DbFactoryDecorator :
            IDbFactory
        {
            #region Constructors

            public DbFactoryDecorator(
                IDbFactory factory,
                string databaseName)
            {
                this.factory = factory;
                this.databaseName = databaseName;
            }

            #endregion

            #region Fields

            private readonly string databaseName;

            private readonly IDbFactory factory;

            #endregion

            #region IDbManagerFactory Members

            public DbManager Create()
            {
                DbManager db = null;
                try
                {
                    db = this.factory.Create();
                    if (db.DataConnection.Connection.Database != this.databaseName)
                    {
                        db.DataConnection.Connection.ChangeDatabase(this.databaseName);
                    }
                }
                catch (Exception)
                {
                    if (db is not null)
                    {
                        var valueTask = db.DisposeAsync();
                        if (!valueTask.IsCompletedSuccessfully)
                        {
                            valueTask.AsTask().GetAwaiter().GetResult();
                        }
                    }

                    throw;
                }

                return db;
            }

            #endregion
        }

        #endregion

        #region Fields

        private IDbFactory dbManagerFactory;

        private static readonly SynchronizedOneTimeRegistrator postgreSqlMapperRegistrator =
            new SynchronizedOneTimeRegistrator(PostgreSqlMapper.Register);

        #endregion

        #region Properties

        private Type dbFactoryType;

        /// <summary>
        /// Тип фабрики для создания объектов <see cref="DbManager"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Задаваемое значение равно <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// Задаваемое значение является типом, не реализующим интерфейс <see cref="IDbFactory"/>.
        /// </exception>
        public Type DbFactoryType
        {
            get => this.dbFactoryType;
            set
            {
                if (this.dbFactoryType != value)
                {
                    TestHelper.CheckTypeOf<IDbFactory>(value, nameof(this.DbFactoryType));
                    this.dbFactoryType = value;
                }
            }
        }

        /// <summary>
        /// Имя строки подключения из конфигурационного файла,
        /// используемой для создания объектов <see cref="DbManager"/>.
        /// Значение по умолчанию: <see cref="TestHelper.DefaultConfigurationString"/>.
        /// </summary>
        public string ConfigurationString { get; set; } = TestHelper.DefaultConfigurationString;

        /// <summary>
        /// Тип СУБД, к которой происходит подключение.
        /// </summary>
        public Dbms Dbms { get; set; } = TestHelper.DefaultDbms;

        #endregion

        #region Protected Declarations

        /// <summary>
        /// Экземпляр фабрики, используемой для заполнения свойств объекта <see cref="IDbScopeContainer"/>.
        /// </summary>
        /// <remarks>
        /// Значение свойства может быть получено через отложенную инициализацию.
        /// </remarks>
        protected IDbFactory DbManagerFactory => TestHelper.InitValue(
            this.DbFactoryType,
            ref this.dbManagerFactory);

        #endregion

        #region Properties

        /// <summary>
        /// Признак того, что объект <see cref="DbScope"/> создаётся таким образом,
        /// что вызовы <see cref="DbScope.CreateNew"/> не создают новых соединений с базой данных и продолжают работать в пределах Scope.
        /// По умолчанию значение <see langword="false"/>.
        /// </summary>
        public bool SingleConnectionDbScope { get; set; } = false;

        #endregion

        #region Base Overrides

        /// <summary>
        /// Создаёт экземпляр класса с параметрами по умолчанию.
        /// </summary>
        /// <remarks>Атрибут всегда применяется на TestFixture.</remarks>
        public override ActionTargets Targets => ActionTargets.Suite;

        /// <summary>
        /// Метод, выполняемый перед запуском теста.
        /// </summary>
        /// <param name="test">Информация о тесте.</param>
        public override void BeforeTest(ITest test)
        {
            base.BeforeTest(test);

            var testActions = test.Get<ITestActionsContainer>();
            testActions.GetTestActions(ActionStage.BeforeInitialize).Add(
                new TestAction(
                    this,
                    static sender => ((SetupDbScopeAttribute) sender).BeforeInitializeAsync()));

            testActions.GetTestActions(ActionStage.AfterOneTimeTearDownScope).Add(
                new TestAction(this, AfterOneTimeTearDownScopeAsync));
        }

        #endregion

        #region Decorate Protected Static Method

        /// <summary>
        /// Создаёт экземпляр фабрики, декорирующей объекты, которые создаются заданной фабрикой <paramref name="factory"/>,
        /// таким образом, чтобы изменять их базу данных сразу после создания.
        /// </summary>
        /// <param name="factory">Декорируемая фабрика объектов.</param>
        /// <param name="databaseName">Имя базы данных.</param>
        /// <returns>Фабрика объектов, являющаяся декоратором для заданной фабрики.</returns>
        public static IDbFactory Decorate(IDbFactory factory, string databaseName) =>
            factory != null && factory.GetType() != typeof(DbFactoryDecorator)
                ? new DbFactoryDecorator(factory, databaseName)
                : factory;

        #endregion

        #region IApplyToTest Members

        /// <inheritdoc/>
        public void ApplyToTest(NUnit.Framework.Internal.Test test) =>
            TestHelper.SetTestCategory(test.Properties, this.Dbms);

        #endregion

        #region Private Methods

        private ValueTask BeforeInitializeAsync()
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;

            if (this.Dbms == Dbms.PostgreSql)
            {
                postgreSqlMapperRegistrator.Register();
            }

            var container = test.Get<IDbScopeContainer>();
            if (container.DbFactory is null || container.DbScope is null)
            {
                var factory = this.DbManagerFactory;
                if (factory is IConfigurationStringContainer factoryContainer)
                {
                    factoryContainer.ConfigurationString = this.ConfigurationString;
                }

                container.DbFactory = factory;
                container.DbScope = this.SingleConnectionDbScope
                    ? new SingleConnectionDbScope(factory.Create)
                    : new DbScope(factory.Create);
            }

            return ValueTask.CompletedTask;
        }

        private static ValueTask AfterOneTimeTearDownScopeAsync(object sender)
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;
            var container = test.Get<IDbScopeContainer>();

            container.DbFactory = null;
            container.DbScope = null;

            return ValueTask.CompletedTask;
        }

        #endregion
    }
}
