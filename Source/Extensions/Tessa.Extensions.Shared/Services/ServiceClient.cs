using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Shared.Services
{
    public sealed class ServiceClient :
        IService
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="proxies">Фабрики прокси-объектов для обращения к веб-сервису.</param>
        public ServiceClient(IWebProxyFactory proxies) =>
            this.proxies = proxies ?? throw new ArgumentNullException(nameof(proxies));

        #endregion

        #region Fields

        private readonly IWebProxyFactory proxies;

        #endregion

        #region IService Members

        public async Task<string> LoginAsync(IntegrationLoginParameters parameters, CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            return await proxy.LoginAsync(parameters, cancellationToken).ConfigureAwait(false);
        }


        public async Task LogoutAsync(string token, CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            await proxy.LogoutAsync(token, cancellationToken).ConfigureAwait(false);
        }


        public async Task<string> GetDataAsync(string parameter, CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            return await proxy.GetDataAsync(parameter, cancellationToken).ConfigureAwait(false);
        }


        public async Task<string> GetDataWhenTokenInParameterAsync(string token, string parameter, CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            return await proxy.GetDataWhenTokenInParameterAsync(token, parameter, cancellationToken).ConfigureAwait(false);
        }


        public async Task<string> GetDataWithoutCheckingTokenAsync(string parameter, CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            return await proxy.GetDataWithoutCheckingTokenAsync(parameter, cancellationToken).ConfigureAwait(false);
        }


        public async Task<CardGetResponse> GetCardAsync(CardGetRequest request, CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            return await proxy.GetCardAsync(request, cancellationToken).ConfigureAwait(false);
        }


        public async Task<CardGetResponse> GetCardByIDAsync(
            Guid cardID,
            string? cardTypeName = null,
            CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            return await proxy.GetCardByIDAsync(cardID, cardTypeName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task GetValidationResultErrorAsync(CancellationToken cancellationToken = default)
        {
            await using var proxy = await this.proxies.UseProxyAsync<ServiceWebProxy>(cancellationToken: cancellationToken).ConfigureAwait(false);
            await proxy.GetValidationResultErrorAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
