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
    /// Помечает универсальный метод как параметризованный набор тестов и предоставляет аргументы для каждого тестового примера.
    /// </summary>
    /// <remarks>
    /// Позволяет явно указать типы параметров метода в отличии от <see cref="TestCaseAttribute"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class TestCaseGenericAttribute :
        TestCaseAttribute,
        ITestBuilder
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public TestCaseGenericAttribute()
            : this(Array.Empty<object>())
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="arg">Значение параметра тестового метода.</param>
        public TestCaseGenericAttribute(
            object? arg)
            : base(arg)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="arg1">Значение первого параметра тестового метода.</param>
        /// <param name="arg2">Значение второго параметра тестового метода.</param>
        public TestCaseGenericAttribute(
            object? arg1,
            object? arg2)
            : base(arg1, arg2)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="arg1">Значение первого параметра тестового метода.</param>
        /// <param name="arg2">Значение второго параметра тестового метода.</param>
        /// <param name="arg3">Значение третьего параметра тестового метода.</param>
        public TestCaseGenericAttribute(
            object? arg1,
            object? arg2,
            object? arg3)
            : base(arg1, arg2, arg3)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="arguments">Параметры тестового метода.</param>
        public TestCaseGenericAttribute(
            params object?[]? arguments)
            : base(arguments)
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
