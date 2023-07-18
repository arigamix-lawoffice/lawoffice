import { ValidationStorageObject } from './validationStorageObject';
import { ValidationStorageResultItem } from './validationStorageResultItem';
import { ValidationResultType } from './validationResultType';
import { ValidationResult } from './validationResult';
import { IValidationResultBuilder } from './validationResultBuilder';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
export declare class ValidationStorageResultBuilder extends ValidationStorageObject implements IValidationResultBuilder {
    constructor(storage?: IStorage);
    static readonly itemsKey = "Items";
    get isSuccessful(): boolean;
    get hasData(): boolean;
    get items(): ArrayStorage<ValidationStorageResultItem>;
    set items(value: ArrayStorage<ValidationStorageResultItem>);
    tryGetItems(): ArrayStorage<ValidationStorageResultItem> | null | undefined;
    private static readonly _itemFactory;
    clean(): void;
    add(type: ValidationResultType, message: string, fieldName?: string, objectName?: string, objectType?: string, details?: string, key?: guid): IValidationResultBuilder;
    add(validationResult: ValidationResult): IValidationResultBuilder;
    add(validationResultBuilder: IValidationResultBuilder): IValidationResultBuilder;
    private addWithArgs;
    private addWithValidationResult;
    private addWithValidationResultBuilder;
    build(): ValidationResult;
    clear(): void;
}
