import { ValidationResultType } from './validationResultType';
import { ValidationLevel } from './validationLevel';
export interface IValidationResultItem {
    readonly type: ValidationResultType;
    readonly message: string;
    readonly fieldName: string;
    readonly objectName: string;
    readonly objectType: string;
    readonly details: string;
    readonly key: guid;
}
export declare class ValidationResultItem implements IValidationResultItem {
    constructor(args: {
        type: ValidationResultType;
        message: string;
        fieldName?: string;
        objectName?: string;
        objectType?: string;
        details?: string;
        key?: guid;
    });
    readonly type: ValidationResultType;
    readonly message: string;
    readonly fieldName: string;
    readonly objectName: string;
    readonly objectType: string;
    readonly details: string;
    readonly key: guid;
    toString(level?: ValidationLevel): string;
    private static getMessage;
}
