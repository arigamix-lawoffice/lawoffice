#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Maintenance;

namespace Tessa.Extensions.Default.Shared.Maintenance
{
    /// <summary>
    /// Стратегия получения строк локализации для режима техобслуживания, агрегирующая все именованные реализации <see cref="IMaintenanceLocalizationStrategy"/>.
    /// Учитывает порядок вызова стратегий с атрибутом <see cref="OrderAttribute"/>.
    /// </summary>
    public sealed class AggregateMaintenanceLocalizationStrategy :
        MaintenanceLocalizationStrategyBase
    {
        #region Fields

        private readonly Func<IEnumerable<IMaintenanceLocalizationStrategy>> getStrategiesFunc;

        private readonly object syncObject = new();

        private IMaintenanceLocalizationStrategy[]? strategies;

        #endregion

        #region Constructor

        public AggregateMaintenanceLocalizationStrategy(Func<IEnumerable<IMaintenanceLocalizationStrategy>> getStrategiesFunc) =>
            this.getStrategiesFunc = NotNullOrThrow(getStrategiesFunc);

        #endregion
        
        #region Base Overrides
        
        /// <inheritdoc />
        public override async ValueTask<LocalizationEntryCollection> GetEntriesAsync(
            IReadOnlySet<string> names,
            IDictionary<string, object?> args,
            CancellationToken cancellationToken = default)
        {
            var collection = new LocalizationEntryCollection();
            var neededEntries = names.ToHashSet();
            foreach (var strategy in this.GetStrategies())
            {
                // is strategy allowed to override final entries
                var shouldOverride = strategy.ShouldOverride();
                
                // if so give it neededEntries, elsewise - give it all
                var entries = await strategy.GetEntriesAsync(shouldOverride ? names : neededEntries, args, cancellationToken);
                
                // out merge strategy is first-wins
                foreach (var entry in entries)
                {
                    var name = entry.Name;
                    // exclude entry
                    neededEntries.Remove(name);
                    // first wins (with overrides) - entry taken as a whole (no internal merges for strings and cultures)
                    if (!shouldOverride && collection.ContainsKey(name))
                    {
                        continue;
                    }

                    collection[name] = entry;
                }
            }

            return collection;
        }

        #endregion

        #region Private Methods

        private IEnumerable<IMaintenanceLocalizationStrategy> GetStrategies()
        {
            if (this.strategies is not null)
            {
                return this.strategies;
            }

            lock (this.syncObject)
            {
                var strategies = this.strategies;
                if (strategies is not null)
                {
                    return strategies;
                }

                strategies = this.getStrategiesFunc()
                    .OrderByAttributeAndType()
                    .ToArray();

                this.strategies = strategies;
                return strategies;
            }
        }

        #endregion
    }
}
