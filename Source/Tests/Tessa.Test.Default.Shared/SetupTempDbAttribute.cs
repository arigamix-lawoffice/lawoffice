using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Test.Default.Shared.GC;
using Tessa.Test.Default.Shared.GC.Handlers;
using Tessa.Test.Default.Shared.Kr;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// <para>Создаёт временную базу данных, выполняет на ней заданный скрипт и устанавливает
    /// свойства на объекте <see cref="IDbScopeContainer"/>, являющемся TestFixture,
    /// с помощью фабрики указанного типа <see cref="IDbFactory"/> таким образом,
    /// чтобы объект <see cref="DbManager"/> ссылался на временную базу данных.</para>
    /// <para>Затем выполняются все тесты в TestFixture, которые могут считывать и изменять
    /// временную базу данных. После завершения тестов временная база данных удаляется.</para>
    /// </summary>
    /// <remarks>
    /// <para>Атрибут должен быть применён только на класс с тестом при условии,
    /// что соответствующий TestFixture (класс) реализует интерфейсы: <see cref="IDbScopeContainer"/>, <see cref="ITestActionsContainer"/> и <see cref="IFixtureNameProvider"/>.
    /// В противном случае перед выполнением теста будет сгенерировано исключение <see cref="InvalidOperationException"/>.</para>
    /// <para>Фабрика, доступная через свойство <see cref="IDbScopeContainer.DbFactory"/> на объекте
    /// <see cref="IDbScopeContainer"/>, создаёт объекты <see cref="DbManager"/>, указывающие
    /// на временную базу данных.</para>
    /// <para>По умолчанию строка подключения к временной базе данных находится в конфигурационном файле по имени,
    /// указанном в константе <see cref="TestHelper.TempConfigurationStringMs"/>.</para>
    /// </remarks>
    public class SetupTempDbAttribute :
        SetupDbScopeAttribute
    {
        #region Constants

        private const string ConnectionStringKey = StorageHelper.SystemKeyPrefix + nameof(SetupTempDbAttribute) + "." + "ConnectionString";

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием имени скрипта, выполняемого на временной базе данных.
        /// </summary>
        /// <param name="testScriptFileNames">
        /// Имена SQL-скриптов, добавленных в ресурсы сборки (Build Action = Embedded Resource) и выполняемых в заданном порядке.
        /// Скрипты должны помещаться в папке <c>Sql</c> проекта содержащего класс к которому применён данный атрибут.
        /// Пример: <c>"Default.sql"</c>, <c>"SubFolder/Cards.sql"</c>.
        /// </param>
        public SetupTempDbAttribute(params string[] testScriptFileNames)
        {
            Check.ArgumentNotNull(testScriptFileNames, nameof(testScriptFileNames));

            foreach (var fileName in testScriptFileNames)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentException("Invalid SQL script file name.", nameof(testScriptFileNames));
                }
            }

            this.testScriptFileNames = testScriptFileNames;
            this.ConfigurationString = TestHelper.TempConfigurationStringMs;
        }

        /// <summary>
        /// Создаёт экземпляр атрибута с указанием имени скрипта, выполняемого на временной базе данных.
        /// </summary>
        /// <param name="dbms">Тип СУБД.</param>
        /// <param name="configurationString">Имя используемой строки подключения из конфигурационного файла.</param>
        /// <param name="testScriptFileNames">
        /// Имена SQL-скриптов, добавленных в ресурсы сборки (Build Action = Embedded Resource) и выполняемых в заданном порядке.
        /// Скрипты должны помещаться в папке <c>Sql</c> проекта содержащего класс к которому применён данный атрибут.
        /// Пример: <c>"Default.sql"</c>, <c>"SubFolder/Cards.sql"</c>.
        /// </param>
        public SetupTempDbAttribute(
            Dbms dbms,
            string configurationString,
            params string[] testScriptFileNames)
            : this(testScriptFileNames)
        {
            this.Dbms = dbms;
            this.ConfigurationString = configurationString;
        }

        #endregion

        #region Fields

        private readonly string[] testScriptFileNames;

        private ConfigurationConnection connection;

        #endregion

        #region Properties

        /// <summary>
        /// Признак того, что временную базу данных следует удалить после завершения всех тестов.
        /// По умолчанию равно <c>true</c>.
        /// </summary>
        public bool RemoveDatabase { get; set; } = true;

        /// <summary>
        /// Признак того, что имя базы данных надо рандомизировать в зависимости от имени класса тестов.
        /// Это позволяет параллельно выполнять тесты на разных базах данных.
        /// </summary>
        public bool RandomizeDbName { get; set; } = true;

        /// <summary>
        /// Таймаут в секундах, используемый при выполнении SQL-скриптов, или значение <see langword="null"/>, если используется таймаут по умолчанию. Укажите <c>0</c>, чтобы таймаут был неограниченным.
        /// </summary>
        public int? TimeoutSeconds { get; set; }

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
            testActions
                .GetTestActions(ActionStage.AfterInitializeContainer)
                .Add(new TestAction(this, this.AfterInitializeContainerAsync));

            testActions
                .GetTestActions(ActionStage.AfterOneTimeTearDownScope)
                .Add(new TestAction(this, AfterOneTimeTearDownScopeAsync));
        }

        #endregion

        #region Private Methods

        private async ValueTask AfterInitializeContainerAsync(object sender)
        {
            var test = TestHelper.TestExecutionContext.CurrentTest;

            // здесь уже есть активное соединение с базой
            var dbScopeContainer = test.Get<IDbScopeContainer>();

            var scopeContext = KrTestContext.CurrentContext.ScopeContext;

            if (scopeContext is null)
            {
                return;
            }

            var scopeConnectionString = scopeContext.Info.TryGet<string>(ConnectionStringKey);
            if (!string.IsNullOrEmpty(scopeConnectionString))
            {
                this.InitializeDbScopeContainerWithConnectionString(dbScopeContainer, scopeConnectionString);
                return;
            }

            using (await scopeContext.AsyncLock.EnterAsync())
            {
                scopeConnectionString = scopeContext.Info.TryGet<string>(ConnectionStringKey);
                if (!string.IsNullOrEmpty(scopeConnectionString))
                {
                    this.InitializeDbScopeContainerWithConnectionString(dbScopeContainer, scopeConnectionString);
                    return;
                }

                // если при загрузке файла произойдёт ошибка (неверное имя файла), то соединение с базой открыто не будет
                var sqlTextScripts = TestHelper.GetSqlTextScripts(
                    test.TypeInfo.Assembly,
                    this.testScriptFileNames);

                this.connection = ConfigurationManager.Connections[this.ConfigurationString];

                if (this.RandomizeDbName)
                {
                    var currentTest = test.Get<IFixtureNameProvider>();
                    var tempDbNamePostfix = $"_{(await currentTest.GetFixtureDateTimeAsync()).FormatDateTimeCode()}_{await currentTest.GetFixtureNameAsync()}";

                    this.connection = TestHelper.RewriteConnection(
                        this.connection,
                        (databaseName, _) => databaseName + tempDbNamePostfix,
                        out _);
                }

                await TestHelper.CreateDatabaseAsync(this.connection);

                if (this.RemoveDatabase && test.Fixture is IUnityContainerHolder unityContainerHolder)
                {
                    var externalObjectManager = unityContainerHolder.UnityContainer
                        ?.TryResolve<IExternalObjectManager>();

                    if (externalObjectManager is not null)
                    {
                        var obj = DbExternalObjectHandler.CreateObjectInfo(
                            this.connection.ConnectionString,
                            this.connection.DataProvider,
                            test.Fixture.GetHashCode());

                        externalObjectManager.RegisterForFinalize(obj);
                    }
                }

                this.InitializeDbScopeContainer(dbScopeContainer);

                if (scopeContext is not null)
                {
                    scopeContext.Info[ConnectionStringKey] = this.connection.ConnectionString;
                }

                await TestHelper.ExecuteSqlScriptsAsync(
                    dbScopeContainer.DbScope,
                    sqlTextScripts,
                    this.TimeoutSeconds);
            }
        }

        private void InitializeDbScopeContainerWithConnectionString(IDbScopeContainer dbScopeContainer, string scopeConnectionString)
        {
            var connection = ConfigurationManager.Connections[this.ConfigurationString];
            this.connection = new ConfigurationConnection(scopeConnectionString, connection.DataProvider);
            this.InitializeDbScopeContainer(dbScopeContainer);
        }

        private void InitializeDbScopeContainer(
            IDbScopeContainer dbScopeContainer)
        {
            var factory = this.DbManagerFactory;
            if (factory is IConfigurationStringContainer factoryContainer)
            {
                factoryContainer.ConnectionString = this.connection.ConnectionString;
            }

            dbScopeContainer.DbFactory = factory;
            dbScopeContainer.DbScope = this.SingleConnectionDbScope
                ? new SingleConnectionDbScope(factory.Create)
                : new DbScope(factory.Create);
        }

        private static async ValueTask AfterOneTimeTearDownScopeAsync(object sender)
        {
            var instance = (SetupTempDbAttribute) sender;

            if (instance.RemoveDatabase)
            {
                var test = TestHelper.TestExecutionContext.CurrentTest;
                var container = test.Get<IDbScopeContainer>();

                await using (container.DbScope.Create())
                {
                    if (instance.connection is not null)
                    {
                        await TestHelper.DropDatabaseAsync(instance.connection);
                    }
                }
            }
        }

        #endregion
    }
}
