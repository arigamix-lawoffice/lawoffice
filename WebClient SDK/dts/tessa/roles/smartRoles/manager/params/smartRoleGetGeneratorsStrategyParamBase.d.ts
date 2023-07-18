import { StorageObject, IStorage, IStorageMapProvider } from 'tessa/platform/storage';
/**
 * Параметры для получения списка генераторов для перерасчёта умных ролей.
 */
export interface ISmartRoleGetGeneratorsStrategyParam extends IStorageMapProvider {
    /**
     * Имя стратегии, которая обрабатывает данный параметр.
     */
    readonly strategyName: string;
}
/**
 * Базовая реализация параметров для получения списка генераторов умных ролей для перерасчёта умных ролей.
 */
export declare abstract class SmartRoleGetGeneratorsStrategyParamBase extends StorageObject implements ISmartRoleGetGeneratorsStrategyParam {
    constructor(storage?: IStorage);
    /** @inheritdoc */
    get strategyName(): string;
    /** @inheritdoc */
    protected abstract get strategyNameCore(): string;
}
