using System;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    public static class FileConverterSettings
    {
        #region Constants

        public const string CacheCleanPeriodPropertyName = "FileConverter.CacheCleanPeriod";

        public const string OldestPreviewFilePeriodPropertyName = "FileConverter.OldestPreviewFilePeriod";

        public const string MaintenancePeriodPropertyName = "FileConverter.MaintenancePeriod";

        #endregion

        #region Static Properties

        private static TimeSpan? cacheCleanPeriod;

        public static TimeSpan CacheCleanPeriod =>
            cacheCleanPeriod ?? (cacheCleanPeriod =
                TimeSpan.Parse(ConfigurationManager.Settings.Get<string>(CacheCleanPeriodPropertyName))).Value;


        private static TimeSpan? oldestPreviewFilePeriod;

        public static TimeSpan OldestPreviewFilePeriod =>
            oldestPreviewFilePeriod ?? (oldestPreviewFilePeriod =
                TimeSpan.Parse(ConfigurationManager.Settings.Get<string>(OldestPreviewFilePeriodPropertyName))).Value;


        private static TimeSpan? maintenancePeriod;

        public static TimeSpan MaintenancePeriod =>
            maintenancePeriod ?? (maintenancePeriod =
                TimeSpan.Parse(ConfigurationManager.Settings.Get<string>(MaintenancePeriodPropertyName))).Value;

        #endregion
    }
}