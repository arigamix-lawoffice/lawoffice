using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает методы расширяющие запросы выполняемые <see cref="ICardLifecycleCompanion{T}"/>.
    /// </summary>
    public interface ICardLifecycleCompanionRequestExtender
    {
        /// <summary>
        /// Метод для расширения запроса на создание карточки.
        /// </summary>
        /// <param name="request">Расширяемый запрос.</param>
        void ExtendNewRequest(CardNewRequest request);

        /// <summary>
        /// Метод для расширения запроса на получение карточки.
        /// </summary>
        /// <param name="request">Расширяемый запрос.</param>
        void ExtendGetRequest(CardGetRequest request);

        /// <summary>
        /// Метод для расширения запроса на сохранение карточки.
        /// </summary>
        /// <param name="request">Расширяемый запрос.</param>
        void ExtendStoreRequest(CardStoreRequest request);

        /// <summary>
        /// Метод для расширения запроса на удаление карточки.
        /// </summary>
        /// <param name="request">Расширяемый запрос.</param>
        void ExtendDeleteRequest(CardDeleteRequest request);
    }
}
