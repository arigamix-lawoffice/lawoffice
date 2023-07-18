using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="ISessionClient"/>.
    /// </summary>
    public static class SessionClientExtensions
    {
        /// <summary>
        /// Открывает сессию на клиенте.
        /// </summary>
        /// <param name="client">Объект, обеспечивающий взаимодействие с сессиями на клиенте.</param>
        /// <param name="userName">Имя пользователя для подключения. Если не задано, используется значение из app.json</param>
        /// <param name="password">Пароль для подключения. Если не задано, используется значение из app.json</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>
        /// Если имя пользователя и пароль не заданы в параметрах, то они считываются из параметров приложения (app.json) заданные в секции "Settings" по ключам
        /// "UserName" и "Password" соответственно. Если имя пользователя или пароль не заданы в параметрах приложения, то выполняется аутентификация пользователя по учётной записи Windows.<para/>
        /// Перед открытием новой сессии выполняется закрытие старой.
        /// </remarks>
        public static async Task OpenTestSessionAsync(
            this ISessionClient client,
            string userName = null,
            string password = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(client, nameof(client));

            await client.CloseSessionSafeAsync(cancellationToken);

            userName ??= ConfigurationManager.Settings.TryGet<string>("UserName");
            password ??= ConfigurationManager.Settings.TryGet<string>("Password");

            var applicationID = ApplicationIdentifiers.TessaClient;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                await client.OpenSessionWindowsAuthAsync(applicationID, cancellationToken: cancellationToken);
            }
            else
            {
                await client.OpenSessionAsync(userName, password, applicationID, cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Закрывает текущую сессию. Все возможные исключения игнорируются.
        /// </summary>
        /// <param name="client">Объект, обеспечивающий взаимодействие с сессиями на клиенте.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task CloseSessionSafeAsync(this ISessionClient client, CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(client, nameof(client));

            try
            {
                await client.CloseSessionAsync(cancellationToken);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
