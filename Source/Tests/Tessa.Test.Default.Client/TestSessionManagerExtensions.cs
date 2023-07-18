using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="TestSessionManager"/>.
    /// </summary>
    public static class TestSessionManagerExtensions
    {
        /// <summary>
        /// Открывает сессию на клиенте.
        /// </summary>
        /// <param name="sessionManager">Объект, управляющий сессиями в клиентских тестах.</param>
        /// <param name="userName">Имя пользователя для подключения. Если не задано, используется значение из app.json.</param>
        /// <param name="password">Пароль для подключения. Если не задано, используется значение из app.json.</param>
        /// <param name="extendedInitialization">Значение <see langword="true"/>, если должна выполняться расширенная инициализация при открытии сессии, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>
        /// Если имя пользователя и пароль не заданы в параметрах, то они считываются из параметров приложения (app.json) заданные в секция Settings по ключам
        /// "UserName" и "Password" соответственно. Если имя пользователя или пароль не заданы в параметрах приложения, то выполняется аутентификация пользователя по учётной записи Windows.
        /// </remarks>
        public static async Task OpenAsync(
            this ITestSessionManager sessionManager,
            string userName = null,
            string password = null,
            bool extendedInitialization = false,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(sessionManager, nameof(sessionManager));

            if (sessionManager.IsOpened)
            {
                return;
            }

            userName ??= ConfigurationManager.Settings.TryGet<string>("UserName");
            password ??= ConfigurationManager.Settings.TryGet<string>("Password");

            var loginType = string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)
                ? SessionLoginType.Windows
                : SessionLoginType.Anonymous;

            await sessionManager.OpenAsync(
                new SessionCredentials(loginType, userName, password),
                extendedInitialization,
                cancellationToken: cancellationToken);
        }
    }
}
