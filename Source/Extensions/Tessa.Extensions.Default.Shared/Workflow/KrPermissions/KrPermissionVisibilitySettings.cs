using System;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Настройки видимости элементов управления.
    /// </summary>
    public readonly struct KrPermissionVisibilitySettings
    {
        #region Constructors

        public KrPermissionVisibilitySettings(
            string alias,
            int controlType,
            bool isHidden)
        {
            this.Alias = alias;
            this.ControlType = controlType;
            this.IsHidden = isHidden;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Алиас элементов управления, к которым применяется данная настрйока.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Тип элемента управления.
        /// </summary>
        public int ControlType { get; }

        /// <summary>
        /// Определяет, должны ли элементы управления быть скрыты.
        /// </summary>
        public bool IsHidden { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Сериализует настройки в хранилище данных.
        /// </summary>
        /// <returns>Сериализованные в хранилище данных настройки видимости.</returns>
        public Dictionary<string, object> ToStorage()
        {
            return new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [nameof(this.Alias)] = Alias,
                [nameof(this.ControlType)] = Int32Boxes.Box(this.ControlType),
                [nameof(this.IsHidden)] = BooleanBoxes.Box(this.IsHidden),
            };
        }

        /// <summary>
        /// Десериализует настройки видимости из хранилища данных.
        /// </summary>
        /// <param name="storage">Хранилище данных.</param>
        /// <returns>Настройки видимости.</returns>
        public static KrPermissionVisibilitySettings FromStorage(Dictionary<string, object> storage)
        {
            return new KrPermissionVisibilitySettings(
                storage.Get<string>(nameof(Alias)),
                storage.Get<int>(nameof(ControlType)),
                storage.Get<bool>(nameof(IsHidden)));
        }

        #endregion
    }
}
