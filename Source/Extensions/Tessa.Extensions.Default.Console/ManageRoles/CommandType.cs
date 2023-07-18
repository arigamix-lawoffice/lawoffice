using Tessa.Localization;

namespace Tessa.Extensions.Default.Console.ManageRoles
{
    /// <summary>
    /// Тип вызываемой команды.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Пересчёт замещений для всех ролей, кроме динамических ролей и метаролей.
        /// </summary>
        [LocalizableDescription("Common_CLI_ManageRoles_SyncAllDeputies")]
        SyncAllDeputies,
        
        /// <summary>
        /// Пересчёт всех динамических ролей.
        /// </summary>
        [LocalizableDescription("Common_CLI_ManageRoles_RecalcAllDynamicRoles")]
        RecalcAllDynamicRoles,
        
        /// <summary>
        /// Пересчёт метаролей для всех генераторов метаролей.
        /// </summary>
        [LocalizableDescription("Common_CLI_ManageRoles_RecalcAllRoleGenerators")]
        RecalcAllRoleGenerators,
        
        /// <summary>
        /// Пересчёт указанных динамических ролей.
        /// </summary>
        [LocalizableDescription("Common_CLI_ManageRoles_RecalcDynamicRoles")]
        RecalcDynamicRoles,
        
        /// <summary>
        /// Пересчёт метаролей для указанных генераторов метаролей.
        /// </summary>
        [LocalizableDescription("Common_CLI_ManageRoles_RecalcRoleGenerators")]
        RecalcRoleGenerators,
    }
}