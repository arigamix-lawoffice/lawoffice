using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;

namespace Tessa.Extensions.Shared.Services
{
    /// <summary>
    /// A web service for performing arbitrary actions.
    /// The interface is registered on the client.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Logs in to the system for integration interaction with the web service.
        /// Returns a string with a session token that can be used to pass to other methods for authorization.
        /// </summary>
        /// <param name="parameters">Login parameters.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Session Token.</returns>
        Task<string> LoginAsync(IntegrationLoginParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// We log out of the system by closing the session, which is described by the specified token <paramref name="token"/>.
        /// </summary>
        /// <param name="token">Session token. Returns in methods <c>LoginAsync</c>.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Asynchronous task.</returns>
        Task LogoutAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Service method. It can accept and return arbitrary data.
        /// </summary>
        /// <param name="parameter">Method parameter.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Data returned by the method.</returns>
        Task<string> GetDataAsync(string parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Service method. It can accept and return arbitrary data.
        /// </summary>
        /// <param name="token">Serialized security token.</param>
        /// <param name="parameter">Method parameter.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Data returned by the method.</returns>
        Task<string> GetDataWhenTokenInParameterAsync(string token, string parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Service method. It can accept and return arbitrary data.
        /// When calling the method, authorization via the [SessionMethod] attribute is not performed.
        /// </summary>
        /// <param name="parameter">Method parameter.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Data returned by the method.</returns>
        Task<string> GetDataWithoutCheckingTokenAsync(string parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Загружает карточку. Не отличается от аналогичного метода <see cref="ICardService.GetAsync"/>.
        /// </summary>
        /// <param name="request">Запрос на загрузку карточки.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Ответ на запрос на загрузку карточки</returns>
        Task<CardGetResponse> GetCardAsync(CardGetRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Opens the card and returns JSON with the typed object structure <see cref="CardGetResponse"/>.
        /// The session token is passed in the HTTP header "Tessa-Session".
        /// </summary>
        /// <param name="cardID">Card ID <see cref="Guid"/>. Passed in the address bar.</param>
        /// <param name="cardTypeName">Card type alias. Passed as a parameter in the address bar. Optional parameter.</param>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>The response to the card upload request, serialized as typed JSON.</returns>
        Task<CardGetResponse> GetCardByIDAsync(Guid cardID, string? cardTypeName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns <c>ValidationResult</c> with <c>ValidationKeys.OperationIsUnavailable</c>
        /// and with code <c>HttpStatusCode.Forbidden</c>  - 403.
        /// </summary>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Data returned by the method.</returns>
        Task GetValidationResultErrorAsync(CancellationToken cancellationToken = default);
    }
}
