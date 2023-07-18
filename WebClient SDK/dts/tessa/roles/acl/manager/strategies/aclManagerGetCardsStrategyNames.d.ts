/**
 * Список имен стратегий получения карточек <see cref="IAclManagerGetCardsStrategy"/>.
 */
export declare class AclManagerGetCardsStrategyNames {
    /**
     * Имя стратегии по умолчанию.
     */
    static readonly DefaultStrategy: string;
    /**
     * Имя стратегии, производящей получение карточек по триггерам правил при изменении карточки.
     */
    static readonly TriggerCardStrategy: string;
    /**
     * Имя стратегии, производящей получение карточек по конкретным триггерам.
     */
    static readonly TriggersStrategy: string;
    /**
     * Имя стратегии, производящей получение конкретных карточек.
     */
    static readonly CardsStretegy: string;
    /**
     * Имя стратегии, производящей получение карточек при добавлении новых умных ролей.
     */
    static readonly SmartRolesInsertStretegy: string;
}
