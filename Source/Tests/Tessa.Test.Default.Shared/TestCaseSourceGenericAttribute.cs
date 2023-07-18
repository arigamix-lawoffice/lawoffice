#nullable enable

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Указывает источник, который будет использоваться для предоставления параметров теста.
    /// </summary>
    /// <remarks>
    /// Позволяет явно указать типы параметров метода в отличии от <see cref="TestCaseSourceAttribute"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class TestCaseSourceGenericAttribute :
        TestCaseSourceAttribute,
        ITestBuilder
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sourceName">Имя статического метода, свойства или поля, которые предоставляют данные.</param>
        public TestCaseSourceGenericAttribute(
            string sourceName)
            : base(sourceName)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sourceType">Тип, предоставляющий данные.</param>
        /// <param name="sourceName">Имя статического метода, свойства или поля, которые предоставляют данные.</param>
        public TestCaseSourceGenericAttribute(
            Type sourceType,
            string sourceName)
            : base(
                  sourceType,
                  sourceName)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sourceType">Тип, предоставляющий данные.</param>
        /// <param name="sourceName">Имя статического метода, свойства или поля, которые предоставляют данные.</param>
        /// <param name="methodParams">Набор параметров, передаваемых методу, которые предоставляют данные. Не оказывает влияния, если в <paramref name="sourceName"/> указано поле или свойство.</param>
        public TestCaseSourceGenericAttribute(
            Type sourceType,
            string sourceName,
            object?[]? methodParams)
            : base(
                  sourceType,
                  sourceName,
                  methodParams)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sourceName">Имя статического метода, свойства или поля, которые предоставляют данные.</param>
        /// <param name="methodParams">Набор параметров, передаваемых методу, которые предоставляют данные. Не оказывает влияния, если в <paramref name="sourceName"/> указано поле или свойство.</param>
        public TestCaseSourceGenericAttribute(
            string sourceName,
            object?[]? methodParams)
            : base(
                  sourceName,
                  methodParams)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sourceType">Тип, предоставляющий данные.</param>
        public TestCaseSourceGenericAttribute(
            Type sourceType)
            : base(sourceType)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Массив типов параметров тестового метода.
        /// </summary>
        public Type[]? TypeArguments { get; set; }

        #endregion

        #region ITestBuilder Members

        /// <inheritdoc/>
        IEnumerable<TestMethod> ITestBuilder.BuildFrom(
            IMethodInfo method,
            NUnit.Framework.Internal.Test? suite)
        {
            if (!method.IsGenericMethodDefinition)
            {
                return this.BuildFrom(method, suite);
            }

            if (this.TypeArguments is null
                || this.TypeArguments.Length != method.GetGenericArguments().Length)
            {
                var parms = new TestCaseParameters
                {
                    RunState = RunState.NotRunnable,
                };
                parms.Properties.Set(
                    PropertyNames.SkipReason,
                    $"{nameof(this.TypeArguments)} should have {method.GetGenericArguments().Length} elements.");

                return new[]
                {
                    new NUnitTestCaseBuilder()
                        .BuildTestMethod(
                            method,
                            suite,
                            parms),
                };
            }

            var genMethod = method.MakeGenericMethod(
                this.TypeArguments);

            return this.BuildFrom(
                genMethod,
                suite);
        }

        #endregion
    }
}
