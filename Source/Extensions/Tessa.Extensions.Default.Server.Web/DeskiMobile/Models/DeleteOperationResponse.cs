#nullable enable
using System.Collections.Generic;
using Tessa.Platform.Storage;
using Tessa.Platform.Operations;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации, передаваемой в ЛК при завершении работы с deskiMobile.
    /// </summary>
    public sealed class DeleteOperationResponse : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Полезная нагрузка, заложенная в операцию
        /// </summary>
        public OperationResponse? Response { get; set; }

        /// <summary>
        /// Указывает, была ли удалена операция
        /// </summary>
        public bool Deleted { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object?> storage)
        {
            storage[nameof(this.Response)] = this.Response?.GetStorage();
            storage[nameof(this.Deleted)] = this.Deleted;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object?> storage)
        {
            var responseDict = storage.TryGet<Dictionary<string, object?>>(nameof(this.Response));
            this.Response = responseDict is null ? null : new OperationResponse(responseDict);
            this.Deleted = TryGetBoolean(storage, nameof(this.Deleted)) ?? false;
        }

        #endregion
    }
}
