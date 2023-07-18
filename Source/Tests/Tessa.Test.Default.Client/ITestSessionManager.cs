using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Runtime;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Объект, управляющий сессиями в клиентских тестах.
    /// </summary>
    public interface ITestSessionManager
    {
        #region Properties

        /// <summary>
        /// Возвращает значение, показывающее, открыта ли сессия.
        /// </summary>
        bool IsOpened { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Открывает сессию.
        /// </summary>
        /// <param name="credentials">Настройки входа, используемые для открытия сессии.</param>
        /// <param name="extendedInitialization">Значение <see langword="true"/>, если должна выполняться расширенная инициализация при открытии сессии, иначе - <see langword="false"/>.</param>
        /// <param name="overrideApplicationID">Идентификатор приложения, переопределяющий значение по умолчанию <see cref="ApplicationIdentifiers.TessaClient"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OpenAsync(
            ISessionCredentials credentials,
            bool extendedInitialization = false,
            Guid? overrideApplicationID = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Закрывает текущую сессию, если она открыта.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task CloseAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
