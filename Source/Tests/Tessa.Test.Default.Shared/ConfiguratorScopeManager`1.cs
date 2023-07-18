using System;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет метод возвращающий конфигуратор верхнего уровня.
    /// </summary>
    /// <typeparam name="T">Тип конфигуратора верхнего уровня.</typeparam>
    public class ConfiguratorScopeManager<T> :
        IConfiguratorScopeManager<T>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConfiguratorScopeManager{T}"/> указанным объектом.
        /// </summary>
        /// <param name="scope">Конфигуратор верхнего уровня.</param>
        public ConfiguratorScopeManager(T scope)
        {
            this.Scope = scope;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает конфигуратор верхнего уровня.
        /// </summary>
        public T Scope { get; }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public T Complete()
        {
            if (this.Scope is null)
            {
                throw new InvalidOperationException($"The {typeof(IConfiguratorScopeManager<T>).Name}.{nameof(IConfiguratorScopeManager<T>.Complete)} method is called outside of scope of the type configurator {typeof(T).FullName}.");
            }

            return this.Scope;
        }

        #endregion
    }
}
