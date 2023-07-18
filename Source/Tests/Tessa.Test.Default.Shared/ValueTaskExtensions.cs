using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="ValueTask"/> и <see cref="ValueTask{TResult}"/>.
    /// </summary>
    public static class ValueTaskExtensions
    {
        /// <summary>
        /// Возвращает <see cref="ValueTask"/> представляющий заданный <see cref="ValueTask{TResult}"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта возвращаемого преобразуемым объектом.</typeparam>
        /// <param name="valueTask">Преобразуемый объект.</param>
        /// <returns>Объект <see cref="ValueTask"/> представляющий заданный <see cref="ValueTask{TResult}"/>.</returns>
        public static ValueTask AsValueTask<T>(this ValueTask<T> valueTask)
        {
            if (valueTask.IsCompletedSuccessfully)
            {
                valueTask.GetAwaiter().GetResult();
                return default;
            }

            return new ValueTask(valueTask.AsTask());
        }
    }
}
