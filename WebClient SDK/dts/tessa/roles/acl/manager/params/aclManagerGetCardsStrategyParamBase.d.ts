import { StorageObject, IStorage, IStorageMapProvider } from 'tessa/platform/storage';
/**
 * Параметры для получения списка карточек для обновления при перерасчёте ACL.
 */
export interface IAclManagerGetCardsStrategyParam extends IStorageMapProvider {
    /**
     * Имя стратегии, которая обрабатывает данный параметр.
     */
    readonly strategyName: string;
}
/**
 * Базовая реализация параметров для получения списка карточек для перерасчёта ACL.
 */
export declare abstract class AclManagerGetCardsStrategyParamBase extends StorageObject implements IAclManagerGetCardsStrategyParam {
    constructor(storage?: IStorage);
    /** @inheritdoc */
    get strategyName(): string;
    /** @inheritdoc */
    protected abstract get strategyNameCore(): string;
}
