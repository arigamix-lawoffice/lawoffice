using System.Collections.Generic;
using Tessa.Platform.EDS;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации о подписи, передаваемой в ЛК, после успешной подписи в мобильном приложении.
    /// </summary>
    public sealed class SignedDataResult : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Данные подписи в виде base64 строки.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Тип подписи.
        /// </summary>
        public SignatureType Type { get; set; }

        /// <summary>
        /// Профиль ЭЦП, обычно для типа подписи <see cref="Tessa.Platform.EDS.SignatureType.CAdES"/>.
        /// </summary>
        public SignatureProfile Profile { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.Signature)] = this.Signature;
            storage[nameof(this.Type)] = this.Type.ToString();
            storage[nameof(this.Profile)] = this.Profile.ToString();
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.Signature = TryGetString(storage, nameof(this.Signature));
            this.Type = TryGetEnum<SignatureType>(storage, nameof(this.Type));
            this.Profile = TryGetEnum<SignatureProfile>(storage, nameof(this.Profile));
        }

        #endregion
    }
}
