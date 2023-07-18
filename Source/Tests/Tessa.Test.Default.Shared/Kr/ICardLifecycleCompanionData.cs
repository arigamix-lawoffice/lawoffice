using System.Collections.Generic;
using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает объект предоставляющий информацию по последним запросам и ответам выполненным <see cref="ICardLifecycleCompanion{T}"/>.
    /// </summary>
    public interface ICardLifecycleCompanionData
    {
        /// <summary>
        /// Возвращает последний запрос на создание карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял создание карточки.
        /// </summary>
        CardNewRequest NewRequest { get; }

        /// <summary>
        /// Возвращает последний ответ на запрос на создание карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял создание карточки.
        /// </summary>
        CardNewResponse NewResponse { get; }

        /// <summary>
        /// Возвращает последний запрос на получение карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял получение карточки.
        /// </summary>
        CardGetRequest GetRequest { get; }

        /// <summary>
        /// Возвращает последний ответ на запрос на получение карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял получение карточки.
        /// </summary>
        CardGetResponse GetResponse { get; }

        /// <summary>
        /// Возвращает последний запрос на сохранение карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял сохранение карточки.
        /// </summary>
        CardStoreRequest StoreRequest { get; }

        /// <summary>
        /// Возвращает последний ответ на запрос на сохранение карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял сохранение карточки.
        /// </summary>
        CardStoreResponse StoreResponse { get; }

        /// <summary>
        /// Возвращает последний запрос на удаление карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял удаление карточки.
        /// </summary>
        CardDeleteRequest DeleteRequest { get; }

        /// <summary>
        /// Возвращает последний ответ на запрос на удаление карточки.
        /// Может быть равен <see langword="null"/>, если текущий объект <see cref="ICardLifecycleCompanion{T}"/> ещё не выполнял удаление карточки.
        /// </summary>
        CardDeleteResponse DeleteResponse { get; }

        /// <summary>
        /// Возвращает словарь содержащий запросы любых других типов.
        /// Не равен <see langword="null"/>.
        /// </summary>
        Dictionary<string, object> OtherRequests { get; }

        /// <summary>
        /// Возвращает словарь содержащий ответы на запросы любых других типов.
        /// Не равен <see langword="null"/>.
        /// </summary>
        Dictionary<string, object> OtherResponses { get; }

        /// <summary>
        /// Сбрасывает объект в состояние по умолчанию.
        /// </summary>
        void Reset();
    }
}
