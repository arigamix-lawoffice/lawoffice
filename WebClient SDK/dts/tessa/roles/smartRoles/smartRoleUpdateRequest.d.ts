import { StorageObject, IStorage } from 'tessa/platform/storage';
import { ISmartRoleGetGeneratorsStrategyParam, ISmartRoleGetOwnersStrategyParam } from './manager';
/**
 * Запрос на обновление состава умных ролей в <see cref="ISmartRoleManager"/>
 */
export declare class SmartRoleUpdateRequest extends StorageObject {
    private getGeneratorsParamLocal;
    private getOwnersParamLocal;
    constructor(storage?: IStorage, getGeneratorsParam?: ISmartRoleGetGeneratorsStrategyParam | null, getOwnersParam?: ISmartRoleGetOwnersStrategyParam | null);
    static readonly getGeneratorsStrategyKey: string;
    static readonly getGeneratorsParamKey: string;
    static readonly getOwnersStrategyKey: string;
    static readonly getOwnersParamKey: string;
    static readonly canDeferKey: string;
    static readonly deferFromKey: string;
    static readonly fullRecalculateKey: string;
    static readonly checkInitRolesKey: string;
    static readonly ignoreOperationProgressKey: string;
    /**
     * Данные с параметром для получения правил для обновления ACL.
     */
    get getGeneratorsParam(): IStorage;
    /**
     * Данные с параметром для получения правил для обновления ACL.
     */
    set getGeneratorsParam(value: IStorage);
    /**
     * Данные с параметром для получения правил для обновления ACL.
     */
    get getGeneratorsStrategy(): string;
    /**
     * Данные с параметром для получения правил для обновления ACL.
     */
    set getGeneratorsStrategy(value: string);
    /**
     * Данные с параметром для получения карточек для обновления ACL.
     */
    get getOwnersParam(): IStorage;
    /**
     * Данные с параметром для получения карточек для обновления ACL.
     */
    set getOwnersParam(value: IStorage);
    /**
     * Имя стратегии для получения правил для обновления ACL.
     */
    get getOwnersStrategy(): string;
    /**
     * Имя стратегии для получения правил для обновления ACL.
     */
    set getOwnersStrategy(value: string);
    /**
     * Определяет, нужно ли проверять флаг <see cref="ISmartRoleGenerator.InitRoles"/> для создания новых умных ролей при перерасчёте.
     * Если не установлен, умные роли создаются всегда.
     */
    get checkInitRoles(): boolean;
    /**
     * Определяет, нужно ли проверять флаг <see cref="ISmartRoleGenerator.InitRoles"/> для создания новых умных ролей при перерасчёте.
     * Если не установлен, умные роли создаются всегда.
     */
    set checkInitRoles(value: boolean);
    /**
     * Определяет, нужно ли полностью перерасчитать Acl для правила <see cref="RuleID"/>.
     */
    get fullRecalculate(): boolean;
    /**
     * Определяет, нужно ли полностью перерасчитать Acl для правила <see cref="RuleID"/>.
     */
    set fullRecalculate(value: boolean);
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
     * Метод для установки в запрос параметра для получения владельцев умных ролей для перерасчёта умных ролей.
     * @param param Параметр для получения владельцев умных ролей для перерасчёта умных ролей.
     */
    setGetOwnersParam(param: ISmartRoleGetOwnersStrategyParam): void;
    /**
     * Метод для установки в запрос параметра для получения генераторов для перерасчёта умных ролей.
     * @param param Параметр для получения генераторов для перерасчёта умных ролей.
     */
    setGetGeneratorsParam(param: ISmartRoleGetGeneratorsStrategyParam): void;
    /**
     * Метод для получения объекта параметра для получения генераторов для парерасчёта умных ролей, если объект задан.
     * Если объект не задан, его следует восстановить через его данные <see cref="GetGeneratorsParam"/> с помощью стратегии.
     * @returns Параметр для получения генераторов для парерасчёта умных ролей.
     */
    tryGetGetGeneratorsParam(): ISmartRoleGetGeneratorsStrategyParam;
    /**
     * Метод для получения объекта параметра для получения владельцев умных ролей для перерасчёта умных ролей, если объект задан.
     * Если объект не задан, его следует восстановить через его данные <see cref="GetOwnersParam"/> с помощью стратегии.
     * @returns Параметр для получения владельцев умных ролей для перерасчёта умных ролей.
     */
    tryGetGetOwnersParam(): ISmartRoleGetOwnersStrategyParam;
}
