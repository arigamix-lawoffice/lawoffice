using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации по проверке параметров подписи, передаваемой в ЛК, после проверки подписи в мобильном приложении.
    /// </summary>
    public sealed class SignatureValidationDataResult : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Флаг, указывающий на необходимость отображения диалога с информацией по проверке параметров подписи.
        /// </summary>
        public bool ShowValidationDialog { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.ShowValidationDialog)] = this.ShowValidationDialog;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.ShowValidationDialog = TryGetBoolean(storage, nameof(this.ShowValidationDialog)) ?? true;
        }

        #endregion
    }
}
