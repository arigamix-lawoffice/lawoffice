import { SmartRoleGetGeneratorsStrategyParamBase } from './smartRoleGetGeneratorsStrategyParamBase';
import { IStorage } from 'tessa/platform/storage';
/**
 * Реализация параметра для получения одного генератора для перерасчёта.
 */
export declare class GetGeneratorsByGeneratorIDParam extends SmartRoleGetGeneratorsStrategyParamBase {
    constructor(storage: IStorage);
    static readonly generatorIDKey: string;
    /**  @inheritdoc */
    protected get strategyNameCore(): string;
    /**
     * Идентификатор генератора умных ролей, который необходимо обновить.
     */
    get generatorID(): guid;
    /**
     * Идентификатор генератора умных ролей, который необходимо обновить.
     */
    set generatorID(value: guid);
}
