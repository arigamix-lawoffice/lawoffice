using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы для управления кэшем объектов типа <see cref="CardLifecycleCompanion"/>.
    /// </summary>
    /// <typeparam name="T">Тип ключа.</typeparam>
    public class CardCollectionConfigurator<T> :
        IEnumerable<KeyValuePair<T, CardLifecycleCompanion>>
    {
        #region Fields

        private readonly Dictionary<T, CardLifecycleCompanion> cache;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает ключ текущего объекта <see cref="Current"/> или значение по умолчанию для типа, если значение <see cref="Current"/> не определено.
        /// </summary>
        public T CurrentKey { get; private set; }
        
        /// <summary>
        /// Возвращает текущий объект, управляющий жизненным циклом карточки, или значение по умолчанию для типа, если объект не был установлен или не доступен.
        /// </summary>
        public CardLifecycleCompanion Current { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardCollectionConfigurator{T}"/>.
        /// </summary>
        public CardCollectionConfigurator()
            : this(default)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardCollectionConfigurator{T}"/>.
        /// </summary>
        /// <param name="comparer">Реализация <see cref="IEqualityComparer{T}"/>, которую следует использовать при сравнении ключей, или <see langword="null"/>, если для данного типа ключа должна использоваться реализация <see cref="IEqualityComparer{T}"/> по умолчанию.</param>
        public CardCollectionConfigurator(IEqualityComparer<T> comparer)
        {
            this.cache = new Dictionary<T, CardLifecycleCompanion>(comparer);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Удаляет объект по указанному ключу из кэша конфигуратора.
        /// </summary>
        /// <param name="key">Ключ.</param>
        public void Invalidate(T key)
        {
            if (this.cache.Remove(key, out var deletedValue)
                && this.Current == deletedValue)
            {
                this.InvalidateCurrent();
            }
        }

        /// <summary>
        /// Удаляет все объекты из кэша конфигуратора.
        /// </summary>
        public void Invalidate()
        {
            this.cache.Clear();
            this.InvalidateCurrent();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Устанавливает объект имеющий указанный ключ в качестве текущего. Если объект не существует, то он будет создан.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="getValueFunc">Функция возвращающая объект, если он отсутствует в кэше.</param>
        protected void SetCurrent(T key, Func<CardLifecycleCompanion> getValueFunc)
        {
            Check.ArgumentNotNull(getValueFunc, nameof(getValueFunc));

            if (!this.cache.TryGetValue(key, out var clc))
            {
                clc = getValueFunc();
                this.cache.Add(key, clc);
            }

            this.CurrentKey = key;
            this.Current = clc;
        }

        /// <summary>
        /// Проверяет задан ли текущий объект, если нет, то создаёт исключение <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">The current object is not specified. To set it, use the method <see cref="SetCurrent"/>.</exception>
        [DebuggerStepThrough]
        protected void CheckCurrent()
        {
            if (this.Current is null)
            {
                throw new InvalidOperationException("The current object is not specified. To set it, use the method " + nameof(SetCurrent) + ".");
            }
        }

        #endregion

        #region IEnumerable<T> Members
        
        /// <summary>
        /// Возвращает перечисление значений содержащихся в кэше конфигуратора.
        /// </summary>
        /// <returns>Перечислитель выполняющий перечисление значений содержащихся в кэше конфигуратора.</returns>
        public IEnumerator<KeyValuePair<T, CardLifecycleCompanion>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<T, CardLifecycleCompanion>>)this.cache).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Возвращает перечисление значений содержащихся в кэше конфигуратора.
        /// </summary>
        /// <returns>Перечислитель выполняющий перечисление значений содержащихся в кэше конфигуратора.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.cache).GetEnumerator();
        }

        #endregion

        #region Private methods

        private void InvalidateCurrent()
        {
            this.Current = default;
            this.CurrentKey = default;
        }

        #endregion
    }
}
