/**
 * Набор имен стратегий для получения владельцев умных ролей для перерасчёта умных ролей.
 */
export declare class SmartRoleGetOwnersStrategyNames {
    /**
     * Имя стратегии по умолчанию.
     */
    static readonly DefaultStrategy = "DefaultStrategy";
    /**
     * Имя стратегии, производящей получение владельцев умных ролей по триггерам генераторов при изменении карточки.
     */
    static readonly TriggerCardStrategy = "TriggerCardStrategy";
    /**
     * Имя стратегии, производящей получение владельцев умных ролей по конкретным триггерам.
     */
    static readonly TriggersStrategy = "TriggersStrategy";
    /**
     * Имя стратегии, производящей получение конкретных владельцев умных ролей.
     */
    static readonly OwnersStretegy = "OwnersStretegy";
    /**
     * Имя стратегии, производящей получение владельцев умных ролей по списку умных ролей.
     */
    static readonly SmartRolesStretegy = "SmartRolesStretegy";
}
