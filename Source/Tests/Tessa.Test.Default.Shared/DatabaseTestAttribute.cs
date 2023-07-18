using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal.Commands;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Устанавливает информацию о подключении к базе данные, которое должно использоваться в тесте.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class DatabaseTestAttribute :
        TestAttribute,
        ITestBuilder,
        IApplyToTest,
        IWrapSetUpTearDown
    {
        #region Nested Types

        private sealed class ConnectionFactory
        {
            #region Fields

            internal IPropertyBag Properties;

            #endregion

            #region Constructors

            public ConnectionFactory()
            {
                var valueTask = new UnityContainer().RegisterDatabaseForConsoleAsync();
                if (!valueTask.IsCompletedSuccessfully)
                {
                    valueTask.AsTask().GetAwaiter().GetResult();
                }
            }

            #endregion

            #region Methods

            public DbConnection CreateConnection()
            {
                var connectionString =
                    (string) this.Properties.Get(DatabasePropertyNames.ConnectionString) ??
                    throw new InvalidOperationException("Connection string is not specified");
                var providerName =
                    (string) this.Properties.Get(DatabasePropertyNames.ProviderName);

                var factory = ConfigurationManager
                    .GetConfigurationDataProviderFromType(providerName)
                    .GetDbProviderFactory();
                var connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;

                return connection;
            }

            public IDbScope CreateDbScope()
            {
                return new DbScope(() =>
                {
                    var connectionString =
                        (string) this.Properties.Get(DatabasePropertyNames.ConnectionString) ??
                        throw new InvalidOperationException("Connection string is not specified");
                    var providerName =
                        (string) this.Properties.Get(DatabasePropertyNames.ProviderName);

                    var dataProvider = ConfigurationManager
                        .GetConfigurationDataProviderFromType(providerName)
                        .GetDataProvider(connectionString);

                    return new DbManager(dataProvider, connectionString);
                });
            }

            #endregion
        }

        #endregion

        #region Fields

        private static readonly NUnitTestCaseBuilder sTestBuilder = new NUnitTestCaseBuilder();

        private object expectedResult;

        #endregion

        #region ITestBuilder Overrides

        /// <inheritdoc/>
        IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, NUnit.Framework.Internal.Test testSuite)
        {
            Check.ArgumentNotNull(method, nameof(method));

            var connectionStringName = this.ConnectionString;
            var usingPrefix = false;

            if (connectionStringName == null)
            {
                connectionStringName = string.Empty;
                usingPrefix = true;
            }
            else
            {
                var length = this.ConnectionString.Length - 1;
                if (length > 0 && this.ConnectionString[length] == '*')
                {
                    connectionStringName = this.ConnectionString[..length];
                    usingPrefix = true;
                }
            }

            var hasNoMatches = true;
            foreach (var connection in ConfigurationManager.Connections)
            {
                if (ConnectionStringMatches(connection.Key, connectionStringName, usingPrefix))
                {
                    hasNoMatches = false;

                    foreach (var testCaseData in this.GetTestCases(method, connection.Key))
                    {
                        var testCase = (TestCaseParameters) testCaseData;
                        var testMethod = sTestBuilder.BuildTestMethod(method, testSuite, testCase);
                        var properties = testMethod.Properties;
                        var connectionProperties = connection.Value;

                        properties.Add(DatabasePropertyNames.ConnectionStringName, connection.Key);

                        if (connectionProperties.DataProvider is not null)
                        {
                            properties.Add(DatabasePropertyNames.ProviderName, connectionProperties.DataProvider);
                        }

                        foreach (var argument in testCase.Arguments)
                        {
                            if (argument is Func<DbConnection> factoryDelegate &&
                                factoryDelegate.Target is ConnectionFactory factory)
                            {
                                factory.Properties = properties;
                            }
                            else if (argument is Func<IDbScope> factoryDbScopeDelegate &&
                                     factoryDbScopeDelegate.Target is ConnectionFactory factoryDbScope)
                            {
                                factoryDbScope.Properties = properties;
                            }
                        }

                        try
                        {
                            var factory = ConfigurationManager
                                .GetConfigurationDataProviderFromType(connectionProperties.DataProvider)
                                .GetDbProviderFactory();
                            var conStr = this.BuildConnectionString(factory, connectionProperties.ConnectionString, testMethod);

                            properties.Add(DatabasePropertyNames.ConnectionString, conStr);

                            TestHelper.SetTestCategory(properties, factory.GetDbms());
                        }
                        catch (Exception ex)
                        {
                            testMethod.RunState = RunState.NotRunnable;
                            properties.Set(PropertyNames.SkipReason, ex.Message);
                        }

                        foreach (var attribute in method.GetCustomAttributes<DatabaseScriptsAttribute>(true))
                        {
                            attribute.ApplyToTest(testMethod);
                        }

                        yield return testMethod;
                    }
                }
            }

            if (hasNoMatches)
            {
                foreach (var testCaseData in this.GetTestCases(method, null))
                {
                    var testCase = (TestCaseParameters) testCaseData;
                    var testMethod = sTestBuilder.BuildTestMethod(method, testSuite, testCase);
                    var properties = testMethod.Properties;

                    testMethod.RunState = RunState.NotRunnable;
                    properties.Set(PropertyNames.SkipReason, "No connection strings were provided.");

                    yield return testMethod;
                }
            }
        }

        #endregion

        #region IApplyToTest Overrides

        /// <inheritdoc/>
        void IApplyToTest.ApplyToTest(NUnit.Framework.Internal.Test test)
        {
            Check.ArgumentNotNull(test, nameof(test));

            var properties = test.Properties;

            SetProperty(properties, "Description", this.Description);
            SetProperty(properties, "Author", this.Author);
            SetProperty(properties, "TestOf", this.TestOf);
        }

        #endregion

        #region IWrapSetUpTearDown Overrides

        /// <inheritdoc/>
        TestCommand ICommandWrapper.Wrap(TestCommand command) =>
            new DatabaseTestCommand(command ?? throw new ArgumentNullException(nameof(command)));

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задаёт описание этого теста.
        /// </summary>
        public new string Description { get; set; }

        /// <summary>
        /// Возвращает или задаёт автора этого теста.
        /// </summary>
        public new string Author { get; set; }

        /// <summary>
        /// Возвращает или задаёт тип тестирующий этот тест.
        /// </summary>
        public new Type TestOf { get; set; }

        /// <summary>
        /// Возвращает или задаёт ожидаемое значение.
        /// </summary>
        /// <remarks>Значение проверяется, если выставлен флаг <see cref="HasExpectedResult"/>, иначе игнорируется.</remarks>
        public new object ExpectedResult
        {
            get => this.expectedResult;
            set
            {
                this.expectedResult = value;
                this.HasExpectedResult = true;
            }
        }

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, что ожидается значение <see cref="ExpectedResult"/>.
        /// </summary>
        public bool HasExpectedResult { get; private set; }

        /// <summary>
        /// Возвращает или задаёт имя строки подключения.
        /// </summary>
        /// <remarks>
        /// Может быть задан формат имени строки подключения, которые должны использоваться в тесте.<para/>
        /// Формат имени строки подключения: &lt;Имя строки подключения&gt;[*]. Если задана звёздочка, то выполняется поиск всех строк подключения начинающихся с заданной строки.
        /// </remarks>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Признак того, что имя базы данных надо рандомизировать в зависимости от полного имени теста.
        /// Это позволяет параллельно выполнять тесты на разных базах данных.
        /// </summary>
        public bool RandomizeDbName { get; set; } = true;

        #endregion

        #region Methods

        protected virtual IEnumerable<ITestCaseData> GetTestCases(IMethodInfo method, string connectionKey)
        {
            try
            {
                TestCaseData testCase;
                var parameters = method.GetParameters();
                var arguments = new object[parameters.Length];
                var displayNames = new string[parameters.Length];

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    if (parameter.IsOptional)
                    {
                        arguments[i] = Type.Missing;
                    }
                    else
                    {
                        var parameterType = parameter.ParameterType;
                        if (parameterType == typeof(Func<DbConnection>))
                        {
                            arguments[i] = this.CreateConnectionFactory();
                            displayNames[i] = connectionKey;
                        }
                        else if (parameterType == typeof(Func<IDbScope>))
                        {
                            arguments[i] = this.CreateDbScopeFactory();
                            displayNames[i] = connectionKey;
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unsupported parameter type {parameterType.FullName}.");
                        }
                    }
                }

                testCase = new TestCaseData(arguments);
                testCase.SetArgDisplayNames(displayNames);

                if (this.HasExpectedResult)
                {
                    testCase.ExpectedResult = this.ExpectedResult;
                }

                return Enumerable.Empty<ITestCaseData>().Append(testCase);
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ITestCaseData>().Append(new TestCaseParameters(ex));
            }
        }

        protected Func<DbConnection> CreateConnectionFactory() =>
            new ConnectionFactory().CreateConnection;

        protected Func<IDbScope> CreateDbScopeFactory() =>
            new ConnectionFactory().CreateDbScope;

        #endregion

        #region Private methods

        private static void SetProperty(IPropertyBag properties, string key, object value)
        {
            if (properties.ContainsKey(key) || value == null)
            {
                return;
            }

            properties.Set(key, value);
        }

        private static bool ConnectionStringMatches(string name, string pattern, bool usingPrefix) =>
            usingPrefix
                ? name.StartsWith(pattern, StringComparison.Ordinal)
                : name.Equals(pattern, StringComparison.Ordinal);

        private string BuildConnectionString(DbProviderFactory factory, string connectionString, TestMethod testMethod)
        {
            var builder = factory.CreateConnectionStringBuilder();

            builder.ConnectionString = connectionString;

            var dbms = factory.GetDbms();

            var databaseKey = dbms switch
            {
                Dbms.SqlServer => "Initial Catalog",
                Dbms.PostgreSql => "Database",
                _ => throw new NotSupportedException()
            };

            var databaseName = (string) builder[databaseKey];
            string databasePrefix;
            if (string.IsNullOrEmpty(databaseName))
            {
                databasePrefix = testMethod.MethodName;
            }
            else
            {
                if (this.RandomizeDbName)
                {
                    databasePrefix = databaseName;
                }
                else
                {
                    return connectionString;
                }
            }

            builder[databaseKey] = $"{databasePrefix}_{DateTime.Now.FormatDateTimeCode()}_{testMethod.FullName.GetConstantHashCode():x4}";
            return builder.ToString();
        }

        #endregion
    }
}
