#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Maintenance;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Maintenance
{
    /// <inheritdoc cref="IMaintenanceLocalizationStrategy" />
    /// <remarks>
    /// Получает данные из секции <c>Settings</c>-&gt;<c>Maintenance</c>-&gt;<c>Localization</c> конфигурационного файла <c>app*.json</c>.
    /// </remarks>
    [Order(MaintenanceHelper.ConfigFileLocalizationOrder)]
    public sealed class ConfigurationMaintenanceLocalizationStrategy :
        MaintenanceLocalizationStrategyBase
    {
        #region Fields

        private readonly IConfigurationManager configurationManager;

        private const string LocalizationKey = "Localization";

        #endregion

        #region Constructor

        public ConfigurationMaintenanceLocalizationStrategy(IConfigurationManager configurationManager) =>
            this.configurationManager = NotNullOrThrow(configurationManager);

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override ValueTask<LocalizationEntryCollection> GetEntriesAsync(
            IReadOnlySet<string> names, IDictionary<string, object?> args, CancellationToken cancellationToken = default)
        {
            var collection = new LocalizationEntryCollection();
            var settings =
                this.configurationManager.Configuration.Settings.TryGet<IDictionary<string, object?>>(MaintenanceHelper
                    .MaintenanceConfigSection);
            var dataObj = settings?.TryGet<object>(LocalizationKey);
            if (dataObj is IDictionary<string, object?> data)
            {
                foreach ((string? key, object? objValue) in data)
                {
                    if (objValue is string value)
                    {
                        // split key to lang:name pair
                        int idx = key.IndexOf(":", StringComparison.Ordinal);
                        string name = key, lang = string.Empty;
                        if (idx >= 0)
                        {
                            name = key[(idx + 1)..];
                            lang = key[..idx];
                        }

                        if (!collection.TryGetItem(name, out var entry))
                        {
                            entry = new LocalizationEntry(name);
                            collection[entry.Name] = entry;
                        }

                        var str = new LocalizationString(GetCultureInfo(lang), value);
                        entry.Strings[str.Culture] = str;
                    }
                }    
            }

            return ValueTask.FromResult(collection);
        }

        #endregion

        #region Private Methods

        private static CultureInfo GetCultureInfo(string lang) =>
            string.IsNullOrEmpty(lang) || lang == "iv"
                ? CultureInfo.InvariantCulture
                : CultureInfo.GetCultureInfo(lang);

        #endregion
    }
}
