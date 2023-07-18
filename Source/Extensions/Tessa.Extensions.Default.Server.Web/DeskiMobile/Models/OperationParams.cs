using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации о файле, над которым выполняются операции в мобильном приложении.
    /// </summary>
    public class OperationParams : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Идентификатор карточки.
        /// </summary>
        public Guid CardID { get; set; }

        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid FileID { get; set; }

        /// <summary>
        /// Идентификатор версии файла.
        /// </summary>
        public Guid VersionRowID { get; set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.CardID)] = this.CardID;
            storage[nameof(this.FileID)] = this.FileID;
            storage[nameof(this.VersionRowID)] = this.VersionRowID;
            storage[nameof(this.FileName)] = this.FileName;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.CardID = TryGetGuid(storage, nameof(this.CardID)) ?? throw new InvalidOperationException($"{nameof(this.CardID)} is required.");
            this.FileID = TryGetGuid(storage, nameof(this.FileID)) ?? throw new InvalidOperationException($"{nameof(this.FileID)} is required.");
            this.VersionRowID = TryGetGuid(storage, nameof(this.VersionRowID)) ?? throw new InvalidOperationException($"{nameof(this.VersionRowID)} is required.");
            this.FileName = TryGetString(storage, nameof(this.FileName));
        }

        #endregion
    }
}
