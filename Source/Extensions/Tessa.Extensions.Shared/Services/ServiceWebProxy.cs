using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tessa.Cards;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Shared.Services
{
    /// <summary>
    /// Proxy class for accessing controller methods Tessa.Extensions.Server.Web/Controllers/ServiceController.
    /// All the methods in it are asynchronous, but similar to the methods of the service <see cref="IService"/>.
    /// </summary>
    public sealed class ServiceWebProxy :
        WebProxy
    {
        #region Constructors

        /*
         * The path to the controller is passed to the base constructor, specified in the [Route("...")] attribute on the controller class.
         */
        /// <doc path='info[@type="class" and @item=".ctor"]'/>
        public ServiceWebProxy()
            : base("service")
        {
        }

        #endregion

        #region Methods

        /*
         * The Request flag.Ignore Session flag prohibits transmitting the session token in the HTTP header "Tessa-Session".
         * This is relevant in login methods, when passing a token in a parameter, or when calling methods that do not require a login.
         */
        public async Task<string> LoginAsync(IntegrationLoginParameters parameters, CancellationToken cancellationToken = default) =>
            NotNullOrThrow(await this.SendAsync<string>(
                HttpMethod.Post,
                "login",
                RequestFlags.IgnoreSession | RequestFlags.Json,
                cancellationToken,
                parameters: new object[] { parameters }));

        public async Task LogoutAsync(string token, CancellationToken cancellationToken = default) =>
            await this.SendAsync<Void>(
                HttpMethod.Post,
                $"logout?token={HttpUtility.UrlEncode(token)}",
                RequestFlags.IgnoreSession,
                cancellationToken);

        public async Task<string> GetDataAsync(string parameter, CancellationToken cancellationToken = default) =>
            await this.SendAsync<string>(
                HttpMethod.Get,
                $"data?p={HttpUtility.UrlEncode(parameter)}",
                RequestFlags.None,
                cancellationToken) ?? string.Empty;

        public async Task<string> GetDataWhenTokenInParameterAsync(string token, string parameter, CancellationToken cancellationToken = default) =>
            await this.SendAsync<string>(
                HttpMethod.Get,
                $"data?p={HttpUtility.UrlEncode(parameter)}&token={HttpUtility.UrlEncode(token)}",
                RequestFlags.IgnoreSession,
                cancellationToken) ?? string.Empty;

        public async Task<string> GetDataWithoutCheckingTokenAsync(string parameter, CancellationToken cancellationToken = default) =>
            await this.SendAsync<string>(
                HttpMethod.Get,
                $"data-without-login?p={HttpUtility.UrlEncode(parameter)}",
                RequestFlags.IgnoreSession,
                cancellationToken) ?? string.Empty;

        /*
         * To transfer streaming data to the method, use a single parameter of the Stream type.
         * To receive streaming data from the server, return the value Task<Stream>.
         */
        public async Task<CardGetResponse> GetCardAsync(CardGetRequest request, CancellationToken cancellationToken = default) =>
            NotNullOrThrow(await this.SendAsync<CardGetResponse>(
                HttpMethod.Post,
                "cards/get",
                RequestFlags.TypedJson,
                cancellationToken,
                parameters: new object[] { request }));

        /*
         * Example of passing parameters via the address bar.
         */
        public async Task<CardGetResponse> GetCardByIDAsync(Guid cardID, string? cardTypeName = null, CancellationToken cancellationToken = default) =>
            NotNullOrThrow(await this.SendAsync<CardGetResponse>(
                HttpMethod.Get,
                $"cards/{cardID}" + (string.IsNullOrEmpty(cardTypeName) ? null : $"?type={HttpUtility.UrlEncode(cardTypeName)}"),
                cancellationToken: cancellationToken));

        /// <summary>
        /// Returns <c>ValidationResult</c> with <c>ValidationKeys.OperationIsUnavailable</c>
        /// and with code <c>HttpStatusCode.Forbidden</c>  - 403.
        /// </summary>
        /// <param name="cancellationToken">An object that can be used to cancel an asynchronous task.</param>
        /// <returns>Data returned by the method.</returns>
        public async Task GetValidationResultErrorAsync(CancellationToken cancellationToken = default) =>
            await this.SendAsync<Void>(
                HttpMethod.Get,
                "get-validation-result-error",
                RequestFlags.IgnoreSession,
                cancellationToken);

        #endregion
    }
}
