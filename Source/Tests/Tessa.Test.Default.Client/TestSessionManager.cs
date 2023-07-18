using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Test.Default.Client
{
    /// <inheritdoc cref="ITestSessionManager"/>
    public sealed class TestSessionManager :
        ITestSessionManager,
        IDisposable
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestSessionManager"/>.
        /// </summary>
        /// <param name="sessionManager">Объект для управления клиентскими сессиями.</param>
        /// <param name="applicationInitializer">Объект, выполняющий инициализацию приложения заданного типа.</param>
        /// <param name="unityDisposableContainer">Контейнер, содержащий объекты <see cref="IDisposable"/>, которые будут освобождены при закрытии контейнеров <see cref="IUnityContainer"/>.</param>
        public TestSessionManager(
            ISessionManager sessionManager,
            IApplicationInitializer applicationInitializer,
            [OptionalDependency] IUnityDisposableContainer unityDisposableContainer = null)
        {
            this.sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
            this.applicationInitializer = applicationInitializer ?? throw new ArgumentNullException(nameof(applicationInitializer));

            unityDisposableContainer?.Register(this);
        }

        #endregion

        #region Fields

        private readonly ISessionManager sessionManager;

        private readonly IApplicationInitializer applicationInitializer;

        #endregion

        #region ITestSessionManager Members

        /// <inheritdoc/>
        public bool IsOpened => this.sessionManager.IsOpened;

        /// <inheritdoc/>
        public async Task OpenAsync(
            ISessionCredentials credentials,
            bool extendedInitialization = false,
            Guid? overrideApplicationID = null,
            CancellationToken cancellationToken = default)
        {
            this.sessionManager.ApplicationID = overrideApplicationID ?? ApplicationIdentifiers.TessaClient;
            this.sessionManager.Credentials = credentials;

            var opened = await this.sessionManager.OpenAsync(cancellationToken: cancellationToken);

            if (!opened)
            {
                throw new InvalidOperationException(
                    "Connection is failed. If timeout occurred the server may be busy, you can try again." +
                    "\nPlease also check login credentials.");
            }

            if (extendedInitialization)
            {
                if (!await this.applicationInitializer.InitializeAsync(cancellationToken: cancellationToken))
                {
                    await this.sessionManager.CloseAsync(cancellationToken);

                    throw new InvalidOperationException("Session failed to initialize, closing it.");
                }
            }
        }

        ///<inheritdoc/>
        public async Task CloseAsync(CancellationToken cancellationToken = default)
        {
            if (this.IsOpened)
            {
                await this.sessionManager.CloseAsync(cancellationToken);
            }
        }

        #endregion

        #region IDisposable Members

        /// <doc path='info[@type="IDisposable" and @item="Dispose"]'/>
        public void Dispose() => this.sessionManager.Dispose();

        #endregion
    }
}
