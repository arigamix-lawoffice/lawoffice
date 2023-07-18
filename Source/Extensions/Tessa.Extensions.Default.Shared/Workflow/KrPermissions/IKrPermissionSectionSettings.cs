using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Расширенные настройки доступа к секции.
    /// </summary>
    public interface IKrPermissionSectionSettings
    {
        /// <summary>
        /// Идентификатор секции.
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Определяет, запрещено ли добавление строк секции.
        /// </summary>
        bool DisallowRowAdding { get; set; }

        /// <summary>
        /// Определяет, запрещено ли удаление строк секции.
        /// </summary>
        bool DisallowRowDeleting { get; set; }

        /// <summary>
        /// Определяет, разрешено ли полное изменение секции.
        /// </summary>
        bool IsAllowed { get; set; }

        /// <summary>
        /// Определяет, запрещено ли полное изменение секции.
        /// </summary>
        bool IsDisallowed { get; set; }

        /// <summary>
        /// Определяет, должны ли быть контролы, построенные на базе данной секции, быть скрыты.
        /// </summary>
        bool IsHidden { get; set; }

        /// <summary>
        /// Определяет, должны ли быть контролы, построенные на базе данной секции, быть отображены.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Определяет, должна ли секция быть обязательной для заполнения.
        /// </summary>
        bool IsMandatory { get; set; }

        /// <summary>
        /// Определяет, замаскированы ли все данные секции.
        /// </summary>
        bool IsMasked { get; set; }

        /// <summary>
        /// Список полей, доступных для редактирования.
        /// </summary>
        IReadOnlyCollection<Guid> AllowedFields { get; set; }

        /// <summary>
        /// Список полей, запрещенных для редактирования.
        /// </summary>
        IReadOnlyCollection<Guid> DisallowedFields { get; set; }

        /// <summary>
        /// Список полей, для которых должны быть скрыты контролы, построенные на базе них.
        /// </summary>
        IReadOnlyCollection<Guid> HiddenFields { get; set; }

        /// <summary>
        /// Список полей, для которых должны быть отображены контролы, построенные на базе них.
        /// </summary>
        IReadOnlyCollection<Guid> VisibleFields { get; set; }

        /// <summary>
        /// Список полей обязательных для заполнения.
        /// </summary>
        IReadOnlyCollection<Guid> MandatoryFields { get; set; }

        /// <summary>
        /// Список замаскированных полей.
        /// </summary>
        IReadOnlyCollection<Guid> MaskedFields { get; set; }
    }
}
