using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения результата проверки конкретной подписи, передаваемой в мобильное приложение.
    /// </summary>
    public sealed class VerifyResponse : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Идентификатор подписи
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Cериализованную структура, которая содержит информацию по проверке различных параметров подписи.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Информация о подписи в виде html строки для отображения во webView внутри мобильного приложения.
        /// </summary>
        public string Html { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.ID)] = this.ID;
            storage[nameof(this.Message)] = this.Message;
            storage[nameof(this.Html)] = this.Html;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.ID = TryGetGuid(storage, nameof(this.ID)) ?? Guid.Empty;
            this.Message = TryGetString(storage, nameof(this.Message));
            this.Html = TryGetString(storage, nameof(this.Html));
        }

        #endregion
    }
}
