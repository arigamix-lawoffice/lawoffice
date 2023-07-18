import { ValidationResultType } from './validationResultType';
import { ValidationResult } from './validationResult';
import { IValidationResultItem } from './validationResultItem';
export interface IValidationResultBuilder {
    readonly items: ReadonlyArray<IValidationResultItem>;
    readonly isSuccessful: boolean;
    add(type: ValidationResultType, message: string, fieldName?: string, objectName?: string, objectType?: string, details?: string, key?: guid): IValidationResultBuilder;
    add(validationResult: ValidationResult): IValidationResultBuilder;
    add(validationResultBuilder: IValidationResultBuilder): IValidationResultBuilder;
    build(): ValidationResult;
    clear(): any;
}
export declare class ValidationResultBuilder implements IValidationResultBuilder {
    private _items;
    get items(): ReadonlyArray<IValidationResultItem>;
    get isSuccessful(): boolean;
    get hasData(): boolean;
    add(type: ValidationResultType, message: string, fieldName?: string, objectName?: string, objectType?: string, details?: string, key?: guid): IValidationResultBuilder;
    add(validationResult: ValidationResult): IValidationResultBuilder;
    add(validationResultBuilder: IValidationResultBuilder): IValidationResultBuilder;
    private addWithArgs;
    private addWithValidationResult;
    private addWithValidationResultBuilder;
    build(): ValidationResult;
    clear(): void;
}
