using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель, которая ожидается в теле запроса на усовершенствование подписи.
    /// </summary>
    public sealed class EnhanceRequest : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Данные подписи в виде base64 строки.
        /// </summary>
        public string Signature { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.Signature)] = this.Signature;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.Signature = TryGetString(storage, nameof(this.Signature));
        }

        #endregion
    }
}
