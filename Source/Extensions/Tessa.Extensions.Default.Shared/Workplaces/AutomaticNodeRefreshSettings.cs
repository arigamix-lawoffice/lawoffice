#nullable enable

using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workplaces
{
    /// <summary>
    /// Настройки автоматического обновления узлов рабочего места.
    /// </summary>
    public class AutomaticNodeRefreshSettings : StorageSerializable, IAutomaticNodeRefreshSettings
    {
        #region Constructor

        /// <inheritdoc />
        public AutomaticNodeRefreshSettings()
        {
            this.RefreshInterval = 300;
            this.WithContentDataRefreshing = true;
        }

        #endregion

        #region IAutomaticNodeRefreshSettings Implementation

        /// <inheritdoc />
        public int RefreshInterval { get; set; }

        /// <inheritdoc />
        public bool WithContentDataRefreshing { get; set; }

        #endregion

        #region IStorageSerializable Members

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object?> storage)
        {
            base.SerializeCore(storage);
        
            storage[nameof(this.RefreshInterval)] = this.RefreshInterval;
            storage[nameof(this.WithContentDataRefreshing)] = this.WithContentDataRefreshing;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object?> storage)
        {
            base.DeserializeCore(storage);
        
            this.RefreshInterval = storage.TryGet<int>(nameof(this.RefreshInterval));
            this.WithContentDataRefreshing = storage.TryGet<bool>(nameof(this.WithContentDataRefreshing));
        }

        #endregion
    }
}
