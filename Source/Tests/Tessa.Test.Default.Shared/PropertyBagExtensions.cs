using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="IPropertyBag"/>.
    /// </summary>
    public static class PropertyBagExtensions
    {
        #region Public Methods

        /// <summary>
        /// Возвращает значение из <paramref name="propertyBag"/>. Если ключ не существует в <paramref name="propertyBag"/>, то добавляет значение, возвращаемое <paramref name="defaultValueFactory"/>, и возвращает его.
        /// </summary>
        /// <typeparam name="T">Тип получаемого значения.</typeparam>
        /// <param name="propertyBag">Объект, из которого требуется получить значение.</param>
        /// <param name="key">Ключ, по которому содержится значение.</param>
        /// <param name="defaultValueFactory">Метод, возвращающий значение используемое при отсутствии ключа. Возвращаемое значение может быть равно <see langword="null"/>.</param>
        /// <returns>Полученное значение.</returns>
        public static T GetOrAdd<T>(
            this IPropertyBag propertyBag,
            string key,
            Func<T> defaultValueFactory = null)
        {
            Check.ArgumentNotNull(propertyBag, nameof(propertyBag));
            Check.ArgumentNotNull(key, nameof(key));

            var value = (T) propertyBag.Get(key);

            if (value is null)
            {
                value = defaultValueFactory is null
                    ? default
                    : defaultValueFactory();

                if (value is not null)
                {
                    propertyBag.Set(key, value);
                }
            }

            return value;
        }

        /// <summary>
        /// Устанавливает значение, которое может быть равно <see langword="null"/>.
        /// </summary>
        /// <param name="propertyBag">Объект, в котором требуется задать значение.</param>
        /// <param name="key">Ключ, по которому должно быть задано значение.</param>
        /// <param name="value">Значение.</param>
        public static void SetNullable(
            this IPropertyBag propertyBag,
            string key,
            object value)
        {
            Check.ArgumentNotNull(propertyBag, nameof(propertyBag));
            Check.ArgumentNotNull(key, nameof(key));

            if (value is null)
            {
                if (propertyBag.Keys.Contains(key))
                {
                    // Из-за особенности реализации безопасным значением является пустой список.
                    // Значение null приведёт к NRE в: "PropertyBag.Get", "PropertyBag.Add", "PropertyBag.this[string]".
                    // Замечание актуально для версии NUnit v3.13.3.

                    // Нельзя использовать постоянные значения из-за возможной модификации списка с помощью метода "PropertyBag.Add" или "PropertyBag.this[string].Add/Remove/...".

                    propertyBag[key] = new List<object>(0);
                }
            }
            else
            {
                propertyBag.Set(key, value);
            }
        }

        #endregion
    }
}
