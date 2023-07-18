using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает метод выполняющий запланированные действия с отложенным выполнением.
    /// </summary>
    /// <typeparam name="T">Тип объекта запланированные действия которого выполняются методом <see cref="GoAsync(Action{ValidationResult}, CancellationToken)"/>.</typeparam>
    public interface IPendingActionsExecutor<T>
        where T : IPendingActionsExecutor<T>
    {
        /// <summary>
        /// Выполняет все запланированные действия и проводит валидацию результата выполнения.
        /// </summary>
        /// <param name="validationFunc">Метод выполняющий дополнительную валидацию. Если метод не задан, то используется указанный в контексте <see cref="KrTestContext.ValidationFunc"/>, если указанное свойство возвращает значение <see langword="null"/>, то для проверки используется метод <see cref="ValidationAssert.IsSuccessful(ValidationResult)"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Объект типа <typeparamref name="T"/> запланированные действия которого были выполнены.</returns>
        /// <remarks>По умолчанию результат выполнения проверяется на наличие ошибок, если он содержит ошибки выполнения, то создаётся исключение <see cref="AssertionException"/>.</remarks>
        ValueTask<T> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default);
    }
}