using Tessa.Localization;

namespace Tessa.Extensions.Default.Console.TimeZone
{
    public enum OperationFunction
    {
        /// <summary>
        /// Изменение временной зоны из справочника.
        /// </summary>
        [LocalizableDescription("Common_CLI_TimeZone_Update")]
        Update,

        /// <summary>
        /// Заполнение временных зон из справочника .NET.
        /// </summary>
        [LocalizableDescription("Common_CLI_TimeZone_GenerateFromSystem")]
        GenerateFromSystem,

        /// <summary>
        /// Установка временной зоны "По умолчанию" (Default) для всех ролей.
        /// </summary>
        [LocalizableDescription("Common_CLI_TimeZone_SetDefaultForAllRoles")]
        SetDefaultForAllRoles,

        /// <summary>
        /// Проверка и обновление цепочек наследования временных зон.
        /// </summary>
        [LocalizableDescription("Common_CLI_TimeZone_UpdateInheritance")]
        UpdateInheritance,

        /// <summary>
        /// Проверка и обновление смещений временных зон в ролях на соответсвие справочнику.
        /// </summary>
        [LocalizableDescription("Common_CLI_TimeZone_UpdateOffsets")]
        UpdateOffsets
    }
}