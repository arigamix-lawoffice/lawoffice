#nullable enable

using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Контекст, содержащий дополнительную информацию. Является адаптером для <see cref="ITest"/>.
    /// </summary>
    public class KrTestContext
    {
        #region Fields

        private readonly ITest test;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrTestContext"/>.
        /// </summary>
        /// <param name="test">Информация о тесте.</param>
        public KrTestContext(ITest test) =>
            this.test = NotNullOrThrow(test);

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает текущий контекст.
        /// </summary>
        public static KrTestContext CurrentContext => new KrTestContext(TestHelper.TestExecutionContext.CurrentTest);

        /// <summary>
        /// Возвращает или задаёт метод выполняющий проверку результатов валидации по умолчанию. Возвращает <see langword="null"/>, если значение не задано.
        /// </summary>
        public Action<ValidationResult>? ValidationFunc
        {
            get => (Action<ValidationResult>?) this.test.Properties.Get(nameof(this.ValidationFunc));
            set => this.test.Properties.SetNullable(nameof(this.ValidationFunc), value);
        }

        /// <summary>
        /// Возвращает или задаёт контекст области выполнения. Возвращает значение <see langword="null"/>, если область выполнения не задана.
        /// </summary>
        /// <exception cref="ArgumentNullException">Заданное значение равно <see langword="null"/>.</exception>
        public ScopeContext? ScopeContext
        {
            // Значение устанавливается при обработке класса с атрибутом TestScopeAttribute.
            get => this.test.FirstValueOrDefaultFromTree<ScopeContext?>(nameof(this.ScopeContext));
            set => this.test.Properties.Set(nameof(this.ScopeContext), NotNullOrThrow(value));
        }

        /// <summary>
        /// Возвращает или задаёт объект, предоставляющий методы для планирования и удаления карточек после каждого теста. Возвращает <see langword="null"/>, если значение не задано.
        /// </summary>
        /// <remarks>В проектном коде используйте значение свойства <see cref="ITestBase.TestCardManager"/>.</remarks>
        public ITestCardManager? TestCardManager
        {
            get => (ITestCardManager?) this.test.Properties.Get(nameof(this.TestCardManager));
            set => this.test.Properties.SetNullable(nameof(this.TestCardManager), value);
        }

        /// <summary>
        /// Возвращает или задаёт Unity-контейнер.
        /// </summary>
        /// <exception cref="ArgumentNullException">Заданное значение равно <see langword="null"/>.</exception>
        /// <remarks>В проектном коде используйте значение свойства <see cref="IUnityContainerHolder.UnityContainer"/>.</remarks>
        public IUnityContainer? UnityContainer
        {
            get => this.test.FirstValueOrDefaultFromTree<IUnityContainer?>(nameof(this.UnityContainer));
            set => this.test.FirstItemFromTree<TestFixture>().Properties.Set(nameof(this.UnityContainer), NotNullOrThrow(value));
        }

        #endregion
    }
}
