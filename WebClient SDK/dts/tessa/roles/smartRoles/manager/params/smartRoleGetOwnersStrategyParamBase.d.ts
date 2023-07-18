import { StorageObject, IStorage, IStorageMapProvider } from 'tessa/platform/storage';
/**
 * Базовая реализация параметров для получения списка владельцев умных ролей для перерасчёта умных ролей.
 */
export interface ISmartRoleGetOwnersStrategyParam extends IStorageMapProvider {
    /**
     * Имя стратегии, которая обрабатывает данный параметр.
     */
    readonly strategyName: string;
}
/**
 * Базовая реализация параметров для получения списка владельцев умных ролей для перерасчёта умных ролей.
 */
export declare abstract class SmartRoleGetOwnersStrategyParamBase extends StorageObject implements ISmartRoleGetOwnersStrategyParam {
    constructor(storage?: IStorage);
    /** @inheritdoc */
    get strategyName(): string;
    /** @inheritdoc */
    protected abstract get strategyNameCore(): string;
}
