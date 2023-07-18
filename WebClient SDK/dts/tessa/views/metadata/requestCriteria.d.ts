import { CriteriaValue } from './criteriaValue';
export declare class RequestCriteria {
    constructor();
    criteriaName: string | null;
    readOnly: boolean;
    values: CriteriaValue[];
    clone(): RequestCriteria;
    cloneAsReadOnly(): RequestCriteria;
}
