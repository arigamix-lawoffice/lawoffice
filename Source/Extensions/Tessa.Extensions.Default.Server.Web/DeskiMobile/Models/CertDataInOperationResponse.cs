using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации о сертификате подписи, передаваемой в ЛК, после успешной подписи в мобильном приложении.
    /// </summary>
    public sealed class CertDataInOperationResponse : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Название компании указанное в сертификате.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Получатель сертификата.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Серийный номер сертификата.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Издатель сертификата.
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Дата, начиная с которой сертификат валиден в миллисекундах.
        /// </summary>
        public string ValidFrom { get; set; }

        /// <summary>
        /// Дата окончания валидности сертификата в миллисекундах.
        /// </summary>
        public string ValidTo { get; set; }

        /// <summary>
        /// Объект сертификата в base64.
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        /// Отпечаток сертификата.
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Комментарии к подписи.
        /// </summary>
        public string? Comment { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.Company)] = this.Company;
            storage[nameof(this.SubjectName)] = this.SubjectName;
            storage[nameof(this.SerialNumber)] = this.SerialNumber;
            storage[nameof(this.IssuerName)] = this.IssuerName;
            storage[nameof(this.ValidFrom)] = this.ValidFrom;
            storage[nameof(this.ValidTo)] = this.ValidTo;
            storage[nameof(this.Certificate)] = this.Certificate;
            storage[nameof(this.Thumbprint)] = this.Thumbprint;
            storage[nameof(this.Comment)] = this.Comment;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.Company = TryGetString(storage, nameof(this.Company));
            this.SubjectName = TryGetString(storage, nameof(this.SubjectName));
            this.SerialNumber = TryGetString(storage, nameof(this.SerialNumber));
            this.IssuerName = TryGetString(storage, nameof(this.IssuerName));
            this.ValidFrom = TryGetString(storage, nameof(this.ValidFrom));
            this.ValidTo = TryGetString(storage, nameof(this.ValidTo));
            this.Certificate = TryGetString(storage, nameof(this.Certificate));
            this.Thumbprint = TryGetString(storage, nameof(this.Thumbprint));
            this.Comment = TryGetString(storage, nameof(this.Comment));
        }

        #endregion
    }
}
