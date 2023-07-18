using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Test.Default.Shared.Kr;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Устанавливает область выполнения по умолчанию.<para/>
    /// Для использования в пользовательском коде предназначен атрибут <see cref="TestScopeAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class DefaultTestScopeAttribute :
        NUnitAttribute,
        IApplyToContext
    {
        #region IApplyToContext Members

        /// <inheritdoc/>
        public void ApplyToContext(TestExecutionContext context) =>
            // Действия IApplyToContext.ApplyToContext обрабатываются начиная с родительского класса.
            new KrTestContext(context.CurrentTest).ScopeContext = new ScopeContext(context.CurrentTest.FullName, 1);

        #endregion
    }
}
