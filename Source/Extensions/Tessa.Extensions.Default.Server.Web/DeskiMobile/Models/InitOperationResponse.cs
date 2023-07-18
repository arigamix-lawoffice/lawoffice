#nullable enable
using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации, передаваемой в ЛК при инициализации операции для работы с мобильным приложением.
    /// </summary>
    public sealed class InitOperationResponse : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Идентификатор операции.
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// Ссылка для подтверждения наличия deskiMobile.
        /// </summary>
        public string? Link { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object?> storage)
        {
            storage[nameof(this.OperationID)] = this.OperationID;
            storage[nameof(this.Link)] = this.Link;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object?> storage)
        {
            this.OperationID = TryGetGuid(storage, nameof(this.OperationID))
                ?? throw new InvalidOperationException($"{nameof(this.OperationID)} is required.");
            this.Link = TryGetString(storage, nameof(this.Link));
        }

        #endregion
    }
}
