#nullable enable

using System;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Настройки ограничения размера файлов.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class KrPermissionsFileSizeLimitSettings : StorageObject
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр настроек ограничения для размера файлов.
        /// </summary>
        /// <param name="storage">Хранилище с данными настроек.</param>
        public KrPermissionsFileSizeLimitSettings(Dictionary<string, object?> storage)
            : base(storage)
        {
            this.Init(nameof(this.Limit), Int64Boxes.Zero);
        }

        #endregion

        #region Storage Properties

        /// <summary>
        /// Ограничение на размер файла в байтах.
        /// </summary>
        public long Limit
        {
            get => this.Get<long>(nameof(this.Limit));
            set => this.Set(nameof(this.Limit), Int64Boxes.Box(value));
        }

        /// <summary>
        /// Список категорий, к которым относится настройка, или <c>null</c>, если она относится ко всем категориям.
        /// </summary>
        public IList<Guid>? Categories
        {

            get => this.TryGet<IList<Guid>>(nameof(this.Categories));
            set => this.Set(nameof(this.Categories), value);
        }

        #endregion
    }
}
