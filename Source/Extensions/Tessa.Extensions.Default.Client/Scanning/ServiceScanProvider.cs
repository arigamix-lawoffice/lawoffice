using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Extensions.Platform.Client.Scanning;
using Tessa.Host;
using Tessa.Platform.Validation;
using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Notifications;

namespace Tessa.Extensions.Default.Client.Scanning
{
    /// <summary>
    ///     Провайдер взаимодействующий с сервисом сканирования
    /// </summary>
    public sealed class ServiceScanProvider :
        ViewModel<EmptyModel>,
        IScanProvider
    {
        #region Constructors

        public ServiceScanProvider(
            [NotNull] ScanServiceProxyFactory proxyFactory,
            [NotNull] INotificationUIManager notificationUIManager)
        {
            this.notificationUIManager = notificationUIManager ?? throw new ArgumentNullException(nameof(notificationUIManager));
            this.proxyFactory = proxyFactory ?? throw new ArgumentNullException(nameof(proxyFactory));
            this.proxy = new Lazy<IScanServiceProxy>(this.CreateProxy);
        }

        #endregion

        #region Fields

        [NotNull]
        private readonly INotificationUIManager notificationUIManager;

        [NotNull]
        private readonly ScanServiceProxyFactory proxyFactory;

        [CanBeNull]
        private Func<MemoryStream, CancellationToken, ValueTask> processScannedStreamActionAsync;

        [CanBeNull]
        private Lazy<IScanServiceProxy> proxy;

        #endregion

        #region Private Methods

        private IScanServiceProxy CreateProxy()
        {
            IScanServiceProxy proxy = proxyFactory();
            PropertyChangedEventManager.AddHandler(proxy, this.StateChanged, nameof(IScanServiceProxy.State));
            return proxy;
        }

        private IScanServiceProxy GetProxy() =>
            (this.proxy ?? throw new ObjectDisposedException(this.GetType().Name)).Value;

        private async void StateChanged(object sender, PropertyChangedEventArgs e) =>
            await this.OnPropertyChangedAsync(nameof(State));

        private async ValueTask DisposeProxyAsync()
        {
            if (this.proxy?.IsValueCreated == true)
            {
                await this.proxy.Value.DisposeAsync().ConfigureAwait(false);
                this.proxy = null;
            }
        }

        #endregion

        #region IScanProvider Members

        /// <inheritdoc />
        public ScanState State => this.proxy?.IsValueCreated == true ? this.proxy.Value.State : ScanState.Completed;

        /// <inheritdoc />
        public async ValueTask<List<IScanSource>> GetSourcesAsync(CancellationToken cancellationToken = default) =>
            (await this.GetProxy().GetSourcesAsync(cancellationToken).ConfigureAwait(false))
            .Select(s => (IScanSource) new ServiceScanSourceViewModel(s))
            .ToList();

        /// <inheritdoc />
        public async ValueTask<bool> ScanAsync(IScanSource source, CancellationToken cancellationToken = default)
        {
            try
            {
                var scanSource = new ScanSource(
                    ((ServiceScanSourceViewModel) source).ID,
                    source.Name,
                    source.ProtocolVersion,
                    source.DriverVersion);

                var request = new ScanRequest { ScanSource = scanSource };
                await this.GetProxy().StartScanAsync(
                    request,
                    (stream, ct) => this.processScannedStreamActionAsync?.Invoke(stream, ct) ?? new ValueTask(),
                    async (result, ct) =>
                    {
                        if (!result.IsSuccessful)
                        {
                            var _ = Task.Run(() => this.CancelAsync(ct), ct);
                        }

                        if (!this.notificationUIManager.IsMuted())
                        {
                            foreach (ValidationResultItem item in result.Items)
                            {
                                await this.notificationUIManager.ShowTextAsync(
                                    item.Message,
                                    clickCommand: new DelegateCommand(p => TessaDialog.ShowNotEmpty(result)),
                                    textAlignment: TextAlignment.Left).ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            await TessaDialog.ShowNotEmptyAsync(result).ConfigureAwait(false);
                        }
                    },
                    cancellationToken).ConfigureAwait(false);
            }
            catch (ScannerBusyException)
            {
                await this.notificationUIManager.ShowTextOrMessageBoxAsync("$UI_Misc_ScannerIsBusy").ConfigureAwait(false);
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public async ValueTask CancelAsync(CancellationToken cancellationToken = default)
        {
            if (this.proxy?.IsValueCreated == true)
            {
                await this.proxy.Value.CancelAsync(cancellationToken).ConfigureAwait(false);
                await this.DisposeProxyAsync().ConfigureAwait(false);
                this.proxy = new Lazy<IScanServiceProxy>(this.CreateProxy);
            }
        }

        /// <inheritdoc />
        public void SetProcessAction(Func<MemoryStream, CancellationToken, ValueTask> actionAsync) =>
            this.processScannedStreamActionAsync = actionAsync;

        #endregion

        #region IAsyncDisposable Members

        /// <inheritdoc />
        public ValueTask DisposeAsync()
        {
            // внутри лямбды может держаться существенная часть UI;
            // если вдруг утечёт ссылка на ServiceScanProvider, то хотя бы не содержимое лямбды
            this.processScannedStreamActionAsync = null;
            
            return this.DisposeProxyAsync();
        }

        #endregion
    }
}