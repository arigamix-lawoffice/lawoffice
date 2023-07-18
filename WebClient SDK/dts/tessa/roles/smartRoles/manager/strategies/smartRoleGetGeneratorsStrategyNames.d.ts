/**
 * Набор имен стратегий для получения генераторов умных ролей для перерасчёта умных ролей.
 */
export declare class SmartRoleGetGeneratorsStrategyNames {
    /**
     * Имя стратегии по умолчанию.
     */
    static readonly DefaultStrategy = "DefaultStrategy";
    /**
     * Имя стратегии, которая возвращает генераторы умных ролей по триггерам генераторов при изменении карточки.
     */
    static readonly TriggerCardStrategy = "TriggerCardStrategy";
    /**
     * Имя стратегии, которая возвращает только один генератор умных ролей по идентификатору.
     */
    static readonly GeneratorStrategy = "GeneratorStrategy";
    /**
     * Имя стратегии, которая возвращает генераторы умных ролей по списку идентификаторов.
     */
    static readonly GeneratorsStrategy = "GeneratorsStrategy";
    /**
     * Имя стратегии, которая возвращает генераторы умных ролей по списку идентификаторов умных ролей.
     */
    static readonly SmartRolesStretegy = "SmartRolesStretegy";
}
