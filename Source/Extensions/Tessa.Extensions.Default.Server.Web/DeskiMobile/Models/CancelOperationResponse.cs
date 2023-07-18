#nullable enable
using System.Collections.Generic;
using Tessa.Platform.Operations;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации, передаваемой в ЛК при завершении работы с deskiMobile.
    /// </summary>
    public sealed class CancelOperationResponse : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Полезная нагрузка, заложенная в операцию
        /// </summary>
        public OperationResponse? Response { get; set; }

        /// <summary>
        /// Указывает, была ли отменена операция (пользователем, по timeout отменилась или еще как-то)
        /// </summary>
        public bool Cancelled { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object?> storage)
        {
            storage[nameof(this.Response)] = this.Response?.GetStorage();
            storage[nameof(this.Cancelled)] = this.Cancelled;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object?> storage)
        {
            var responseStorage = storage.TryGet<Dictionary<string, object?>>(nameof(this.Response));
            this.Response = responseStorage is null ? null : new OperationResponse(responseStorage);

            this.Cancelled = TryGetBoolean(storage, nameof(this.Cancelled)) ?? false;
        }

        #endregion
    }
}
