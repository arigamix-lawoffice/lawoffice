#nullable enable
using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации по проверке параметров подписи, передаваемой в ЛК, после проверки подписи в мобильном приложении.
    /// </summary>
    public sealed class SignatureValidationInfoItemsResult : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Идентификатор подписи.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Сериализованная структура, которая содержит информацию по проверке различных параметров подписи.
        /// </summary>
        public List<Dictionary<string, object?>>? ValidationInfo { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object?> storage)
        {
            storage[nameof(this.ID)] = this.ID;
            storage[nameof(this.ValidationInfo)] = this.ValidationInfo;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object?> storage)
        {
            this.ID = TryGetGuid(storage, nameof(this.ID)) ?? Guid.Empty;
            this.ValidationInfo = storage.TryGet<List<Dictionary<string, object?>>>(nameof(this.ValidationInfo));
        }

        #endregion
    }
}
