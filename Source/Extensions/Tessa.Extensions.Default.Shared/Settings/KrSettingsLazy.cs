using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Shared.Settings
{
    public sealed class KrSettingsLazy
    {
        #region Constructors

        public KrSettingsLazy(Func<CancellationToken, ValueTask<KrSettings>> getSettingsFuncAsync) =>
            this.getSettingsFuncAsync = getSettingsFuncAsync ?? throw new ArgumentNullException(nameof(getSettingsFuncAsync));

        #endregion

        #region Fields

        private readonly Func<CancellationToken, ValueTask<KrSettings>> getSettingsFuncAsync;

        private volatile KrSettings value;

        #endregion

        #region Properties

        public async ValueTask<KrSettings> GetValueAsync(CancellationToken cancellationToken = default) =>
            this.value ??= await this.getSettingsFuncAsync(cancellationToken).ConfigureAwait(false);

        #endregion
    }
}