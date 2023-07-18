using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Tessa.Test.Default.Shared.Web
{
    /// <summary>
    /// Описывает объект предоставляющий методы для создания тестового сервера предназначенного для тестирования web-приложений.
    /// </summary>
    public interface IWebApplicationFactory :
        IAsyncDisposable
    {
        #region Properties

        /// <summary>
        /// Возвращает объект представляющий сервер.
        /// </summary>
        IHost Host { get; }

        /// <summary>
        /// Возвращает тестовый сервер.
        /// </summary>
        TestServer Server { get; }

        /// <summary>
        /// Возвращает объект управляющий зависимостями на тестовом сервере.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Возвращает объект содержащий параметры используемые при создании <see cref="HttpClient"/> в методе <see cref="CreateClient"/>.
        /// </summary>
        WebApplicationFactoryClientOptions ClientOptions { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Создаёт экземпляр <see cref="HttpClient"/> с помощью которого можно отправлять запросы на тестовый сервер. Используются параметры заданные в <see cref="ClientOptions"/>.
        /// </summary>
        /// <returns>Экземпляр <see cref="HttpClient"/> с помощью которого можно отправлять запросы на тестовый сервер.</returns>
        HttpClient CreateClient();

        /// <summary>
        /// Создаёт экземпляр <see cref="HttpClient"/> с помощью которого можно отправлять запросы на тестовый сервер.
        /// </summary>
        /// <param name="options">Параметры создаваемого клиента.</param>
        /// <returns>Экземпляр <see cref="HttpClient"/> с помощью которого можно отправлять запросы на тестовый сервер.</returns>
        HttpClient CreateClient(WebApplicationFactoryClientOptions options);

        /// <summary>
        /// Создаёт экземпляр <see cref="HttpClient"/> с помощью которого можно отправлять запросы на тестовый сервер.
        /// </summary>
        /// <param name="baseAddress">Базовый адрес создаваемого экземпляра <see cref="HttpClient"/> (<see cref="HttpClient.BaseAddress"/>).</param>
        /// <param name="handlers">Список обработчиков HTTP-ответов используемых создаваемых экземпляром. При обработке списка элементы выстраиваются в цепочку в соответствии с порядком их расположения в списке с использованием свойства <see cref="DelegatingHandler.InnerHandler"/>.</param>
        /// <returns>Экземпляр <see cref="HttpClient"/> с помощью которого можно отправлять запросы на тестовый сервер.</returns>
        HttpClient CreateClient(Uri baseAddress, IList<DelegatingHandler> handlers = default);

        /// <summary>
        /// Инициализирует и запускает тестовый сервер.
        /// </summary>
        void InitializeAndStart();

        #endregion
    }
}
