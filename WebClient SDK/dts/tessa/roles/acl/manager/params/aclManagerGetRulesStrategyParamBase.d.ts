import { IStorageMapProvider, StorageObject, IStorage } from 'tessa/platform/storage';
/**
 * Параметр для стратегии <see cref="IAclManagerGetRulesStrategy"/>.
 */
export interface IAclManagerGetRulesStrategyParam extends IStorageMapProvider {
    /**
     * Имя стратегии, которая обрабатывает данный параметр.
     */
    strategyName: string;
}
/**
 * Базовая реализация параметров для получения списка правил для перерасчёта ACL.
 */
export declare abstract class AclManagerGetRulesStrategyParamBase extends StorageObject implements IAclManagerGetRulesStrategyParam {
    constructor(storage: IStorage);
    /** @inheritdoc */
    get strategyName(): string;
    /** @inheritdoc */
    protected abstract get strategyNameCore(): string;
}
