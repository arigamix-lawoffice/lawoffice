using System;
using System.Collections.Generic;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Возвращает индекс первого элемента перечисления удовлетворяющего условию.
        /// </summary>
        /// <typeparam name="T">Тип элементов перечисления.</typeparam>
        /// <param name="collection">Перечисление в котором выполняется поиск индекса элемента.</param>
        /// <param name="predicate">Условие поиска.</param>
        /// <returns>Индекс первого элемента перечисления удовлетворяющего условию или значение -1, если такой элемент не был найден.</returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            Check.ArgumentNotNull(collection, nameof(collection));
            Check.ArgumentNotNull(predicate, nameof(predicate));

            int index = 0;
            foreach (T value in collection)
            {
                if (predicate(value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
