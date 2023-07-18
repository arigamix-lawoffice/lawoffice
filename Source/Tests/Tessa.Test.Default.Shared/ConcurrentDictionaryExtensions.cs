using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="ConcurrentDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class ConcurrentDictionaryExtensions
    {
        #region Public Methods

        /// <summary>
        /// Добавляет пару "ключ-значение" в коллекцию <see cref="ConcurrentDictionary{TKey, TValue}"/>, если ключ еще не существует. Возвращает новое значение или существующее значение, если ключ существует.
        /// </summary>
        /// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
        /// <typeparam name="TValue">Тип значений в словаре.</typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Ключ.</param>
        /// <param name="valueFactoryAsync">Функция, используемая для создания значения для ключа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение для ключа. Этим значением будет существующее значение ключа, если ключ уже имеется в словаре, или новое значение, если ключ не существовал в словаре.</returns>
        /// <remarks>
        /// Функция <paramref name="valueFactoryAsync"/> выполняется вне блокировки.<para/>
        /// Поскольку пара "ключ-значение" может быть вставлена другим потоком, пока <paramref name="valueFactoryAsync"/> генерирует значение, вы не можете быть уверены, что созданное значение будет вставлено в словарь и возвращено. Если вы вызываете <see cref="GetOrAddAsync{TKey, TValue}(ConcurrentDictionary{TKey, TValue}, TKey, Func{CancellationToken, ValueTask{TValue}}, CancellationToken)"/> одновременно в разных потоках, <paramref name="valueFactoryAsync"/> может вызываться несколько раз, но в словарь будет добавлена только одна пара "ключ-значение".
        /// </remarks>
        public static async ValueTask<TValue> GetOrAddAsync<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dict,
            TKey key,
            Func<CancellationToken, ValueTask<TValue>> valueFactoryAsync,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dict, nameof(dict));
            Check.ArgumentNotNull(key, nameof(key));
            Check.ArgumentNotNull(valueFactoryAsync, nameof(valueFactoryAsync));

            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }

            var newValue = await valueFactoryAsync(cancellationToken);
            var actualValue = dict.GetOrAdd(key, newValue);

            return actualValue;
        }

        #endregion
    }
}
