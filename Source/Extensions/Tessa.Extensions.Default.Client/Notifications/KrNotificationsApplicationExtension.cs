using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Client.Notifications
{
    /// <summary>
    /// Расширение, которое добавляет всплывающие уведомления
    /// </summary>
    public sealed class KrNotificationsApplicationExtension :
        ApplicationExtension
    {
        #region Constructors

        public KrNotificationsApplicationExtension(
            KrSettingsLazy settingsLazy,
            IKrNotificationManager manager)
        {
            this.settingsLazy = settingsLazy;
            this.manager = manager;
        }

        #endregion

        #region Fields

        private Timer timer;

        private readonly KrSettingsLazy settingsLazy;

        private readonly IKrNotificationManager manager;

        #endregion

        #region Base Overrides

        public override async Task Initialize(IApplicationExtensionContext context)
        {
            await this.manager.InitializeAsync(context.CancellationToken).ConfigureAwait(false);
            KrSettings settings = await this.settingsLazy.GetValueAsync(context.CancellationToken).ConfigureAwait(false);

            if (this.timer != null)
            {
                await this.timer.DisposeAsync().ConfigureAwait(false);
            }

            this.timer = new Timer(
                p => this.manager.CheckTasksAsync(),
                null,
                0,
                (int) settings.NotificationCheckInterval.TotalMilliseconds);
        }


        public override async Task Shutdown(IApplicationExtensionContext context)
        {
            if (this.timer != null)
            {
                await this.timer.DisposeAsync().ConfigureAwait(false);
            }

            this.timer = null;

            await this.manager.ShutdownAsync(context.CancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}