using System;
using System.Collections.Generic;

namespace Tessa.Test.Default.Shared.Platform.Storage
{
    /// <summary>
    /// Фабрика для создания пустых объектов <c>Dictionary&lt;string, object&gt;</c>. Используется в юнит-тестах.
    /// </summary>
    public sealed class EmptyStorageFactory :
        IStorageFactory
    {
        #region IStorageFactory Members

        /// <summary>
        /// Создаёт экземпляр класса <c>Dictionary&lt;string, object&gt;</c> и не заполняет его.
        /// </summary>
        /// <returns>Созданный экземпляр класса <c>Dictionary&lt;string, object&gt;</c>.</returns>
        public Dictionary<string, object> Create() => new Dictionary<string, object>(StringComparer.Ordinal);

        #endregion
    }
}
