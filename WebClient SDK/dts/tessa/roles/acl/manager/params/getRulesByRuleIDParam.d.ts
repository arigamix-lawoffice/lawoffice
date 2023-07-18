import { AclManagerGetRulesStrategyParamBase } from './aclManagerGetRulesStrategyParamBase';
import { IStorage } from 'tessa/platform/storage';
/**
 * Реализация параметра для получения одного правила для перерасчёта.
 */
export declare class GetRulesByRuleIDParam extends AclManagerGetRulesStrategyParamBase {
    constructor(storage: IStorage);
    static readonly ruleIDKey: string;
    /**  @inheritdoc */
    protected get strategyNameCore(): string;
    /**
     * Идентификатор правила расчёта ACL, который необходимо обновить
     */
    get ruleID(): guid;
    /**
     * Идентификатор правила расчёта ACL, который необходимо обновить
     */
    set ruleID(value: guid);
}
