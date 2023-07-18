using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы для планирования и удаления карточек после выполнения тестов.
    /// </summary>
    public interface ITestCardManager
    {
        #region Public Methods

        /// <summary>
        /// Добавляет идентификатор карточки, в список на удаление.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки, которая должна быть удалена.</param>
        /// <param name="successActionAsync">Метод, выполняемый при успешном удалении карточки.
        /// Параметры:<para/>
        /// Объект, управляющий жизненным циклом карточки.<para/>
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </param>
        ITestCardManager DeleteCardAfterTest(
            Guid cardID,
            Func<ICardLifecycleCompanion, CancellationToken, ValueTask> successActionAsync = null);

        /// <summary>
        /// Добавляет указанный объект, управляющий жизненным циклом карточки, в список на удаление.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="companion">Объект, управляющий жизненным циклом карточки, которая должна быть удалена.</param>
        /// <param name="successActionAsync">Метод, выполняемый при успешном удалении карточки.<para/>
        /// Параметры:<para/>
        /// Объект, указанный в параметре <paramref name="companion"/>.<para/>
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </param>
        ITestCardManager DeleteCardAfterTest<T>(
            T companion,
            Func<T, CancellationToken, ValueTask> successActionAsync = null)
            where T : ICardLifecycleCompanion;

        /// <summary>
        /// Асинхронно удаляет карточки, добавленные в список на удаление.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task AfterTestAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}