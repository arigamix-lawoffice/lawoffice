using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.GC
{
    /// <summary>
    /// Объект, обеспечивающий обработку внешних объектов.
    /// </summary>
    public interface IExternalObjectManager
    {
        /// <summary>
        /// Выполняет сборку мусора для всех зарегистрированных объектов старше чем <paramref name="keepAliveInterval"/>.
        /// </summary>
        /// <param name="keepAliveInterval">Время жизни объектов. Значение должно быть не отрицательным.</param>
        /// <param name="validationResult">Объект, выполняющий построение результатов валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask CollectAsync(
            TimeSpan keepAliveInterval,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет сборку мусора для указанного владельца объектов.
        /// </summary>
        /// <param name="fixtureID">Идентификатор владельца удаляемых объектов. Обычно это значение, возвращаемое методом <see cref="object.GetHashCode()"/>, где <see cref="object"/> - класс, содержащий текущий набор тестов, в котором был создан внешний ресурс (test fixture).</param>
        /// <param name="validationResult">Объект, выполняющий построение результатов валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask CollectAsync(
            int fixtureID,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет сборку мусора для объекта с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <param name="validationResult">Объект, выполняющий построение результатов валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask CollectAsync(
            Guid id,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет сборку мусора для указанного объекта.
        /// </summary>
        /// <param name="externalObjectInfo">Информация об объекте.</param>
        /// <param name="validationResult">Объект, выполняющий построение результатов валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask CollectAsync(
            ExternalObjectInfo externalObjectInfo,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Регистрирует указанный объект для отслеживания и последующего освобождения при сборе мусора.
        /// </summary>
        /// <param name="obj">Информация о внешнем объекте.</param>
        void RegisterForFinalize(ExternalObjectInfo obj);

        /// <summary>
        /// Прекращает отслеживание указанного объекта.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        void KeepAlive(Guid id);
    }
}
