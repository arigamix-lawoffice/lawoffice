using System;
using System.Threading.Tasks;
using NLog;
using Tessa.Extensions.Default.Chronos.FileConverters;
using Tessa.Extensions.Default.Server.OnlyOffice;
using Tessa.Platform.Plugins;

namespace Tessa.Extensions.Default.Chronos.OnlyOffice
{
    public sealed class OnlyOfficeRemoveFileCacheInfoPlugin :
        PluginExtension
    {
        #region Constructors

        public OnlyOfficeRemoveFileCacheInfoPlugin(IOnlyOfficeFileCacheInfoStrategy cacheInfoStrategy) =>
            this.cacheInfoStrategy = cacheInfoStrategy ?? throw new ArgumentNullException(nameof(cacheInfoStrategy));

        #endregion

        #region Fields

        private readonly IOnlyOfficeFileCacheInfoStrategy cacheInfoStrategy;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        public override async Task EntryPoint(IPluginExtensionContext context)
        {
            TimeSpan cacheCleanPeriod = FileConverterSettings.OldestPreviewFilePeriod;

            if (cacheCleanPeriod.Ticks > 0L)
            {
                logger.Info("Removing OnlyOffice file cache info older than {0}", cacheCleanPeriod);

                await this.cacheInfoStrategy
                    .CleanCacheInfoAsync(
                        DateTime.UtcNow.Subtract(cacheCleanPeriod),
                        context.CancellationToken);
            }
            else
            {
                logger.Info("Removing OnlyOffice file cache info: disabled in configuration");
            }
        }

        #endregion
    }
}