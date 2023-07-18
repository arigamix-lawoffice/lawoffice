using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Collections;
using Tessa.Platform.Conditions;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Настройки правил доступа.
    /// </summary>
    public interface IKrPermissionRuleSettings
    {
        /// <summary>
        /// Идентификатор правила доступа.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Список идентификаторов типов правила доступа.
        /// </summary>
        HashSet<Guid> Types { get; }

        /// <summary>
        /// Список идентификаторов состояний правила доступа.
        /// </summary>
        HashSet<int> States { get; }

        /// <summary>
        /// Определяет, что данное правило помечено как обязательное для проверки.
        /// </summary>
        bool IsRequired { get; }

        /// <summary>
        /// Определяет, что данное правило помечено как расширенное.
        /// </summary>
        bool IsExtended { get; }

        /// <summary>
        /// Приоритет настроек правил доступа. Правила с более высоким приоритетом перезатируют настройки с более низким.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Список флагов правила доступа.
        /// </summary>
        HashSet<KrPermissionFlagDescriptor> Flags { get; }

        /// <summary>
        /// Условия правила доступа.
        /// </summary>
        IEnumerable<ConditionSettings> Conditions { get; }

        /// <summary>
        /// Список контекстных ролей правила доступа.
        /// </summary>
        ICollection<Guid> ContextRoles { get; }

        /// <summary>
        /// Набор настроек секций для карточек.
        /// </summary>
        HashSet<Guid, KrPermissionSectionSettings> CardSettings { get; }

        /// <summary>
        /// Набор настроек секций для заданий по типам заданий.
        /// </summary>
        Dictionary<Guid, HashSet<Guid, KrPermissionSectionSettings>> TaskSettingsByTypes { get; }

        /// <summary>
        /// Набор правил проверки обязательности полей.
        /// </summary>
        ICollection<KrPermissionMandatoryRule> MandatoryRules { get; }

        /// <summary>
        /// Набор настроек видимости контролов.
        /// </summary>
        ICollection<KrPermissionVisibilitySettings> VisibilitySettings { get; }

        /// <summary>
        /// Набор правил доступа к файлам.
        /// </summary>
        ICollection<KrPermissionsFileRule> FileRules { get; }
    }
}
