using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель, которая ожидается в теле запроса на проверку подписи.
    /// </summary>
    public sealed class VerifyRequest : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Идентификатор подписи.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Данные подписи в виде base64 строки.
        /// </summary>
        public string Signature { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.ID)] = this.ID;
            storage[nameof(this.Signature)] = this.Signature;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.ID = TryGetGuid(storage, nameof(this.ID)) ?? throw new InvalidOperationException($"{nameof(this.ID)} is required.");
            this.Signature = TryGetString(storage, nameof(this.Signature));
        }

        #endregion
    }
}
