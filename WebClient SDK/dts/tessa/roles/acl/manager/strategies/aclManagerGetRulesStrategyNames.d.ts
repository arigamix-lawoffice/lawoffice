/**
 * Имена стратегий получения правил расчёта ACL для обновления.
 */
export declare class AclManagerGetRulesStrategyNames {
    /**
     * Имя стратегии по умолчанию.
     */
    static readonly DefaultStrategy = "DefaultStrategy";
    /**
     * Имя стратегии, которая возвращает правила расчёта ACL по триггерам правил при изменении карточки.
     */
    static readonly TriggerCardStrategy = "TriggerCardStrategy";
    /**
     * Имя стратегии, которая возвращает только одно правило расчёта ACL по идентификатору.
     */
    static readonly RuleStrategy = "RuleStrategy";
    /**
     * Имя стратегии, которая возвращает правила расчёта ACL по списку идентификаторов.
     */
    static readonly RulesStrategy = "RulesStrategy";
    /**
     * Имя стратегии, которая возвращает правила расчёта ACL по идентификатора генератора умных ролей.
     */
    static readonly SmartRoleGeneratorStrategy = "SmartRoleGeneratorStrategy";
}
