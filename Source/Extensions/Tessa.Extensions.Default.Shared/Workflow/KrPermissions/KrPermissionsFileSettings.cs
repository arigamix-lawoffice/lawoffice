using System;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Расширенные настройки доступа для конкретного файла.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class KrPermissionsFileSettings : StorageObject
    {
        #region Constructors

        public KrPermissionsFileSettings(Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(nameof(this.FileID), GuidBoxes.Empty);
            this.Init(nameof(this.OwnFile), BooleanBoxes.False);
        }

        #endregion

        #region Storage Properties

        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid FileID
        {
            get => this.Get<Guid>(nameof(this.FileID));
            set => this.Set(nameof(this.FileID), value);
        }

        /// <summary>
        /// Настройка доступа на чтение файла.
        /// </summary>
        public int? ReadAccessSetting
        {
            get => this.TryGet<int?>(nameof(this.ReadAccessSetting));
            set => this.Set(nameof(this.ReadAccessSetting), Int32Boxes.Box(value));
        }

        /// <summary>
        /// Настройка доступа на редактирование файла.
        /// </summary>
        public int? EditAccessSetting
        {
            get => this.TryGet<int?>(nameof(this.EditAccessSetting));
            set => this.Set(nameof(this.EditAccessSetting), Int32Boxes.Box(value));
        }

        /// <summary>
        /// Настройка доступа на удаление файла.
        /// </summary>
        public int? DeleteAccessSetting
        {
            get => this.TryGet<int?>(nameof(this.DeleteAccessSetting));
            set => this.Set(nameof(this.DeleteAccessSetting), Int32Boxes.Box(value));
        }

        /// <summary>
        /// Настройка доступа на подписание файла.
        /// </summary>
        public int? SignAccessSetting
        {
            get => this.TryGet<int?>(nameof(this.SignAccessSetting));
            set => this.Set(nameof(this.SignAccessSetting), Int32Boxes.Box(value));
        }

        /// <summary>
        /// Ограничение на размер файла в байтах.
        /// </summary>
        public long? FileSizeLimit
        {
            get => this.TryGet<long?>(nameof(this.FileSizeLimit));
            set => this.Set(nameof(this.FileSizeLimit), Int64Boxes.Box(value));
        }

        /// <summary>
        /// Определяет, владеет ли текущий сотрудник данным файлом как своим.
        /// </summary>
        public bool OwnFile
        {
            get => this.Get<bool>(nameof(this.OwnFile));
            set => this.Set(nameof(this.OwnFile), BooleanBoxes.Box(value));
        }

        #endregion
    }
}
