using System;
using NUnit.Framework.Interfaces;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="ITest"/>.
    /// </summary>
    public static class TestExtensions
    {
        #region ITest Extensions

        /// <summary>
        /// Возвращает экземпляр класса TestFixture, приведённый к типу <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип, к которому должен быть приведён экземпляр класса TestFixture.</typeparam>
        /// <param name="test">Информация о тесте.</param>
        /// <returns>Экземпляр класса TestFixture, приведённый к <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="test"/> равен <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// Экземпляр класса TestFixture, полученный из <paramref name="test"/>,
        /// не может быть приведён к <typeparamref name="T"/>.
        /// </exception>
        public static T Get<T>(this ITest test)
            where T : class
        {
            Check.ArgumentNotNull(test, nameof(test));

            if (test.Fixture is null)
            {
                throw new ArgumentException("Test fixture is null.", nameof(test));
            }

            if (test.Fixture is not T container)
            {
                throw new InvalidCastException($"Can't cast value fixture of type \"{test.Fixture?.GetType().FullName} to a resulting value \"{typeof(T).FullName}\".");
            }

            return container;
        }

        /// <summary>
        /// Возвращает значение первого свойства из <see cref="ITest.Properties"/>, расположенного в <paramref name="test"/> или в первом объекте <see cref="ITest.Parent"/> его содержащем.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="test">Информация о тесте.</param>
        /// <param name="key">Ключ, по которому расположено значение.</param>
        /// <param name="defaultValueFactory">Фабрика, создающая значение по умолчанию, если оно не найдено в <paramref name="test"/> или его родительских объектах. Если задано значение <see langword="null"/>, то возвращается значение по умолчанию для типа <typeparamref name="T"/>.</param>
        /// <returns>Полученное значение или значение по умолчанию.</returns>
        public static T FirstValueOrDefaultFromTree<T>(
            this ITest test,
            string key,
            Func<T> defaultValueFactory = null)
        {
            Check.ArgumentNotNull(test, nameof(test));
            Check.ArgumentNotNull(key, nameof(key));

            object? value;
            while ((value = test.Properties.Get(key)) is null)
            {
                if (test.Parent is null)
                {
                    return defaultValueFactory is null
                        ? default
                        : defaultValueFactory.Invoke();
                }

                test = test.Parent;
            }

            return (T) value;
        }

        /// <summary>
        /// Возвращает первый объект заданного типа из дерева <see cref="ITest.Parent"/>.
        /// </summary>
        /// <typeparam name="T">Тип искомого объекта.</typeparam>
        /// <param name="test">Информация о тесте.</param>
        /// <returns>Объект искомого типа или значение <see langword="null"/>, если его не удалось найти.</returns>
        public static ITest FirstItemOrDefaultFromTree<T>(
            this ITest test)
            where T : ITest
        {
            Check.ArgumentNotNull(test, nameof(test));

            var typeT = typeof(T);

            while (test.GetType() != typeT)
            {
                test = test.Parent;

                if (test is null)
                {
                    return null;
                }
            }

            return test;
        }

        /// <summary>
        /// Возвращает первый объект заданного типа из дерева <see cref="ITest.Parent"/>.
        /// </summary>
        /// <typeparam name="T">Тип искомого объекта.</typeparam>
        /// <param name="test">Информация о тесте.</param>
        /// <returns>Объект искомого типа.</returns>
        /// <exception cref="InvalidOperationException">Ни один элемент не соответствует типу <typeparamref name="T"/>.</exception>
        public static ITest FirstItemFromTree<T>(
            this ITest test)
            where T : ITest
        {
            var item = FirstItemOrDefaultFromTree<T>(test);

            return item is null
                ? throw new InvalidOperationException($"No element matches the type {typeof(T).FullName}.")
                : item;
        }

        #endregion
    }
}
