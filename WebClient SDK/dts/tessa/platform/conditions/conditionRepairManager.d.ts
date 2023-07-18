import { ValidationResult } from 'tessa/platform/validation';
/**
 * Менеджер починки условий.
 * */
export interface IConditionRepairManager {
    /**
     * Выполняет починку условий для указанных типов условий во всех карточках.
     *
     * @param conditionTypeIDs Идентификаторы типов условий, для которых нужно починить условия, или null,
     * если нужно починить условия для всех типов условий.
     * @returns Результат валидации починки условий.
     * */
    repairConditionTypes(conditionTypeIDs: guid[] | null): Promise<ValidationResult>;
}
export declare class ConditionRepairManager implements IConditionRepairManager {
    private constructor();
    private static _instance;
    static get instance(): ConditionRepairManager;
    repairConditionTypes(conditionTypeIDs: guid[] | null): Promise<ValidationResult>;
}
