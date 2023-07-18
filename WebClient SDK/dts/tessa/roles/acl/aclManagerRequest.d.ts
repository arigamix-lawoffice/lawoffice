import { StorageObject, IStorage } from 'tessa/platform/storage';
import { IAclManagerGetCardsStrategyParam } from './manager/params';
import { IAclManagerGetRulesStrategyParam } from './manager';
/**
 * Запрос на расчёт Acl в <see cref="IAclManager"/>.
 */
export declare class AclManagerRequest extends StorageObject {
    private getCardsParamLocal;
    private getRulesParamLocal;
    constructor(storage?: IStorage, getRulesParam?: IAclManagerGetRulesStrategyParam | null, getCardsParam?: IAclManagerGetCardsStrategyParam | null);
    static readonly getRulesStrategyKey: string;
    static readonly getRulesParamKey: string;
    static readonly getCardsStrategyKey: string;
    static readonly getCardsParamKey: string;
    static readonly canDeferKey: string;
    static readonly deferFromKey: string;
    static readonly fullRecalculationOfRulesKey: string;
    static readonly fullRecalculationOfCardsKey: string;
    static readonly ignoreRuleCheckKey: string;
    static readonly ignoreOperationProgressKey: string;
    /**
     * Данные с параметром для получения правил для обновления ACL.
     */
    get getRulesParam(): IStorage;
    /**
     * Данные с параметром для получения правил для обновления ACL.
     */
    set getRulesParam(value: IStorage);
    /**
     * Имя стратегии для получения правил для обновления ACL.
     */
    get getRulesStrategy(): string;
    /**
     * Имя стратегии для получения правил для обновления ACL.
     */
    set getRulesStrategy(value: string);
    /**
     * Данные с параметром для получения карточек для обновления ACL.
     */
    get getCardsParam(): IStorage;
    /**
     * Данные с параметром для получения карточек для обновления ACL.
     */
    set getCardsParam(value: IStorage);
    /**
     * Имя стратегии для получения правил для обновления ACL.
     */
    get getCardsStrategy(): string;
    /**
     * Имя стратегии для получения правил для обновления ACL.
     */
    set getCardsStrategy(value: string);
    /**
     * Определяет, нужно ли полностью перерасчитать ACL для правил, полученных по параметру <see cref="GetRulesParam"/>.
     */
    get fullRecalculationOfRules(): boolean;
    set fullRecalculationOfRules(value: boolean);
    /**
     * Определяет, нужно ли полностью перерасчитать ACL для карточек, полученных по параметру <see cref="GetCardsParam"/>.
     * Флаг используется только для стратегий получения карточек, которые не зависят от правил.
     */
    get fullRecalculationOfCards(): boolean;
    set fullRecalculationOfCards(value: boolean);
    /**
     * Определяет, нужно ли игнорировать проверки правила расчёта ACL для возможности расчёта ACL карточки.
     */
    get ignoreRuleCheck(): boolean;
    set ignoreRuleCheck(value: boolean);
    /**
     * Флаг определяет, можно ли данный запрос обработать отложенно. Не передается с запросом при отложенной отправке.
     */
    get canDefer(): boolean;
    /**
     * Флаг определяет, можно ли данный запрос обработать отложенно. Не передается с запросом при отложенной отправке.
     */
    set canDefer(value: boolean);
    /**
     * Определяет, начиная с какого числа карточек, которым нужно обновить ACL, перерасчёт будет выполняться отложенно.
     * Если не задано, то используется значение по умолчанию (100).
     */
    get deferFrom(): number | null;
    /**
     * Определяет, начиная с какого числа карточек, которым нужно обновить ACL, перерасчёт будет выполняться отложенно.
     * Если не задано, то используется значение по умолчанию (100).
     */
    set deferFrom(value: number | null);
    /**
     * Признак того, что при обработке запроса не нужно выводить прогресс операции.
     */
    get ignoreOperationProgress(): boolean;
    set ignoreOperationProgress(value: boolean);
    /**
     * Метод для установки в запрос параметра для получения карточек для обновления ACL.
     * @param param Параметр для получения правил для обновления ACL.
     */
    setGetCardsParam(param: IAclManagerGetCardsStrategyParam): void;
    /**
     * Метод для установки в запрос параметра для получения правил для обновления ACL.
     * @param param Параметр для получения правил для обновления ACL.
     */
    setGetRulesParam(param: IAclManagerGetRulesStrategyParam): void;
    /**
     * Метод для получения объекта параметра для получения правил для обновления ACL, если объект задан.
     * Если объект не задан, его следует восстановить через его данные <see cref="GetRulesParam"/> с помощью стратегии.
     * @returns true, если удалось получить параметр, иначе false.
     */
    tryGetGetRulesParam(): IAclManagerGetRulesStrategyParam | null;
    /**
     * Метод для получения объекта параметра для получения карточек для обновления ACL, если объект задан.
     * Если объект не задан, его следует восстановить через его данные <see cref="GetCardsParam"/> с помощью стратегии.
     *
     * @returns true, если удалось получить параметр, иначе false.
     */
    tryGetGetCardsParam(): IAclManagerGetCardsStrategyParam | null;
}
