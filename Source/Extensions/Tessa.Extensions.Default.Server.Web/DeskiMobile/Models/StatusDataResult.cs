using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации об операции, передаваемой в ЛК, после отмены операции в мобильном приложении.
    /// </summary>
    public class StatusDataResult : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Флаг указывает, что операция отменена в мобильном приложении.
        /// </summary>
        public bool Canceled { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.Canceled)] = this.Canceled;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.Canceled = TryGetBoolean(storage, nameof(this.Canceled)) ?? true;
        }

        #endregion
    }
}
