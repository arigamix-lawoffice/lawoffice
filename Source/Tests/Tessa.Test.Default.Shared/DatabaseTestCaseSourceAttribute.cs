using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Устанавливает информацию о подключении к базе данные, получая её из указанного типа или члена класса, в том числе возвращаемого значения метода.
    /// </summary>
    /// <remarks>
    /// Особенности использования:
    /// <list type="bullet">
    /// <item>
    ///     <description>Источник может вернуть значение только для одного параметра не соответствующего типу Func&lt;DbConnection&gt;.</description>
    /// </item>
    /// <item>
    ///     <description>Если источник данных возвращает значение типа <see cref="ITestCaseData"/>, то оно используется без изменений. Значение параметра типа Func&lt;DbConnection&gt; автоматически не задаётся. Оно должно присутствовать в источнике <see cref="ITestCaseData"/>.</description>
    /// </item>
    /// </list>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class DatabaseTestCaseSourceAttribute : DatabaseTestAttribute
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseTestCaseSourceAttribute"/>.
        /// </summary>
        /// <param name="sourceName">Имя члена класса <see cref="SourceType"/> или если он не задан, то класса содержащего член к которому применён данный атрибут.</param>
        public DatabaseTestCaseSourceAttribute(string sourceName)
        {
            this.SourceName = sourceName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseTestCaseSourceAttribute"/>.
        /// </summary>
        /// <param name="sourceType">Тип содержащий параметры подключения.</param>
        /// <param name="sourceName">Имя члена класса <see cref="SourceType"/> или если он не задан, то класса содержащего член к которому применён данный атрибут.</param>
        /// <param name="methodParams">Параметры метода. Используются для вызова метода с именем <paramref name="sourceName"/>.</param>
        public DatabaseTestCaseSourceAttribute(Type sourceType, string sourceName, object[] methodParams)
        {
            this.MethodParams = methodParams;
            this.SourceType = sourceType;
            this.SourceName = sourceName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseTestCaseSourceAttribute"/>.
        /// </summary>
        /// <param name="sourceType">Тип содержащий параметры подключения.</param>
        /// <param name="sourceName">Имя члена класса <see cref="SourceType"/> или если он не задан, то класса содержащего член к которому применён данный атрибут.</param>
        public DatabaseTestCaseSourceAttribute(Type sourceType, string sourceName)
        {
            this.SourceType = sourceType;
            this.SourceName = sourceName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseTestCaseSourceAttribute"/>.
        /// </summary>
        /// <param name="sourceName">Имя члена класса <see cref="SourceType"/> или если он не задан, то класса содержащего член к которому применён данный атрибут.</param>
        /// <param name="methodParams">Параметры метода. Используются для вызова метода с именем <paramref name="sourceName"/>.</param>
        public DatabaseTestCaseSourceAttribute(string sourceName, object[] methodParams)
        {
            this.MethodParams = methodParams;
            this.SourceName = sourceName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseTestCaseSourceAttribute"/>.
        /// </summary>
        /// <param name="sourceType">Тип содержащий параметры подключения.</param>
        public DatabaseTestCaseSourceAttribute(Type sourceType)
        {
            this.SourceType = sourceType;
        }

        #endregion

        #region DatabaseTestAttribute Overrides

        /// <inheritdoc/>
        protected override IEnumerable<ITestCaseData> GetTestCases(IMethodInfo method, string connectionKey)
        {
            try
            {
                var source = this.GetTestCaseSource(method);
                if (source is not null)
                {
                    var parameters = method.GetParameters();
                    var argsNeeded = parameters.Length;
                    var data = new List<ITestCaseData>();

                    foreach (var item in source)
                    {
                        // First handle two easy cases:
                        // 1. Source is null. This is really an error but if we
                        //    throw an exception we simply get an invalid fixture
                        //    without good info as to what caused it. Passing a
                        //    single null argument will cause an error to be
                        //    reported at the test level, in most cases.
                        // 2. User provided an ITestCaseData and we just use it.
                        var testCase = item is null
                            ? new TestCaseParameters(new object[] { null })
                            : item as ITestCaseData;
                        if (testCase is null)
                        {
                            object[] args = null;
                            string[] displayNames = null;

                            // 3. An array was passed, it may be an object[]
                            //    or possibly some other kind of array, which
                            //    TestCaseSource can accept.
                            if (item is Array array)
                            {
                                // If array has the same number of elements as parameters
                                // and it does not fit exactly into single existing parameter
                                // we believe that this array contains arguments, not is a bare
                                // argument itself.
                                if (argsNeeded > 0 && argsNeeded == array.Length && parameters[0].ParameterType != array.GetType())
                                {
                                    args = new object[argsNeeded];
                                    displayNames = new string[argsNeeded];

                                    for (var i = 0; i < argsNeeded; i++)
                                    {
                                        var value = array.GetValue(i);
                                        if (value is null)
                                        {
                                            var parameter = parameters[i];
                                            var parameterType = parameter.ParameterType;
                                            if (parameterType == typeof(Func<DbConnection>))
                                            {
                                                value = this.CreateConnectionFactory();
                                                displayNames[i] = connectionKey;
                                            }
                                            else if (parameterType == typeof(Func<IDbScope>))
                                            {
                                                value = this.CreateDbScopeFactory();
                                                displayNames[i] = connectionKey;
                                            }
                                        }
                                        else
                                        {
                                            var nameGenerator = new TestNameGenerator($"{{{i}}}");
                                            displayNames[i] = nameGenerator.GetDisplayName(new TestMethod(method), args);
                                        }

                                        args[i] = value;
                                    }
                                }
                            }

                            if (args is null)
                            {
                                if (argsNeeded > 2)
                                {
                                    throw new InvalidOperationException("Unable to bind parameters.");
                                }

                                args = new object[argsNeeded];
                                displayNames = new string[argsNeeded];

                                for (var i = 0; i < argsNeeded; i++)
                                {
                                    var parameter = parameters[i];
                                    var parameterType = parameter.ParameterType;

                                    if (parameterType == typeof(Func<DbConnection>))
                                    {
                                        args[i] = this.CreateConnectionFactory();
                                        displayNames[i] = connectionKey;
                                    }
                                    else if (parameterType == typeof(Func<IDbScope>))
                                    {
                                        args[i] = this.CreateDbScopeFactory();
                                        displayNames[i] = connectionKey;
                                    }
                                    else
                                    {
                                        args[i] = item;

                                        var nameGenerator = new TestNameGenerator($"{{{i}}}");
                                        displayNames[i] = nameGenerator.GetDisplayName(new TestMethod(method), args);
                                    }
                                }
                            }

                            var testCaseData = new TestCaseData(args);
                            testCaseData.SetArgDisplayNames(displayNames);

                            testCase = testCaseData;
                        }

                        if (this.HasExpectedResult)
                        {
                            if (testCase is TestCaseParameters testCaseParams)
                            {
                                testCaseParams.ExpectedResult = this.ExpectedResult;
                            }
                        }

                        data.Add(testCase);
                    }

                    return data;
                }

                return Enumerable.Empty<ITestCaseData>()
                    .Append(new TestCaseParameters(new Exception("The test case source could not be found.")));
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ITestCaseData>().Append(new TestCaseParameters(ex));
            }
        }

        private IEnumerable GetTestCaseSource(IMethodInfo testMethod)
        {
            var sourceType = this.SourceType ?? testMethod.TypeInfo.Type;

            // Handle Type implementing IEnumerable separately
            if (this.SourceName is null)
            {
                return Reflect.Construct(sourceType, null) as IEnumerable;
            }

            var members = sourceType.GetMember(this.SourceName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            if (members.Length != 1)
            {
                return null;
            }

            switch (members[0])
            {
                case FieldInfo field when field.IsStatic:
                    return this.MethodParams is null
                        ? (IEnumerable) field.GetValue(null)
                        : ReturnErrorAsParameter(ParamGivenToField);

                case FieldInfo _:
                    return ReturnErrorAsParameter(SourceMustBeStatic);

                case PropertyInfo property:
                    var getMethod = property.GetGetMethod(true);
                    return getMethod?.IsStatic == true
                        ? this.MethodParams is null
                            ? (IEnumerable) getMethod.Invoke(null, null)
                            : ReturnErrorAsParameter(ParamGivenToProperty)
                        : ReturnErrorAsParameter(SourceMustBeStatic);

                case MethodInfo method when method.IsStatic:
                    return this.MethodParams is null || this.MethodParams.Length == method.GetParameters().Length
                        ? (IEnumerable) method.Invoke(null, this.MethodParams)
                        : ReturnErrorAsParameter(NumberOfArgsDoesNotMatch);

                case MethodInfo _:
                    return ReturnErrorAsParameter(SourceMustBeStatic);
            }

            return null;
        }

        private static IEnumerable ReturnErrorAsParameter(string errorMessage)
        {
            var parms = new TestCaseParameters { RunState = RunState.NotRunnable };
            parms.Properties.Set(PropertyNames.SkipReason, errorMessage);

            return new[] { parms };
        }

        private const string SourceMustBeStatic =
            "The sourceName specified on a TestCaseSourceAttribute must refer to a static field, property or method.";

        private const string ParamGivenToField =
            "You have specified a data source field but also given a set of parameters. Fields cannot take parameters, " +
            "please revise the 3rd parameter passed to the TestCaseSourceAttribute and either remove " +
            "it or specify a method.";

        private const string ParamGivenToProperty =
            "You have specified a data source property but also given a set of parameters. " +
            "Properties cannot take parameters, please revise the 3rd parameter passed to the " +
            "TestCaseSource attribute and either remove it or specify a method.";

        private const string NumberOfArgsDoesNotMatch =
            "You have given the wrong number of arguments to the method in the TestCaseSourceAttribute" +
            ", please check the number of parameters passed in the object is correct in the 3rd parameter for the " +
            "TestCaseSourceAttribute and this matches the number of parameters in the target method and try again.";

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает параметры метода возвращающего параметры о подключения.
        /// </summary>
        public object[] MethodParams { get; }

        /// <summary>
        /// Возвращает имя члена класса <see cref="SourceType"/> или если он не задан, то класса содержащего член к которому применён данный атрибут содержащий параметры подключения.
        /// </summary>
        public string SourceName { get; }

        /// <summary>
        /// Возвращает тип содержащий параметры подключения.
        /// </summary>
        public Type SourceType { get; }

        #endregion
    }
}