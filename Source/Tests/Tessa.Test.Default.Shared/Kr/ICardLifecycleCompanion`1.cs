using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает объект, управляющий жизненным циклом карточки.
    /// </summary>
    /// <typeparam name="T">Тип объекта, реализующего данный интерфейс.</typeparam>
    public interface ICardLifecycleCompanion<T> :
        IPendingActionsProvider<IPendingAction, T>,
        ICardLifecycleCompanion where T : ICardLifecycleCompanion<T>
    {
        #region Properties

        /// <summary>
        /// Возвращает информацию по последним запросам и ответам на них выполненным этим объектом.
        /// </summary>
        ICardLifecycleCompanionData LastData { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Создаёт карточку типа <see cref="ICardLifecycleCompanion.CardTypeID"/>.
        /// </summary>
        /// <param name="modifyRequestAction">Метод изменяющий запрос на создание карточки. Выполняется после <see cref="ICardLifecycleCompanionRequestExtender.ExtendNewRequest(CardNewRequest)"/>. Для централизованного управления запросами используйте объект <see cref="ICardLifecycleCompanionRequestExtender"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на создание карточки.
        /// </remarks>
        T Create(Action<CardNewRequest> modifyRequestAction = null);

        /// <summary>
        /// Сохраняет карточку <see cref="Card"/>.
        /// </summary>
        /// <param name="modifyRequestAction">Метод изменяющий запрос на сохранение карточки. Выполняется после <see cref="ICardLifecycleCompanionRequestExtender.ExtendStoreRequest(CardStoreRequest)"/>. Для централизованного управления запросами используйте объект <see cref="ICardLifecycleCompanionRequestExtender"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на сохранение карточки.
        /// </remarks>
        T Save(Action<CardStoreRequest> modifyRequestAction = null);

        /// <summary>
        /// Загружает карточку.
        /// </summary>
        /// <param name="modifyRequestAction">Метод изменяющий запрос на загрузку карточки. Выполняется после <see cref="ICardLifecycleCompanionRequestExtender.ExtendGetRequest(CardGetRequest)"/>. Для централизованного управления запросами используйте объект <see cref="ICardLifecycleCompanionRequestExtender"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на получение карточки.
        /// </remarks>
        T Load(Action<CardGetRequest> modifyRequestAction = null);

        /// <summary>
        /// Удаляет карточку.
        /// </summary>
        /// <param name="modifyRequestAction">Метод изменяющий запрос на удаление карточки. Выполняется после <see cref="ICardLifecycleCompanionRequestExtender.ExtendDeleteRequest(CardDeleteRequest)"/>. Для централизованного управления запросами используйте объект <see cref="ICardLifecycleCompanionRequestExtender"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на удаление карточки.
        /// </remarks>
        T Delete(Action<CardDeleteRequest> modifyRequestAction = null);

        /// <summary>
        /// Добавляет указанную пару ключ-значение в дополнительную информацию (Info) передаваемую при выполнении последнего добавленного действия.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="val">Значение.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T WithInfoPair(string key, object val);

        /// <summary>
        /// Устанавливает указанный словарь в качестве дополнительной информации (Info) для последнего добавленного действия.
        /// </summary>
        /// <param name="info">Дополнительная информация. Если для действия уже была задана дополнительная информация, то она будет объединена с указанной (см. <see cref="Tessa.Platform.Storage.StorageHelper.Merge(IDictionary{string, object}, IDictionary{string, object})"/>).</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T WithInfo(Dictionary<string, object> info);

        /// <summary>
        /// Создаёт новую или загружает существующую карточку синглтон.
        /// </summary>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        T CreateOrLoadSingleton();

        #endregion
    }
}