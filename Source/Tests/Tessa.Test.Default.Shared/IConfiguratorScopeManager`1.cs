using System;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Описывает метод возвращающий конфигуратор верхнего уровня.
    /// </summary>
    /// <typeparam name="T">Тип конфигуратора верхнего уровня.</typeparam>
    public interface IConfiguratorScopeManager<T>
    {
        /// <summary>
        /// Возвращает конфигуратор верхнего уровня.
        /// </summary>
        /// <returns>Конфигуратор верхнего уровня.</returns>
        /// <exception cref="InvalidOperationException">Выполнение осуществляется не в рамках конфигуратора типа <typeparamref name="T"/>.</exception>
        T Complete();
    }
}