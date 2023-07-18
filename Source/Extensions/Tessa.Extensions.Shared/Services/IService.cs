using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;

namespace Tessa.Extensions.Shared.Services
{
    /// <summary>
    /// Веб-сервис для выполнения произвольных действий.
    /// Интерфейс зарегистрирован на клиенте.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Выполняет вход в систему для интеграционного взаимодействия с веб-сервисом.
        /// Возвращает строку с токеном сессии, которую можно использовать для передачи в другие методы для авторизации.
        /// </summary>
        /// <param name="parameters">Параметры входа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Токен сессии.</returns>
        Task<string> LoginAsync(IntegrationLoginParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выходим из системы, закрывая сессию, которая описывается указанным токеном <paramref name="token"/>.
        /// </summary>
        /// <param name="token">Токен сессии. Возвращается в методах <c>LoginAsync</c>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task LogoutAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод сервиса. Может принимать и возвращать произвольные данные.
        /// </summary>
        /// <param name="parameter">Параметр метода.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные, возвращаемые методом.</returns>
        Task<string> GetDataAsync(string parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод сервиса. Может принимать и возвращать произвольные данные.
        /// </summary>
        /// <param name="token">Сериализованный токен безопасности.</param>
        /// <param name="parameter">Параметр метода.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные, возвращаемые методом.</returns>
        Task<string> GetDataWhenTokenInParameterAsync(string token, string parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод сервиса. Может принимать и возвращать произвольные данные.
        /// При вызове метода не выполняется авторизация через атрибут [SessionMethod].
        /// </summary>
        /// <param name="parameter">Параметр метода.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные, возвращаемые методом.</returns>
        Task<string> GetDataWithoutCheckingTokenAsync(string parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Загружает карточку. Не отличается от аналогичного метода <see cref="ICardService.GetAsync"/>.
        /// </summary>
        /// <param name="request">Запрос на загрузку карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Ответ на запрос на загрузку карточки</returns>
        Task<CardGetResponse> GetCardAsync(CardGetRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Открывает карточку и возвращает JSON с типизированной структурой объекта <see cref="CardGetResponse"/>.
        /// Токен сессии передаётся в HTTP-заголовке "Tessa-Session".
        /// </summary>
        /// <param name="cardID">Идентификатор карточки <see cref="Guid"/>. Передаётся в адресной строке.</param>
        /// <param name="cardTypeName">Алиас типа карточки. Передаётся как параметр в адресной строке. Необязательный параметр.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Ответ на запрос на загрузку карточки, сериализованный как типизированный JSON.</returns>
        Task<CardGetResponse> GetCardByIDAsync(Guid cardID, string? cardTypeName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает <c>ValidationResult</c> с <c>ValidationKeys.OperationIsUnavailable</c>
        /// и кодом <c>HttpStatusCode.Forbidden</c>  - 403.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные, возвращаемые методом.</returns>
        Task GetValidationResultErrorAsync(CancellationToken cancellationToken = default);
    }
}
