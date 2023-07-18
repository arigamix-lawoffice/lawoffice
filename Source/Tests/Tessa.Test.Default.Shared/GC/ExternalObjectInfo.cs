using System;
using System.Collections.Generic;
using LiteDB;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.GC
{
    /// <summary>
    /// Информация о внешнем объекте.
    /// </summary>
    public sealed class ExternalObjectInfo
    {
        #region Fields

        private Dictionary<string, object> info;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задаёт идентификатор объекта.
        /// </summary>
        [BsonId]
        public Guid ID { get; set; }

        /// <summary>
        /// Возвращает или задаёт дату и время создания объекта в UTC.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Возвращает или задаёт идентификатор типа объекта.
        /// </summary>
        public Guid TypeID { get; set; }

        /// <summary>
        /// Возвращает или задаёт дополнительную информацию об объекте.
        /// </summary>
        public Dictionary<string, object> Info
        {
            get => this.info ??= new Dictionary<string, object>(StringComparer.Ordinal);
            init => this.info = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор владельца объекта.
        /// </summary>
        /// <remarks>Обычно это значение, возвращаемое методом <see cref="object.GetHashCode()"/>, где <see cref="object"/> - класс, содержащий текущий набор тестов, в котором был создан внешний ресурс (test fixture).</remarks>
        public int FixtureID { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override string ToString() =>
            $"{nameof(this.ID)}={this.ID}, " +
            $"{nameof(this.TypeID)}={this.TypeID}, " +
            $"{nameof(this.Created)}={this.Created}, " +
            $"{nameof(this.Info)}={StorageHelper.Print(this.Info)}";

        #endregion
    }
}
