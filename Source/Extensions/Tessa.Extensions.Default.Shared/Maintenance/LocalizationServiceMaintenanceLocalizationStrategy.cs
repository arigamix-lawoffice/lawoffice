#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Maintenance;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Maintenance
{
    /// <inheritdoc cref="IMaintenanceLocalizationStrategy" />
    /// <remarks>
    /// Получает данные для локализации из <see cref="ILocalizationService"/>.
    /// </remarks>
    [Order(MaintenanceHelper.LocalizationServiceLocalizationOrder)]
    public sealed class LocalizationServiceMaintenanceLocalizationStrategy : 
        MaintenanceLocalizationStrategyBase
    {
        #region Fields

        private readonly ISessionTokenHolder tokenHolder;
        private readonly ILocalizationService service;

        #endregion

        #region Constructor

        public LocalizationServiceMaintenanceLocalizationStrategy(
            ISessionTokenHolder tokenHolder,
            ILocalizationService service)
        {
            this.tokenHolder = NotNullOrThrow(tokenHolder);
            this.service = NotNullOrThrow(service);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask<LocalizationEntryCollection> GetEntriesAsync(
            IReadOnlySet<string> names,
            IDictionary<string, object?> args,
            CancellationToken cancellationToken = default)
        {
            var collection = new LocalizationEntryCollection();
            if (this.tokenHolder.SessionToken is null)
            {
                // no session - couldn't use service.
                return collection;
            }

            if (names.Count < 1)
            {
                // nothing to fetch
                return collection;
            }
            
            string prefix = args.TryGet<string>(MaintenanceHelper.Prefix) ?? string.Empty;
            int prefixLen = prefix.Length;
            var requestedEntries = prefixLen == 0
                ? names
                : names.Select(x => prefix + x).ToHashSet();
            var entries = await this.service.GetGivenEntriesAsync(requestedEntries, cancellationToken);
            
            if (prefixLen == 0)
            {
                // short cut, no need to fix up entry names
                return entries;
            }
            
            foreach (var entry in entries)
            {
                var neededName = entry.Name[prefixLen..];
                var normalizedEntry = new LocalizationEntry(neededName, entry.Comment);
                foreach (var entryString in entry.Strings)
                {
                    normalizedEntry.Strings[entryString.Culture] = entryString;
                }

                collection[normalizedEntry.Name] = normalizedEntry;
            }

            return collection;
        }

        #endregion
    }
}
