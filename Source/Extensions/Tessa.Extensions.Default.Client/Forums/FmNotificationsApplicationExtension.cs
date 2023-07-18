using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Forums;
using Tessa.Forums.Notifications;
using Tessa.Platform.Licensing;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Controls.Forums;

namespace Tessa.Extensions.Default.Client.Forums
{
    internal class FmNotificationsApplicationExtension : ApplicationExtension
    {
        #region Private Fields

        private Timer timer;

        private readonly INotificationButtonUIManager manager;

        private readonly IUserSettings userSettings;

        private readonly IForumServerSettings forumServerSettings;

        private readonly ILicenseManager licenseManager;

        private readonly IFmNotificationProvider notificationProvider;

        private readonly ISessionFailedChecker sessionFailedChecker;

        private int forumRefreshInterval;

        /// <summary>
        /// Флаг, по которумы мы понимаем выполняется ли сейчас запрос (обработка этого запроса) к серверу
        /// </summary>
        private int isProcessingRequest;

        #endregion

        #region Constructors

        public FmNotificationsApplicationExtension(
            INotificationButtonUIManager manager,
            IUserSettings userSettings,
            IForumServerSettings forumServerSettings,
            ILicenseManager licenseManager,
            IFmNotificationProvider notificationProvider,
            ISessionFailedChecker sessionFailedChecker)
        {
            this.manager = NotNullOrThrow(manager);
            this.userSettings = NotNullOrThrow(userSettings);
            this.forumServerSettings = NotNullOrThrow(forumServerSettings);
            this.licenseManager = NotNullOrThrow(licenseManager);
            this.notificationProvider = NotNullOrThrow(notificationProvider);
            this.sessionFailedChecker = NotNullOrThrow(sessionFailedChecker);
        }

        #endregion

        #region Private Methods

        private async void CheckNotificationsAsync(CancellationToken cancellationToken = default)
        {
            // Обрабатываем случай, у пользователя были включены уведомления и он их отключил.
            // Таймер крутится, но не обновляет ленту уведомлений.
            if (!this.userSettings.EnableMessageIndicator
                || await this.sessionFailedChecker.IsCurrentSessionFailedAsync(cancellationToken))
            {
                return;
            }

            // если isProcessingRequest == (0) false то записываем в isProcessingRequest = 1
            if (Interlocked.CompareExchange(ref this.isProcessingRequest, 1, 0) == 0)
            {
                try
                {
                    await using var _ = SessionRequestTypeContext.Create(SessionRequestType.Background);

                    this.manager.SetNotificationProvider(this.notificationProvider);

                    if (await this.manager.IsExistNotificationsAsync(cancellationToken))
                    {
                        await this.manager.ShowNotificationButtonAsync(false, true, cancellationToken);
                    }

                    this.manager.SetNotificationProvider(this.notificationProvider);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    await this.manager.ShowErrorAsync(ex);
                }
                finally
                {
                    Interlocked.Exchange(ref this.isProcessingRequest, 0);
                }
            }
        }

        #endregion

        #region BaseOverride

        public override async Task Initialize(IApplicationExtensionContext context)
        {
            // получаем интервал из карточки "настройки сервера", если значение пустое (null),
            // то в этом решении отключен индикатор
            var forumRefreshInterval = await this.forumServerSettings.GetForumRefreshIntervalAsync(context.CancellationToken);
            if (forumRefreshInterval != null)
            {
                this.forumRefreshInterval = forumRefreshInterval.Value;

                // проверяем this.userSettings.EnableMessageIndicator, если отключен, то выходим.
                // Но при этом надо учесть момент, что для того чтобы включить индикатор, необходим перезапуск приложения.

                if (LicensingHelper.CheckForumLicense(await this.licenseManager.GetLicenseAsync(context.CancellationToken), out _))
                {
                    if (!this.userSettings.DoNotShowMessageIndicatorOnStartup &&
                        this.userSettings.EnableMessageIndicator &&
                        await this.manager.IsExistNotificationsAsync(context.CancellationToken))
                    {
                        await this.manager.ShowNotificationButtonAsync(true, false, context.CancellationToken);
                    }

                    if (this.timer != null)
                    {
                        await this.timer.DisposeAsync();
                    }

                    // таймер запускаем с задержкой в period, т.к. выше уже выполнялась проверка IsExistNotificationsAsync
                    long period = this.forumRefreshInterval * 1000L;

                    this.timer = new Timer(
                        p => this.CheckNotificationsAsync(),
                        null,
                        period,
                        period);
                }
            }
        }

        public override async Task Shutdown(IApplicationExtensionContext context)
        {
            if (this.timer != null)
            {
                await this.timer.DisposeAsync();
            }

            this.timer = null;
        }

        #endregion
    }
}
