using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Test.Default.Shared.Kr;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Устанавливает область выполнения.
    /// </summary>
    /// <remarks>
    /// Для глобального включения или отключения областей выполнения, используйте параметр <see cref="TestSettings.UseTestScope"/> в файле конфигурации.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TestScopeAttribute :
        NUnitAttribute,
        IApplyToTest,
        IApplyToContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestScopeAttribute"/>.
        /// </summary>
        /// <param name="name">Название области выполнения.</param>
        public TestScopeAttribute(
            string name)
        {
            TestScopeHelper.ThrowIfNotValidScopeName(name);
            this.Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает название области выполнения.
        /// </summary>
        public string Name { get; }

        #endregion

        #region IApplyToTest Members

        /// <inheritdoc/>
        public void ApplyToTest(NUnit.Framework.Internal.Test test)
        {
            if (!TestSettings.UseTestScope)
            {
                return;
            }

            TestHelper.SetTestCategory(test.Properties, "scope-" + this.Name);
        }

        #endregion

        #region IApplyToContext Members

        /// <inheritdoc/>
        public void ApplyToContext(TestExecutionContext context)
        {
            if (!TestSettings.UseTestScope)
            {
                return;
            }

            var test = context.CurrentTest;

            try
            {
                TestScopeContainer.Instance.Initialize(test.TypeInfo.Assembly);
            }
            catch (Exception ex)
            {
                // Вывод ошибки в результаты теста.
                // В ApplyToTest вывод ошибок не работает совсем.
                // В ApplyToContext необработанная ошибка приводит к прерыванию работы NUnit без вывода какой-либо информации.
                // Для вывода её необходимо указать как результат выполнения теста.
                context.CurrentResult.RecordException(ex, FailureSite.SetUp);
                return;
            }

            TestScopeContainer.Instance.TryGetScopeContext(this.Name, out var scopeContext);
            new KrTestContext(test).ScopeContext = scopeContext;
        }

        #endregion
    }
}
