using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile.Models
{
    /// <summary>
    /// Модель для хранения информации о доступных endpoint'ах при выполнении операции в deskiMobile.
    /// </summary>
    public sealed class StartOperationResponse : StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Абсолютный URI для загрузки потока с бинарными данными файла, указанного в операции.
        /// </summary>
        public string GetContent { get; set; }

        /// <summary>
        /// Абсолютный URI для отмены операции.
        /// </summary>
        public string PostCancel { get; set; }

        /// <summary>
        /// Абсолютный URI для загрузки информации о подписантах.
        /// </summary>
        public string? GetSignatures { get; set; }

        /// <summary>
        /// Абсолютный URI для проверки подписи.
        /// </summary>
        public string? PostVerify { get; set; }

        /// <summary>
        /// Абсолютный URI для усовершенствования подписи.
        /// </summary>
        public string? PostEnhance { get; set; }

        /// <summary>
        /// Абсолютный URI для скрытия диалога с результатами проверки подписи в ЛК.
        /// </summary>
        public string? PostHiddenVerifyWebDialog { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            storage[nameof(this.GetContent)] = this.GetContent;
            storage[nameof(this.PostCancel)] = this.PostCancel;
            storage[nameof(this.GetSignatures)] = this.GetSignatures;
            storage[nameof(this.PostVerify)] = this.PostVerify;
            storage[nameof(this.PostEnhance)] = this.PostEnhance;
            storage[nameof(this.PostHiddenVerifyWebDialog)] = this.PostHiddenVerifyWebDialog;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            this.GetContent = TryGetString(storage, nameof(this.GetContent));
            this.PostCancel = TryGetString(storage, nameof(this.PostCancel));
            this.GetSignatures = TryGetString(storage, nameof(this.GetSignatures));
            this.PostVerify = TryGetString(storage, nameof(this.PostVerify));
            this.PostEnhance = TryGetString(storage, nameof(this.PostEnhance));
            this.PostHiddenVerifyWebDialog = TryGetString(storage, nameof(this.PostHiddenVerifyWebDialog));
        }

        #endregion
    }
}
