using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Перечисление режимов вывода информации об изменениях в маршруте после пересчёта.
    /// </summary>
    [Flags]
    public enum InfoAboutChanges
    {
        /// <summary>
        /// В response.Info не будет информации об изменениях маршрута
        /// после пересчета.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Булевое значение о наличии изменений будет помещено в response.Info[KrCompilersInfo.HasChangesInRoute].
        /// </summary>
        HasChangesToInfo = 0x1,

        /// <summary>
        /// Список изменений RouteDiff будет помещен в response.Info[KrCompilersInfo.RouteChanges].
        /// </summary>
        ChangesListToInfo = 0x2,

        /// <summary>
        /// Информация о наличии изменений в маршруте будет помещена в response.ValidationResult в читаемом виде.
        /// </summary>
        HasChangesToValidationResult = 0x4,

        /// <summary>
        /// Список изменений будет помещен в response.ValidationResult в читаемом виде.
        /// </summary>
        ChangesListToValidationResult = 0x8,

        /// <summary>
        /// В список изменений будут включены изменения в скрытых этапах.
        /// </summary>
        ChangesInHiddenStages = 0x16,

        /// <summary>
        /// Логическое значение о наличии изменений будет помещено в response.Info[KrCompilersInfo.HasChangesInRoute].
        /// Список изменений RouteDiff будет помещен в response.Info[KrCompilersInfo.RouteChanges].
        /// </summary>
        ToInfo = HasChangesToInfo | ChangesListToInfo,

        /// <summary>
        /// Аналогично <see cref="ToInfo" /> с учетом скрытых этапов.
        /// </summary>
        ToInfoIncludingHiddenStages = ToInfo | ChangesInHiddenStages,

        /// <summary>
        /// Информация о наличии изменений в маршруте будет помещена в response.ValidationResult в читаемом виде.
        /// </summary>
        ToValidationResult = HasChangesToValidationResult | ChangesListToValidationResult,

        /// <summary>
        /// Аналогично <see cref="ToValidationResult" /> с учетом скрытых этапов.
        /// </summary>
        ToValidationResultIncludingHiddenStages = ToValidationResult | ChangesInHiddenStages,

        /// <summary>
        /// Список изменений RouteDiff будет помещен в response.Info[KrCompilersInfo.RouteChanges].<para/>
        /// Список изменений будет помещен в response.ValidationResult в читаемом виде.<para/>
        /// Во все списки изменений будут включены скрытые этапы.
        /// </summary>
        Full = ToInfoIncludingHiddenStages | ToValidationResultIncludingHiddenStages,
    }
}