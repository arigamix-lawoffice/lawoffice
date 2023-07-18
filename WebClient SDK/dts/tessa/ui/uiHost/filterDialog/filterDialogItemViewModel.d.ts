import { FilterDialogCriteriaViewModel } from './filterDialogCriteriaViewModel';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
export declare class FilterDialogItemViewModel {
    constructor(parameter: RequestParameter);
    readonly parameter: RequestParameter;
    readonly values: FilterDialogCriteriaViewModel[];
    get caption(): string;
    get readOnly(): boolean;
    get isHidden(): boolean;
    get isMultiply(): boolean;
}
